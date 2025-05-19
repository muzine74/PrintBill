namespace SendBillWF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            From = new TextBox();
            To = new TextBox();
            label3 = new Label();
            AttachedFile = new Label();
            label4 = new Label();
            MessageBody = new TextBox();
            splitContainer1 = new SplitContainer();
            Begin = new Button();
            Subject = new TextBox();
            label5 = new Label();
            NextMail = new Button();
            Send = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 7);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 0;
            label1.Text = "From";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 66);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 1;
            label2.Text = "To";
            // 
            // From
            // 
            From.Location = new Point(8, 23);
            From.Name = "From";
            From.Size = new Size(325, 23);
            From.TabIndex = 2;
            // 
            // To
            // 
            To.Location = new Point(10, 85);
            To.Name = "To";
            To.Size = new Size(320, 23);
            To.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 127);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 4;
            label3.Text = "Attache File";
            // 
            // AttachedFile
            // 
            AttachedFile.AutoSize = true;
            AttachedFile.Location = new Point(13, 151);
            AttachedFile.Name = "AttachedFile";
            AttachedFile.Size = new Size(0, 15);
            AttachedFile.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 260);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 6;
            label4.Text = "Message";
            // 
            // MessageBody
            // 
            MessageBody.Location = new Point(12, 279);
            MessageBody.Multiline = true;
            MessageBody.Name = "MessageBody";
            MessageBody.Size = new Size(429, 228);
            MessageBody.TabIndex = 7;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.ControlLight;
            splitContainer1.Panel1.Controls.Add(Begin);
            splitContainer1.Panel1.Controls.Add(Subject);
            splitContainer1.Panel1.Controls.Add(label5);
            splitContainer1.Panel1.Controls.Add(NextMail);
            splitContainer1.Panel1.Controls.Add(Send);
            splitContainer1.Panel1.Controls.Add(MessageBody);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(label4);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(AttachedFile);
            splitContainer1.Panel1.Controls.Add(From);
            splitContainer1.Panel1.Controls.Add(label3);
            splitContainer1.Panel1.Controls.Add(To);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.ControlLight;
            splitContainer1.Size = new Size(1391, 814);
            splitContainer1.SplitterDistance = 466;
            splitContainer1.TabIndex = 8;
            // 
            // Begin
            // 
            Begin.Location = new Point(48, 660);
            Begin.Name = "Begin";
            Begin.Size = new Size(75, 23);
            Begin.TabIndex = 12;
            Begin.Text = "button1";
            Begin.UseVisualStyleBackColor = true;
            Begin.Click += Begin_Click;
            // 
            // Subject
            // 
            Subject.Location = new Point(12, 217);
            Subject.Name = "Subject";
            Subject.Size = new Size(429, 23);
            Subject.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 199);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 10;
            label5.Text = "Subject";
            // 
            // NextMail
            // 
            NextMail.Location = new Point(210, 572);
            NextMail.Name = "NextMail";
            NextMail.Size = new Size(120, 33);
            NextMail.TabIndex = 9;
            NextMail.Text = "Next ...";
            NextMail.UseVisualStyleBackColor = true;
            // 
            // Send
            // 
            Send.Location = new Point(27, 572);
            Send.Name = "Send";
            Send.Size = new Size(120, 33);
            Send.TabIndex = 8;
            Send.Text = "Send";
            Send.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1415, 824);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox From;
        private TextBox To;
        private Label label3;
        private Label AttachedFile;
        private Label label4;
        private TextBox MessageBody;
        private SplitContainer splitContainer1;
        private Button NextMail;
        private Button Send;
        private TextBox Subject;
        private Label label5;
        private Button Begin;
    }
}
