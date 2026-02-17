using System;
using System.Drawing;
using System.Windows.Forms;

namespace SendBillWF.Report.EmployeeWork.Base
{
    public partial class SectionToggleUctr : UserControl  // ← PLUS DE "partial"
    {
        private Button btnToggle;
        private Panel panelContenu;

        public event EventHandler<bool> VisibiliteChangee;

        public SectionToggleUctr()
        {
            // InitializeComponent();  // ← SUPPRIMEZ ou COMMENTEZ

            this.Height = 35;
            this.Width = 1000;

            // Bouton toggle
            btnToggle = new Button
            {
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                UseVisualStyleBackColor = false
            };

            // Panel contenu
            panelContenu = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(245, 245, 245),
                Padding = new Padding(15),
                Visible = true,
                Height = 100
            };

            btnToggle.Click += (s, e) =>
            {
                panelContenu.Visible = !panelContenu.Visible;
                btnToggle.Text = panelContenu.Visible ? $"▼ {Titre}" : $"▶ {Titre}";
                this.Height = 35 + (panelContenu.Visible ? panelContenu.Height + 5 : 0);
                VisibiliteChangee?.Invoke(this, panelContenu.Visible);
            };

            this.Controls.Add(panelContenu);
            this.Controls.Add(btnToggle);
        }

        public string Titre
        {
            get => btnToggle.Text.Replace("▼ ", "").Replace("▶ ", "");
            set => btnToggle.Text = $"▼ {value}";
        }

        public Panel ContenuPanel => panelContenu;

        public bool EstDeplie
        {
            get => panelContenu.Visible;
            set
            {
                panelContenu.Visible = value;
                btnToggle.Text = value ? $"▼ {Titre}" : $"▶ {Titre}";
                this.Height = 35 + (value ? panelContenu.Height + 5 : 0);
            }
        }

        public void DefinirHauteurContenu(int hauteur)
        {
            panelContenu.Height = hauteur;
            this.Height = 35 + (panelContenu.Visible ? hauteur + 5 : 0);
        }
    }
}