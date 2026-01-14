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
using DataBridge;

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
        public List<CompagniePoco> GetCompagniesByUser()
        {
            return compagniManipulation.GetCompagieInfo();
        }

        public void FillSourceCompagnyByUser()
        {
            employeeInfoUsCtr1.FillSourceCompagnyByUser(GetCompagniesByUser(), destinationCompagnirByUserList);
        }

        private void SaveEmplyee_Click(object sender, EventArgs e)
        {


            employeeManipulation.SaveCreateEmployee(employeeInfoUsCtr1.SaveNewEmployee());
        }
    }
}
