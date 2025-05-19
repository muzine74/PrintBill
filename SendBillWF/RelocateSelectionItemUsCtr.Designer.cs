
namespace SendBillWF
{
    partial class RelocateSelectionItemUsCtr
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ItemListSource = new ListBox();
            ItemListDestination = new ListBox();
            groupBox1 = new GroupBox();
            FromListDestinationToSource = new Label();
            FromListSourceToDestination = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // ItemListSource
            // 
            ItemListSource.FormattingEnabled = true;
            ItemListSource.ItemHeight = 15;
            ItemListSource.Location = new Point(38, 37);
            ItemListSource.Name = "ItemListSource";
            ItemListSource.SelectionMode = SelectionMode.MultiSimple;
            ItemListSource.Size = new Size(212, 229);
            ItemListSource.TabIndex = 0;
            ItemListSource.SelectedIndexChanged += ItemListSource_SelectedIndexChanged;
            // 
            // ItemListDestination
            // 
            ItemListDestination.FormattingEnabled = true;
            ItemListDestination.ItemHeight = 15;
            ItemListDestination.Location = new Point(405, 37);
            ItemListDestination.Name = "ItemListDestination";
            ItemListDestination.SelectionMode = SelectionMode.MultiSimple;
            ItemListDestination.Size = new Size(195, 229);
            ItemListDestination.TabIndex = 1;
            ItemListDestination.SelectedIndexChanged += ItemListDestination_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FromListDestinationToSource);
            groupBox1.Controls.Add(FromListSourceToDestination);
            groupBox1.Controls.Add(ItemListSource);
            groupBox1.Controls.Add(ItemListDestination);
            groupBox1.Location = new Point(15, 16);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(723, 343);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // FromListDestinationToSource
            // 
            FromListDestinationToSource.AutoSize = true;
            FromListDestinationToSource.BackColor = SystemColors.ControlLight;
            FromListDestinationToSource.Font = new Font("Segoe UI", 15F);
            FromListDestinationToSource.Location = new Point(307, 156);
            FromListDestinationToSource.Margin = new Padding(1);
            FromListDestinationToSource.Name = "FromListDestinationToSource";
            FromListDestinationToSource.Padding = new Padding(1);
            FromListDestinationToSource.RightToLeft = RightToLeft.No;
            FromListDestinationToSource.Size = new Size(42, 30);
            FromListDestinationToSource.TabIndex = 5;
            FromListDestinationToSource.Text = "<<";
            FromListDestinationToSource.TextAlign = ContentAlignment.MiddleCenter;
            FromListDestinationToSource.Click += FromListDestinationToSource_Click;
            // 
            // FromListSourceToDestination
            // 
            FromListSourceToDestination.AutoSize = true;
            FromListSourceToDestination.BackColor = SystemColors.ControlLight;
            FromListSourceToDestination.Font = new Font("Segoe UI", 15F);
            FromListSourceToDestination.Location = new Point(307, 102);
            FromListSourceToDestination.Margin = new Padding(1);
            FromListSourceToDestination.Name = "FromListSourceToDestination";
            FromListSourceToDestination.Padding = new Padding(1);
            FromListSourceToDestination.RightToLeft = RightToLeft.No;
            FromListSourceToDestination.Size = new Size(42, 30);
            FromListSourceToDestination.TabIndex = 2;
            FromListSourceToDestination.Text = ">>";
            FromListSourceToDestination.TextAlign = ContentAlignment.MiddleCenter;
            FromListSourceToDestination.Click += FromListSourceToDestination_Click;
            // 
            // RelocateSelectionItemUsCtr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "RelocateSelectionItemUsCtr";
            Size = new Size(800, 386);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }


        #endregion

        private ListBox ItemListSource;
        private ListBox ItemListDestination;
        private GroupBox groupBox1;
        private Label FromListSourceToDestination;
        private Label FromListDestinationToSource;
    }
}
