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
    public partial class AddressUsctr : UserControl
    {
        public AddressUsctr()
        {
            InitializeComponent();
        }

        public string civicNumberAdress
        {
            get { return civicNumberTxt.Text; }
            set { civicNumberTxt.Text = value; }
        }

        public string suiteAdress
        {
            get { return suiteTxt.Text; }
            set { suiteTxt.Text = value; }
        }

        public string cityAdress
        {
            get { return cityTxt.Text; }
            set { cityTxt.Text = value; }
        }


        public string stateAdress
        {
            get { return stateTxt.Text; }
            set { stateTxt.Text = value; }
        }

        public string countryAdress
        {
            get { return countryTxt.Text; }
            set { countryTxt.Text = value; }
        }

        public string zipCodeAdress
        {
            get { return zipCodeTxt.Text; }
            set { zipCodeTxt.Text = value; }
        }

        public string noteAdress
        {
            get { return adressTxt.Text; }
            set { adressTxt.Text = value; }
        }

    }
}
