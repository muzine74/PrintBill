namespace SendBillWF.Employee
{
    partial class CreatEmployeeUsCtr
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
            employeeInfoUsCtr1 = new EmployeeInfoUsCtr();
            SaveEmplyee = new Button();
            CancelEmployee = new Button();
            SuspendLayout();
            // 
            // employeeInfoUsCtr1
            // 
            employeeInfoUsCtr1.Location = new Point(3, 3);
            employeeInfoUsCtr1.MailEmployeeSaisi = "";
            employeeInfoUsCtr1.Name = "employeeInfoUsCtr1";
            employeeInfoUsCtr1.NameEmployeeSaisi = "";
            employeeInfoUsCtr1.NoteEmployeeSaisi = "";
            employeeInfoUsCtr1.PhoneEmployeeSaisi = "";
            employeeInfoUsCtr1.Size = new Size(763, 801);
            employeeInfoUsCtr1.TabIndex = 0;
            // 
            // SaveEmplyee
            // 
            SaveEmplyee.Location = new Point(206, 824);
            SaveEmplyee.Name = "SaveEmplyee";
            SaveEmplyee.Size = new Size(118, 31);
            SaveEmplyee.TabIndex = 1;
            SaveEmplyee.Text = "Save";
            SaveEmplyee.UseVisualStyleBackColor = true;
            SaveEmplyee.Click += SaveEmplyee_Click;
            // 
            // CancelEmployee
            // 
            CancelEmployee.Location = new Point(377, 824);
            CancelEmployee.Name = "CancelEmployee";
            CancelEmployee.Size = new Size(118, 31);
            CancelEmployee.TabIndex = 2;
            CancelEmployee.Text = "Annuler";
            CancelEmployee.UseVisualStyleBackColor = true;
            // 
            // CreatEmployeeUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CancelEmployee);
            Controls.Add(SaveEmplyee);
            Controls.Add(employeeInfoUsCtr1);
            Name = "CreatEmployeeUsCtr";
            Size = new Size(1785, 874);
            ResumeLayout(false);
        }

        #endregion

        private EmployeeInfoUsCtr employeeInfoUsCtr1;
        private Button SaveEmplyee;
        private Button CancelEmployee;
    }
}
