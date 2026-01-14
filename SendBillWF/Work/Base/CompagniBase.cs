using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBillWF.Work.Base
{
    public partial class CompagniBase : UserControl
    {
        public CompagniBase()
        {
            InitializeComponent();
        }

        public string CompagnyBaseName
        {
            get { return CompagnyName.Text; }
            set { CompagnyName.Text = value; }
        }

        public string CompagnyBaseCode
        {
            get { return CompagnyCode.Text; }
            set { CompagnyCode.Text = value; }
        }


        public string CompagnyBaseAdress
        {
            get { return ComapgnyAdress.Text; }
            set { ComapgnyAdress.Text = value; }
        }


        public bool CompagnyBaseIsActive
        {
            get { return IsActiveComapgny.Checked; }
            set { IsActiveComapgny.Checked = value; }
        }

    }
}


