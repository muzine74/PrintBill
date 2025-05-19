using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.itextpdf.text.pdf;
using DataBridge.Entity;
using SendBillWF.Employee;
using static iTextSharp.text.pdf.PdfDocument;

namespace SendBillWF
{
    public partial class RelocateSelectionItemUsCtr : UserControl
    {
        private List<CompagniePoco> sourceList = new List<CompagniePoco>();
        private List<CompagniePoco> destinationList = new List<CompagniePoco>();

        //EmployeePoco employeePoco 
        public RelocateSelectionItemUsCtr()
        {
            InitializeComponent();
        }

        private void FromListSourceToDestination_Click(object sender, EventArgs e)
        {
            // List<CompagniePoco> selectedValues = ItemListSource.SelectedItems.Cast<CompagniePoco>().ToList();

            //ItemListDestination.Items.AddRange(ItemListSource.SelectedItems);
            TransferSelectedItems(ItemListSource, ItemListDestination, sourceList, destinationList);
        }

        private void TransferSelectedItems(ListBox source, ListBox destination, List<CompagniePoco> sourceData, List<CompagniePoco> destinationData)
        {
            if (source.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select items to transfer");
                return;
            }

            var selectedItems = source.SelectedItems.Cast<CompagniePoco>().ToList();

            foreach (var item in selectedItems)
            {
                sourceData.Remove(item);
                destinationData.Add(item);
            }

            // Refresh the data source
            source.DataSource = null;
            source.DataSource = sourceData;
            source.DisplayMember = "CompagnieName";
            source.ValueMember = "CompagnieCode";

            destination.DataSource = null;
            destination.DataSource = destinationData;
            destination.DisplayMember = "CompagnieName";
            destination.ValueMember = "CompagnieCode";
        }


        private void FromListDestinationToSource_Click(object sender, EventArgs e)
        {
            TransferSelectedItems(ItemListDestination, ItemListSource, destinationList, sourceList);
        }

        public void FillSourceCompagnyByUser(List<CompagniePoco> source, List<CompagniePoco> destination)
        {

            sourceList = source;
            destinationList = destination;

            ItemListSource.DataSource = null;
            ItemListDestination.DataSource = null;

            ItemListSource.DataSource = sourceList;
            ItemListSource.DisplayMember = "CompagnieName";
            ItemListSource.ValueMember = "CompagnieCode";

            ItemListDestination.DataSource = destinationList;
            ItemListDestination.DisplayMember = "CompagnieName";
            ItemListDestination.ValueMember = "CompagnieCode";
        }

        private void ItemListDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemListSource.ClearSelected();
        }

        private void ItemListSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemListDestination.ClearSelected();
        }

        public List<CompagniePoco> GetSelectItemDestiniationList()
        {
            //List < CompagniePoco > SelectedItem = new List < CompagniePoco >();

             return destinationList;

        }
    }
}
