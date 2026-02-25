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
        private SheetsService _sheetsService;

        IConfigurationRoot configuration;

        public GoogleDrive()
        {
            _workBillInfoList = new List<WorkBillInfo>();
            _badeDataList = new List<WorkBillInfo>();
            configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("C:\\Users\\Administrator\\Desktop\\projet Facture\\Print\\GDTOSQL\\appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

            _sheetsService = GetGDConnection();
        }

        private SheetsService GetGDConnection()
        {

            FileStream stream;
            SheetsService service;

            // Define the Google Sheets API version and scope            
            string[] scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string applicationName = "Google Drive Connection";
            stream = new FileStream("C:\\Users\\Administrator\\Desktop\\projet Facture\\Print\\client_secret.json", FileMode.Open, FileAccess.Read);
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                scopes,
                "user",
                CancellationToken.None).Result;

            // Create the Google Sheets service
            return service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
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

            

            // Read the data from the specified range
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    _sheetsService.Spreadsheets.Values.Get(sfo.spreadsheetId, sfo.SheetTitle + "!C4:AY8");
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
            var spreadsheetId = sfo.spreadsheetId;            

            // Récupérer les métadonnées de la feuille de calcul
            var spreadsheet = _sheetsService.Spreadsheets.Get(spreadsheetId).Execute();

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



        // Méthode pour créer un nouveau spreadsheet
        public string CreateNewSpreadsheet(string title)
        {
            var spreadsheet = new Spreadsheet
            {
                Properties = new SpreadsheetProperties
                {
                    Title = title
                }
            };

            var request = _sheetsService.Spreadsheets.Create(spreadsheet);
            var response = request.Execute();

            Console.WriteLine($"Spreadsheet créé avec l'ID: {response.SpreadsheetId}");
            Console.WriteLine($"URL: {response.SpreadsheetUrl}");

            return response.SpreadsheetId;
        }

        // Méthode pour ajouter une validation de données (dropdown/calendrier)
        public void AddDataValidationWithCalendar(string spreadsheetId, string sheetName, int columnIndex, int startRow, int endRow)
        {
            // Récupérer l'ID de la feuille
            var spreadsheet = _sheetsService.Spreadsheets.Get(spreadsheetId).Execute();
            int sheetId = GetSheetId(spreadsheet, sheetName);

            var requests = new List<Request>();

            // 1. Ajouter un dropdown avec validation de date (calendrier)
            requests.Add(new Request
            {
                SetDataValidation = new SetDataValidationRequest
                {
                    Range = new GridRange
                    {
                        SheetId = sheetId,
                        StartColumnIndex = columnIndex,
                        EndColumnIndex = columnIndex + 1,
                        StartRowIndex = startRow,
                        EndRowIndex = endRow
                    },
                    Rule = new DataValidationRule
                    {
                        Condition = new BooleanCondition
                        {
                            Type = "DATE_IS_VALID",
                            Values = new List<ConditionValue>()
                        },
                        InputMessage = "Veuillez sélectionner une date valide",
                        ShowCustomUi = true,
                        Strict = true
                    }
                }
            });

            // 2. Ajouter un en-tête pour la colonne de date
            requests.Add(new Request
            {
                UpdateCells = new UpdateCellsRequest
                {
                    Range = new GridRange
                    {
                        SheetId = sheetId,
                        StartColumnIndex = columnIndex,
                        EndColumnIndex = columnIndex + 1,
                        StartRowIndex = 0,
                        EndRowIndex = 1
                    },
                    Rows = new List<RowData>
                {
                    new RowData
                    {
                        Values = new List<CellData>
                        {
                            new CellData
                            {
                                UserEnteredValue = new ExtendedValue
                                {
                                    StringValue = "Date d'échéance 📅"
                                },
                                UserEnteredFormat = new CellFormat
                                {
                                    TextFormat = new TextFormat
                                    {
                                        Bold = true,
                                        FontSize = 12
                                    },
                                    BackgroundColor = new Color
                                    {
                                        Red = 0.9f,
                                        Green = 0.9f,
                                        Blue = 0.9f
                                    }
                                }
                            }
                        }
                    }
                },
                    Fields = "userEnteredValue,userEnteredFormat"
                }
            });

            // Appliquer les changements
            var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
            {
                Requests = requests
            };

            _sheetsService.Spreadsheets.BatchUpdate(batchUpdateRequest, spreadsheetId).Execute();
            Console.WriteLine("Validation de date (calendrier) ajoutée avec succès!");
        }

        // Méthode pour ajouter une liste déroulante personnalisée
        public void AddDropdownList(string spreadsheetId, string sheetName, int columnIndex, int startRow, int endRow, List<string> options)
        {
            var spreadsheet = _sheetsService.Spreadsheets.Get(spreadsheetId).Execute();
            int sheetId = GetSheetId(spreadsheet, sheetName);

            var requests = new List<Request>();

            // Créer les valeurs de condition pour le dropdown
            var conditionValues = new List<ConditionValue>();
            foreach (var option in options)
            {
                conditionValues.Add(new ConditionValue { UserEnteredValue = option });
            }

            // Ajouter la validation pour le dropdown
            requests.Add(new Request
            {
                SetDataValidation = new SetDataValidationRequest
                {
                    Range = new GridRange
                    {
                        SheetId = sheetId,
                        StartColumnIndex = columnIndex,
                        EndColumnIndex = columnIndex + 1,
                        StartRowIndex = startRow,
                        EndRowIndex = endRow
                    },
                    Rule = new DataValidationRule
                    {
                        Condition = new BooleanCondition
                        {
                            Type = "ONE_OF_LIST",
                            Values = conditionValues
                        },
                        InputMessage = "Sélectionnez une option",
                        ShowCustomUi = true,
                        Strict = true
                    }
                }
            });

            // Ajouter un en-tête pour le dropdown
            requests.Add(new Request
            {
                UpdateCells = new UpdateCellsRequest
                {
                    Range = new GridRange
                    {
                        SheetId = sheetId,
                        StartColumnIndex = columnIndex,
                        EndColumnIndex = columnIndex + 1,
                        StartRowIndex = 0,
                        EndRowIndex = 1
                    },
                    Rows = new List<RowData>
                {
                    new RowData
                    {
                        Values = new List<CellData>
                        {
                            new CellData
                            {
                                UserEnteredValue = new ExtendedValue
                                {
                                    StringValue = "Statut ▼"
                                },
                                UserEnteredFormat = new CellFormat
                                {
                                    TextFormat = new TextFormat
                                    {
                                        Bold = true
                                    }
                                }
                            }
                        }
                    }
                },
                    Fields = "userEnteredValue,userEnteredFormat"
                }
            });

            var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
            {
                Requests = requests
            };

            _sheetsService.Spreadsheets.BatchUpdate(batchUpdateRequest, spreadsheetId).Execute();
            Console.WriteLine("Liste déroulante ajoutée avec succès!");
        }

        // Méthode pour ajouter un calendrier avec formatage de date
        public void AddDatePickerWithFormatting(string spreadsheetId, string sheetName)
        {
            var spreadsheet = _sheetsService.Spreadsheets.Get(spreadsheetId).Execute();
            int sheetId = GetSheetId(spreadsheet, sheetName);

            var requests = new List<Request>();

            // Colonnes pour différentes dates
            int[] dateColumns = { 2, 3, 4 }; // Colonnes C, D, E

            foreach (int colIndex in dateColumns)
            {
                // Ajouter validation de date pour les lignes 2 à 100
                requests.Add(new Request
                {
                    SetDataValidation = new SetDataValidationRequest
                    {
                        Range = new GridRange
                        {
                            SheetId = sheetId,
                            StartColumnIndex = colIndex,
                            EndColumnIndex = colIndex + 1,
                            StartRowIndex = 1,
                            EndRowIndex = 100
                        },
                        Rule = new DataValidationRule
                        {
                            Condition = new BooleanCondition
                            {
                                Type = "DATE_IS_VALID"
                            },
                            InputMessage = "Cliquez pour sélectionner une date",
                            ShowCustomUi = true,
                            Strict = true
                        }
                    }
                });
            }

            // Formater les colonnes de date
            requests.Add(new Request
            {
                RepeatCell = new RepeatCellRequest
                {
                    Range = new GridRange
                    {
                        SheetId = sheetId,
                        StartColumnIndex = 2,
                        EndColumnIndex = 5,
                        StartRowIndex = 1,
                        EndRowIndex = 100
                    },
                    Cell = new CellData
                    {
                        UserEnteredFormat = new CellFormat
                        {
                            NumberFormat = new NumberFormat
                            {
                                Type = "DATE",
                                Pattern = "dd/mm/yyyy"
                            }
                        }
                    },
                    Fields = "userEnteredFormat.numberFormat"
                }
            });

            // Ajouter des en-têtes pour les colonnes de date
            var headers = new List<string> { "Date de création", "Date d'échéance", "Date de paiement" };
            for (int i = 0; i < headers.Count; i++)
            {
                requests.Add(new Request
                {
                    UpdateCells = new UpdateCellsRequest
                    {
                        Range = new GridRange
                        {
                            SheetId = sheetId,
                            StartColumnIndex = 2 + i,
                            EndColumnIndex = 3 + i,
                            StartRowIndex = 0,
                            EndRowIndex = 1
                        },
                        Rows = new List<RowData>
                    {
                        new RowData
                        {
                            Values = new List<CellData>
                            {
                                new CellData
                                {
                                    UserEnteredValue = new ExtendedValue
                                    {
                                        StringValue = headers[i]
                                    },
                                    UserEnteredFormat = new CellFormat
                                    {
                                        TextFormat = new TextFormat { Bold = true },
                                        BackgroundColor = new Color { Red = 0.8f, Green = 0.8f, Blue = 1.0f }
                                    }
                                }
                            }
                        }
                    },
                        Fields = "userEnteredValue,userEnteredFormat"
                    }
                });
            }

            var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
            {
                Requests = requests
            };

            _sheetsService.Spreadsheets.BatchUpdate(batchUpdateRequest, spreadsheetId).Execute();
            Console.WriteLine("Calendriers (date pickers) ajoutés avec succès!");
        }

        // Méthode utilitaire pour obtenir l'ID de la feuille
        private int GetSheetId(Spreadsheet spreadsheet, string sheetName)
        {
            foreach (var sheet in spreadsheet.Sheets)
            {
                if (sheet.Properties.Title == sheetName)
                {
                    return sheet.Properties.SheetId.Value;
                }
            }
            return 0; // Default to first sheet if not found
        }

        // Méthode pour ajouter des données avec les nouvelles fonctionnalités
        public void AddDataWithDropdownsAndCalendars(string spreadsheetId)
        {
            var data = new List<List<object>>
        {
            new List<object> { "ID", "Client", "Date création", "Date échéance", "Date paiement", "Statut", "Montant" },
            new List<object> { 1, "Jean Dupont", "2024-01-15", "2024-02-15", "", "En attente", 1500.00 },
            new List<object> { 2, "Marie Martin", "2024-01-20", "2024-02-20", "2024-02-18", "Payé", 2750.00 },
            new List<object> { 3, "Pierre Durand", "2024-02-01", "2024-03-01", "", "En retard", 890.99 },
            new List<object> { 4, "Sophie Lefebvre", "2024-02-10", "2024-03-10", "", "En attente", 4200.75 }
        };

            // Fix for CS0266: Explicitly cast the List<List<object>> to IList<IList<object>>.
            var valueRange = new ValueRange
            {
                Values = data.Cast<IList<object>>().ToList()
            };

            var updateRequest = _sheetsService.Spreadsheets.Values.Update(valueRange, spreadsheetId, "Feuille1!A1");
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
        }

        // Démonstration complète
        public void DemoGoogleSheetsWithDropdownsAndCalendars()
        {
            try
            {
                // 1. Créer le spreadsheet
                string spreadsheetId = CreateNewSpreadsheet("Gestion des factures avec calendriers");

                // 2. Ajouter les données
                AddDataWithDropdownsAndCalendars(spreadsheetId);

                // 3. Ajouter des calendriers (date pickers)
                AddDatePickerWithFormatting(spreadsheetId, "Feuille1");

                // 4. Ajouter une liste déroulante pour le statut (colonne F)
                var statusOptions = new List<string> { "En attente", "Payé", "En retard", "Annulé", "En traitement" };
                AddDropdownList(spreadsheetId, "Feuille1", 5, 1, 100, statusOptions);

                // 5. Ajouter une validation de date supplémentaire pour une colonne spécifique
                AddDataValidationWithCalendar(spreadsheetId, "Feuille1", 2, 1, 100); // Colonne C - Date création

                Console.WriteLine($"\n✅ Toutes les opérations sont terminées avec succès!");
                Console.WriteLine($"📊 URL du spreadsheet: {_sheetsService.Spreadsheets.Get(spreadsheetId).Execute().SpreadsheetUrl}");
                Console.WriteLine("\nFonctionnalités ajoutées:");
                Console.WriteLine("   - 📅 Calendriers (date pickers) pour les colonnes de date");
                Console.WriteLine("   - ▼ Listes déroulantes pour le statut");
                Console.WriteLine("   - ✨ Formatage automatique des dates");
                Console.WriteLine("   - 🎨 En-têtes colorés");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur: {ex.Message}");
            }
        }



    }
}
