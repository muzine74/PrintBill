namespace SendBillWF
{
    partial class VisitWorkSheetUctr
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
            WorkVisiteDgv = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)WorkVisiteDgv).BeginInit();
            SuspendLayout();
            // 
            // WorkVisiteDgv
            // 
            WorkVisiteDgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WorkVisiteDgv.Location = new Point(3, 3);
            WorkVisiteDgv.Name = "WorkVisiteDgv";
            WorkVisiteDgv.Size = new Size(978, 355);
            WorkVisiteDgv.TabIndex = 0;
            // 
            // VisitWorkSheetUctr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(WorkVisiteDgv);
            Name = "VisitWorkSheetUctr";
            Size = new Size(990, 361);
            ((System.ComponentModel.ISupportInitialize)WorkVisiteDgv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView WorkVisiteDgv;
    }
}
