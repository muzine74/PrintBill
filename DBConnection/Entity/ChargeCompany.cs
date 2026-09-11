using System;

namespace DBConnection.Entity
{
    public class ChargeCompany
    {
        public Guid    ChargeId   { get; set; }
        public Guid    CompanyId  { get; set; }
        public decimal Percentage { get; set; }

        public virtual Charge  Charge  { get; set; } = null!;
        public virtual Company Company { get; set; } = null!;
    }
}
