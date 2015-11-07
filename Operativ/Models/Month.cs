using Operativ.Extensions;

namespace Operativ.Models
{
    public class Month
    {
        #region Properties

        public int YearMonth { get; set; }
        public string Percent { get; set; }
        public bool IsBreak { get { return Percent == "до грудня \r\n\t\tпопереднього року" || Percent == "&nbsp;"; } }

        #endregion

        #region General Functions

        public void Init(string year, string month, string percent)
        {
            YearMonth = year.ToYearMonth(month);
            Percent = percent;
        }

        #endregion
    }
}