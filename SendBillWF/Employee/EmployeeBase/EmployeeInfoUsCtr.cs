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
        public EmployeeInfoUsCtr()
        {
            InitializeComponent();
            employeePoco = new EmployeePoco();
           
        }

      
        public void FillSourceCompagnyByUser(List<CompagniePoco> SourceList, List<CompagniePoco> DestinationList)
        {
            relocateSelectionItemUsCtr1.FillSourceCompagnyByUser(SourceList, DestinationList);
        }


        //employeeUsCtr2

        //public string NameEmployee
        //{
        //    get { return employeeUsCtr1.NameEmployee; }
        //    set { employeeUsCtr1.NameEmployee = value; }
        //}

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
            get { return employeeUsCtr1.MailEmployee; }
            set { employeeUsCtr1.MailEmployee = value; }
        }

        public string NoteEmployeeSaisi
        {
            get { return employeeUsCtr1.MailEmployee; }
            set { employeeUsCtr1.MailEmployee = value; }
        }


        //public EmployeePoco GetEmployeeiDentity()
        //{

        //}
        public EmployeePoco SaveNewEmployee()
        {
            List<CompagniePoco> SelectItemDestiniationList = relocateSelectionItemUsCtr1.GetSelectItemDestiniationList();

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

            employeePoco.EmployeeCompagnies.AddRange(relocateSelectionItemUsCtr1.GetSelectItemDestiniationList());

            return employeePoco;
        }
      

    }
}
