namespace SendBillWF
{
    partial class BillHistoryUsctr
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
            components = new System.ComponentModel.Container();
            SearchbtnHsBtn = new Button();
            keySearchtxt = new TextBox();
            BeginDate = new DateTimePicker();
            EndDate = new DateTimePicker();
            BillHistoryDGrid = new DataGridView();
            PrintMenuStrip = new ContextMenuStrip(components);
            SendedCheckBox = new CheckedListBox();
            payedListBox = new CheckedListBox();
            label1 = new Label();
            label2 = new Label();
            intervalDatecheckBox = new CheckBox();
            printFile = new PrintDialog();
            ((System.ComponentModel.ISupportInitialize)BillHistoryDGrid).BeginInit();
            SuspendLayout();
            // 
            // SearchbtnHsBtn
            // 
            SearchbtnHsBtn.Location = new Point(47, 97);
            SearchbtnHsBtn.Name = "SearchbtnHsBtn";
            SearchbtnHsBtn.Size = new Size(368, 38);
            SearchbtnHsBtn.TabIndex = 0;
            SearchbtnHsBtn.Text = "Search";
            SearchbtnHsBtn.UseVisualStyleBackColor = true;
            SearchbtnHsBtn.Click += SearchbtnHsBtn_Click;
            // 
            // keySearchtxt
            // 
            keySearchtxt.Location = new Point(47, 54);
            keySearchtxt.Name = "keySearchtxt";
            keySearchtxt.Size = new Size(235, 23);
            keySearchtxt.TabIndex = 1;
            // 
            // BeginDate
            // 
            BeginDate.Enabled = false;
            BeginDate.Location = new Point(861, 33);
            BeginDate.Name = "BeginDate";
            BeginDate.Size = new Size(200, 23);
            BeginDate.TabIndex = 2;
            // 
            // EndDate
            // 
            EndDate.Enabled = false;
            EndDate.Location = new Point(861, 74);
            EndDate.Name = "EndDate";
            EndDate.Size = new Size(200, 23);
            EndDate.TabIndex = 3;
            // 
            // BillHistoryDGrid
            // 
            BillHistoryDGrid.AllowUserToAddRows = false;
            BillHistoryDGrid.AllowUserToDeleteRows = false;
            BillHistoryDGrid.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BillHistoryDGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            BillHistoryDGrid.ContextMenuStrip = PrintMenuStrip;
            BillHistoryDGrid.Location = new Point(12, 141);
            BillHistoryDGrid.Name = "BillHistoryDGrid";
            BillHistoryDGrid.Size = new Size(1064, 678);
            BillHistoryDGrid.TabIndex = 4;
            BillHistoryDGrid.MouseDown += BillHistoryDGrid_MouseDown;
            // 
            // PrintMenuStrip
            // 
            PrintMenuStrip.Name = "PrintMenuStrip";
            PrintMenuStrip.Size = new Size(61, 4);
            // 
            // SendedCheckBox
            // 
            SendedCheckBox.FormattingEnabled = true;
            SendedCheckBox.Items.AddRange(new object[] { "Envoye", "NonEnvoye" });
            SendedCheckBox.Location = new Point(367, 37);
            SendedCheckBox.Name = "SendedCheckBox";
            SendedCheckBox.Size = new Size(120, 40);
            SendedCheckBox.TabIndex = 5;
            // 
            // payedListBox
            // 
            payedListBox.FormattingEnabled = true;
            payedListBox.Items.AddRange(new object[] { "payer", "NonPayer" });
            payedListBox.Location = new Point(545, 36);
            payedListBox.Name = "payedListBox";
            payedListBox.Size = new Size(120, 40);
            payedListBox.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(837, 36);
            label1.Name = "label1";
            label1.Size = new Size(22, 15);
            label1.TabIndex = 7;
            label1.Text = "Du";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(836, 78);
            label2.Name = "label2";
            label2.Size = new Size(22, 15);
            label2.TabIndex = 8;
            label2.Text = "Au";
            // 
            // intervalDatecheckBox
            // 
            intervalDatecheckBox.AutoSize = true;
            intervalDatecheckBox.Location = new Point(754, 54);
            intervalDatecheckBox.Name = "intervalDatecheckBox";
            intervalDatecheckBox.Size = new Size(66, 19);
            intervalDatecheckBox.TabIndex = 9;
            intervalDatecheckBox.Text = "periode";
            intervalDatecheckBox.UseVisualStyleBackColor = true;
            intervalDatecheckBox.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // printFile
            // 
            printFile.UseEXDialog = true;
            // 
            // BillHistoryUsctr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(intervalDatecheckBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(payedListBox);
            Controls.Add(SendedCheckBox);
            Controls.Add(BillHistoryDGrid);
            Controls.Add(EndDate);
            Controls.Add(BeginDate);
            Controls.Add(keySearchtxt);
            Controls.Add(SearchbtnHsBtn);
            Name = "BillHistoryUsctr";
            Size = new Size(1100, 862);
            ((System.ComponentModel.ISupportInitialize)BillHistoryDGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SearchbtnHsBtn;
        private TextBox keySearchtxt;
        private DateTimePicker BeginDate;
        private DateTimePicker EndDate;
        private DataGridView BillHistoryDGrid;
        private CheckedListBox SendedCheckBox;
        private CheckedListBox payedListBox;
        private Label label1;
        private Label label2;
        private CheckBox intervalDatecheckBox;
        private PrintDialog printFile;
        private ContextMenuStrip PrintMenuStrip;
        //private ToolStripMenuItem toolPrintMenuStrip;
        //private CheckedListBox checkedListBox1;
    }
}
