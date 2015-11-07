using HtmlAgilityPack;
using Operativ.Extensions;
using Operativ.Models;

namespace Operativ.BLL
{
    public class IscParser : Parser
    {
        public static int parseLineNumber = 2;
        static Month record = new Month();
        static IscParser webSite = new IscParser();

        public static Month Parse(string year)
        {
            webSite.Link = "http://www.ukrstat.gov.ua/operativ/operativ" + year + "/ct/is_c/isc_u/isc" + year + "m_u.html";
            if (year == "2014") webSite.Link = "http://www.ukrstat.gov.ua/operativ/operativ" + year + "/ct/is_c/isc_u/isc" + year + "m_u_.html";//get html-page for parse
            var bodyNode = webSite.abstractNode(webSite.Link);
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

        public override HtmlNode abstractNode(string link)
        {
            var docBisc = WebGet.Load(link);
            return docBisc.DocumentNode.SelectSingleNode("//table[@class ='MsoNormalTable']");
        }
    }
}