using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using SendBillWF.BL;
using SendBillWF.Compagny;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SendBillWF.Compagny.Compagny
{
    public partial class AssignedEmployeePriceToCompagny : UserControl
    {
        List<TreeNode> originalNodes;
        EmployeePoco employeePoco;
        EmployeeManipulation employeeManipulation;
        CompagniManipulation compagniManipulation;
        List<CompagniePoco> EmployeCompagnyWekly;
        List<CompagniePoco> EmployeCompagnyBiweekly;
        WorkManipulation workManipulation;

        private Dictionary<(Guid CompanyPricingCalendarId,Guid EmployeeId, string Day), decimal> weeklyChanges = new Dictionary<(Guid CompanyPricingCalendarId, Guid EmployeeId, string Day), decimal>();
        private Dictionary<(Guid CompanyPricingCalendarId, Guid EmployeeId, string Day), decimal> biWeeklyChanges = new Dictionary<(Guid CompanyPricingCalendarId, Guid EmployeeId, string Day), decimal>();

        public AssignedEmployeePriceToCompagny()
        {
            InitializeComponent();
            employeePoco = new EmployeePoco();
            employeeManipulation = new EmployeeManipulation();
            compagniManipulation = new CompagniManipulation();
            EmployeCompagnyWekly = new List<CompagniePoco>();
            EmployeCompagnyBiweekly = new List<CompagniePoco>();
            workManipulation = new WorkManipulation();
            EmployeetreeView.AfterSelect += EmployeetreeView_AfterSelect;

            FillTreeViewFromDatabase();
            SaveOriginalTree();
            //employeeUsSchedulair.DisableUserControl();
        }


        private void FillTreeViewFromDatabase()
        {
            EmployeetreeView.BeginUpdate();
            EmployeetreeView.Nodes.Clear();

            // Charger les données en une seule fois


            var Employee = employeeManipulation.GetEmployee();


            //- {employee.EmployeeId}" as changé par NAS
            foreach (var employee in Employee)
            {
                TreeNode emplyeeNode = new TreeNode(
                    $"{employee.name} "
                )
                {
                    Tag = employee.EmployeeId
                };
                EmployeetreeView.Nodes.Add(emplyeeNode);
            }

            EmployeetreeView.EndUpdate();
        }

        private void EmployeetreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            List<CompagniePoco> EmployeCompagny = new List<CompagniePoco>();
            EmployeePoco employeePoco = new EmployeePoco();
            //List<EmployeeCompagnyPricingDto> employeeCompagnyPricingWekklyDto = new List<EmployeeCompagnyPricingDto>();
            //List<EmployeeCompagnyPricingDto> employeeCompagnyPricingBiWeeklyDto = new List<EmployeeCompagnyPricingDto>();
            List<EmployeeCompagnyPricingDto> employeeCompagnyPricingDto = new List<EmployeeCompagnyPricingDto>();

            if (e.Node.Tag != null)
            {
                var employeeId = new Guid(e.Node.Tag.ToString());

                //visitWorkSheetUctr1.updateCompagnyList();
                employeePoco = employeeManipulation.GetEmployeeById(e.Node.Tag.ToString());
                EmployeCompagny = compagniManipulation.GetCompagnyByEmployee(employeePoco.EmployeeId);

            }


            employeeCompagnyPricingDto = (from c in EmployeCompagny
                                                      join p in workManipulation.GetCompanyPricingCalendarsList(EmployeCompagny) on c.CompagnieID equals p.CompanyId
                                                      select new EmployeeCompagnyPricingDto
                                                      {
                                                          CompanyPricingCalendarId = p.CompanyPricingCalendarId,
                                                          CompanyId = c.CompagnieID,
                                                          CompagnyCode = c.CompagnieCode,
                                                          CompagnyName = c.CompagnieName,
                                                          EmployeeId = employeePoco.EmployeeId,
                                                          Emplyeepaiment = p.Emplyeepaiment,
                                                          DaysStatus = p.DaysStatus,
                                                          Days = p.Days
                                                      }).ToList();


            //IL faut aller cherché les paiment specilal pour les emplyéé 
            EmployeeCompagnyPricing employeeCompagnyPricing ;
            foreach (var item in employeeCompagnyPricingDto)
            {
                

                employeeCompagnyPricing = workManipulation.GetEmployeeCompagnyPricing(item.CompanyPricingCalendarId, item.EmployeeId);
                if (employeeCompagnyPricing != null)
                {
                    item.Emplyeepaiment = employeeCompagnyPricing.EmplyeePaiment;
                }
            }

            LoadWeeklyDataGrid(employeeCompagnyPricingDto);

        }

        private void SaveOriginalTree()
        {
            originalNodes = CloneNodes(EmployeetreeView.Nodes);
        }

        private List<TreeNode> CloneNodes(TreeNodeCollection nodes)
        {
            List<TreeNode> list = new();

            foreach (TreeNode node in nodes)
            {
                list.Add((TreeNode)node.Clone());
            }

            return list;
        }


        private void LoadWeeklyDataGrid(List<EmployeeCompagnyPricingDto> allData)
        {
            // Séparer les données
            var weeklyData = allData.Where(d => d.Days != null && d.Days.StartsWith("Weekly_")).ToList();
            var biWeeklyData = allData.Where(d => d.Days != null && d.Days.StartsWith("BiWeek")).ToList();

            // Convertir en listes pour DataGrid
            var weeklyList = new List<WeeklyDisplayItem>();
            var biWeeklyList = new List<BiWeeklyDisplayItem>();
            
            // Traitement des données Weekly
            var weeklyGroups = weeklyData.GroupBy(d => d.CompanyId);
            foreach (var group in weeklyGroups)
            {
                var item = new WeeklyDisplayItem
                {
                   // CompanyPricingCalendarId = group.First().CompanyPricingCalendarId,
                    EmployeeId = group.First().EmployeeId,
                    CompanyId = group.First().CompanyId, // Fix: Use the Guid directly
                    CompanyName = group.First().CompagnyName
                };

                // Remplir chaque jour séparément
                foreach (var record in group)
                {
                    item.CompanyPricingCalendarId = record.CompanyPricingCalendarId;

                    string dayName = record.Days.Replace("Weekly_", "");
                    string value = record.DaysStatus ? record.Emplyeepaiment.ToString() : "";

                    switch (dayName.ToLower())
                    {
                        case "monday": item.Monday = value; break;
                        case "tuesday": item.Tuesday = value; break;
                        case "wednesday": item.Wednesday = value; break;
                        case "thursday": item.Thursday = value; break;
                        case "friday": item.Friday = value; break;
                        case "saturday": item.Saturday = value; break;
                        case "sunday": item.Sunday = value; break;
                    }

                    // Ajouter une propriété pour savoir si la cellule est éditable
                    switch (dayName.ToLower())
                    {
                        case "monday": item.IsMondayEditable = record.DaysStatus; break;
                        case "tuesday": item.IsTuesdayEditable = record.DaysStatus; break;
                        case "wednesday": item.IsWednesdayEditable = record.DaysStatus; break;
                        case "thursday": item.IsThursdayEditable = record.DaysStatus; break;
                        case "friday": item.IsFridayEditable = record.DaysStatus; break;
                        case "saturday": item.IsSaturdayEditable = record.DaysStatus; break;
                        case "sunday": item.IsSundayEditable = record.DaysStatus; break;
                    }
                }

                weeklyList.Add(item);
            }

            // Traitement des données BiWeekly
            var biWeeklyGroups = biWeeklyData.GroupBy(d => d.CompanyId);
            foreach (var group in biWeeklyGroups)
            {
                var item = new BiWeeklyDisplayItem
                {
                    CompanyPricingCalendarId = group.First().CompanyPricingCalendarId,
                    EmployeeId = group.First().EmployeeId,
                    CompanyId = group.First().CompanyId,
                    CompanyName = group.First().CompagnyName
                };

                // Remplir chaque période bi-hebdomadaire
                foreach (var record in group)
                {
                    string dayKey = record.Days;
                    string value = record.DaysStatus ? record.Emplyeepaiment.ToString() : "";

                    // Map des noms de colonnes
                    switch (dayKey)
                    {
                        case "BiWeek1_Monday":
                            item.BiWeek1_Monday = value;
                            item.IsBiWeek1_MondayEditable = record.DaysStatus;
                            break;
                        case "BiWeek1_Tuesday":
                            item.BiWeek1_Tuesday = value;
                            item.IsBiWeek1_TuesdayEditable = record.DaysStatus;
                            break;
                        case "BiWeek1_Wednesday":
                            item.BiWeek1_Wednesday = value;
                            item.IsBiWeek1_WednesdayEditable = record.DaysStatus;
                            break;
                        case "BiWeek1_Thursday":
                            item.BiWeek1_Thursday = value;
                            item.IsBiWeek1_ThursdayEditable = record.DaysStatus;
                            break;
                        case "BiWeek1_Friday":
                            item.BiWeek1_Friday = value;
                            item.IsBiWeek1_FridayEditable = record.DaysStatus;
                            break;
                        case "BiWeek1_Saturday":
                            item.BiWeek1_Saturday = value;
                            item.IsBiWeek1_SaturdayEditable = record.DaysStatus;
                            break;
                        case "BiWeek1_Sunday":
                            item.BiWeek1_Sunday = value;
                            item.IsBiWeek1_SundayEditable = record.DaysStatus;
                            break;
                        case "BiWeek2_Monday":
                            item.BiWeek2_Monday = value;
                            item.IsBiWeek2_MondayEditable = record.DaysStatus;
                            break;
                        case "BiWeek2_Tuesday":
                            item.BiWeek2_Tuesday = value;
                            item.IsBiWeek2_TuesdayEditable = record.DaysStatus;
                            break;
                        case "BiWeek2_Wednesday":
                            item.BiWeek2_Wednesday = value;
                            item.IsBiWeek2_WednesdayEditable = record.DaysStatus;
                            break;
                        case "BiWeek2_Thursday":
                            item.BiWeek2_Thursday = value;
                            item.IsBiWeek2_ThursdayEditable = record.DaysStatus;
                            break;
                        case "BiWeek2_Friday":
                            item.BiWeek2_Friday = value;
                            item.IsBiWeek2_FridayEditable = record.DaysStatus;
                            break;
                        case "BiWeek2_Saturday":
                            item.BiWeek2_Saturday = value;
                            item.IsBiWeek2_SaturdayEditable = record.DaysStatus;
                            break;
                        case "BiWeek2_Sunday":
                            item.BiWeek2_Sunday = value;
                            item.IsBiWeek2_SundayEditable = record.DaysStatus;
                            break;
                    }
                }

                biWeeklyList.Add(item);
            }



            // Définir les DataSource
            WeeklyDtg.DataSource = null;
            WeeklyDtg.DataSource = weeklyList;

            BiWeeklyDtg.DataSource = null;
            BiWeeklyDtg.DataSource = biWeeklyList;

            AppliquerStylesCellules();
            RendreCellulesNonEditables();

            ConfigureDataGridDisplay();
            AddCellEditEvents();
            AddCellValueChangedEvents();

            // Rafraîchir
            WeeklyDtg.Refresh();
            BiWeeklyDtg.Refresh();
        }

        private void ConfigureDataGridDisplay()
        {
            ConfigureWeeklyGrid();
            ConfigureBiWeeklyGrid();
        }

        private void ConfigureWeeklyGrid()
        {
            WeeklyDtg.AutoGenerateColumns = false;
            WeeklyDtg.Columns.Clear();

            // Colonne Company Name
            WeeklyDtg.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Company Name",
                DataPropertyName = "CompanyName",
                Name = "colCompanyName",
                Width = 150,
                ReadOnly = true
            });

            // Ajouter les colonnes avec template pour contrôler l'édition
            AddWeeklyColumn("Monday", "Monday", "IsMondayEditable");
            AddWeeklyColumn("Tuesday", "Tuesday", "IsTuesdayEditable");
            AddWeeklyColumn("Wednesday", "Wednesday", "IsWednesdayEditable");
            AddWeeklyColumn("Thursday", "Thursday", "IsThursdayEditable");
            AddWeeklyColumn("Friday", "Friday", "IsFridayEditable");
            AddWeeklyColumn("Saturday", "Saturday", "IsSaturdayEditable");
            AddWeeklyColumn("Sunday", "Sunday", "IsSundayEditable");
        }

        private void AddWeeklyColumn(string dataPropertyName, string headerText, string editablePropertyName)
        {
            // Créer une colonne template pour contrôler l'édition
            var column = new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                Name = "col" + dataPropertyName,
                Width = 100,
                ReadOnly = false // Laisser false, on contrôlera via l'événement
            };

            WeeklyDtg.Columns.Add(column);
        }

        private void ConfigureBiWeeklyGrid()
        {
            BiWeeklyDtg.AutoGenerateColumns = false;
            BiWeeklyDtg.Columns.Clear();

            //BiWeeklyDtg.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    HeaderText = "Company ID",
            //    DataPropertyName = "CompanyId",
            //    Name = "colBiCompanyId",
            //    Width = 150,
            //    ReadOnly = true,
            //    Visible = false // Changez à false pour masquer
            //}); 
            // Colonne Company Name
            BiWeeklyDtg.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Company Name",
                DataPropertyName = "CompanyName",
                Name = "colBiCompanyName",
                Width = 150,
                ReadOnly = true
            });

            // Ajouter les colonnes BiWeek avec template pour contrôler l'édition
            AddBiWeeklyColumn("BiWeek1_Monday", "1 - Monday", "IsBiWeek1_MondayEditable");
            AddBiWeeklyColumn("BiWeek1_Tuesday", "1 - Tuesday", "IsBiWeek1_TuesdayEditable");
            AddBiWeeklyColumn("BiWeek1_Wednesday", "1 - Wednesday", "IsBiWeek1_WednesdayEditable");
            AddBiWeeklyColumn("BiWeek1_Thursday", "1 - Thursday", "IsBiWeek1_ThursdayEditable");
            AddBiWeeklyColumn("BiWeek1_Friday", "1 - Friday", "IsBiWeek1_FridayEditable");
            AddBiWeeklyColumn("BiWeek1_Saturday", "1 - Saturday", "IsBiWeek1_SaturdayEditable");
            AddBiWeeklyColumn("BiWeek1_Sunday", "1 - Sunday", "IsBiWeek1_SundayEditable");

            AddBiWeeklyColumn("BiWeek2_Monday", "2 - Monday", "IsBiWeek2_MondayEditable");
            AddBiWeeklyColumn("BiWeek2_Tuesday", "2 - Tuesday", "IsBiWeek2_TuesdayEditable");
            AddBiWeeklyColumn("BiWeek2_Wednesday", "2 - Wednesday", "IsBiWeek2_WednesdayEditable");
            AddBiWeeklyColumn("BiWeek2_Thursday", "2 - Thursday", "IsBiWeek2_ThursdayEditable");
            AddBiWeeklyColumn("BiWeek2_Friday", "2 - Friday", "IsBiWeek2_FridayEditable");
            AddBiWeeklyColumn("BiWeek2_Saturday", "2 - Saturday", "IsBiWeek2_SaturdayEditable");
            AddBiWeeklyColumn("BiWeek2_Sunday", "2 - Sunday", "IsBiWeek2_SundayEditable");
        }

        private void AddBiWeeklyColumn(string dataPropertyName, string headerText, string editablePropertyName)
        {
            var column = new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                Name = "col" + dataPropertyName,
                Width = 80,
                ReadOnly = false // Contrôlé via événement
            };

            BiWeeklyDtg.Columns.Add(column);
        }

        private void AddCellEditEvents()
        {
            // Pour le DataGrid Weekly
            WeeklyDtg.CellBeginEdit += (sender, e) =>
            {
                var dgv = sender as DataGridView;
                if (dgv != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var row = dgv.Rows[e.RowIndex];
                    var item = row.DataBoundItem as WeeklyDisplayItem;

                    if (item != null)
                    {
                        bool isEditable = false;
                        string columnName = dgv.Columns[e.ColumnIndex].Name;

                        // Déterminer si la cellule est éditable
                        switch (columnName)
                        {
                            case "colMonday":
                                isEditable = item.IsMondayEditable && !string.IsNullOrEmpty(item.Monday);
                                break;
                            case "colTuesday":
                                isEditable = item.IsTuesdayEditable && !string.IsNullOrEmpty(item.Tuesday);
                                break;
                            case "colWednesday":
                                isEditable = item.IsWednesdayEditable && !string.IsNullOrEmpty(item.Wednesday);
                                break;
                            case "colThursday":
                                isEditable = item.IsThursdayEditable && !string.IsNullOrEmpty(item.Thursday);
                                break;
                            case "colFriday":
                                isEditable = item.IsFridayEditable && !string.IsNullOrEmpty(item.Friday);
                                break;
                            case "colSaturday":
                                isEditable = item.IsSaturdayEditable && !string.IsNullOrEmpty(item.Saturday);
                                break;
                            case "colSunday":
                                isEditable = item.IsSundayEditable && !string.IsNullOrEmpty(item.Sunday);
                                break;
                        }

                        // Annuler l'édition si non éditable
                        if (!isEditable)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            };

            // Pour le DataGrid BiWeekly
            BiWeeklyDtg.CellBeginEdit += (sender, e) =>
            {
                var dgv = sender as DataGridView;
                if (dgv != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var row = dgv.Rows[e.RowIndex];
                    var item = row.DataBoundItem as BiWeeklyDisplayItem;

                    if (item != null)
                    {
                        bool isEditable = false;
                        string columnName = dgv.Columns[e.ColumnIndex].Name;

                        // Déterminer si la cellule est éditable
                        switch (columnName)
                        {
                            case "colBiWeek1_Monday":
                                isEditable = item.IsBiWeek1_MondayEditable && !string.IsNullOrEmpty(item.BiWeek1_Monday);
                                break;
                            case "colBiWeek1_Tuesday":
                                isEditable = item.IsBiWeek1_TuesdayEditable && !string.IsNullOrEmpty(item.BiWeek1_Tuesday);
                                break;
                            case "colBiWeek1_Wednesday":
                                isEditable = item.IsBiWeek1_WednesdayEditable && !string.IsNullOrEmpty(item.BiWeek1_Wednesday);
                                break;
                            case "colBiWeek1_Thursday":
                                isEditable = item.IsBiWeek1_ThursdayEditable && !string.IsNullOrEmpty(item.BiWeek1_Thursday);
                                break;
                            case "colBiWeek1_Friday":
                                isEditable = item.IsBiWeek1_FridayEditable && !string.IsNullOrEmpty(item.BiWeek1_Friday);
                                break;
                            case "colBiWeek1_Saturday":
                                isEditable = item.IsBiWeek1_SaturdayEditable && !string.IsNullOrEmpty(item.BiWeek1_Saturday);
                                break;
                            case "colBiWeek1_Sunday":
                                isEditable = item.IsBiWeek1_SundayEditable && !string.IsNullOrEmpty(item.BiWeek1_Sunday);
                                break;
                            case "colBiWeek2_Monday":
                                isEditable = item.IsBiWeek2_MondayEditable && !string.IsNullOrEmpty(item.BiWeek2_Monday);
                                break;
                            case "colBiWeek2_Tuesday":
                                isEditable = item.IsBiWeek2_TuesdayEditable && !string.IsNullOrEmpty(item.BiWeek2_Tuesday);
                                break;
                            case "colBiWeek2_Wednesday":
                                isEditable = item.IsBiWeek2_WednesdayEditable && !string.IsNullOrEmpty(item.BiWeek2_Wednesday);
                                break;
                            case "colBiWeek2_Thursday":
                                isEditable = item.IsBiWeek2_ThursdayEditable && !string.IsNullOrEmpty(item.BiWeek2_Thursday);
                                break;
                            case "colBiWeek2_Friday":
                                isEditable = item.IsBiWeek2_FridayEditable && !string.IsNullOrEmpty(item.BiWeek2_Friday);
                                break;
                            case "colBiWeek2_Saturday":
                                isEditable = item.IsBiWeek2_SaturdayEditable && !string.IsNullOrEmpty(item.BiWeek2_Saturday);
                                break;
                            case "colBiWeek2_Sunday":
                                isEditable = item.IsBiWeek2_SundayEditable && !string.IsNullOrEmpty(item.BiWeek2_Sunday);
                                break;
                        }

                        // Annuler l'édition si non éditable
                        if (!isEditable)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            };

            // Optionnel: Changer l'apparence des cellules non éditables
            // Dans AddCellEditEvents(), remplacez le gestionnaire CellFormatting par ceci :
            WeeklyDtg.CellFormatting += (sender, e) =>
            {
                var dgv = sender as DataGridView;
                if (dgv != null && e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < dgv.Rows.Count)
                {
                    var row = dgv.Rows[e.RowIndex];
                    var item = row.DataBoundItem as WeeklyDisplayItem;

                    if (item != null)
                    {
                        bool isEditable = false;
                        string columnName = dgv.Columns[e.ColumnIndex].Name;

                        // Vérifier toutes les colonnes de jours
                        switch (columnName)
                        {
                            case "colMonday":
                                isEditable = item.IsMondayEditable && !string.IsNullOrEmpty(item.Monday);
                                break;
                            case "colTuesday":
                                isEditable = item.IsTuesdayEditable && !string.IsNullOrEmpty(item.Tuesday);
                                break;
                            case "colWednesday":
                                isEditable = item.IsWednesdayEditable && !string.IsNullOrEmpty(item.Wednesday);
                                break;
                            case "colThursday":
                                isEditable = item.IsThursdayEditable && !string.IsNullOrEmpty(item.Thursday);
                                break;
                            case "colFriday":
                                isEditable = item.IsFridayEditable && !string.IsNullOrEmpty(item.Friday);
                                break;
                            case "colSaturday":
                                isEditable = item.IsSaturdayEditable && !string.IsNullOrEmpty(item.Saturday);
                                break;
                            case "colSunday":
                                isEditable = item.IsSundayEditable && !string.IsNullOrEmpty(item.Sunday);
                                break;
                        }

                        // Appliquer le style uniquement pour les colonnes de jours
                        if (columnName.StartsWith("col") && columnName != "colCompanyName")
                        {
                            if (!isEditable)
                            {
                                e.CellStyle.BackColor = Color.LightGray;
                                e.CellStyle.ForeColor = Color.DarkGray;
                                e.CellStyle.SelectionBackColor = Color.LightGray;
                                e.CellStyle.SelectionForeColor = Color.DarkGray;
                            }
                            else
                            {
                                e.CellStyle.BackColor = Color.White;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.SelectionBackColor = SystemColors.Highlight;
                                e.CellStyle.SelectionForeColor = SystemColors.HighlightText;
                            }
                        }
                    }
                }
            };

            // AJOUTEZ AUSSI pour BiWeekly (après l'événement WeeklyDtg.CellFormatting) :
            BiWeeklyDtg.CellFormatting += (sender, e) =>
            {
                var dgv = sender as DataGridView;
                if (dgv != null && e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < dgv.Rows.Count)
                {
                    var row = dgv.Rows[e.RowIndex];
                    var item = row.DataBoundItem as BiWeeklyDisplayItem;

                    if (item != null)
                    {
                        bool isEditable = false;
                        string columnName = dgv.Columns[e.ColumnIndex].Name;

                        // Vérifier toutes les colonnes BiWeek
                        switch (columnName)
                        {
                            case "colBiWeek1_Monday":
                                isEditable = item.IsBiWeek1_MondayEditable && !string.IsNullOrEmpty(item.BiWeek1_Monday);
                                break;
                            case "colBiWeek1_Tuesday":
                                isEditable = item.IsBiWeek1_TuesdayEditable && !string.IsNullOrEmpty(item.BiWeek1_Tuesday);
                                break;
                            case "colBiWeek1_Wednesday":
                                isEditable = item.IsBiWeek1_WednesdayEditable && !string.IsNullOrEmpty(item.BiWeek1_Wednesday);
                                break;
                            case "colBiWeek1_Thursday":
                                isEditable = item.IsBiWeek1_ThursdayEditable && !string.IsNullOrEmpty(item.BiWeek1_Thursday);
                                break;
                            case "colBiWeek1_Friday":
                                isEditable = item.IsBiWeek1_FridayEditable && !string.IsNullOrEmpty(item.BiWeek1_Friday);
                                break;
                            case "colBiWeek1_Saturday":
                                isEditable = item.IsBiWeek1_SaturdayEditable && !string.IsNullOrEmpty(item.BiWeek1_Saturday);
                                break;
                            case "colBiWeek1_Sunday":
                                isEditable = item.IsBiWeek1_SundayEditable && !string.IsNullOrEmpty(item.BiWeek1_Sunday);
                                break;
                            case "colBiWeek2_Monday":
                                isEditable = item.IsBiWeek2_MondayEditable && !string.IsNullOrEmpty(item.BiWeek2_Monday);
                                break;
                            case "colBiWeek2_Tuesday":
                                isEditable = item.IsBiWeek2_TuesdayEditable && !string.IsNullOrEmpty(item.BiWeek2_Tuesday);
                                break;
                            case "colBiWeek2_Wednesday":
                                isEditable = item.IsBiWeek2_WednesdayEditable && !string.IsNullOrEmpty(item.BiWeek2_Wednesday);
                                break;
                            case "colBiWeek2_Thursday":
                                isEditable = item.IsBiWeek2_ThursdayEditable && !string.IsNullOrEmpty(item.BiWeek2_Thursday);
                                break;
                            case "colBiWeek2_Friday":
                                isEditable = item.IsBiWeek2_FridayEditable && !string.IsNullOrEmpty(item.BiWeek2_Friday);
                                break;
                            case "colBiWeek2_Saturday":
                                isEditable = item.IsBiWeek2_SaturdayEditable && !string.IsNullOrEmpty(item.BiWeek2_Saturday);
                                break;
                            case "colBiWeek2_Sunday":
                                isEditable = item.IsBiWeek2_SundayEditable && !string.IsNullOrEmpty(item.BiWeek2_Sunday);
                                break;
                        }

                        // Appliquer le style uniquement pour les colonnes BiWeek
                        if (columnName.StartsWith("colBiWeek"))
                        {
                            if (!isEditable)
                            {
                                e.CellStyle.BackColor = Color.LightGray;
                                e.CellStyle.ForeColor = Color.DarkGray;
                                e.CellStyle.SelectionBackColor = Color.LightGray;
                                e.CellStyle.SelectionForeColor = Color.DarkGray;
                            }
                            else
                            {
                                e.CellStyle.BackColor = Color.White;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.SelectionBackColor = SystemColors.Highlight;
                                e.CellStyle.SelectionForeColor = SystemColors.HighlightText;
                            }
                        }
                    }
                }
            };
        }


        // AJOUTEZ CES MÉTHODES APRÈS AddCellEditEvents()
        private void AppliquerStylesCellules()
        {
            // Appliquer les styles au DataGrid Weekly
            for (int i = 0; i < WeeklyDtg.Rows.Count; i++)
            {
                var row = WeeklyDtg.Rows[i];
                if (row.DataBoundItem is WeeklyDisplayItem item)
                {
                    AppliquerStyleLigneWeekly(row, item);
                }
            }

            // Appliquer les styles au DataGrid BiWeekly
            for (int i = 0; i < BiWeeklyDtg.Rows.Count; i++)
            {
                var row = BiWeeklyDtg.Rows[i];
                if (row.DataBoundItem is BiWeeklyDisplayItem item)
                {
                    AppliquerStyleLigneBiWeekly(row, item);
                }
            }
        }

        private void AppliquerStyleLigneWeekly(DataGridViewRow row, WeeklyDisplayItem item)
        {
            // Appliquer le style à chaque colonne de jour
            ColorerCellule(row, "colMonday", item.IsMondayEditable, item.Monday);
            ColorerCellule(row, "colTuesday", item.IsTuesdayEditable, item.Tuesday);
            ColorerCellule(row, "colWednesday", item.IsWednesdayEditable, item.Wednesday);
            ColorerCellule(row, "colThursday", item.IsThursdayEditable, item.Thursday);
            ColorerCellule(row, "colFriday", item.IsFridayEditable, item.Friday);
            ColorerCellule(row, "colSaturday", item.IsSaturdayEditable, item.Saturday);
            ColorerCellule(row, "colSunday", item.IsSundayEditable, item.Sunday);
        }

        private void AppliquerStyleLigneBiWeekly(DataGridViewRow row, BiWeeklyDisplayItem item)
        {
            // Appliquer le style à chaque colonne BiWeek
            ColorerCellule(row, "colBiWeek1_Monday", item.IsBiWeek1_MondayEditable, item.BiWeek1_Monday);
            ColorerCellule(row, "colBiWeek1_Tuesday", item.IsBiWeek1_TuesdayEditable, item.BiWeek1_Tuesday);
            ColorerCellule(row, "colBiWeek1_Wednesday", item.IsBiWeek1_WednesdayEditable, item.BiWeek1_Wednesday);
            ColorerCellule(row, "colBiWeek1_Thursday", item.IsBiWeek1_ThursdayEditable, item.BiWeek1_Thursday);
            ColorerCellule(row, "colBiWeek1_Friday", item.IsBiWeek1_FridayEditable, item.BiWeek1_Friday);
            ColorerCellule(row, "colBiWeek1_Saturday", item.IsBiWeek1_SaturdayEditable, item.BiWeek1_Saturday);
            ColorerCellule(row, "colBiWeek1_Sunday", item.IsBiWeek1_SundayEditable, item.BiWeek1_Sunday);

            ColorerCellule(row, "colBiWeek2_Monday", item.IsBiWeek2_MondayEditable, item.BiWeek2_Monday);
            ColorerCellule(row, "colBiWeek2_Tuesday", item.IsBiWeek2_TuesdayEditable, item.BiWeek2_Tuesday);
            ColorerCellule(row, "colBiWeek2_Wednesday", item.IsBiWeek2_WednesdayEditable, item.BiWeek2_Wednesday);
            ColorerCellule(row, "colBiWeek2_Thursday", item.IsBiWeek2_ThursdayEditable, item.BiWeek2_Thursday);
            ColorerCellule(row, "colBiWeek2_Friday", item.IsBiWeek2_FridayEditable, item.BiWeek2_Friday);
            ColorerCellule(row, "colBiWeek2_Saturday", item.IsBiWeek2_SaturdayEditable, item.BiWeek2_Saturday);
            ColorerCellule(row, "colBiWeek2_Sunday", item.IsBiWeek2_SundayEditable, item.BiWeek2_Sunday);
        }

        private void ColorerCellule(DataGridViewRow row, string nomColonne, bool estEditable, string valeur)
        {
            // Trouver la colonne par son nom
            foreach (DataGridViewColumn colonne in row.DataGridView.Columns)
            {
                if (colonne.Name == nomColonne)
                {
                    var cellule = row.Cells[colonne.Index];

                    // Déterminer si la cellule doit être éditable
                    // (DaysStatus = true ET valeur non vide)
                    bool doitEtreEditable = estEditable && !string.IsNullOrEmpty(valeur);

                    if (!doitEtreEditable)
                    {
                        // Colorer en gris si non éditable
                        cellule.Style.BackColor = Color.LightGray;
                        cellule.Style.ForeColor = Color.DarkGray;
                        cellule.Style.SelectionBackColor = Color.LightGray;
                        cellule.Style.SelectionForeColor = Color.DarkGray;
                    }
                    else
                    {
                        // Style normal pour les cellules éditables
                        cellule.Style.BackColor = Color.White;
                        cellule.Style.ForeColor = Color.Black;
                        cellule.Style.SelectionBackColor = SystemColors.Highlight;
                        cellule.Style.SelectionForeColor = SystemColors.HighlightText;
                    }
                    break;
                }
            }
        }

        private void RendreCellulesNonEditables()
        {
            // Rendre les cellules non éditables pour Weekly
            for (int i = 0; i < WeeklyDtg.Rows.Count; i++)
            {
                var row = WeeklyDtg.Rows[i];
                if (row.DataBoundItem is WeeklyDisplayItem item)
                {
                    RendreLigneWeeklyNonEditable(row, item);
                }
            }

            // Rendre les cellules non éditables pour BiWeekly
            for (int i = 0; i < BiWeeklyDtg.Rows.Count; i++)
            {
                var row = BiWeeklyDtg.Rows[i];
                if (row.DataBoundItem is BiWeeklyDisplayItem item)
                {
                    RendreLigneBiWeeklyNonEditable(row, item);
                }
            }
        }

        private void RendreLigneWeeklyNonEditable(DataGridViewRow row, WeeklyDisplayItem item)
        {
            RendreCelluleNonEditable(row, "colMonday", item.IsMondayEditable, item.Monday);
            RendreCelluleNonEditable(row, "colTuesday", item.IsTuesdayEditable, item.Tuesday);
            RendreCelluleNonEditable(row, "colWednesday", item.IsWednesdayEditable, item.Wednesday);
            RendreCelluleNonEditable(row, "colThursday", item.IsThursdayEditable, item.Thursday);
            RendreCelluleNonEditable(row, "colFriday", item.IsFridayEditable, item.Friday);
            RendreCelluleNonEditable(row, "colSaturday", item.IsSaturdayEditable, item.Saturday);
            RendreCelluleNonEditable(row, "colSunday", item.IsSundayEditable, item.Sunday);
        }

        private void RendreLigneBiWeeklyNonEditable(DataGridViewRow row, BiWeeklyDisplayItem item)
        {
            RendreCelluleNonEditable(row, "colBiWeek1_Monday", item.IsBiWeek1_MondayEditable, item.BiWeek1_Monday);
            RendreCelluleNonEditable(row, "colBiWeek1_Tuesday", item.IsBiWeek1_TuesdayEditable, item.BiWeek1_Tuesday);
            RendreCelluleNonEditable(row, "colBiWeek1_Wednesday", item.IsBiWeek1_WednesdayEditable, item.BiWeek1_Wednesday);
            RendreCelluleNonEditable(row, "colBiWeek1_Thursday", item.IsBiWeek1_ThursdayEditable, item.BiWeek1_Thursday);
            RendreCelluleNonEditable(row, "colBiWeek1_Friday", item.IsBiWeek1_FridayEditable, item.BiWeek1_Friday);
            RendreCelluleNonEditable(row, "colBiWeek1_Saturday", item.IsBiWeek1_SaturdayEditable, item.BiWeek1_Saturday);
            RendreCelluleNonEditable(row, "colBiWeek1_Sunday", item.IsBiWeek1_SundayEditable, item.BiWeek1_Sunday);

            RendreCelluleNonEditable(row, "colBiWeek2_Monday", item.IsBiWeek2_MondayEditable, item.BiWeek2_Monday);
            RendreCelluleNonEditable(row, "colBiWeek2_Tuesday", item.IsBiWeek2_TuesdayEditable, item.BiWeek2_Tuesday);
            RendreCelluleNonEditable(row, "colBiWeek2_Wednesday", item.IsBiWeek2_WednesdayEditable, item.BiWeek2_Wednesday);
            RendreCelluleNonEditable(row, "colBiWeek2_Thursday", item.IsBiWeek2_ThursdayEditable, item.BiWeek2_Thursday);
            RendreCelluleNonEditable(row, "colBiWeek2_Friday", item.IsBiWeek2_FridayEditable, item.BiWeek2_Friday);
            RendreCelluleNonEditable(row, "colBiWeek2_Saturday", item.IsBiWeek2_SaturdayEditable, item.BiWeek2_Saturday);
            RendreCelluleNonEditable(row, "colBiWeek2_Sunday", item.IsBiWeek2_SundayEditable, item.BiWeek2_Sunday);
        }

        private void RendreCelluleNonEditable(DataGridViewRow row, string nomColonne, bool estEditable, string valeur)
        {
            foreach (DataGridViewColumn colonne in row.DataGridView.Columns)
            {
                if (colonne.Name == nomColonne)
                {
                    var cellule = row.Cells[colonne.Index];
                    bool doitEtreEditable = estEditable && !string.IsNullOrEmpty(valeur);
                    cellule.ReadOnly = !doitEtreEditable;
                    break;
                }
            }
        }


        private void AddCellValueChangedEvents()
        {
            // Pour le DataGrid Weekly
            WeeklyDtg.CellValueChanged += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    HandleWeeklyCellValueChanged(e.RowIndex, e.ColumnIndex);
                }
            };

            // Pour le DataGrid BiWeekly
            BiWeeklyDtg.CellValueChanged += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    HandleBiWeeklyCellValueChanged(e.RowIndex, e.ColumnIndex);
                }
            };

            // Pour gérer la validation de l'entrée
            WeeklyDtg.CellValidating += (sender, e) =>
            {
                ValidateCellInput(WeeklyDtg, e.RowIndex, e.ColumnIndex, e);
            };

            BiWeeklyDtg.CellValidating += (sender, e) =>
            {
                ValidateCellInput(BiWeeklyDtg, e.RowIndex, e.ColumnIndex, e);
            };
        }

        private void ValidateCellInput(DataGridView dgv, int rowIndex, int columnIndex, DataGridViewCellValidatingEventArgs e)
        {
            if (columnIndex  != 0 && dgv.Columns[columnIndex].Name.StartsWith("col") &&
                dgv.Columns[columnIndex].Name != "colCompanyName")
            {
                if (!string.IsNullOrEmpty(e.FormattedValue?.ToString()))
                {
                    if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal value))
                    {
                        MessageBox.Show("Veuillez entrer une valeur numérique valide.",
                            "Erreur de saisie",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    else if (value < 0)
                    {
                        MessageBox.Show("La valeur ne peut pas être négative.",
                            "Erreur de saisie",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
            }
        }

        private void HandleWeeklyCellValueChanged(int rowIndex, int columnIndex)
        {
            var row = WeeklyDtg.Rows[rowIndex];
            var column = WeeklyDtg.Columns[columnIndex];

            if (row.DataBoundItem is WeeklyDisplayItem item && column.Name != "colCompanyName")
            {
                string dayColumn = column.Name.Replace("col", "");
                string newValue = row.Cells[columnIndex].Value?.ToString() ?? "";

                if (Guid.TryParse(item.CompanyId.ToString(), out Guid companyId))
                {
                    string dayKey = "";

                    // Mapper le nom de colonne au nom du jour
                    switch (dayColumn)
                    {
                        case "Monday": dayKey = "Weekly_Monday"; break;
                        case "Tuesday": dayKey = "Weekly_Tuesday"; break;
                        case "Wednesday": dayKey = "Weekly_Wednesday"; break;
                        case "Thursday": dayKey = "Weekly_Thursday"; break;
                        case "Friday": dayKey = "Weekly_Friday"; break;
                        case "Saturday": dayKey = "Weekly_Saturday"; break;
                        case "Sunday": dayKey = "Weekly_Sunday"; break;
                    }

                    if (!string.IsNullOrEmpty(dayKey))
                    {
                        if (decimal.TryParse(newValue, out decimal decimalValue))
                        {
                            // Convert Guid to int for compatibility with the dictionary key  GetCompanyPricingCalendarsByDaysKey()

                            Guid CPCId = workManipulation.GetCompanyPricingCalendarsByDaysKey(dayKey);
                            var key = (CPCId, item.EmployeeId, dayKey);
                            weeklyChanges[key] = decimalValue;

                            // Mettre à jour la couleur pour indiquer un changement
                            row.Cells[columnIndex].Style.BackColor = Color.LightYellow;

                            Console.WriteLine($"Changement enregistré - CompanyId: {companyId}, Day: {dayKey}, Value: {decimalValue}");
                        }
                        else if (string.IsNullOrEmpty(newValue))
                        {
                            // Si la valeur est vidée, marquer pour suppression
                            var key = (item.CompanyPricingCalendarId, item.EmployeeId, dayKey);
                            weeklyChanges[key] = 0; // ou une valeur spéciale pour indiquer la suppression

                            // Mettre à jour la couleur
                            row.Cells[columnIndex].Style.BackColor = Color.LightYellow;
                        }
                    }
                }

        
            }
        }

        private void HandleBiWeeklyCellValueChanged(int rowIndex, int columnIndex)
        {
            var row = BiWeeklyDtg.Rows[rowIndex];
            var column = BiWeeklyDtg.Columns[columnIndex];

            if (row.DataBoundItem is BiWeeklyDisplayItem item && column.Name.StartsWith("colBiWeek"))
            {
                string columnName = column.Name.Replace("col", "");
                string newValue = row.Cells[columnIndex].Value?.ToString() ?? "";

                // Note: Vous aurez besoin d'un moyen de récupérer le CompanyId pour BiWeeklyDisplayItem
                // Si ce n'est pas disponible, vous devrez l'ajouter à la classe BiWeeklyDisplayItem

                // Pour l'instant, supposons que CompanyId soit disponible ou trouvé d'une autre manière
                Guid companyId = GetCompanyIdForBiWeeklyItem(item);

                if (companyId != Guid.Empty)
                {
                    string dayKey = columnName; // Ex: "BiWeek1_Monday"

                    if (decimal.TryParse(newValue, out decimal decimalValue))
                    {
                        Guid CPCId = workManipulation.GetCompanyPricingCalendarsByDaysKey(dayKey);
                        // Ajouter ou mettre à jour le changement    GetCompanyPricingCalendarsByDaysKey()
                        var key = (CPCId, item.EmployeeId, dayKey);
                        biWeeklyChanges[key] = decimalValue;

                        // Mettre à jour la couleur pour indiquer un changement
                        row.Cells[columnIndex].Style.BackColor = Color.LightYellow;

                        Console.WriteLine($"Changement BiWeekly enregistré - CompanyId: {companyId}, Day: {dayKey}, Value: {decimalValue}");
                    }
                    else if (string.IsNullOrEmpty(newValue))
                    {
                        // Si la valeur est vidée
                        var key = (item.CompanyPricingCalendarId, item.EmployeeId, dayKey);
                        biWeeklyChanges[key] = 0;

                        row.Cells[columnIndex].Style.BackColor = Color.LightYellow;
                    }
                }
            }
        }

        // Méthode pour obtenir le CompanyId pour un item BiWeekly
        private Guid GetCompanyIdForBiWeeklyItem(BiWeeklyDisplayItem item)
        {
            // Implémentez la logique pour récupérer le CompanyId
            // Cela dépend de comment vos données sont structurées
            // Vous pourriez avoir besoin d'un dictionnaire de mapping ou d'une autre méthode

            // Exemple temporaire - vous devrez adapter ceci
            return item.CompanyId; // À remplacer par la logique appropriée
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }
        private void SaveChanges()
        {
            try
            {
                bool hasChanges = weeklyChanges.Count > 0 || biWeeklyChanges.Count > 0;

                if (!hasChanges)
                {
                    MessageBox.Show("Aucun changement à sauvegarder.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show($"Voulez-vous sauvegarder {weeklyChanges.Count + biWeeklyChanges.Count} changement(s)?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Désactiver le bouton de sauvegarde pendant l'opération
                    btnSave.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;

                    // Sauvegarder les changements Weekly
                    if (weeklyChanges.Count > 0)
                    {
                        compagniManipulation.SaveEmployeePriceChanged(weeklyChanges);
                    }

                    // Sauvegarder les changements BiWeekly
                    if (biWeeklyChanges.Count > 0)
                    {
                        compagniManipulation.SaveEmployeePriceChanged(biWeeklyChanges);
                    }

                    // Réinitialiser les dictionnaires de changements
                    weeklyChanges.Clear();
                    biWeeklyChanges.Clear();

                    // Recharger les données si nécessaire
                    // await LoadDataAsync();

                    MessageBox.Show("Les changements ont été sauvegardés avec succès!",
                        "Succès",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Réappliquer les styles pour enlever la couleur jaune
                    AppliquerStylesCellules();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde: {ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        //private void SaveWeeklyChangesAsync()
        //{
        //    // Implémentez la logique de sauvegarde pour les données Weekly
        //    // Exemple avec Entity Framework (adaptez à votre contexte)

        //    /*
        //    using (var context = new YourDbContext())
        //    {
        //        foreach (var change in weeklyChanges)
        //        {
        //            var (companyId, day) = change.Key;
        //            var newValue = change.Value;

        //            // Trouver l'enregistrement existant
        //            var record = await context.EmployeeCompagnyPricing
        //                .FirstOrDefaultAsync(r => r.CompanyId == companyId && r.Days == day);

        //            if (record != null)
        //            {
        //                // Mettre à jour la valeur
        //                record.Emplyeepaiment = newValue;
        //                record.ModifiedDate = DateTime.Now;
        //                // Ajoutez d'autres champs de suivi si nécessaire
        //            }
        //        }

        //        await context.SaveChangesAsync();
        //    }
        //    */

        //    throw new NotImplementedException();

        //    // Pour l'instant, simulez la sauvegarde
        //    Console.WriteLine($"Sauvegarde de {weeklyChanges.Count} changements Weekly...");

        //}

        //private async Task SaveBiWeeklyChangesAsync()
        //{
        //    // Implémentez la logique de sauvegarde pour les données BiWeekly

        //    /*
        //    using (var context = new YourDbContext())
        //    {
        //        foreach (var change in biWeeklyChanges)
        //        {
        //            var (companyId, day) = change.Key;
        //            var newValue = change.Value;

        //            var record = await context.EmployeeCompagnyPricing
        //                .FirstOrDefaultAsync(r => r.CompanyId == companyId && r.Days == day);

        //            if (record != null)
        //            {
        //                record.Emplyeepaiment = newValue;
        //                record.ModifiedDate = DateTime.Now;
        //            }
        //        }

        //        await context.SaveChangesAsync();
        //    }
        //    */
        //    throw new NotImplementedException();
        //    Console.WriteLine($"Sauvegarde de {biWeeklyChanges.Count} changements BiWeekly...");

        //}


    }
}
