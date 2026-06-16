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
using GDTOSQL.Entity;
using Helpers.generalHelp;
using Helpers.GoogleDrive;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using PrintPDF;
using Helpers.pdfBill;
using Helpers.generalHelp;

namespace SendBillWF
{
    public partial class PDFBillFromGDriveUsCtr : UserControl
    {
        CompagniManipulation compagniManipulation = new CompagniManipulation();
        List<CompagniePoco> compagnieInfoList = new List<CompagniePoco>();
        List<WorkBillInfo> workBillInfos = new List<WorkBillInfo>();
        SheetInfo sheetInfo = new SheetInfo();
        IConfigurationRoot configuration;
        DateFormatter dateFormatter = new DateFormatter();

        public PDFBillFromGDriveUsCtr()
        {
            InitializeComponent();
            //compagnieInfoList = compagniManipulation.LinkCompagniePayment();
            configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile(@"C:\Users\Administrator\Desktop\projet Facture\Print\GDTOSQL\appsettings.json", optional: true, reloadOnChange: true)
                               .Build();

            sheetInfo.spreadsheetId = configuration["AppSettings:spreadsheetId"];

            var ListgoogleSheet = compagniManipulation.GetListgoogleSheet(sheetInfo);
            SpreadSheetCombobx.DataSource = ListgoogleSheet;

            SpreadSheetCombobx.Name = "SheetId";
            SpreadSheetCombobx.ValueMember = "SheetTitle";
            // Fix for CS8629: Nullable value type may be null.
            HeadersBill.BillHeadersDate = HeadersBill.getBilledDate().ToString(("yyyyMMdd"));
            HeadersBill._jobDate = billDate.Value;

            HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill() + 1;
            HeadersBill.BillHeadersDate = billDate.Value.ToString("yyyyMMdd");
        }



        private void SpreadSheetCombobx_SelectedIndexChanged(object sender, EventArgs e)
        {
            //add range configuration["AppSettings:range"]; to sheetInfo
            if (SpreadSheetCombobx.SelectedItem is SheetInfo selectedSheet)
            {
                workBillInfos.Clear();
                //MessageBox.Show("SheetId : " + selectedSheet.SheetId + "   SheetTitle : " + selectedSheet.SheetTitle);
                sheetInfo.SheetId = selectedSheet.SheetId;
                sheetInfo.SheetTitle = selectedSheet.SheetTitle;
                workBillInfos = compagniManipulation.GetGGDrBillData(sheetInfo);

                CreateCheckBoxesFromList(workBillInfos);
            }
        }

        //fonction qui permet de creer des cheqck boxe 
        private void CreateCheckBoxesFromList(List<WorkBillInfo> items)
        {
            int yPosition = 100; // Position verticale initiale
            int xPosition = 20;

            foreach (var chk in this.Controls.OfType<CheckBox>().ToList())
            {
                if (chk.Name != "checkAll")
                {
                    this.Controls.Remove(chk);
                    chk.Dispose();
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                //for (int j = 0; j < 3; j++)
                // {
                CheckBox checkBox = new CheckBox();
                checkBox.Name = items[i].CompagnyCode;
                checkBox.Text = items[i].CompagnyName;
                checkBox.AutoSize = true;
                checkBox.Location = new Point(xPosition, yPosition);

                // Optionnel : ajouter un événement
                checkBox.CheckedChanged += (sender, e) =>
                {
                    CheckBox cb = (CheckBox)sender;
                };

                this.Controls.Add(checkBox);
                xPosition += 250;
                //  }

                //i = i + 3;
                // Créer une nouvelle checkbox

                xPosition = 20;
                yPosition += 30; // Espacement vertical


            }
        }

        private void Save_Click(object sender, EventArgs e)
        {

            var selectedItems = new List<string>();

            // Pour WinForms
            foreach (Control control in this.Controls)
            {
                if (control is CheckBox cb && cb.Checked)
                {
                    selectedItems.Add(cb.Name);
                }
            }

           
            SavePdfSelectedCompagny(selectedItems);
            UncheckAll();
        }


        public void SavePdfSelectedCompagny(List<string> selectedCompagnyListToprint)

        {
            List<BillDescriptionPoco> billDescriptionlist = new List<BillDescriptionPoco>();

            // HeadersBill.BillHeadersDate = DateTime.Now.ToString("yyMMdd");
            // HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill();

            PrintPDFBIll _printPDFBIll;
            float sumPrice = 0;
            int nbrCompagny = 0;

            // BillHistoryPoco billHistoryPoco1 = new BillHistoryPoco();
            List<BillHistoryPoco> _billHistoryPocoLst = new List<BillHistoryPoco>();



            compagnieInfoList = compagniManipulation.LinkCompagniePayment(sheetInfo);


            var resultCustomer = compagnieInfoList.Where(item => selectedCompagnyListToprint.Any(item2 => item2 == item.CompagnieCode)).ToList();

            HeadersBill._jobDate = billDate.Value;



            //HeadersBill._jobDate = new DateTime(2025, dateFormatter.getCorrecteNameOfMouth(sheetInfo.SheetTitle), 1);

            // ILFAUT TROUVER LE PROVIDERS DE LA COMPAGNY

            foreach (var compagnieInfo in resultCustomer)
            {
                if (!string.IsNullOrEmpty(compagnieInfo.CompagnieProvider))
                {
                    var compagnieProviderInfo = compagniManipulation.GetCompagnyByName(compagnieInfo.CompagnieProvider).First();

                    //DateTime date = HeadersBill._jobDate.Value;

                    //DateTime finMois = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)).AddDays(1).AddTicks(-1);


                    HeadersBill.BillCompagnieCode = compagnieInfo.CompagnieCode;
                    //HeadersBill.BillPath = @"C:\Users\Administrator\Desktop\projet Facture\Print\PrintPDF\Facture\teste\" + HeadersBill.BillCompagnieCode
                    //                   + "_" + HeadersBill.endMounth().ToString("yyMMdd")  + HeadersBill.BillHeadersidentifier + ".pdf"; 
                    //HeadersBill.BillHeadersDate.ToString("YYMMdd")


                    HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill() + 1;
                    HeadersBill.BillPath = @"C:\Users\Administrator\Desktop\projet Facture\Print\PrintPDF\Facture\teste\" + HeadersBill.BillCompagnieCode
                                       + "_" + HeadersBill.BillHeadersDate + "_" + HeadersBill.BillHeadersidentifier + ".pdf";

                    _printPDFBIll = new PrintPDFBIll(compagnieInfo, compagnieProviderInfo);

                    sumPrice = 0;
                    nbrCompagny = 0;

                    BillHistoryPoco billHistoryPoco = new BillHistoryPoco();

                    //fill object to Bill histryPoco
                    billHistoryPoco.Id = Guid.NewGuid();
                    billHistoryPoco.billIdentifier = HeadersBill.BillHeadersidentifier;
                    billHistoryPoco.compagnyName = compagnieInfo.CompagnieName;
                    billHistoryPoco.compagnyCode = compagnieInfo.CompagnieCode;

                    // il faut changer decebre par la date choisi dans la dropdownlist du controleur BillHistoryUsctr
                    billHistoryPoco.MouthBill = "Decembre";
                    billHistoryPoco.BilledDate = HeadersBill.getBilledDate();

                    // billHistoryPoco.BilledDate = HeadersBill._jobDate;
                    billHistoryPoco.BillNumber = HeadersBill.getBilledDate().ToString("yyyyMMdd") + "_" + HeadersBill.BillHeadersidentifier;

                    string descriptionToSaveHistory = "";
                    foreach (var item in compagnieInfo._workBillInfoList)
                    {
                        billDescriptionlist.Add(new BillDescriptionPoco
                        {
                            BillDescriptionPocoId = Guid.NewGuid(),
                            BillHistoryIdPoco = billHistoryPoco.billIdentifier,
                            QuantityPoco = item.NumberOfVisite,
                            DescriptionPoco = item.JobDescription,
                            UnitPricePoco = item.compagnyPrice,
                            SubTotalPricePoco = item.Totalprice,

                        });

                        descriptionToSaveHistory += "  ||CompagnyName : " + item.CompagnyName + "  ||NumberOfVisite : " + item.NumberOfVisite + "  ||compagnyPrice : " + item.compagnyPrice +
                                    Environment.NewLine;
                        sumPrice = sumPrice + item.Totalprice;
                        nbrCompagny = nbrCompagny + 1;

                    }

                    //billHistoryPoco.BillDescription = descriptionToSaveHistory;
                    billHistoryPoco.TotalWithOutTax = sumPrice;
                    billHistoryPoco.TPS = (sumPrice * 0.05f);
                    billHistoryPoco.TVQ = (sumPrice * 0.0975f);
                    billHistoryPoco.TotalWithTax = (sumPrice + sumPrice * 0.05f + sumPrice * 0.09975f);
                    billHistoryPoco.BillPath = HeadersBill.BillPath;
                    billHistoryPoco.BillHistoryNote = "";


                    if (sumPrice != 0 && nbrCompagny != 0 && !string.IsNullOrEmpty(descriptionToSaveHistory))
                    {
                        _billHistoryPocoLst.Add(billHistoryPoco);

                    }
                    else
                    {

                    }

                    
                    compagniManipulation.SaveBillHisrory(_billHistoryPocoLst, billDescriptionlist);
                    HeadersBill.BillHeadersidentifier += 1;
                    _billHistoryPocoLst.Remove(billHistoryPoco);
                }

            }

           // compagniManipulation.SaveBillHisrory(_billHistoryPocoLst);
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAll.Checked)
            {
                foreach (Control control in this.Controls)
                {
                    if (control is CheckBox cb)
                    {
                        cb.Checked = true;
                    }
                }
            }
            else
            {
                UncheckAll();
            }
        }

        private void UncheckAll()
        {
            foreach (Control control in this.Controls)
            {
                if (control is CheckBox cb)
                {
                    cb.Checked = false;
                }
            }
        }

        private void billDate_ValueChanged(object sender, EventArgs e)
        {
            HeadersBill.BillHeadersDate = billDate.Value.ToString("yyyyMMdd");
        }
    }
}
