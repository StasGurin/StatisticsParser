using HtmlAgilityPack;
using Operativ.Extensions;

namespace Operativ.BLL
{
    public class IscParser : Parser
    {
        protected override void Init(string year)
        {
            ParseLineNumber = 2;
            Link = string.Format("http://www.ukrstat.gov.ua/operativ/operativ{0}/ct/is_c/isc_u/isc{0}m_u{1}.html", year, year == "2014" ? "_" : "");
        }

        protected override void ParseBody(string year, HtmlNode bodyNode)
        {
            var nodeMounth = bodyNode.SelectSingleNode(".//tr[1]");
            var nodePercent = bodyNode.SelectSingleNode(".//tr[2]");
            if (nodeMounth != null && nodePercent != null)
            {
                nodeMounth = nodeMounth.SelectSingleNode(".//td[" + ParseLineNumber + "]");
                nodePercent = nodePercent.SelectSingleNode(".//td[" + ParseLineNumber + "]");
                Record.Init(year, nodeMounth.GetText(), nodePercent.GetText());
            }
        }
    }
}