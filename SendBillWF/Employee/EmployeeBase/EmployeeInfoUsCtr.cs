using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBridge.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SendBillWF.Employee
{
    public partial class EmployeeInfoUsCtr : UserControl
    {
        EmployeePoco employeePoco;

        public Guid employeeId { get; set; }
        public Guid addressId { get; set; }

        public EmployeeInfoUsCtr()
        {
            InitializeComponent();
            employeePoco = new EmployeePoco();
           
        }

      
        public void FillSourceCompagnyByUser(List<CompagniePoco> SourceList, List<CompagniePoco> DestinationList)
        {
            relocateSelectionItemUsCtr1.FillSourceCompagnyByUser(SourceList, DestinationList);
        }


        //Employee Info Saisi
        public string NameEmployeeSaisi
        {
            get { return employeeUsCtr1.NameEmployee; }
            set { employeeUsCtr1.NameEmployee = value; }
        }

        public string MailEmployeeSaisi
        {
            get { return employeeUsCtr1.MailEmployee; }
            set { employeeUsCtr1.MailEmployee = value; }
        }

        public string PhoneEmployeeSaisi
        {
            get { return employeeUsCtr1.PhoneEmployee; }
            set { employeeUsCtr1.PhoneEmployee = value; }
        }

        public string NoteEmployeeSaisi
        {
            get { return employeeUsCtr1.NoteEmployee; }
            set { employeeUsCtr1.NoteEmployee = value; }
        }

        public string NasEmployeeSaisi
        {
            get { return employeeUsCtr1.NasEmployee; }
            set { employeeUsCtr1.NasEmployee = value; }
        }

        //Adress Info Saisi

        public string CivicNumberAdressSaisi
        {
            get { return addressUsctr1.civicNumberAdress; }
            set { addressUsctr1.civicNumberAdress = value; }
        }
        public string SuiteAdressSaisi
        {
            get { return addressUsctr1.suiteAdress; }
            set { addressUsctr1.suiteAdress = value; }
        }
        public string CityAdressSaisi
        {
            get { return addressUsctr1.cityAdress; }
            set { addressUsctr1.cityAdress = value; }
        }

        public string StateAdressSaisi
        {
            get { return addressUsctr1.stateAdress; }
            set { addressUsctr1.stateAdress = value; }
        }

        public string CountryAdressSaisi
        {
            get { return addressUsctr1.countryAdress; }
            set { addressUsctr1.countryAdress = value; }
        }
        public string ZipCodeAdressSaisi
        {
            get { return addressUsctr1.zipCodeAdress; }
            set { addressUsctr1.zipCodeAdress = value; }
        }
        public string NoteAdressSaisi
        {
            get { return addressUsctr1.noteAdress; }
            set { addressUsctr1.noteAdress = value; }
        }


        public void NasTxtDisable()
        {
            employeeUsCtr1.NasTxtDisable();
        }

        public List<CompagniePoco> SelectedCompanies { get; private set; } = new List<CompagniePoco>();

        public void AddCompany(CompagniePoco comp)
        {
            if (!SelectedCompanies.Any(c => c.CompagnieID == comp.CompagnieID))
                SelectedCompanies.Add(comp);
        }

        public void RemoveCompany(CompagniePoco comp)
        {
            var existing = SelectedCompanies.FirstOrDefault(c => c.CompagnieID == comp.CompagnieID);
            if (existing != null)
                SelectedCompanies.Remove(existing);
        }

        public EmployeePoco SaveEmployee()
        {
            List<CompagniePoco> SelectItemDestiniationList = relocateSelectionItemUsCtr1.GetSelectItemDestiniationList();

            employeePoco.NAS = NasEmployeeSaisi;
            employeePoco.AddressId = addressId;
            employeePoco.EmployeeId = employeeId;
            employeePoco.EmployeeName = NameEmployeeSaisi;
            employeePoco.EmployeeMail = MailEmployeeSaisi;
            employeePoco.EmployeePhone = PhoneEmployeeSaisi;
            employeePoco.EmployeeNote = NoteEmployeeSaisi;



            employeePoco.EmployeeCivicNumber = addressUsctr1.civicNumberAdress;
            employeePoco.EmployeeSuite = addressUsctr1.suiteAdress;
            employeePoco.EmployeeCity = addressUsctr1.cityAdress;
            employeePoco.EmployeeState = addressUsctr1.stateAdress;
            employeePoco.EmployeeCountry = addressUsctr1.countryAdress;
            employeePoco.EmployeeZipCode = addressUsctr1.zipCodeAdress;
            employeePoco.EmployeeAdressNote = addressUsctr1.noteAdress;

            employeePoco.EmployeeCompagnies = SelectedCompanies.ToList();

            employeePoco.EmployeeCompagnies.AddRange(relocateSelectionItemUsCtr1.GetSelectItemDestiniationList());

            return employeePoco;
        }
      

    }
}
