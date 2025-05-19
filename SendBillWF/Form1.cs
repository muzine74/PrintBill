using System.IO;
using System.Windows.Forms;
using SendBillBL;
using SendBillBL.Entity;

namespace SendBillWF
{
    public partial class Form1 : Form
    {
        SendBill sendBill;
        MailPoco mailPoco;
        public Form1()
        {
            InitializeComponent();
            sendBill = new SendBill();
            mailPoco = new MailPoco();
           

        }

        void FillMessage()
        {
            foreach (var billHistories in sendBill.BillHistories)
            {
                foreach (var compagieInfoLst in sendBill.CompagieInfoLst)
                {
                    if (billHistories.compagnyCode == compagieInfoLst.CompagnieCode)
                    {
                        From.Text = "contact@nettoyageRamssis.ca";
                        To.Text  = "contact@nettoyageRamssis.ca";  //compagieInfoLst.ContactMail;
                        Subject.Text = "Facturation des services d'entretien ménager pour le mois de " + billHistories.MouthBill;
                        AttachedFile.Text  = billHistories.BillPath;
                        MessageBody.Text  = "Bonjour," + Environment.NewLine + "Ci-joint en pièce jointe la facture correspondant aux services d'entretien ménager pour le mois de "
                                + billHistories.MouthBill + Environment.NewLine + "Merci !" + Environment.NewLine;

                        mailPoco.From = "contact@nettoyageRamssis.ca";
                        To.Text = mailPoco.To = "contact@nettoyageRamssis.ca";  //compagieInfoLst.ContactMail;
                        Subject.Text = mailPoco.Subject = "Facturation des services d'entretien ménager pour le mois de " + billHistories.MouthBill;
                        AttachedFile.Text = mailPoco.Path = billHistories.BillPath;
                        MessageBody.Text = mailPoco.Body = "Bonjour," + Environment.NewLine + "Ci-joint en pièce jointe la facture correspondant aux services d'entretien ménager pour le mois de "
                                + billHistories.MouthBill + Environment.NewLine + "Merci !" + Environment.NewLine;

                        if (sendBill.Send(mailPoco))
                        {
                            File.Copy(mailPoco.Path, Path.GetDirectoryName(mailPoco.Path) + @"\SendedBill\" + Path.GetFileName(mailPoco.Path));
                        }
                    }
                }

            }
        }

        private void Begin_Click(object sender, EventArgs e)
        {
            var compagnie = sendBill.GetBillHistoryList().FirstOrDefault();
            if (compagnie != null)
            {
                foreach (var compagieInfoLst in sendBill.CompagieInfoLst)
                {
                    if (compagieInfoLst.CompagnieCode == compagnie.compagnyCode)
                    {
                        From.Text = "contact@nettoyageRamssis.ca";
                        To.Text = "contact@nettoyageRamssis.ca"; //compagieInfoLst.ContactMail;
                        Subject.Text = "Facturation des services d'entretien ménager pour le mois de " + compagnie.MouthBill;
                        AttachedFile.Text = compagnie.BillPath;
                        MessageBody.Text = "Bonjour," + Environment.NewLine + "Ci-joint en pièce jointe la facture correspondant aux services d'entretien ménager pour le mois de "
                                + compagnie.MouthBill + Environment.NewLine + "Merci !" + Environment.NewLine;

                        mailPoco.From = "contact@nettoyageRamssis.ca";
                        mailPoco.To = "contact@nettoyageRamssis.ca";  //compagieInfoLst.ContactMail;
                        mailPoco.Subject = "Facturation des services d'entretien ménager pour le mois de " + compagnie.MouthBill;
                        mailPoco.Path = compagnie.BillPath;
                        mailPoco.Body = "Bonjour," + Environment.NewLine + "Ci-joint en pièce jointe la facture correspondant aux services d'entretien ménager pour le mois de "
                                + compagnie.MouthBill + Environment.NewLine + "Merci !" + Environment.NewLine;

                        if (sendBill.Send(mailPoco))
                        {
                            

                            try
                            {
                                if (File.Exists(mailPoco.Path))
                                {
                                     //File.Copy(mailPoco.Path, Path.GetDirectoryName(mailPoco.Path) + @"\SendedBill\" + Path.GetFileName(mailPoco.Path));
                                      //File.Delete(mailPoco.Path);

                                    //File.Move(mailPoco.Path, Path.GetDirectoryName(mailPoco.Path) + @"\SendedBill\" + Path.GetFileName(mailPoco.Path));
                                    sendBill.compagniManipulation.UpdateBillHistory(mailPoco.Path);


                                }
                                else
                                {
                                    Console.WriteLine("File does not exist.");
                                }
                            }
                            catch (IOException ex)
                            {
                                Console.WriteLine("An error occurred: " + ex.Message);
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                Console.WriteLine("Access denied: " + ex.Message);
                            }
                        }
                    }

                }
                

            }

        }
    }
}
