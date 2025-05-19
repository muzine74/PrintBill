namespace SendBillWF
{
    partial class PDFBillFromGDriveUsCtr
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
            SpreadSheetCombobx = new ComboBox();
            button1 = new Button();
            checkAll = new CheckBox();
            SuspendLayout();
            // 
            // SpreadSheetCombobx
            // 
            SpreadSheetCombobx.FormattingEnabled = true;
            SpreadSheetCombobx.Location = new Point(26, 25);
            SpreadSheetCombobx.Name = "SpreadSheetCombobx";
            SpreadSheetCombobx.Size = new Size(395, 23);
            SpreadSheetCombobx.TabIndex = 2;
            SpreadSheetCombobx.SelectedIndexChanged += SpreadSheetCombobx_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(482, 25);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkAll
            // 
            checkAll.AutoSize = true;
            checkAll.Location = new Point(26, 67);
            checkAll.Name = "checkAll";
            checkAll.Size = new Size(74, 19);
            checkAll.TabIndex = 4;
            checkAll.Text = "check All";
            checkAll.UseVisualStyleBackColor = true;
            checkAll.CheckedChanged += checkAll_CheckedChanged;
            // 
            // PDFBillFromGDriveUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(checkAll);
            Controls.Add(button1);
            Controls.Add(SpreadSheetCombobx);
            Name = "PDFBillFromGDriveUsCtr";
            Size = new Size(1062, 560);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox SpreadSheetCombobx;
        private Button button1;
        private CheckBox checkAll;
    }
}
