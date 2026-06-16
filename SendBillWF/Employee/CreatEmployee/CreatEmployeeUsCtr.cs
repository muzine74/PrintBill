using DataBridge;
using DataBridge.Entity;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Operators;
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
    public partial class CreatEmployeeUsCtr : UserControl
    {
        List<CompagniePoco> destinationCompagnirByUserList;
        CompagniManipulation compagniManipulation;
        EmployeeManipulation employeeManipulation;

        EmployeeInfoUsCtr employeeInfoUsCtr;
        //RelocateSelectionItemUsCtr relocateSelectionItemUsCtr;



        public CreatEmployeeUsCtr()
        {
            InitializeComponent();

            compagniManipulation = new CompagniManipulation();
            destinationCompagnirByUserList = new List<CompagniePoco>();
            //relocateSelectionItemUsCtr = new RelocateSelectionItemUsCtr();

            employeeManipulation = new EmployeeManipulation();
            employeeInfoUsCtr = new EmployeeInfoUsCtr();    

            FillSourceCompagnyByUser();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompagniePoco> GetAllCompagnies()
        {
            return compagniManipulation.GetCompagieInfo();
        }

        public void FillSourceCompagnyByUser()
        {
            employeeInfoUsCtr1.FillSourceCompagnyByUser(GetAllCompagnies(), destinationCompagnirByUserList);

            if (VerifyEmployeeInfoSaisi() == false)
            {
                MessageBox.Show("Veuillez saisir tous les informations de l'employé avant d'assigner des compagnies.", "Informations manquantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void SaveEmplyee_Click(object sender, EventArgs e)
        {
            employeeManipulation.SaveCreateEmployee(employeeInfoUsCtr1.SaveEmployee());
        }

        // verifier si tous les info emplyé sont bien saisi

        private bool VerifyEmployeeInfoSaisi()
        {
            //ajouter un message pour indiquer que l'employée existe deja   a tenir en compte le numero d'assurance sociale

            if (string.IsNullOrEmpty(employeeInfoUsCtr1.NameEmployeeSaisi)  || string.IsNullOrEmpty(employeeInfoUsCtr1.MailEmployeeSaisi) || string.IsNullOrEmpty(employeeInfoUsCtr1.PhoneEmployeeSaisi))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }

}
