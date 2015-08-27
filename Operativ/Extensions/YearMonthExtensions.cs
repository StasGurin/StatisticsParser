using System;
using System.Collections.Generic;

namespace Operativ.Extensions
{
    public static class YearMonthExtensions
    {
     

        public static int ToYearMonth(this string year, string month)
        {
            var monthYear = new Dictionary<string, int>();
            monthYear.Add("січень", 1);
            monthYear.Add("лютий", 2);
            monthYear.Add("березень", 3);
            monthYear.Add("квітень", 4);                    //dictionare of string month to integer
            monthYear.Add("травень", 5);
            monthYear.Add("червень", 6);
            monthYear.Add("липень", 7);
            monthYear.Add("серпень", 8);
            monthYear.Add("вересень", 9);
            monthYear.Add("жовтень", 10);
            monthYear.Add("листопад", 11);
            monthYear.Add("грудень", 12);
            month = month.Trim();
            foreach (var kvp in monthYear) 
            {
                if (kvp.Key.Equals(month)) year = year + kvp.Value.ToString("D2");
            }
            var yearInt = Convert.ToInt32(year);
            return yearInt;
         }
     }
}
