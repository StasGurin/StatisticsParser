namespace Operativ.Models
{
    public class Month
    {
        #region Properties

        public int YearMonth { get; set; }
        public string Percent { get; set; }

        #endregion

        #region Constructors

        public Month()
        {
        }

        public Month(int yearMonth)
        {
            YearMonth = yearMonth;
        }

        #endregion
    }
}