using HtmlAgilityPack;
using System.Text;
using Operativ.Models;
using Operativ.Extensions;

namespace Operativ.BLL
{
    public class BiscParser
    {
        static int parseLineNumber = 3;

        public static Month Parse(string year)
        {
            Month record = new Month();
            var webGet = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = UTF8Encoding.GetEncoding("windows-1251"),
            };
            var link = "http://www.ukrstat.gov.ua/operativ/operativ" + year + "/ct/";
            var docBisc = webGet.Load(link + "bisc/bisc_u/bisc_" + year + "u.htm");//get html-page for parse
            var bodyNode = docBisc.DocumentNode.SelectSingleNode("//table[@class ='MsoNormalTable']");
            if (bodyNode != null)
            {
                var node = bodyNode.SelectSingleNode(".//tr[" + parseLineNumber + "]");
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
            parseLineNumber++;
            return record;
        }
    }
}