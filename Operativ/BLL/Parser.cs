using HtmlAgilityPack;
using System.Text;
using Operativ.Models;
using Operativ.Extensions;

namespace Operativ.BLL
{
    public class Parser
    {

        public static Month Parse(string year, int n, int i)
        {
            Month record = new Month();
            var webGet = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = UTF8Encoding.GetEncoding("windows-1251"),
            };
            var link = "http://www.ukrstat.gov.ua/operativ/operativ" + year + "/ct/";
            var docBisc = webGet.Load(link + "bisc/bisc_u/bisc_" + year + "u.htm");
            var docIsc = webGet.Load(link + "is_c/isc_u/isc" + year + "m_u.html");
            if (year == "2014") docIsc = webGet.Load(link + "is_c/isc_u/isc" + year + "m_u_.html");//get html-page for parse
            var bodyNode = docIsc.DocumentNode.SelectSingleNode("//table[@class ='MsoNormalTable']");
            if ((bodyNode != null) && (i == 0))
            {
                var nodeMounth = bodyNode.SelectSingleNode(".//tr[1]");
                var nodePercent = bodyNode.SelectSingleNode(".//tr[2]");
                if ((nodeMounth != null) && (nodePercent != null))
                {
                    var node = nodeMounth.SelectSingleNode(".//td[" + n + "]");
                    if (node != null)
                    {
                      //get id (format:yearmonth)
                        record.YearMonth = YearMonthExtensions.ToYearMonth(year, node.InnerText);

                    }
                    node = nodePercent.SelectSingleNode(".//td[" + n + "]");
                    if (node != null)
                    {
                        record.Percent = node.InnerText.Trim();
                    }
                }
            }

            if ((bodyNode != null) && (i == 1))
            {
                bodyNode = docBisc.DocumentNode.SelectSingleNode("//table[@class ='MsoNormalTable']");
                var node = bodyNode.SelectSingleNode(".//tr[" + n + "]");
                if (node != null)
                {
                    var nodeMounth = node.SelectSingleNode(".//td[1]");
                    var nodePercent = node.SelectSingleNode(".//td[2]"); //get percetns from html page
                    if ((nodeMounth != null) && (nodePercent != null))
                    {  
                        //get id (format:yearmonth)
                        record.YearMonth = YearMonthExtensions.ToYearMonth(year, nodeMounth.InnerText);
                        record.Percent = nodePercent.InnerText.Trim();

                    }
                }
            }
            return record;
        }
    }
}