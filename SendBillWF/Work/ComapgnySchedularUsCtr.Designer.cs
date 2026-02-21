namespace SendBillWF.Work
{
    partial class ComapgnySchedularUsCtr
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
            CompagnytreeView = new TreeView();
            SearchCompagnyTxt = new TextBox();
            compagniBase = new SendBillWF.Work.Base.CompagniBase();
            WeekToFill = new DateTimePicker();
            label1 = new Label();
            visitWorkSheetUctr1 = new VisitWorkSheetUctr();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(CompagnytreeView);
            panel1.Controls.Add(SearchCompagnyTxt);
            panel1.Location = new Point(19, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(260, 780);
            panel1.TabIndex = 0;
            // 
            // CompagnytreeView
            // 
            CompagnytreeView.Location = new Point(3, 29);
            CompagnytreeView.Name = "CompagnytreeView";
            CompagnytreeView.Size = new Size(254, 748);
            CompagnytreeView.TabIndex = 1;
            // 
            // SearchCompagnyTxt
            // 
            SearchCompagnyTxt.Location = new Point(3, 3);
            SearchCompagnyTxt.Name = "SearchCompagnyTxt";
            SearchCompagnyTxt.Size = new Size(254, 23);
            SearchCompagnyTxt.TabIndex = 0;
            SearchCompagnyTxt.TextChanged += SearchCompagnyTxt_TextChanged;
            // 
            // compagniBase
            // 
            compagniBase.BackColor = Color.FromArgb(250, 250, 250);
            compagniBase.CompagnyBaseAdress = "label6";
            compagniBase.CompagnyBaseCode = "LABEL5";
            compagniBase.CompagnyBaseIsActive = false;
            compagniBase.CompagnyBaseName = "label4";
            compagniBase.Location = new Point(293, 30);
            compagniBase.Name = "compagniBase";
            compagniBase.Padding = new Padding(15);
            compagniBase.Size = new Size(600, 179);
            compagniBase.TabIndex = 1;
            // 
            // WeekToFill
            // 
            WeekToFill.Location = new Point(1039, 216);
            WeekToFill.Name = "WeekToFill";
            WeekToFill.Size = new Size(200, 23);
            WeekToFill.TabIndex = 3;
            WeekToFill.ValueChanged += WeekToFill_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(907, 224);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 4;
            label1.Text = "choisir la date :";
            // 
            // visitWorkSheetUctr1
            // 
            visitWorkSheetUctr1.Location = new Point(286, 259);
            visitWorkSheetUctr1.Name = "visitWorkSheetUctr1";
            visitWorkSheetUctr1.Size = new Size(990, 386);
            visitWorkSheetUctr1.TabIndex = 5;
            // 
            // ComapgnySchedularUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(visitWorkSheetUctr1);
            Controls.Add(label1);
            Controls.Add(WeekToFill);
            Controls.Add(compagniBase);
            Controls.Add(panel1);
            Name = "ComapgnySchedularUsCtr";
            Size = new Size(1276, 810);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TreeView CompagnytreeView;
        private TextBox SearchCompagnyTxt;
        private Base.CompagniBase compagniBase;
        private DateTimePicker WeekToFill;
        private Label label1;
        private VisitWorkSheetUctr visitWorkSheetUctr1;
    }
}
