namespace SendBillWF
{
    partial class MailCrdentialUsCtr
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
            PwdMail = new TextBox();
            MailAdress = new TextBox();
            label2 = new Label();
            label1 = new Label();
            TaxcNumber = new GroupBox();
            TaxcNumber.SuspendLayout();
            SuspendLayout();
            // 
            // PwdMail
            // 
            PwdMail.Location = new Point(50, 45);
            PwdMail.Name = "PwdMail";
            PwdMail.Size = new Size(263, 23);
            PwdMail.TabIndex = 3;
            // 
            // MailAdress
            // 
            MailAdress.Location = new Point(50, 17);
            MailAdress.Name = "MailAdress";
            MailAdress.Size = new Size(263, 23);
            MailAdress.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 48);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 1;
            label2.Text = "Pwd : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 20);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Mail : ";
            // 
            // TaxcNumber
            // 
            TaxcNumber.Controls.Add(PwdMail);
            TaxcNumber.Controls.Add(MailAdress);
            TaxcNumber.Controls.Add(label2);
            TaxcNumber.Controls.Add(label1);
            TaxcNumber.Location = new Point(3, 3);
            TaxcNumber.Name = "TaxcNumber";
            TaxcNumber.Size = new Size(338, 90);
            TaxcNumber.TabIndex = 1;
            TaxcNumber.TabStop = false;
            TaxcNumber.Text = "Mail";
            // 
            // MailCrdentialUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TaxcNumber);
            Name = "MailCrdentialUsCtr";
            Size = new Size(350, 101);
            TaxcNumber.ResumeLayout(false);
            TaxcNumber.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox PwdMail;
        private TextBox MailAdress;
        private Label label2;
        private Label label1;
        private GroupBox TaxcNumber;
    }
}
