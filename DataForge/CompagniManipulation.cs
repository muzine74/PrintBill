using DataBridge.Entity;
using DBConnection;
using DBConnection.Entity;
using GDTOSQL;
using GDTOSQL.Entity;
using Helpers.generalHelp;
using Helpers.GoogleDrive;
using Microsoft.EntityFrameworkCore;




namespace DataBridge
{
    public class CompagniManipulation
    {
        DbContextOptions<RamssisCleaningContex> options;
        RamssisCleaningContex ramssisCleaningContex;
        CompagniePoco compagniePoco;
        public List<WorkBillInfo> _badeDataList = new List<WorkBillInfo>();
        public CompanyAddress CompagnyAdress = new CompanyAddress();
        List<CompagniePoco> compagnieInfoList = new List<CompagniePoco>();

        public List<Address> AdressList = new List<Address>();
        public List<Company> CompagnyList = new List<Company>();
        public List<Client> ClientList = new List<Client>();


        public CompagniManipulation()
        {
            options = new DbContextOptionsBuilder<RamssisCleaningContex>()
                        .UseSqlServer("Server=DESKTOP-71ON71H\\SQLEXPRESS;Database=RamssisCleaningDB;Trusted_Connection=True;TrustServerCertificate=true;")
          .Options;

            ramssisCleaningContex = new RamssisCleaningContex(options);

            compagniePoco = new CompagniePoco();

        }

        public List<CompagniePoco> GetCompagieInfo()
        {
            List<CompagniePoco> copiedList = new List<CompagniePoco>();

            var compagniesInfoList = (
                from C in ramssisCleaningContex.Companies
                join AC in ramssisCleaningContex.CompanyAddresses on C.CompanyId equals AC.CompanyId
                join A in ramssisCleaningContex.Addresses on AC.AddressId equals A.AddressId
                join CL in ramssisCleaningContex.Clients on C.CompanyId equals CL.CompanyId
                where C.companyStatus == "Active"
                orderby C.companyName

                select new
                {
                    C.CompanyId,
                    C.companyName,
                    C.companyStatus,
                    C.companyCode,
                    A.country,
                    A.state,
                    A.city,
                    A.zipCode,
                    A.suite,
                    A.civicNumber,
                    CL.name,
                    CL.mail,
                    CL.phone,
                    C.prividercode

                }
                ).ToList();

            copiedList = compagniesInfoList.Select(cp => new CompagniePoco
            {
                CompagnieID = cp.CompanyId,
                CompagnieName = cp.companyName,
                CompagnieStatus = cp.companyStatus,
                CompagnieCode = cp.companyCode,
                Compagniecountry = cp.country,
                CompagnieState = cp.state,
                Compagniecity = cp.city,
                CompagnieZipCode = cp.zipCode,
                CompagnieSuite = cp.suite,
                CompagnieCivicNumber = cp.civicNumber,
                ContactName = cp.name,
                ContactMail = cp.mail,
                ContactPhones = cp.phone,
                Compagnieprividercode = cp.prividercode
            }
            ).ToList();


            //enlever les code doublons qui devrais aller dans la meme factuer
            return (copiedList.GroupBy(p => p.CompagnieCode)
            .Select(g => g.First()).ToList());
        }

        public List<CompagniePoco> LinkCompagniePayment(SheetInfo sfo)
        {
            List<CompagniePoco> CompagieInfowithPrice = new List<CompagniePoco>();
            List<WorkBillInfo> GGDrBillDataList = new List<WorkBillInfo>();


            CompagieInfowithPrice = GetCompagieInfo();
            GGDrBillDataList = GetGGDrBillData(sfo);

            for (int i = 0; i < CompagieInfowithPrice.Count; i++)
            {
                if (!string.IsNullOrEmpty(CompagieInfowithPrice[i].CompagnieCode) && !string.IsNullOrEmpty(CompagieInfowithPrice[i].CompagnieName))
                {
                    for (int j = 0; j < GGDrBillDataList.Count; j++)
                    {
                        // teste si les champs sont vide ou egal 0
                        //si information compagny est null ou linformation du prix egal 0 ou vide
                        if (CompagieInfowithPrice[i].CompagnieCode == GGDrBillDataList[j].CompagnyCode)
                        {

                            CompagieInfowithPrice[i]._workBillInfoList.Add(new WorkBillInfo
                            {
                                CompagnyName = GGDrBillDataList[j].CompagnyName,
                                CompagnyCode = GGDrBillDataList[j].CompagnyCode,
                                compagnyPrice = GGDrBillDataList[j].compagnyPrice,
                                NumberOfVisite = GGDrBillDataList[j].NumberOfVisite,
                                Totalprice = GGDrBillDataList[j].Totalprice

                            });
                        }
                    }

                }
                else
                {
                    _badeDataList.Add(new WorkBillInfo
                    {
                        CompagnyName = CompagieInfowithPrice[i].ContactName,
                        CompagnyCode = CompagieInfowithPrice[i].CompagnieCode

                    });

                }


            }

            //_badeDataList.AddRange(googleDrive._badeDataList);

            return CompagieInfowithPrice;
        }

        public List<WorkBillInfo> GetGGDrBillData(SheetInfo sfo)
        {
            GoogleDrive googleDrive = new GoogleDrive();
            return googleDrive.LoadGGDrBillData(sfo);
        }

        public void SaveBillHisrory(List<BillHistoryPoco> _billHistoryPocoLst)
        {
            foreach (var item in _billHistoryPocoLst)
            {
                BillHistory billHistory = new BillHistory();

                billHistory.Id = item.Id;
                //billHistory.billIdentifier = item.billIdentifier;
                billHistory.BillNumber = item.BillNumber;
                billHistory.compagnyName = item.compagnyName;
                billHistory.compagnyCode = item.compagnyCode;
                billHistory.compagnyPrice = item.compagnyPrice;
                billHistory.MouthBill = item.MouthBill;
                billHistory.BilledDate = item.BilledDate;
                billHistory.BillDescription = item.BillDescription;
                billHistory.compagnyPrice = item.compagnyPrice;
                billHistory.NumberOfVisite = item.NumberOfVisite;
                billHistory.TotalWithOutTax = item.TotalWithOutTax;
                billHistory.TPS = item.TPS;
                billHistory.TVQ = item.TVQ;
                billHistory.TotalWithTax = item.TotalWithTax;
                billHistory.BillPath = item.BillPath;
                billHistory.BillHistoryNote = item.BillHistoryNote;


                ramssisCleaningContex.BillHistories.Add(billHistory);
                ramssisCleaningContex.SaveChanges();

            }

            ramssisCleaningContex.SaveChanges();
        }

        public List<BillHistory> GetBill(BillSearchStatus BillSearchStatus)
        {
            var billhs = (from rcc in ramssisCleaningContex.BillHistories
                          select rcc).AsQueryable();

            if (!string.IsNullOrEmpty(BillSearchStatus.keysearch))
            {
                billhs = billhs.Where(s => s.compagnyName.Contains(BillSearchStatus.keysearch) || s.compagnyCode.Contains(BillSearchStatus.keysearch)
                                        || s.BillDescription.Contains(BillSearchStatus.keysearch) || s.BillNumber.ToString().Contains(BillSearchStatus.keysearch)
                                        || s.TotalWithTax.ToString().Contains(BillSearchStatus.keysearch) || s.TotalWithOutTax.ToString().Contains(BillSearchStatus.keysearch)
                                        || s.BillNumber.ToString().Contains(BillSearchStatus.keysearch));
            }

            if (BillSearchStatus == null)
            {
                if (billhs == null)
                {
                    List<BillHistory> bhs = new List<BillHistory>();
                    return bhs;
                }
                else
                {
                    return billhs.ToList();
                }
            }
            else
            {

                if (BillSearchStatus.Sended && BillSearchStatus.NotSended)
                {
                    billhs = billhs.Where(s => s.Issended == true || s.Issended == false);
                }

                if (BillSearchStatus.Sended && !BillSearchStatus.NotSended)
                {
                    billhs = billhs.Where(s => s.Issended == true);
                }

                if (!BillSearchStatus.Sended && BillSearchStatus.NotSended)
                {
                    billhs = billhs.Where(s => s.Issended == false);
                }

                if (!BillSearchStatus.Sended && !BillSearchStatus.NotSended)
                {
                    billhs = billhs.Where(s => s.Issended != true && s.Issended != false);
                }



                if (BillSearchStatus.payed && BillSearchStatus.notPayed)
                {
                    billhs = billhs.Where(s => s.IsPayed == true || s.IsPayed == false);
                }

                if (BillSearchStatus.payed && !BillSearchStatus.notPayed)
                {
                    billhs = billhs.Where(s => s.IsPayed == true);
                }

                if (!BillSearchStatus.payed && BillSearchStatus.notPayed)
                {
                    billhs = billhs.Where(s => s.IsPayed == false);
                }

                if (!BillSearchStatus.payed && !BillSearchStatus.notPayed)
                {
                    billhs = billhs.Where(s => s.IsPayed != true && s.IsPayed != false);
                }



                if (BillSearchStatus.BeginDate != null && BillSearchStatus.EndDate != null)
                {
                    if (BillSearchStatus.BeginDate <= BillSearchStatus.EndDate)
                    {
                        billhs = billhs.Where(d => d.BilledDate >= BillSearchStatus.BeginDate && d.BilledDate <= BillSearchStatus.EndDate);
                    }
                    else
                    {
                        billhs = billhs.Where(d => d.BilledDate <= BillSearchStatus.BeginDate && d.BilledDate >= BillSearchStatus.EndDate);
                    }
                }

                return billhs.ToList();
            }
        }

        public void UpdateBillHistory(string billPath)
        {
            var billToUpdate = ramssisCleaningContex.BillHistories.FirstOrDefault(c => c.BillPath == billPath);

            if (billToUpdate != null)
            {
                billToUpdate.Issended = true;

                ramssisCleaningContex.SaveChanges();

            }

        }

        public int getLastBill()
        {
            return (from rcc in ramssisCleaningContex.BillHistories
                    orderby rcc.billIdentifier descending
                    select rcc.billIdentifier).FirstOrDefault();
        }

        public void SaveCompagnyInfo(CompagniePoco cmpPoco)
        {
            Company CompagnyTemp = new Company();
            Address AdressTemp = new Address();
            Client ClientTemp = new Client();
            CompanyAddress CompagnyAdress = new CompanyAddress();

            Guid CompagnyGuid = Guid.NewGuid();
            Guid AdressGuid = Guid.NewGuid();
            Guid ClientGuid = Guid.NewGuid();

            // Fill Compagny Info
            CompagnyTemp.CompanyId = CompagnyGuid;
            CompagnyTemp.companyName = cmpPoco.CompagnieName;
            CompagnyTemp.companyCode = cmpPoco.CompagnieCode;
            CompagnyTemp.companyStatus = cmpPoco.CompagnieStatus;


            //Fill Address to last Company
            AdressTemp.AddressId = AdressGuid;
            AdressTemp.civicNumber = cmpPoco.CompagnieCivicNumber;
            AdressTemp.suite = cmpPoco.CompagnieSuite;
            AdressTemp.zipCode = cmpPoco.CompagnieZipCode;
            AdressTemp.city = cmpPoco.Compagniecity;
            AdressTemp.state = cmpPoco.CompagnieState;


            //Fill Contact Client to Last Compagny
            ClientTemp.clientID = ClientGuid;
            ClientTemp.name = cmpPoco.ContactName;
            ClientTemp.mail = cmpPoco.ContactMail;
            ClientTemp.phone = cmpPoco.ContactPhones;

            ClientTemp.CompanyId = CompagnyGuid;

            CompagnyAdress.AddressId = AdressGuid;
            CompagnyAdress.CompanyId = CompagnyGuid;
            ramssisCleaningContex.CompanyAddresses.Add(CompagnyAdress);

            ramssisCleaningContex.Companies.Add(CompagnyTemp);
            ramssisCleaningContex.Addresses.Add(AdressTemp);
            ramssisCleaningContex.Clients.Add(ClientTemp);


            ramssisCleaningContex.SaveChanges();

        }

        public List<CompagniePoco> GetCompagnyByName(string searchKey)
        {
            List<CompagniePoco> copiedList = new List<CompagniePoco>();

            var compagniesInfoList = (
                from C in ramssisCleaningContex.Companies
                join AC in ramssisCleaningContex.CompanyAddresses on C.CompanyId equals AC.CompanyId
                join A in ramssisCleaningContex.Addresses on AC.AddressId equals A.AddressId
                join CL in ramssisCleaningContex.Clients on C.CompanyId equals CL.CompanyId
                where C.companyStatus == "Active" && (C.companyName.Contains(searchKey) || C.companyCode.Contains(searchKey))

                select new
                {
                    C.CompanyId,
                    A.AddressId,
                    CL.clientID,
                    C.companyName,
                    C.companyStatus,
                    C.companyCode,
                    C.prividercode,
                    A.country,
                    A.state,
                    A.city,
                    A.zipCode,
                    A.suite,
                    A.civicNumber,
                    CL.name,
                    CL.mail,
                    CL.phone
                }
                ).ToList();

            copiedList = compagniesInfoList.Select(cp => new CompagniePoco
            {
                CompagnieID = cp.CompanyId,
                AddressID = cp.AddressId,
                ContactID = cp.clientID,
                CompagnieName = cp.companyName,
                CompagnieStatus = cp.companyStatus,
                CompagnieCode = cp.companyCode,
                Compagniecountry = cp.country,
                CompagnieState = cp.state,
                Compagniecity = cp.city,
                CompagnieZipCode = cp.zipCode,
                CompagnieSuite = cp.suite,
                CompagnieCivicNumber = cp.civicNumber,
                CompagnieProvider =cp.prividercode,
                ContactName = cp.name,
                ContactMail = cp.mail,
                ContactPhones = cp.phone

            }
            ).ToList();


            //enlever les code doublons qui devrais aller dans la meme factuer
            return (copiedList.GroupBy(p => p.CompagnieCode)
            .Select(g => g.First()).ToList());

        }
        public void UpdateCompagnyInfo(CompagniePoco cmpPocoUp)
        {
            var companie = ramssisCleaningContex.Companies.Where(c => c.CompanyId == cmpPocoUp.CompagnieID).First();
            var Adresse = ramssisCleaningContex.Addresses.Where(c => c.AddressId == cmpPocoUp.AddressID).First();
            var client = ramssisCleaningContex.Clients.Where(c => c.clientID == cmpPocoUp.ContactID).First();



            companie.companyName = cmpPocoUp.CompagnieName;
            companie.companyCode = cmpPocoUp.CompagnieCode;
            companie.companyStatus = cmpPocoUp.CompagnieStatus;
            companie.prividercode = cmpPocoUp.Compagnieprividercode;

            Adresse.country = cmpPocoUp.Compagniecountry;
            Adresse.state = cmpPocoUp.CompagnieState;
            Adresse.city = cmpPocoUp.Compagniecity;
            Adresse.zipCode = cmpPocoUp.CompagnieZipCode;
            Adresse.suite = cmpPocoUp.CompagnieSuite;
            Adresse.civicNumber = cmpPocoUp.CompagnieCivicNumber;

            client.name = cmpPocoUp.ContactName;
            client.mail = cmpPocoUp.ContactMail;
            client.phone = cmpPocoUp.ContactPhones;
            Adresse.country = "Canada";

            ramssisCleaningContex.SaveChanges();
        }

        public List<SheetInfo> GetListgoogleSheet(SheetInfo sheetInfo)
        {
            GoogleDrive googleDrive = new GoogleDrive();

            //return la liste des page relier a un document
            return googleDrive.GetListSpreadSheet(sheetInfo);
        }

        public List<CompagniePoco> getActiveCompagnies()
        {
            List<CompagniePoco> copiedList = new List<CompagniePoco>();

            var cpmList = (
                        from C in ramssisCleaningContex.Companies
                        join AC in ramssisCleaningContex.CompanyAddresses on C.CompanyId equals AC.CompanyId
                        join A in ramssisCleaningContex.Addresses on AC.AddressId equals A.AddressId
                        join CL in ramssisCleaningContex.Clients on C.CompanyId equals CL.CompanyId
                        where C.companyStatus == "Active"

                        select new
                        {
                            C.CompanyId,
                            A.AddressId,
                            CL.clientID,
                            C.companyName,
                            C.companyStatus,
                            C.companyCode,
                            A.country,
                            A.state,
                            A.city,
                            A.zipCode,
                            A.suite,
                            A.civicNumber,
                            CL.name,
                            CL.mail,
                            CL.phone,
                            C.prividercode

                        }
                        ).ToList();

            copiedList = cpmList.Select(cp => new CompagniePoco
            {
                CompagnieID = cp.CompanyId,
                AddressID = cp.AddressId,
                ContactID = cp.clientID,


                CompagnieName = cp.companyName,
                CompagnieStatus = cp.companyStatus,
                CompagnieCode = cp.companyCode,
                Compagniecountry = cp.country,
                CompagnieState = cp.state,
                Compagniecity = cp.city,
                CompagnieZipCode = cp.zipCode,
                CompagnieSuite = cp.suite,
                CompagnieCivicNumber = cp.civicNumber,
                ContactName = cp.name,
                ContactMail = cp.mail,
                ContactPhones = cp.phone,
                Compagnieprividercode = cp.prividercode

            }
                         ).ToList();


            //enlever les code doublons qui devrais aller dans la meme factuer
            return (copiedList.GroupBy(p => p.CompagnieCode)
            .Select(g => g.First()).ToList());

        }

    }
}
