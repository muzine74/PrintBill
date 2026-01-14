using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using Helpers.generalHelp;
using Helpers.pdfBill;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SendBillWF.Bill
{
    public partial class UpdateBillUsCtr : UserControl
    {
        CompagniManipulation compagniManipulation;
        BillSearchStatus billSearchStatus;
        public BillHistory BillHistories;
        CompagniePoco cpyPocoClient;
        CompagniePoco cpyPocoProvider;
        CompagniePoco compagniePoco;
        private readonly int _billIdentifier;


        public UpdateBillUsCtr()
        {
            billSearchStatus = new BillSearchStatus();
            compagniManipulation = new CompagniManipulation();

            InitializeComponent();
        }

        //private void LoadBill()
        //{
        //    // Exemple EF Core
        //    var billHistoryResult = compagniManipulation.GetBillByBillNumber(billSearchStatus).FirstOrDefault();

        //    if (billHistoryResult == null)
        //        return;

        //   MessageBox.Show("je suis dans le user control update bill");    
        //    // autres champs...
        //}

        // public List<CompagniePoco> CompagieInfoLst;

        private void SearchLbl_Click(object sender, EventArgs e)
        {
            // ProviderCBX

            //createBillUpdateUsCtr1.ProviderCBX.Items.Clear();

            //BillHistories = new BillHistory();
            cpyPocoClient = new CompagniePoco();
            cpyPocoProvider = new CompagniePoco();
            compagniePoco = new CompagniePoco();

            HeadersBill.IsUpdate = true;

            billSearchStatus.keysearch = SearchTxt.Text;

            var billHistoryResult = compagniManipulation.GetBillByBillNumber(billSearchStatus).FirstOrDefault();

            if (billHistoryResult == null)
            {
                //BillHistories = billHistoryResult;
            }


            cpyPocoClient = compagniManipulation.GetCompagnyByCode(billHistoryResult.compagnyCode);
            cpyPocoProvider = compagniManipulation.GetCompagnyByCode(cpyPocoClient.CompagnieProvider);


            // reste a remplir l'interface a updater

            compagniePoco.CompagnieName = billHistoryResult.compagnyName;
            compagniePoco.CompagnieID = cpyPocoClient.CompagnieID;
            compagniePoco.CompagnieCode = billHistoryResult.compagnyCode;
            compagniePoco.CompagnieCode = billHistoryResult.compagnyCode;
            //compagniePoco.CompagnieStatus = billHistoryResult.com
            compagniePoco.Compagniecountry = cpyPocoClient.Compagniecountry ;
            compagniePoco.CompagnieState = cpyPocoClient.CompagnieState ;
            compagniePoco.Compagniecity = cpyPocoClient.Compagniecity ;
            compagniePoco.CompagnieZipCode = cpyPocoClient.CompagnieZipCode ;
            compagniePoco.CompagnieSuite = cpyPocoClient.CompagnieSuite ;
            compagniePoco.CompagnieCivicNumber = cpyPocoClient.CompagnieCivicNumber ;
            compagniePoco.CompagnieProvider = cpyPocoClient.CompagnieProvider;
            compagniePoco.ContactName = cpyPocoClient.ContactName ;
            compagniePoco.ContactMail = cpyPocoClient.ContactMail ;
            compagniePoco.ContactPhones = cpyPocoClient.ContactPhones ;
            compagniePoco.smtpServer = cpyPocoClient.smtpServer ;
            compagniePoco.smtpPort = cpyPocoClient.smtpPort ;
            compagniePoco.smtpPassword = cpyPocoClient.smtpPassword ;
            compagniePoco.TPSNumber = cpyPocoClient.TPSNumber ;
            compagniePoco.TVQNumber = cpyPocoClient.TVQNumber ;

            createBillUpdateUsCtr1.FillBillToUpdate(compagniePoco, billHistoryResult);
        }


    }
}
