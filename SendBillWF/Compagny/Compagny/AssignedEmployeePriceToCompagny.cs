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


                //visitWorkSheetUctr1.employeePoco = employeePoco;
                //WeekToFillData();

                //employeeUsSchedulair.NameEmployee = employeePoco.EmployeeName;
                //employeeUsSchedulair.MailEmployee = employeePoco.EmployeeMail;
                //employeeUsSchedulair.PhoneEmployee = employeePoco.EmployeePhone;

            }

            //EmployeCompagnyWekly    = EmployeCompagny.Where(c => c.WorkFrequency == "Par visite"|| c.WorkFrequency == "Hebdomadaire").ToList();
            //EmployeCompagnyBiweekly = EmployeCompagny.Where(c => c.WorkFrequency == "Bi-hebdomadaire" || c.WorkFrequency == "Bi-mensuel").ToList();


            //// contient tous les prix active pour tous les compagny  relier a ce emplyè
            //var t = workManipulation.GetCompanyPricingCalendarsList(EmployeCompagny);


            EmployeCompagnyWekly = EmployeCompagny.Where(c => c.WorkFrequency == "Par visite" || c.WorkFrequency == "Hebdomadaire").ToList();
            EmployeCompagnyBiweekly = EmployeCompagny.Where(c => c.WorkFrequency == "Bi-hebdomadaire" || c.WorkFrequency == "Bi-mensuel").ToList();


            // contient tous les prix active pour tous les compagny  relier a ce emplyè
            var EmployeCompagnyWeekly = workManipulation.GetCompanyPricingCalendarsList(EmployeCompagnyWekly);
            var EmployeCompagnyBiwekly = workManipulation.GetCompanyPricingCalendarsList(EmployeCompagnyBiweekly);


            //il faut creer une class pour permet la fluidité des donnèes
            employeeCompagnyPricingWekklyDto = (from c in EmployeCompagnyWekly
                                                join p in EmployeCompagnyWeekly on c.CompagnieID equals p.CompanyId
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


            employeeCompagnyPricingBiWeeklyDto = (from c in EmployeCompagnyBiweekly
                                                  join p in EmployeCompagnyBiwekly on c.CompagnieID equals p.CompanyId
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

            //WeeklyDtg.Refresh();
            //BiWeeklyDtg.Refresh();








        }

        private void WeekToFillData()
        {
            //List<DateTime> jours = new List<DateTime>();
            //DateTime selectedDate = WeekToFill.Value;

            //visitWorkSheetUctr1.FillDaysDateWeek(selectedDate);
            //visitWorkSheetUctr1.initValueEmployeeSchedular();
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
                    CompanyId = group.First().CompanyId.ToString(),  // ICI: Conversion en string
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
                    CompanyId = group.First().CompanyId.ToString(),  // ICI: Conversion en string
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
                        case "BiWeek1_Monday": item.BiWeek1_Monday = value; break;
                        case "BiWeek1_Tuesday": item.BiWeek1_Tuesday = value; break;
                        case "BiWeek1_Wednesday": item.BiWeek1_Wednesday = value; break;
                        case "BiWeek1_Thursday": item.BiWeek1_Thursday = value; break;
                        case "BiWeek1_Friday": item.BiWeek1_Friday = value; break;
                        case "BiWeek1_Saturday": item.BiWeek1_Saturday = value; break;
                        case "BiWeek1_Sunday": item.BiWeek1_Sunday = value; break;
                        case "BiWeek2_Monday": item.BiWeek2_Monday = value; break;
                        case "BiWeek2_Tuesday": item.BiWeek2_Tuesday = value; break;
                        case "BiWeek2_Wednesday": item.BiWeek2_Wednesday = value; break;
                        case "BiWeek2_Thursday": item.BiWeek2_Thursday = value; break;
                        case "BiWeek2_Friday": item.BiWeek2_Friday = value; break;
                        case "BiWeek2_Saturday": item.BiWeek2_Saturday = value; break;
                        case "BiWeek2_Sunday": item.BiWeek2_Sunday = value; break;
                    }
                }

                biWeeklyList.Add(item);
            }

            ConfigureDataGridDisplay();
            // Définir les DataSource
            WeeklyDtg.DataSource = null;
            WeeklyDtg.DataSource = weeklyList;

            BiWeeklyDtg.DataSource = null;
            BiWeeklyDtg.DataSource = biWeeklyList;

            // Configurer l'affichage des colonnes
            

            // Rafraîchir
            WeeklyDtg.Refresh();
            BiWeeklyDtg.Refresh();
        }

        private void ConfigureDataGridDisplay()
        {
            // Pour afficher CompanyId
            WeeklyDtg.AutoGenerateColumns = false;
            WeeklyDtg.Columns.Clear();

            // CHOIX: Afficher ou masquer CompanyId


            // Option B: Masquer CompanyId - ne pas ajouter cette colonne
            // OU l'ajouter et la masquer:
            // WeeklyDtg.Columns["colCompanyId"].Visible = false;

            WeeklyDtg.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Company Name",
                DataPropertyName = "CompanyName",
                Name = "colCompanyName",
                Width = 150,
                ReadOnly = true
            });

            // Ajouter les jours
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            foreach (var day in days)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    HeaderText = day,
                    DataPropertyName = day,
                    Name = "col" + day,
                    Width = 100
                };
                WeeklyDtg.Columns.Add(column);
            }

            // Configurer BiWeekly de la même manière
            ConfigureBiWeeklyGrid();
        }

        private void ConfigureBiWeeklyGrid()
        {
            // Désactiver l'auto-génération pour contrôler manuellement
            BiWeeklyDtg.AutoGenerateColumns = false;
            BiWeeklyDtg.Columns.Clear();

            // OPTION 1: Afficher CompanyId (décommentez si nécessaire)
            /*
            BiWeeklyDtg.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Company ID",
                DataPropertyName = "CompanyId",
                Name = "colBiCompanyId",
                Width = 150,
                ReadOnly = true,
                Visible = true // Changez à false pour masquer
            });
            */

            // Colonne Company Name
            BiWeeklyDtg.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Company Name",
                DataPropertyName = "CompanyName",
                Name = "colBiCompanyName",
                Width = 100,
                ReadOnly = true
            });

            // Ajouter les colonnes pour BiWeek 1
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek1_Monday", "1 - Monday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek1_Tuesday", "1 - Tuesday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek1_Wednesday", "1 - Wednesday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek1_Thursday", "1 - Thursday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek1_Friday", "1 - Friday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek1_Saturday", "1 - Saturday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek1_Sunday", "1 - Sunday"));

            // Ajouter les colonnes pour BiWeek 2
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek2_Monday", "2 - Monday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek2_Tuesday", "2 - Tuesday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek2_Wednesday", "2 - Wednesday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek2_Thursday", "2 - Thursday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek2_Friday", "2 - Friday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek2_Saturday", "2 - Saturday"));
            BiWeeklyDtg.Columns.Add(CreateBiWeekColumn("BiWeek2_Sunday", "2 - Sunday"));
        }

        private DataGridViewColumn CreateBiWeekColumn(string dataPropertyName, string headerText)
        {
            return new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                Name = "col" + dataPropertyName,
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2" // Format numérique avec 2 décimales
                }
            };
        }




    }



}
