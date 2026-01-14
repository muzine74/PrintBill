using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBillWF
{
    public partial class TaxUsCtr : UserControl
    {
        public TaxUsCtr()
        {
            InitializeComponent();
        }

        public string compagnyTPSCompagny
        {
            get { return TPSNumber.Text; }
            set { TPSNumber.Text = value; }
        }

        public string compagnyTVQCompagny
        {
            get { return TVQNumber.Text; }
            set { TVQNumber.Text = value; }
        }
    }
}
