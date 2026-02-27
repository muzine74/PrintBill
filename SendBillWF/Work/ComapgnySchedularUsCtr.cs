using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBridge;
using DataBridge.Entity;

namespace SendBillWF.Work
{
    public partial class ComapgnySchedularUsCtr : UserControl
    {
        CompagniePoco compagniePoco;
        CompagniManipulation compagniManipulation;
        List<TreeNode> originalNodes;


        public ComapgnySchedularUsCtr()
        {
            compagniePoco = new CompagniePoco();
            compagniManipulation = new CompagniManipulation();
            //compagnytreeView = new TreeView();


            InitializeComponent();
            CompagnytreeView.AfterSelect += CompagnytreeView_AfterSelect;
            
            FillCompagnyTreeViesFromDB();
            SaveOriginalTree();

        }

        void FillCompagnyTreeViesFromDB()
        {
            CompagnytreeView.BeginUpdate();
            CompagnytreeView.Nodes.Clear();

            var compagnies = compagniManipulation.getActiveCompagnies();

            foreach (var cmp in compagnies)
            {
                TreeNode cmpNode = new TreeNode(
                    $"{cmp.CompagnieName} "
                )
                {
                    Tag = cmp.CompagnieCode
                };
                CompagnytreeView.Nodes.Add(cmpNode);
            }
        }

        private void SaveOriginalTree()
        {
            originalNodes = CloneNodes(CompagnytreeView.Nodes);
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

        private void SearchCompagnyTxt_TextChanged(object sender, EventArgs e)
        {
           FilterTreeView(SearchCompagnyTxt.Text);
        }

        private void FilterTreeView(string searchText)
        {
            CompagnytreeView.BeginUpdate();
            CompagnytreeView.Nodes.Clear();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                CompagnytreeView.Nodes.AddRange(originalNodes
                    .Select(n => (TreeNode)n.Clone())
                    .ToArray());
            }
            else
            {
                foreach (var node in originalNodes)
                {
                    var filtered = FilterNode(node, searchText);
                    if (filtered != null)
                        CompagnytreeView.Nodes.Add(filtered);
                }
            }

            CompagnytreeView.EndUpdate();
            CompagnytreeView.ExpandAll();
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


        private void CompagnytreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                var compagnyCode = e.Node.Tag.ToString();

                //visitWorkSheetUctr1.updateEmployeeWorkList();
                CompagniePoco cmpPoco = compagniManipulation.GetCompagnyByCode(e.Node.Tag.ToString());
                visitWorkSheetUctr1.compagniePoco = cmpPoco;
                WeekToFillData();

                compagniBase.CompagnyBaseName = cmpPoco.CompagnieName;
                compagniBase.CompagnyBaseCode = cmpPoco.CompagnieCode;
                compagniBase.CompagnyBaseAdress = cmpPoco.CompagnieCivicNumber + "." + cmpPoco.CompagnieSuite + ""+ Environment.NewLine + cmpPoco.CompagnieZipCode + "" + cmpPoco.Compagniecity + "" + cmpPoco.CompagnieState ;
                compagniBase.CompagnyBaseIsActive = cmpPoco.CompagnieStatus;
            }
        }

        private void WeekToFillData()
        {
            List<DateOnly> jours = new List<DateOnly>();
            DateOnly selectedDate = DateOnly.FromDateTime(WeekToFill.Value);

            visitWorkSheetUctr1.FillDaysDateWeek(selectedDate);
            visitWorkSheetUctr1.initValueCompagnySchedular();   //  il faut faire la meme chsoe pour les compagnie
        }

        private void WeekToFill_ValueChanged(object sender, EventArgs e)
        {
            WeekToFillData();
        }


    }
}
