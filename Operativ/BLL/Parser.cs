using HtmlAgilityPack;
using System.Text;
using Operativ.Models;
using Operativ.Extensions;

namespace Operativ.BLL
{
    public class Parser
    {
 
         public static Month Parse(string year, int n)
        {

            var webGet = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = UTF8Encoding.GetEncoding("windows-1251"),
            };
            var doc = webGet.Load("http://www.ukrstat.gov.ua/operativ/operativ"+ year +"/ct/bisc/bisc_u/bisc_"+ year +"u.htm");//get html-page for parse
            var bodyNode = doc.DocumentNode.SelectSingleNode("//table[@class ='MsoNormalTable']");
            Month record = new Month();
            if (bodyNode != null)
            {
                var node = bodyNode.SelectSingleNode(".//tr[" + n + "]");
                if (node != null)
                {
                    var nodeMounth = node.SelectSingleNode(".//td[1]");
                    var nodePersent = node.SelectSingleNode(".//td[2]"); //get persetns from html page
                    if ((nodeMounth != null)&&(nodePersent!=null))
                    {
                        var id = YearMonthExtensions.ToYearMonth(year, nodeMounth.InnerText);  //get id (format:yearmonth)
                        record.YearMonth = id;
                        record.Persent = nodePersent.InnerText.Trim();
                        
                    }
                }
            }
            return record;
        }
    }
}