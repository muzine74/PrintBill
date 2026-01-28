namespace SendBillWF
{
    partial class test
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            visitFrequencyPricingUctr1 = new SendBillWF.Compagny.CompagnyBase.VisitFrequencyPricingUctr();
            SuspendLayout();
            // 
            // visitFrequencyPricingUctr1
            // 
            visitFrequencyPricingUctr1.Location = new Point(118, 58);
            visitFrequencyPricingUctr1.Name = "visitFrequencyPricingUctr1";
            visitFrequencyPricingUctr1.Size = new Size(1259, 865);
            visitFrequencyPricingUctr1.TabIndex = 0;
            // 
            // test
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1220, 756);
            Controls.Add(visitFrequencyPricingUctr1);
            Name = "test";
            Text = "test";
            ResumeLayout(false);
        }

        #endregion

        private Compagny.CompagnyBase.VisitFrequencyPricingUctr visitFrequencyPricingUctr1;
    }
}