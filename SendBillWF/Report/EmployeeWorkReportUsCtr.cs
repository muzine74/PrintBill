using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using SendBillWF.Report.EmployeeWork;
using SendBillWF.Report.EmployeeWorkReport;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessEmployee = DataBridge.Entity.EmployeePoco;
// Alias pour plus de clarté
using DbEmployee = DBConnection.Entity.Employee;
using Timer = System.Windows.Forms.Timer;

namespace SendBillWF.Report
{
    public partial class EmployeeWorkReportUsCtr : UserControl
    {
        private List<TreeNode> _originalNodes;
        private BusinessEmployee _currentEmployee;
        private readonly Timer _searchDebounceTimer;
        private readonly EmployeeManipulation _employeeManipulation;
        private Guid employeeId;

        private static readonly Color PrimaryColor = Color.FromArgb(52, 152, 219);
        private static readonly Color SecondaryColor = Color.FromArgb(46, 204, 113);

        public EmployeeWorkReportUsCtr()
        {
            _employeeManipulation = new EmployeeManipulation();
            _searchDebounceTimer = new Timer { Interval = 300 };

            InitializeComponent();

            // CRÉER LE PANEL RAPPORT
            //Panel panelRapport = new Panel
            //{
            //    Dock = DockStyle.Fill,
            //    Name = "panelRapport",
            //    BackColor = Color.White // Optionnel, pour visibilité
            //};
            this.Controls.Add(panelRapport);

            // CRÉER ET CONFIGURER LE CONTROLE DE RAPPORT
            employeeWorkRepUctr1 = new EmployeeWorkRepUctr();
            employeeWorkRepUctr1.Name = "employeeWorkRepUctr1";
            employeeWorkRepUctr1.Dock = DockStyle.Fill;
            employeeWorkRepUctr1.Visible = false;

            // AJOUTER LE CONTROLE AU PANEL
            panelRapport.Controls.Add(employeeWorkRepUctr1);

            InitializeCustomComponents();
            SetupEventHandlers();

            LoadDataAsync();
        }
        private void SetupEventHandlers()
        {
            
            UsersTreeViewReport.AfterSelect += OnUserSelected;
            SearchUserReportTxt.TextChanged += OnSearchTextChanged;


            SearchUserReportTxt.Enter += (s, e) =>
            {
                if (SearchUserReportTxt.Text == "🔍 Rechercher un employé...")
                {
                    SearchUserReportTxt.Text = "";
                    SearchUserReportTxt.ForeColor = Color.Black;
                }
            };

            SearchUserReportTxt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(SearchUserReportTxt.Text))
                {
                    SearchUserReportTxt.Text = "🔍 Rechercher un employé...";
                    SearchUserReportTxt.ForeColor = Color.Gray;
                }
            };
        }

        private void InitializeCustomComponents()
        {
            UsersTreeViewReport.Font = new Font("Segoe UI Emoji", 11, FontStyle.Regular);
            UsersTreeViewReport.LineColor = PrimaryColor;

            _searchDebounceTimer.Tick += (s, e) =>
            {
                try
                {
                    // Log entry
                    System.Diagnostics.Debug.WriteLine("Timer tick started");

                    // Stop the timer first
                    if (_searchDebounceTimer != null)
                    {
                        _searchDebounceTimer.Stop();
                        System.Diagnostics.Debug.WriteLine("Timer stopped");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("CRITICAL: _searchDebounceTimer is null!");
                        return;
                    }

                    // Check each object individually with detailed logging
                    System.Diagnostics.Debug.WriteLine($"this.IsHandleCreated: {this.IsHandleCreated}");

                    if (_originalNodes == null)
                    {
                        System.Diagnostics.Debug.WriteLine("_originalNodes is null");
                        // Don't return yet, continue checking other objects
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"_originalNodes count: {_originalNodes.Count}");
                    }

                    if (SearchUserReportTxt == null)
                    {
                        System.Diagnostics.Debug.WriteLine("SearchUserReportTxt is null");
                        return;
                    }

                    System.Diagnostics.Debug.WriteLine($"SearchUserReportTxt.IsDisposed: {SearchUserReportTxt.IsDisposed}");

                    if (SearchUserReportTxt.IsDisposed)
                    {
                        System.Diagnostics.Debug.WriteLine("SearchUserReportTxt is disposed");
                        return;
                    }

                    // Safe text retrieval
                    string searchText = SearchUserReportTxt.Text ?? string.Empty;
                    System.Diagnostics.Debug.WriteLine($"Search text: '{searchText}'");

                    // Use BeginInvoke to ensure UI thread safety
                    if (this.IsHandleCreated)
                    {
                        System.Diagnostics.Debug.WriteLine("Invoking BeginInvoke");
                        this.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine("BeginInvoke executing FilterTreeView");

                                // Additional check inside BeginInvoke
                                if (_originalNodes == null)
                                {
                                    System.Diagnostics.Debug.WriteLine("_originalNodes became null before FilterTreeView");
                                    return;
                                }

                                FilterTreeView(searchText);
                                System.Diagnostics.Debug.WriteLine("FilterTreeView completed");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error in filter operation: {ex.Message}");
                                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                            }
                        }));
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Handle not created, cannot BeginInvoke");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Critical error in timer tick: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                }
            };


        }

        private void FilterTreeView(string searchText)
        {
            if (_originalNodes == null) return;

            UsersTreeViewReport.BeginUpdate();
            UsersTreeViewReport.Nodes.Clear();

            if (string.IsNullOrWhiteSpace(searchText) || searchText == "🔍 Rechercher un employé...")
            {
                foreach (var node in _originalNodes)
                {
                    UsersTreeViewReport.Nodes.Add((TreeNode)node.Clone());
                }
            }
            else
            {
                foreach (var node in _originalNodes)
                {
                    var filtered = FilterNode(node, searchText);
                    if (filtered != null)
                        UsersTreeViewReport.Nodes.Add(filtered);
                }
            }

            UsersTreeViewReport.EndUpdate();

            if (UsersTreeViewReport.Nodes.Count > 0)
                UsersTreeViewReport.ExpandAll();
        }

        private TreeNode FilterNode(TreeNode node, string searchText)
        {
            bool match = node.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;

            TreeNode newNode = (TreeNode)node.Clone();
            newNode.Nodes.Clear();

            foreach (TreeNode child in node.Nodes)
            {
                TreeNode filteredChild = FilterNode(child, searchText);
                if (filteredChild != null)
                    newNode.Nodes.Add(filteredChild);
            }

            return (match || newNode.Nodes.Count > 0) ? newNode : null;
        }

        private async void LoadDataAsync()
        {
            DateTimePickerFiltre.Visible = false;

            try
            {
                ShowLoading(true);
                var employees = await Task.Run(() => _employeeManipulation.GetEmployee());
                FillTreeViewFromDatabase(employees);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void ShowLoading(bool show)
        {
            this.Cursor = show ? Cursors.WaitCursor : Cursors.Default;
        }

        private void FillTreeViewFromDatabase(IEnumerable<DbEmployee> employees)
        {
            UsersTreeViewReport.BeginUpdate();
            UsersTreeViewReport.Nodes.Clear();

            var employeeList = employees.ToList();

            foreach (var employee in employeeList)
            {
                var node = CreateEmployeeNode(employee);
                UsersTreeViewReport.Nodes.Add(node);
            }

            UsersTreeViewReport.EndUpdate();

            _originalNodes = UsersTreeViewReport.Nodes.Cast<TreeNode>()
                .Select(n => (TreeNode)n.Clone()).ToList();

            UsersTreeViewReport.ExpandAll();
        }

        private TreeNode CreateEmployeeNode(DbEmployee employee)
        {
            return new TreeNode(employee.name)
            {
                Tag = employee.EmployeeId,
                ToolTipText = $"Email: {employee.EmployeeMail}\nTél: {employee.EmployeePhone}"
            };
        }

        private void OnUserSelected(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag != null)
            {
                employeeId = ConvertStringToGuid(e.Node.Tag.ToString());

                displayDateFiltertoDiplay();
                //LoadEmployeeData(employeeId);
            }
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private void LoadEmployeeData(string employeeId)
        {
            try
            {
                ShowLoading(true);

                // GetEmployeeById retourne EmployeePoco directement
                _currentEmployee = _employeeManipulation.GetEmployeeById(employeeId);

                if (_currentEmployee != null)
                {
                    //UpdateEmployeeDisplay();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement de l'employé : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void displayDateFiltertoDiplay()
        {
            DateTimePickerFiltre.Show();
            employeeWorkRepUctr1.Visible = false; // Cacher le rapport quand on change d'employé
        }

        private void Display_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Display_Click - Début");
            System.Diagnostics.Debug.WriteLine($"employeeWorkRepUctr1 is null: {employeeWorkRepUctr1 == null}");

            if (employeeWorkRepUctr1 != null)
            {
                ReportDTO reportDTO = new ReportDTO
                {
                    ReportName = _employeeManipulation.GetEmployeeById(employeeId.ToString()).EmployeeName,
                    ReportId = employeeId,
                    BeginDate = DateOnly.FromDateTime(BeguinDate.Value),
                    EndDate = DateOnly.FromDateTime(EndDate.Value)
                };

                employeeWorkRepUctr1.ChargerDonnees(reportDTO);

                // Cacher les autres contrôles si nécessaire
                UsersTreeViewReport.Visible = true;
                SearchUserReportTxt.Visible = true;
                // ... etc.

                // Afficher le rapport
                employeeWorkRepUctr1.BringToFront();
                employeeWorkRepUctr1.Visible = true;
            }
        }
        public Guid ConvertStringToGuid(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return Guid.Empty;

            if (Guid.TryParse(str, out Guid result))
                return result;

            // Try different formats
            if (Guid.TryParseExact(str, "N", out result))
                return result;

            if (Guid.TryParseExact(str, "B", out result))
                return result;

            if (Guid.TryParseExact(str, "P", out result))
                return result;

            return Guid.Empty;
        }

    }
}
