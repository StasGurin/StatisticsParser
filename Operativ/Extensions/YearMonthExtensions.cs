using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Operativ.Extensions
{
    public static class YearMonthExtensions
    {
        static readonly Dictionary<string, int> MonthYear = new Dictionary<string, int>()
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
            if (MonthYear.ContainsKey(month))
                year += MonthYear[month].ToString("D2");
            return Convert.ToInt32(year);
        }

        public static string GetText(this HtmlNode node)
        {
            return node == null ? null : node.InnerText.Trim();
        }
     }
}
