using AutoMapper;
using DataBridge;
using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using GDTOSQL.Entity;
using Helpers.generalHelp;
using Helpers.generalHelp;
using PdfiumViewer;
using PrintPDF;
using SendBillBL;
using SendBillBL.Entity;
using SendBillWF.PocoGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SendBillWF
{
    public partial class CreateBillUsCtr : UserControl
    {
        public CompagniManipulation compagniManipulation;
        List<CompagniePoco> compagnieInfoList = new List<CompagniePoco>();
        List<BillHistoryPoco> _billHistoryPocoLst = new List<BillHistoryPoco>();
        List<BillDescriptionMAnipulation> billDescriptionMAnipulationList;
        BillDescriptionMAnipulation billDescriptionMAnipulation;
        List<BillDescriptionPoco> billDescriptionPoco;

        MapperConfiguration config;
        CompagniePoco compagniePocoProvider;
        CompagniePoco compagniePocoClient;


        SendBill sendBill;
        MailPoco mailPoco;
        DateTime date;
        //string dest;
        List<CompagniePoco> compagniePocosList;


        CreateBillUsCtr createBillUsCtr;
        IMapper mapper;


        public CreateBillUsCtr()
        {
            compagniePocosList = new List<CompagniePoco>();
            billDescriptionMAnipulationList = new List<BillDescriptionMAnipulation>();
            compagniManipulation = new CompagniManipulation();
            billDescriptionMAnipulation = new BillDescriptionMAnipulation();
            billDescriptionPoco = new List<BillDescriptionPoco>();

            InitializeComponent();

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompagniePoco, CompagniePoco>();
                cfg.CreateMap<BillDescriptionPoco, BillDescription>();
            });

            compagniePocosList = compagniManipulation.getActiveCompagnies();

            sendBill = new SendBill();
            mailPoco = new MailPoco();

            mapper = config.CreateMapper();

            //compagnieInfoList = compagniManipulation.getActiveCompagnies();
            ProviderCBX.DataSource = compagniePocosList.Where(c => c.CompagnieCode == "NettoyageRamssis" || c.CompagnieCode == "SeifDeals" || c.CompagnieCode == "EntretienRamssis").ToList();  //string.IsNullOrEmpty(c.Compagnieprividercode)).ToList();
            ProviderCBX.DisplayMember = "CompagnieName";
            ProviderCBX.ValueMember = "CompagnieID";

            ClientCBX.DataSource = compagniePocosList;
            ClientCBX.DisplayMember = "CompagnieName";
            ClientCBX.ValueMember = "CompagnieID";

            BillNbrLbl.Text = "Bill Number  : " + (HeadersBill.BillHeadersidentifier).ToString();


            // Fix for CS8629: Nullable value type may be null.
            //HeadersBill.BillHeadersDate = HeadersBill.getBilledDate().ToString("dd-MM-yyyy HH:mm:ss");


            //HeadersBill._jobDate.HasValue
            //? HeadersBill._jobDate.Value.ToString("dd-MM-yyyy HH:mm:ss")
            //: DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); ; // Provide a default value (e.g., empty string) if _jobDate is null.





            //BillDatLbl.Text = "Bill Date        : " + HeadersBill._jobDate.Value.ToString("dd-MM-yyyy HH:mm:ss");

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Supprimer");

            item.Click += SupprimerLigneMenu_Click;
            menu.Items.Add(item);
            WorkInfoDgrd.ContextMenuStrip = menu;


            HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill();


        }

        private void ProviderCBX_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ProviderCBX.SelectedItem != null)
            {
                // ProviderTxt 

                compagniePocoProvider = mapper.Map<CompagniePoco>((CompagniePoco)ProviderCBX.SelectedItem);
                FillPriidersCBX(compagniePocoProvider);
            }
        }

        private void FillPriidersCBX(CompagniePoco compagniePoc)
        {

            ProviderCBX.BindingContext = new BindingContext();
            ProviderCBX.SelectedItem = compagniePocosList.Where(c => c.CompagnieCode.Equals(compagniePoc.CompagnieCode)).FirstOrDefault();


            ProviderTxt.Text = compagniePocoProvider.CompagnieName;

            ProviderTxt.Text += Environment.NewLine;
            if (!string.IsNullOrEmpty(compagniePocoProvider.CompagnieSuite))
            {
                ProviderTxt.Text += compagniePocoProvider.CompagnieSuite + "_";
            }

            ProviderTxt.Text += compagniePocoProvider.CompagnieCivicNumber;
            ProviderTxt.Text += Environment.NewLine;

            ProviderTxt.Text += compagniePocoProvider.Compagniecity + "," + compagniePocoProvider.CompagnieState
                             + "," + compagniePocoProvider.Compagniecountry;
            ProviderTxt.Text += Environment.NewLine;

            ProviderTxt.Text += compagniePocoProvider.CompagnieZipCode;
            ProviderTxt.Text += Environment.NewLine;
        }

        private void ClientCBX_SelectedIndexChanged(object sender, EventArgs e)   //ClientCBX
        {
            if (ClientCBX.SelectedItem != null)
            {
                compagniePocoClient = mapper.Map<CompagniePoco>((CompagniePoco)ClientCBX.SelectedItem);
                FillClientCBX(compagniePocoClient);
            }
        }

        public void FillClientCBX(CompagniePoco compagniePoco)
        {

            HeadersBill.BillHeadersDate = HeadersBill._jobDate.HasValue
            ? HeadersBill._jobDate.Value.ToString("yyyyMMdd")
            : DateTime.Now.ToString("yyyyMMdd"); ; // Provide a default value (e.g., empty string) if _jobDate is null.


            ClientCBX.BindingContext = new BindingContext();
            ClientCBX.SelectedItem = compagniePocosList.Where(c => c.CompagnieCode.Equals(compagniePoco.CompagnieCode)).FirstOrDefault();



            HeadersBill.BillCompagnieCode = compagniePoco.CompagnieCode;

            ClientLbl.Text = compagniePoco.CompagnieName;

            ClientLbl.Text += Environment.NewLine;
            if (!string.IsNullOrEmpty(compagniePoco.CompagnieSuite))
            {
                ClientLbl.Text += compagniePoco.CompagnieSuite + "_";

            }




            ClientLbl.Text += compagniePoco.CompagnieCivicNumber;
            ClientLbl.Text += Environment.NewLine;

            ClientLbl.Text += compagniePoco.Compagniecity + "," + compagniePoco.CompagnieState
                             + "," + compagniePoco.Compagniecountry;
            ClientLbl.Text += Environment.NewLine;

            ClientLbl.Text += compagniePocoClient.CompagnieZipCode;
            ClientLbl.Text += Environment.NewLine;
        }

        public void FillBillToUpdate(CompagniePoco compagniePoco, BillHistory billHistory)
        {
            ClearUserControl();

            var prv = compagniePocosList.Where(c => c.CompagnieCode.Equals(compagniePoco.CompagnieProvider)).FirstOrDefault();

            FillClientCBX(compagniePoco);
            FillPriidersCBX(prv);
            FillBillInfoToUpdate(billHistory);



        }

        private void ClearUserControl()
        {
            BillNbrLbl.Text = "";
            billDate.Value = DateTime.Now;
            TotalwithoutTaxLbl.Text = "";
            TPSLbl.Text = "";
            TVQLbl.Text = "";
            TotalwithTaxLbl.Text = "";

        }

        private void FillBillInfoToUpdate(BillHistory billHistory)
        {
            billDescriptionPoco = billDescriptionMAnipulation.GetBillDescriptionByBillHistoryId(billHistory.billIdentifier);


            var gridList = billDescriptionPoco.Select(x => new BillDescriptionGridRow
            {
                QuantityDtg = x.QuantityPoco,
                DescriptionDtg = x.DescriptionPoco,
                UnitPrceDgr = x.UnitPricePoco,
                SumDtg = x.SubTotalPricePoco
            }).ToList();


            WorkInfoDgrd.DataSource = new BindingList<BillDescriptionGridRow>(gridList);

            BillNbrLbl.Text = billHistory.BillNumber;
            billDate.Value = billHistory.BilledDate;
            TotalwithoutTaxLbl.Text = $"{billHistory.TotalWithOutTax:N2}  $";
            TPSLbl.Text = $"{billHistory.TPS:N2}  $";
            TVQLbl.Text = $"{billHistory.TVQ:N2}  $";
            TotalwithTaxLbl.Text = $"{billHistory.TotalWithTax:N2}  $";



        }

        private void WorkInfoDgrd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 2)
            {
                // Obtenir les valeurs des cellules
                DataGridViewRow row = WorkInfoDgrd.Rows[e.RowIndex];

                if (row.Cells["QuantityDtg"].Value != null &&
                    row.Cells["UnitPrceDgr"].Value != null)
                {
                    try
                    {
                        // Convertir les valeurs en nombres
                        double Quantity = Convert.ToDouble(row.Cells["QuantityDtg"].Value);
                        double UnitPrce = Convert.ToDouble(row.Cells["UnitPrceDgr"].Value);

                        // Calculer et afficher le résultat
                        row.Cells["SumDtg"].Value = Quantity * UnitPrce;

                        // Mettre à jour la somme
                        MettreAJourSommeProduits();
                    }
                    catch (FormatException)
                    {
                        row.Cells["SumDtg"].Value = "Erreur";
                    }
                }
                else
                {
                    row.Cells["SumDtg"].Value = DBNull.Value;
                }
            }

        }


        // 3. Méthode pour calculer et afficher la somme
        private void MettreAJourSommeProduits()
        {
            double somme = 0;
            double TPS = 0;
            double TVQ = 0;
            double Total = 0;

            foreach (DataGridViewRow row in WorkInfoDgrd.Rows)
            {
                if (!row.IsNewRow && row.Cells["SumDtg"].Value != null)
                {
                    // Vérifier si la valeur est numérique
                    if (double.TryParse(row.Cells["SumDtg"].Value.ToString(), out double valeur))
                    {
                        somme += valeur;
                    }
                }
            }

            TPS = somme * 0.05;
            TVQ = somme * 0.09975;
            Total = somme + (somme * 0.14975);

            // Afficher la somme dans le Label avec formatage
            TotalwithoutTaxLbl.Text = $"{somme:N2}  $";
            TPSLbl.Text = $"{TPS:N2}  $";
            TVQLbl.Text = $"{TVQ:N2}  $";
            TotalwithTaxLbl.Text = $"{Total:N2}  $";
        }

        private void WorkInfoDgrd_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            MettreAJourSommeProduits();
        }

        private void SupprimerLigneMenu_Click(object sender, EventArgs e)
        {
            if (WorkInfoDgrd.CurrentRow != null && !WorkInfoDgrd.CurrentRow.IsNewRow)
            {
                WorkInfoDgrd.Rows.Remove(WorkInfoDgrd.CurrentRow);
                MettreAJourSommeProduits();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {

            HeadersBill.IsSingleBill = true;
            SaveButton();
            ReloadUsrcontrol();
            HeadersBill.IsUpdate = false;

            ClearUserControl();


        }

        private void SaveButton()
        {

            int val = 0;
            float cpyPrice = 0;
            string cpyDescription = "";
            int nbrVisites = 0;
            float ttlPrice = 0;

            PrintPDFBIll _printPDFBIll;
            BillSearchStatus billSearchStatus = new BillSearchStatus();

            //CreateBillUsCtr createBillUsCtr = new CreateBillUsCtr();

            foreach (DataGridViewRow row in WorkInfoDgrd.Rows)
            {
                // Vérifier si la ligne n'est pas la ligne "nouvelle ligne" (si elle existe)
                if (!row.IsNewRow)
                {
                    float.TryParse(row.Cells[2].Value.ToString(), out cpyPrice);

                    cpyDescription = row.Cells[1].Value.ToString();
                    if (string.IsNullOrEmpty(cpyDescription))
                    {
                        cpyDescription = "service d'entretien menager";
                    }

                    int.TryParse(row.Cells[0].Value.ToString(), out nbrVisites);
                    float.TryParse(row.Cells[3].Value.ToString(), out ttlPrice);




                    compagniePocoClient._workBillInfoList.Add(
                        new WorkBillInfo
                        {
                            CompagnyName = compagniePocoClient.CompagnieName,
                            CompagnyCode = compagniePocoClient.CompagnieCode,

                            compagnyPrice = cpyPrice,
                            JobDescription = row.Cells["DescriptionDtg"].Value as string,
                            NumberOfVisite = nbrVisites,
                            Totalprice = ttlPrice,

                        });
                }
            }

            //compagniePocoClient._workBillInfoList.Clear();




            billSearchStatus.keysearch = BillNbrLbl.Text;
            var bhs = compagniManipulation.GetBillByBillNumber(billSearchStatus).FirstOrDefault();
            

            if (!HeadersBill.IsUpdate)
            {
                
                //HeadersBill.BillHeadersidentifier = bhs.billIdentifier;

                HeadersBill._jobDate = billDate.Value;
                HeadersBill.BillHeadersDate = billDate.Value.ToString("yyyyMMdd");
                HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill() + 1;
                HeadersBill.BillPath = @"C:\Users\Administrator\Desktop\projet Facture\Print\PrintPDF\Facture\teste\" + HeadersBill.BillCompagnieCode
                              + "_" + HeadersBill.BillHeadersDate + "_" + HeadersBill.BillHeadersidentifier + ".pdf";

                HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill() + 1;
            }
            else
            {
                HeadersBill.BillPath = bhs.BillPath;
                HeadersBill._jobDate = bhs.BilledDate;

            }


            if (compagniePocoClient != null && compagniePocoProvider != null)
            {

                HeadersBill.IsSingleBill = true;

                _printPDFBIll = new PrintPDFBIll(compagniePocoClient, compagniePocoProvider);
                SaveBillHistory();
            }

            WorkInfoDgrd.Rows.Clear();
            ProviderCBX.SelectedIndex = -1;
            ClientCBX.SelectedIndex = -1;
        }

        public void SaveBillHistory()
        {
            List<BillDescriptionPoco> billDescriptionlist = new List<BillDescriptionPoco>();

            date = HeadersBill._jobDate.Value;

            float sumPrice = 0;
            int nbrCompagny = 0;
            int Billidentifier = 0;

            //HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill();

            sumPrice = 0;
            nbrCompagny = 0;

            BillHistoryPoco billHistoryPoco = new BillHistoryPoco();
            _billHistoryPocoLst.Clear();


            //fill object to Bill histryPoco
            billHistoryPoco.Id = Guid.NewGuid();
            billHistoryPoco.billIdentifier = HeadersBill.BillHeadersidentifier;
            billHistoryPoco.compagnyName = compagniePocoClient.CompagnieName;
            billHistoryPoco.compagnyCode = compagniePocoClient.CompagnieCode;
            billHistoryPoco.MouthBill = "Fevrier";
            billHistoryPoco.BilledDate = date;
            billHistoryPoco.BillNumber = HeadersBill.getBilledDate().ToString("yyyyMMdd") + "_" + HeadersBill.BillHeadersidentifier;

            string descriptionToSaveHistory = "";

            foreach (var item in compagniePocoClient._workBillInfoList)
            {
                billDescriptionlist.Add(new BillDescriptionPoco
                {
                    BillDescriptionPocoId = Guid.NewGuid(),
                    BillHistoryIdPoco = billHistoryPoco.billIdentifier,
                    QuantityPoco = item.NumberOfVisite,
                    DescriptionPoco = item.JobDescription,
                    UnitPricePoco = item.compagnyPrice,
                    SubTotalPricePoco = item.Totalprice,

                    // DescriptionPoco = "CompagnyName : " + item.CompagnyName + "  ||NumberOfVisite : " + item.NumberOfVisite + "  ||compagnyPrice : " + item.compagnyPrice,

                });

                descriptionToSaveHistory += "  ||CompagnyName : " + item.CompagnyName + "  ||NumberOfVisite : " + item.NumberOfVisite + "  ||compagnyPrice : " + item.compagnyPrice +
                            Environment.NewLine;

                sumPrice = sumPrice + item.Totalprice;
                nbrCompagny = nbrCompagny + 1;

            }

            billHistoryPoco.BillDescriptionText = descriptionToSaveHistory;
            billHistoryPoco.TotalWithOutTax = sumPrice;
            billHistoryPoco.TPS = (sumPrice * 0.05f);
            billHistoryPoco.TVQ = (sumPrice * 0.09975f);
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
        }

        private void ReloadUsrcontrol()
        {
            //var parent = this.Parent;
            //createBillUsCtr = new CreateBillUsCtr()
            //{
            //    Location = new Point(201, 33),
            //    Name = "createBillUsCtr",
            //    Size = new Size(1750, 900)
            //};

            //parent.Controls.Add(createBillUsCtr);
            //this.Dispose();


        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            ReloadUsrcontrol();
        }

        private void sendBillbtn_Click(object sender, EventArgs e)
        {
            SaveButton();

            mailPoco.smtpServer = compagniePocoProvider.smtpServer;
            mailPoco.smtpPort = (int)compagniePocoProvider.smtpPort;
            mailPoco.smtpUsername = compagniePocoProvider.smtpUsername;
            mailPoco.smtpPasswor = compagniePocoProvider.smtpPassword;



            mailPoco.From = compagniePocoProvider.ContactMail;
            mailPoco.To = compagniePocoClient.ContactMail;
            mailPoco.Subject = "Facturation des services d'entretien ménager pour le mois de " + date.ToString("MMMM");
            mailPoco.Path = HeadersBill.BillPath;
            mailPoco.Body = "Bonjour," + Environment.NewLine + "Ci-joint en pièce jointe la facture correspondant aux services d'entretien ménager pour le mois de "
                    + date.ToString("MMMM") + Environment.NewLine + "Merci !" + Environment.NewLine;

            bool state = sendBill.Send(mailPoco);


        }

        private void Print_Click(object sender, EventArgs e)
        {
            SaveButton();
            PrintPDF(HeadersBill.BillPath);
            ReloadUsrcontrol();
        }

        public void PrintPDF(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
            {
                MessageBox.Show("Aucun PDF sélectionné ou chemin invalide.", "Erreur",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);

                // throw new NotImplementedException();
                return;
            }

            if (!File.Exists(pdfPath))
            {
                MessageBox.Show($"Le fichier PDF n'existe pas:\n{pdfPath}", "Erreur",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                //throw new NotImplementedException();
            }

            try
            {
                PrintPdfWithDialog(pdfPath);
                MessageBox.Show("Le PDF a été envoyé à l'imprimante.", "Succès",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'impression:\n{ex.Message}", "Erreur",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PrintPdfWithDialog(string pdfFilePath)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();

            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                using (var document = PdfDocument.Load(pdfFilePath))
                {
                    printDoc.PrintPage += (sender, e) =>
                    {
                        // Correct Render() usage
                        var image = document.Render(
                            page: 0,
                            (int)e.PageBounds.Width,
                            (int)e.PageBounds.Height,
                            dpiX: 300,
                            dpiY: 300,
                            flags: PdfRenderFlags.ForPrinting
                        );

                        e.Graphics.DrawImage(image, e.PageBounds);
                        e.HasMorePages = false; // Set to true if printing multiple pages
                    };
                    printDoc.Print();
                }
            }
        }

        private void billDate_ValueChanged(object sender, EventArgs e)
        {
            HeadersBill.BillHeadersDate = billDate.Value.ToString("yyyyMMdd");        // HeadersBill.getBilledDate().ToString("dd-MM-yyyy HH:mm:ss");
            HeadersBill._jobDate = billDate.Value;


        }
    }
}



