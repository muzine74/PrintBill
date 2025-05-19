using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBridge.Entity;
using DBConnection.Entity;
using Microsoft.IdentityModel.Tokens;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SendBillWF
{
    public partial class CompagnyUsCtr : UserControl
    {
        public CompagnyUsCtr()
        {
            InitializeComponent();
            compagnieProviderCBX.SelectedIndexChanged += compagnieProviderCBX_SelectedIndexChanged;
        }

        public string compagnyNameCompagny
        {
            get { return compagnyNameTxt.Text; }
            set { compagnyNameTxt.Text = value; }
        }

        public string CompagnieCodeCompagny
        {
            get { return CompagnieCodeTxt.Text; }
            set { CompagnieCodeTxt.Text = value; }
        }

        public string compagnieNoteCompagny
        {
            get { return compagnieNoteTxt.Text; }
            set { compagnieNoteTxt.Text = value; }
        }

        public bool compagnieStatusCompagny
        {
            get { return CompagnyStatusTxt.Checked; }
            set { CompagnyStatusTxt.Checked = value; }
        }

        //public string compagnyProvider
        //{
        //    get { return compagnyProviderCBX.Text; }
        //    set { compagnyNameTxt.Text = value; }
        //}


        public event EventHandler<CompagniePoco> OnCompanySelected;

        private void compagnieProviderCBX_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = compagnieProviderCBX.SelectedItem as CompagniePoco;
            OnCompanySelected?.Invoke(this, selected);
        }

        public void FillProviderCBX(IEnumerable<CompagniePoco> items, CompagniePoco cpy)
        {
            compagnieProviderCBX.DataSource = items.ToList();
            compagnieProviderCBX.DisplayMember = "CompagnieName"; // Afficher le nom
            compagnieProviderCBX.ValueMember = "CompagnieID"; // Valeur de l'item

            if (cpy != null)
                compagnieProviderCBX.SelectedItem = cpy;
            else
                compagnieProviderCBX.SelectedIndex = -1;
        }


    }
}


