using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBridge;
using DBConnection.Entity;
using Helpers.generalHelp;
using PdfiumViewer;


namespace SendBillWF
{
    public partial class BillHistoryUsctr : UserControl
    {
        public CompagniManipulation compagniManipulation = new CompagniManipulation();
        private const int PdfPathColumnIndex = 14;

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem itemImprimer;
        private ToolStripMenuItem itemEnvoyer;
        private ToolStripMenuItem itemDisplay;

        public BillHistoryUsctr()
        {
            InitializeComponent();
            InitializeContextMenu();
        }

        public List<BillHistory> GetBillHistoryList(BillSearchStatus BillSearchStatus)
        {

            return compagniManipulation.GetBill(BillSearchStatus);
        }

        private void SearchbtnHsBtn_Click(object sender, EventArgs e)
        {
            BillSearchStatus BillSearchStatus = new BillSearchStatus();

            BillHistoryDGrid.AutoGenerateColumns = false;
            BillHistoryDGrid.AllowUserToOrderColumns = true;

            // Supprimer toutes les colonnes existantes (au cas où)
            BillHistoryDGrid.Columns.Clear();

            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            ID.HeaderText = "ID"; // Texte d'en-tête de la colonne
            ID.DataPropertyName = "ID"; // Propriété de la source de données à lier
            ID.Visible = false;
            BillHistoryDGrid.Columns.Add(ID);

            // Colonne pour la propriété "Nom"
            DataGridViewTextBoxColumn BillNumber = new DataGridViewTextBoxColumn();
            BillNumber.HeaderText = "Bill Number"; // Texte d'en-tête de la colonne
            BillNumber.DataPropertyName = "BillNumber"; // Propriété de la source de données à lier
            BillNumber.SortMode = DataGridViewColumnSortMode.Automatic;
            BillHistoryDGrid.Columns.Add(BillNumber);

            DataGridViewTextBoxColumn billIdentifier = new DataGridViewTextBoxColumn();
            billIdentifier.HeaderText = "billIdentifier"; // Texte d'en-tête de la colonne
            billIdentifier.DataPropertyName = "billIdentifier"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(billIdentifier);

            DataGridViewTextBoxColumn compagnyName = new DataGridViewTextBoxColumn();
            compagnyName.HeaderText = "compagnyName"; // Texte d'en-tête de la colonne
            compagnyName.DataPropertyName = "compagnyName"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(compagnyName);

            DataGridViewTextBoxColumn compagnyCode = new DataGridViewTextBoxColumn();
            compagnyCode.HeaderText = "compagnyCode"; // Texte d'en-tête de la colonne
            compagnyCode.DataPropertyName = "compagnyCode"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(compagnyCode);

            DataGridViewTextBoxColumn MouthBill = new DataGridViewTextBoxColumn();
            MouthBill.HeaderText = "MouthBill"; // Texte d'en-tête de la colonne
            MouthBill.DataPropertyName = "MouthBill"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(MouthBill);

            DataGridViewTextBoxColumn BilledDate = new DataGridViewTextBoxColumn();
            BilledDate.HeaderText = "BilledDate"; // Texte d'en-tête de la colonne
            BilledDate.DataPropertyName = "BilledDate"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(BilledDate);


            DataGridViewTextBoxColumn BillDescription = new DataGridViewTextBoxColumn();
            BillDescription.HeaderText = "BillDescription"; // Texte d'en-tête de la colonne
            BillDescription.DataPropertyName = "BillDescription"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(BillDescription);

            DataGridViewTextBoxColumn compagnyPrice = new DataGridViewTextBoxColumn();
            compagnyPrice.HeaderText = "compagnyPrice"; // Texte d'en-tête de la colonne
            compagnyPrice.DataPropertyName = "compagnyPrice"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(compagnyPrice);

            DataGridViewTextBoxColumn NumberOfVisite = new DataGridViewTextBoxColumn();
            NumberOfVisite.HeaderText = "NumberOfVisite"; // Texte d'en-tête de la colonne
            NumberOfVisite.DataPropertyName = "NumberOfVisite"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(NumberOfVisite);

            DataGridViewTextBoxColumn TotalWithOutTax = new DataGridViewTextBoxColumn();
            TotalWithOutTax.HeaderText = "TotalWithOutTax"; // Texte d'en-tête de la colonne
            TotalWithOutTax.DataPropertyName = "TotalWithOutTax"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(TotalWithOutTax);

            DataGridViewTextBoxColumn TPS = new DataGridViewTextBoxColumn();
            TPS.HeaderText = "TPS"; // Texte d'en-tête de la colonne
            TPS.DataPropertyName = "TPS"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(TPS);

            DataGridViewTextBoxColumn TVQ = new DataGridViewTextBoxColumn();
            TVQ.HeaderText = "TVQ"; // Texte d'en-tête de la colonne
            TVQ.DataPropertyName = "TVQ"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(TVQ);

            DataGridViewTextBoxColumn TotalWithTax = new DataGridViewTextBoxColumn();
            TotalWithTax.HeaderText = "TotalWithTax"; // Texte d'en-tête de la colonne
            TotalWithTax.DataPropertyName = "TotalWithTax"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(TotalWithTax);

            DataGridViewTextBoxColumn BillPath = new DataGridViewTextBoxColumn();
            BillPath.HeaderText = "BillPath"; // Texte d'en-tête de la colonne
            BillPath.DataPropertyName = "BillPath"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(BillPath);

            DataGridViewTextBoxColumn Issended = new DataGridViewTextBoxColumn();
            Issended.HeaderText = "Issended"; // Texte d'en-tête de la colonne
            Issended.DataPropertyName = "Issended"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(Issended);

            DataGridViewTextBoxColumn IsPayed = new DataGridViewTextBoxColumn();
            IsPayed.HeaderText = "IsPayed"; // Texte d'en-tête de la colonne
            IsPayed.DataPropertyName = "IsPayed"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(IsPayed);

            DataGridViewTextBoxColumn BillHistoryNote = new DataGridViewTextBoxColumn();
            BillHistoryNote.HeaderText = "BillHistoryNote"; // Texte d'en-tête de la colonne
            BillHistoryNote.DataPropertyName = "BillHistoryNote"; // Propriété de la source de données à lier
            BillHistoryDGrid.Columns.Add(BillHistoryNote);


            BillSearchStatus.keysearch = keySearchtxt.Text;

            foreach (object itemChecked in payedListBox.CheckedItems)
            {
                if (!string.IsNullOrEmpty(itemChecked.ToString()))
                {
                    if (itemChecked.ToString() == "payer")
                    {
                        BillSearchStatus.payed = true;
                    }

                    if (itemChecked.ToString() == "Nonpayer")
                    {
                        BillSearchStatus.notPayed = true;
                    }

                }
            }


            foreach (object itemChecked in SendedCheckBox.CheckedItems)
            {
                if (!string.IsNullOrEmpty(itemChecked.ToString()))
                {
                    if (itemChecked.ToString() == "Envoye")
                    {
                        BillSearchStatus.Sended = true;
                    }

                    if (itemChecked.ToString() == "NonEnvoye")
                    {
                        BillSearchStatus.NotSended = true;
                    }
                }
            }

            if (intervalDatecheckBox.Checked)
            {
                BillSearchStatus.BeginDate = BeginDate.Value;
                BillSearchStatus.EndDate = EndDate.Value;
            }
            else
            {
                BillSearchStatus.BeginDate = null;
                BillSearchStatus.EndDate = null;
            }


            BillHistoryDGrid.DataSource = GetBillHistoryList(BillSearchStatus);
            BillHistoryDGrid.Refresh();

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (intervalDatecheckBox.Checked)
            {
                BeginDate.Enabled = true;
                EndDate.Enabled = true;
            }
            else
            {
                BeginDate.Enabled = false;
                EndDate.Enabled = false;
            }
        }


        

        //private void PrintPdfMenuItem_Click(object sender, MouseEventArgs e)
        //{
        //    string pdfPath = GetSelectedPdfPath();
        //    PrintPDF(pdfPath);

        //}

        public void PrintPDF(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
            {
                //MessageBox.Show("Aucun PDF sélectionné ou chemin invalide.", "Erreur",
                //              MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw new NotImplementedException();
                //return;
            }

            if (!File.Exists(pdfPath))
            {
                //MessageBox.Show($"Le fichier PDF n'existe pas:\n{pdfPath}", "Erreur",
                //              MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                throw new NotImplementedException();
            }

            try
            {
                PrintPdfWithDialog(pdfPath);
                //MessageBox.Show("Le PDF a été envoyé à l'imprimante.", "Succès",
                //              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Erreur lors de l'impression:\n{ex.Message}", "Erreur",
                //              MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private string GetSelectedPdfPath()
        {

            if (BillHistoryDGrid.SelectedRows.Count == 0) return null;

            var selectedRow = BillHistoryDGrid.SelectedRows[0];
            if (PdfPathColumnIndex < selectedRow.Cells.Count)
            {
                return selectedRow.Cells[PdfPathColumnIndex].Value?.ToString();
            }
            return null;

        }

        private void BillHistoryDGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = BillHistoryDGrid.HitTest(e.X, e.Y);
                if (hitTest.Type == DataGridViewHitTestType.Cell)
                {
                    BillHistoryDGrid.ClearSelection();
                    BillHistoryDGrid.Rows[hitTest.RowIndex].Selected = true;

                    contextMenuStrip1.Show(BillHistoryDGrid, new Point(e.X, e.Y));

                    //stripMenuPrint();
                }
            }

        }

        private void InitializeContextMenu()
        {
            contextMenuStrip1 = new ContextMenuStrip();
            itemImprimer = new ToolStripMenuItem("Imprimer");
            itemEnvoyer = new ToolStripMenuItem("Envoyer");
            itemDisplay = new ToolStripMenuItem("afficher");

            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { itemImprimer, itemEnvoyer , itemDisplay });

            itemImprimer.Click += ItemImprimer_Click;
            itemEnvoyer.Click += ItemEnvoyer_Click;
            itemDisplay.Click += ItemDisplay_Click;
        }

        private void ItemDisplay_Click(object? sender, EventArgs e)
        {
            if (BillHistoryDGrid.SelectedRows.Count > 0)
            {
                var row = BillHistoryDGrid.SelectedRows[0];
                MessageBox.Show("Envoi de la ligne : " + row.Cells[14].Value.ToString(), "Imprimer");


                Process.Start(new ProcessStartInfo(row.Cells[14].Value.ToString()) { UseShellExecute = true });
            }

            
            //throw new NotImplementedException();
        }

        private void ItemImprimer_Click(object sender, EventArgs e)
        {
            if (BillHistoryDGrid.SelectedRows.Count > 0)
            {
                var row = BillHistoryDGrid.SelectedRows[0];

                // Ajoute ici ton code d'impression réel
                stripMenuPrint();
            }
        }

        private void ItemEnvoyer_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("la fonction Envoyer n'est pas implementè : ");
            //if (BillHistoryDGrid.SelectedRows.Count > 0)
            //{
               
            //}
        }

        //private void PrintPdfMenuItem_Click(object sender, DataGridViewCellEventArgs e)
        //{
        //    //stripMenuPrint();
        //}

        private void stripMenuPrint()
        {
            string pdfPath = GetSelectedPdfPath();

            if (string.IsNullOrEmpty(pdfPath))
            {
                MessageBox.Show("Aucun PDF sélectionné ou chemin invalide.", "Erreur",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(pdfPath))
            {
                MessageBox.Show($"Le fichier PDF n'existe pas:\n{pdfPath}", "Erreur",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
    }
}
