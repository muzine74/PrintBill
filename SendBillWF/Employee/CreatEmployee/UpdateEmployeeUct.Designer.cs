namespace SendBillWF.Employee.CreatEmployee
{
    partial class UpdateEmployeeUct
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            employeeInfoUpDateUsCtr1 = new EmployeeInfoUsCtr();
            panel1 = new Panel();
            EmployeSearchTxt = new TextBox();
            EmployeeTreeView = new TreeView();
            UpdateEmployee = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // employeeInfoUpDateUsCtr1
            // 
            employeeInfoUpDateUsCtr1.CityAdressSaisi = "";
            employeeInfoUpDateUsCtr1.CivicNumberAdressSaisi = "";
            employeeInfoUpDateUsCtr1.CountryAdressSaisi = "";
            employeeInfoUpDateUsCtr1.Location = new Point(258, 0);
            employeeInfoUpDateUsCtr1.MailEmployeeSaisi = "";
            employeeInfoUpDateUsCtr1.Name = "employeeInfoUpDateUsCtr1";
            employeeInfoUpDateUsCtr1.NameEmployeeSaisi = "";
            employeeInfoUpDateUsCtr1.NoteAdressSaisi = "";
            employeeInfoUpDateUsCtr1.NoteEmployeeSaisi = "";
            employeeInfoUpDateUsCtr1.PhoneEmployeeSaisi = "";
            employeeInfoUpDateUsCtr1.Size = new Size(755, 824);
            employeeInfoUpDateUsCtr1.StateAdressSaisi = "";
            employeeInfoUpDateUsCtr1.SuiteAdressSaisi = "";
            employeeInfoUpDateUsCtr1.TabIndex = 0;
            employeeInfoUpDateUsCtr1.ZipCodeAdressSaisi = "";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(EmployeSearchTxt);
            panel1.Controls.Add(EmployeeTreeView);
            panel1.Location = new Point(18, 72);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 716);
            panel1.TabIndex = 1;
            // 
            // EmployeSearchTxt
            // 
            EmployeSearchTxt.Location = new Point(3, 16);
            EmployeSearchTxt.Name = "EmployeSearchTxt";
            EmployeSearchTxt.Size = new Size(228, 23);
            EmployeSearchTxt.TabIndex = 1;
            EmployeSearchTxt.TextChanged += txtSearch_TextChanged;
            // 
            // EmployeeTreeView
            // 
            EmployeeTreeView.Location = new Point(3, 39);
            EmployeeTreeView.Name = "EmployeeTreeView";
            EmployeeTreeView.Size = new Size(228, 678);
            EmployeeTreeView.TabIndex = 0;
            EmployeeTreeView.BeforeExpand += EmployeeTreeView_BeforeExpand;
            // 
            // UpdateEmployee
            // 
            UpdateEmployee.Location = new Point(357, 828);
            UpdateEmployee.Name = "UpdateEmployee";
            UpdateEmployee.Size = new Size(148, 38);
            UpdateEmployee.TabIndex = 2;
            UpdateEmployee.Text = "Update Employee";
            UpdateEmployee.UseVisualStyleBackColor = true;
            UpdateEmployee.Click += UpdateEmployee_Click;
            // 
            // UpdateEmployeeUct
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(UpdateEmployee);
            Controls.Add(panel1);
            Controls.Add(employeeInfoUpDateUsCtr1);
            Name = "UpdateEmployeeUct";
            Size = new Size(1070, 914);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private EmployeeInfoUsCtr employeeInfoUpDateUsCtr1;
        private Panel panel1;
        private TreeView EmployeeTreeView;
        private TextBox EmployeSearchTxt;
        private Button UpdateEmployee;
    }
}
