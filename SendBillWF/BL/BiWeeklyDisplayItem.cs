using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBillWF.BL
{
    public class BiWeeklyDisplayItem
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string BiWeek1_Monday { get; set; }
        public string BiWeek1_Tuesday { get; set; }
        public string BiWeek1_Wednesday { get; set; }
        public string BiWeek1_Thursday { get; set; }
        public string BiWeek1_Friday { get; set; }
        public string BiWeek1_Saturday { get; set; }
        public string BiWeek1_Sunday { get; set; }
        public string BiWeek2_Monday { get; set; }
        public string BiWeek2_Tuesday { get; set; }
        public string BiWeek2_Wednesday { get; set; }
        public string BiWeek2_Thursday { get; set; }
        public string BiWeek2_Friday { get; set; }
        public string BiWeek2_Saturday { get; set; }
        public string BiWeek2_Sunday { get; set; }

        // Propriétés pour contrôler l'édition
        public bool IsBiWeek1_MondayEditable { get; set; }
        public bool IsBiWeek1_TuesdayEditable { get; set; }
        public bool IsBiWeek1_WednesdayEditable { get; set; }
        public bool IsBiWeek1_ThursdayEditable { get; set; }
        public bool IsBiWeek1_FridayEditable { get; set; }
        public bool IsBiWeek1_SaturdayEditable { get; set; }
        public bool IsBiWeek1_SundayEditable { get; set; }

        public bool IsBiWeek2_MondayEditable { get; set; }
        public bool IsBiWeek2_TuesdayEditable { get; set; }
        public bool IsBiWeek2_WednesdayEditable { get; set; }
        public bool IsBiWeek2_ThursdayEditable { get; set; }
        public bool IsBiWeek2_FridayEditable { get; set; }
        public bool IsBiWeek2_SaturdayEditable { get; set; }
        public bool IsBiWeek2_SundayEditable { get; set; }
    }
}
