namespace SendBillWF.Compagny.Compagny
{
    partial class AssignedEmployeePriceToCompagny
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
            panel1 = new Panel();
            EmployeetreeView = new TreeView();
            SearchEmployeeTxt = new TextBox();
            WeeklyDtg = new DataGridView();
            BiWeeklyDtg = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WeeklyDtg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BiWeeklyDtg).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(EmployeetreeView);
            panel1.Controls.Add(SearchEmployeeTxt);
            panel1.Location = new Point(19, 19);
            panel1.Name = "panel1";
            panel1.Size = new Size(260, 780);
            panel1.TabIndex = 1;
            // 
            // EmployeetreeView
            // 
            EmployeetreeView.Location = new Point(3, 29);
            EmployeetreeView.Name = "EmployeetreeView";
            EmployeetreeView.Size = new Size(254, 748);
            EmployeetreeView.TabIndex = 1;
            // 
            // SearchEmployeeTxt
            // 
            SearchEmployeeTxt.Location = new Point(3, 3);
            SearchEmployeeTxt.Name = "SearchEmployeeTxt";
            SearchEmployeeTxt.Size = new Size(254, 23);
            SearchEmployeeTxt.TabIndex = 0;
            // 
            // WeeklyDtg
            // 
            WeeklyDtg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WeeklyDtg.Location = new Point(333, 198);
            WeeklyDtg.Name = "WeeklyDtg";
            WeeklyDtg.Size = new Size(876, 200);
            WeeklyDtg.TabIndex = 2;
            // 
            // BiWeeklyDtg
            // 
            BiWeeklyDtg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            BiWeeklyDtg.Location = new Point(333, 433);
            BiWeeklyDtg.Name = "BiWeeklyDtg";
            BiWeeklyDtg.Size = new Size(876, 200);
            BiWeeklyDtg.TabIndex = 3;
            // 
            // AssignedEmployeePriceToCompagny
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(BiWeeklyDtg);
            Controls.Add(WeeklyDtg);
            Controls.Add(panel1);
            Name = "AssignedEmployeePriceToCompagny";
            Size = new Size(1311, 808);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WeeklyDtg).EndInit();
            ((System.ComponentModel.ISupportInitialize)BiWeeklyDtg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TreeView CompagnytreeView;
        private TextBox SearchCompagnyTxt;
        private VisitWorkSheetUctr visitWorkSheetUctr1;
        private TreeView EmployeetreeView;
        private TextBox SearchEmployeeTxt;
        private DataGridView WeeklyDtg;
        private DataGridView BiWeeklyDtg;
    }
}
