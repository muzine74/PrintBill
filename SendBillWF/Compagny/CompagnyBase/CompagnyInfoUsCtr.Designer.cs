namespace SendBillWF.Compagny.CompagnyBase
{
    partial class CompagnyInfoUsCtr
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
            compagnyUsCtr1 = new CompagnyUsCtr();
            addressUsctr1 = new AddressUsctr();
            SuspendLayout();
            // 
            // compagnyUsCtr1
            // 
            compagnyUsCtr1.CompagnieCodeCompagny = " ";
            compagnyUsCtr1.compagnieNoteCompagny = " ";
            compagnyUsCtr1.compagnieStatusCompagny = false;
            compagnyUsCtr1.compagnyNameCompagny = " ";
            compagnyUsCtr1.Location = new Point(3, 3);
            compagnyUsCtr1.Name = "compagnyUsCtr1";
            compagnyUsCtr1.Size = new Size(724, 179);
            compagnyUsCtr1.TabIndex = 0;
            // 
            // addressUsctr1
            // 
            addressUsctr1.cityAdress = "";
            addressUsctr1.civicNumberAdress = "";
            addressUsctr1.countryAdress = "";
            addressUsctr1.Location = new Point(3, 188);
            addressUsctr1.Name = "addressUsctr1";
            addressUsctr1.noteAdress = "";
            addressUsctr1.Size = new Size(720, 253);
            addressUsctr1.stateAdress = "";
            addressUsctr1.suiteAdress = "";
            addressUsctr1.TabIndex = 1;
            addressUsctr1.zipCodeAdress = "";
            // 
            // CompagnyInfoUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(addressUsctr1);
            Controls.Add(compagnyUsCtr1);
            Name = "CompagnyInfoUsCtr";
            Size = new Size(1253, 789);
            ResumeLayout(false);
        }

        #endregion

        private CompagnyUsCtr compagnyUsCtr1;
        private AddressUsctr addressUsctr1;
    }
}
