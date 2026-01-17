using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBillWF.Compagny.CompagnyBase
{
    public partial class VisitFrequencyPricingUctr : UserControl
    {
        public VisitFrequencyPricingUctr()
        {
            InitializeComponent();
            FrquencyWork.Items.AddRange(new string[]
       {
            "semaine",
            "2 semaine",
            "2 fois par mois",
            "1 fois par mois"
       });
        }

        private void FrquencyWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide all configuration group boxes
            BiWeekly.Visible = false;
            Weekly.Visible = false;
            Week1.Visible = false;
            Week2.Visible = false;
            Monthly.Visible = false;

            CompagnyWorkInfo.Size = new System.Drawing.Size(375, 446);

            // Show relevant group box based on selection
            switch (FrquencyWork.SelectedItem?.ToString())
            {
                case "semaine":
                    Weekly.Visible = true;
                    break;
                case "2 semaine":
                    
                case "2 fois par mois":
                    Week1.Visible = true;
                    Week2.Visible = true;

                    BiWeekly.Visible = true;
                    break;

                case "1 fois par mois":
                    Monthly.Visible = true;
                    CompagnyWorkInfo.Size = new System.Drawing.Size(375, 200);
                    break;
            }
        }
    }
}
