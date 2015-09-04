using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Operativ.Models
{
    public class Month
    {
        #region Properties

        public int YearMonth { get; set; }
        public string Persent { get; set; }


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