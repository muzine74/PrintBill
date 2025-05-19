using GDTOSQL.Entity;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Configuration;

namespace GDTOSQL
{
    public class GoogleDriveBase
    {
        List<WorkBillInfo> _workBillInfoList;


        public List<WorkBillInfo> LoadGGDrBillData()
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

            // Define the Google Sheets API version and scope
            string[] scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string applicationName = "Google Drive Connection";

            //Console.WriteLine("Répertoire courant : " + Environment.CurrentDirectory);
            // Accéder aux valeurs de configuration
            var spreadsheetId = configuration["AppSettings:spreadsheetId"];
            var range = configuration["AppSettings:range"];

            // Load the credentials JSON file
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
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



                // The ID of the spreadsheet to read data from
                //string spreadsheetId = "1YZ8gMGqKNJc-Ok9X0BtaHsouDtpzzVPpLDRl_aoGxns";

                // The range of cells to read (e.g., "Sheet1!A1:B10")
                //string range = "January!C4:AY8";

                // Récupérer les métadonnées de la feuille de calcul
                var spreadsheet = service.Spreadsheets.Get(spreadsheetId).Execute();

                if (spreadsheet.Sheets != null)
                {
                    foreach (var sheet in spreadsheet.Sheets)
                    {
                        // _workBillInfo.CompagnyName = sheet[].
                        Console.WriteLine($"Nom de la feuille : {sheet.Properties.Title}" + $"  " + $"ID de la feuille : {sheet.Properties.SheetId}");

                    }
                }
                else
                {
                    Console.WriteLine("Aucune feuille trouvée.");
                }



                // Read the data from the specified range
                SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);
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


                        if (p && tv && tp)
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

                        }
                    }

                }
            }

            return _workBillInfoList;

        }
    }
}