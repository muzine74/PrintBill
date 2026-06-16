using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Helpers
{
    public static class HelpersExtensions
    {
        public static (DateOnly StartOfWeek, DateOnly EndOfWeek) GetWeekDates(this DateOnly date)
        {
            // Semaine commence le lundi
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateOnly startOfWeek = date.AddDays(-diff);
            DateOnly endOfWeek = startOfWeek.AddDays(6);

            return (startOfWeek, endOfWeek);
        }
    }
}
