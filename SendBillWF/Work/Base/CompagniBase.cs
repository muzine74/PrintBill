using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SendBillWF.Work.Base
{
    public partial class CompagniBase : UserControl
    {
        // Constantes pour les couleurs et styles
        private static class Colors
        {
            public static readonly Color Primary = Color.FromArgb(52, 152, 219);
            public static readonly Color Success = Color.FromArgb(76, 175, 80);
            public static readonly Color Danger = Color.FromArgb(244, 67, 54);
            public static readonly Color Warning = Color.FromArgb(255, 152, 0);
            public static readonly Color TextPrimary = Color.FromArgb(33, 33, 33);
            public static readonly Color TextSecondary = Color.FromArgb(117, 117, 117);
            public static readonly Color BorderLight = Color.FromArgb(224, 224, 224);
            public static readonly Color BackgroundHover = Color.FromArgb(245, 245, 245);
            public static readonly Color PanelBackground = Color.FromArgb(250, 250, 250);
            public static readonly Color ErrorBackground = Color.FromArgb(255, 235, 235);
            public static readonly Color ReadOnlyBackground = Color.FromArgb(245, 245, 245);
            public static readonly Color DisabledCheckBox = Color.FromArgb(180, 180, 180);
        }

        private static class Dimensions
        {
            public const int BorderRadius = 8;
            public const int Padding = 15;
            public const int Spacing = 12;
            public const int ControlHeight = 30;
            public const int LabelHeight = 20;
            public const int LabelWidth = 60;
            public const int CheckBoxWidth = 70;
            public const int FieldWidth = 400; // Largeur augmentée
            public const int PanelWidth = 600; // Largeur totale du panel
        }

        // Événements
        public event EventHandler<string> CompanyNameChanged;
        public event EventHandler<string> CompanyCodeChanged;
        public event EventHandler<string> CompanyAddressChanged;
        public event EventHandler<bool> ActiveStateChanged;

        private bool _isHovered;
        private ToolTip _toolTip;

        // Labels pour les champs
        private Label lblCompanyName;
        private Label lblCompanyCode;
        private Label lblCompanyAddress;

        // Contrôles
        private TextBox txtCompanyName;
        private TextBox txtCompanyCode;
        private TextBox txtCompanyAddress;
        private CheckBox chkIsActive;

        public CompagniBase()
        {
            InitializeComponent();
            InitializeControls();
            ApplyModernStyle();
            SetupEventHandlers();

            // Forcer le redessin
            this.Invalidate();
        }

        private void InitializeControls()
        {
            // Création des labels
            lblCompanyName = new Label();
            lblCompanyCode = new Label();
            lblCompanyAddress = new Label();

            // Création des champs de saisie
            txtCompanyName = new TextBox();
            txtCompanyCode = new TextBox();
            txtCompanyAddress = new TextBox();
            chkIsActive = new CheckBox();
            _toolTip = new ToolTip();

            // Configurer les propriétés de base
            ConfigureLabels();
            ConfigureTextBox(txtCompanyName, "Nom de l'entreprise");
            ConfigureTextBox(txtCompanyCode, "Code entreprise");
            ConfigureTextBox(txtCompanyAddress, "Adresse complète");
            ConfigureActiveCheckBox();

            // Ajouter tous les contrôles au UserControl
            this.Controls.AddRange(new Control[] {
                lblCompanyName, lblCompanyCode, lblCompanyAddress,
                txtCompanyName, txtCompanyCode, txtCompanyAddress,
                chkIsActive
            });
        }

        private void ConfigureLabels()
        {
            Font labelFont = new Font("Segoe UI", 9, FontStyle.Bold);

            // Label NOM
            lblCompanyName.Font = labelFont;
            lblCompanyName.ForeColor = Colors.TextSecondary;
            lblCompanyName.Text = "NOM";
            lblCompanyName.TextAlign = ContentAlignment.MiddleRight; // Aligné à droite
            lblCompanyName.Size = new Size(Dimensions.LabelWidth, Dimensions.LabelHeight);
            lblCompanyName.BackColor = Color.Transparent;

            // Label CODE
            lblCompanyCode.Font = labelFont;
            lblCompanyCode.ForeColor = Colors.TextSecondary;
            lblCompanyCode.Text = "CODE";
            lblCompanyCode.TextAlign = ContentAlignment.MiddleRight; // Aligné à droite
            lblCompanyCode.Size = new Size(Dimensions.LabelWidth, Dimensions.LabelHeight);
            lblCompanyCode.BackColor = Color.Transparent;

            // Label ADRESSE
            lblCompanyAddress.Font = labelFont;
            lblCompanyAddress.ForeColor = Colors.TextSecondary;
            lblCompanyAddress.Text = "ADRESSE";
            lblCompanyAddress.TextAlign = ContentAlignment.MiddleRight; // Aligné à droite
            lblCompanyAddress.Size = new Size(Dimensions.LabelWidth, Dimensions.LabelHeight);
            lblCompanyAddress.BackColor = Color.Transparent;
        }

        private void ApplyModernStyle()
        {
            // Style du conteneur principal
            this.BackColor = Colors.PanelBackground;
            this.Padding = new Padding(Dimensions.Padding);
            this.DoubleBuffered = true;
            this.Size = new Size(Dimensions.PanelWidth, 200); // Panel plus large

            // Effet de survol
            this.MouseEnter += (s, e) => { _isHovered = true; this.Invalidate(); };
            this.MouseLeave += (s, e) => { _isHovered = false; this.Invalidate(); };
        }

        private void SetupEventHandlers()
        {
            // Gestionnaire pour le checkbox (readonly visuel)
            chkIsActive.Click += (s, e) =>
            {
                // Ne rien faire pour empêcher le changement
            };

            chkIsActive.CheckedChanged += (s, e) =>
            {
                ActiveStateChanged?.Invoke(this, chkIsActive.Checked);
            };

            // Gestionnaires pour les TextBox
            txtCompanyName.TextChanged += (s, e) =>
                CompanyNameChanged?.Invoke(this, txtCompanyName.Text);

            txtCompanyCode.TextChanged += (s, e) =>
                CompanyCodeChanged?.Invoke(this, txtCompanyCode.Text);

            txtCompanyAddress.TextChanged += (s, e) =>
                CompanyAddressChanged?.Invoke(this, txtCompanyAddress.Text);
        }

        private void ConfigureTextBox(TextBox textBox, string placeholder)
        {
            textBox.Font = new Font("Segoe UI", 10);
            textBox.ForeColor = Colors.TextPrimary;
            textBox.BackColor = Colors.ReadOnlyBackground;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Padding = new Padding(8);
            textBox.ReadOnly = true;

            // Stocker le placeholder dans le Tag
            textBox.Tag = placeholder;
        }

        private void ConfigureActiveCheckBox()
        {
            chkIsActive.Text = "Actif";
            chkIsActive.Font = new Font("Segoe UI", 10);
            chkIsActive.ForeColor = Colors.TextSecondary;
            chkIsActive.FlatStyle = FlatStyle.Standard;
            chkIsActive.BackColor = Color.Transparent;
            chkIsActive.Padding = new Padding(5, 0, 0, 0);
            chkIsActive.Height = 25;
            chkIsActive.Width = Dimensions.CheckBoxWidth;
            chkIsActive.UseVisualStyleBackColor = false;
            chkIsActive.Checked = true;

            // Rendre le checkbox non cliquable
            chkIsActive.Enabled = false;

            // Personnaliser l'apparence quand désactivé
            chkIsActive.Paint += (s, e) =>
            {
                CheckBox cb = (CheckBox)s;
                if (!cb.Enabled)
                {
                    // Dessiner le checkbox avec une couleur grisée
                    ControlPaint.DrawCheckBox(e.Graphics,
                        new Rectangle(3, (cb.Height - 13) / 2, 13, 13),
                        cb.Checked ? ButtonState.Checked : ButtonState.Normal);

                    // Dessiner le texte
                    TextRenderer.DrawText(e.Graphics, cb.Text, cb.Font,
                        new Rectangle(20, 0, cb.Width - 20, cb.Height),
                        Colors.DisabledCheckBox,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }
            };
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            PositionControls();
        }

        private void PositionControls()
        {
            int startX = Dimensions.Padding;
            int labelX = startX;
            int fieldX = startX + Dimensions.LabelWidth + 10;
            int currentY = Dimensions.Padding;

            // LIGNE 1 : NOM + ACTIF
            // Label NOM aligné à droite
            lblCompanyName.Location = new Point(labelX, currentY + 5);

            // TextBox NOM
            txtCompanyName.Location = new Point(fieldX, currentY);
            txtCompanyName.Size = new Size(Dimensions.FieldWidth - 100, Dimensions.ControlHeight);

            // Checkbox ACTIF à droite du textbox NOM
            chkIsActive.Location = new Point(fieldX + txtCompanyName.Width + 15, currentY + 2);

            currentY += Dimensions.ControlHeight + Dimensions.Spacing;

            // LIGNE 2 : CODE
            // Label CODE aligné à droite
            lblCompanyCode.Location = new Point(labelX, currentY + 5);

            // TextBox CODE
            txtCompanyCode.Location = new Point(fieldX, currentY);
            txtCompanyCode.Size = new Size(200, Dimensions.ControlHeight);

            currentY += Dimensions.ControlHeight + Dimensions.Spacing;

            // LIGNE 3 : ADRESSE
            // Label ADRESSE aligné à droite
            lblCompanyAddress.Location = new Point(labelX, currentY + 10);

            // TextBox ADRESSE (multiligne)
            txtCompanyAddress.Location = new Point(fieldX, currentY);
            txtCompanyAddress.Size = new Size(Dimensions.FieldWidth, 60);
            txtCompanyAddress.Multiline = true;

            // Ajuster la hauteur du UserControl
            this.Height = currentY + 80;
            this.Width = Dimensions.PanelWidth;
        }

        // Propriétés
        [Category("Company Data")]
        [Description("Nom de l'entreprise")]
        public string CompagnyBaseName
        {
            get => txtCompanyName.Text;
            set
            {
                txtCompanyName.Text = value;
                txtCompanyName.Invalidate();
                this.Invalidate();
            }
        }

        [Category("Company Data")]
        [Description("Code de l'entreprise")]
        public string CompagnyBaseCode
        {
            get => txtCompanyCode.Text;
            set
            {
                txtCompanyCode.Text = value?.ToUpperInvariant();
                txtCompanyCode.Invalidate();
                this.Invalidate();
            }
        }

        [Category("Company Data")]
        [Description("Adresse de l'entreprise")]
        public string CompagnyBaseAdress
        {
            get => txtCompanyAddress.Text;
            set
            {
                txtCompanyAddress.Text = value;
                txtCompanyAddress.Invalidate();
                this.Invalidate();
            }
        }

        [Category("Company Data")]
        [Description("Statut actif/inactif")]
        public bool CompagnyBaseIsActive
        {
            get => chkIsActive.Checked;
            set
            {
                chkIsActive.Checked = value;
                chkIsActive.Invalidate();
            }
        }

        // Méthodes publiques
        public void ClearAllFields()
        {
            txtCompanyName.Text = string.Empty;
            txtCompanyCode.Text = string.Empty;
            txtCompanyAddress.Text = string.Empty;
            chkIsActive.Checked = false;
        }

        public void SetData(string name, string code, string address, bool isActive)
        {
            txtCompanyName.Text = name;
            txtCompanyCode.Text = code;
            txtCompanyAddress.Text = address;
            chkIsActive.Checked = isActive;

            // Forcer le rafraîchissement
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Dessiner la bordure avec coins arrondis
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = Dimensions.BorderRadius;
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseFigure();

                using (Pen pen = new Pen(_isHovered ? Colors.Primary : Colors.BorderLight, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}