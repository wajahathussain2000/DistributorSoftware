namespace DistributionSoftware.Presentation.Forms
{
    partial class GRNForm
    {
        private System.ComponentModel.IContainer components = null;
        
        // Control declarations
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox grnInfoGroup;
        private System.Windows.Forms.Label lblGRNNo;
        private System.Windows.Forms.TextBox txtGRNNo;
        private System.Windows.Forms.Label lblPurchase;
        private System.Windows.Forms.ComboBox cmbPurchase;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label lblGRNDate;
        private System.Windows.Forms.DateTimePicker dtpGRNDate;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.GroupBox barcodeGroup;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.GroupBox itemsGroup;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblReceivedQty;
        private System.Windows.Forms.TextBox txtReceivedQty;
        private System.Windows.Forms.Label lblAcceptedQty;
        private System.Windows.Forms.TextBox txtAcceptedQty;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.DataGridView dgvGRNItems;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnSaveDraft;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox grnListGroup;
        private System.Windows.Forms.DataGridView dgvGRNList;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.grnInfoGroup = new System.Windows.Forms.GroupBox();
            this.lblGRNNo = new System.Windows.Forms.Label();
            this.txtGRNNo = new System.Windows.Forms.TextBox();
            this.lblPurchase = new System.Windows.Forms.Label();
            this.cmbPurchase = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.lblGRNDate = new System.Windows.Forms.Label();
            this.dtpGRNDate = new System.Windows.Forms.DateTimePicker();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.barcodeGroup = new System.Windows.Forms.GroupBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.itemsGroup = new System.Windows.Forms.GroupBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblReceivedQty = new System.Windows.Forms.Label();
            this.txtReceivedQty = new System.Windows.Forms.TextBox();
            this.lblAcceptedQty = new System.Windows.Forms.Label();
            this.txtAcceptedQty = new System.Windows.Forms.TextBox();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.dgvGRNItems = new System.Windows.Forms.DataGridView();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnSaveDraft = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.grnListGroup = new System.Windows.Forms.GroupBox();
            this.dgvGRNList = new System.Windows.Forms.DataGridView();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.grnInfoGroup.SuspendLayout();
            this.barcodeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.itemsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNItems)).BeginInit();
            this.actionsGroup.SuspendLayout();
            this.grnListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNList)).BeginInit();
            this.SuspendLayout();
            
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1400, 60);
            this.headerPanel.TabIndex = 0;
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Goods Receipt Note (GRN) Management";
            
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Controls.Add(this.grnInfoGroup);
            this.contentPanel.Controls.Add(this.barcodeGroup);
            this.contentPanel.Controls.Add(this.itemsGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            this.contentPanel.Controls.Add(this.grnListGroup);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 60);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.TabIndex = 1;
            
            // 
            // grnInfoGroup
            // 
            this.grnInfoGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.grnInfoGroup.Controls.Add(this.lblGRNNo);
            this.grnInfoGroup.Controls.Add(this.txtGRNNo);
            this.grnInfoGroup.Controls.Add(this.lblPurchase);
            this.grnInfoGroup.Controls.Add(this.cmbPurchase);
            this.grnInfoGroup.Controls.Add(this.lblSupplier);
            this.grnInfoGroup.Controls.Add(this.txtSupplier);
            this.grnInfoGroup.Controls.Add(this.lblGRNDate);
            this.grnInfoGroup.Controls.Add(this.dtpGRNDate);
            this.grnInfoGroup.Controls.Add(this.lblRemarks);
            this.grnInfoGroup.Controls.Add(this.txtRemarks);
            this.grnInfoGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grnInfoGroup.Location = new System.Drawing.Point(20, 20);
            this.grnInfoGroup.Name = "grnInfoGroup";
            this.grnInfoGroup.Size = new System.Drawing.Size(1000, 200);
            this.grnInfoGroup.TabIndex = 0;
            this.grnInfoGroup.TabStop = false;
            this.grnInfoGroup.Text = "GRN Information";
            
            // 
            // lblGRNNo
            // 
            this.lblGRNNo.AutoSize = true;
            this.lblGRNNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGRNNo.Location = new System.Drawing.Point(20, 30);
            this.lblGRNNo.Name = "lblGRNNo";
            this.lblGRNNo.Size = new System.Drawing.Size(60, 15);
            this.lblGRNNo.TabIndex = 0;
            this.lblGRNNo.Text = "GRN No:";
            
            // 
            // txtGRNNo
            // 
            this.txtGRNNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGRNNo.Location = new System.Drawing.Point(130, 28);
            this.txtGRNNo.Name = "txtGRNNo";
            this.txtGRNNo.ReadOnly = true;
            this.txtGRNNo.Size = new System.Drawing.Size(150, 23);
            this.txtGRNNo.TabIndex = 1;
            
            // 
            // lblPurchase
            // 
            this.lblPurchase.AutoSize = true;
            this.lblPurchase.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPurchase.Location = new System.Drawing.Point(300, 30);
            this.lblPurchase.Name = "lblPurchase";
            this.lblPurchase.Size = new System.Drawing.Size(60, 15);
            this.lblPurchase.TabIndex = 2;
            this.lblPurchase.Text = "Purchase:";
            
            // 
            // cmbPurchase
            // 
            this.cmbPurchase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPurchase.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPurchase.FormattingEnabled = true;
            this.cmbPurchase.Location = new System.Drawing.Point(390, 28);
            this.cmbPurchase.Name = "cmbPurchase";
            this.cmbPurchase.Size = new System.Drawing.Size(200, 23);
            this.cmbPurchase.TabIndex = 3;
            this.cmbPurchase.SelectedIndexChanged += new System.EventHandler(this.CmbPurchase_SelectedIndexChanged);
            
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSupplier.Location = new System.Drawing.Point(20, 70);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(50, 15);
            this.lblSupplier.TabIndex = 4;
            this.lblSupplier.Text = "Supplier:";
            
            // 
            // txtSupplier
            // 
            this.txtSupplier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSupplier.Location = new System.Drawing.Point(130, 68);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.ReadOnly = true;
            this.txtSupplier.Size = new System.Drawing.Size(200, 23);
            this.txtSupplier.TabIndex = 5;
            
            // 
            // lblGRNDate
            // 
            this.lblGRNDate.AutoSize = true;
            this.lblGRNDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGRNDate.Location = new System.Drawing.Point(350, 70);
            this.lblGRNDate.Name = "lblGRNDate";
            this.lblGRNDate.Size = new System.Drawing.Size(65, 15);
            this.lblGRNDate.TabIndex = 6;
            this.lblGRNDate.Text = "GRN Date:";
            
            // 
            // dtpGRNDate
            // 
            this.dtpGRNDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpGRNDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGRNDate.Location = new System.Drawing.Point(450, 68);
            this.dtpGRNDate.Name = "dtpGRNDate";
            this.dtpGRNDate.Size = new System.Drawing.Size(200, 23);
            this.dtpGRNDate.TabIndex = 7;
            
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRemarks.Location = new System.Drawing.Point(20, 110);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(55, 15);
            this.lblRemarks.TabIndex = 8;
            this.lblRemarks.Text = "Remarks:";
            
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRemarks.Location = new System.Drawing.Point(130, 108);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.Size = new System.Drawing.Size(520, 60);
            this.txtRemarks.TabIndex = 9;
            
            // 
            // barcodeGroup
            // 
            this.barcodeGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.barcodeGroup.Controls.Add(this.lblBarcode);
            this.barcodeGroup.Controls.Add(this.txtBarcode);
            this.barcodeGroup.Controls.Add(this.picBarcode);
            this.barcodeGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.barcodeGroup.Location = new System.Drawing.Point(1040, 20);
            this.barcodeGroup.Name = "barcodeGroup";
            this.barcodeGroup.Size = new System.Drawing.Size(400, 200);
            this.barcodeGroup.TabIndex = 1;
            this.barcodeGroup.TabStop = false;
            this.barcodeGroup.Text = "Barcode";
            
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBarcode.Location = new System.Drawing.Point(20, 30);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(50, 15);
            this.lblBarcode.TabIndex = 0;
            this.lblBarcode.Text = "Barcode:";
            
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBarcode.Location = new System.Drawing.Point(110, 28);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.ReadOnly = true;
            this.txtBarcode.Size = new System.Drawing.Size(150, 23);
            this.txtBarcode.TabIndex = 1;
            
            // 
            // picBarcode
            // 
            this.picBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBarcode.Location = new System.Drawing.Point(20, 70);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(300, 100);
            this.picBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBarcode.TabIndex = 2;
            this.picBarcode.TabStop = false;
            
            // 
            // itemsGroup
            // 
            this.itemsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.itemsGroup.Controls.Add(this.lblProduct);
            this.itemsGroup.Controls.Add(this.cmbProduct);
            this.itemsGroup.Controls.Add(this.lblReceivedQty);
            this.itemsGroup.Controls.Add(this.txtReceivedQty);
            this.itemsGroup.Controls.Add(this.lblAcceptedQty);
            this.itemsGroup.Controls.Add(this.txtAcceptedQty);
            this.itemsGroup.Controls.Add(this.lblBatchNo);
            this.itemsGroup.Controls.Add(this.txtBatchNo);
            this.itemsGroup.Controls.Add(this.lblExpiryDate);
            this.itemsGroup.Controls.Add(this.dtpExpiryDate);
            this.itemsGroup.Controls.Add(this.btnAddItem);
            this.itemsGroup.Controls.Add(this.btnRemoveItem);
            this.itemsGroup.Controls.Add(this.dgvGRNItems);
            this.itemsGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.itemsGroup.Location = new System.Drawing.Point(20, 240);
            this.itemsGroup.Name = "itemsGroup";
            this.itemsGroup.Size = new System.Drawing.Size(1000, 240);
            this.itemsGroup.TabIndex = 2;
            this.itemsGroup.TabStop = false;
            this.itemsGroup.Text = "GRN Items";
            
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProduct.Location = new System.Drawing.Point(20, 30);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(50, 15);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product:";
            
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(110, 28);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(150, 23);
            this.cmbProduct.TabIndex = 1;
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.CmbProduct_SelectedIndexChanged);
            
            // 
            // lblReceivedQty
            // 
            this.lblReceivedQty.AutoSize = true;
            this.lblReceivedQty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReceivedQty.Location = new System.Drawing.Point(280, 30);
            this.lblReceivedQty.Name = "lblReceivedQty";
            this.lblReceivedQty.Size = new System.Drawing.Size(80, 15);
            this.lblReceivedQty.TabIndex = 2;
            this.lblReceivedQty.Text = "Received Qty:";
            
            // 
            // txtReceivedQty
            // 
            this.txtReceivedQty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReceivedQty.Location = new System.Drawing.Point(380, 28);
            this.txtReceivedQty.Name = "txtReceivedQty";
            this.txtReceivedQty.Size = new System.Drawing.Size(80, 23);
            this.txtReceivedQty.TabIndex = 3;
            
            // 
            // lblAcceptedQty
            // 
            this.lblAcceptedQty.AutoSize = true;
            this.lblAcceptedQty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAcceptedQty.Location = new System.Drawing.Point(480, 30);
            this.lblAcceptedQty.Name = "lblAcceptedQty";
            this.lblAcceptedQty.Size = new System.Drawing.Size(80, 15);
            this.lblAcceptedQty.TabIndex = 4;
            this.lblAcceptedQty.Text = "Accepted Qty:";
            
            // 
            // txtAcceptedQty
            // 
            this.txtAcceptedQty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAcceptedQty.Location = new System.Drawing.Point(580, 28);
            this.txtAcceptedQty.Name = "txtAcceptedQty";
            this.txtAcceptedQty.Size = new System.Drawing.Size(80, 23);
            this.txtAcceptedQty.TabIndex = 5;
            
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBatchNo.Location = new System.Drawing.Point(20, 70);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(60, 15);
            this.lblBatchNo.TabIndex = 6;
            this.lblBatchNo.Text = "Batch No:";
            
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBatchNo.Location = new System.Drawing.Point(110, 68);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(150, 23);
            this.txtBatchNo.TabIndex = 7;
            
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExpiryDate.Location = new System.Drawing.Point(280, 70);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(70, 15);
            this.lblExpiryDate.TabIndex = 8;
            this.lblExpiryDate.Text = "Expiry Date:";
            
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiryDate.Location = new System.Drawing.Point(370, 68);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(150, 23);
            this.dtpExpiryDate.TabIndex = 9;
            
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Location = new System.Drawing.Point(540, 68);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(80, 30);
            this.btnAddItem.TabIndex = 10;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.BtnAddItem_Click);
            
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(540, 110);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(80, 30);
            this.btnRemoveItem.TabIndex = 11;
            this.btnRemoveItem.Text = "Remove";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            
            // 
            // dgvGRNItems
            // 
            this.dgvGRNItems.AllowUserToAddRows = true;
            this.dgvGRNItems.AllowUserToDeleteRows = true;
            this.dgvGRNItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGRNItems.Location = new System.Drawing.Point(20, 150);
            this.dgvGRNItems.MultiSelect = false;
            this.dgvGRNItems.Name = "dgvGRNItems";
            this.dgvGRNItems.ReadOnly = false;
            this.dgvGRNItems.RowHeadersVisible = false;
            this.dgvGRNItems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvGRNItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGRNItems.Size = new System.Drawing.Size(450, 80);
            this.dgvGRNItems.TabIndex = 12;
            this.dgvGRNItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvGRNItems_CellClick);
            this.dgvGRNItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvGRNItems_CellValueChanged);
            
            // 
            // actionsGroup
            // 
            this.actionsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionsGroup.Controls.Add(this.btnSaveDraft);
            this.actionsGroup.Controls.Add(this.btnPost);
            this.actionsGroup.Controls.Add(this.btnPrint);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.actionsGroup.Location = new System.Drawing.Point(20, 500);
            this.actionsGroup.Name = "actionsGroup";
            this.actionsGroup.Size = new System.Drawing.Size(650, 80);
            this.actionsGroup.TabIndex = 3;
            this.actionsGroup.TabStop = false;
            this.actionsGroup.Text = "Actions";
            
            // 
            // btnSaveDraft
            // 
            this.btnSaveDraft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSaveDraft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDraft.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveDraft.ForeColor = System.Drawing.Color.White;
            this.btnSaveDraft.Location = new System.Drawing.Point(20, 30);
            this.btnSaveDraft.Name = "btnSaveDraft";
            this.btnSaveDraft.Size = new System.Drawing.Size(120, 40);
            this.btnSaveDraft.TabIndex = 0;
            this.btnSaveDraft.Text = "💾 Save Draft";
            this.btnSaveDraft.UseVisualStyleBackColor = false;
            this.btnSaveDraft.Click += new System.EventHandler(this.BtnSaveDraft_Click);
            
            // 
            // btnPost
            // 
            this.btnPost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPost.ForeColor = System.Drawing.Color.White;
            this.btnPost.Location = new System.Drawing.Point(160, 30);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(120, 40);
            this.btnPost.TabIndex = 1;
            this.btnPost.Text = "📋 Post";
            this.btnPost.UseVisualStyleBackColor = false;
            this.btnPost.Click += new System.EventHandler(this.BtnPost_Click);
            
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(300, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 40);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "🖨️ Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(440, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 40);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "🔄 Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            
            // 
            // grnListGroup
            // 
            this.grnListGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grnListGroup.Controls.Add(this.dgvGRNList);
            this.grnListGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grnListGroup.Location = new System.Drawing.Point(20, 600);
            this.grnListGroup.Name = "grnListGroup";
            this.grnListGroup.Size = new System.Drawing.Size(650, 200);
            this.grnListGroup.TabIndex = 4;
            this.grnListGroup.TabStop = false;
            this.grnListGroup.Text = "GRN List";
            
            // 
            // dgvGRNList
            // 
            this.dgvGRNList.AllowUserToAddRows = false;
            this.dgvGRNList.AllowUserToDeleteRows = false;
            this.dgvGRNList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGRNList.Location = new System.Drawing.Point(20, 30);
            this.dgvGRNList.MultiSelect = false;
            this.dgvGRNList.Name = "dgvGRNList";
            this.dgvGRNList.ReadOnly = true;
            this.dgvGRNList.RowHeadersVisible = false;
            this.dgvGRNList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvGRNList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGRNList.Size = new System.Drawing.Size(610, 150);
            this.dgvGRNList.TabIndex = 0;
            this.dgvGRNList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvGRNList_CellClick);
            
            // 
            // GRNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1000, 850);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.MinimumSize = new System.Drawing.Size(950, 800);
            this.Name = "GRNForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Goods Receipt Note (GRN) Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.grnInfoGroup.ResumeLayout(false);
            this.grnInfoGroup.PerformLayout();
            this.barcodeGroup.ResumeLayout(false);
            this.barcodeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.itemsGroup.ResumeLayout(false);
            this.itemsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNItems)).EndInit();
            this.actionsGroup.ResumeLayout(false);
            this.grnListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNList)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
