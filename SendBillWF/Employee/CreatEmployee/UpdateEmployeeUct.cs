using DataBridge;
using DataBridge.Entity;
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

namespace SendBillWF.Employee.CreatEmployee
{
    public partial class UpdateEmployeeUct : UserControl
    {

        EmployeeManipulation employeeManipulation;
        CompagniManipulation compagniManipulation;
        List<CompagniePoco> destinationCompagnirByUserList;
        EmployeeInfoUsCtr employeeInfoUsCtr;

        public UpdateEmployeeUct()
        {
            InitializeComponent();
            employeeManipulation = new EmployeeManipulation();
            compagniManipulation = new CompagniManipulation();
            destinationCompagnirByUserList = new List<CompagniePoco>();
            employeeInfoUsCtr = new EmployeeInfoUsCtr();

            EmployeeTreeView.BeforeExpand += EmployeeTreeView_BeforeExpand;
            EmployeSearchTxt.TextChanged += txtSearch_TextChanged;
            fillEmployeeTreeView();
        }

        public void fillEmployeeTreeView()
        {
            var empList = employeeManipulation.GetAllEmplyee();
            EmployeeTreeView.AfterSelect += UpdateUserTreeView_AfterSelect;
            FillTreeViewRoot(EmployeeTreeView, empList);

        }

        private void FillTreeViewRoot(System.Windows.Forms.TreeView treeView, List<EmployeePoco> empList)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();

            foreach (var emp in empList)
            {
                TreeNode employeeNode = new TreeNode(emp.EmployeeName)
                {
                    Tag = emp // stocker directement EmployeePoco
                };

                // Placeholder pour lazy loading
                employeeNode.Nodes.Add(new TreeNode("Loading..."));

                treeView.Nodes.Add(employeeNode);
            }

            treeView.EndUpdate();

            //CollapseAllExceptRoot(EmployeeTreeView);            
        }

        private void EmployeeTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "Loading...")
            {
                e.Node.Nodes.Clear();

                if (e.Node.Tag is EmployeePoco emp)
                {
                    // ---- Informations
                    TreeNode infoNode = new TreeNode("Informations");
                    infoNode.Nodes.Add(new TreeNode($"NAS : {emp.NAS}") { Tag = emp.NAS });
                    infoNode.Nodes.Add(new TreeNode($"Email : {emp.EmployeeMail}") { Tag = emp.EmployeeMail });
                    infoNode.Nodes.Add(new TreeNode($"Téléphone : {emp.EmployeePhone}") { Tag = emp.EmployeePhone });
                    e.Node.Nodes.Add(infoNode);

                    // ---- Adresse
                    TreeNode addressNode = new TreeNode("Adresse");
                    addressNode.Nodes.Add(new TreeNode($"Rue : {emp.EmployeeCivicNumber}"));
                    addressNode.Nodes.Add(new TreeNode($"Ville : {emp.EmployeeCity}"));
                    addressNode.Nodes.Add(new TreeNode($"Code postal : {emp.EmployeeZipCode}"));
                    addressNode.Nodes.Add(new TreeNode($"Pays : {emp.EmployeeCountry}"));
                    e.Node.Nodes.Add(addressNode);

                    // ---- Compagnies
                    TreeNode companiesNode = new TreeNode("Compagnies");
                    foreach (var comp in emp.EmployeeCompagnies)
                    {
                        TreeNode companyNode = new TreeNode(comp.CompagnieName)
                        {
                            Tag = comp.CompagnieID
                        };
                        companiesNode.Nodes.Add(companyNode);
                    }
                    e.Node.Nodes.Add(companiesNode);
                }
            }
        }

        private bool SearchHighlightAndCollapse(TreeNode node, string searchText, ref TreeNode firstFound)
        {
            // Reset visuel
            node.BackColor = Color.White;
            node.ForeColor = Color.Black;

            // Lazy loading si nécessaire
            EnsureNodeLoaded(node);

            bool selfMatch =
                !string.IsNullOrEmpty(searchText) &&
                node.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;

            bool childMatch = false;

            // Recherche récursive dans les enfants
            foreach (TreeNode child in node.Nodes)
            {
                if (SearchHighlightAndCollapse(child, searchText, ref firstFound))
                {
                    childMatch = true;
                }
            }

            // Si ce nœud match
            if (selfMatch)
            {
                node.BackColor = Color.LightYellow;
                node.ForeColor = Color.Red;

                if (firstFound == null)
                    firstFound = node;
            }

            bool hasMatch = selfMatch || childMatch;

            // Gestion expansion / collapse
            if (hasMatch && !string.IsNullOrEmpty(searchText))
            {
                node.Expand();
            }
            else
            {
                node.Collapse();
            }

            return hasMatch;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = EmployeSearchTxt.Text.Trim();
            TreeNode firstFound = null;

            EmployeeTreeView.BeginUpdate();

            if (string.IsNullOrEmpty(searchText))
            {
                // Effacer les surlignages
                ClearHighlight(EmployeeTreeView.Nodes);

                // Replier tout
                EmployeeTreeView.CollapseAll();

                // Optionnel : désélectionner
                EmployeeTreeView.SelectedNode = null;
            }
            else
            {
                foreach (TreeNode root in EmployeeTreeView.Nodes)
                {
                    SearchHighlightAndCollapse(root, searchText, ref firstFound);
                }

                if (firstFound != null)
                {
                    EmployeeTreeView.SelectedNode = firstFound;
                    firstFound.EnsureVisible();
                }
            }

            EmployeeTreeView.EndUpdate();
        }


        private void UpdateUserTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;

            // Remonter jusqu'à la racine
            TreeNode rootNode = GetRootNode(selectedNode);

            if (rootNode?.Tag is EmployeePoco emp)
            {
                // Tu as l'employé racine
                //Guid employeeId = emp.EmployeeId;
                //string employeeName = emp.EmployeeName;

                //MessageBox.Show(
                //    $"Employé : {employeeName}\nID : {employeeId}",
                //    "Noeud racine"
                //);

                FillemplyeeToUpdate(emp);

                
            }
        }

        private TreeNode GetRootNode(TreeNode node)
        {
            while (node.Parent != null)
            {
                node = node.Parent;
            }
            return node;
        }

        private void FillemplyeeToUpdate(EmployeePoco emp)
        {
            employeeInfoUpDateUsCtr1.NasTxtDisable();
            employeeInfoUpDateUsCtr1.employeeId = emp.EmployeeId;
            employeeInfoUpDateUsCtr1.addressId = emp.AddressId;            
            employeeInfoUpDateUsCtr1.NasEmployeeSaisi = emp.NAS;
            employeeInfoUpDateUsCtr1.NameEmployeeSaisi = emp.EmployeeName;
            employeeInfoUpDateUsCtr1.MailEmployeeSaisi = emp.EmployeeMail;
            employeeInfoUpDateUsCtr1.PhoneEmployeeSaisi = emp.EmployeePhone;
            employeeInfoUpDateUsCtr1.NoteEmployeeSaisi = emp.EmployeeNote;
            employeeInfoUpDateUsCtr1.CivicNumberAdressSaisi = emp.EmployeeCivicNumber;
            employeeInfoUpDateUsCtr1.SuiteAdressSaisi = emp.EmployeeSuite;
            employeeInfoUpDateUsCtr1.CityAdressSaisi = emp.EmployeeCity;
            employeeInfoUpDateUsCtr1.StateAdressSaisi = emp.EmployeeState;
            employeeInfoUpDateUsCtr1.CountryAdressSaisi = emp.EmployeeCountry;
            employeeInfoUpDateUsCtr1.ZipCodeAdressSaisi = emp.EmployeeZipCode;
            employeeInfoUpDateUsCtr1.NoteAdressSaisi = emp.EmployeeAdressNote;

            destinationCompagnirByUserList.Clear();
            destinationCompagnirByUserList = emp.EmployeeCompagnies
                                                .Select(c => new CompagniePoco
                                                {
                                                    CompagnieID = c.CompagnieID,
                                                    CompagnieName = c.CompagnieName
                                                })
                                                .ToList();


            FillSourceCompagnyByUser();

            //destinationCompagnirByUserList
        }

        //private void ExpandNodeWithParents(TreeNode node)
        //{
        //    TreeNode current = node;
        //    while (current != null)
        //    {
        //        current.Expand();
        //        current = current.Parent;
        //    }
        //}

        private void EnsureNodeLoaded(TreeNode node)
        {
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "Loading...")
            {
                node.Expand();   // déclenche BeforeExpand
                node.Collapse();
            }
        }

        private void ClearHighlight(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.BackColor = Color.White;
                node.ForeColor = Color.Black;

                if (node.Nodes.Count > 0)
                    ClearHighlight(node.Nodes);
            }
        }


        // Mnipulation compagny

        public List<CompagniePoco> GetAllCompagnies()
        {
            return compagniManipulation.GetCompagieInfo();
        }

        public void FillSourceCompagnyByUser()
        {
            List<CompagniePoco> AllCompagnList = GetAllCompagnies();
            List<CompagniePoco> sourceCompagnyForUser = new List<CompagniePoco>();

            var ids = destinationCompagnirByUserList.Select(x => x.CompagnieID).ToList();



            sourceCompagnyForUser = AllCompagnList.Where(c => !ids.Contains(c.CompagnieID)).ToList();//.Value
            employeeInfoUpDateUsCtr1.FillSourceCompagnyByUser(sourceCompagnyForUser, destinationCompagnirByUserList);
        }

        private void UpdateEmployee_Click(object sender, EventArgs e)
        {

            //    employeeManipulation.UpdateEmployee(
            //employeeInfoUpDateUsCtr1.SaveEmployee());


            //    var employeeId = employeeInfoUpDateUsCtr1.employeeId;
            //    var refreshedEmployee = employeeManipulation.GetEmployeeById(employeeId.ToString());

            //RefreshEmployeeUI(employeeId);

            employeeManipulation.UpdateEmployee(employeeInfoUpDateUsCtr1.SaveEmployee());

            fillEmployeeTreeView(); // 🔥 solution fiable

            MessageBox.Show("Employé mis à jour avec succès.");

        }

        private void RefreshEmployeeNode(EmployeePoco updatedEmp)
        {
            foreach (TreeNode node in EmployeeTreeView.Nodes)
            {
                if (node.Tag is EmployeePoco emp &&
                    emp.EmployeeId == updatedEmp.EmployeeId)
                {
                    node.Tag = updatedEmp;
                    node.Text = updatedEmp.EmployeeName;

                    node.Nodes.Clear();
                    node.Nodes.Add(new TreeNode("Loading..."));

                    node.Expand();
                    node.Collapse();

                    EmployeeTreeView.SelectedNode = node;
                    node.EnsureVisible();
                    break;
                }
            }
        }

        private void RefreshEmployeeUI(Guid employeeId)
        {
            var emp = employeeManipulation.GetEmployeeById(employeeId.ToString());
            FillemplyeeToUpdate(emp);
            RefreshEmployeeNode(emp);
        }

    }
}
