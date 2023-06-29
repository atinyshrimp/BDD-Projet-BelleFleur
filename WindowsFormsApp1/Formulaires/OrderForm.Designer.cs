namespace WindowsFormsApp1.Formulaires
{
    partial class OrderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdate = new FontAwesome.Sharp.IconButton();
            this.btnSave = new FontAwesome.Sharp.IconButton();
            this.cbBouquet = new System.Windows.Forms.ComboBox();
            this.lblBouquet = new System.Windows.Forms.Label();
            this.btnAddProduct = new FontAwesome.Sharp.IconButton();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblRealPrice = new System.Windows.Forms.Label();
            this.cbCustomer = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvOrderContent = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbOrderType = new System.Windows.Forms.ComboBox();
            this.cbOrderState = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOrderMsg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderContent)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.SeaShell;
            this.lblTitle.Location = new System.Drawing.Point(13, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(246, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Informations de la commande";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.ForestGreen;
            this.panel1.Controls.Add(this.lblOrderNo);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 73);
            this.panel1.TabIndex = 2;
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNo.ForeColor = System.Drawing.Color.SeaShell;
            this.lblOrderNo.Location = new System.Drawing.Point(13, 38);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(123, 18);
            this.lblOrderNo.TabIndex = 2;
            this.lblOrderNo.Text = "CMD-XXXX-XXX";
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
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.cbBouquet);
            this.panel2.Controls.Add(this.lblBouquet);
            this.panel2.Controls.Add(this.btnAddProduct);
            this.panel2.Controls.Add(this.lblPrice);
            this.panel2.Controls.Add(this.lblRealPrice);
            this.panel2.Controls.Add(this.cbCustomer);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.dgvOrderContent);
            this.panel2.Controls.Add(this.cbOrderType);
            this.panel2.Controls.Add(this.cbOrderState);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbOrderMsg);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbAddress);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(501, 698);
            this.panel2.TabIndex = 3;
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
            this.btnUpdate.Location = new System.Drawing.Point(189, 635);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 36);
            this.btnUpdate.TabIndex = 24;
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
            this.btnSave.Location = new System.Drawing.Point(178, 635);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 36);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Sauvegarder";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbBouquet
            // 
            this.cbBouquet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbBouquet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbBouquet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBouquet.FormattingEnabled = true;
            this.cbBouquet.Location = new System.Drawing.Point(181, 323);
            this.cbBouquet.Margin = new System.Windows.Forms.Padding(1);
            this.cbBouquet.Name = "cbBouquet";
            this.cbBouquet.Size = new System.Drawing.Size(282, 21);
            this.cbBouquet.TabIndex = 22;
            this.cbBouquet.SelectedIndexChanged += new System.EventHandler(this.cbBouquet_SelectedIndexChanged);
            // 
            // lblBouquet
            // 
            this.lblBouquet.AutoSize = true;
            this.lblBouquet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBouquet.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblBouquet.Location = new System.Drawing.Point(43, 323);
            this.lblBouquet.Name = "lblBouquet";
            this.lblBouquet.Size = new System.Drawing.Size(109, 17);
            this.lblBouquet.TabIndex = 21;
            this.lblBouquet.Text = "Bouquet choisi :";
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.AutoSize = true;
            this.btnAddProduct.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddProduct.BackColor = System.Drawing.Color.Transparent;
            this.btnAddProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddProduct.FlatAppearance.BorderSize = 0;
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProduct.IconChar = FontAwesome.Sharp.IconChar.Add;
            this.btnAddProduct.IconColor = System.Drawing.Color.Black;
            this.btnAddProduct.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAddProduct.IconSize = 15;
            this.btnAddProduct.Location = new System.Drawing.Point(442, 370);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(21, 21);
            this.btnAddProduct.TabIndex = 20;
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Visible = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.lblPrice.Location = new System.Drawing.Point(359, 562);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(104, 17);
            this.lblPrice.TabIndex = 19;
            this.lblPrice.Text = "XXX,XX €";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRealPrice
            // 
            this.lblRealPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRealPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRealPrice.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblRealPrice.Location = new System.Drawing.Point(286, 545);
            this.lblRealPrice.Name = "lblRealPrice";
            this.lblRealPrice.Size = new System.Drawing.Size(177, 17);
            this.lblRealPrice.TabIndex = 18;
            this.lblRealPrice.Text = "Prix : XXX,XX €";
            this.lblRealPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbCustomer
            // 
            this.cbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCustomer.FormattingEnabled = true;
            this.cbCustomer.Location = new System.Drawing.Point(181, 286);
            this.cbCustomer.Margin = new System.Windows.Forms.Padding(1);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Size = new System.Drawing.Size(282, 21);
            this.cbCustomer.TabIndex = 17;
            this.cbCustomer.SelectedIndexChanged += new System.EventHandler(this.cbCustomer_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label7.Location = new System.Drawing.Point(43, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Commandé par :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label6.Location = new System.Drawing.Point(43, 372);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Contenu de la commande :";
            // 
            // dgvOrderContent
            // 
            this.dgvOrderContent.AllowUserToAddRows = false;
            this.dgvOrderContent.AllowUserToDeleteRows = false;
            this.dgvOrderContent.AllowUserToResizeColumns = false;
            this.dgvOrderContent.AllowUserToResizeRows = false;
            this.dgvOrderContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column2,
            this.Column4});
            this.dgvOrderContent.Location = new System.Drawing.Point(46, 392);
            this.dgvOrderContent.Name = "dgvOrderContent";
            this.dgvOrderContent.ReadOnly = true;
            this.dgvOrderContent.RowHeadersVisible = false;
            this.dgvOrderContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderContent.Size = new System.Drawing.Size(417, 150);
            this.dgvOrderContent.TabIndex = 14;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID produit";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Produit";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Quantité";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Prix (en €)";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // cbOrderType
            // 
            this.cbOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderType.FormattingEnabled = true;
            this.cbOrderType.Location = new System.Drawing.Point(181, 140);
            this.cbOrderType.Margin = new System.Windows.Forms.Padding(1);
            this.cbOrderType.Name = "cbOrderType";
            this.cbOrderType.Size = new System.Drawing.Size(282, 21);
            this.cbOrderType.TabIndex = 13;
            this.cbOrderType.SelectedIndexChanged += new System.EventHandler(this.cbOrderType_SelectedIndexChanged);
            // 
            // cbOrderState
            // 
            this.cbOrderState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderState.FormattingEnabled = true;
            this.cbOrderState.Location = new System.Drawing.Point(181, 215);
            this.cbOrderState.Margin = new System.Windows.Forms.Padding(1);
            this.cbOrderState.Name = "cbOrderState";
            this.cbOrderState.Size = new System.Drawing.Size(282, 21);
            this.cbOrderState.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(181, 251);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(1);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(282, 20);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label5.Location = new System.Drawing.Point(43, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Date de livraison :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label1.Location = new System.Drawing.Point(43, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Etat :";
            // 
            // tbOrderMsg
            // 
            this.tbOrderMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOrderMsg.Location = new System.Drawing.Point(181, 178);
            this.tbOrderMsg.MaxLength = 140;
            this.tbOrderMsg.Multiline = true;
            this.tbOrderMsg.Name = "tbOrderMsg";
            this.tbOrderMsg.Size = new System.Drawing.Size(281, 17);
            this.tbOrderMsg.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label4.Location = new System.Drawing.Point(43, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Message :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label3.Location = new System.Drawing.Point(43, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Type de commande :";
            // 
            // tbAddress
            // 
            this.tbAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAddress.Location = new System.Drawing.Point(181, 104);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(281, 20);
            this.tbAddress.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label2.Location = new System.Drawing.Point(43, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Adresse de livraison :";
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(501, 698);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "OrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrderForm";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TextBox tbOrderMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbOrderType;
        public System.Windows.Forms.ComboBox cbOrderState;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.Label lblOrderNo;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cbCustomer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblRealPrice;
        public FontAwesome.Sharp.IconButton btnAddProduct;
        public System.Windows.Forms.ComboBox cbBouquet;
        private System.Windows.Forms.Label lblBouquet;
        public System.Windows.Forms.DataGridView dgvOrderContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        public FontAwesome.Sharp.IconButton btnUpdate;
        public FontAwesome.Sharp.IconButton btnSave;
    }
}