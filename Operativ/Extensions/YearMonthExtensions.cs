using System;
using System.Collections.Generic;

namespace Operativ.Extensions
{
    public static class YearMonthExtensions
    {
     

        public static int ToYearMonth(this string year, string month)
        {
            Dictionary<string, int> monthYear = new Dictionary<string, int>();
            monthYear.Add("січень", 1);
            monthYear.Add("лютий", "02");
            monthYear.Add("березень", "03");
            monthYear.Add("квітень", "04");
            monthYear.Add("травень", "05");
            monthYear.Add("червень", "06");
            monthYear.Add("липень", "07");
            monthYear.Add("серпень", "08");
            monthYear.Add("вересень", "09");
            monthYear.Add("жовтень", "10");
            monthYear.Add("листопад", "11");
            monthYear.Add("грудень", "12");
            month = month.Trim();
            foreach (KeyValuePair<string, string> kvp in monthYear) 
            {
                if (kvp.Key.Equals(month)) year = year + kvp.Value.ToString("D2");
            }
            var yearInt = Convert.ToInt32(year);
            return yearInt;
         }
     }
}
