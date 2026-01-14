namespace SendBillWF.Bill
{
    partial class UpdateBillUsCtr
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
            SearchLbl = new Button();
            SearchTxt = new TextBox();
            label1 = new Label();
            createBillUpdateUsCtr1 = new CreateBillUsCtr();
            SuspendLayout();
            // 
            // SearchLbl
            // 
            SearchLbl.Location = new Point(349, 26);
            SearchLbl.Name = "SearchLbl";
            SearchLbl.Size = new Size(75, 23);
            SearchLbl.TabIndex = 0;
            SearchLbl.Text = "chercher";
            SearchLbl.UseVisualStyleBackColor = true;
            SearchLbl.Click += SearchLbl_Click;
            // 
            // SearchTxt
            // 
            SearchTxt.Location = new Point(162, 27);
            SearchTxt.Name = "SearchTxt";
            SearchTxt.Size = new Size(165, 23);
            SearchTxt.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 30);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 2;
            label1.Text = "Numero Facture";
            // 
            // createBillUpdateUsCtr1
            // 
            createBillUpdateUsCtr1.Location = new Point(25, 68);
            createBillUpdateUsCtr1.Name = "createBillUpdateUsCtr1";
            createBillUpdateUsCtr1.Size = new Size(1094, 834);
            createBillUpdateUsCtr1.TabIndex = 3;
            // 
            // UpdateBillUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(createBillUpdateUsCtr1);
            Controls.Add(label1);
            Controls.Add(SearchTxt);
            Controls.Add(SearchLbl);
            Name = "UpdateBillUsCtr";
            Size = new Size(1173, 921);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SearchLbl;
        private TextBox SearchTxt;
        private Label label1;
        private CreateBillUsCtr createBillUpdateUsCtr1;
    }
}
