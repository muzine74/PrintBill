using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SendBillWF.Work.Base
{
    public partial class CompagniBase : UserControl
    {
        // Constantes pour les couleurs et styles
        private static readonly Color ActiveColor = Color.FromArgb(76, 175, 80);
        private static readonly Color InactiveColor = Color.FromArgb(244, 67, 54);
        private static readonly Color LabelColor = Color.FromArgb(100, 100, 100);
        private static readonly Color BorderColor = Color.FromArgb(230, 230, 230);

        // Événements pour notifier les changements
        public event EventHandler<string> CompanyNameChanged;
        public event EventHandler<bool> ActiveStateChanged;

        public CompagniBase()
        {
            InitializeComponent();
            ApplyModernStyle();
            SetupEventHandlers();
        }

        private void ApplyModernStyle()
        {
            // Style du conteneur principal
            this.BackColor = Color.White;
            this.Padding = new Padding(10);
            this.BorderStyle = BorderStyle.None;

            // Ajouter une bordure élégante
            this.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(BorderColor, 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                }
            };

            // Style des labels
            ApplyLabelStyle(CompagnyName, "🏢 Nom de l'entreprise");
            ApplyLabelStyle(CompagnyCode, "📋 Code");
            ApplyLabelStyle(ComapgnyAdress, "📍 Adresse");

            // Style du checkbox d'activité
            IsActiveComapgny.Text = " Actif";
            IsActiveComapgny.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            IsActiveComapgny.ForeColor = LabelColor;
            IsActiveComapgny.FlatStyle = FlatStyle.Flat;
            IsActiveComapgny.BackColor = Color.Transparent;
        }

        private void ApplyLabelStyle(Label label, string placeholder)
        {
            label.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            label.ForeColor = LabelColor;
            label.Text = placeholder;
            label.AutoSize = false;
            label.TextAlign = ContentAlignment.MiddleLeft;
        }

        private void SetupEventHandlers()
        {
            IsActiveComapgny.CheckedChanged += (s, e) =>
            {
                ActiveStateChanged?.Invoke(this, IsActiveComapgny.Checked);
                UpdateActiveStateVisual();
            };

            CompagnyName.TextChanged += (s, e) =>
                CompanyNameChanged?.Invoke(this, CompagnyName.Text);
        }

        private void UpdateActiveStateVisual()
        {
            IsActiveComapgny.ForeColor = IsActiveComapgny.Checked ? ActiveColor : InactiveColor;
        }

        // Propriétés avec validation et notifications
        [Category("Company Data")]
        [Description("Nom de l'entreprise")]
        public string CompagnyBaseName
        {
            get => CompagnyName.Text;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 100)
                    throw new ArgumentException("Le nom ne peut pas dépasser 100 caractères");

                CompagnyName.Text = value;
                CompagnyName.ForeColor = Color.Black;
            }
        }

        [Category("Company Data")]
        [Description("Code de l'entreprise")]
        public string CompagnyBaseCode
        {
            get => CompagnyCode.Text;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 20)
                    throw new ArgumentException("Le code ne peut pas dépasser 20 caractères");

                CompagnyCode.Text = value?.ToUpperInvariant();
                CompagnyCode.ForeColor = Color.Black;
            }
        }

        [Category("Company Data")]
        [Description("Adresse de l'entreprise")]
        public string CompagnyBaseAdress
        {
            get => ComapgnyAdress.Text;
            set
            {
                ComapgnyAdress.Text = value;
                ComapgnyAdress.ForeColor = Color.Black;
            }
        }

        [Category("Company Data")]
        [Description("Statut actif/inactif")]
        public bool CompagnyBaseIsActive
        {
            get => IsActiveComapgny.Checked;
            set
            {
                IsActiveComapgny.Checked = value;
                UpdateActiveStateVisual();
            }
        }

        // Méthode utilitaire pour effacer tous les champs
        public void ClearAllFields()
        {
            CompagnyBaseName = string.Empty;
            CompagnyBaseCode = string.Empty;
            CompagnyBaseAdress = string.Empty;
            CompagnyBaseIsActive = false;
        }
    }
}