using DataBridge;
using DataBridge.Entity;
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
            List<EmployeeCompagnyPricingDto> employeeCompagnyPricingWekklyDto = new List<EmployeeCompagnyPricingDto>();
            List<EmployeeCompagnyPricingDto> employeeCompagnyPricingBiWeeklyDto = new List<EmployeeCompagnyPricingDto>();
            List<EmployeeCompagnyPricingDto> employeeCompagnyPricingBiWeeklyDtoTest = new List<EmployeeCompagnyPricingDto>();

            if (e.Node.Tag != null)
            {
                var employeeId = new Guid(e.Node.Tag.ToString());

                //visitWorkSheetUctr1.updateCompagnyList();
                EmployeePoco employeePoco = employeeManipulation.GetEmployeeById(e.Node.Tag.ToString());
                EmployeCompagny = compagniManipulation.GetCompagnyByEmployee(employeePoco.EmployeeId);

            }

          
            employeeCompagnyPricingBiWeeklyDtoTest = (from c in EmployeCompagny
                                                      join p in workManipulation.GetCompanyPricingCalendarsList(EmployeCompagny) on c.CompagnieID equals p.CompanyId
                                                      select new EmployeeCompagnyPricingDto
                                                      {
                                                          CompanyId = c.CompagnieID,
                                                          CompagnyCode = c.CompagnieCode,
                                                          CompagnyName = c.CompagnieName,
                                                          EmployeeId = employeePoco.EmployeeId,
                                                          Emplyeepaiment = p.Emplyeepaiment,
                                                          DaysStatus = p.DaysStatus,
                                                          Days = p.Days
                                                      }).ToList();

            LoadWeeklyDataGrid(employeeCompagnyPricingBiWeeklyDtoTest);

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
                    CompanyId = group.First().CompanyId.ToString(),
                    CompanyName = group.First().CompagnyName
                };

                // Remplir chaque jour séparément
                foreach (var record in group)
                {
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
                    //CompanyId = group.First().CompanyId.ToString(),
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

            // Ajouter les événements pour gérer l'édition
            
            ConfigureDataGridDisplay();
            AddCellEditEvents();
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
            WeeklyDtg.CellFormatting += (sender, e) =>
            {
                var dgv = sender as DataGridView;
                if (dgv != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var row = dgv.Rows[e.RowIndex];
                    var item = row.DataBoundItem as WeeklyDisplayItem;

                    if (item != null)
                    {
                        bool isEditable = true;
                        string columnName = dgv.Columns[e.ColumnIndex].Name;

                        // Même logique que pour CellBeginEdit
                        switch (columnName)
                        {
                            case "colMonday":
                                isEditable = item.IsMondayEditable && !string.IsNullOrEmpty(item.Monday);
                                break;
                                // ... autres jours
                        }

                        if (!isEditable)
                        {
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.Green;
                        }
                    }
                }
            };
        }

    }
}
