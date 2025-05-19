namespace SendBillWF
{
    partial class CompagnyUsCtr
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
            compagnyNamelbl = new Label();
            CompagnieCodelbl = new Label();
            CompagnyStatusTxt = new CheckBox();
            CompagnyNotelbl = new Label();
            Statuslbl = new Label();
            compagnyNameTxt = new TextBox();
            CompagnieCodeTxt = new TextBox();
            compagnieNoteTxt = new TextBox();
            compagnieProviderCBX = new ComboBox();
            lbl = new Label();
            SuspendLayout();
            // 
            // compagnyNamelbl
            // 
            compagnyNamelbl.AutoSize = true;
            compagnyNamelbl.Location = new Point(3, 5);
            compagnyNamelbl.Name = "compagnyNamelbl";
            compagnyNamelbl.Size = new Size(99, 15);
            compagnyNamelbl.TabIndex = 0;
            compagnyNamelbl.Text = "compagny Name";
            // 
            // CompagnieCodelbl
            // 
            CompagnieCodelbl.AutoSize = true;
            CompagnieCodelbl.Location = new Point(339, 5);
            CompagnieCodelbl.Name = "CompagnieCodelbl";
            CompagnieCodelbl.Size = new Size(100, 15);
            CompagnieCodelbl.TabIndex = 1;
            CompagnieCodelbl.Text = "Compagnie Code";
            // 
            // CompagnyStatusTxt
            // 
            CompagnyStatusTxt.AutoSize = true;
            CompagnyStatusTxt.Location = new Point(671, 24);
            CompagnyStatusTxt.Name = "CompagnyStatusTxt";
            CompagnyStatusTxt.Size = new Size(51, 19);
            CompagnyStatusTxt.TabIndex = 2;
            CompagnyStatusTxt.Text = "Actif";
            CompagnyStatusTxt.UseVisualStyleBackColor = true;
            // 
            // CompagnyNotelbl
            // 
            CompagnyNotelbl.AutoSize = true;
            CompagnyNotelbl.Location = new Point(273, 56);
            CompagnyNotelbl.Name = "CompagnyNotelbl";
            CompagnyNotelbl.Size = new Size(33, 15);
            CompagnyNotelbl.TabIndex = 3;
            CompagnyNotelbl.Text = "Note";
            // 
            // Statuslbl
            // 
            Statuslbl.AutoSize = true;
            Statuslbl.Location = new Point(671, 5);
            Statuslbl.Name = "Statuslbl";
            Statuslbl.Size = new Size(39, 15);
            Statuslbl.TabIndex = 4;
            Statuslbl.Text = "Status";
            // 
            // compagnyNameTxt
            // 
            compagnyNameTxt.Location = new Point(3, 23);
            compagnyNameTxt.Name = "compagnyNameTxt";
            compagnyNameTxt.Size = new Size(309, 23);
            compagnyNameTxt.TabIndex = 5;
            compagnyNameTxt.Text = " ";
            // 
            // CompagnieCodeTxt
            // 
            CompagnieCodeTxt.Location = new Point(339, 23);
            CompagnieCodeTxt.Name = "CompagnieCodeTxt";
            CompagnieCodeTxt.Size = new Size(309, 23);
            CompagnieCodeTxt.TabIndex = 6;
            CompagnieCodeTxt.Text = " ";
            // 
            // compagnieNoteTxt
            // 
            compagnieNoteTxt.Location = new Point(273, 74);
            compagnieNoteTxt.Multiline = true;
            compagnieNoteTxt.Name = "compagnieNoteTxt";
            compagnieNoteTxt.Size = new Size(437, 91);
            compagnieNoteTxt.TabIndex = 7;
            compagnieNoteTxt.Text = " ";
            // 
            // compagnieProviderCBX
            // 
            compagnieProviderCBX.FormattingEnabled = true;
            compagnieProviderCBX.Location = new Point(3, 74);
            compagnieProviderCBX.Name = "compagnieProviderCBX";
            compagnieProviderCBX.Size = new Size(212, 23);
            compagnieProviderCBX.TabIndex = 8;
            compagnieProviderCBX.SelectedIndexChanged += compagnieProviderCBX_SelectedIndexChanged;
            // 
            // lbl
            // 
            lbl.AutoSize = true;
            lbl.Location = new Point(3, 56);
            lbl.Name = "lbl";
            lbl.Size = new Size(51, 15);
            lbl.TabIndex = 9;
            lbl.Text = "Provider";
            // 
            // CompagnyUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lbl);
            Controls.Add(compagnieProviderCBX);
            Controls.Add(compagnieNoteTxt);
            Controls.Add(CompagnieCodeTxt);
            Controls.Add(compagnyNameTxt);
            Controls.Add(Statuslbl);
            Controls.Add(CompagnyNotelbl);
            Controls.Add(CompagnyStatusTxt);
            Controls.Add(CompagnieCodelbl);
            Controls.Add(compagnyNamelbl);
            Name = "CompagnyUsCtr";
            Size = new Size(724, 179);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label compagnyNamelbl;
        private Label CompagnieCodelbl;
        private CheckBox CompagnyStatusTxt;
        private Label CompagnyNotelbl;
        private Label Statuslbl;
        private TextBox compagnyNameTxt;
        private TextBox CompagnieCodeTxt;
        private TextBox compagnieNoteTxt;
        private ComboBox compagnieProviderCBX;
        private Label lbl;
    }
}
