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
                where C.companyStatus == true
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
                    C.prividercode,
                    C.PaymentFrequency,
                    C.WorkFrequency
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
                CompagnieProvider = cp.prividercode,
                PaymentFrequency = cp.PaymentFrequency,
                WorkFrequency = cp.WorkFrequency
            }
            ).ToList();


            //enlever les code doublons qui devrais aller dans la meme factuer
            return (copiedList.GroupBy(p => p.CompagnieCode)
            .Select(g => g.First()).OrderBy(c => c.CompagnieName).ToList());
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

        public void SaveBillHisrory(List<BillHistoryPoco> _billHistoryPocoLst, List<BillDescriptionPoco> billDescriptionPocolist)
        {
            BillSearchStatus billSearchStatus = new BillSearchStatus();

            billSearchStatus.keysearch = _billHistoryPocoLst[0].BillNumber;
            ;//_billHistoryPocoLst[0].BillNumber;
             //var bhs = GetBillByBillNumber(billSearchStatus).FirstOrDefault();



            // var bhst = (from rcc in ramssisCleaningContex.BillHistories
            //             select rcc).AsQueryable();


            var bhs = ramssisCleaningContex.BillHistories
                     .Include(b => b.BillDescriptions)
                     .FirstOrDefault(bhs => bhs.BillNumber == billSearchStatus.keysearch);
            //select rcc).Where(s => s.BillNumber.ToString().Contains("")).FirstOrDefault();



            if (bhs == null)
            {
                InsertNewHistoryBill(_billHistoryPocoLst, billDescriptionPocolist);
            }
            else
            {
                UpdatExistingHistoryBill(_billHistoryPocoLst, billDescriptionPocolist, bhs);
            }
        }

        void UpdatExistingHistoryBill(List<BillHistoryPoco> _billHistoryPocoLst, List<BillDescriptionPoco> billDescriptionPocolist, BillHistory bhs)
        {
            //les seul information qui devrais pas etre mis a jours
            //  bhs.Id;bhs.BillNumber
            //  bhs.billIdentifier;
            // bhs.BillPath;
            bhs.compagnyName = _billHistoryPocoLst[0].compagnyName;
            bhs.compagnyCode = _billHistoryPocoLst[0].compagnyCode;
            bhs.MouthBill = _billHistoryPocoLst[0].MouthBill;
            bhs.BilledDate = _billHistoryPocoLst[0].BilledDate;
            bhs.BillDescriptionText = _billHistoryPocoLst[0].BillDescriptionText;
            bhs.compagnyPrice = _billHistoryPocoLst[0].compagnyPrice;
            bhs.NumberOfVisite = _billHistoryPocoLst[0].NumberOfVisite;
            bhs.TotalWithOutTax = _billHistoryPocoLst[0].TotalWithOutTax;
            bhs.TPS = _billHistoryPocoLst[0].TPS;
            bhs.TVQ = _billHistoryPocoLst[0].TVQ;
            bhs.TotalWithTax = _billHistoryPocoLst[0].TotalWithTax;
            bhs.BillHistoryNote = _billHistoryPocoLst[0].BillHistoryNote;
            bhs.Issended = _billHistoryPocoLst[0].Issended;
            bhs.IsPayed = _billHistoryPocoLst[0].IsPayed;


            var toRemove = bhs.BillDescriptions
                            .Where(d => !billDescriptionPocolist
                            .Any(p => p.BillDescriptionPocoId == d.BillDescriptionId))
                            .ToList();

            ramssisCleaningContex.BillDescriptions.RemoveRange(toRemove);

            foreach (var itr in billDescriptionPocolist)
            {
                var existingDesc = bhs.BillDescriptions
                    .FirstOrDefault(d => d.BillDescriptionId == itr.BillDescriptionPocoId);

                if (existingDesc != null)
                {
                    // UPDATE
                    existingDesc.Quantity = itr.QuantityPoco;
                    existingDesc.Description = itr.DescriptionPoco;
                    existingDesc.UnitPrice = itr.UnitPricePoco;
                    existingDesc.SubTotalPrice = itr.SubTotalPricePoco;
                }
                else
                {
                    // INSERT
                    bhs.BillDescriptions.Add(new BillDescription
                    {
                        Quantity = itr.QuantityPoco,
                        Description = itr.DescriptionPoco,
                        UnitPrice = itr.UnitPricePoco,
                        SubTotalPrice = itr.SubTotalPricePoco,
                        BillHistoris = bhs
                    });
                }
            }

            ramssisCleaningContex.SaveChanges();
        }


        private void InsertNewHistoryBill(List<BillHistoryPoco> _billHistoryPocoLst, List<BillDescriptionPoco> billDescriptionPocolist)
        {
            List<BillDescription> billDescriptionlist = new List<BillDescription>();
            //List<BillDescriptionPoco> billDescriptionPocolist = new List<BillDescriptionPoco>();

            foreach (var item in _billHistoryPocoLst)
            {
                BillHistory billHistory = new BillHistory();

                billHistory.Id = item.Id;
                // billHistory.billIdentifier = item.billIdentifier;
                billHistory.BillNumber = item.BillNumber;
                billHistory.compagnyName = item.compagnyName;
                billHistory.compagnyCode = item.compagnyCode;
                billHistory.compagnyPrice = item.compagnyPrice;
                billHistory.MouthBill = item.MouthBill;
                billHistory.BilledDate = item.BilledDate;
                billHistory.compagnyPrice = item.compagnyPrice;
                billHistory.NumberOfVisite = item.NumberOfVisite;
                billHistory.TotalWithOutTax = item.TotalWithOutTax;
                billHistory.TPS = item.TPS;
                billHistory.TVQ = item.TVQ;
                billHistory.TotalWithTax = item.TotalWithTax;
                billHistory.BillPath = item.BillPath;
                billHistory.BillHistoryNote = item.BillHistoryNote;

                billHistory.BillDescriptions = new List<BillDescription>();

                foreach (var itr in billDescriptionPocolist)
                {

                    billHistory.BillDescriptions.Add(new BillDescription
                    {
                        BillDescriptionId = itr.BillDescriptionPocoId, // Corrected property assignment
                        Quantity = itr.QuantityPoco,
                        Description = itr.DescriptionPoco,
                        UnitPrice = itr.UnitPricePoco,
                        SubTotalPrice = itr.SubTotalPricePoco,
                        BillHistoryId = itr.BillHistoryIdPoco,
                        BillHistoris = billHistory

                    });
                }

                ramssisCleaningContex.BillHistories.Add(billHistory);
                ramssisCleaningContex.BillDescriptions.AddRange(billHistory.BillDescriptions);

            }

            //string logChanges = "";

            //foreach (var entry in ramssisCleaningContex.ChangeTracker.Entries())
            //{
            //    logChanges +=  $"{entry.Entity.GetType().Name} - {entry.State}"; 
            //    logChanges += Environment.NewLine;
            //}

            ramssisCleaningContex.SaveChanges();
            billDescriptionPocolist.Clear();
            _billHistoryPocoLst.Clear();

        }



        public List<BillDescription> GetBilDescription(string searchKey)
        {

            var billDescriptions = (from bdsc in ramssisCleaningContex.BillDescriptions
                                    where bdsc.BillHistoryId.Equals(searchKey)
                                    select bdsc).AsQueryable();

            return billDescriptions.ToList();
        }

        public List<BillHistory> GetBill(BillSearchStatus BillSearchStatus)
        {
            var billhs = (from rcc in ramssisCleaningContex.BillHistories
                          select rcc).AsQueryable();

            if (!string.IsNullOrEmpty(BillSearchStatus.keysearch))
            {
                billhs = billhs.Where(s => s.compagnyName.Contains(BillSearchStatus.keysearch) || s.compagnyCode.Contains(BillSearchStatus.keysearch)
                                        || s.BillNumber.ToString().Contains(BillSearchStatus.keysearch) //s.BillDescription.Contains(BillSearchStatus.keysearch) ||
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

            CompagnyTemp.WorkTypeId = GetWorkTypeId(cmpPoco.WorkFrequency); //default work type id
            DesactivateOldWorkType(cmpPoco.CompagnieID);


            // Fill Compagny Info
            CompagnyTemp.CompanyId = cmpPoco.CompagnieID;
            CompagnyTemp.companyName = cmpPoco.CompagnieName;
            CompagnyTemp.companyCode = cmpPoco.CompagnieCode;
            CompagnyTemp.companyStatus = cmpPoco.CompagnieStatus;
            CompagnyTemp.prividercode = cmpPoco.CompagnieProvider;
            CompagnyTemp.TPSNumber = cmpPoco.TPSNumber;
            CompagnyTemp.TVQNumber = cmpPoco.TVQNumber;
            CompagnyTemp.PaymentFrequency = cmpPoco.PaymentFrequency;
            CompagnyTemp.WorkFrequency = cmpPoco.WorkFrequency;


            //Fill Address to last Company
            AdressTemp.AddressId = cmpPoco.AddressID;
            AdressTemp.civicNumber = cmpPoco.CompagnieCivicNumber;
            AdressTemp.suite = cmpPoco.CompagnieSuite;
            AdressTemp.zipCode = cmpPoco.CompagnieZipCode;
            AdressTemp.city = cmpPoco.Compagniecity;
            AdressTemp.state = cmpPoco.CompagnieState;


            //Fill Contact Client to Last Compagny
            ClientTemp.clientID = cmpPoco.CompagnieID;
            ClientTemp.name = cmpPoco.ContactName;
            ClientTemp.mail = cmpPoco.ContactMail;
            ClientTemp.phone = cmpPoco.ContactPhones;


            ClientTemp.CompanyId = cmpPoco.CompagnieID;

            CompagnyAdress.AddressId = cmpPoco.AddressID;
            CompagnyAdress.CompanyId = cmpPoco.CompagnieID;
            ramssisCleaningContex.CompanyAddresses.Add(CompagnyAdress);

            CompagnyTemp.CompanyPricingCalendars = cmpPoco._companyPricingCalendar;



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
                where (C.companyName.Contains(searchKey) || C.companyCode.Contains(searchKey))

                select new
                {
                    C.CompanyId,
                    A.AddressId,
                    CL.clientID,
                    C.companyName,
                    C.companyStatus,
                    C.companyCode,
                    C.prividercode,
                    C.TPSNumber,
                    C.TVQNumber,
                    C.PaymentFrequency,
                    C.WorkFrequency,
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
                CompagnieProvider = cp.prividercode,
                TPSNumber = cp.TPSNumber,
                TVQNumber = cp.TVQNumber,
                ContactName = cp.name,
                ContactMail = cp.mail,
                ContactPhones = cp.phone,
                PaymentFrequency = cp.PaymentFrequency,
                WorkFrequency = cp.WorkFrequency,


            }
            ).ToList();


            //enlever les code doublons qui devrais aller dans la meme factuer
            return (copiedList.GroupBy(p => p.CompagnieCode)
            .Select(g => g.First()).ToList());

        }

        public CompagniePoco GetCompagnyByCode(string cpCode)
        {
            var result =
                (from c in ramssisCleaningContex.Companies
                 join ac in ramssisCleaningContex.CompanyAddresses on c.CompanyId equals ac.CompanyId
                 join a in ramssisCleaningContex.Addresses on ac.AddressId equals a.AddressId
                 join cl in ramssisCleaningContex.Clients on c.CompanyId equals cl.CompanyId
                 where c.companyStatus == true
                       && c.companyCode == cpCode
                 select new CompagniePoco
                 {
                     CompagnieID = c.CompanyId,
                     AddressID = a.AddressId,
                     ContactID = cl.clientID,
                     CompagnieName = c.companyName,
                     CompagnieStatus = c.companyStatus,
                     CompagnieCode = c.companyCode,
                     Compagniecountry = a.country,
                     CompagnieState = a.state,
                     Compagniecity = a.city,
                     CompagnieZipCode = a.zipCode,
                     CompagnieSuite = a.suite,
                     CompagnieCivicNumber = a.civicNumber,
                     CompagnieProvider = c.prividercode,
                     PaymentFrequency = c.PaymentFrequency,
                     WorkFrequency = c.WorkFrequency,
                     TPSNumber = c.TPSNumber,
                     TVQNumber = c.TVQNumber,
                     ContactName = cl.name,
                     ContactMail = cl.mail,
                     ContactPhones = cl.phone,


                     smtpServer = c.MailCredential.smtpServer,
                     smtpPort = c.MailCredential.smtpPort,
                     smtpUsername = c.MailCredential.smtpUsername,
                     smtpPassword = c.MailCredential.smtpPassword,
                 }).ToList().GroupBy(p => p.CompagnieCode)
            .Select(g => g.First()).FirstOrDefault();

            // 🔥 Si le résultat est null → retourner un objet vide (jamais null)
            return result ?? new CompagniePoco();
        }


        public void UpdateCompagnyInfo(CompagniePoco cmpPocoUp)
        {
            var companie = ramssisCleaningContex.Companies
                            .Include(c => c.CompanyPricingCalendars)
                            .Where(c => c.CompanyId == cmpPocoUp.CompagnieID)
                            .First();
            var Adresse = ramssisCleaningContex.Addresses
                            .Where(c => c.AddressId == cmpPocoUp.AddressID)
                            .First();
            var client = ramssisCleaningContex.Clients.Where(c => c.clientID == cmpPocoUp.ContactID).First();



            companie.companyName = cmpPocoUp.CompagnieName;
            companie.companyCode = cmpPocoUp.CompagnieCode;
            companie.companyStatus = cmpPocoUp.CompagnieStatus;
            companie.prividercode = cmpPocoUp.CompagnieProvider;
            companie.PaymentFrequency = cmpPocoUp.PaymentFrequency;
            companie.WorkFrequency = cmpPocoUp.WorkFrequency;
            //companie.CompanyPricingCalendars = cmpPocoUp._companyPricingCalendar;

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


            List<CompanyPricingCalendarPoco> CpcPOldList = new List<CompanyPricingCalendarPoco>();
            List<CompanyPricingCalendarPoco> CpcPNewList = new List<CompanyPricingCalendarPoco>();


            CpcPOldList = companie.CompanyPricingCalendars
                .Where(x => x.IsActive == true)
                .Select(x => 
                    new CompanyPricingCalendarPoco
                    {
                        CompanyPricingCalendarId = x.CompanyPricingCalendarId,
                        Days = x.Days,
                        DaysStatus = x.DaysStatus,
                        CopagnyBenifictPrice = x.CopagnyBenifictPrice,
                        Emplyeepaiment = x.Emplyeepaiment,
                        IsActive = x.IsActive
                    }
                ).ToList();

            // ÉTAPE 1: Mettre à jour une propriété sur TOUS les calendriers existants
            if (companie.CompanyPricingCalendars != null && companie.CompanyPricingCalendars.Any())
            {
                //foreach (var existingCalendar in companie.CompanyPricingCalendars)
                //{
                //    // Mettre à jour la propriété spécifique
                //    // Exemple: mettre à jour le statut ou une autre propriété
                //    //existingCalendar.Status = "Updated"; // Remplacez par la propriété réelle
                //      existingCalendar.IsActive = false;
                //                                         // Ou: existingCalendar.LastModified = DateTime.Now;

                //    // Vous pouvez mettre à jour plusieurs propriétés si nécessaire
                //    // existingCalendar.SomeProperty = newValue;
                //}
            }

            // ÉTAPE 2: Réinsérer la nouvelle liste (ajouter de nouveaux)
            // Supposons que cmpPocoUp._companyPricingCalendar est la nouvelle liste
            if (cmpPocoUp._companyPricingCalendar != null && cmpPocoUp._companyPricingCalendar.Any())
            {
                companie.CompanyPricingCalendars.ToList().ForEach(item => item.IsActive = false);

                foreach (var newCalendar in cmpPocoUp._companyPricingCalendar)
                {
                    // Vérifier si ce calendrier existe déjà (basé sur une clé unique)
                    // Si vous avez un identifiant unique comme CalendarId ou une combinaison de propriétés
                    //var exists = companie.CompanyPricingCalendars
                    //    .Any(cpc => cpc.CompanyPricingCalendarId == newCalendar.CompanyPricingCalendarId); // Adaptez avec votre clé

                    //if (!exists)
                    //{
                    // Ajouter comme nouveau
                    newCalendar.CompanyId = companie.CompanyId; // S'assurer de la relation
                    companie.CompanyPricingCalendars.Add(newCalendar);
                    //}
                    //else
                    //{

                    //    // Optionnel: Mettre à jour complètement l'existant si trouvé
                    //    var existing = companie.CompanyPricingCalendars
                    //        .First(cpc => cpc.CompanyPricingCalendarId == newCalendar.CompanyPricingCalendarId);

                    //    existing.IsActive = false;
                    //    // ramssisCleaningContex.Entry(existing).CurrentValues.SetValues(newCalendar);
                    //}
                }
            }

            ramssisCleaningContex.SaveChanges();


            CpcPNewList = companie.CompanyPricingCalendars
                .Where(x => x.IsActive == true)
                .Select(x =>
                    new CompanyPricingCalendarPoco
                    {
                        CompanyPricingCalendarId = x.CompanyPricingCalendarId,
                        Days = x.Days,
                        DaysStatus = x.DaysStatus,
                        CopagnyBenifictPrice = x.CopagnyBenifictPrice,
                        Emplyeepaiment = x.Emplyeepaiment,
                        IsActive = x.IsActive
                    }
                ).ToList();




            List<(Guid OldId, Guid NewId)> idsPairs = CpcPOldList.Join(
                CpcPNewList,
                o => o.Days,
                n => n.Days,
                (oldItem, newItem) => (OldId: oldItem.CompanyPricingCalendarId, NewId: newItem.CompanyPricingCalendarId)
            ).ToList();


            UpdateEmployeeCompagnyPricings(idsPairs);
        }

        private void UpdateEmployeeCompagnyPricings(List<(Guid OldId, Guid NewId)> idsPairs)
        {
            foreach (var (OldId, NewId) in idsPairs)
            {
                var empCompagnyPricings = ramssisCleaningContex.EmployeeCompagnyPricings
                    .Where(ecp => ecp.CompanyPricingCalendarId == OldId)
                    .ToList();
                foreach (var ecp in empCompagnyPricings)
                {
                    ecp.CompanyPricingCalendarId = NewId;
                }
            }
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
                        where C.companyStatus == true

                        select new
                        {
                            C.CompanyId,
                            A.AddressId,
                            CL.clientID,
                            C.companyName,
                            C.companyStatus,
                            C.companyCode,
                            C.prividercode,
                            C.PaymentFrequency,
                            C.WorkFrequency,
                            A.country,
                            A.state,
                            A.city,
                            A.zipCode,
                            A.suite,
                            A.civicNumber,
                            CL.name,
                            CL.mail,
                            CL.phone,
                            //C.TaxCredential.TVQNumber,
                            //C.TaxCredential.TPSNumber,
                            C.TPSNumber,
                            C.TVQNumber,
                            C.MailCredential.smtpUsername,
                            C.MailCredential.smtpPassword,
                            C.MailCredential.smtpServer,
                            C.MailCredential.smtpPort
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
                CompagnieProvider = cp.prividercode,

                Compagniecountry = cp.country,
                CompagnieState = cp.state,
                Compagniecity = cp.city,
                CompagnieZipCode = cp.zipCode,
                CompagnieSuite = cp.suite,
                CompagnieCivicNumber = cp.civicNumber,
                PaymentFrequency = cp.PaymentFrequency,
                WorkFrequency = cp.WorkFrequency,
                TPSNumber = cp.TPSNumber,
                TVQNumber = cp.TVQNumber,

                ContactName = cp.name,
                ContactMail = cp.mail,
                ContactPhones = cp.phone,

                smtpUsername = cp.smtpUsername,
                smtpPassword = cp.smtpPassword,
                smtpServer = cp.smtpServer,
                smtpPort = cp.smtpPort
            }
            ).ToList();


            //enlever les code doublons qui devrais aller dans la meme factuer
            return (copiedList.GroupBy(p => p.CompagnieCode)
            .Select(g => g.First()).OrderBy(c => c.CompagnieName).ToList());

        }

        public List<BillHistory> GetBillByBillNumber(BillSearchStatus BillSearchStatus)
        {

            if (BillSearchStatus == null)
                return new List<BillHistory>();


            var billhs = (from rcc in ramssisCleaningContex.BillHistories
                          select rcc).AsQueryable();

            if (!string.IsNullOrEmpty(BillSearchStatus.keysearch))
            {
                billhs = billhs.Where(s => s.BillNumber.ToString().Contains(BillSearchStatus.keysearch));
            }

            if (billhs == null)
            {
                List<BillHistory> bhs = new List<BillHistory>();
                return bhs;
            }

            return billhs.ToList();
        }

        public void DesactivateOldWorkType(Guid _CompagnyId)
        {
            // Corrected the LINQ query to properly filter and retrieve the work types
            var workTypes = ramssisCleaningContex.CompanyPricingCalendars
                .Where(wt => wt.CompanyId.Equals(_CompagnyId))
                .ToList();


            foreach (var wt in workTypes)
            {
                wt.IsActive = false;
            }

            ramssisCleaningContex.SaveChanges();
        }
        private int GetWorkTypeId(string workType)
        {
            int workTypeId = ramssisCleaningContex.WorkTypes
                .Where(wt => wt.Name == workType)
                .Select(wt => wt.WorkTypeId)
                .FirstOrDefault();
            return workTypeId;
        }

        public List<CompanyPricingCalendarPoco> GetCompanyPricingCalendars(Guid CompanyId)
        {
            List<CompanyPricingCalendarPoco> companyPricingCalendarPocoLSt = new List<CompanyPricingCalendarPoco>();

            List<CompanyPricingCalendar> companyPricingCalendarLst = ramssisCleaningContex.CompanyPricingCalendars
                .Where(cpc => cpc.CompanyId == CompanyId && cpc.IsActive == true)
                .ToList();

            foreach (var wt in companyPricingCalendarLst)
            {
                companyPricingCalendarPocoLSt.Add(new CompanyPricingCalendarPoco
                {
                    CompanyPricingCalendarId = wt.CompanyPricingCalendarId,
                    Days = wt.Days,
                    DaysStatus = wt.DaysStatus,
                    CopagnyBenifictPrice = wt.CopagnyBenifictPrice,
                    Emplyeepaiment = wt.Emplyeepaiment,
                    IsActive = wt.IsActive

                });
            }

            return companyPricingCalendarPocoLSt;


        }

        public List<CompagniePoco> GetCompagnyByEmployee(Guid EmployeeId)
        {
            var companiesQuery =
                from company in ramssisCleaningContex.Companies
                join companyAddress in ramssisCleaningContex.CompanyAddresses
                    on company.CompanyId equals companyAddress.CompanyId
                join address in ramssisCleaningContex.Addresses
                    on companyAddress.AddressId equals address.AddressId
                join client in ramssisCleaningContex.Clients
                    on company.CompanyId equals client.CompanyId
                join employeeCompany in ramssisCleaningContex.EmployeeCompanies
                on company.CompanyId equals employeeCompany.CompanyId
                where employeeCompany.EmployeeId == EmployeeId  //&& company.companyStatus == true
                //   || company.CompanyCode.Contains(searchKey)
                select new CompanyInfoDto
                {
                    CompanyId = company.CompanyId,
                    AddressId = address.AddressId,
                    ClientId = client.clientID,

                    CompanyName = company.companyName,
                    CompanyStatus = company.companyStatus,
                    CompanyCode = company.companyCode,
                    ProviderCode = company.prividercode,
                    TPSNumber = company.TPSNumber,
                    TVQNumber = company.TVQNumber,
                    PaymentFrequency = company.PaymentFrequency,
                    WorkFrequency = company.WorkFrequency,
                    Country = address.country,
                    State = address.state,
                    City = address.city,
                    ZipCode = address.zipCode,
                    Suite = address.suite,
                    CivicNumber = address.civicNumber,
                    ContactName = client.name,
                    ContactEmail = client.mail,
                    ContactPhone = client.phone
                };

            var companies = companiesQuery.ToList();

            return companies.Select(MapToPoco).ToList();


        }

        private CompagniePoco MapToPoco(CompanyInfoDto dto)
        {
            return new CompagniePoco
            {
                CompagnieID = dto.CompanyId,
                AddressID = dto.AddressId,
                ContactID = dto.ClientId,
                CompagnieName = dto.CompanyName,
                CompagnieStatus = dto.CompanyStatus,
                CompagnieCode = dto.CompanyCode,
                Compagniecountry = dto.Country,
                CompagnieState = dto.State,
                Compagniecity = dto.City,
                CompagnieZipCode = dto.ZipCode,
                CompagnieSuite = dto.Suite,
                CompagnieCivicNumber = dto.CivicNumber,
                CompagnieProvider = dto.ProviderCode,
                TPSNumber = dto.TPSNumber,
                TVQNumber = dto.TVQNumber,
                ContactName = dto.ContactName,
                ContactMail = dto.ContactEmail,
                ContactPhones = dto.ContactPhone,
                PaymentFrequency = dto.PaymentFrequency,
                WorkFrequency = dto.WorkFrequency
            };
        }

        public void SaveEmployeePriceChanged(Dictionary<(Guid CompanyPricingCalendarId, Guid EmployeeId, string Day), decimal> EmployeePriceChanged)
        {
               foreach (var change in EmployeePriceChanged)
                {

                EmployeeCompagnyPricing record = ramssisCleaningContex.EmployeeCompagnyPricings
                            .FirstOrDefault(r => r.CompanyPricingCalendarId == change.Key.CompanyPricingCalendarId
                            && r.EmployeeId == change.Key.EmployeeId
                            );


                    if (record != null)
                    {
                         record.IsActive = false;
                        // record.ModifiedDate = DateTime.Now;
                    }
                    //else
                    //{
                        EmployeeCompagnyPricing employeeCompagnyPricing = new EmployeeCompagnyPricing
                        {
                            CompanyPricingCalendarId = change.Key.CompanyPricingCalendarId,
                            EmployeeId = change.Key.EmployeeId,
                            EmplyeePaiment = change.Value,
                            IsActive = true,
                            ApplicatedDate = DateTime.Now
                        };

                        ramssisCleaningContex.EmployeeCompagnyPricings.Add(employeeCompagnyPricing);
                    //}              
                }
            ramssisCleaningContex.SaveChangesAsync();
        }
    }
}
