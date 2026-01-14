namespace SendBillWF
{
    partial class UpdateCmpUsctr
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
            CmpUsCtr = new CompagnyUsCtr();
            addUsctr = new AddressUsctr();
            custUsCtr = new CustomUsCtr();
            UpddateCmp = new Button();
            CncelUpdate = new Button();
            dataGridView1 = new DataGridView();
            SearchTxtUp = new TextBox();
            SearchbtnUp = new Button();
            compagniesGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)compagniesGrid).BeginInit();
            SuspendLayout();
            // 
            // CmpUsCtr
            // 
            CmpUsCtr.CompagnieCodeCompagny = "";
            CmpUsCtr.compagnieNoteCompagny = "";
            CmpUsCtr.compagnieStatusCompagny = true;
            CmpUsCtr.compagnyNameCompagny = "";
            CmpUsCtr.Location = new Point(39, 198);
            CmpUsCtr.Name = "CmpUsCtr";
            CmpUsCtr.Size = new Size(765, 188);
            CmpUsCtr.TabIndex = 0;
            // 
            // addUsctr
            // 
            addUsctr.cityAdress = " ";
            addUsctr.civicNumberAdress = " ";
            addUsctr.countryAdress = "Canada";
            addUsctr.Location = new Point(39, 392);
            addUsctr.Name = "addUsctr";
            addUsctr.noteAdress = "";
            addUsctr.Size = new Size(513, 223);
            addUsctr.stateAdress = "";
            addUsctr.suiteAdress = "";
            addUsctr.TabIndex = 1;
            addUsctr.zipCodeAdress = "";
            // 
            // custUsCtr
            // 
            custUsCtr.CustomMailCustom = "";
            custUsCtr.CustomNameCustom = "";
            custUsCtr.CustomNoteCustom = "";
            custUsCtr.CustomPhoneCustom = "";
            custUsCtr.Location = new Point(39, 618);
            custUsCtr.Name = "custUsCtr";
            custUsCtr.Size = new Size(642, 163);
            custUsCtr.TabIndex = 2;
            // 
            // UpddateCmp
            // 
            UpddateCmp.Location = new Point(132, 787);
            UpddateCmp.Name = "UpddateCmp";
            UpddateCmp.Size = new Size(186, 39);
            UpddateCmp.TabIndex = 3;
            UpddateCmp.Text = "Update";
            UpddateCmp.UseVisualStyleBackColor = true;
            UpddateCmp.Click += UpddateCmp_Click;
            // 
            // CncelUpdate
            // 
            CncelUpdate.Location = new Point(394, 787);
            CncelUpdate.Name = "CncelUpdate";
            CncelUpdate.Size = new Size(186, 39);
            CncelUpdate.TabIndex = 4;
            CncelUpdate.Text = "Cancel";
            CncelUpdate.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(39, 51);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(793, 80);
            dataGridView1.TabIndex = 5;
            // 
            // SearchTxtUp
            // 
            SearchTxtUp.Location = new Point(39, 12);
            SearchTxtUp.Name = "SearchTxtUp";
            SearchTxtUp.Size = new Size(158, 23);
            SearchTxtUp.TabIndex = 6;
            // 
            // SearchbtnUp
            // 
            SearchbtnUp.Location = new Point(233, 12);
            SearchbtnUp.Name = "SearchbtnUp";
            SearchbtnUp.Size = new Size(63, 23);
            SearchbtnUp.TabIndex = 7;
            SearchbtnUp.Text = "Search";
            SearchbtnUp.UseVisualStyleBackColor = true;
            SearchbtnUp.Click += SearchbtnUp_Click;
            // 
            // compagniesGrid
            // 
            compagniesGrid.AllowUserToAddRows = false;
            compagniesGrid.AllowUserToDeleteRows = false;
            compagniesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            compagniesGrid.Location = new Point(39, 52);
            compagniesGrid.Name = "compagniesGrid";
            compagniesGrid.ReadOnly = true;
            compagniesGrid.Size = new Size(1027, 140);
            compagniesGrid.TabIndex = 8;
            compagniesGrid.CellClick += compagniesGrid_CellClick;
            // 
            // UpdateCmpUsctr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(compagniesGrid);
            Controls.Add(SearchbtnUp);
            Controls.Add(SearchTxtUp);
            Controls.Add(dataGridView1);
            Controls.Add(CncelUpdate);
            Controls.Add(UpddateCmp);
            Controls.Add(custUsCtr);
            Controls.Add(addUsctr);
            Controls.Add(CmpUsCtr);
            Name = "UpdateCmpUsctr";
            Size = new Size(1139, 840);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)compagniesGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CompagnyUsCtr CmpUsCtr;
        private AddressUsctr addUsctr;
        private CustomUsCtr custUsCtr;
        private Button UpddateCmp;
        private Button CncelUpdate;
        private DataGridView dataGridView1;
        private TextBox SearchTxtUp;
        private Button SearchbtnUp;
        private DataGridView compagniesGrid;
    }
}
