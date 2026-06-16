
using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using SendBillWF.BL;
using SendBillWF.Compagny.CompagnyBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SendBillWF
{
    public partial class AddCompagnyusctr : UserControl
    {

        CompagniePoco compagniePoco;
        CompagniManipulation compagniManipulation;
        VisitFrequencyPricingUctr visittFreqPriceUsctr;


        public AddCompagnyusctr()
        {
            InitializeComponent();
            compagniePoco = new CompagniePoco();
            compagniManipulation = new CompagniManipulation();
            visittFreqPriceUsctr = new VisitFrequencyPricingUctr();




            LoadCompanies();
            compagnyUsCtr1.OnCompanySelected += CompagnyUsCtr1_CompanySelected;

        }

        //proprietè compagnie
        public string compagnyNameSaisie
        {
            get { return compagnyUsCtr1.compagnyNameCompagny; }
            set { compagnyUsCtr1.compagnyNameCompagny = value; }
        }

        public string CompagnieCodeSaisie
        {
            get { return compagnyUsCtr1.CompagnieCodeCompagny; }
            set { compagnyUsCtr1.CompagnieCodeCompagny = value; }
        }

        public string compagnieNoteSaisie
        {
            get { return compagnyUsCtr1.compagnieNoteCompagny; }
            set { compagnyUsCtr1.compagnieNoteCompagny = value; }
        }
        public string compagnyProviderSelected
        {
            get { return compagnyUsCtr1.compagnyProviderselected; }
            set { compagnyUsCtr1.compagnyProviderselected = value; }
        }
        public bool compagnieStatusSaisie
        {
            get { return compagnyUsCtr1.compagnieStatusCompagny; }
            set { compagnyUsCtr1.compagnieStatusCompagny = value; }
        }



        ////proprietè Addresse
        public string civicNumberSaisie
        {
            get { return addressUsctr1.civicNumberAdress; }
            set { addressUsctr1.civicNumberAdress = value; }
        }

        public string suiteSaisie
        {
            get { return addressUsctr1.suiteAdress; }
            set { addressUsctr1.civicNumberAdress = value; }
        }

        public string CitySaisie
        {
            get { return addressUsctr1.cityAdress; }
            set { addressUsctr1.cityAdress = value; }
        }

        public string StateSaisie
        {
            get { return addressUsctr1.stateAdress; }
            set { addressUsctr1.stateAdress = value; }
        }

        public string CountrySaisie
        {
            get { return addressUsctr1.countryAdress; }
            set { addressUsctr1.countryAdress = value; }
        }

        public string ZipCodeSaisie
        {
            get { return addressUsctr1.zipCodeAdress; }
            set { addressUsctr1.zipCodeAdress = value; }
        }

        public string NoteSaisie
        {
            get { return addressUsctr1.noteAdress; }
            set { addressUsctr1.noteAdress = value; }
        }


        public string CustomNameSaisie
        {
            get { return customUsCtr1.CustomNameCustom; }
            set { customUsCtr1.CustomNameCustom = value; }
        }

        public string CustomMailSaisie
        {
            get { return customUsCtr1.CustomMailCustom; }
            set { customUsCtr1.CustomMailCustom = value; }
        }

        public string CustomPhoneSaisie
        {
            get { return customUsCtr1.CustomPhoneCustom; }
            set { customUsCtr1.CustomPhoneCustom = value; }
        }

        public string CustomNoteSaisie
        {
            get { return customUsCtr1.CustomNoteCustom; }
            set { customUsCtr1.CustomNoteCustom = value; }
        }

        // Tax

        public string compagnyTPSSaisi
        {
            get { return taxUsCtr1.compagnyTPSCompagny; }
            set { taxUsCtr1.compagnyTPSCompagny = value; }
        }

        public string compagnyTVQCompagny
        {
            get { return taxUsCtr1.compagnyTVQCompagny; }
            set { taxUsCtr1.compagnyTVQCompagny = value; }
        }



        private bool CheckValidField()
        {
            if (string.IsNullOrWhiteSpace(compagnyNameSaisie) || string.IsNullOrWhiteSpace(CompagnieCodeSaisie) || string.IsNullOrWhiteSpace(compagnieNoteSaisie)
              || string.IsNullOrWhiteSpace(civicNumberSaisie) || string.IsNullOrWhiteSpace(CitySaisie) || string.IsNullOrWhiteSpace(StateSaisie) || string.IsNullOrWhiteSpace(CountrySaisie)
              || string.IsNullOrWhiteSpace(ZipCodeSaisie))
            {
                return false;

            }



            return true;

        }

        private void AddCompagny_Click(object sender, EventArgs e)
        {
            Guid CompagnyGuid = Guid.NewGuid();
            Guid AdressGuid = Guid.NewGuid();
            Guid ClientGuid = Guid.NewGuid();

            List<VisittFreqPri> visittFreqPri = new List<VisittFreqPri>();


            switch (visitFrequencyPricingUctr1.WorkFrequencySelectedItem)
            {
                case "visite":
                case "Hebdomadaire":
                    visittFreqPri = visitFrequencyPricingUctr1.GetWeeklyVisitFrequencyPricing();
                    break;

                case "Bi-hebdomadaire":
                case "Bi-mensuel":
                    visittFreqPri = visitFrequencyPricingUctr1.GetBi_WeeklyVisitFrequencyPricing();
                    break;

                case "Mensuel":

                    break;

                default:
                    break;
            }




            if (CheckValidField() == true)
            {
                foreach (var item in visittFreqPri)
                {
                    compagniePoco._companyPricingCalendar.Add(new CompanyPricingCalendar
                    {
                        CompanyPricingCalendarId = Guid.NewGuid(),
                        CompanyId = CompagnyGuid,
                        Days = item.Days,
                        CompanyBenefitPrice = item.CompanyBenefitPrice,
                        EmployeePayment = item.EmployeePayment,
                        ApplicatedDate = DateTime.Now,
                        IsActive = true
                    });
                }




                compagniePoco.CompagnieID = CompagnyGuid;
                compagniePoco.AddressID = AdressGuid;
                compagniePoco.ContactID = ClientGuid;

                compagniePoco.CompagnieName = compagnyNameSaisie;
                compagniePoco.CompagnieCode = CompagnieCodeSaisie;
                compagniePoco.CompagnieStatus = compagnieStatusSaisie;

                compagniePoco.CompagnieCivicNumber = civicNumberSaisie;
                compagniePoco.Compagniecity = CitySaisie;
                compagniePoco.CompagnieState = StateSaisie;
                compagniePoco.Compagniecountry = CountrySaisie;
                compagniePoco.CompagnieZipCode = ZipCodeSaisie;
                compagniePoco.ContactName = CustomNameSaisie;
                compagniePoco.ContactMail = CustomMailSaisie;
                compagniePoco.ContactPhones = CustomPhoneSaisie;
                compagniePoco.CompagnieProvider = compagnyProviderSelected;
                compagniePoco.TPSNumber = compagnyTPSSaisi;
                compagniePoco.TVQNumber = compagnyTVQCompagny;


                compagniePoco.WorkFrequency = visitFrequencyPricingUctr1.WorkFrequencySelectedItem;
                compagniePoco.PaymentFrequency = visitFrequencyPricingUctr1.PaimentFrequencySelectedItem;

                compagniManipulation.SaveCompagnyInfo(compagniePoco);
            }
            else
            {
                //MessageBox.Show("il faut remplir tous les champs en etoile");
            }

        }


        private void LoadCompanies()
        {
            var companies = compagniManipulation.getActiveCompagnies();
            compagnyUsCtr1.FillProviderCBX(companies, companies.FirstOrDefault());
        }

        private void CompagnyUsCtr1_CompanySelected(object sender, CompagniePoco selectedCompany)
        {
            if (selectedCompany != null)
            {
                MessageBox.Show($"Société sélectionnée : {selectedCompany.CompagnieName}");
            }
        }





    }
}
