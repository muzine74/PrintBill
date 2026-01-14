namespace SendBillWF
{
    partial class TaxUsCtr
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
            TaxcNumber = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            TPSNumber = new TextBox();
            TVQNumber = new TextBox();
            TaxcNumber.SuspendLayout();
            SuspendLayout();
            // 
            // TaxcNumber
            // 
            TaxcNumber.Controls.Add(TVQNumber);
            TaxcNumber.Controls.Add(TPSNumber);
            TaxcNumber.Controls.Add(label2);
            TaxcNumber.Controls.Add(label1);
            TaxcNumber.Location = new Point(3, 3);
            TaxcNumber.Name = "TaxcNumber";
            TaxcNumber.Size = new Size(338, 90);
            TaxcNumber.TabIndex = 0;
            TaxcNumber.TabStop = false;
            TaxcNumber.Text = "Tax";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 20);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 0;
            label1.Text = "TPS : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 48);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 1;
            label2.Text = "TVQ : ";
            // 
            // TPSNumber
            // 
            TPSNumber.Location = new Point(50, 17);
            TPSNumber.Name = "TPSNumber";
            TPSNumber.Size = new Size(263, 23);
            TPSNumber.TabIndex = 2;
            // 
            // TVQNumber
            // 
            TVQNumber.Location = new Point(50, 45);
            TVQNumber.Name = "TVQNumber";
            TVQNumber.Size = new Size(263, 23);
            TVQNumber.TabIndex = 3;
            // 
            // TaxUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TaxcNumber);
            Name = "TaxUsCtr";
            Size = new Size(350, 106);
            TaxcNumber.ResumeLayout(false);
            TaxcNumber.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox TaxcNumber;
        private TextBox TVQNumber;
        private TextBox TPSNumber;
        private Label label2;
        private Label label1;
    }
}
