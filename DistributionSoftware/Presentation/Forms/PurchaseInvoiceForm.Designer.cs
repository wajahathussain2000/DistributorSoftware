namespace DistributionSoftware.Presentation.Forms
{
    partial class PurchaseInvoiceForm
    {
        private System.ComponentModel.IContainer components = null;
        
        // Control declarations
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox purchaseInfoGroup;
        private System.Windows.Forms.Label lblPurchaseNo;
        private System.Windows.Forms.TextBox txtPurchaseNo;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label lblDiscountAmount;
        private System.Windows.Forms.TextBox txtDiscountAmount;
        private System.Windows.Forms.Label lblFreightCharges;
        private System.Windows.Forms.TextBox txtFreightCharges;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.TextBox txtNetAmount;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.GroupBox barcodeGroup;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.GroupBox itemsGroup;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.DataGridView dgvPurchaseItems;
        private System.Windows.Forms.GroupBox totalsGroup;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnSaveDraft;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox purchaseListGroup;
        private System.Windows.Forms.DataGridView dgvPurchases;

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
            this.purchaseInfoGroup = new System.Windows.Forms.GroupBox();
            this.lblPurchaseNo = new System.Windows.Forms.Label();
            this.txtPurchaseNo = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.lblTaxAmount = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.lblDiscountAmount = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.TextBox();
            this.lblFreightCharges = new System.Windows.Forms.Label();
            this.txtFreightCharges = new System.Windows.Forms.TextBox();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.barcodeGroup = new System.Windows.Forms.GroupBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.itemsGroup = new System.Windows.Forms.GroupBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.dgvPurchaseItems = new System.Windows.Forms.DataGridView();
            this.totalsGroup = new System.Windows.Forms.GroupBox();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnSaveDraft = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.purchaseListGroup = new System.Windows.Forms.GroupBox();
            this.dgvPurchases = new System.Windows.Forms.DataGridView();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.purchaseInfoGroup.SuspendLayout();
            this.barcodeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.itemsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseItems)).BeginInit();
            this.totalsGroup.SuspendLayout();
            this.actionsGroup.SuspendLayout();
            this.purchaseListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).BeginInit();
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
            this.lblTitle.Text = "Purchase Invoice Management";
            
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Controls.Add(this.purchaseInfoGroup);
            this.contentPanel.Controls.Add(this.barcodeGroup);
            this.contentPanel.Controls.Add(this.itemsGroup);
            this.contentPanel.Controls.Add(this.totalsGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            this.contentPanel.Controls.Add(this.purchaseListGroup);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 60);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.Size = new System.Drawing.Size(1400, 790);
            this.contentPanel.TabIndex = 1;
            
            // 
            // purchaseInfoGroup
            // 
            this.purchaseInfoGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.purchaseInfoGroup.Controls.Add(this.lblPurchaseNo);
            this.purchaseInfoGroup.Controls.Add(this.txtPurchaseNo);
            this.purchaseInfoGroup.Controls.Add(this.lblSupplier);
            this.purchaseInfoGroup.Controls.Add(this.cmbSupplier);
            this.purchaseInfoGroup.Controls.Add(this.lblInvoiceNo);
            this.purchaseInfoGroup.Controls.Add(this.txtInvoiceNo);
            this.purchaseInfoGroup.Controls.Add(this.lblInvoiceDate);
            this.purchaseInfoGroup.Controls.Add(this.dtpInvoiceDate);
            this.purchaseInfoGroup.Controls.Add(this.lblTaxAmount);
            this.purchaseInfoGroup.Controls.Add(this.txtTaxAmount);
            this.purchaseInfoGroup.Controls.Add(this.lblDiscountAmount);
            this.purchaseInfoGroup.Controls.Add(this.txtDiscountAmount);
            this.purchaseInfoGroup.Controls.Add(this.lblFreightCharges);
            this.purchaseInfoGroup.Controls.Add(this.txtFreightCharges);
            this.purchaseInfoGroup.Controls.Add(this.lblNetAmount);
            this.purchaseInfoGroup.Controls.Add(this.txtNetAmount);
            this.purchaseInfoGroup.Controls.Add(this.lblNotes);
            this.purchaseInfoGroup.Controls.Add(this.txtNotes);
            this.purchaseInfoGroup.Controls.Add(this.lblStatus);
            this.purchaseInfoGroup.Controls.Add(this.cmbStatus);
            this.purchaseInfoGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.purchaseInfoGroup.Location = new System.Drawing.Point(20, 20);
            this.purchaseInfoGroup.Name = "purchaseInfoGroup";
            this.purchaseInfoGroup.Size = new System.Drawing.Size(650, 200);
            this.purchaseInfoGroup.TabIndex = 0;
            this.purchaseInfoGroup.TabStop = false;
            this.purchaseInfoGroup.Text = "Purchase Information";
            
            // 
            // lblPurchaseNo
            // 
            this.lblPurchaseNo.AutoSize = true;
            this.lblPurchaseNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPurchaseNo.Location = new System.Drawing.Point(20, 30);
            this.lblPurchaseNo.Name = "lblPurchaseNo";
            this.lblPurchaseNo.Size = new System.Drawing.Size(80, 15);
            this.lblPurchaseNo.TabIndex = 0;
            this.lblPurchaseNo.Text = "Purchase No:";
            
            // 
            // txtPurchaseNo
            // 
            this.txtPurchaseNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPurchaseNo.Location = new System.Drawing.Point(130, 28);
            this.txtPurchaseNo.Name = "txtPurchaseNo";
            this.txtPurchaseNo.ReadOnly = true;
            this.txtPurchaseNo.Size = new System.Drawing.Size(150, 23);
            this.txtPurchaseNo.TabIndex = 1;
            
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSupplier.Location = new System.Drawing.Point(300, 30);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(50, 15);
            this.lblSupplier.TabIndex = 2;
            this.lblSupplier.Text = "Supplier:";
            
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(390, 28);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(200, 23);
            this.cmbSupplier.TabIndex = 3;
            
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInvoiceNo.Location = new System.Drawing.Point(20, 70);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(70, 15);
            this.lblInvoiceNo.TabIndex = 4;
            this.lblInvoiceNo.Text = "Invoice No:";
            
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtInvoiceNo.Location = new System.Drawing.Point(130, 68);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(150, 23);
            this.txtInvoiceNo.TabIndex = 5;
            
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInvoiceDate.Location = new System.Drawing.Point(280, 70);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(75, 15);
            this.lblInvoiceDate.TabIndex = 6;
            this.lblInvoiceDate.Text = "Invoice Date:";
            
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(390, 68);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(200, 23);
            this.dtpInvoiceDate.TabIndex = 7;
            
            // 
            // lblTaxAmount
            // 
            this.lblTaxAmount.AutoSize = true;
            this.lblTaxAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTaxAmount.Location = new System.Drawing.Point(20, 110);
            this.lblTaxAmount.Name = "lblTaxAmount";
            this.lblTaxAmount.Size = new System.Drawing.Size(75, 15);
            this.lblTaxAmount.TabIndex = 8;
            this.lblTaxAmount.Text = "Tax Amount:";
            
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTaxAmount.Location = new System.Drawing.Point(130, 108);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.Size = new System.Drawing.Size(150, 23);
            this.txtTaxAmount.TabIndex = 9;
            this.txtTaxAmount.Text = "0.00";
            this.txtTaxAmount.TextChanged += new System.EventHandler(this.TxtTaxAmount_TextChanged);
            
            // 
            // lblDiscountAmount
            // 
            this.lblDiscountAmount.AutoSize = true;
            this.lblDiscountAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiscountAmount.Location = new System.Drawing.Point(280, 110);
            this.lblDiscountAmount.Name = "lblDiscountAmount";
            this.lblDiscountAmount.Size = new System.Drawing.Size(100, 15);
            this.lblDiscountAmount.TabIndex = 10;
            this.lblDiscountAmount.Text = "Discount Amount:";
            
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiscountAmount.Location = new System.Drawing.Point(420, 108);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Size = new System.Drawing.Size(140, 23);
            this.txtDiscountAmount.TabIndex = 11;
            this.txtDiscountAmount.Text = "0.00";
            this.txtDiscountAmount.TextChanged += new System.EventHandler(this.TxtDiscountAmount_TextChanged);
            
            // 
            // lblFreightCharges
            // 
            this.lblFreightCharges.AutoSize = true;
            this.lblFreightCharges.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFreightCharges.Location = new System.Drawing.Point(20, 150);
            this.lblFreightCharges.Name = "lblFreightCharges";
            this.lblFreightCharges.Size = new System.Drawing.Size(90, 15);
            this.lblFreightCharges.TabIndex = 12;
            this.lblFreightCharges.Text = "Freight Charges:";
            
            // 
            // txtFreightCharges
            // 
            this.txtFreightCharges.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFreightCharges.Location = new System.Drawing.Point(130, 148);
            this.txtFreightCharges.Name = "txtFreightCharges";
            this.txtFreightCharges.Size = new System.Drawing.Size(150, 23);
            this.txtFreightCharges.TabIndex = 13;
            this.txtFreightCharges.Text = "0.00";
            this.txtFreightCharges.TextChanged += new System.EventHandler(this.TxtFreightCharges_TextChanged);
            
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNetAmount.Location = new System.Drawing.Point(300, 150);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(75, 15);
            this.lblNetAmount.TabIndex = 14;
            this.lblNetAmount.Text = "Base Amount:";
            
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNetAmount.Location = new System.Drawing.Point(420, 148);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = false;
            this.txtNetAmount.Size = new System.Drawing.Size(140, 23);
            this.txtNetAmount.TabIndex = 15;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextChanged += new System.EventHandler(this.TxtBaseAmount_TextChanged);
            
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNotes.Location = new System.Drawing.Point(600, 30);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(40, 15);
            this.lblNotes.TabIndex = 16;
            this.lblNotes.Text = "Notes:";
            
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNotes.Location = new System.Drawing.Point(650, 28);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(280, 60);
            this.txtNotes.TabIndex = 17;
            
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(600, 110);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 15);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status:";
            
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Draft",
            "Posted",
            "Cancelled"});
            this.cmbStatus.Location = new System.Drawing.Point(650, 108);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(150, 23);
            this.cmbStatus.TabIndex = 19;
            this.cmbStatus.SelectedIndex = 0;
            
            // 
            // barcodeGroup
            // 
            this.barcodeGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.barcodeGroup.Controls.Add(this.lblBarcode);
            this.barcodeGroup.Controls.Add(this.txtBarcode);
            this.barcodeGroup.Controls.Add(this.picBarcode);
            this.barcodeGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.barcodeGroup.Location = new System.Drawing.Point(690, 20);
            this.barcodeGroup.Name = "barcodeGroup";
            this.barcodeGroup.Size = new System.Drawing.Size(300, 200);
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
            this.itemsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsGroup.Controls.Add(this.lblProduct);
            this.itemsGroup.Controls.Add(this.cmbProduct);
            this.itemsGroup.Controls.Add(this.lblQuantity);
            this.itemsGroup.Controls.Add(this.txtQuantity);
            this.itemsGroup.Controls.Add(this.lblUnitPrice);
            this.itemsGroup.Controls.Add(this.txtUnitPrice);
            this.itemsGroup.Controls.Add(this.lblBatchNo);
            this.itemsGroup.Controls.Add(this.txtBatchNo);
            this.itemsGroup.Controls.Add(this.lblExpiryDate);
            this.itemsGroup.Controls.Add(this.dtpExpiryDate);
            this.itemsGroup.Controls.Add(this.btnAddItem);
            this.itemsGroup.Controls.Add(this.btnRemoveItem);
            this.itemsGroup.Controls.Add(this.dgvPurchaseItems);
            this.itemsGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.itemsGroup.Location = new System.Drawing.Point(20, 240);
            this.itemsGroup.Name = "itemsGroup";
            this.itemsGroup.Size = new System.Drawing.Size(650, 240);
            this.itemsGroup.TabIndex = 2;
            this.itemsGroup.TabStop = false;
            this.itemsGroup.Text = "Purchase Items";
            
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
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuantity.Location = new System.Drawing.Point(280, 30);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(55, 15);
            this.lblQuantity.TabIndex = 2;
            this.lblQuantity.Text = "Quantity:";
            
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtQuantity.Location = new System.Drawing.Point(350, 28);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(80, 23);
            this.txtQuantity.TabIndex = 3;
            
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUnitPrice.Location = new System.Drawing.Point(450, 30);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(65, 15);
            this.lblUnitPrice.TabIndex = 4;
            this.lblUnitPrice.Text = "Unit Price:";
            
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUnitPrice.Location = new System.Drawing.Point(540, 28);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(80, 23);
            this.txtUnitPrice.TabIndex = 5;
            
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
            // dgvPurchaseItems
            // 
            this.dgvPurchaseItems.AllowUserToAddRows = true;
            this.dgvPurchaseItems.AllowUserToDeleteRows = true;
            this.dgvPurchaseItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseItems.Location = new System.Drawing.Point(20, 150);
            this.dgvPurchaseItems.MultiSelect = false;
            this.dgvPurchaseItems.Name = "dgvPurchaseItems";
            this.dgvPurchaseItems.ReadOnly = false;
            this.dgvPurchaseItems.RowHeadersVisible = false;
            this.dgvPurchaseItems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvPurchaseItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseItems.Size = new System.Drawing.Size(600, 80);
            this.dgvPurchaseItems.TabIndex = 12;
            this.dgvPurchaseItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchaseItems_CellClick);
            this.dgvPurchaseItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchaseItems_CellValueChanged);
            
            // 
            // totalsGroup
            // 
            this.totalsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalsGroup.Controls.Add(this.lblSubTotal);
            this.totalsGroup.Controls.Add(this.txtSubTotal);
            this.totalsGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.totalsGroup.Location = new System.Drawing.Point(690, 240);
            this.totalsGroup.Name = "totalsGroup";
            this.totalsGroup.Size = new System.Drawing.Size(300, 100);
            this.totalsGroup.TabIndex = 3;
            this.totalsGroup.TabStop = false;
            this.totalsGroup.Text = "Totals";
            
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTotal.Location = new System.Drawing.Point(20, 30);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(60, 15);
            this.lblSubTotal.TabIndex = 0;
            this.lblSubTotal.Text = "Sub Total:";
            
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSubTotal.Location = new System.Drawing.Point(110, 28);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.ReadOnly = true;
            this.txtSubTotal.Size = new System.Drawing.Size(150, 23);
            this.txtSubTotal.TabIndex = 1;
            this.txtSubTotal.Text = "0.00";
            
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
            this.actionsGroup.Size = new System.Drawing.Size(970, 80);
            this.actionsGroup.TabIndex = 4;
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
            // purchaseListGroup
            // 
            this.purchaseListGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.purchaseListGroup.Controls.Add(this.dgvPurchases);
            this.purchaseListGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.purchaseListGroup.Location = new System.Drawing.Point(20, 600);
            this.purchaseListGroup.Name = "purchaseListGroup";
            this.purchaseListGroup.Size = new System.Drawing.Size(970, 200);
            this.purchaseListGroup.TabIndex = 5;
            this.purchaseListGroup.TabStop = false;
            this.purchaseListGroup.Text = "Purchase List";
            
            // 
            // dgvPurchases
            // 
            this.dgvPurchases.AllowUserToAddRows = false;
            this.dgvPurchases.AllowUserToDeleteRows = false;
            this.dgvPurchases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchases.Location = new System.Drawing.Point(20, 30);
            this.dgvPurchases.MultiSelect = false;
            this.dgvPurchases.Name = "dgvPurchases";
            this.dgvPurchases.ReadOnly = true;
            this.dgvPurchases.RowHeadersVisible = false;
            this.dgvPurchases.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvPurchases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchases.Size = new System.Drawing.Size(930, 150);
            this.dgvPurchases.TabIndex = 0;
            this.dgvPurchases.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellClick);
            
            // 
            // PurchaseInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1400, 850);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "PurchaseInvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Purchase Invoice Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.purchaseInfoGroup.ResumeLayout(false);
            this.purchaseInfoGroup.PerformLayout();
            this.barcodeGroup.ResumeLayout(false);
            this.barcodeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.itemsGroup.ResumeLayout(false);
            this.itemsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseItems)).EndInit();
            this.totalsGroup.ResumeLayout(false);
            this.totalsGroup.PerformLayout();
            this.actionsGroup.ResumeLayout(false);
            this.purchaseListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
