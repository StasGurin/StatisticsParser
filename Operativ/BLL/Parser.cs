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
            var nodeTrYear = bodyNode.SelectSingleNode(".//tr[1]");
            var nodeYear = nodeTrYear.SelectSingleNode(".//td[2]");
     
            Month record = new Month();

            if (bodyNode != null)
            {
                var node = bodyNode.SelectSingleNode(".//tr[" + n + "]");
                var nodeMounth = node.SelectSingleNode(".//td[1]");
                var id = YearMonthExtensions.ToYearMonth(year, nodeMounth.InnerText);  //get id (format:yearmonth)
                record.Id = id;
                var nodePersent = node.SelectSingleNode(".//td[2]"); //get persetns from html page
                record.Persent = nodePersent.InnerText.Trim();
            }  
            return record;
        }
    }
}