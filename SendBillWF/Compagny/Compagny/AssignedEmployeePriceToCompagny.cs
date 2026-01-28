using DataBridge;
using DataBridge.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        public AssignedEmployeePriceToCompagny()
        {
            InitializeComponent();
            employeePoco = new EmployeePoco();
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
            if (e.Node.Tag != null)
            {
                var employeeId = new Guid(e.Node.Tag.ToString());

                //visitWorkSheetUctr1.updateCompagnyList();
                EmployeePoco employeePoco = employeeManipulation.GetEmployeeById(e.Node.Tag.ToString());
                visitWorkSheetUctr1.employeePoco = employeePoco;
                WeekToFillData();

                //employeeUsSchedulair.NameEmployee = employeePoco.EmployeeName;
                //employeeUsSchedulair.MailEmployee = employeePoco.EmployeeMail;
                //employeeUsSchedulair.PhoneEmployee = employeePoco.EmployeePhone;

            }
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


    }
}
