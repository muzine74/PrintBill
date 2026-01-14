using DataBridge;
using DataBridge.Entity;
using DBConnection;
using DBConnection.Entity;
using DBConnection.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SendBillWF.Work
{
    public partial class EmployeeSchedularUsCtr : UserControl
    {
        List<TreeNode> originalNodes;
        EmployeePoco employeePoco;

        EmployeeManipulation employeeManipulation;
        public EmployeeSchedularUsCtr()
        {
            InitializeComponent();
            employeeManipulation = new EmployeeManipulation();
            employeePoco = new EmployeePoco();
            UsersTreeView.AfterSelect += UsersTreeView_AfterSelect;
            FillTreeViewFromDatabase();
            SaveOriginalTree();
            employeeUsSchedulair.DisableUserControl();
        }

        private void SaveOriginalTree()
        {
            originalNodes = CloneNodes(UsersTreeView.Nodes);
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

        private void FilterTreeView(string searchText)
        {
            UsersTreeView.BeginUpdate();
            UsersTreeView.Nodes.Clear();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                UsersTreeView.Nodes.AddRange(originalNodes
                    .Select(n => (TreeNode)n.Clone())
                    .ToArray());
            }
            else
            {
                foreach (var node in originalNodes)
                {
                    var filtered = FilterNode(node, searchText);
                    if (filtered != null)
                        UsersTreeView.Nodes.Add(filtered);
                }
            }

            UsersTreeView.EndUpdate();
            UsersTreeView.ExpandAll();
        }

        private TreeNode FilterNode(TreeNode node, string searchText)
        {
            bool match = node.Text
                .IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;

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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterTreeView(SearchUserTxt.Text);
        }

        private void FillTreeViewFromDatabase()
        {
            UsersTreeView.BeginUpdate();
            UsersTreeView.Nodes.Clear();

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
                UsersTreeView.Nodes.Add(emplyeeNode);
            }

            UsersTreeView.EndUpdate();
        }

        private void UsersTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                var employeeId = new Guid(e.Node.Tag.ToString());

                //visitWorkSheetUctr1.updateCompagnyList();
                EmployeePoco employeePoco = employeeManipulation.GetEmployeeById(e.Node.Tag.ToString());
                visitWorkSheetUctr1.employeePoco = employeePoco;
                WeekToFillData();

                employeeUsSchedulair.NameEmployee = employeePoco.EmployeeName;
                employeeUsSchedulair.MailEmployee = employeePoco.EmployeeMail;
                employeeUsSchedulair.PhoneEmployee = employeePoco.EmployeePhone;

            }
        }

        private void WeekToFill_ValueChanged(object sender, EventArgs e)
        {
            WeekToFillData();
        }

        private void WeekToFillData()
        {
            List<DateTime> jours = new List<DateTime>();
            DateTime selectedDate = WeekToFill.Value;

            visitWorkSheetUctr1.FillDaysDateWeek(selectedDate);
            visitWorkSheetUctr1.initValueEmployeeSchedular();
        }

        private void SaveWork_Click(object sender, EventArgs e)
        {
            visitWorkSheetUctr1.SaveWorkLstChanege();
            visitWorkSheetUctr1.initValueEmployeeSchedular();
        }


    }
}
