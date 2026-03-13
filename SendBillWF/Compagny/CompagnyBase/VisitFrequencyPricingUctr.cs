using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using Google.Apis.Util;
using SendBillWF.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace SendBillWF.Compagny.CompagnyBase
{
    public partial class VisitFrequencyPricingUctr : UserControl
    {
        WorkManipulation workManipulation;
        List<CompagniePoco> compagniePoco;
        public VisitFrequencyPricingUctr()
        {
            InitializeComponent();
            workManipulation = new WorkManipulation();
            compagniePoco = new List<CompagniePoco>();

            InitializeFrequency();


        }

        private void InitializeFrequency()
        {
            workManipulation.GetAllWorkTypes();
            var frequencyList = workManipulation.WorkTypePoco.WorkTypesLst;

            WorkFrequency.DataSource = frequencyList.Select(w => w.Name).Distinct().ToList();
            PaimentFrequency.DataSource = frequencyList.Select(w => w.Name).Distinct().ToList();

            WorkFrequency.SelectedIndex = 0;
        }

        public string WorkFrequencySelectedItem
        {
            get { return WorkFrequency.SelectedItem.ToString(); }
            set { WorkFrequency.SelectedItem = value; }
        }


        public string PaimentFrequencySelectedItem
        {
            get { return PaimentFrequency.SelectedItem.ToString(); }
            set { PaimentFrequency.SelectedItem = value; }
        }



        private void WorkFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableAllGroupeBox();
            //visittFreqPriLst

            VisittFreqPri visittFreqPri = new VisittFreqPri();



            visittFreqPri.WorkFrequencySelected = WorkFrequencySelectedItem;
            visittFreqPri.PaimentFrequencySelected = PaimentFrequencySelectedItem;


            switch (visittFreqPri.WorkFrequencySelected.ToString())
            {
                case "visite":
                case "Hebdomadaire":
                    Weekly.Visible = true;
                    break;

                case "Bi-hebdomadaire":
                case "Bi-mensuel":
                    BiWeekly.Visible = true;
                    break;

                case "Mensuel":
                    Monthly.Visible = true;
                    break;

                default:
                    break;
            }
        }

        void DisableAllGroupeBox()
        {
            BiWeekly.Visible = false;
            Weekly.Visible = false;
            Monthly.Visible = false;

        }

        private void VisitFrequencyPricingUctr_Load(object sender, EventArgs e)
        {

        }

        public void PaimentFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public List<VisittFreqPri> GetWeeklyVisitFrequencyPricing()
        {
            //List<VisittFreqPri> visittFreqPriLst = new List<VisittFreqPri>();

            var daysAndValues = new[]
                    {
            new { Day="Weekly_Monday",DaysStatus = Weekly_Monday.Checked, Company = WeeklyCpmMonday.Text, Employee = WeeklyEmplMonday.Text },
            new { Day="Weekly_Tuesday",DaysStatus = Weekly_Tuesday.Checked, Company = WeeklyCpmTuesday.Text, Employee = WeeklyEmplTuesday.Text },
            new { Day="Weekly_Wednesday",DaysStatus = Weekly_Wednesday.Checked, Company = WeeklyCpmWednesday.Text, Employee = WeeklyEmplWednesday.Text },
            new { Day="Weekly_Thursday",DaysStatus = Weekly_Thursday.Checked, Company = WeeklyCpmThursday.Text, Employee = WeeklyEmplThursday.Text },
            new { Day="Weekly_Friday",DaysStatus = Weekly_Friday.Checked, Company = WeeklyCpmFriday.Text, Employee = WeeklyEmplFriday.Text },
            new { Day="Weekly_Saturday",DaysStatus = Weekly_Saturday.Checked, Company = WeeklyCpmSaturday.Text, Employee = WeeklyEmplSaturday.Text },
        };


            return daysAndValues.Select(d => new VisittFreqPri
            {
                WorkFrequencySelected = WorkFrequencySelectedItem,
                PaimentFrequencySelected = PaimentFrequencySelectedItem,
                Days = d.Day,
                DaysStatus = d.DaysStatus,
                CopagnyBenifictPrice = SafeParseDecimal(d.Company),
                Emplyeepaiment = SafeParseDecimal(d.Employee)
            }).ToList();
            //return visittFreqPriLst;
        }

        public List<VisittFreqPri> GetBi_WeeklyVisitFrequencyPricing()
        {
            //List<VisittFreqPri> visittFreqPriLst;


            var daysAndValues = new[]
            {
                  new { Day = "BiWeek1_Monday", DaysStatus = BiWeek1_Monday.Checked, Company = BiWeek1PriceCpmMonday.Text, Employee =   BiWeek1PriceEmpMonday.Text },
                  new { Day = "BiWeek1_Tuesday",DaysStatus = BiWeek1_Tuesday.Checked, Company = BiWeek1PriceCpmTuesday.Text, Employee = BiWeek1PriceEmpTuesday.Text },
                  new { Day = "BiWeek1_Wednesday", DaysStatus = BiWeek1_Wednesday.Checked, Company = BiWeek1PriceCpmWednesday.Text, Employee = BiWeek1PriceEmpWednesday.Text },
                  new { Day = "BiWeek1_Thursday", DaysStatus = BiWeek1_Thursday.Checked, Company = BiWeek1PriceCpmThursday.Text, Employee = BiWeek1PriceEmpThursday.Text },
                  new { Day = "BiWeek1_Friday", DaysStatus = BiWeek1_Friday.Checked, Company = BiWeek1PriceCpmFriday.Text, Employee = BiWeek1PriceEmpFriday.Text },
                  new { Day = "BiWeek1_Saturday", DaysStatus = BiWeek1_Saturday.Checked, Company = BiWeek1PriceCpmSaturday.Text, Employee = BiWeek1PriceEmpSaturday.Text },
                  new { Day = "BiWeek1_Sunday", DaysStatus = BiWeek1_Sunday.Checked, Company = BiWeek1PriceCpmSaturday.Text, Employee = BiWeek1PriceEmpSunday.Text },

                  //Week2
                  new { Day = "BiWeek2_Monday", DaysStatus = BiWeek2_Monday.Checked, Company = BiWeek2PriceCpmMonday.Text, Employee = BiWeek2PriceEmpMonday.Text },
                  new { Day = "BiWeek2_Tuesday", DaysStatus = BiWeek2_Tuesday.Checked, Company = BiWeek2PriceCpmTuesday.Text, Employee = BiWeek2PriceEmpTuesday.Text },
                  new { Day = "BiWeek2_Wednesday", DaysStatus = BiWeek2_Wednesday.Checked, Company = BiWeek2PriceCpmWednesday.Text, Employee = BiWeek2PriceEmpWednesday.Text },
                  new { Day = "BiWeek2_Thursday", DaysStatus = BiWeek2_Thursday.Checked, Company = BiWeek2PriceCpmThursday.Text, Employee = BiWeek2PriceEmpThursday.Text },
                  new { Day = "BiWeek2_Friday", DaysStatus = BiWeek2_Friday.Checked, Company = BiWeek2PriceCpmFriday.Text, Employee = BiWeek2PriceEmpFriday.Text },
                  new { Day = "BiWeek2_Saturday", DaysStatus = BiWeek2_Saturday.Checked, Company = BiWeek2PriceCpmSaturday.Text, Employee = BiWeek2PriceEmpSaturday.Text },
                  new { Day = "BiWeek2_Sunday",DaysStatus = BiWeek2_Sunday.Checked, Company = BiWeek2PriceCpmSaturday.Text, Employee = BiWeek2PriceEmpSaturday.Text }
            };


            return daysAndValues.Select(d => new VisittFreqPri
            {
                WorkFrequencySelected = WorkFrequencySelectedItem,
                PaimentFrequencySelected = PaimentFrequencySelectedItem,
                Days = d.Day,
                DaysStatus = d.DaysStatus,
                CopagnyBenifictPrice = SafeParseDecimal(d.Company),
                Emplyeepaiment = SafeParseDecimal(d.Employee)
            }).ToList();

        }

        private void GetMonthlyVisitFrequencyPricing()
        {
            throw new NotImplementedException();
        }

        private decimal SafeParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0m;

            // Essayer avec la culture actuelle
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result))
                return result;

            // Essayer avec la culture invariante
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            // Essayer de remplacer la virgule par un point
            var normalizedValue = value.Replace(",", ".");
            if (decimal.TryParse(normalizedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            return 0m;
        }


        public void FillVisitFrequencyUct(CompagniePoco compagniePoco)
        {
            WorkFrequency.SelectedItem = compagniePoco.WorkFrequency;
            PaimentFrequency.SelectedItem = compagniePoco.PaymentFrequency;

            switch (compagniePoco.WorkFrequency)
            {
                case "visite":
                case "Hebdomadaire":
                    FillWeeklyVisitFrequencyPricing(compagniePoco);
                    break;

                case "Bi-hebdomadaire":
                case "Bi-mensuel":
                    FillBiWeeklyVisitFrequencyPricing(compagniePoco);

                    break;

                case "Mensuel":
                    Monthly.Visible = true;
                    break;

                default:
                    break;
            }
        }


        private void FillWeeklyVisitFrequencyPricing(CompagniePoco compagniePoco)
        {

            Weekly_Monday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Monday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            Weekly_Tuesday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Tuesday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            Weekly_Wednesday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Wednesday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            Weekly_Thursday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Thursday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            Weekly_Friday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Friday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            Weekly_Saturday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Saturday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            Weekly_Sunday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Sunday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();



            WeeklyCpmMonday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Monday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            WeeklyCpmTuesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Tuesday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            WeeklyCpmWednesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Wednesday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            WeeklyCpmThursday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Thursday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            WeeklyCpmFriday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Friday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            WeeklyCpmSaturday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Saturday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            WeeklyCpmSunday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Sunday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();


            WeeklyEmplMonday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Monday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            WeeklyEmplTuesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Tuesday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            WeeklyEmplWednesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Wednesday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            WeeklyEmplThursday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Thursday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            WeeklyEmplFriday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Friday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            WeeklyEmplSaturday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Saturday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            WeeklyEmplSunday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "Weekly_Sunday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();


            Weekly.Visible = true;
        }


        //private void callTest(CompagniePoco compagniePoco)
        //{
        //    //List<string> frequence = new List<string> { "Weekly_", "BiWeek1_", "BiWeek2_", "WeeklyCpm", "BiWeek1PriceCpm", "BiWeek2PriceCpm", "WeeklyEmpl", "BiWeek1PriceEmp", "BiWeek2PriceEmp" };

        //    //test("Weekly_", compagniePoco);
        //    //test("WeeklyCpm", compagniePoco);
        //    //test("WeeklyEmpl", compagniePoco);
        //}


        //private void test(string weworkfrequeneek, CompagniePoco compagniePoco)
        //{

        //    //weworkfrequeneek = Weekly_,WeeklyCpm,WeeklyEmpl
        //    List<string> days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        //    foreach (var day in days)
        //    {
        //        string checkBoxName = weworkfrequeneek + day;
        //        var checkBox = this.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;
        //        if (checkBox != null)
        //        {
        //            bool isChecked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == checkBoxName && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
        //            checkBox.Checked = isChecked;
        //        }
        //    }
        //}

        private void FillBiWeeklyVisitFrequencyPricing(CompagniePoco compagniePoco)
        {

            //week 1

            BiWeek1_Monday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Monday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek1_Tuesday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Tuesday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek1_Wednesday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Wednesday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek1_Thursday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Thursday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek1_Friday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Friday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek1_Saturday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Saturday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek1_Sunday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Sunday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();



            BiWeek1PriceCpmMonday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Monday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek1PriceCpmTuesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Tuesday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek1PriceCpmWednesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Wednesday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek1PriceCpmThursday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Thursday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek1PriceCpmFriday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Friday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek1PriceCpmSaturday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Saturday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek1PriceCpmSunday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Sunday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();


            BiWeek1PriceEmpMonday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Monday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek1PriceEmpTuesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Tuesday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek1PriceEmpWednesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Wednesday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek1PriceEmpThursday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Thursday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek1PriceEmpFriday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Friday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek1PriceEmpSaturday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Saturday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek1PriceEmpSunday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek1_Sunday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();



            //Week1 2
            BiWeek2_Monday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Monday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek2_Tuesday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Tuesday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek2_Wednesday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Wednesday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek2_Thursday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Thursday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek2_Friday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Friday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek2_Saturday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Saturday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();
            BiWeek2_Sunday.Checked = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Sunday" && C.IsActive == true)).Select(CP => CP.DaysStatus).FirstOrDefault();



            BiWeek2PriceCpmMonday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Monday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek2PriceCpmTuesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Tuesday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek2PriceCpmWednesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Wednesday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek2PriceCpmThursday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Thursday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek2PriceCpmFriday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Friday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek2PriceCpmSaturday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Saturday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();
            BiWeek2PriceCpmSunday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Sunday" && C.IsActive == true)).Select(CP => CP.CopagnyBenifictPrice).FirstOrDefault().ToString();


            BiWeek2PriceEmpMonday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Monday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek2PriceEmpTuesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Monday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek2PriceEmpWednesday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Monday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek2PriceEmpThursday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Monday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek2PriceEmpFriday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Monday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek2PriceEmpSaturday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Monday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();
            BiWeek2PriceEmpSunday.Text = (compagniePoco._companyPricingCalendar.Where(C => C.Days == "BiWeek2_Sunday" && C.IsActive == true)).Select(CP => CP.Emplyeepaiment).FirstOrDefault().ToString();


            BiWeekly.Visible = true;
        }


        // mettre a jours la liste des visittFreqPriLst avant d'enregistrer
        public List<CompanyPricingCalendar> GetUpdatedVisitFrequencyPricingList(CompagniePoco compagniePoco)
        {
            compagniePoco._companyPricingCalendar.Clear();
            List<CompanyPricingCalendar> companyPricingCalendars = new List<CompanyPricingCalendar>();
            switch (WorkFrequencySelectedItem)
            {
                case "visite":
                case "Hebdomadaire":
                    companyPricingCalendars = GetWeeklyVisitFrequencyPricingListAfterUpdate(compagniePoco);
                    break;
                case "Bi-hebdomadaire":
                case "Bi-mensuel":
                    companyPricingCalendars =  GetBi_WeeklyVisitFrequencyPricingListAfterUpdate(compagniePoco);
                    break;
                case "Mensuel":
                    //Monthly.Visible = true;
                    break;
                default:
                    break;
            }

            return companyPricingCalendars;
        }

        public List<CompanyPricingCalendar> GetWeeklyVisitFrequencyPricingListAfterUpdate(CompagniePoco compagniePoco)
        {
            List<VisittFreqPri> temp = GetWeeklyVisitFrequencyPricing();

            foreach (var item in temp)
            {
                compagniePoco._companyPricingCalendar.Add(new CompanyPricingCalendar
                {
                    Days = item.Days,
                    DaysStatus = item.DaysStatus,
                    CopagnyBenifictPrice = item.CopagnyBenifictPrice,
                    Emplyeepaiment = item.Emplyeepaiment,
                    ApplicatedDate = DateTime.Now,
                    IsActive = true 
                });
            }

            return compagniePoco._companyPricingCalendar;
        }

        public List<CompanyPricingCalendar> GetBi_WeeklyVisitFrequencyPricingListAfterUpdate(CompagniePoco compagniePoco)
        {
            List<VisittFreqPri> temp = GetBi_WeeklyVisitFrequencyPricing();

            foreach (var item in temp)
            {
                compagniePoco._companyPricingCalendar.Add(new CompanyPricingCalendar
                {
                    Days = item.Days,
                    DaysStatus = item.DaysStatus,
                    CopagnyBenifictPrice = item.CopagnyBenifictPrice,
                    Emplyeepaiment = item.Emplyeepaiment,
                    ApplicatedDate = DateTime.Now,
                    IsActive = true
                });
            }
            return compagniePoco._companyPricingCalendar;
        }






        ////region Weekly

        //public bool Weekly_MondayUsctr
        //{
        //    get { return Weekly_Monday.Checked; }
        //    set { Weekly_Monday.Checked = value; }
        //}

        //public bool Weekly_TuesdayUsctr
        //{
        //    get { return Weekly_Tuesday.Checked; }
        //    set { Weekly_Tuesday.Checked = value; }
        //}

        //public bool Weekly_WednesdayUsctr
        //{
        //    get { return Weekly_Wednesday.Checked; }
        //    set { Weekly_Wednesday.Checked = value; }
        //}

        //public bool Weekly_ThursdayUsctr
        //{
        //    get { return Weekly_Thursday.Checked; }
        //    set { Weekly_Thursday.Checked = value; }
        //}

        //public bool Weekly_FridayUsctr
        //{
        //    get { return Weekly_Friday.Checked; }
        //    set { Weekly_Friday.Checked = value; }
        //}

        //public bool Weekly_SaturdayUsctr
        //{
        //    get { return Weekly_Saturday.Checked; }
        //    set { Weekly_Saturday.Checked = value; }
        //}

        //public bool Weekly_SundayUsctr
        //{
        //    get { return Weekly_Sunday.Checked; }
        //    set { Weekly_Sunday.Checked = value; }
        //}
        //public string WeeklyCpmMondayusctr
        //{
        //    get { return WeeklyCpmMonday.Text; }
        //    set { WeeklyCpmMonday.Text = value; }
        //}

        //public string WeeklyCpmTuesdayusctr
        //{
        //    get { return WeeklyCpmTuesday.Text; }
        //    set { WeeklyCpmTuesday.Text = value; }
        //}

        //public string WeeklyCpmWednesdayusctr
        //{
        //    get { return WeeklyCpmWednesday.Text; }
        //    set { WeeklyCpmWednesday.Text = value; }
        //}

        //public string WWeeklyCpmThursdayusctr
        //{
        //    get { return WeeklyCpmThursday.Text; }
        //    set { WeeklyCpmThursday.Text = value; }
        //}

        //public string WWeeklyCpmFridayusctr
        //{
        //    get { return WeeklyCpmFriday.Text; }
        //    set { WeeklyCpmFriday.Text = value; }
        //}

        //public string WeeklyCpmSaturdayusctr
        //{
        //    get { return WeeklyCpmSaturday.Text; }
        //    set { WeeklyCpmSaturday.Text = value; }

        //}

        //public string WeeklyCpmSundayusctr
        //{
        //    get { return WeeklyCpmSaturday.Text; }
        //    set { WeeklyCpmSaturday.Text = value; }

        //}

        //public string WeeklyEmplMondayusctr
        //{
        //    get { return WeeklyEmplMonday.Text; }

        //    set { WeeklyEmplMonday.Text = value; }
        //}

        //public string WeeklyEmplTuesdayusctr
        //{
        //    get { return WeeklyEmplTuesday.Text; }
        //    set { WeeklyEmplTuesday.Text = value; }
        //}

        //public string WeeklyEmplWednesdayusctr
        //{
        //    get { return WeeklyEmplWednesday.Text; }
        //    set { WeeklyEmplWednesday.Text = value; }
        //}


        //public string WWeeklyEmplThursdayusctr
        //{
        //    get { return WeeklyEmplThursday.Text; }
        //    set { WeeklyEmplThursday.Text = value; }
        //}

        //public string WWeeklyEmplFridayusctr
        //{
        //    get { return WeeklyEmplFriday.Text; }
        //    set { WeeklyEmplFriday.Text = value; }
        //}

        //public string WeeklyEmplSaturdayusctr
        //{
        //    get { return WeeklyEmplSaturday.Text; }
        //    set { WeeklyEmplSaturday.Text = value; }

        //}

        //public string WeeklyEmplSundayusctr
        //{
        //    get { return WeeklyEmplSunday.Text; }
        //    set { WeeklyEmplSunday.Text = value; }

        //}
        ////end Weekly


        ////Region BiWeekly;
        ////Region Week1;



        //public bool BiWeek1_MondayUsctr
        //{
        //    get { return BiWeek1_Monday.Checked; }
        //    set { BiWeek1_Monday.Checked = value; }
        //}

        //public bool BiWeek1_TuesdayUsctr
        //{
        //    get { return BiWeek1_Tuesday.Checked; }
        //    set { BiWeek1_Tuesday.Checked = value; }
        //}

        //public bool BiWeek1_WednesdayUsctr
        //{
        //    get { return BiWeek1_Wednesday.Checked; }
        //    set { BiWeek1_Wednesday.Checked = value; }
        //}

        //public bool BiWeek1_ThursdayUsctr
        //{
        //    get { return BiWeek1_Thursday.Checked; }
        //    set { BiWeek1_Thursday.Checked = value; }
        //}

        //public bool BiWeek1_FridayUsctr
        //{
        //    get { return BiWeek1_Friday.Checked; }
        //    set { BiWeek1_Friday.Checked = value; }
        //}

        //public bool BiWeek1_SaturdayUsctr
        //{
        //    get { return BiWeek1_Saturday.Checked; }
        //    set { BiWeek1_Saturday.Checked = value; }
        //}

        //public bool BiWeek1_SundayUsctr
        //{
        //    get { return BiWeek1_Sunday.Checked; }
        //    set { BiWeek1_Sunday.Checked = value; }
        //}

        //public string BiWeek1PriceCpmMondayusctr
        //{
        //    get { return BiWeek1PriceCpmMonday.Text; }
        //    set { BiWeek1PriceCpmMonday.Text = value; }
        //}

        //public string BiWeek1PriceCpmTuesdayusctr
        //{
        //    get { return BiWeek1PriceCpmTuesday.Text; }
        //    set { BiWeek1PriceCpmTuesday.Text = value; }
        //}

        //public string BiWeek1PriceCpmWednesdayusctr
        //{
        //    get { return BiWeek1PriceCpmWednesday.Text; }
        //    set { BiWeek1PriceCpmWednesday.Text = value; }
        //}

        //public string BiWeek1PriceCpmThursdaysctr
        //{
        //    get { return BiWeek1PriceCpmThursday.Text; }
        //    set { BiWeek1PriceCpmThursday.Text = value; }
        //}

        //public string BiWeek1PriceCpmFridayusctr
        //{
        //    get { return BiWeek1PriceCpmFriday.Text; }
        //    set { BiWeek1PriceCpmFriday.Text = value; }
        //}

        //public string BiWeek1PriceCpmSaturdayusctr
        //{
        //    get { return BiWeek1PriceCpmSaturday.Text; }
        //    set { BiWeek1PriceCpmSaturday.Text = value; }
        //}

        //public string BiWeek1PriceCpmSundayusctr
        //{
        //    get { return BiWeek1PriceCpmSaturday.Text; }
        //    set { BiWeek1PriceCpmSaturday.Text = value; }
        //}
        //public string BiWeek1PriceEmpMondayusctr
        //{
        //    get { return BiWeek1PriceEmpMonday.Text; }
        //    set { BiWeek1PriceEmpMonday.Text = value; }
        //}

        //public string BiWeek1PriceEmpTuesdayusctr
        //{
        //    get { return BiWeek1PriceEmpTuesday.Text; }
        //    set { BiWeek1PriceEmpTuesday.Text = value; }
        //}

        //public string BiWeek1PriceEmpWednesdayusctr
        //{
        //    get { return BiWeek1PriceEmpWednesday.Text; }
        //    set { BiWeek1PriceEmpWednesday.Text = value; }
        //}

        //public string BiWeek1PriceEmpThursdaysctr
        //{
        //    get { return BiWeek1PriceEmpThursday.Text; }
        //    set { BiWeek1PriceEmpThursday.Text = value; }
        //}

        //public string BiWeek1PriceEmpFridayusctr
        //{
        //    get { return BiWeek1PriceEmpFriday.Text; }
        //    set { BiWeek1PriceEmpFriday.Text = value; }
        //}

        //public string BiWeek1PriceEmpSaturdayusctr
        //{
        //    get { return BiWeek1PriceEmpSaturday.Text; }
        //    set { BiWeek1PriceEmpSaturday.Text = value; }
        //}

        //public string BiWeek1PriceEmpSundayusctr
        //{
        //    get { return BiWeek1PriceEmpSaturday.Text; }
        //    set { BiWeek1PriceEmpSaturday.Text = value; }
        //}

        ////Region Week2;

        //public bool BiWeek2_MondayUsctr
        //{
        //    get { return BiWeek2_Monday.Checked; }
        //    set { BiWeek2_Monday.Checked = value; }
        //}

        //public bool BiWeek2_TuesdayUsctr
        //{
        //    get { return BiWeek2_Tuesday.Checked; }
        //    set { BiWeek2_Tuesday.Checked = value; }
        //}

        //public bool BiWeek2_WednesdayUsctr
        //{
        //    get { return BiWeek2_Wednesday.Checked; }
        //    set { BiWeek2_Wednesday.Checked = value; }
        //}

        //public bool BiWeek2_ThursdayUsctr
        //{
        //    get { return BiWeek2_Thursday.Checked; }
        //    set { BiWeek2_Thursday.Checked = value; }
        //}

        //public bool BiWeek2_FridayUsctr
        //{
        //    get { return BiWeek2_Friday.Checked; }
        //    set { BiWeek2_Friday.Checked = value; }
        //}

        //public bool BiWeek2_SaturdayUsctr
        //{
        //    get { return BiWeek2_Saturday.Checked; }
        //    set { BiWeek2_Saturday.Checked = value; }
        //}

        //public bool BiWeek2_SundayUsctr
        //{
        //    get { return BiWeek2_Sunday.Checked; }
        //    set { BiWeek2_Sunday.Checked = value; }
        //}

        //public string BiWeek2PriceCpmMondayusctr
        //{
        //    get { return BiWeek2PriceCpmMonday.Text; }
        //    set { BiWeek2PriceCpmMonday.Text = value; }
        //}

        //public string BiWeek2PriceCpmTuesdayusctr
        //{
        //    get { return BiWeek2PriceCpmTuesday.Text; }
        //    set { BiWeek2PriceCpmTuesday.Text = value; }
        //}

        //public string BiWeek2PriceCpmWednesdayusctr
        //{
        //    get { return BiWeek2PriceCpmWednesday.Text; }
        //    set { BiWeek2PriceCpmWednesday.Text = value; }
        //}

        //public string BiWeek2PriceCpmThursdaysctr
        //{
        //    get { return BiWeek2PriceCpmThursday.Text; }
        //    set { BiWeek2PriceCpmThursday.Text = value; }
        //}

        //public string BiWeek2PriceCpmFridayusctr
        //{
        //    get { return BiWeek2PriceCpmFriday.Text; }
        //    set { BiWeek2PriceCpmFriday.Text = value; }
        //}

        //public string BiWeek2PriceCpmSaturdayusctr
        //{
        //    get { return BiWeek2PriceCpmSaturday.Text; }
        //    set { BiWeek2PriceCpmSaturday.Text = value; }
        //}

        //public string BiWeek2PriceCpmSundayusctr
        //{
        //    get { return BiWeek2PriceCpmSaturday.Text; }
        //    set { BiWeek2PriceCpmSaturday.Text = value; }
        //}
        //public string BiWeek2PriceEmpMondayusctr
        //{
        //    get { return BiWeek2PriceEmpMonday.Text; }
        //    set { BiWeek2PriceEmpMonday.Text = value; }
        //}

        //public string BiWeek2PriceEmpTuesdayusctr
        //{
        //    get { return BiWeek2PriceEmpTuesday.Text; }
        //    set { BiWeek2PriceEmpTuesday.Text = value; }
        //}

        //public string BiWeek2PriceEmpWednesdayusctr
        //{
        //    get { return BiWeek2PriceEmpWednesday.Text; }
        //    set { BiWeek2PriceEmpWednesday.Text = value; }
        //}

        //public string BiWeek2PriceEmpThursdaysctr
        //{
        //    get { return BiWeek2PriceEmpThursday.Text; }
        //    set { BiWeek2PriceEmpThursday.Text = value; }
        //}

        //public string BiWeek2PriceEmpFridayusctr
        //{
        //    get { return BiWeek2PriceEmpFriday.Text; }
        //    set { BiWeek2PriceEmpFriday.Text = value; }
        //}

        //public string BiWeek2PriceEmpSaturdayusctr
        //{
        //    get { return BiWeek2PriceEmpSaturday.Text; }
        //    set { BiWeek2PriceEmpSaturday.Text = value; }
        //}

        //public string BiWeek2PriceEmpSundayusctr
        //{
        //    get { return BiWeek2PriceEmpSaturday.Text; }
        //    set { BiWeek2PriceEmpSaturday.Text = value; }
        //}

    }
}
