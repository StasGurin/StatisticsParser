using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text;
using Operativ.Models;

namespace Operativ.BLL
{
    public abstract class Parser
    {
        #region Properties

        public Month Record { get; set; }
        public HtmlWeb WebGet { get; set; }
        protected string Link { get; set; }
        protected int ParseLineNumber { get; set; }

        #endregion

        #region Constructors

        protected Parser()
        {
            WebGet = new HtmlWeb { AutoDetectEncoding = false, OverrideEncoding = Encoding.GetEncoding("windows-1251") };
        }

        #endregion

        #region Abstract Functions

        protected abstract void Init(string year);

        protected abstract void ParseBody(string year, HtmlNode bodyNode);

        #endregion

        #region General Functions

        public List<Month> Parse(string year)
        {
            Init(year);
            var months = new List<Month>();
            var docBisc = WebGet.Load(Link);
            var bodyNode = docBisc.DocumentNode.SelectSingleNode("//table[@class ='MsoNormalTable']");
            if (bodyNode != null)
            {
                do
                {
                    Record = new Month();
                    ParseBody(year, bodyNode);
                    ParseLineNumber++;
                    if(!Record.IsBreak)
                        months.Add(Record);
                } while (!Record.IsBreak);
            }
            return months;
        }

        #endregion
    }
}
