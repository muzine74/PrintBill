using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity
{
    public class WorkSchedule
    {
        public Guid WorkScheduleId { get; set; }

        public Guid CompanyWorkId { get; set; }

        public byte DayOfWeek { get; set; }     // 1 = Lundi ... 7 = Dimanche
        public byte? DayOfMonth { get; set; }   // Pour bi-mensuel
        public byte? WeekCycle { get; set; }    // 1 ou 2 (bi-hebdomadaire)

        public decimal Price { get; set; }

        public CompanyWork CompanyWork { get; set; } = null!;
    }
}
