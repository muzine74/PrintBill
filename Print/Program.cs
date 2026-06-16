
using DataBridge;
using DataBridge.Entity;
using DBConnection;
using DBConnection.Entity;
using GDTOSQL;
using Microsoft.EntityFrameworkCore;
using PrintPDF;
using System;
using System.IO;
using static iTextSharp.text.pdf.AcroFields;


PrintPDFBIll _printPDFBIll;

//GoogleDrive _GoogleDrive = new GoogleDrive();

//_GoogleDrive.LoadGGDrBillData();


CompagniManipulation compagniManipulation = new CompagniManipulation();
List<CompagniePoco> compagnieInfoList = new List<CompagniePoco>();
//compagnieInfoList = compagniManipulation.LinkCompagniePayment();
List<BillHistoryPoco> _billHistoryPocoLst = new List<BillHistoryPoco>();
List<BillDescriptionPoco> billDescriptionlist = new List<BillDescriptionPoco>();
float sumPrice = 0;
int nbrCompagny = 0;
int Billidentifier = 0;


Billidentifier = compagniManipulation.getLastBill();

if (Billidentifier == 0)
{
    Billidentifier = 1;
}

//Billidentifier = 100;

DateTime date = DateTime.Now;

foreach (var compagnieInfo in compagnieInfoList)
{


    _printPDFBIll = new PrintPDFBIll(compagnieInfo, compagnieInfo);




    string dest = @"C:\Users\Administrator\Desktop\projet Facture\Print\PrintPDF\Facture\" + compagnieInfo.CompagnieCode + "_" + date.ToString("yyMMdd") + "_" + Billidentifier.ToString() + ".pdf";
    sumPrice = 0;
    nbrCompagny = 0;

    BillHistoryPoco billHistoryPoco = new BillHistoryPoco();
    //fill object to Bill histryPoco
    billHistoryPoco.Id = Guid.NewGuid();
    billHistoryPoco.billIdentifier = Billidentifier;
    billHistoryPoco.compagnyName = compagnieInfo.CompagnieName;
    billHistoryPoco.compagnyCode = compagnieInfo.CompagnieCode;
    billHistoryPoco.MouthBill = "Fevrier";
    billHistoryPoco.BilledDate = date;
    billHistoryPoco.BillNumber = date.ToString("yyMMdd");



    string descriptionToSaveHistory = "";

    foreach (var item in compagnieInfo._workBillInfoList)
    {
        billDescriptionlist.Add(new BillDescriptionPoco
        {
            BillDescriptionPocoId = Guid.NewGuid(),
            DescriptionPoco = "[  ||CompagnyName : " + item.CompagnyName + "  ||NumberOfVisite : " + item.NumberOfVisite + "  ||compagnyPrice : " + item.compagnyPrice ,
            BillHistoryIdPoco = billHistoryPoco.billIdentifier
        });

        descriptionToSaveHistory += "`[  ||CompagnyName : " + item.CompagnyName + "  ||NumberOfVisite : " + item.NumberOfVisite + "  ||compagnyPrice : " + item.compagnyPrice +
                    Environment.NewLine;
        sumPrice = sumPrice + item.Totalprice;
        nbrCompagny = nbrCompagny + 1 ;

    }



   // billHistoryPoco.BillDescription = descriptionToSaveHistory;
    billHistoryPoco.TotalWithOutTax = sumPrice;
    billHistoryPoco.TPS = (sumPrice * 0.05f);
    billHistoryPoco.TVQ = (sumPrice * 0.0975f);
    billHistoryPoco.TotalWithTax = (sumPrice + sumPrice * 0.05f + sumPrice * 0.09975f);
    billHistoryPoco.BillPath = dest;
    billHistoryPoco.BillHistoryNote = "";


    if (sumPrice != 0 && nbrCompagny != 0 && !string.IsNullOrEmpty(descriptionToSaveHistory))
    {
        _billHistoryPocoLst.Add(billHistoryPoco);

    }
    else
    {

    }

    Billidentifier = Billidentifier + 1;
}


compagniManipulation.SaveBillHisrory(_billHistoryPocoLst, billDescriptionlist);





foreach (var compagnieInfo in compagniManipulation._badeDataList)
{
    Console.WriteLine(compagnieInfo.CompagnyName);
    Console.WriteLine(compagnieInfo.CompagnyCode);
    Console.WriteLine(compagnieInfo.compagnyPrice);
    Console.WriteLine(compagnieInfo);
    Console.WriteLine(compagnieInfo.CompagnyName);
    Console.WriteLine(compagnieInfo.CompagnyName);
}







var t = "";