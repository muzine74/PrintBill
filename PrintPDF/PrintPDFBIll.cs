
using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using Helpers.generalHelp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfiumViewer;
using System;
using System.IO;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using iTextSharp.LGPLv2.Core;





namespace PrintPDF
{
    public class PrintPDFBIll
    {
        //int Billidentifier = 0;
        CompagniManipulation compagniManipulation = new CompagniManipulation();

        public PrintPDFBIll()
        {

        }
        public PrintPDFBIll(CompagniePoco CustomToPrint , CompagniePoco Provider)
        {
  
            //DateTime now = DateTime.Now;
            // Chemin du fichier PDF de sortie
            float sumPrice = 0;
            int nbrCompagny = 0;
            string descriptionToSaveHistory = "";

            // Ajouter l'image en arrière-plan
          //string imagePath = "background.jpg"; // Chemin de l'image
          //  Image background = Image.GetInstance(imagePath);
          //  background.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height); // Ajuster l'image à la taille de la page
          //      background.SetAbsolutePosition(0, 0); // Positionner l'image en bas à gauche


            DateTime date = HeadersBill._jobDate.Value;

            DateTime debutMois = new DateTime(date.Year, date.Month, 1);

            DateTime finMois = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)).AddDays(1).AddTicks(-1);

            

            //remplir les info de la facture
            foreach (var item in CustomToPrint._workBillInfoList)
            {
                sumPrice = sumPrice + item.Totalprice;
                nbrCompagny = nbrCompagny + 1;

                descriptionToSaveHistory += "||CompagnyName : " + item.NumberOfVisite + "||NumberOfVisite : " + item.compagnyPrice +
                    "||compagnyPrice : " + item.CompagnyName + Environment.NewLine;


            }

            //teste si la facture est valide
            if (sumPrice != 0 && nbrCompagny != 0 && !string.IsNullOrEmpty(descriptionToSaveHistory))
            {
                // Création du document PDF
                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HeadersBill.BillPath, FileMode.Create));
                document.Open();


                Font boldFont = new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD);
                Font infoFont = new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL);
                Font font = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD); // Taille 10
                Font clausefont = new Font(Font.FontFamily.HELVETICA, 6, Font.NORMAL); // Taille 10
                Font billnumberfont = new Font(Font.FontFamily.HELVETICA, 6, Font.NORMAL); // Taille 10


                // Ajouter le contenu de la facture
                Paragraph title = new Paragraph("Facture", new Font(Font.FontFamily.HELVETICA, 30, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                //document.Add(new Paragraph(" ")); // Espace

                Paragraph billInfo = new Paragraph();

                billInfo.Alignment = Element.ALIGN_RIGHT;
                billInfo.Add("Facture Nº : " + HeadersBill.getBilledDate().ToString("yyyyMMdd") + "_" + HeadersBill.BillHeadersidentifier + "\n");

                if (HeadersBill.IsSingleBill)
                {
                    billInfo.Add("Date: " + HeadersBill._jobDate + "\n");
                }
                else
                {
                    billInfo.Add("Date: " + finMois + "\n");
                }

                
                document.Add(billInfo);





                // remplir le fournisseur   Provider
                Paragraph providerInfo = new Paragraph();
                providerInfo.Alignment = Element.ALIGN_LEFT;
                providerInfo.Add(new Paragraph("De : \n", boldFont));
                providerInfo.Add(new Paragraph(Provider.CompagnieName + "\n", boldFont));
                providerInfo.Add(new Paragraph(Provider.CompagnieCivicNumber + " ,suite : " + CustomToPrint.CompagnieSuite + "\n", infoFont));
                providerInfo.Add(new Paragraph(Provider.Compagniecity + ","
                                                + Provider.CompagnieState + ","
                                                + Provider.Compagniecountry + "\n", infoFont));
                providerInfo.Add(new Paragraph(Provider.CompagnieZipCode + "\n", infoFont));
                providerInfo.Add(new Paragraph("Tel : " + Provider.ContactPhones + "\n", infoFont));
                document.Add(providerInfo);

                document.Add(new Paragraph(" ")); // Espace

                //remplir le client
                Paragraph customInfo = new Paragraph();
                customInfo.Alignment = Element.ALIGN_LEFT;
                customInfo.Add(new Paragraph("A : \n", boldFont));
                customInfo.Add(new Paragraph(CustomToPrint.CompagnieName + "\n", boldFont));
                customInfo.Add(new Paragraph(CustomToPrint.CompagnieCivicNumber + " ,suite : " + CustomToPrint.CompagnieSuite + "\n", infoFont));
                customInfo.Add(new Paragraph(CustomToPrint.Compagniecity + ","
                                                + CustomToPrint.CompagnieState + ","
                                                + CustomToPrint.Compagniecountry + "\n", infoFont));
                customInfo.Add(new Paragraph(CustomToPrint.CompagnieZipCode + "\n", infoFont));
                //providerInfo.Add(new Paragraph("H1J 1A3,\n", +"\n", infoFont));
                customInfo.Add(new Paragraph("Tel : " + CustomToPrint.ContactPhones + "\n", infoFont));
                document.Add(customInfo);

                document.Add(new Paragraph(" ")); // Espace
                document.Add(new Paragraph(" ")); // Espace
                                                  // En-têtes du tableau




                //remplir la facture

                PdfPTable table = new PdfPTable(4); // 4 colonnes
                table.WidthPercentage = 100; // Largeur du tableau à 100% de la page
                float[] columnWidths = { 60f, 8f, 16f, 16f }; // 60%, 10%, 10%, 20%
                table.SetWidths(columnWidths);
                table.HorizontalAlignment = Element.ALIGN_CENTER; // Centrer le tableau

                table.AddCell(new PdfPCell(new Phrase("Description du services", font)));
                table.AddCell(new PdfPCell(new Phrase("Qty", font)));
                table.AddCell(new PdfPCell(new Phrase("Prix unitaire", font)));
                table.AddCell(new PdfPCell(new Phrase("Total", font)));

                

                foreach (var item in CustomToPrint._workBillInfoList)
                {
                    if (string.IsNullOrEmpty(item.JobDescription))
                    {                      

                        if (HeadersBill._jobDate.HasValue)
                        {
                            item.JobDescription = "service d'entretien menager " + "du  " + debutMois.ToString("dd/MMM/yyyy") + " au " + finMois.ToString("dd/MMM/yyyy");
                        }
                        else
                        {
                            item.JobDescription = "service d'entretien menager";
                        }
                    }

                    table.AddCell(item.JobDescription);
                    table.AddCell(item.NumberOfVisite.ToString());
                    table.AddCell(item.compagnyPrice.ToString());
                    table.AddCell(item.Totalprice.ToString());
                    //sumPrice = sumPrice + item.Totalprice;
                    //nbrCompagny = nbrCompagny + 1;

                    //descriptionToSaveHistory += "||CompagnyName : " + item.NumberOfVisite + "||NumberOfVisite : " + item.compagnyPrice +
                    //    "||compagnyPrice : " + item.CompagnyName + Environment.NewLine;


                }

                for (int i = 0; i < 20 - nbrCompagny; i++)
                {
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                }

                document.Add(table);


                PdfPTable table2 = new PdfPTable(2); // 4 colonnes

                table2.WidthPercentage = 32; // Largeur du tableau à 100% de la page
                float[] columnWidths2 = { 16f, 16f }; // 60%, 10%, 15%, 15%
                table2.SetWidths(columnWidths2);
                table2.HorizontalAlignment = Element.ALIGN_RIGHT; // Centrer le tableau


                // Ajout des lignes d'articles
                table2.AddCell("TPS (5%)");
                table2.AddCell((sumPrice * 0.05).ToString("F2") + "$");

                table2.AddCell("TVQ (9.975%)");
                table2.AddCell((sumPrice * 0.0975).ToString("F2") + "$");

                table2.AddCell("Total");
                table2.AddCell((sumPrice + sumPrice * 0.05 + sumPrice * 0.09975).ToString("F2") + "$");


                document.Add(table2);


                document.Add(new Paragraph(new Phrase("TPS : " + Provider.TPSNumber, font)));
                document.Add(new Paragraph(new Phrase("TVQ : " + Provider.TVQNumber, font)));

                document.Add(new Paragraph(" ")); // Espace

                document.Add(new Paragraph(new Phrase("Veuillez rédiger tous les chèques à l'ordre de " + Provider.CompagnieName + " \n", clausefont )));
                document.Add(new Paragraph(new Phrase("Pour toute question concernant cette facture, veuillez contacter ." + Provider .ContactPhones +" " + Provider.ContactMail, clausefont)));

                document.Close();
            }
        }
    }
}
