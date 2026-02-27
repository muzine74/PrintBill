using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessEmployee = DataBridge.Entity.EmployeePoco;
// Alias pour plus de clarté
using DbEmployee = DBConnection.Entity.Employee;
using Timer = System.Windows.Forms.Timer;

namespace SendBillWF.Work
{
    public partial class EmployeeSchedularUsCtr : UserControl
    {
        private List<TreeNode> _originalNodes;
        private BusinessEmployee _currentEmployee;
        private DbEmployee _currentEmployeeDb;
        private readonly EmployeeManipulation _employeeManipulation;
        private readonly Timer _searchDebounceTimer;

        private static readonly Color PrimaryColor = Color.FromArgb(52, 152, 219);
        private static readonly Color SecondaryColor = Color.FromArgb(46, 204, 113);

        public EmployeeSchedularUsCtr()
        {
            InitializeComponent();

            _employeeManipulation = new EmployeeManipulation();
            _searchDebounceTimer = new Timer { Interval = 300 };

            InitializeCustomComponents();
            SetupEventHandlers();

            LoadDataAsync();
        }

        private void InitializeCustomComponents()
        {
            UsersTreeView.Font = new Font("Segoe UI Emoji", 11, FontStyle.Regular);
            UsersTreeView.LineColor = PrimaryColor;

            _searchDebounceTimer.Tick += (s, e) =>
            {
                _searchDebounceTimer.Stop();
                FilterTreeView(SearchUserTxt.Text);
            };

            SaveWork.BackColor = SecondaryColor;
            SaveWork.ForeColor = Color.White;
            SaveWork.Text = "💾 Enregistrer";

            WeekToFill.Format = DateTimePickerFormat.Custom;
            WeekToFill.CustomFormat = "dddd d MMMM yyyy";

            SearchUserTxt.Font = new Font("Segoe UI Emoji", 10);
            SearchUserTxt.Text = "🔍 Rechercher un employé...";
            SearchUserTxt.ForeColor = Color.Gray;

            employeeUsSchedulair.DisableUserControl();
        }

        private void SetupEventHandlers()
        {
            UsersTreeView.AfterSelect += OnUserSelected;
            SearchUserTxt.TextChanged += OnSearchTextChanged;
            WeekToFill.ValueChanged += OnWeekChanged;
            SaveWork.Click += OnSaveWorkClicked;

            SearchUserTxt.Enter += (s, e) =>
            {
                if (SearchUserTxt.Text == "🔍 Rechercher un employé...")
                {
                    SearchUserTxt.Text = "";
                    SearchUserTxt.ForeColor = Color.Black;
                }
            };

            SearchUserTxt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(SearchUserTxt.Text))
                {
                    SearchUserTxt.Text = "🔍 Rechercher un employé...";
                    SearchUserTxt.ForeColor = Color.Gray;
                }
            };
        }

        private async void LoadDataAsync()
        {
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

        private void FillTreeViewFromDatabase(IEnumerable<DbEmployee> employees)
        {
            UsersTreeView.BeginUpdate();
            UsersTreeView.Nodes.Clear();

            var employeeList = employees.ToList();

            foreach (var employee in employeeList)
            {
                var node = CreateEmployeeNode(employee);
                UsersTreeView.Nodes.Add(node);
            }

            UsersTreeView.EndUpdate();

            _originalNodes = UsersTreeView.Nodes.Cast<TreeNode>()
                .Select(n => (TreeNode)n.Clone()).ToList();

            UsersTreeView.ExpandAll();
        }

        private TreeNode CreateEmployeeNode(DbEmployee employee)
        {
            return new TreeNode(employee.name)
            {
                Tag = employee.EmployeeId,
                ToolTipText = $"Email: {employee.EmployeeMail}\nTél: {employee.EmployeePhone}"
            };
        }

        private void ShowLoading(bool show)
        {
            this.Cursor = show ? Cursors.WaitCursor : Cursors.Default;
        }

        private void OnUserSelected(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag != null)
            {
                var employeeId = e.Node.Tag.ToString();
                LoadEmployeeData(employeeId);
            }
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private void OnWeekChanged(object sender, EventArgs e)
        {
            LoadWeeklyData();
        }

        private async void OnSaveWorkClicked(object sender, EventArgs e)
        {
            try
            {
                SaveWork.Enabled = false;
                SaveWork.Text = "⏳ Enregistrement...";

                await Task.Run(() => visitWorkSheetUctr1.SaveWorkLstChanege());
                visitWorkSheetUctr1.initValueEmployeeSchedular();

                MessageBox.Show("Données enregistrées avec succès !", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SaveWork.Enabled = true;
                SaveWork.Text = "💾 Enregistrer";
            }
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
                    UpdateEmployeeDisplay();
                    LoadWeeklyData();
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

        // Méthode de conversion si nécessaire
        private BusinessEmployee ConvertToBusinessEmployee(DbEmployee dbEmployee)
        {
            return new BusinessEmployee
            {
                EmployeeId = dbEmployee.EmployeeId,
                EmployeeName = dbEmployee.name,
                EmployeeMail = dbEmployee.EmployeeMail,
                EmployeePhone = dbEmployee.EmployeePhone
            };
        }
        private void UpdateEmployeeDisplay()
        {
            if (_currentEmployee != null)
            {
                employeeUsSchedulair.NameEmployee = _currentEmployee.EmployeeName;
                employeeUsSchedulair.MailEmployee = _currentEmployee.EmployeeMail;
                employeeUsSchedulair.PhoneEmployee = _currentEmployee.EmployeePhone;
            }
        }

        private void LoadWeeklyData()
        {
            //_currentEmployee plein ici mais l'appel  est vide a l'interieure de cette fonction
            if (_currentEmployee != null)
            {
                DateOnly selectedDate = DateOnly.FromDateTime(WeekToFill.Value);
                visitWorkSheetUctr1.initEmployee(_currentEmployee);
                visitWorkSheetUctr1.FillDaysDateWeek(selectedDate);
                visitWorkSheetUctr1.initValueEmployeeSchedular();
            }
        }

        private void FilterTreeView(string searchText)
        {
            if (_originalNodes == null) return;

            UsersTreeView.BeginUpdate();
            UsersTreeView.Nodes.Clear();

            if (string.IsNullOrWhiteSpace(searchText) || searchText == "🔍 Rechercher un employé...")
            {
                foreach (var node in _originalNodes)
                {
                    UsersTreeView.Nodes.Add((TreeNode)node.Clone());
                }
            }
            else
            {
                foreach (var node in _originalNodes)
                {
                    var filtered = FilterNode(node, searchText);
                    if (filtered != null)
                        UsersTreeView.Nodes.Add(filtered);
                }
            }

            UsersTreeView.EndUpdate();

            if (UsersTreeView.Nodes.Count > 0)
                UsersTreeView.ExpandAll();
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
    }
}