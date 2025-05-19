namespace SendBillWF
{
    partial class CustomUsCtr
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
            CustomNameTxt = new TextBox();
            CustomMailTxt = new TextBox();
            label2 = new Label();
            CustomPhoneTxt = new TextBox();
            label3 = new Label();
            CustomNoteTxt = new TextBox();
            AdressNote = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 9);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // CustomNameTxt
            // 
            CustomNameTxt.Location = new Point(15, 27);
            CustomNameTxt.Name = "CustomNameTxt";
            CustomNameTxt.Size = new Size(215, 23);
            CustomNameTxt.TabIndex = 1;
            CustomNameTxt.Text = " ";
            // 
            // CustomMailTxt
            // 
            CustomMailTxt.Location = new Point(15, 80);
            CustomMailTxt.Name = "CustomMailTxt";
            CustomMailTxt.Size = new Size(215, 23);
            CustomMailTxt.TabIndex = 3;
            CustomMailTxt.Text = " ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 62);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 2;
            label2.Text = "Mail";
            // 
            // CustomPhoneTxt
            // 
            CustomPhoneTxt.Location = new Point(15, 134);
            CustomPhoneTxt.Name = "CustomPhoneTxt";
            CustomPhoneTxt.Size = new Size(215, 23);
            CustomPhoneTxt.TabIndex = 5;
            CustomPhoneTxt.Text = " ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 116);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 4;
            label3.Text = "Phone";
            // 
            // CustomNoteTxt
            // 
            CustomNoteTxt.Location = new Point(294, 27);
            CustomNoteTxt.Multiline = true;
            CustomNoteTxt.Name = "CustomNoteTxt";
            CustomNoteTxt.Size = new Size(310, 130);
            CustomNoteTxt.TabIndex = 7;
            CustomNoteTxt.Text = " ";
            // 
            // AdressNote
            // 
            AdressNote.AutoSize = true;
            AdressNote.Location = new Point(294, 9);
            AdressNote.Name = "AdressNote";
            AdressNote.Size = new Size(33, 15);
            AdressNote.TabIndex = 6;
            AdressNote.Text = "Note";
            // 
            // CustomUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CustomNoteTxt);
            Controls.Add(AdressNote);
            Controls.Add(CustomPhoneTxt);
            Controls.Add(label3);
            Controls.Add(CustomMailTxt);
            Controls.Add(label2);
            Controls.Add(CustomNameTxt);
            Controls.Add(label1);
            Name = "CustomUsCtr";
            Size = new Size(616, 170);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox CustomNameTxt;
        private TextBox CustomMailTxt;
        private Label label2;
        private TextBox CustomPhoneTxt;
        private Label label3;
        private TextBox CustomNoteTxt;
        private Label AdressNote;
    }
}
