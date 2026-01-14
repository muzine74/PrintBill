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
            enregistrer = new Button();
            checkAll = new CheckBox();
            billDate = new DateTimePicker();
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
            // enregistrer
            // 
            enregistrer.Location = new Point(482, 25);
            enregistrer.Name = "enregistrer";
            enregistrer.Size = new Size(75, 23);
            enregistrer.TabIndex = 3;
            enregistrer.Text = "enregistrer";
            enregistrer.UseVisualStyleBackColor = true;
            enregistrer.Click += Save_Click;
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
            // billDate
            // 
            billDate.Location = new Point(831, 17);
            billDate.Name = "billDate";
            billDate.Size = new Size(200, 23);
            billDate.TabIndex = 5;
            billDate.ValueChanged += billDate_ValueChanged;
            // 
            // PDFBillFromGDriveUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(billDate);
            Controls.Add(checkAll);
            Controls.Add(enregistrer);
            Controls.Add(SpreadSheetCombobx);
            Name = "PDFBillFromGDriveUsCtr";
            Size = new Size(1062, 638);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox SpreadSheetCombobx;
        private Button enregistrer;
        private CheckBox checkAll;
        private DateTimePicker billDate;
    }
}
