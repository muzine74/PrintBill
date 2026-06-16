namespace SendBillWF.Employee
{
    partial class EmployeeInfoUsCtr
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
            addressUsctr1 = new AddressUsctr();
            relocateSelectionItemUsCtr1 = new RelocateSelectionItemUsCtr();
            groupBox1 = new GroupBox();
            employeeUsCtr1 = new EmployeeUsCtr();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // addressUsctr1
            // 
            addressUsctr1.cityAdress = "";
            addressUsctr1.civicNumberAdress = "";
            addressUsctr1.countryAdress = "";
            addressUsctr1.Location = new Point(17, 239);
            addressUsctr1.Name = "addressUsctr1";
            addressUsctr1.noteAdress = "";
            addressUsctr1.Size = new Size(696, 254);
            addressUsctr1.stateAdress = "";
            addressUsctr1.suiteAdress = "";
            addressUsctr1.TabIndex = 1;
            addressUsctr1.zipCodeAdress = "";
            // 
            // relocateSelectionItemUsCtr1
            // 
            relocateSelectionItemUsCtr1.Location = new Point(8, 499);
            relocateSelectionItemUsCtr1.Name = "relocateSelectionItemUsCtr1";
            relocateSelectionItemUsCtr1.Size = new Size(751, 301);
            relocateSelectionItemUsCtr1.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(employeeUsCtr1);
            groupBox1.Location = new Point(17, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(742, 219);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // employeeUsCtr1
            // 
            employeeUsCtr1.Location = new Point(7, 22);
            employeeUsCtr1.MailEmployee = "";
            employeeUsCtr1.Name = "employeeUsCtr1";
            employeeUsCtr1.NameEmployee = "";
            employeeUsCtr1.NasEmployee = "";
            employeeUsCtr1.NoteEmployee = "";
            employeeUsCtr1.PhoneEmployee = "";
            employeeUsCtr1.Size = new Size(715, 191);
            employeeUsCtr1.TabIndex = 0;
            // 
            // EmployeeInfoUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(relocateSelectionItemUsCtr1);
            Controls.Add(addressUsctr1);
            Name = "EmployeeInfoUsCtr";
            Size = new Size(1243, 824);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private AddressUsctr addressUsctr1;
        private RelocateSelectionItemUsCtr relocateSelectionItemUsCtr1;
        private GroupBox groupBox1;
        private EmployeeUsCtr employeeUsCtr1;
    }
}
