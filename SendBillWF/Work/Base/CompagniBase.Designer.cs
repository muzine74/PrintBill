namespace SendBillWF.Work.Base
{
    partial class CompagniBase
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
            IsActiveComapgny = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            CompagnyName = new Label();
            CompagnyCode = new Label();
            ComapgnyAdress = new Label();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // IsActiveComapgny
            // 
            IsActiveComapgny.AutoSize = true;
            IsActiveComapgny.Enabled = false;
            IsActiveComapgny.Location = new Point(542, 19);
            IsActiveComapgny.Name = "IsActiveComapgny";
            IsActiveComapgny.Size = new Size(59, 19);
            IsActiveComapgny.TabIndex = 0;
            IsActiveComapgny.Text = "Active";
            IsActiveComapgny.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 26);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 1;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 54);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 2;
            label2.Text = "Code";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 80);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 3;
            label3.Text = "Adress";
            // 
            // CompagnyName
            // 
            CompagnyName.AutoSize = true;
            CompagnyName.Location = new Point(71, 23);
            CompagnyName.Name = "CompagnyName";
            CompagnyName.Size = new Size(38, 15);
            CompagnyName.TabIndex = 4;
            CompagnyName.Text = "label4";
            // 
            // CompagnyCode
            // 
            CompagnyCode.AutoSize = true;
            CompagnyCode.Location = new Point(71, 50);
            CompagnyCode.Name = "CompagnyCode";
            CompagnyCode.Size = new Size(38, 15);
            CompagnyCode.TabIndex = 5;
            CompagnyCode.Text = "label5";
            // 
            // ComapgnyAdress
            // 
            ComapgnyAdress.AutoSize = true;
            ComapgnyAdress.Location = new Point(6, 108);
            ComapgnyAdress.Name = "ComapgnyAdress";
            ComapgnyAdress.Size = new Size(38, 15);
            ComapgnyAdress.TabIndex = 6;
            ComapgnyAdress.Text = "label6";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(IsActiveComapgny);
            groupBox1.Controls.Add(ComapgnyAdress);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(CompagnyCode);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(CompagnyName);
            groupBox1.Location = new Point(3, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(616, 172);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Compagny Info";
            // 
            // CompagniBase
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "CompagniBase";
            Size = new Size(628, 181);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CheckBox IsActiveComapgny;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label CompagnyName;
        private Label CompagnyCode;
        private Label ComapgnyAdress;
        private GroupBox groupBox1;
    }
}
