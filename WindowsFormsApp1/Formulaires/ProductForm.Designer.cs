namespace WindowsFormsApp1.Formulaires
{
    partial class ProductForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductForm));
            this.lblBouquet = new System.Windows.Forms.Label();
            this.cbShop = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbProductType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbProductName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdate = new FontAwesome.Sharp.IconButton();
            this.btnSave = new FontAwesome.Sharp.IconButton();
            this.cbOccasion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkGenInfo = new System.Windows.Forms.CheckBox();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.numStock = new System.Windows.Forms.NumericUpDown();
            this.numThreshold = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numEndMonth = new System.Windows.Forms.NumericUpDown();
            this.numBegMonth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBegMonth)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBouquet
            // 
            this.lblBouquet.AutoSize = true;
            this.lblBouquet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBouquet.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblBouquet.Location = new System.Drawing.Point(41, 356);
            this.lblBouquet.Name = "lblBouquet";
            this.lblBouquet.Size = new System.Drawing.Size(51, 17);
            this.lblBouquet.TabIndex = 21;
            this.lblBouquet.Text = "Stock :";
            // 
            // cbShop
            // 
            this.cbShop.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbShop.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShop.Enabled = false;
            this.cbShop.FormattingEnabled = true;
            this.cbShop.Location = new System.Drawing.Point(178, 120);
            this.cbShop.Margin = new System.Windows.Forms.Padding(1);
            this.cbShop.Name = "cbShop";
            this.cbShop.Size = new System.Drawing.Size(282, 21);
            this.cbShop.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label7.Location = new System.Drawing.Point(40, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Boutique :";
            // 
            // cbProductType
            // 
            this.cbProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductType.FormattingEnabled = true;
            this.cbProductType.Location = new System.Drawing.Point(179, 74);
            this.cbProductType.Margin = new System.Windows.Forms.Padding(1);
            this.cbProductType.Name = "cbProductType";
            this.cbProductType.Size = new System.Drawing.Size(282, 21);
            this.cbProductType.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label5.Location = new System.Drawing.Point(40, 409);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Seuil d\'alerte :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label4.Location = new System.Drawing.Point(41, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Prix (en €) :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label3.Location = new System.Drawing.Point(41, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nom du produit :";
            // 
            // tbProductName
            // 
            this.tbProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbProductName.Enabled = false;
            this.tbProductName.Location = new System.Drawing.Point(179, 163);
            this.tbProductName.Name = "tbProductName";
            this.tbProductName.Size = new System.Drawing.Size(281, 20);
            this.tbProductName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label2.Location = new System.Drawing.Point(42, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Type de produit :";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.cbOccasion);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.checkGenInfo);
            this.panel2.Controls.Add(this.numPrice);
            this.panel2.Controls.Add(this.numStock);
            this.panel2.Controls.Add(this.numThreshold);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.numEndMonth);
            this.panel2.Controls.Add(this.numBegMonth);
            this.panel2.Controls.Add(this.lblBouquet);
            this.panel2.Controls.Add(this.cbShop);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cbProductType);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbProductName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(501, 625);
            this.panel2.TabIndex = 5;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUpdate.BackColor = System.Drawing.Color.Goldenrod;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.SeaShell;
            this.btnUpdate.IconChar = FontAwesome.Sharp.IconChar.Rotate;
            this.btnUpdate.IconColor = System.Drawing.Color.SeaShell;
            this.btnUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUpdate.IconSize = 30;
            this.btnUpdate.Location = new System.Drawing.Point(189, 540);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 36);
            this.btnUpdate.TabIndex = 33;
            this.btnUpdate.Text = "Actualiser";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.BackColor = System.Drawing.Color.ForestGreen;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.SeaShell;
            this.btnSave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnSave.IconColor = System.Drawing.Color.SeaShell;
            this.btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSave.IconSize = 30;
            this.btnSave.Location = new System.Drawing.Point(178, 540);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 36);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Sauvegarder";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbOccasion
            // 
            this.cbOccasion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbOccasion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbOccasion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOccasion.Enabled = false;
            this.cbOccasion.FormattingEnabled = true;
            this.cbOccasion.Location = new System.Drawing.Point(179, 209);
            this.cbOccasion.Margin = new System.Windows.Forms.Padding(1);
            this.cbOccasion.Name = "cbOccasion";
            this.cbOccasion.Size = new System.Drawing.Size(282, 21);
            this.cbOccasion.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label6.Location = new System.Drawing.Point(41, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 30;
            this.label6.Text = "Occasion :";
            // 
            // checkGenInfo
            // 
            this.checkGenInfo.AutoSize = true;
            this.checkGenInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkGenInfo.FlatAppearance.BorderSize = 0;
            this.checkGenInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkGenInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkGenInfo.ForeColor = System.Drawing.Color.SaddleBrown;
            this.checkGenInfo.Location = new System.Drawing.Point(45, 28);
            this.checkGenInfo.Name = "checkGenInfo";
            this.checkGenInfo.Size = new System.Drawing.Size(286, 19);
            this.checkGenInfo.TabIndex = 29;
            this.checkGenInfo.Text = "Changer les informations générales du produit ?";
            this.checkGenInfo.UseVisualStyleBackColor = true;
            this.checkGenInfo.CheckedChanged += new System.EventHandler(this.checkGenInfo_CheckedChanged);
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Enabled = false;
            this.numPrice.Location = new System.Drawing.Point(179, 257);
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(282, 20);
            this.numPrice.TabIndex = 28;
            // 
            // numStock
            // 
            this.numStock.Location = new System.Drawing.Point(179, 356);
            this.numStock.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numStock.Name = "numStock";
            this.numStock.Size = new System.Drawing.Size(282, 20);
            this.numStock.TabIndex = 27;
            // 
            // numThreshold
            // 
            this.numThreshold.Enabled = false;
            this.numThreshold.Location = new System.Drawing.Point(178, 409);
            this.numThreshold.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numThreshold.Name = "numThreshold";
            this.numThreshold.Size = new System.Drawing.Size(282, 20);
            this.numThreshold.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label8.Location = new System.Drawing.Point(310, 304);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "à";
            // 
            // numEndMonth
            // 
            this.numEndMonth.Enabled = false;
            this.numEndMonth.Location = new System.Drawing.Point(359, 304);
            this.numEndMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numEndMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEndMonth.Name = "numEndMonth";
            this.numEndMonth.Size = new System.Drawing.Size(101, 20);
            this.numEndMonth.TabIndex = 24;
            this.numEndMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numBegMonth
            // 
            this.numBegMonth.Enabled = false;
            this.numBegMonth.Location = new System.Drawing.Point(179, 304);
            this.numBegMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numBegMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBegMonth.Name = "numBegMonth";
            this.numBegMonth.Size = new System.Drawing.Size(101, 20);
            this.numBegMonth.TabIndex = 23;
            this.numBegMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label1.Location = new System.Drawing.Point(41, 304);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Disponibilité :";
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnClose.IconColor = System.Drawing.Color.Sienna;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 30;
            this.btnClose.Location = new System.Drawing.Point(465, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.ForestGreen;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 73);
            this.panel1.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.SeaShell;
            this.lblTitle.Location = new System.Drawing.Point(12, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(151, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Ajout d\'un produit";
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(501, 698);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductForm";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBegMonth)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblBouquet;
        public System.Windows.Forms.ComboBox cbShop;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cbProductType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tbProductName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown numStock;
        public System.Windows.Forms.NumericUpDown numThreshold;
        public System.Windows.Forms.NumericUpDown numEndMonth;
        public System.Windows.Forms.NumericUpDown numBegMonth;
        public System.Windows.Forms.NumericUpDown numPrice;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.CheckBox checkGenInfo;
        public System.Windows.Forms.ComboBox cbOccasion;
        private System.Windows.Forms.Label label6;
        public FontAwesome.Sharp.IconButton btnUpdate;
        public FontAwesome.Sharp.IconButton btnSave;
    }
}