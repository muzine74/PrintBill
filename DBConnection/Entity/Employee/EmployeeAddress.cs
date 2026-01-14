namespace DBConnection.Entity
{
    public class EmployeeAddress
    {
        public Guid EmployeeId { get; set; }
        public Guid AddressId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Address Address { get; set; }
    }
}
