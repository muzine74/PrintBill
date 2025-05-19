namespace SendBillWF
{
    partial class AddCompagnyusctr
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
            AddCompagny = new Button();
            compagnyUsCtr1 = new CompagnyUsCtr();
            addressUsctr1 = new AddressUsctr();
            customUsCtr1 = new CustomUsCtr();
            CancelCompBtn = new Button();
            SuspendLayout();
            // 
            // AddCompagny
            // 
            AddCompagny.Location = new Point(153, 647);
            AddCompagny.Name = "AddCompagny";
            AddCompagny.Size = new Size(75, 23);
            AddCompagny.TabIndex = 0;
            AddCompagny.Text = "Ajouter";
            AddCompagny.UseVisualStyleBackColor = true;
            AddCompagny.Click += AddCompagny_Click;
            // 
            // compagnyUsCtr1
            // 
            compagnyUsCtr1.CompagnieCodeCompagny = "";
            compagnyUsCtr1.compagnieNoteCompagny = "";
            compagnyUsCtr1.compagnieStatusCompagny = false;
            compagnyUsCtr1.compagnyNameCompagny = "";
            compagnyUsCtr1.Location = new Point(3, 3);
            compagnyUsCtr1.Name = "compagnyUsCtr1";
            compagnyUsCtr1.Size = new Size(724, 179);
            compagnyUsCtr1.TabIndex = 1;
            // 
            // addressUsctr1
            // 
            addressUsctr1.cityAdress = "";
            addressUsctr1.civicNumberAdress = "";
            addressUsctr1.countryAdress = "Canada";
            addressUsctr1.Location = new Point(3, 204);
            addressUsctr1.Name = "addressUsctr1";
            addressUsctr1.noteAdress = "";
            addressUsctr1.Size = new Size(478, 214);
            addressUsctr1.stateAdress = "";
            addressUsctr1.suiteAdress = "";
            addressUsctr1.TabIndex = 2;
            addressUsctr1.zipCodeAdress = "";
            // 
            // customUsCtr1
            // 
            customUsCtr1.CustomMailCustom = "";
            customUsCtr1.CustomNameCustom = "";
            customUsCtr1.CustomNoteCustom = "";
            customUsCtr1.CustomPhoneCustom = "";
            customUsCtr1.Location = new Point(3, 424);
            customUsCtr1.Name = "customUsCtr1";
            customUsCtr1.Size = new Size(616, 170);
            customUsCtr1.TabIndex = 3;
            // 
            // CancelCompBtn
            // 
            CancelCompBtn.Location = new Point(456, 644);
            CancelCompBtn.Name = "CancelCompBtn";
            CancelCompBtn.Size = new Size(75, 23);
            CancelCompBtn.TabIndex = 4;
            CancelCompBtn.Text = "Cancel";
            CancelCompBtn.UseVisualStyleBackColor = true;
            // 
            // AddCompagnyusctr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CancelCompBtn);
            Controls.Add(customUsCtr1);
            Controls.Add(addressUsctr1);
            Controls.Add(compagnyUsCtr1);
            Controls.Add(AddCompagny);
            Name = "AddCompagnyusctr";
            Size = new Size(796, 681);
            ResumeLayout(false);
        }

        #endregion

        private Button AddCompagny;
        private CompagnyUsCtr compagnyUsCtr1;
        private AddressUsctr addressUsctr1;
        private CustomUsCtr customUsCtr1;
        private Button CancelCompBtn;
    }
}
