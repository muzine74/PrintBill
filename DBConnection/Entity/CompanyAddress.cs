namespace DBConnection.Entity
{
    public class CompanyAddress
    {
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public Guid AddressId { get; set; }        
        public virtual Address Address { get; set; }

        public string? note { get; set; }
    }
}
