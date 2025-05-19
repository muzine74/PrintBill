using SendBillWF.Employee;

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
            TreeNode treeNode6 = new TreeNode("Employee", new TreeNode[] { treeNode4, treeNode5 });
            TreeNode treeNode7 = new TreeNode("CreateBill");
            TreeNode treeNode8 = new TreeNode("Update Bill");
            TreeNode treeNode9 = new TreeNode("Print Bill");
            TreeNode treeNode10 = new TreeNode("Send Bill");
            TreeNode treeNode11 = new TreeNode("generate Bill From GDrive");
            TreeNode treeNode12 = new TreeNode("BillHistory");
            TreeNode treeNode13 = new TreeNode("Bill", new TreeNode[] { treeNode7, treeNode8, treeNode9, treeNode10, treeNode11, treeNode12 });
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
            treeNode5.Name = "UpdateEmployee";
            treeNode5.Text = "UpdateEmployee";
            treeNode6.Name = "Employee";
            treeNode6.Text = "Employee";
            treeNode7.ForeColor = Color.Green;
            treeNode7.Name = "CreateBill";
            treeNode7.Text = "CreateBill";
            treeNode8.Name = "UpdateBill";
            treeNode8.Text = "Update Bill";
            treeNode9.Name = "PrintBill";
            treeNode9.Text = "Print Bill";
            treeNode10.Name = "SendBill";
            treeNode10.Text = "Send Bill";
            treeNode11.ForeColor = Color.DarkGreen;
            treeNode11.Name = "PDFBillFromGDrive";
            treeNode11.Text = "generate Bill From GDrive";
            treeNode12.ForeColor = Color.DarkGreen;
            treeNode12.Name = "BillHistory";
            treeNode12.Text = "BillHistory";
            treeNode13.Name = "Bill";
            treeNode13.Text = "Bill";
            Menu.Nodes.AddRange(new TreeNode[] { treeNode3, treeNode6, treeNode13 });
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
            ClientSize = new Size(1621, 854);
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
        

        //private UpdateCmpUsctr updateCmpUsctr1;
    }


}