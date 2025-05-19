using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using DataBridge;
using DataBridge.Entity;
using GDTOSQL.Entity;
using PrintPDF;
using SendBillBL.Entity;
using SendBillBL;
using Helpers.generalHelp;
using System.Drawing.Printing;
using PdfiumViewer;


namespace SendBillWF
{
    public partial class CreateBillUsCtr : UserControl
    {
        public CompagniManipulation compagniManipulation = new CompagniManipulation();
        List<CompagniePoco> compagnieInfoList = new List<CompagniePoco>();
        List<BillHistoryPoco> _billHistoryPocoLst = new List<BillHistoryPoco>();

        MapperConfiguration config;
        CompagniePoco compagniePocoProvider;
        CompagniePoco compagniePocoClient;

        SendBill sendBill;
        MailPoco mailPoco;
        DateTime date;
        //string dest;


        CreateBillUsCtr createBillUsCtr;
        IMapper mapper;


        public CreateBillUsCtr()
        {
            List<CompagniePoco> compagniePocosList = new List<CompagniePoco>();

            InitializeComponent();

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompagniePoco, CompagniePoco>();
            });

            compagniePocosList = compagniManipulation.getActiveCompagnies();

            sendBill = new SendBill();
            mailPoco = new MailPoco();

            mapper = config.CreateMapper();

            //compagnieInfoList = compagniManipulation.getActiveCompagnies();
            ProviderCBX.DataSource = compagniePocosList.Where(c => string.IsNullOrEmpty(c.Compagnieprividercode)).ToList();
            ProviderCBX.Name = "CompagnieID";
            ProviderCBX.ValueMember = "CompagnieName";

            ClientCBX.DataSource = compagniePocosList;
            ClientCBX.Name = "CompagnieID";
            ClientCBX.ValueMember = "CompagnieName";

            BillNbrLbl.Text = "Bill Number  : " + (HeadersBill.BillHeadersidentifier).ToString();
            BillDatLbl.Text = "Bill Date        : " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Supprimer");

            item.Click += SupprimerLigneMenu_Click;
            menu.Items.Add(item);
            WorkInfoDgrd.ContextMenuStrip = menu;


            HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill()+1;

        }

        private void ProviderCBX_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ProviderCBX.SelectedItem != null)
            {
                // ProviderTxt 

                compagniePocoProvider = mapper.Map<CompagniePoco>((CompagniePoco)ProviderCBX.SelectedItem);

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
        }

        private void ClientCBX_SelectedIndexChanged(object sender, EventArgs e)   //ClientCBX
        {
            HeadersBill.BillHeadersDate = DateTime.Now.ToString("yyMMdd");
            //HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill();

            //HeadersBill.BillHeadersidentifier = compagniManipulation.getLastBill();
            //HeadersBill.BillHeadersidentifier += 1;



            if (ClientCBX.SelectedItem != null)
            {

                compagniePocoClient = mapper.Map<CompagniePoco>((CompagniePoco)ClientCBX.SelectedItem);

                HeadersBill.BillCompagnieCode = compagniePocoClient.CompagnieCode;

                HeadersBill.BillPath = @"C:\Users\Administrator\Desktop\projet Facture\Print\PrintPDF\Facture\teste\" + HeadersBill.BillCompagnieCode
                                   + "_" + HeadersBill.BillHeadersDate + "_" + HeadersBill.BillHeadersidentifier + ".pdf";

                ClientLbl.Text = compagniePocoClient.CompagnieName;

                ClientLbl.Text += Environment.NewLine;
                if (!string.IsNullOrEmpty(compagniePocoClient.CompagnieSuite))
                {
                    ClientLbl.Text += compagniePocoClient.CompagnieSuite + "_";

                }


                ClientLbl.Text += compagniePocoClient.CompagnieCivicNumber;
                ClientLbl.Text += Environment.NewLine;

                ClientLbl.Text += compagniePocoClient.Compagniecity + "," + compagniePocoClient.CompagnieState
                                 + "," + compagniePocoClient.Compagniecountry;
                ClientLbl.Text += Environment.NewLine;

                ClientLbl.Text += compagniePocoClient.CompagnieZipCode;
                ClientLbl.Text += Environment.NewLine;
            }

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
            SaveButton();
            ReloadUsrcontrol();
        }

        private void SaveButton()
        {
            PrintPDFBIll _printPDFBIll;
            int val = 0;
            float cpyPrice = 0;
            string cpyDescription = "";
            int nbrVisites = 0;
            float ttlPrice = 0;

            //CreateBillUsCtr createBillUsCtr = new CreateBillUsCtr();

            compagniePocoClient._workBillInfoList.Clear();
            

            foreach (DataGridViewRow row in WorkInfoDgrd.Rows)
            {
                // Vérifier si la ligne n'est pas la ligne "nouvelle ligne" (si elle existe)
                if (!row.IsNewRow)
                {
                    float.TryParse(row.Cells[2].Value as string, out cpyPrice); //row.Cells["UnitPrceDgr"]
                    //int.TryParse(row.Cells[1].Value as string, out cpyDescription);//row.Cells["DescriptionDtg"]

                    cpyDescription = row.Cells[1].Value.ToString();
                    int.TryParse(row.Cells[0].Value as string, out nbrVisites); //row.Cells["QuantityDtg"]
                    float.TryParse(row.Cells[3].Value.ToString(), out ttlPrice); //row.Cells["SumDtg"]




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

            if (compagniePocoClient != null && compagniePocoProvider != null)
            {
                _printPDFBIll = new PrintPDFBIll(compagniePocoClient, compagniePocoProvider);
                HeadersBill.BillHeadersidentifier += 1;
                SaveBillHistory();                
            }

            WorkInfoDgrd.Rows.Clear();
            ProviderCBX.SelectedIndex = -1;
            ClientCBX.SelectedIndex = -1;
        }

        public void SaveBillHistory()
        {
            date = DateTime.Now;

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
            billHistoryPoco.BillNumber = HeadersBill.BillHeadersDate;

            string descriptionToSaveHistory = "";

            foreach (var item in compagniePocoClient._workBillInfoList)
            {
                descriptionToSaveHistory += "  ||CompagnyName : " + item.CompagnyName + "  ||NumberOfVisite : " + item.NumberOfVisite + "  ||compagnyPrice : " + item.compagnyPrice +
                            Environment.NewLine;
                sumPrice = sumPrice + item.Totalprice;
                nbrCompagny = nbrCompagny + 1;

            }

            billHistoryPoco.BillDescription = descriptionToSaveHistory;
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

            compagniManipulation.SaveBillHisrory(_billHistoryPocoLst);
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
    }
}



