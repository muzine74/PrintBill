using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBillWF.Employee
{
    public partial class EmployeeUsCtr : UserControl
    {
        public EmployeeUsCtr()
        {
            InitializeComponent();
        }

        public string NameEmployee
        {
            get { return EmployeeNameTxt.Text; }
            set { EmployeeNameTxt.Text = value; }
        }

        public string MailEmployee
        {
            get { return EmployeeMailTxt.Text; }
            set { EmployeeMailTxt.Text = value; }
        }

        public string PhoneEmployee
        {
            get { return EmployeePhoneTxt.Text; }
            set { EmployeePhoneTxt.Text = value; }
        }

        public string NoteEmployee
        {
            get { return EmployeeNoteTxt.Text; }
            set { EmployeeNoteTxt.Text = value; }
        }
    }
}
