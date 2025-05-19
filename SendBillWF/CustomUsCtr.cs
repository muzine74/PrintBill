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
    public partial class CustomUsCtr : UserControl
    {
        public CustomUsCtr()
        {
            InitializeComponent();
        }

        public string CustomNameCustom
        {
            get { return CustomNameTxt.Text; }
            set { CustomNameTxt.Text = value; }
        }

        public string CustomMailCustom
        {
            get { return CustomMailTxt.Text; }
            set { CustomMailTxt.Text = value; }
        }

        public string CustomPhoneCustom
        {
            get { return CustomPhoneTxt.Text; }
            set { CustomPhoneTxt.Text = value; }
        }

        public string CustomNoteCustom
        {
            get { return CustomNoteTxt.Text; }
            set { CustomNoteTxt.Text = value; }
        }

    }
}
