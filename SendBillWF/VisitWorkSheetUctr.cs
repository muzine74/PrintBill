using DataBridge;
using DataBridge.Entity;
using Helpers.PocoGrid;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.util.collections;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SendBillWF
{
    public partial class VisitWorkSheetUctr : UserControl
    {
        //List<DateTime> jours = new List<DateTime>();
        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("fr-FR");
        WorkManipulation workManipulation = new WorkManipulation();
        CompagniManipulation compagniManipulation = new CompagniManipulation();

        public EmployeePoco employeePoco ;
        public CompagniePoco compagniePoco;


        public VisitWorkSheetUctr()
        {
            InitializeComponent();
            employeePoco = new EmployeePoco();
            compagniePoco = new CompagniePoco();

            WorkVisiteDgv.CellValueChanged += WorkVisiteDgv_CellValueChanged;
            WorkVisiteDgv.CurrentCellDirtyStateChanged += WorkVisiteDgv_CurrentCellDirtyStateChanged;

            //initValue();

        }


        public void initValueEmployeeSchedular()
        {
            WorkVisiteDgv.Rows.Clear();
            WorkVisiteDgv.Columns.Clear();
            WorkVisiteDgv.Refresh();

            workManipulation.initValuefromEmployeeId(employeePoco);

            var horaire = LoadWorkedCompagyDate();
            CreerCompagnyColonnes(workManipulation.jours);
            //CreerEmployeeColonnes(workManipulation.jours);
            ChargerCompagnies(workManipulation.jours, horaire);

        }

        public void initValueCompagnySchedular()
        {
            WorkVisiteDgv.Rows.Clear();
            WorkVisiteDgv.Columns.Clear();
            WorkVisiteDgv.Refresh();

            workManipulation.initValuefromCompagnyId(compagniePoco);

            var horaire = LoadWorkedEmployeeDate();
            //CreerCompagnyColonnes(workManipulation.jours);
            CreerEmployeeColonnes(workManipulation.jours);
            ChargerEmployee(workManipulation.jours, horaire);

        }

        public void updateCompagnyList()
        {
            workManipulation.GetCompagnyAssinedToEmployeeList();
            //workManipulation.GetEmployeeAssinedCompanyList();
        }

        public void updateEmployeeWorkList()
        {
            //workManipulation.GetCompagnyAssinedToEmployeeList();
            workManipulation.GetEmployeeAssinedToCompanyList();
        }

        Dictionary<string, HashSet<string>> LoadWorkedCompagyDate()
        {

            //HashSet<string> val1 = new HashSet<string>();
            List<string> val1 = new List<string>();

            Dictionary<string, HashSet<string>> keyValuePairsfinal = new Dictionary<string, HashSet<string>>();


            foreach (var cl in workManipulation.workPoco.companyLst)
            {


                var wcompany = workManipulation.workPoco.workLst.Where(w => w.CompanyId == cl.CompanyId).ToList();
                if (wcompany.Count != 0)
                {
                    foreach (var wc in wcompany)
                    {
                        val1.Add(wc.Workdate);
                    }

                    HashSet<string> hset = new HashSet<string>();
                    hset.UnionWith(val1.ToList());


                    keyValuePairsfinal.Add(cl.companyCode, hset);
                    val1.Clear();
                }
                else
                {
                    keyValuePairsfinal.Add(cl.companyCode, new HashSet<string>());
                }

            }

            return keyValuePairsfinal;
        }

         Dictionary<string, HashSet<string>> LoadWorkedEmployeeDate()
        {

            //HashSet<string> val1 = new HashSet<string>();
            List<string> val1 = new List<string>();

            Dictionary<string, HashSet<string>> keyValuePairsfinal = new Dictionary<string, HashSet<string>>();


            foreach (var cl in workManipulation.workPoco.employeeLst)
            {


                var wcompany = workManipulation.workPoco.workLst.Where(w => w.EmployeeId == cl.EmployeeId).ToList();
                if (wcompany.Count != 0)
                {
                    foreach (var wc in wcompany)
                    {
                        val1.Add(wc.Workdate);
                    }

                    HashSet<string> hset = new HashSet<string>();
                    hset.UnionWith(val1.ToList());


                    keyValuePairsfinal.Add(cl.name, hset);
                    val1.Clear();
                }
                else
                {
                    keyValuePairsfinal.Add(cl.name, new HashSet<string>());
                }

            }

            return keyValuePairsfinal;
        }

        private void CreerCompagnyColonnes(List<DateTime> jours)
        {

            WorkVisiteDgv.Columns.Clear();

            WorkVisiteDgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Compagnie",
                HeaderText = "Compagnie",
                ReadOnly = true,
                Width = 220
            });

            foreach (var jour in jours)
            {
                WorkVisiteDgv.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = jour.ToString("ddMMyyyy", culture),
                    HeaderText = jour.ToString("ddd dd MM yyyy", culture),
                    Width = 100,
                    Tag = "clearCheckBoxDatagrid"
                });
            }

            // Empêche l'ajout de nouvelles lignes par l'utilisateur
            WorkVisiteDgv.AllowUserToAddRows = false;
        }



        private void CreerEmployeeColonnes(List<DateTime> jours)
        {

            WorkVisiteDgv.Columns.Clear();

            WorkVisiteDgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Employee",
                HeaderText = "Employee",
                ReadOnly = true,
                Width = 220
            });

            foreach (var jour in jours)
            {
                WorkVisiteDgv.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = jour.ToString("ddMMyyyy", culture),
                    HeaderText = jour.ToString("ddd dd MM yyyy", culture),
                    Width = 100,
                    Tag = "clearCheckBoxDatagrid"
                });
            }

            // Empêche l'ajout de nouvelles lignes par l'utilisateur
            WorkVisiteDgv.AllowUserToAddRows = false;
        }

        private void ChargerCompagnies(List<DateTime> jours, Dictionary<string, HashSet<string>> horaire)
        {
            foreach (var item in horaire)
            {
                int rowIndex = WorkVisiteDgv.Rows.Add();
                var row = WorkVisiteDgv.Rows[rowIndex];

                string compagnie = item.Key;
                row.Cells["Compagnie"].Value = compagnie;

                for (int i = 0; i < jours.Count; i++)
                {
                    row.Cells[jours[i].ToString("ddMMyyyy", culture)].Value = item.Value.Contains(jours[i].ToString("ddMMyyyy", culture));

                    bool isChecked = Convert.ToBoolean(row.Cells[jours[i].ToString("ddMMyyyy", culture)].Value);


                    if (isChecked)
                    {
                        row.Cells[jours[i].ToString("ddMMyyyy", culture)].Style.BackColor = Color.Silver;
                    }
                    else
                    {
                        row.Cells[jours[i].ToString("ddMMyyyy", culture)].Style.BackColor = Color.White;
                    }
                }
            }
        }

        private void ChargerEmployee(List<DateTime> jours, Dictionary<string, HashSet<string>> horaire)
        {
            foreach (var item in horaire)
            {
                int rowIndex = WorkVisiteDgv.Rows.Add();
                var row = WorkVisiteDgv.Rows[rowIndex];

                string employee = item.Key;
                row.Cells["Employee"].Value = employee;

                for (int i = 0; i < jours.Count; i++)
                {
                    row.Cells[jours[i].ToString("ddMMyyyy", culture)].Value = item.Value.Contains(jours[i].ToString("ddMMyyyy", culture));

                    bool isChecked = Convert.ToBoolean(row.Cells[jours[i].ToString("ddMMyyyy", culture)].Value);


                    if (isChecked)
                    {
                        row.Cells[jours[i].ToString("ddMMyyyy", culture)].Style.BackColor = Color.Silver;
                    }
                    else
                    {
                        row.Cells[jours[i].ToString("ddMMyyyy", culture)].Style.BackColor = Color.White;
                    }
                }
            }
        }

        public void initEmployee(EmployeePoco _employeePoco)
        {
            employeePoco = _employeePoco;
        }
        public void FillDaysDateWeek(DateTime selectedDate)
        {
            ClearAllCheckBoxes();

            
            // 1. D'abord récupérer la semaine
            workManipulation.GetWeek(selectedDate);

            // 2. Pour l'affichage employé (vue compagnie)
            if (employeePoco != null && employeePoco.EmployeeId != Guid.Empty)
            {
                workManipulation.initValuefromEmployeeId(employeePoco);
                var horaire = LoadWorkedCompagyDate();
                CreerCompagnyColonnes(workManipulation.jours);
                ChargerCompagnies(workManipulation.jours, horaire);
            }
            // 3. Pour l'affichage compagnie (vue employé)
            else if (compagniePoco != null && compagniePoco.CompagnieID != Guid.Empty)
            {
                workManipulation.initValuefromCompagnyId(compagniePoco);
                var horaire = LoadWorkedEmployeeDate();
                CreerEmployeeColonnes(workManipulation.jours);
                ChargerEmployee(workManipulation.jours, horaire);
            }
        }
        private void ClearAllCheckBoxes()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is CheckBox cb && cb.Tag?.ToString() == "clearCheckBoxDatagrid")
                {
                    cb.Checked = false;
                }
            }
        }

        // Quand la cellule change
        private void WorkVisiteDgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var cell = WorkVisiteDgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell is DataGridViewCheckBoxCell)
            {
                bool isChecked = Convert.ToBoolean(cell.Value);
                //if (isChecked) {
                //                  }

                cell.Style.BackColor = isChecked ? Color.Silver : Color.White;
            }
        }

        // Permet de valider le changement immédiatement après le clic sur la checkbox
        private void WorkVisiteDgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (WorkVisiteDgv.IsCurrentCellDirty)
            {
                WorkVisiteDgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        public void SaveWorkLstChanege()
        {

            List<DtvToWorkManipPoco> dtvToWorkManipPoco = new List<DtvToWorkManipPoco>();

            //foreach (var wkl in workManipulation.workPoco.workLst)
            //{
            //    wkl.Workdate = ""; // Reset Workdate

            //}
            string temp = new string("");


            foreach (DataGridViewRow row in WorkVisiteDgv.Rows)
            {
                string compagnie = row.Cells["Compagnie"].Value.ToString();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != compagnie) 
                    {
                        bool isChecked = Convert.ToBoolean(cell.Value);

                        if (isChecked)
                        {
                            if (cell.OwningColumn.Name != "Compagnie")
                            {
                                dtvToWorkManipPoco.Add(new DtvToWorkManipPoco
                                {
                                    compagnyCode = compagnie,
                                    workdate = cell.OwningColumn.Name
                                });                               
                            }
                        }
                    }                    
                }
            }

            workManipulation.SaveWorkLstChanege(dtvToWorkManipPoco);

        }

    }
}
