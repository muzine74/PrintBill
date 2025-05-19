namespace SendBillWF.Employee
{
    partial class EmployeeUsCtr
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
            label4 = new Label();
            EmployeeNameTxt = new TextBox();
            label3 = new Label();
            label2 = new Label();
            EmployeePhoneTxt = new TextBox();
            label1 = new Label();
            EmployeeNoteTxt = new TextBox();
            EmployeeMailTxt = new TextBox();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 157);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 3;
            label4.Text = "Note";
            // 
            // EmployeeNameTxt
            // 
            EmployeeNameTxt.Location = new Point(93, 52);
            EmployeeNameTxt.Name = "EmployeeNameTxt";
            EmployeeNameTxt.Size = new Size(237, 23);
            EmployeeNameTxt.TabIndex = 4;
            EmployeeNameTxt.Text = "asdds";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 123);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 2;
            label3.Text = "Phone";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 89);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 1;
            label2.Text = "Mail";
            // 
            // EmployeePhoneTxt
            // 
            EmployeePhoneTxt.Location = new Point(93, 120);
            EmployeePhoneTxt.Name = "EmployeePhoneTxt";
            EmployeePhoneTxt.Size = new Size(237, 23);
            EmployeePhoneTxt.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 55);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // EmployeeNoteTxt
            // 
            EmployeeNoteTxt.Location = new Point(93, 154);
            EmployeeNoteTxt.Name = "EmployeeNoteTxt";
            EmployeeNoteTxt.Size = new Size(237, 23);
            EmployeeNoteTxt.TabIndex = 7;
            // 
            // EmployeeMailTxt
            // 
            EmployeeMailTxt.Location = new Point(93, 86);
            EmployeeMailTxt.Name = "EmployeeMailTxt";
            EmployeeMailTxt.Size = new Size(237, 23);
            EmployeeMailTxt.TabIndex = 5;
            // 
            // EmployeeUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(EmployeeMailTxt);
            Controls.Add(EmployeeNoteTxt);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(EmployeePhoneTxt);
            Controls.Add(EmployeeNameTxt);
            Controls.Add(label2);
            Controls.Add(label3);
            Name = "EmployeeUsCtr";
            Size = new Size(407, 210);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private TextBox EmployeeNameTxt;
        private Label label3;
        private Label label2;
        private TextBox EmployeePhoneTxt;
        private Label label1;
        private TextBox EmployeeNoteTxt;
        private TextBox EmployeeMailTxt;
    }
}
