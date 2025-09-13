namespace DistributionSoftware.Presentation.Forms
{
    partial class SupplierDebitNoteForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.grpBasicInfo = new System.Windows.Forms.GroupBox();
            this.lblDebitNoteNo = new System.Windows.Forms.Label();
            this.txtDebitNoteNo = new System.Windows.Forms.TextBox();
            this.lblDebitNoteBarcode = new System.Windows.Forms.Label();
            this.txtDebitNoteBarcode = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblDebitDate = new System.Windows.Forms.Label();
            this.dtpDebitDate = new System.Windows.Forms.DateTimePicker();
            this.lblReason = new System.Windows.Forms.Label();
            this.cmbReason = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.grpReference = new System.Windows.Forms.GroupBox();
            this.lblOriginalInvoice = new System.Windows.Forms.Label();
            this.cmbOriginalInvoice = new System.Windows.Forms.ComboBox();
            this.lblOriginalInvoiceAmount = new System.Windows.Forms.Label();
            this.txtOriginalInvoiceAmount = new System.Windows.Forms.TextBox();
            this.lblReferencePurchase = new System.Windows.Forms.Label();
            this.cmbReferencePurchase = new System.Windows.Forms.ComboBox();
            this.lblReferenceGRN = new System.Windows.Forms.Label();
            this.cmbReferenceGRN = new System.Windows.Forms.ComboBox();
            this.pnlItemsAndTotals = new System.Windows.Forms.Panel();
            this.grpTotals = new System.Windows.Forms.GroupBox();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.lblTaxAmount = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.pnlItemButtons = new System.Windows.Forms.Panel();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.pnlItems = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.grpBasicInfo.SuspendLayout();
            this.grpReference.SuspendLayout();
            this.pnlItemsAndTotals.SuspendLayout();
            this.grpTotals.SuspendLayout();
            this.pnlItemButtons.SuspendLayout();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Supplier Debit Note";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(1000, 700);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.Controls.Add(this.grpBasicInfo);
            this.pnlContent.Controls.Add(this.grpReference);
            this.pnlContent.Controls.Add(this.pnlItemsAndTotals);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(10, 10);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(980, 680);
            this.pnlContent.TabIndex = 0;
            // 
            // grpBasicInfo
            // 
            this.grpBasicInfo.Controls.Add(this.lblDebitNoteNo);
            this.grpBasicInfo.Controls.Add(this.txtDebitNoteNo);
            this.grpBasicInfo.Controls.Add(this.lblDebitNoteBarcode);
            this.grpBasicInfo.Controls.Add(this.txtDebitNoteBarcode);
            this.grpBasicInfo.Controls.Add(this.lblSupplier);
            this.grpBasicInfo.Controls.Add(this.cmbSupplier);
            this.grpBasicInfo.Controls.Add(this.lblDebitDate);
            this.grpBasicInfo.Controls.Add(this.dtpDebitDate);
            this.grpBasicInfo.Controls.Add(this.lblReason);
            this.grpBasicInfo.Controls.Add(this.cmbReason);
            this.grpBasicInfo.Controls.Add(this.lblStatus);
            this.grpBasicInfo.Controls.Add(this.cmbStatus);
            this.grpBasicInfo.Controls.Add(this.lblRemarks);
            this.grpBasicInfo.Controls.Add(this.txtRemarks);
            this.grpBasicInfo.Location = new System.Drawing.Point(6, 6);
            this.grpBasicInfo.Name = "grpBasicInfo";
            this.grpBasicInfo.Size = new System.Drawing.Size(960, 200);
            this.grpBasicInfo.TabIndex = 0;
            this.grpBasicInfo.TabStop = false;
            this.grpBasicInfo.Text = "Debit Note Information";
            // 
            // lblDebitNoteNo
            // 
            this.lblDebitNoteNo.AutoSize = true;
            this.lblDebitNoteNo.Location = new System.Drawing.Point(20, 30);
            this.lblDebitNoteNo.Name = "lblDebitNoteNo";
            this.lblDebitNoteNo.Size = new System.Drawing.Size(85, 13);
            this.lblDebitNoteNo.TabIndex = 0;
            this.lblDebitNoteNo.Text = "Debit Note No:";
            // 
            // txtDebitNoteNo
            // 
            this.txtDebitNoteNo.Location = new System.Drawing.Point(120, 30);
            this.txtDebitNoteNo.Name = "txtDebitNoteNo";
            this.txtDebitNoteNo.ReadOnly = true;
            this.txtDebitNoteNo.Size = new System.Drawing.Size(150, 20);
            this.txtDebitNoteNo.TabIndex = 1;
            // 
            // lblDebitNoteBarcode
            // 
            this.lblDebitNoteBarcode.AutoSize = true;
            this.lblDebitNoteBarcode.Location = new System.Drawing.Point(300, 30);
            this.lblDebitNoteBarcode.Name = "lblDebitNoteBarcode";
            this.lblDebitNoteBarcode.Size = new System.Drawing.Size(50, 13);
            this.lblDebitNoteBarcode.TabIndex = 2;
            this.lblDebitNoteBarcode.Text = "Barcode:";
            // 
            // txtDebitNoteBarcode
            // 
            this.txtDebitNoteBarcode.Location = new System.Drawing.Point(360, 30);
            this.txtDebitNoteBarcode.Name = "txtDebitNoteBarcode";
            this.txtDebitNoteBarcode.ReadOnly = true;
            this.txtDebitNoteBarcode.Size = new System.Drawing.Size(150, 20);
            this.txtDebitNoteBarcode.TabIndex = 3;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(20, 60);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(48, 13);
            this.lblSupplier.TabIndex = 4;
            this.lblSupplier.Text = "Supplier:";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(120, 60);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(200, 21);
            this.cmbSupplier.TabIndex = 5;
            // 
            // lblDebitDate
            // 
            this.lblDebitDate.AutoSize = true;
            this.lblDebitDate.Location = new System.Drawing.Point(350, 60);
            this.lblDebitDate.Name = "lblDebitDate";
            this.lblDebitDate.Size = new System.Drawing.Size(33, 13);
            this.lblDebitDate.TabIndex = 6;
            this.lblDebitDate.Text = "Date:";
            // 
            // dtpDebitDate
            // 
            this.dtpDebitDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDebitDate.Location = new System.Drawing.Point(390, 60);
            this.dtpDebitDate.Name = "dtpDebitDate";
            this.dtpDebitDate.Size = new System.Drawing.Size(120, 20);
            this.dtpDebitDate.TabIndex = 7;
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(20, 90);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(44, 13);
            this.lblReason.TabIndex = 8;
            this.lblReason.Text = "Reason:";
            // 
            // cmbReason
            // 
            this.cmbReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReason.FormattingEnabled = true;
            this.cmbReason.Location = new System.Drawing.Point(120, 90);
            this.cmbReason.Name = "cmbReason";
            this.cmbReason.Size = new System.Drawing.Size(200, 21);
            this.cmbReason.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(350, 90);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(390, 90);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(120, 21);
            this.cmbStatus.TabIndex = 11;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(20, 120);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(49, 13);
            this.lblRemarks.TabIndex = 12;
            this.lblRemarks.Text = "Remarks:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(120, 120);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(390, 50);
            this.txtRemarks.TabIndex = 13;
            // 
            // grpReference
            // 
            this.grpReference.Controls.Add(this.lblOriginalInvoice);
            this.grpReference.Controls.Add(this.cmbOriginalInvoice);
            this.grpReference.Controls.Add(this.lblOriginalInvoiceAmount);
            this.grpReference.Controls.Add(this.txtOriginalInvoiceAmount);
            this.grpReference.Controls.Add(this.lblReferencePurchase);
            this.grpReference.Controls.Add(this.cmbReferencePurchase);
            this.grpReference.Controls.Add(this.lblReferenceGRN);
            this.grpReference.Controls.Add(this.cmbReferenceGRN);
            this.grpReference.Location = new System.Drawing.Point(6, 220);
            this.grpReference.Name = "grpReference";
            this.grpReference.Size = new System.Drawing.Size(960, 100);
            this.grpReference.TabIndex = 1;
            this.grpReference.TabStop = false;
            this.grpReference.Text = "Reference Information";
            // 
            // lblOriginalInvoice
            // 
            this.lblOriginalInvoice.AutoSize = true;
            this.lblOriginalInvoice.Location = new System.Drawing.Point(20, 30);
            this.lblOriginalInvoice.Name = "lblOriginalInvoice";
            this.lblOriginalInvoice.Size = new System.Drawing.Size(85, 13);
            this.lblOriginalInvoice.TabIndex = 0;
            this.lblOriginalInvoice.Text = "Original Purchase Invoice:";
            // 
            // cmbOriginalInvoice
            // 
            this.cmbOriginalInvoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOriginalInvoice.FormattingEnabled = true;
            this.cmbOriginalInvoice.Location = new System.Drawing.Point(120, 30);
            this.cmbOriginalInvoice.Name = "cmbOriginalInvoice";
            this.cmbOriginalInvoice.Size = new System.Drawing.Size(200, 21);
            this.cmbOriginalInvoice.TabIndex = 1;
            this.cmbOriginalInvoice.SelectedIndexChanged += new System.EventHandler(this.cmbOriginalInvoice_SelectedIndexChanged);
            // 
            // lblOriginalInvoiceAmount
            // 
            this.lblOriginalInvoiceAmount.AutoSize = true;
            this.lblOriginalInvoiceAmount.Location = new System.Drawing.Point(350, 30);
            this.lblOriginalInvoiceAmount.Name = "lblOriginalInvoiceAmount";
            this.lblOriginalInvoiceAmount.Size = new System.Drawing.Size(43, 13);
            this.lblOriginalInvoiceAmount.TabIndex = 2;
            this.lblOriginalInvoiceAmount.Text = "Amount:";
            // 
            // txtOriginalInvoiceAmount
            // 
            this.txtOriginalInvoiceAmount.Location = new System.Drawing.Point(400, 30);
            this.txtOriginalInvoiceAmount.Name = "txtOriginalInvoiceAmount";
            this.txtOriginalInvoiceAmount.ReadOnly = true;
            this.txtOriginalInvoiceAmount.Size = new System.Drawing.Size(100, 20);
            this.txtOriginalInvoiceAmount.TabIndex = 3;
            this.txtOriginalInvoiceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblReferencePurchase
            // 
            this.lblReferencePurchase.AutoSize = true;
            this.lblReferencePurchase.Location = new System.Drawing.Point(20, 60);
            this.lblReferencePurchase.Name = "lblReferencePurchase";
            this.lblReferencePurchase.Size = new System.Drawing.Size(110, 13);
            this.lblReferencePurchase.TabIndex = 4;
            this.lblReferencePurchase.Text = "Reference Purchase:";
            // 
            // cmbReferencePurchase
            // 
            this.cmbReferencePurchase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferencePurchase.FormattingEnabled = true;
            this.cmbReferencePurchase.Location = new System.Drawing.Point(140, 57);
            this.cmbReferencePurchase.Name = "cmbReferencePurchase";
            this.cmbReferencePurchase.Size = new System.Drawing.Size(200, 21);
            this.cmbReferencePurchase.TabIndex = 5;
            // 
            // lblReferenceGRN
            // 
            this.lblReferenceGRN.AutoSize = true;
            this.lblReferenceGRN.Location = new System.Drawing.Point(380, 60);
            this.lblReferenceGRN.Name = "lblReferenceGRN";
            this.lblReferenceGRN.Size = new System.Drawing.Size(85, 13);
            this.lblReferenceGRN.TabIndex = 6;
            this.lblReferenceGRN.Text = "Reference GRN:";
            // 
            // cmbReferenceGRN
            // 
            this.cmbReferenceGRN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferenceGRN.FormattingEnabled = true;
            this.cmbReferenceGRN.Location = new System.Drawing.Point(480, 57);
            this.cmbReferenceGRN.Name = "cmbReferenceGRN";
            this.cmbReferenceGRN.Size = new System.Drawing.Size(200, 21);
            this.cmbReferenceGRN.TabIndex = 7;
            // 
            // pnlItemsAndTotals
            // 
            this.pnlItemsAndTotals.Controls.Add(this.grpTotals);
            this.pnlItemsAndTotals.Controls.Add(this.pnlItemButtons);
            this.pnlItemsAndTotals.Controls.Add(this.pnlItems);
            this.pnlItemsAndTotals.Location = new System.Drawing.Point(10, 350);
            this.pnlItemsAndTotals.Name = "pnlItemsAndTotals";
            this.pnlItemsAndTotals.Size = new System.Drawing.Size(960, 320);
            this.pnlItemsAndTotals.TabIndex = 2;
            // 
            // grpTotals
            // 
            this.grpTotals.Controls.Add(this.lblSubTotal);
            this.grpTotals.Controls.Add(this.txtSubTotal);
            this.grpTotals.Controls.Add(this.lblTaxAmount);
            this.grpTotals.Controls.Add(this.txtTaxAmount);
            this.grpTotals.Controls.Add(this.lblTotalAmount);
            this.grpTotals.Controls.Add(this.txtTotalAmount);
            this.grpTotals.Location = new System.Drawing.Point(6, 430);
            this.grpTotals.Name = "grpTotals";
            this.grpTotals.Size = new System.Drawing.Size(960, 100);
            this.grpTotals.TabIndex = 2;
            this.grpTotals.TabStop = false;
            this.grpTotals.Text = "Amount Summary";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSubTotal.Location = new System.Drawing.Point(20, 30);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(70, 17);
            this.lblSubTotal.TabIndex = 0;
            this.lblSubTotal.Text = "Sub Total:";
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSubTotal.Location = new System.Drawing.Point(120, 27);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.ReadOnly = true;
            this.txtSubTotal.Size = new System.Drawing.Size(150, 23);
            this.txtSubTotal.TabIndex = 1;
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTaxAmount
            // 
            this.lblTaxAmount.AutoSize = true;
            this.lblTaxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblTaxAmount.Location = new System.Drawing.Point(20, 60);
            this.lblTaxAmount.Name = "lblTaxAmount";
            this.lblTaxAmount.Size = new System.Drawing.Size(85, 17);
            this.lblTaxAmount.TabIndex = 2;
            this.lblTaxAmount.Text = "Tax Amount:";
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTaxAmount.Location = new System.Drawing.Point(120, 57);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.ReadOnly = true;
            this.txtTaxAmount.Size = new System.Drawing.Size(150, 23);
            this.txtTaxAmount.TabIndex = 3;
            this.txtTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.Location = new System.Drawing.Point(300, 30);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(49, 20);
            this.lblTotalAmount.TabIndex = 4;
            this.lblTotalAmount.Text = "Total:";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmount.Location = new System.Drawing.Point(360, 27);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(150, 26);
            this.txtTotalAmount.TabIndex = 5;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlItemButtons
            // 
            this.pnlItemButtons.Controls.Add(this.btnAddItem);
            this.pnlItemButtons.Controls.Add(this.btnRemoveItem);
            this.pnlItemButtons.Location = new System.Drawing.Point(6, 400);
            this.pnlItemButtons.Name = "pnlItemButtons";
            this.pnlItemButtons.Size = new System.Drawing.Size(200, 30);
            this.pnlItemButtons.TabIndex = 1;
            this.pnlItemButtons.Visible = false;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(0, 3);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 0;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Visible = false;
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(81, 3);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveItem.TabIndex = 1;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Visible = false;
            // 
            // pnlItems
            // 
            this.pnlItems.Controls.Add(this.dgvItems);
            this.pnlItems.Location = new System.Drawing.Point(6, 6);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(960, 200);
            this.pnlItems.TabIndex = 0;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 0);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(960, 200);
            this.dgvItems.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnApprove);
            this.pnlButtons.Controls.Add(this.btnReject);
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 650);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1000, 50);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(20, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnApprove
            // 
            this.btnApprove.Location = new System.Drawing.Point(101, 15);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(75, 23);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(182, 15);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 23);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(263, 15);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(905, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // SupplierDebitNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(1000, 800);
            this.Name = "SupplierDebitNoteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supplier Debit Note";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.grpBasicInfo.ResumeLayout(false);
            this.grpBasicInfo.PerformLayout();
            this.grpReference.ResumeLayout(false);
            this.grpReference.PerformLayout();
            this.pnlItemsAndTotals.ResumeLayout(false);
            this.grpTotals.ResumeLayout(false);
            this.grpTotals.PerformLayout();
            this.pnlItemButtons.ResumeLayout(false);
            this.pnlItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.GroupBox grpBasicInfo;
        private System.Windows.Forms.Label lblDebitNoteNo;
        private System.Windows.Forms.TextBox txtDebitNoteNo;
        private System.Windows.Forms.Label lblDebitNoteBarcode;
        private System.Windows.Forms.TextBox txtDebitNoteBarcode;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblDebitDate;
        private System.Windows.Forms.DateTimePicker dtpDebitDate;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.ComboBox cmbReason;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.GroupBox grpReference;
        private System.Windows.Forms.Label lblOriginalInvoice;
        private System.Windows.Forms.ComboBox cmbOriginalInvoice;
        private System.Windows.Forms.Label lblOriginalInvoiceAmount;
        private System.Windows.Forms.TextBox txtOriginalInvoiceAmount;
        private System.Windows.Forms.Label lblReferencePurchase;
        private System.Windows.Forms.ComboBox cmbReferencePurchase;
        private System.Windows.Forms.Label lblReferenceGRN;
        private System.Windows.Forms.ComboBox cmbReferenceGRN;
        private System.Windows.Forms.Panel pnlItemsAndTotals;
        private System.Windows.Forms.GroupBox grpTotals;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Panel pnlItemButtons;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Panel pnlItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
    }
}

