namespace SendBillWF
{
    partial class test
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
            employeeWorkRepUctr1 = new SendBillWF.Report.EmployeeWorkReport.EmployeeWorkRepUctr();
            SuspendLayout();
            // 
            // employeeWorkRepUctr1
            // 
            employeeWorkRepUctr1.AutoScroll = true;
            employeeWorkRepUctr1.BackColor = SystemColors.ActiveCaption;
            employeeWorkRepUctr1.Dock = DockStyle.Fill;
            employeeWorkRepUctr1.Location = new Point(0, 0);
            employeeWorkRepUctr1.Margin = new Padding(0);
            employeeWorkRepUctr1.Name = "employeeWorkRepUctr1";
            employeeWorkRepUctr1.Size = new Size(1220, 756);
            employeeWorkRepUctr1.TabIndex = 0;
            // 
            // test
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1220, 756);
            Controls.Add(employeeWorkRepUctr1);
            Name = "test";
            Text = "test";
            ResumeLayout(false);
        }

        #endregion

        private Report.EmployeeWorkReport.EmployeeWorkRepUctr employeeWorkRepUctr1;
    }
}