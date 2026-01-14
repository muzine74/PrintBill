
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnection.Entity;


namespace DataBridge.Entity
{
    public class EmployeePoco
    {
        public EmployeePoco()
        {
            EmployeeCompagnies = new List<CompagniePoco>();

        }

        public Guid EmployeeId { get; set; }
        public string NAS { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeMail { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeNote { get; set; }


        #region Adress

        public Guid AddressId { get; set; }
        public string EmployeeCivicNumber { get; set; }
        public string EmployeeSuite { get; set; }
        public string EmployeeZipCode { get; set; }
        public string EmployeeCity { get; set; }
        public string EmployeeState { get; set; }
        public string EmployeeCountry { get; set; }
        public string EmployeeAdressNote { get; set; }

        #endregion
        public List<CompagniePoco> EmployeeCompagnies { get; set; }
        #region EmployeeListcompagnies


        #endregion

    }
}
