using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillWF.BL
{
    public class WeeklyDisplayItem
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }

        // Propriétés pour contrôler l'édition
        public bool IsMondayEditable { get; set; }
        public bool IsTuesdayEditable { get; set; }
        public bool IsWednesdayEditable { get; set; }
        public bool IsThursdayEditable { get; set; }
        public bool IsFridayEditable { get; set; }
        public bool IsSaturdayEditable { get; set; }
        public bool IsSundayEditable { get; set; }
    }
}
