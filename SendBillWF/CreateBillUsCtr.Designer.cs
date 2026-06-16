namespace SendBillWF
{
    partial class CreateBillUsCtr
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
            ProviderCBX = new ComboBox();
            ProviderTxt = new Label();
            BillNbrLbl = new Label();
            ClientCBX = new ComboBox();
            ClientLbl = new Label();
            WorkInfoDgrd = new DataGridView();
            QuantityDtg = new DataGridViewTextBoxColumn();
            DescriptionDtg = new DataGridViewTextBoxColumn();
            UnitPrceDgr = new DataGridViewTextBoxColumn();
            SumDtg = new DataGridViewTextBoxColumn();
            label1 = new Label();
            TotalwithoutTaxLbl = new Label();
            TPSLbl = new Label();
            label3 = new Label();
            TVQLbl = new Label();
            label5 = new Label();
            TotalwithTaxLbl = new Label();
            label7 = new Label();
            Save = new Button();
            Cancel = new Button();
            Print = new Button();
            sendBillbtn = new Button();
            billDate = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)WorkInfoDgrd).BeginInit();
            SuspendLayout();
            // 
            // ProviderCBX
            // 
            ProviderCBX.FormattingEnabled = true;
            ProviderCBX.Location = new Point(20, 45);
            ProviderCBX.Name = "ProviderCBX";
            ProviderCBX.Size = new Size(257, 23);
            ProviderCBX.TabIndex = 0;
            ProviderCBX.SelectedIndexChanged += ProviderCBX_SelectedIndexChanged;
            // 
            // ProviderTxt
            // 
            ProviderTxt.AutoSize = true;
            ProviderTxt.Location = new Point(22, 75);
            ProviderTxt.Name = "ProviderTxt";
            ProviderTxt.Size = new Size(38, 15);
            ProviderTxt.TabIndex = 1;
            ProviderTxt.Text = "label1";
            // 
            // BillNbrLbl
            // 
            BillNbrLbl.AutoSize = true;
            BillNbrLbl.Location = new Point(883, 10);
            BillNbrLbl.Name = "BillNbrLbl";
            BillNbrLbl.Size = new Size(38, 15);
            BillNbrLbl.TabIndex = 2;
            BillNbrLbl.Text = "label1";
            // 
            // ClientCBX
            // 
            ClientCBX.FormattingEnabled = true;
            ClientCBX.Location = new Point(20, 158);
            ClientCBX.Name = "ClientCBX";
            ClientCBX.Size = new Size(262, 23);
            ClientCBX.TabIndex = 4;
            ClientCBX.SelectedIndexChanged += ClientCBX_SelectedIndexChanged;
            // 
            // ClientLbl
            // 
            ClientLbl.AutoSize = true;
            ClientLbl.Location = new Point(21, 188);
            ClientLbl.Name = "ClientLbl";
            ClientLbl.Size = new Size(38, 15);
            ClientLbl.TabIndex = 5;
            ClientLbl.Text = "label1";
            // 
            // WorkInfoDgrd
            // 
            WorkInfoDgrd.AllowDrop = true;
            WorkInfoDgrd.AllowUserToOrderColumns = true;
            WorkInfoDgrd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WorkInfoDgrd.Columns.AddRange(new DataGridViewColumn[] { QuantityDtg, DescriptionDtg, UnitPrceDgr, SumDtg });
            WorkInfoDgrd.Location = new Point(20, 310);
            WorkInfoDgrd.Name = "WorkInfoDgrd";
            WorkInfoDgrd.Size = new Size(1030, 299);
            WorkInfoDgrd.TabIndex = 6;
            WorkInfoDgrd.CellEndEdit += WorkInfoDgrd_CellEndEdit;
            WorkInfoDgrd.UserDeletedRow += WorkInfoDgrd_UserDeletedRow;
            // 
            // QuantityDtg
            // 
            QuantityDtg.DataPropertyName = "QuantityDtg";
            QuantityDtg.HeaderText = "Quantity";
            QuantityDtg.Name = "QuantityDtg";
            // 
            // DescriptionDtg
            // 
            DescriptionDtg.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DescriptionDtg.DataPropertyName = "DescriptionDtg";
            DescriptionDtg.HeaderText = "Description";
            DescriptionDtg.Name = "DescriptionDtg";
            // 
            // UnitPrceDgr
            // 
            UnitPrceDgr.DataPropertyName = "UnitPrceDgr";
            UnitPrceDgr.HeaderText = "Unite Price";
            UnitPrceDgr.Name = "UnitPrceDgr";
            // 
            // SumDtg
            // 
            SumDtg.DataPropertyName = "SumDtg";
            SumDtg.HeaderText = "Sum";
            SumDtg.Name = "SumDtg";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(843, 636);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 7;
            label1.Text = "Total without Tax";
            // 
            // TotalwithoutTaxLbl
            // 
            TotalwithoutTaxLbl.AutoSize = true;
            TotalwithoutTaxLbl.Location = new Point(954, 636);
            TotalwithoutTaxLbl.Name = "TotalwithoutTaxLbl";
            TotalwithoutTaxLbl.Size = new Size(0, 15);
            TotalwithoutTaxLbl.TabIndex = 8;
            // 
            // TPSLbl
            // 
            TPSLbl.AutoSize = true;
            TPSLbl.Location = new Point(954, 662);
            TPSLbl.Name = "TPSLbl";
            TPSLbl.Size = new Size(38, 15);
            TPSLbl.TabIndex = 10;
            TPSLbl.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(843, 662);
            label3.Name = "label3";
            label3.Size = new Size(26, 15);
            label3.TabIndex = 9;
            label3.Text = "TPS";
            // 
            // TVQLbl
            // 
            TVQLbl.AutoSize = true;
            TVQLbl.Location = new Point(954, 691);
            TVQLbl.Name = "TVQLbl";
            TVQLbl.Size = new Size(38, 15);
            TVQLbl.TabIndex = 12;
            TVQLbl.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(843, 691);
            label5.Name = "label5";
            label5.Size = new Size(29, 15);
            label5.TabIndex = 11;
            label5.Text = "TVQ";
            // 
            // TotalwithTaxLbl
            // 
            TotalwithTaxLbl.AutoSize = true;
            TotalwithTaxLbl.Location = new Point(954, 728);
            TotalwithTaxLbl.Name = "TotalwithTaxLbl";
            TotalwithTaxLbl.Size = new Size(38, 15);
            TotalwithTaxLbl.TabIndex = 14;
            TotalwithTaxLbl.Text = "label6";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(843, 728);
            label7.Name = "label7";
            label7.Size = new Size(78, 15);
            label7.TabIndex = 13;
            label7.Text = "Total with Tax";
            // 
            // Save
            // 
            Save.Location = new Point(22, 787);
            Save.Name = "Save";
            Save.Size = new Size(207, 28);
            Save.TabIndex = 15;
            Save.Text = "Enregistrer";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // Cancel
            // 
            Cancel.Location = new Point(807, 787);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(207, 28);
            Cancel.TabIndex = 16;
            Cancel.Text = "Annuler";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // Print
            // 
            Print.Location = new Point(271, 787);
            Print.Name = "Print";
            Print.Size = new Size(207, 28);
            Print.TabIndex = 17;
            Print.Text = "Imprimer";
            Print.UseVisualStyleBackColor = true;
            Print.Click += Print_Click;
            // 
            // sendBillbtn
            // 
            sendBillbtn.Location = new Point(498, 787);
            sendBillbtn.Name = "sendBillbtn";
            sendBillbtn.Size = new Size(207, 28);
            sendBillbtn.TabIndex = 18;
            sendBillbtn.Text = "envoyè";
            sendBillbtn.UseVisualStyleBackColor = true;
            sendBillbtn.Click += sendBillbtn_Click;
            // 
            // billDate
            // 
            billDate.Location = new Point(866, 45);
            billDate.Name = "billDate";
            billDate.Size = new Size(166, 23);
            billDate.TabIndex = 19;
            billDate.ValueChanged += billDate_ValueChanged;
            // 
            // CreateBillUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(billDate);
            Controls.Add(sendBillbtn);
            Controls.Add(Print);
            Controls.Add(Cancel);
            Controls.Add(Save);
            Controls.Add(TotalwithTaxLbl);
            Controls.Add(label7);
            Controls.Add(TVQLbl);
            Controls.Add(label5);
            Controls.Add(TPSLbl);
            Controls.Add(label3);
            Controls.Add(TotalwithoutTaxLbl);
            Controls.Add(label1);
            Controls.Add(WorkInfoDgrd);
            Controls.Add(ClientLbl);
            Controls.Add(ClientCBX);
            Controls.Add(BillNbrLbl);
            Controls.Add(ProviderTxt);
            Controls.Add(ProviderCBX);
            Name = "CreateBillUsCtr";
            Size = new Size(1094, 834);
            ((System.ComponentModel.ISupportInitialize)WorkInfoDgrd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ProviderCBX;
        private Label ProviderTxt;
        private Label BillNbrLbl;
        private ComboBox ClientCBX;
        private Label ClientLbl;
        private DataGridView WorkInfoDgrd;
        private Label label1;
        private Label TotalwithoutTaxLbl;
        private Label TPSLbl;
        private Label label3;
        private Label TVQLbl;
        private Label label5;
        private Label TotalwithTaxLbl;
        private Label label7;
        private Button Save;
        private Button Cancel;
        private Button Print;
        private Button sendBillbtn;
        private DateTimePicker billDate;
        private DataGridViewTextBoxColumn QuantityDtg;
        private DataGridViewTextBoxColumn DescriptionDtg;
        private DataGridViewTextBoxColumn UnitPrceDgr;
        private DataGridViewTextBoxColumn SumDtg;
    }
}
