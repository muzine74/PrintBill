using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using GDTOSQL.Entity;
using Helpers.GoogleDrive;


namespace GDTOSQL
{
    public class GoogleDrive
    {
        List<WorkBillInfo> _workBillInfoList;
        public List<WorkBillInfo> _badeDataList;

        IConfigurationRoot configuration;

        public GoogleDrive()
        {
            _workBillInfoList = new List<WorkBillInfo>();
            _badeDataList = new List<WorkBillInfo>();
            configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("C:\\Users\\Administrator\\Desktop\\projet Facture\\Print\\GDTOSQL\\appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

            //LoadGGDrBillData();
        }

        static bool TryConvertCurrencyToInt(string currencyValue, out int result)
        {
            // Supprimez le symbole de devise et les espaces (séparateurs de milliers)
            string cleanedValue = currencyValue
                .Replace("$", "") // Supprime le symbole de devise
                .Replace(" ", ""); // Supprime les espaces (séparateurs de milliers)

            // Essayez de convertir en entier
            return int.TryParse(cleanedValue, out result);
        }

        //fonction qui retourne list des compagny avec les prix travailler dans chacunes et le nombre de visites
        public List<WorkBillInfo> LoadGGDrBillData(SheetInfo sfo)
        {

            // Define the Google Sheets API version and scope
            string[] scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string applicationName = "Google Drive Connection";

            // Load the credentials JSON file
            using var stream = new FileStream("C:\\Users\\Administrator\\Desktop\\projet Facture\\Print\\client_secret.json", FileMode.Open, FileAccess.Read);
            
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None).Result;

                // Create the Google Sheets service
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });


            // Read the data from the specified range
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(sfo.spreadsheetId, sfo.SheetTitle + "!C4:AY8");
                ValueRange response = request.Execute();
                IList<IList<object>> values = response.Values;

                if (values != null && values.Count > 0)
                {
                    string _price;
                    string _totalVisite;
                    string _totalprices;

                    int _iprice;
                    int _itotalVisite;
                    int _itotalprices;


                    for (int i = 0; i < values[0].Count; i++)
                    {
                        WorkBillInfo _workBillInfo = new WorkBillInfo();


                        _price = values[2][i].ToString();
                        _totalVisite = values[3][i].ToString();
                        _totalprices = values[4][i].ToString();

                        bool p = TryConvertCurrencyToInt(_price, out _iprice);
                        bool tv = TryConvertCurrencyToInt(_totalVisite, out _itotalVisite);
                        bool tp = TryConvertCurrencyToInt(_totalprices, out _itotalprices);

                        if (p && tv && tp && !string.IsNullOrEmpty(values[0][i].ToString()) && !string.IsNullOrEmpty(values[1][i].ToString()) && _price != "0" && _totalVisite != "0" && _totalprices != "0")
                        {
                            _workBillInfo.CompagnyName = values[0][i].ToString();
                            _workBillInfo.CompagnyCode = values[1][i].ToString();
                            _workBillInfo.compagnyPrice = _iprice;
                            _workBillInfo.NumberOfVisite = _itotalVisite;
                            _workBillInfo.Totalprice = _itotalprices;

                            _workBillInfoList.Add(_workBillInfo);
                        }
                        else
                        {
                            _workBillInfo.CompagnyName = values[0][i].ToString();
                            _workBillInfo.CompagnyCode = values[1][i].ToString();
                            _workBillInfo.compagnyPrice = _iprice;
                            _workBillInfo.NumberOfVisite = _itotalVisite;
                            _workBillInfo.Totalprice = _itotalprices;

                            _badeDataList.Add(_workBillInfo);

                        }
                    }
                }
            return _workBillInfoList;
        }
        public List<SheetInfo> GetListSpreadSheet(SheetInfo sfo)
        {
            SheetInfo sheetInfo;
            List<SheetInfo> SheetList = new List<SheetInfo>();    


            // Define the Google Sheets API version and scope
            
                string[] scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string applicationName = "Google Drive Connection";

            var spreadsheetId = sfo.spreadsheetId;

            // Load the credentials JSON file
            using var stream = new FileStream("C:\\Users\\Administrator\\Desktop\\projet Facture\\Print\\client_secret.json", FileMode.Open, FileAccess.Read);

            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                scopes,
                "user",
                CancellationToken.None).Result;

            // Create the Google Sheets service
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });

            // Récupérer les métadonnées de la feuille de calcul
            var spreadsheet = service.Spreadsheets.Get(spreadsheetId).Execute();

            if (spreadsheet.Sheets != null)
            {
                foreach (var sheet in spreadsheet.Sheets)
                {
                    sheetInfo = new SheetInfo();


                    sheetInfo.SheetId = sheet.Properties.SheetId.ToString();
                    sheetInfo.SheetTitle = sheet.Properties.Title.ToString();
                    SheetList.Add(sheetInfo);

                }
            }
            else
            {
                Console.WriteLine("Aucune feuille trouvée.");
            }
            return SheetList;

        }



    }
}
