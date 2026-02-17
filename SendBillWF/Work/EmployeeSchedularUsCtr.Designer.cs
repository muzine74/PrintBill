namespace SendBillWF.Work
{
    partial class EmployeeSchedularUsCtr
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
            visitWorkSheetUctr1 = new VisitWorkSheetUctr();
            WeekToFill = new DateTimePicker();
            label1 = new Label();
            SaveWork = new Button();
            button2 = new Button();
            UsersTreeView = new TreeView();
            panel1 = new Panel();
            SearchUserTxt = new TextBox();
            employeeUsSchedulair = new SendBillWF.Employee.EmployeeUsCtr();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // visitWorkSheetUctr1
            // 
            visitWorkSheetUctr1.Location = new Point(258, 208);
            visitWorkSheetUctr1.Name = "visitWorkSheetUctr1";
            visitWorkSheetUctr1.Size = new Size(987, 368);
            visitWorkSheetUctr1.TabIndex = 0;
            // 
            // WeekToFill
            // 
            WeekToFill.Location = new Point(866, 154);
            WeekToFill.Name = "WeekToFill";
            WeekToFill.Size = new Size(200, 23);
            WeekToFill.TabIndex = 1;
           // WeekToFill.ValueChanged += WeekToFill_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(636, 162);
            label1.Name = "label1";
            label1.Size = new Size(204, 15);
            label1.TabIndex = 2;
            label1.Text = "Choisir une journée dans la date cible";
            // 
            // SaveWork
            // 
            SaveWork.Location = new Point(275, 699);
            SaveWork.Name = "SaveWork";
            SaveWork.Size = new Size(140, 41);
            SaveWork.TabIndex = 3;
            SaveWork.Text = "Save";
            SaveWork.UseVisualStyleBackColor = true;
            //SaveWork.Click += SaveWork_Click;
            // 
            // button2
            // 
            button2.Location = new Point(513, 696);
            button2.Name = "button2";
            button2.Size = new Size(151, 46);
            button2.TabIndex = 4;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // UsersTreeView
            // 
            UsersTreeView.Location = new Point(0, 30);
            UsersTreeView.Name = "UsersTreeView";
            UsersTreeView.Size = new Size(225, 670);
            UsersTreeView.TabIndex = 10;
            //UsersTreeView.AfterSelect += UsersTreeView_AfterSelect;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(SearchUserTxt);
            panel1.Controls.Add(UsersTreeView);
            panel1.Location = new Point(12, 54);
            panel1.Name = "panel1";
            panel1.Size = new Size(225, 700);
            panel1.TabIndex = 11;
            // 
            // SearchUserTxt
            // 
            SearchUserTxt.Location = new Point(3, 5);
            SearchUserTxt.Name = "SearchUserTxt";
            SearchUserTxt.Size = new Size(219, 23);
            SearchUserTxt.TabIndex = 11;
            //SearchUserTxt.TextChanged += txtSearch_TextChanged;
            // 
            // employeeUsSchedulair
            // 
            employeeUsSchedulair.Location = new Point(267, 3);
            employeeUsSchedulair.MailEmployee = "";
            employeeUsSchedulair.Name = "employeeUsSchedulair";
            employeeUsSchedulair.NameEmployee = "asdds";
            employeeUsSchedulair.NasEmployee = "";
            employeeUsSchedulair.NoteEmployee = "";
            employeeUsSchedulair.PhoneEmployee = "";
            employeeUsSchedulair.Size = new Size(342, 188);
            employeeUsSchedulair.TabIndex = 12;
            // 
            // EmployeeSchedularUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(employeeUsSchedulair);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(SaveWork);
            Controls.Add(label1);
            Controls.Add(WeekToFill);
            Controls.Add(visitWorkSheetUctr1);
            Name = "EmployeeSchedularUsCtr";
            Size = new Size(1582, 757);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private VisitWorkSheetUctr visitWorkSheetUctr1;
        private DateTimePicker WeekToFill;
        private Label label1;
        private Button SaveWork;
        private Button button2;
        private TreeView UsersTreeView;
        private Panel panel1;
        private TextBox SearchUserTxt;
        private Employee.EmployeeUsCtr employeeUsSchedulair;
    }
}
