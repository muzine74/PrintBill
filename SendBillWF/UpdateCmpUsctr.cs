using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;

namespace SendBillWF
{
    public partial class UpdateCmpUsctr : UserControl
    {
        CompagniManipulation compagniManipulation = new CompagniManipulation();
        List<CompagniePoco> compagniesByName;
        CompagniePoco compagniePoco;

        public UpdateCmpUsctr()
        {
            InitializeComponent();
            compagniesByName = new List<CompagniePoco>();
            compagniePoco = new CompagniePoco();
            CmpUsCtr.OnCompanySelected += CompagnyUsCtr1_CompanySelected;
        }

        //proprietè compagnie
        public string compagnyNameSaisie
        {
            get { return CmpUsCtr.compagnyNameCompagny; }
            set { CmpUsCtr.compagnyNameCompagny = value; }
        }

        public string CompagnieCodeSaisie
        {
            get { return CmpUsCtr.CompagnieCodeCompagny; }
            set { CmpUsCtr.CompagnieCodeCompagny = value; }
        }

        public string compagnieNoteSaisie
        {
            get { return CmpUsCtr.compagnieNoteCompagny; }
            set { CmpUsCtr.compagnieNoteCompagny = value; }
        }
        public bool compagnieStatusSaisie
        {
            get { return CmpUsCtr.compagnieStatusCompagny; }
            set { CmpUsCtr.compagnieStatusCompagny = value; }
        }

        ////proprietè Addresse
        public string civicNumberSaisie
        {
            get { return addUsctr.civicNumberAdress; }
            set { addUsctr.civicNumberAdress = value; }
        }

        public string suiteSaisie
        {
            get { return addUsctr.suiteAdress; }
            set { addUsctr.civicNumberAdress = value; }
        }

        public string CitySaisie
        {
            get { return addUsctr.cityAdress; }
            set { addUsctr.cityAdress = value; }
        }

        public string StateSaisie
        {
            get { return addUsctr.stateAdress; }
            set { addUsctr.stateAdress = value; }
        }

        public string CountrySaisie
        {
            get { return addUsctr.countryAdress; }
            set { addUsctr.countryAdress = value; }
        }

        public string ZipCodeSaisie
        {
            get { return addUsctr.zipCodeAdress; }
            set { addUsctr.zipCodeAdress = value; }
        }

        public string NoteSaisie
        {
            get { return addUsctr.noteAdress; }
            set { addUsctr.noteAdress = value; }
        }


        public string CustomNameSaisie
        {
            get { return custUsCtr.CustomNameCustom; }
            set { custUsCtr.CustomNameCustom = value; }
        }

        public string CustomMailSaisie
        {
            get { return custUsCtr.CustomMailCustom; }
            set { custUsCtr.CustomMailCustom = value; }
        }

        public string CustomPhoneSaisie
        {
            get { return custUsCtr.CustomPhoneCustom; }
            set { custUsCtr.CustomPhoneCustom = value; }
        }

        public string CustomNoteSaisie
        {
            get { return custUsCtr.CustomNoteCustom; }
            set { custUsCtr.CustomNoteCustom = value; }
        }


        private void SearchbtnUp_Click(object sender, EventArgs e)
        {
            compagniesGrid.AutoGenerateColumns = false;

            // Supprimer toutes les colonnes existantes (au cas où)
            compagniesGrid.Columns.Clear();

            DataGridViewTextBoxColumn CompagnieID = new DataGridViewTextBoxColumn();
            CompagnieID.HeaderText = "CompagnieID"; // Texte d'en-tête de la colonne
            CompagnieID.DataPropertyName = "CompagnieID"; // Propriété de la source de données à lier
            CompagnieID.Visible = false;
            compagniesGrid.Columns.Add(CompagnieID);


            DataGridViewTextBoxColumn AddressID = new DataGridViewTextBoxColumn();
            AddressID.HeaderText = "AddressID"; // Texte d'en-tête de la colonne
            AddressID.DataPropertyName = "AddressID"; // Propriété de la source de données à lier
            AddressID.Visible = false;
            compagniesGrid.Columns.Add(AddressID);


            DataGridViewTextBoxColumn ContactID = new DataGridViewTextBoxColumn();
            ContactID.HeaderText = "ContactID"; // Texte d'en-tête de la colonne
            ContactID.DataPropertyName = "ContactID"; // Propriété de la source de données à lier
            ContactID.Visible = false;
            compagniesGrid.Columns.Add(ContactID);

            // Colonne pour la propriété "Nom"
            DataGridViewTextBoxColumn CompagnieName = new DataGridViewTextBoxColumn();
            CompagnieName.HeaderText = "Compagnie Name"; // Texte d'en-tête de la colonne
            CompagnieName.DataPropertyName = "CompagnieName"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(CompagnieName);

            DataGridViewTextBoxColumn CompagnieCode = new DataGridViewTextBoxColumn();
            CompagnieCode.HeaderText = "CompagnieCode"; // Texte d'en-tête de la colonne
            CompagnieCode.DataPropertyName = "CompagnieCode"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(CompagnieCode);

            DataGridViewTextBoxColumn CompagnieStatus = new DataGridViewTextBoxColumn();
            CompagnieStatus.HeaderText = "Status"; // Texte d'en-tête de la colonne
            CompagnieStatus.DataPropertyName = "CompagnieStatus"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(CompagnieStatus);

            DataGridViewTextBoxColumn CompagnieState = new DataGridViewTextBoxColumn();
            CompagnieState.HeaderText = "State"; // Texte d'en-tête de la colonne
            CompagnieState.DataPropertyName = "CompagnieState"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(CompagnieState);

            DataGridViewTextBoxColumn Compagniecity = new DataGridViewTextBoxColumn();
            Compagniecity.HeaderText = "city"; // Texte d'en-tête de la colonne
            Compagniecity.DataPropertyName = "Compagniecity"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(Compagniecity);

            DataGridViewTextBoxColumn CompagnieZipCode = new DataGridViewTextBoxColumn();
            CompagnieZipCode.HeaderText = "Zip Code"; // Texte d'en-tête de la colonne
            CompagnieZipCode.DataPropertyName = "CompagnieZipCode"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(CompagnieZipCode);

            DataGridViewTextBoxColumn CompagnieSuite = new DataGridViewTextBoxColumn();
            CompagnieSuite.HeaderText = "Suite"; // Texte d'en-tête de la colonne
            CompagnieSuite.DataPropertyName = "CompagnieSuite"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(CompagnieSuite);

            DataGridViewTextBoxColumn CompagnieCivicNumber = new DataGridViewTextBoxColumn();
            CompagnieCivicNumber.HeaderText = "Civic Number"; // Texte d'en-tête de la colonne
            CompagnieCivicNumber.DataPropertyName = "CompagnieCivicNumber"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(CompagnieCivicNumber);

            DataGridViewTextBoxColumn ContactName = new DataGridViewTextBoxColumn();
            ContactName.HeaderText = "Contact"; // Texte d'en-tête de la colonne
            ContactName.DataPropertyName = "ContactName"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(ContactName);

            DataGridViewTextBoxColumn ContactMail = new DataGridViewTextBoxColumn();
            ContactMail.HeaderText = "Mail"; // Texte d'en-tête de la colonne
            ContactMail.DataPropertyName = "ContactMail"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(ContactMail);

            DataGridViewTextBoxColumn ContactPhones = new DataGridViewTextBoxColumn();
            ContactPhones.HeaderText = "Phones"; // Texte d'en-tête de la colonne
            ContactPhones.DataPropertyName = "ContactPhones"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(ContactPhones);

            DataGridViewTextBoxColumn CompagnyProvider = new DataGridViewTextBoxColumn();
            CompagnyProvider.HeaderText = "Provider"; // Texte d'en-tête de la colonne
            CompagnyProvider.DataPropertyName = "CompagnyProvider"; // Propriété de la source de données à lier
            compagniesGrid.Columns.Add(CompagnyProvider);

            SearchUp();

        }

        private void SearchUp()
        {
            compagniesByName = compagniManipulation.GetCompagnyByName(SearchTxtUp.Text);

            // Désactiver la génération automatique des colonnes



            // Lier les données au DataGridView
            compagniesGrid.DataSource = compagniesByName;
            compagniesGrid.Refresh();
        }

        private void compagniesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string st = string.Empty;

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell selectedCell = compagniesGrid.Rows[e.RowIndex].Cells[0];
                // st = $"Ligne : {e.RowIndex}, Colonne : {e.ColumnIndex}, Valeur : {selectedCell.Value}";

                compagniePoco.CompagnieID = (Guid)compagniesGrid.Rows[e.RowIndex].Cells[0].Value;
                compagniePoco.AddressID = (Guid)compagniesGrid.Rows[e.RowIndex].Cells[1].Value;
                compagniePoco.ContactID = (Guid)compagniesGrid.Rows[e.RowIndex].Cells[2].Value;

                compagnyNameSaisie = compagniePoco.CompagnieName = (string)compagniesGrid.Rows[e.RowIndex].Cells[3].Value;
                CompagnieCodeSaisie = compagniePoco.CompagnieCode = (string)compagniesGrid.Rows[e.RowIndex].Cells[4].Value;
                compagniePoco.CompagnieStatus = (string)compagniesGrid.Rows[e.RowIndex].Cells[5].Value;
                if (compagniePoco.CompagnieStatus == "Active")
                {
                    compagnieStatusSaisie = true;
                }
                else
                {
                    compagnieStatusSaisie = false;
                }


                StateSaisie = compagniePoco.CompagnieState = (string)compagniesGrid.Rows[e.RowIndex].Cells[6].Value;
                CitySaisie = compagniePoco.Compagniecity = (string)compagniesGrid.Rows[e.RowIndex].Cells[7].Value;
                ZipCodeSaisie = compagniePoco.CompagnieZipCode = (string)compagniesGrid.Rows[e.RowIndex].Cells[8].Value;
                suiteSaisie = compagniePoco.CompagnieSuite = (string)compagniesGrid.Rows[e.RowIndex].Cells[9].Value;
                civicNumberSaisie = compagniePoco.CompagnieCivicNumber = (string)compagniesGrid.Rows[e.RowIndex].Cells[10].Value;

                CustomNameSaisie = compagniePoco.ContactName = (string)compagniesGrid.Rows[e.RowIndex].Cells[11].Value;
                CustomMailSaisie = compagniePoco.ContactMail = (string)compagniesGrid.Rows[e.RowIndex].Cells[12].Value;
                CustomPhoneSaisie = compagniePoco.ContactPhones = (string)compagniesGrid.Rows[e.RowIndex].Cells[13].Value;
                compagniePoco.CompagnieProvider = (string)compagniesGrid.Rows[e.RowIndex].Cells[14].Value;
                //CmpUsCtr.FillProviderCBX(string)compagniesGrid.Rows[e.RowIndex].Cells[14].Value);


                LoadCompanies();

            }
        }

        private void UpddateCmp_Click(object sender, EventArgs e)
        {
            bool isChanged = false;

            if (compagnyNameSaisie != compagniePoco.CompagnieName)
            {
                compagniePoco.CompagnieName = compagnyNameSaisie;
                isChanged = true;
            }

            if (CompagnieCodeSaisie != compagniePoco.CompagnieCode)
            {
                compagniePoco.CompagnieCode = CompagnieCodeSaisie;
                isChanged = true;

            }

            if (StateSaisie != compagniePoco.CompagnieState)
            {
                compagniePoco.CompagnieState = StateSaisie;
                isChanged = true;
            }

            if (CitySaisie != compagniePoco.Compagniecity)
            {
                compagniePoco.Compagniecity = CitySaisie;
                isChanged = true;
            }

            if (ZipCodeSaisie != compagniePoco.CompagnieZipCode)
            {
                compagniePoco.CompagnieZipCode = ZipCodeSaisie;
                isChanged = true;
            }

            if (suiteSaisie != compagniePoco.CompagnieSuite)
            {
                compagniePoco.CompagnieSuite = suiteSaisie;
                isChanged = true;
            }

            if (civicNumberSaisie != compagniePoco.CompagnieCivicNumber)
            {
                compagniePoco.CompagnieCivicNumber = civicNumberSaisie;
                isChanged = true;
            }

            if (CustomNameSaisie != compagniePoco.ContactName)
            {
                compagniePoco.ContactName = CustomNameSaisie;
                isChanged = true;
            }

            if (CustomMailSaisie != compagniePoco.ContactMail)
            {
                compagniePoco.ContactMail = CustomMailSaisie;
                isChanged = true;
            }

            if (CustomPhoneSaisie != compagniePoco.ContactPhones)
            {
                compagniePoco.ContactPhones = CustomPhoneSaisie;
                isChanged = true;
            }


            //                    compagniePoco.CompagnieStatus = "Active";
            //    compagnieStatusSaisie = true;


            if (CustomPhoneSaisie != compagniePoco.ContactPhones)
            {
                compagniePoco.ContactPhones = CustomPhoneSaisie;
                isChanged = true;
            }

            if (isChanged)
            {
                //appeler la fonction pour la mise a jours
                compagniManipulation.UpdateCompagnyInfo(compagniePoco);
                SearchUp();

            }
            else
            {
                MessageBox.Show("Aucun champs n'a ete changè");
            }
        }


        private void LoadCompanies()
        {
            var companies = compagniManipulation.getActiveCompagnies();


            //get provider compagny info

            CompagniePoco providerCPY = companies.FirstOrDefault(p => p.CompagnieCode == compagniePoco.CompagnieProvider);

            CmpUsCtr.FillProviderCBX(companies, providerCPY);
        }

        private void CompagnyUsCtr1_CompanySelected(object sender, CompagniePoco selectedCompany)
        {
            if (selectedCompany != null)
            {
                var companies = compagniManipulation.getActiveCompagnies();
                CmpUsCtr.FillProviderCBX(companies, selectedCompany);
                // MessageBox.Show($"Société sélectionnée : {selectedCompany.CompagnieName}");
            }
        }
    }
}
