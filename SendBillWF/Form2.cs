using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBridge.Entity;
using SendBillWF.Employee;
using static System.Net.Mime.MediaTypeNames;

namespace SendBillWF
{
    public partial class Form2 : Form
    {
        CompagniePoco compagniePoco;

        public Form2()
        {
            InitializeComponent();
            compagniePoco = new CompagniePoco();
            addCompagnyusctr1 = new AddCompagnyusctr();
            updateCmpUsctr = new UpdateCmpUsctr();
            billHistoryUsctr = new BillHistoryUsctr();
            pDFBillFromGDriveUsCtr = new PDFBillFromGDriveUsCtr();
            createBillUsCtr = new CreateBillUsCtr();
            creatEmployeeUsCtr = new CreatEmployeeUsCtr();


            // updateCmpUsctr1.Hide();


            //addCompagnyusctr1.Hide();   

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Menu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "AddNewCompagny")
            {
                RemoveControl();

                addCompagnyusctr1.Location = new Point(201, 33);
                addCompagnyusctr1.Name = "addCompagnyusctr1";
                addCompagnyusctr1.Size = new Size(1152, 848);
                addCompagnyusctr1.TabIndex = 1;

                Controls.Add(addCompagnyusctr1);
                addCompagnyusctr1.Show();
            }

            if (e.Node.Name == "UpdateCompagny")

            {
                RemoveControl();

                updateCmpUsctr.Location = new Point(201, 33);
                updateCmpUsctr.Name = "updateCmpUsctr";
                updateCmpUsctr.Size = new Size(1152, 848);
                updateCmpUsctr.TabIndex = 1;

                Controls.Add(updateCmpUsctr);
                updateCmpUsctr.Show();
            }

            if (e.Node.Name == "BillHistory")
            {
                RemoveControl();

                billHistoryUsctr.Location = new Point(201, 33);
                billHistoryUsctr.Name = "billHistoryUsctr";
                billHistoryUsctr.Size = new Size(1750, 900);
                billHistoryUsctr.TabIndex = 1;

                Controls.Add(billHistoryUsctr);
                billHistoryUsctr.Show();

            }

            if (e.Node.Name == "PDFBillFromGDrive")
            {
                RemoveControl();

                pDFBillFromGDriveUsCtr.Location = new Point(201, 33);
                pDFBillFromGDriveUsCtr.Name = "pDFBillFromGDriveUsCtr";
                pDFBillFromGDriveUsCtr.Size = new Size(1750, 900);
                pDFBillFromGDriveUsCtr.TabIndex = 1;

                Controls.Add(pDFBillFromGDriveUsCtr);
                billHistoryUsctr.Show();

            }

            if (e.Node.Name == "CreateBill")
            {
                RemoveControl();

                createBillUsCtr.Location = new Point(201, 33);
                createBillUsCtr.Name = "createBillUsCtr";
                createBillUsCtr.Size = new Size(1750, 900);
                createBillUsCtr.TabIndex = 1;

                Controls.Add(createBillUsCtr);
                billHistoryUsctr.Show();

            }

            if (e.Node.Name == "CreateEmployee")
            {
                RemoveControl();

                creatEmployeeUsCtr.Location = new Point(201, 33);
                creatEmployeeUsCtr.Name = "createEmployeeUsCtr";
                creatEmployeeUsCtr.Size = new Size(1750, 900);
                creatEmployeeUsCtr.TabIndex = 1;

                Controls.Add(creatEmployeeUsCtr);
                creatEmployeeUsCtr.Show();

            }

        } //PDFBillFromGDrive


        private void RemoveControl()
        {
            Controls.Remove(addCompagnyusctr1);
            Controls.Remove(updateCmpUsctr);
            Controls.Remove(billHistoryUsctr);
            Controls.Remove(pDFBillFromGDriveUsCtr);
            Controls.Remove(createBillUsCtr);
            Controls.Remove(creatEmployeeUsCtr);


        }
    }
}
