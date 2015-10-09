using System;
using System.Collections.Generic;
using System.Linq;

namespace Operativ.Extensions
{
    public static class YearMonthExtensions
    {
        static Dictionary<string, int> monthYear = new Dictionary<string, int>()
            {
                {"січень", 1},
                {"cічень", 1},
                {"лютий", 2},
                {"березень", 3},
                {"квітень", 4},                    //dictionare of string month to integer
                {"травень", 5},
                {"червень", 6},
                {"липень", 7},
                {"серпень", 8},
                {"вересень", 9},
                {"жовтень", 10},
                {"листопад", 11},
                {"грудень", 12}
            };

        public static int ToYearMonth(this string year, string month)
        {
            
            month = month.Trim().ToLower();
            /*year = year + (from x in monthYear
                          where x.Key.Contains(month)
                          select x).ToString();*/
           // var monthLINQ = monthYear.SingleOrDefault(x => x.Key == month);
            if (monthYear.ContainsKey(month)) year+= monthYear[month].ToString("D2");
            /*foreach (var kvp in monthYear) 
            {
                if (kvp.Key.Equals(month)) year = year + kvp.Value.ToString("D2");
            }*/
            var yearInt = Convert.ToInt32(year);
            return yearInt;
         }
     }
}
