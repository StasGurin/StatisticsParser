using HtmlAgilityPack;
using System.Text;

namespace Operativ.BLL
{
    public abstract class Parser
    {
        public HtmlWeb WebGet { get; set; }
        public string Link { get; set; }

        public Parser()
        {
            var webSite = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = UTF8Encoding.GetEncoding("windows-1251")
            };
            WebGet = webSite;
        }

        public abstract HtmlNode abstractNode(string link);

    }
}
