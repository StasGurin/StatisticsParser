using HtmlAgilityPack;
using System.Text;
using Operativ.Models;
using Operativ.Extensions;

namespace Operativ.BLL
{
    public class IscParser
    {
        static int parseLineNumber = 2;

        public static Month Parse(string year)
        {
            Month record = new Month();
            var webGet = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = UTF8Encoding.GetEncoding("windows-1251"),
            };
            var link = "http://www.ukrstat.gov.ua/operativ/operativ" + year + "/ct/";
            var docIsc = webGet.Load(link + "is_c/isc_u/isc" + year + "m_u.html");
            if (year == "2014") docIsc = webGet.Load(link + "is_c/isc_u/isc" + year + "m_u_.html");//get html-page for parse
            var bodyNode = docIsc.DocumentNode.SelectSingleNode("//table[@class ='MsoNormalTable']");
            if (bodyNode != null)
            {
                var nodeMounth = bodyNode.SelectSingleNode(".//tr[1]");
                var nodePercent = bodyNode.SelectSingleNode(".//tr[2]");
                if ((nodeMounth != null) && (nodePercent != null))
                {
                    var node = nodeMounth.SelectSingleNode(".//td[" + parseLineNumber + "]");
                    if (node != null)
                    {
                        //get id (format:yearmonth)
                        record.YearMonth = YearMonthExtensions.ToYearMonth(year, node.InnerText);

                    }
                    node = nodePercent.SelectSingleNode(".//td[" + parseLineNumber + "]");
                    if (node != null)
                    {
                        record.Percent = node.InnerText.Trim();
                    }
                }
            }
            parseLineNumber++;
            return record;
        }
    }
}