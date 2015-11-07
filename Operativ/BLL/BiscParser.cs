using HtmlAgilityPack;
using Operativ.Extensions;

namespace Operativ.BLL
{
    public class BiscParser : Parser
    {
        protected override void Init(string year)
        {
            ParseLineNumber = 3;
            Link = string.Format("http://www.ukrstat.gov.ua/operativ/operativ{0}/ct/bisc/bisc_u/bisc_{0}u.htm", year);
        }

        protected override void ParseBody(string year, HtmlNode bodyNode)
        {
            var node = bodyNode.SelectSingleNode(".//tr[" + ParseLineNumber + "]");
            if (node != null)
            {
                var nodeMounth = node.SelectSingleNode(".//td[1]");
                var nodePercent = node.SelectSingleNode(".//td[2]");
                if (nodeMounth != null && nodePercent != null)
                    Record.Init(year, nodeMounth.GetText(), nodePercent.GetText());
            }
        }
    }
}