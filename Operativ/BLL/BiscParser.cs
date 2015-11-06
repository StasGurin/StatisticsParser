using HtmlAgilityPack;
using Operativ.Extensions;
using Operativ.Models;

namespace Operativ.BLL
{
    public class BiscParser : Parser
    {
        public static int parseLineNumber = 3;
        static Month record = new Month();
        static BiscParser webSite = new BiscParser();

        public static Month Parse(string year)
        {
            webSite.Link = "http://www.ukrstat.gov.ua/operativ/operativ" + year + "/ct/bisc/bisc_u/bisc_" + year + "u.htm";
            var bodyNode = webSite.abstractNode(webSite.Link);
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

        public override HtmlNode abstractNode(string link)
        {
            var docBisc = WebGet.Load(link);
            return docBisc.DocumentNode.SelectSingleNode("//table[@class ='MsoNormalTable']");
        }
    }
}