using DataBridge;
using DataBridge.Entity;
using DBConnection;
using DBConnection.Entity;
using Helpers.generalHelp;
using Microsoft.EntityFrameworkCore;
using SendBillBL.Entity;



namespace SendBillBL
{
    public class SendBill
    {
        //DbContextOptions<RamssisCleaningContex> options;
        //RamssisCleaningContex ramssisCleaningContex;

        public CompagniManipulation compagniManipulation = new CompagniManipulation();
        public List<CompagniePoco> CompagieInfoLst;
        public List<BillHistory> BillHistories;
        BillInfo billInfo;
        BillSearchStatus billSearchStatus;

        public SendBill()
        {
            billSearchStatus = new BillSearchStatus();
            CompagieInfoLst = compagniManipulation.GetCompagieInfo();
            BillHistories = compagniManipulation.GetBill(billSearchStatus);
            billInfo = new BillInfo();
            //Parcourire al list des facture et appeler la class send bill
        }

        public bool Send(MailPoco mailPoco)
        {
            return billInfo.SendMail(mailPoco);
        }

        public List<BillHistory> GetBillHistoryList()
        {
            return compagniManipulation.GetBill(billSearchStatus);
        }
    }
}
