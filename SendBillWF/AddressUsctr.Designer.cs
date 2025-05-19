namespace SendBillWF
{
    partial class AddressUsctr
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
            label1 = new Label();
            civicNumberTxt = new TextBox();
            suiteTxt = new TextBox();
            label2 = new Label();
            cityTxt = new TextBox();
            label3 = new Label();
            stateTxt = new TextBox();
            label4 = new Label();
            countryTxt = new TextBox();
            label5 = new Label();
            zipCodeTxt = new TextBox();
            label6 = new Label();
            adressTxt = new TextBox();
            label7 = new Label();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 20);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 0;
            label1.Text = "civic Number";
            // 
            // civicNumberTxt
            // 
            civicNumberTxt.Location = new Point(31, 38);
            civicNumberTxt.Name = "civicNumberTxt";
            civicNumberTxt.Size = new Size(270, 23);
            civicNumberTxt.TabIndex = 1;
            // 
            // suiteTxt
            // 
            suiteTxt.Location = new Point(329, 38);
            suiteTxt.Name = "suiteTxt";
            suiteTxt.Size = new Size(100, 23);
            suiteTxt.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(329, 20);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 2;
            label2.Text = "Suite";
            // 
            // cityTxt
            // 
            cityTxt.Location = new Point(31, 86);
            cityTxt.Name = "cityTxt";
            cityTxt.Size = new Size(100, 23);
            cityTxt.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 68);
            label3.Name = "label3";
            label3.Size = new Size(26, 15);
            label3.TabIndex = 4;
            label3.Text = "city";
            // 
            // stateTxt
            // 
            stateTxt.Location = new Point(149, 86);
            stateTxt.Name = "stateTxt";
            stateTxt.Size = new Size(100, 23);
            stateTxt.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(149, 68);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 6;
            label4.Text = "state";
            // 
            // countryTxt
            // 
            countryTxt.Location = new Point(270, 86);
            countryTxt.Name = "countryTxt";
            countryTxt.Size = new Size(100, 23);
            countryTxt.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(270, 68);
            label5.Name = "label5";
            label5.Size = new Size(78, 15);
            label5.TabIndex = 8;
            label5.Text = "civic Number";
            // 
            // zipCodeTxt
            // 
            zipCodeTxt.Location = new Point(396, 86);
            zipCodeTxt.Name = "zipCodeTxt";
            zipCodeTxt.Size = new Size(100, 23);
            zipCodeTxt.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(396, 68);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 10;
            label6.Text = "zip Code";
            // 
            // adressTxt
            // 
            adressTxt.Location = new Point(31, 132);
            adressTxt.Multiline = true;
            adressTxt.Name = "adressTxt";
            adressTxt.Size = new Size(365, 85);
            adressTxt.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(31, 114);
            label7.Name = "label7";
            label7.Size = new Size(33, 15);
            label7.TabIndex = 12;
            label7.Text = "Note";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(stateTxt);
            groupBox1.Controls.Add(adressTxt);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(civicNumberTxt);
            groupBox1.Controls.Add(zipCodeTxt);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(suiteTxt);
            groupBox1.Controls.Add(countryTxt);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cityTxt);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(695, 243);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // AddressUsctr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "AddressUsctr";
            Size = new Size(720, 253);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TextBox civicNumberTxt;
        private TextBox suiteTxt;
        private Label label2;
        private TextBox cityTxt;
        private Label label3;
        private TextBox stateTxt;
        private Label label4;
        private TextBox countryTxt;
        private Label label5;
        private TextBox zipCodeTxt;
        private Label label6;
        private TextBox adressTxt;
        private Label label7;
        private GroupBox groupBox1;
    }
}
