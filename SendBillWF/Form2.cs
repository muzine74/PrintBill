using DataBridge.Entity;
using SendBillWF.Bill;
using SendBillWF.Employee;
using SendBillWF.Employee.CreatEmployee;
using SendBillWF.Work;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using SendBillWF.Compagny.Compagny;
using SendBillWF.Report;

namespace SendBillWF
{
    public partial class Form2 : Form
    {
        CompagniePoco compagniePoco;

        public Form2()
        {
            

            InitializeComponent(); 

            compagniePoco = new CompagniePoco();
            addCompagnyusctr1 = new AddCompagnyusctr();
            updateCmpUsctr = new UpdateCmpUsctr();
            billHistoryUsctr = new BillHistoryUsctr();
            pDFBillFromGDriveUsCtr = new PDFBillFromGDriveUsCtr();
            createBillUsCtr = new CreateBillUsCtr();
            creatEmployeeUsCtr = new CreatEmployeeUsCtr();
            updateBillUsCtr = new UpdateBillUsCtr();
            employeeSchedularUsCtr = new EmployeeSchedularUsCtr();
            updateEmployeeUct = new UpdateEmployeeUct();
            compagnySchedularUsCtr = new ComapgnySchedularUsCtr();
            assignedEmployeePriceToCompagny = new AssignedEmployeePriceToCompagny();
            employeeWorkReportUsCtr = new EmployeeWorkReportUsCtr();


            // updateCmpUsctr1.Hide();


            //addCompagnyusctr1.Hide();   

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Menu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "UpdateBill")  //updateBillUsCtr
            {
                RemoveControl();

                updateBillUsCtr.Location = new Point(201, 33);
                updateBillUsCtr.Name = "updateBillUsCtr";
                updateBillUsCtr.Size = new Size(1750, 900);
                updateBillUsCtr.TabIndex = 1;

                Controls.Add(updateBillUsCtr);
                updateBillUsCtr.Show();
            }

            if (e.Node.Name == "AddNewCompagny")
            {
                RemoveControl();

                addCompagnyusctr1.Location = new Point(201, 33);
                addCompagnyusctr1.Name = "addCompagnyusctr1";
                addCompagnyusctr1.Size = new Size(1152, 848);
                addCompagnyusctr1.TabIndex = 1;

                Controls.Add(addCompagnyusctr1);
                addCompagnyusctr1.Show();
            }

            if (e.Node.Name == "UpdateCompagny")

            {
                RemoveControl();

                updateCmpUsctr.Location = new Point(201, 33);
                updateCmpUsctr.Name = "updateCmpUsctr";
                updateCmpUsctr.Size = new Size(1152, 848);
                updateCmpUsctr.TabIndex = 1;

                Controls.Add(updateCmpUsctr);
                updateCmpUsctr.Show();
            }

            if (e.Node.Name == "BillHistory")
            {
                RemoveControl();

                billHistoryUsctr.Location = new Point(201, 33);
                billHistoryUsctr.Name = "billHistoryUsctr";
                billHistoryUsctr.Size = new Size(1750, 900);
                billHistoryUsctr.TabIndex = 1;

                Controls.Add(billHistoryUsctr);
                billHistoryUsctr.Show();

            }

            if (e.Node.Name == "PDFBillFromGDrive")
            {
                RemoveControl();

                pDFBillFromGDriveUsCtr.Location = new Point(201, 33);
                pDFBillFromGDriveUsCtr.Name = "pDFBillFromGDriveUsCtr";
                pDFBillFromGDriveUsCtr.Size = new Size(1750, 900);
                pDFBillFromGDriveUsCtr.TabIndex = 1;

                Controls.Add(pDFBillFromGDriveUsCtr);
                pDFBillFromGDriveUsCtr.Show();

            }

            if (e.Node.Name == "CreateBill")  //CreateBill 
            {
                RemoveControl();

                createBillUsCtr.Location = new Point(201, 33);
                createBillUsCtr.Name = "createBillUsCtr";
                createBillUsCtr.Size = new Size(1750, 900);
                createBillUsCtr.TabIndex = 1;

                Controls.Add(createBillUsCtr);
                createBillUsCtr.Show();

            }



            if (e.Node.Name == "CreateEmployee")
            {
                RemoveControl();

                creatEmployeeUsCtr.Location = new Point(201, 33);
                creatEmployeeUsCtr.Name = "createEmployeeUsCtr";
                creatEmployeeUsCtr.Size = new Size(1750, 900);
                creatEmployeeUsCtr.TabIndex = 1;

                Controls.Add(creatEmployeeUsCtr);
                creatEmployeeUsCtr.Show();

            }

            if (e.Node.Name == "EmployeeSchedular")  //updateBillUsCtr
            {
                RemoveControl();

                employeeSchedularUsCtr.Location = new Point(201, 33);
                employeeSchedularUsCtr.Name = "employeeSchedularUsCtr";
                employeeSchedularUsCtr.Size = new Size(1750, 900);
                employeeSchedularUsCtr.TabIndex = 1;

                Controls.Add(employeeSchedularUsCtr);
                employeeSchedularUsCtr.Show();
            }


            if (e.Node.Name == "UpdateEmployee")  //updateBillUsCtr
            {
                RemoveControl();

                updateEmployeeUct.Location = new Point(201, 33);
                updateEmployeeUct.Name = "updateEmployeeUct";
                updateEmployeeUct.Size = new Size(1750, 900);
                updateEmployeeUct.TabIndex = 1;

                Controls.Add(updateEmployeeUct);
                updateEmployeeUct.Show();
            }


            if (e.Node.Name == "CompagnySchedular")  //CompagnyShedular
            {
                RemoveControl();

                compagnySchedularUsCtr.Location = new Point(201, 33);
                compagnySchedularUsCtr.Name = "compagnyShedularUsCtr";
                compagnySchedularUsCtr.Size = new Size(1750, 900);
                compagnySchedularUsCtr.TabIndex = 1;

                Controls.Add(compagnySchedularUsCtr);
                compagnySchedularUsCtr.Show();
            }


            if (e.Node.Name == "AssignedEmployeePriceTOCompagny")  //CompagnyShedular
            {
                RemoveControl();

                assignedEmployeePriceToCompagny.Location = new Point(201, 33);
                assignedEmployeePriceToCompagny.Name = "EmployeeWorkReport";
                assignedEmployeePriceToCompagny.Size = new Size(1750, 900);
                assignedEmployeePriceToCompagny.TabIndex = 1;

                Controls.Add(assignedEmployeePriceToCompagny);
                assignedEmployeePriceToCompagny.Show();
            }


            if (e.Node.Name == "EmployeeWorkReport")  //CompagnyShedular
            {
                RemoveControl();

                employeeWorkReportUsCtr.Location = new Point(201, 33);
                employeeWorkReportUsCtr.Name = "employeeWorkReportUsCtr";
                employeeWorkReportUsCtr.Size = new Size(1750, 900);
                employeeWorkReportUsCtr.TabIndex = 1;

                Controls.Add(employeeWorkReportUsCtr);
                employeeWorkReportUsCtr.Show();
            }

            //assignedEmployeePriceToCompagny

        } //updateEmployeeUct


        private void RemoveControl()
        {
            Controls.Remove(addCompagnyusctr1); 
            Controls.Remove(updateCmpUsctr);
            Controls.Remove(billHistoryUsctr);
            Controls.Remove(pDFBillFromGDriveUsCtr);
            Controls.Remove(createBillUsCtr);
            Controls.Remove(creatEmployeeUsCtr);
            Controls.Remove(updateBillUsCtr);
            Controls.Remove(employeeSchedularUsCtr);
            Controls.Remove(updateEmployeeUct);
            Controls.Remove(compagnySchedularUsCtr);
            Controls.Remove(assignedEmployeePriceToCompagny);
            Controls.Remove(employeeWorkReportUsCtr);

        }
    }
}
