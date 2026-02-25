using DataBridge;
using DataBridge.Entity;
using DBConnection.Entity;
using SendBillWF.Report.EmployeeWork;
using SendBillWF.Report.EmployeeWork.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBillWF.Report.EmployeeWorkReport
{
    public partial class EmployeeWorkRepUctr : UserControl
    {
        // Panels principaux
        private Panel panelInformations;
        private Panel panelListeEmployes;
        private Panel panelStatistiques;

        // Boutons toggle
        private Button btnToggleInfos;
        private Button btnToggleEmployes;
        private Button btnToggleStats;

        // DataGridView
        private DataGridView dgvEmployes;

        Statistique statistique;

        Dictionary<string, List<EmployeeReport>> cpmWorkPriceInf = new Dictionary<string, List<EmployeeReport>>();

        public EmployeeWorkRepUctr()
        {
            InitializeComponent();
            statistique = new Statistique();

            this.AutoScroll = true;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);

            //InitialiserAvecSectionsReutilisables();
            //ChargerDonnees();

            // Gérer le redimensionnement
            this.Resize += (s, e) => {
                // Redimensionner les sections
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is FlowLayoutPanel flow)
                    {
                        foreach (Control section in flow.Controls)
                        {
                            if (section is SectionToggleUctr toggle)
                                toggle.Width = this.Width;
                        }
                    }
                }
            };
        }


        private void RemplirPanelInformations(Panel panel , ReportDTO reportDTO)
        {
            panel.Controls.Clear();

            // Layout en FlowLayoutPanel pour meilleure disposition
            FlowLayoutPanel flowLayout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0),
            };

            // Titre (pas d'emoji, garder Segoe UI normal)
            Label lblTitre = new Label
            {
                Text = "Rapport de travail de l'employé : " + reportDTO.ReportName,
                Font = new Font("Segoe UI Emoji", 14, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80),
                Margin = new Padding(0, 0, 0, 10)
            };
            flowLayout.Controls.Add(lblTitre);

            // Période avec emoji 📅
            Label lblPeriode = new Label
            {
                Text = "📅 Période  du: " + reportDTO.BeginDate + "   au: "+ reportDTO.EndDate,
                AutoSize = true,
                Font = new Font("Segoe UI Emoji", 10), // Police avec support emoji
                Margin = new Padding(0, 0, 0, 5)
            };
            flowLayout.Controls.Add(lblPeriode);

            // Service avec emoji 🏢
            Label lblService = new Label
            {
                Text = "🧹✨ Service: Entretien ménager",
                AutoSize = true,
                Font = new Font("Segoe UI Emoji", 10), // Police avec support emoji
                Margin = new Padding(0, 0, 0, 5)
            };
            flowLayout.Controls.Add(lblService);

            // Responsable avec emoji 👤
            Label lblResponsable = new Label
            {
                Text = "👤 Responsable: " + reportDTO.Supervisor,
                AutoSize = true,
                Font = new Font("Segoe UI Emoji", 10), // Police avec support emoji
                Margin = new Padding(0, 0, 0, 5)
            };
            flowLayout.Controls.Add(lblResponsable);

            panel.Controls.Add(flowLayout);
        }

        private void RemplirPanelStatistiques(Panel panel)
        {
            panel.Controls.Clear();

            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 2,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            // Définir les proportions
            //tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            //tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            //tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            //tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            //tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40));

            // Définir les proportions pour 6 colonnes égales
            for (int i = 0; i < 6; i++)
            {
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66f)); // 100/6 ≈ 16.66%
            }

            // Cartes de statistiques
            Panel carte1 = CreerCarteStatistique("🏢 Nombre Compagny", statistique.Nombrecompagny.ToString(), Color.FromArgb(20, 20, 50));
            tableLayout.Controls.Add(carte1, 0, 0);

            Panel carte2 = CreerCarteStatistique("👤 Nbr Visites effectuer", statistique.NombrVIsites.ToString(), Color.FromArgb(46, 204, 40));
            tableLayout.Controls.Add(carte2, 1, 0);

            Panel carte3 = CreerCarteStatistique("💰 Paiment employée", statistique.Totalpaiment.ToString() +" $", Color.FromArgb(46, 204, 40));
            tableLayout.Controls.Add(carte3, 2, 0);

            Panel carte4 = CreerCarteStatistique("🏦 Total revenus", "0 $", Color.FromArgb(46, 204, 40));
            tableLayout.Controls.Add(carte4, 3, 0);

            Panel carte5 = CreerCarteStatistique("📋 rendement employé", "0 $", Color.FromArgb(46, 204, 40));
            tableLayout.Controls.Add(carte5, 4, 0);

            Panel carte6 = CreerCarteStatistique("📈 Rendement investisement", "0 $", Color.FromArgb(46, 204, 40));
            tableLayout.Controls.Add(carte6, 5, 0);

            // Panel des totaux (prend toute la largeur)
            //Panel panelTotal = new Panel
            //{
            //    Name = "panelTotal",
            //    Dock = DockStyle.Fill,
            //    BackColor = Color.FromArgb(52, 73, 94),
            //    Padding = new Padding(15),
            //    Margin = new Padding(0, 10, 0, 0)
            //};

            //FlowLayoutPanel flowTotaux = new FlowLayoutPanel
            //{
            //    Dock = DockStyle.Fill,
            //    FlowDirection = FlowDirection.LeftToRight,
            //    WrapContents = false
            //};

            //Label lblTotalHeures = new Label
            //{
            //    Text = "Total heures: 0 h",
            //    AutoSize = true,
            //    ForeColor = Color.White,
            //    Font = new Font("Segoe UI", 11, FontStyle.Bold),
            //    Margin = new Padding(0, 0, 30, 0)
            //};
            //flowTotaux.Controls.Add(lblTotalHeures);

            //Label lblTotalMontant = new Label
            //{
            //    Text = "Total à payer: 0 €",
            //    AutoSize = true,
            //    ForeColor = Color.White,
            //    Font = new Font("Segoe UI", 11, FontStyle.Bold)
            //};
            //flowTotaux.Controls.Add(lblTotalMontant);

            //panelTotal.Controls.Add(flowTotaux);
            //tableLayout.Controls.Add(panelTotal, 0, 1);
            //tableLayout.SetColumnSpan(panelTotal, 3);

            panel.Controls.Add(tableLayout);
        }

        private Panel CreerCarteStatistique(string titre, string valeur, Color couleur)
        {
            Panel carte = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = couleur,
                Margin = new Padding(5),
                Padding = new Padding(15)
            };

            Label lblTitre = new Label
            {
                Text = titre,
                Location = new Point(15, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Emoji", 11)
            };
            carte.Controls.Add(lblTitre);

            Label lblValeur = new Label
            {
                Text = valeur,
                Location = new Point(15, 45),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI  Emoji", 20, FontStyle.Bold)
            };
            carte.Controls.Add(lblValeur);

            return carte;
        }

        private void InitialiserAvecSectionsReutilisables(ReportDTO reportDTO)
        {
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0)
            };
            this.Controls.Add(flowPanel);

            // SECTION 1: Informations générales (statique)
            var sectionInfos = new SectionToggleUctr
            {
                Titre = "Informations générales",
                Width = this.Width,
                Margin = new Padding(0, 0, 0, 5),
                EstDeplie = true
            };
            sectionInfos.DefinirHauteurContenu(150);
            RemplirPanelInformations(sectionInfos.ContenuPanel , reportDTO);
            this.Resize += (s, e) => sectionInfos.Width = this.Width;
            flowPanel.Controls.Add(sectionInfos);

            statistique.Nombrecompagny = 0;
            statistique.NombrVIsites = 0;
            statistique.Totalpaiment = 0;
            // SECTION 2: Companies (dynamique depuis le dictionnaire)
            foreach (var item in cpmWorkPriceInf)
            {
                string companyName = item.Key;
                List<EmployeeReport> pricingData = item.Value;

                var sectionCompany = new SectionToggleUctr
                {
                    Titre = $"🏢 {companyName}",
                    Width = this.Width,
                    Margin = new Padding(0, 0, 0, 5),
                    Tag = pricingData,
                    EstDeplie = false
                };

                // SOLUTION: NOUVEAU CALCUL DE HAUTEUR PLUS PRÉCIS
                int hauteur = 0;

                if (pricingData.Count > 0)
                {
                    // En-tête DataGridView: 35px
                    // Lignes: 30px chacune
                    // Panel info: 50px minimum
                    // Padding et marges: 20px
                    hauteur = 35 + (pricingData.Count * 50) + 50 + 20;
                }
                else
                {
                    hauteur = 200; // Hauteur minimum si pas de données
                }

                // Limiter à une hauteur maximum raisonnable (600px)
                hauteur = Math.Min(600, hauteur);

                sectionCompany.DefinirHauteurContenu(hauteur);

                statistique.Nombrecompagny += 1;
                // Remplir le panel avec les données de pricing
                RemplirPanelCompany(sectionCompany.ContenuPanel, companyName, pricingData);

                this.Resize += (s, e) => sectionCompany.Width = this.Width;
                flowPanel.Controls.Add(sectionCompany);
            }

            // SECTION 3: Statistiques (statique)
            var sectionStats = new SectionToggleUctr
            {
                Titre = "Statistiques",
                Width = this.Width,
                Margin = new Padding(0, 0, 0, 5),
                EstDeplie = true
            };
            sectionStats.DefinirHauteurContenu(180);
            RemplirPanelStatistiques(sectionStats.ContenuPanel);
            this.Resize += (s, e) => sectionStats.Width = this.Width;
            flowPanel.Controls.Add(sectionStats);
        }

        private void RemplirPanelCompany(Panel panel, string companyName, List<EmployeeReport> pricingData)
        {
            panel.Controls.Clear();

            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            // MODIFICATION: Ajuster les pourcentages pour donner plus d'espace au panel d'info
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 75)); // Réduit de 80% à 75%
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25)); // Augmenté de 20% à 25%

            // DataGridView pour les prix
            DataGridView dgvPricing = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Margin = new Padding(0, 0, 0, 5)
            };

            // Style du DataGridView
            dgvPricing.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvPricing.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPricing.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Emoji", 9, FontStyle.Bold);
            dgvPricing.ColumnHeadersHeight = 35;
            dgvPricing.DefaultCellStyle.Font = new Font("Segoe UI Emoji", 9);
            dgvPricing.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvPricing.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvPricing.RowTemplate.Height = 30;

            // Ajouter des colonnes
            dgvPricing.Columns.Add("Workdate", "Date");
            dgvPricing.Columns.Add("EmployePrice", "Prix ($)");

            // Formater les colonnes
            dgvPricing.Columns["Workdate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPricing.Columns["EmployePrice"].DefaultCellStyle.Format = "N2";

            // Remplir avec les données
            foreach (var pricing in pricingData)
            {
                dgvPricing.Rows.Add(
                    Formatdate(pricing.Workdate),
                    pricing.EmployePrice
                );

                statistique.NombrVIsites += 1;
                statistique.Totalpaiment += pricing.EmployePrice;

            }

            tableLayout.Controls.Add(dgvPricing, 0, 0);

            // MODIFICATION: Panel d'informations amélioré avec hauteur fixe minimum
            FlowLayoutPanel infoPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true, // Permettre le wrapping si nécessaire
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10),
                MinimumSize = new Size(0, 50), // Hauteur minimum
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Calculer les statistiques
            decimal moyennePrix = pricingData.Count > 0 ? pricingData.Average(p => p.EmployePrice) : 0;
            decimal totalPrix = pricingData.Sum(p => p.EmployePrice);
            decimal minPrix = pricingData.Count > 0 ? pricingData.Min(p => p.EmployePrice) : 0;
            decimal maxPrix = pricingData.Count > 0 ? pricingData.Max(p => p.EmployePrice) : 0;

            // Prix moyen
            Label lblMoyennePrix = new Label
            {
                Text = $"Nbre de visite : {Convert.ToInt32(pricingData.Count)} Visites",
                AutoSize = true,
                Font = new Font("Segoe UI Emoji", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Margin = new Padding(0, 0, 15, 0)
            };
            infoPanel.Controls.Add(lblMoyennePrix);

            // Total
            Label lblTotalPrix = new Label
            {
                Text = $" Total: {totalPrix} $",
                AutoSize = true,
                Font = new Font("Segoe UI Emoji", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Margin = new Padding(0, 0, 15, 0)
            };
            infoPanel.Controls.Add(lblTotalPrix);

            //// Min/Max
            //Label lblMinPrix = new Label
            //{
            //    Text = $"⬇️ Min: {minPrix} $",
            //    AutoSize = true,
            //    Font = new Font("Segoe UI", 8),
            //    ForeColor = Color.FromArgb(44, 62, 80),
            //    Margin = new Padding(0, 0, 10, 0)
            //};
            //infoPanel.Controls.Add(lblMinPrix);

            //Label lblMaxPrix = new Label
            //{
            //    Text = $"⬆️ Max: {maxPrix} $",
            //    AutoSize = true,
            //    Font = new Font("Segoe UI", 8),
            //    ForeColor = Color.FromArgb(44, 62, 80),
            //    Margin = new Padding(0, 0, 15, 0)
            //};
            //infoPanel.Controls.Add(lblMaxPrix);

            // Nombre de périodes
            Label lblNbPeriodes = new Label
            {
                Text = $" {pricingData.Count} périodes",
                AutoSize = true,
                Font = new Font("Segoe UI Emoji", 8),
                ForeColor = Color.FromArgb(44, 62, 80),
                Margin = new Padding(0, 0, 15, 0)
            };
            infoPanel.Controls.Add(lblNbPeriodes);

            if (pricingData.Any())
            {
                var premiere = pricingData.OrderBy(p => p.Workdate).First();
                var derniere = pricingData.OrderBy(p => p.Workdate).Last();

                Label lblDateDebut = new Label
                {
                    Text = $"📅 Début: {premiere.Workdate:dd/MM/yy}",
                    AutoSize = true,
                    Font = new Font("Segoe UI Emoji", 8),
                    ForeColor = Color.FromArgb(44, 62, 80),
                    Margin = new Padding(0, 0, 10, 0)
                };
                infoPanel.Controls.Add(lblDateDebut);

                Label lblDateFin = new Label
                {
                    Text = $"📅 Fin: {derniere.Workdate:dd/MM/yy}",
                    AutoSize = true,
                    Font = new Font("Segoe UI Emoji", 8),
                    ForeColor = Color.FromArgb(44, 62, 80)
                };
                infoPanel.Controls.Add(lblDateFin);
            }

            tableLayout.Controls.Add(infoPanel, 0, 1);
            panel.Controls.Add(tableLayout);
        }

        private string Formatdate(string dateStr)
        {
            return DateTime
            .ParseExact(dateStr, "ddMMyyyy", CultureInfo.InvariantCulture)
            .ToString("dddd d MMMM yyyy", new CultureInfo("fr-FR"));
        }

        public void ChargerDonnees(ReportDTO reportDTO)
        {
            //Guid empInf = ConvertStringToGuid("B7422A1B-3B10-4F2C-9367-59F10DD36D1F");
            //reste l'intervalle de travaille

            WorkManipulation workManipulation = new WorkManipulation();
            CompagniManipulation compagniManipulation = new CompagniManipulation();
            List<EmployeeReport> employeeReports ;


            cpmWorkPriceInf.Clear();  // Le dictionnaire est maintenant vide            

            List<CompagniePoco> compagniesLst = compagniManipulation.GetCompagnyByEmployee(reportDTO.ReportId);

            //List<EmployeeCompagnyPricingPoco>
            foreach (var item in compagniesLst)
            {
                employeeReports = new List<EmployeeReport>();

                workManipulation.GetWorkListByCompagnyEmployeeId(reportDTO.ReportId, item.CompagnieID);
                foreach (var work in workManipulation.workPoco.workLst)
                {
                    employeeReports.Add(new EmployeeReport
                    {
                        CompagnyName = item.CompagnieName,
                        Workdate = work.Workdate,
                        EmployePrice = work.ClientPrice
                    });
                }              

                cpmWorkPriceInf[item.CompagnieName] = employeeReports;
            }

            this.Controls.Clear();
            InitialiserAvecSectionsReutilisables(reportDTO);
        }

        //private void BtnExporter_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Export PDF en cours...", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}



        //private void MettreAJourStatistiques(Panel panelStats, decimal totalHeures, decimal totalMontant)
        //{
        //    foreach (Control ctrl in panelStats.Controls)
        //    {
        //        if (ctrl is TableLayoutPanel tableLayout)
        //        {
        //            foreach (Control subCtrl in tableLayout.Controls)
        //            {
        //                if (subCtrl is Panel panel && panel.Name == "panelTotal")
        //                {
        //                    foreach (Control flowCtrl in panel.Controls)
        //                    {
        //                        if (flowCtrl is FlowLayoutPanel flow)
        //                        {
        //                            foreach (Control lblCtrl in flow.Controls)
        //                            {
        //                                if (lblCtrl is Label lbl)
        //                                {
        //                                    if (lbl.Text.StartsWith("Total heures"))
        //                                        lbl.Text = $"Total heures: {totalHeures} h";
        //                                    if (lbl.Text.StartsWith("Total à payer"))
        //                                        lbl.Text = $"Total à payer: {totalMontant:F2} €";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Mettre à jour aussi les cartes (optionnel)
        //    int compteurEmployes = 5; // Vous pouvez calculer dynamiquement
        //    foreach (Control ctrl in panelStats.Controls)
        //    {
        //        if (ctrl is TableLayoutPanel tableLayout)
        //        {
        //            int row = 0, col = 0;
        //            foreach (Control subCtrl in tableLayout.Controls)
        //            {
        //                if (subCtrl is Panel carte && carte.Controls.Count >= 2)
        //                {
        //                    var lblValeur = carte.Controls[1] as Label;
        //                    if (lblValeur != null)
        //                    {
        //                        if (carte.BackColor == Color.FromArgb(52, 152, 219)) // Total employés
        //                            lblValeur.Text = compteurEmployes.ToString();
        //                        else if (carte.BackColor == Color.FromArgb(46, 204, 113)) // Heures totales
        //                            lblValeur.Text = $"{totalHeures} h";
        //                        else if (carte.BackColor == Color.FromArgb(155, 89, 182)) // Coût total
        //                            lblValeur.Text = $"{totalMontant:F0} €";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public Guid ConvertStringToGuid(string str)
        //{
        //    if (string.IsNullOrWhiteSpace(str))
        //        return Guid.Empty;

        //    if (Guid.TryParse(str, out Guid result))
        //        return result;

        //    // Try different formats
        //    if (Guid.TryParseExact(str, "N", out result))
        //        return result;

        //    if (Guid.TryParseExact(str, "B", out result))
        //        return result;

        //    if (Guid.TryParseExact(str, "P", out result))
        //        return result;

        //    return Guid.Empty;
        //}

        //private void RemplirPanelEmployes(Panel panel)
        //{
        //    panel.Controls.Clear();

        //    // Panel conteneur principal
        //    TableLayoutPanel tableLayout = new TableLayoutPanel
        //    {
        //        Dock = DockStyle.Fill,
        //        ColumnCount = 1,
        //        RowCount = 2,
        //        Padding = new Padding(0),
        //        Margin = new Padding(0)
        //    };
        //    tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
        //    tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15));

        //    // DataGridView
        //    dgvEmployes = new DataGridView
        //    {
        //        Dock = DockStyle.Fill,
        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        //        ReadOnly = true,
        //        AllowUserToAddRows = false,
        //        BackgroundColor = Color.White,
        //        BorderStyle = BorderStyle.None,
        //        RowHeadersVisible = false,
        //        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        //        MultiSelect = false,
        //        Margin = new Padding(0, 0, 0, 10)
        //    };

        //    // Style du DataGridView
        //    dgvEmployes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
        //    dgvEmployes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        //    dgvEmployes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Emoji", 10, FontStyle.Bold);
        //    dgvEmployes.ColumnHeadersHeight = 45;
        //    dgvEmployes.DefaultCellStyle.Font = new Font("Segoe UI Emoji", 10);
        //    dgvEmployes.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
        //    dgvEmployes.DefaultCellStyle.Padding = new Padding(5);
        //    dgvEmployes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
        //    dgvEmployes.RowTemplate.Height = 35;

        //    // Ajouter des colonnes
        //    dgvEmployes.Columns.Add("Nom", "Nom");
        //    dgvEmployes.Columns.Add("Prenom", "Prénom");
        //    dgvEmployes.Columns.Add("Poste", "Poste");
        //    dgvEmployes.Columns.Add("Heures", "Heures");
        //    dgvEmployes.Columns.Add("Taux", "Taux (€)");
        //    dgvEmployes.Columns.Add("Total", "Total (€)");

        //    tableLayout.Controls.Add(dgvEmployes, 0, 0);

        //    //// Panel des boutons
        //    //FlowLayoutPanel panelBoutons = new FlowLayoutPanel
        //    //{
        //    //    Dock = DockStyle.Fill,
        //    //    FlowDirection = FlowDirection.LeftToRight,
        //    //    WrapContents = false,
        //    //    AutoSize = true,
        //    //    Margin = new Padding(0, 5, 0, 0)
        //    //};

        //    //Button btnExporter = new Button
        //    //{
        //    //    Text = "📄 Exporter PDF",
        //    //    Size = new Size(130, 35),
        //    //    BackColor = Color.FromArgb(46, 204, 113),
        //    //    ForeColor = Color.White,
        //    //    FlatStyle = FlatStyle.Flat,
        //    //    FlatAppearance = { BorderSize = 0 },
        //    //    Font = new Font("Segoe UI Emoji", 10, FontStyle.Bold),
        //    //    Margin = new Padding(0, 0, 10, 0)
        //    //};
        //    //btnExporter.Click += BtnExporter_Click;
        //    //panelBoutons.Controls.Add(btnExporter);

        //    //Button btnImprimer = new Button
        //    //{
        //    //    Text = "🖨️ Imprimer",
        //    //    Size = new Size(130, 35),
        //    //    BackColor = Color.FromArgb(52, 152, 219),
        //    //    ForeColor = Color.White,
        //    //    FlatStyle = FlatStyle.Flat,
        //    //    FlatAppearance = { BorderSize = 0 },
        //    //    Font = new Font("Segoe UI Emoji", 10, FontStyle.Bold)
        //    //};
        //    //panelBoutons.Controls.Add(btnImprimer);

        //    //tableLayout.Controls.Add(panelBoutons, 0, 1);

        //    panel.Controls.Add(tableLayout);
        //}

    }
}
