namespace DBConnection.Entity
{
    public class Address
    {
        public Address() {

            Employees =new List<Employee>();
            Companies = new List<Company>();

        }
        public Guid AddressId { get; set; }
        public string civicNumber { get; set; }
        public string? suite { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
        public string state { get; set; } = "QC";
        public string country { get; set; } = "canada";
        public string? notes { get; set; }

       
        // Propriété de navigation pour les employés associés à cette adresse
        public virtual ICollection<Company> Companies { get; set; }

        // Propriété de navigation pour les employés associés à cette adresse
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
