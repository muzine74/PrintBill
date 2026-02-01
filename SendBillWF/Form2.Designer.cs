using SendBillWF.Bill;
using SendBillWF.Compagny.Compagny;
using SendBillWF.Employee;
using SendBillWF.Employee.CreatEmployee;
using SendBillWF.Work;

namespace SendBillWF
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("Add New Compagny");
            TreeNode treeNode2 = new TreeNode("Update Compagny");
            TreeNode treeNode3 = new TreeNode("Compagny", new TreeNode[] { treeNode1, treeNode2 });
            TreeNode treeNode4 = new TreeNode("CreateEmployee");
            TreeNode treeNode5 = new TreeNode("UpdateEmployee");
            TreeNode treeNode6 = new TreeNode("Employéé Price");
            TreeNode treeNode7 = new TreeNode("Employee", new TreeNode[] { treeNode4, treeNode5, treeNode6 });
            TreeNode treeNode8 = new TreeNode("CreateBill");
            TreeNode treeNode9 = new TreeNode("UpdateBill");
            TreeNode treeNode10 = new TreeNode("Print Bill");
            TreeNode treeNode11 = new TreeNode("Send Bill");
            TreeNode treeNode12 = new TreeNode("generate Bill From GDrive");
            TreeNode treeNode13 = new TreeNode("BillHistory");
            TreeNode treeNode14 = new TreeNode("Bill", new TreeNode[] { treeNode8, treeNode9, treeNode10, treeNode11, treeNode12, treeNode13 });
            TreeNode treeNode15 = new TreeNode("EmployeeSchedular");
            TreeNode treeNode16 = new TreeNode("CompagnySchedular");
            TreeNode treeNode17 = new TreeNode("schedular", new TreeNode[] { treeNode15, treeNode16 });
            TreeNode treeNode18 = new TreeNode("Visualizer");
            TreeNode treeNode19 = new TreeNode("Employee Work Report");
            TreeNode treeNode20 = new TreeNode("Compagny work Rport");
            TreeNode treeNode21 = new TreeNode("compagny paiment");
            TreeNode treeNode22 = new TreeNode("Employee paiment");
            TreeNode treeNode23 = new TreeNode("Report", new TreeNode[] { treeNode19, treeNode20, treeNode21, treeNode22 });
            panel1 = new Panel();
            panel2 = new Panel();
            Menu = new TreeView();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(Menu);
            panel1.Location = new Point(4, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(204, 771);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Location = new Point(221, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1021, 771);
            panel2.TabIndex = 1;
            // 
            // Menu
            // 
            Menu.Location = new Point(8, 40);
            Menu.Name = "Menu";
            treeNode1.ForeColor = Color.Green;
            treeNode1.Name = "AddNewCompagny";
            treeNode1.Text = "Add New Compagny";
            treeNode2.ForeColor = Color.Green;
            treeNode2.Name = "UpdateCompagny";
            treeNode2.Text = "Update Compagny";
            treeNode3.Name = "Compagny";
            treeNode3.Text = "Compagny";
            treeNode4.ForeColor = Color.Green;
            treeNode4.Name = "CreateEmployee";
            treeNode4.Text = "CreateEmployee";
            treeNode5.BackColor = Color.White;
            treeNode5.ForeColor = Color.Lime;
            treeNode5.Name = "UpdateEmployee";
            treeNode5.Text = "UpdateEmployee";
            treeNode6.Name = "AssignedEmployeePriceTOCompagny";
            treeNode6.Text = "Employéé Price";
            treeNode7.Name = "Employee";
            treeNode7.Text = "Employee";
            treeNode8.ForeColor = Color.Green;
            treeNode8.Name = "CreateBill";
            treeNode8.Text = "CreateBill";
            treeNode9.ForeColor = Color.Green;
            treeNode9.Name = "UpdateBill";
            treeNode9.Text = "UpdateBill";
            treeNode10.Name = "PrintBill";
            treeNode10.Text = "Print Bill";
            treeNode11.Name = "SendBill";
            treeNode11.Text = "Send Bill";
            treeNode12.ForeColor = Color.DarkGreen;
            treeNode12.Name = "PDFBillFromGDrive";
            treeNode12.Text = "generate Bill From GDrive";
            treeNode13.ForeColor = Color.DarkGreen;
            treeNode13.Name = "BillHistory";
            treeNode13.Text = "BillHistory";
            treeNode14.Name = "Bill";
            treeNode14.Text = "Bill";
            treeNode15.Name = "EmployeeSchedular";
            treeNode15.Text = "EmployeeSchedular";
            treeNode16.Name = "CompagnySchedular";
            treeNode16.Text = "CompagnySchedular";
            treeNode17.Name = "s";
            treeNode17.Text = "schedular";
            treeNode18.Name = "V";
            treeNode18.Text = "Visualizer";
            treeNode19.Name = "Node1";
            treeNode19.Text = "Employee Work Report";
            treeNode20.Name = "Node2";
            treeNode20.Text = "Compagny work Rport";
            treeNode21.Name = "Node3";
            treeNode21.Text = "compagny paiment";
            treeNode22.Name = "Node4";
            treeNode22.Text = "Employee paiment";
            treeNode23.Name = "Node0";
            treeNode23.Text = "Report";
            Menu.Nodes.AddRange(new TreeNode[] { treeNode3, treeNode7, treeNode14, treeNode17, treeNode18, treeNode23 });
            Menu.Size = new Size(190, 728);
            Menu.TabIndex = 0;
            Menu.AfterSelect += Menu_AfterSelect;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1647, 999);
            Controls.Add(panel1);
            Name = "Form2";
            Text = "Form2";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TreeView Menu;
        private AddCompagnyusctr addCompagnyusctr1;
        private UpdateCmpUsctr updateCmpUsctr;
        private BillHistoryUsctr billHistoryUsctr;
        private PDFBillFromGDriveUsCtr pDFBillFromGDriveUsCtr;
        private ErrorProvider errorProvider1;
        private CreateBillUsCtr createBillUsCtr;
        private CreatEmployeeUsCtr creatEmployeeUsCtr;
        private UpdateBillUsCtr updateBillUsCtr;
        private EmployeeSchedularUsCtr employeeSchedularUsCtr;
        private UpdateEmployeeUct updateEmployeeUct;
        private ComapgnySchedularUsCtr compagnySchedularUsCtr;
        private AssignedEmployeePriceToCompagny assignedEmployeePriceToCompagny;


        //private UpdateCmpUsctr updateCmpUsctr1;
    }


}