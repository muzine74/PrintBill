namespace SendBillWF.Report
{
    partial class EmployeeWorkReportUsCtr
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
            SearchUserTxt = new TextBox();
            UsersTreeView = new TreeView();
            StartDateLabel = new Label();
            panel1 = new Panel();
            SearchUserReportTxt = new TextBox();
            UsersTreeViewReport = new TreeView();
            Display = new Button();
            BeguinDate = new DateTimePicker();
            EndDate = new DateTimePicker();
            DateTimePickerFiltre = new Panel();
            EndDateLabel = new Label();
            panelRapport = new Panel();
            panel1.SuspendLayout();
            DateTimePickerFiltre.SuspendLayout();
            SuspendLayout();
            // 
            // SearchUserTxt
            // 
            SearchUserTxt.Location = new Point(0, 0);
            SearchUserTxt.Name = "SearchUserTxt";
            SearchUserTxt.Size = new Size(100, 23);
            SearchUserTxt.TabIndex = 0;
            // 
            // UsersTreeView
            // 
            UsersTreeView.LineColor = Color.Empty;
            UsersTreeView.Location = new Point(0, 0);
            UsersTreeView.Name = "UsersTreeView";
            UsersTreeView.Size = new Size(121, 97);
            UsersTreeView.TabIndex = 0;
            // 
            // StartDateLabel
            // 
            StartDateLabel.AutoSize = true;
            StartDateLabel.Location = new Point(15, 14);
            StartDateLabel.Name = "StartDateLabel";
            StartDateLabel.Size = new Size(83, 15);
            StartDateLabel.TabIndex = 0;
            StartDateLabel.Text = "StartDateLabel";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(SearchUserReportTxt);
            panel1.Controls.Add(UsersTreeViewReport);
            panel1.Location = new Point(12, 37);
            panel1.Name = "panel1";
            panel1.Size = new Size(225, 747);
            panel1.TabIndex = 12;
            // 
            // SearchUserReportTxt
            // 
            SearchUserReportTxt.Location = new Point(3, 5);
            SearchUserReportTxt.Name = "SearchUserReportTxt";
            SearchUserReportTxt.Size = new Size(219, 23);
            SearchUserReportTxt.TabIndex = 11;
            // 
            // UsersTreeViewReport
            // 
            UsersTreeViewReport.Location = new Point(0, 32);
            UsersTreeViewReport.Name = "UsersTreeViewReport";
            UsersTreeViewReport.Size = new Size(225, 714);
            UsersTreeViewReport.TabIndex = 10;
            // 
            // Display
            // 
            Display.Location = new Point(15, 54);
            Display.Name = "Display";
            Display.Size = new Size(654, 23);
            Display.TabIndex = 13;
            Display.Text = "Afficher";
            Display.UseVisualStyleBackColor = true;
            Display.Click += Display_Click;
            // 
            // BeguinDate
            // 
            BeguinDate.Location = new Point(106, 9);
            BeguinDate.Name = "BeguinDate";
            BeguinDate.Size = new Size(200, 23);
            BeguinDate.TabIndex = 15;
            // 
            // EndDate
            // 
            EndDate.Location = new Point(469, 11);
            EndDate.Name = "EndDate";
            EndDate.Size = new Size(200, 23);
            EndDate.TabIndex = 16;
            // 
            // DateTimePickerFiltre
            // 
            DateTimePickerFiltre.Controls.Add(EndDateLabel);
            DateTimePickerFiltre.Controls.Add(BeguinDate);
            DateTimePickerFiltre.Controls.Add(EndDate);
            DateTimePickerFiltre.Controls.Add(StartDateLabel);
            DateTimePickerFiltre.Controls.Add(Display);
            DateTimePickerFiltre.Location = new Point(288, 54);
            DateTimePickerFiltre.Name = "DateTimePickerFiltre";
            DateTimePickerFiltre.Size = new Size(684, 82);
            DateTimePickerFiltre.TabIndex = 17;
            // 
            // EndDateLabel
            // 
            EndDateLabel.AutoSize = true;
            EndDateLabel.Location = new Point(384, 14);
            EndDateLabel.Name = "EndDateLabel";
            EndDateLabel.Size = new Size(79, 15);
            EndDateLabel.TabIndex = 17;
            EndDateLabel.Text = "EndDateLabel";
            // 
            // panelRapport
            // 
            panelRapport.Location = new Point(288, 164);
            panelRapport.Name = "panelRapport";
            panelRapport.Size = new Size(1360, 655);
            panelRapport.TabIndex = 18;
            // 
            // EmployeeWorkReportUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelRapport);
            Controls.Add(DateTimePickerFiltre);
            Controls.Add(panel1);
            Name = "EmployeeWorkReportUsCtr";
            Size = new Size(1698, 832);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            DateTimePickerFiltre.ResumeLayout(false);
            DateTimePickerFiltre.PerformLayout();
            ResumeLayout(false);
        }

        #endregion  

        private TreeView UsersTreeView;
        private TextBox SearchUserTxt;
        private Label StartDateLabel;
        private Panel panel1;
        private TextBox SearchUserReportTxt;
        private TreeView UsersTreeViewReport;
        private Button Display;
        private DateTimePicker BeguinDate;
        private DateTimePicker EndDate;
        private Panel DateTimePickerFiltre;
        private Label EndDateLabel;
        private EmployeeWorkReport.EmployeeWorkRepUctr employeeWorkRepUctr1;
        private Panel panelRapport;
    }
}
