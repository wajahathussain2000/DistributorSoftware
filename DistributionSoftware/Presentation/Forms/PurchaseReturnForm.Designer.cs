namespace DistributionSoftware.Presentation.Forms
{
    partial class PurchaseReturnForm
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
            this.SuspendLayout();

            // Form settings
            this.Text = "Purchase Return Management";
            this.Size = new System.Drawing.Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Size = new System.Drawing.Size(1360, 80),
                Location = new System.Drawing.Point(20, 10),
                BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label headerLabel = new Label
            {
                Text = "🔄 Purchase Return Management",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };

            headerPanel.Controls.Add(headerLabel);

            // Content Panel
            Panel contentPanel = new Panel
            {
                Size = new System.Drawing.Size(1360, 600),
                Location = new System.Drawing.Point(20, 100),
                BackColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            // Purchase Return Header Group
            GroupBox headerGroup = new GroupBox
            {
                Text = "📋 Purchase Return Information",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(1320, 200),
                Location = new System.Drawing.Point(20, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Return Number
            Label returnNoLabel = new Label { Text = "🔢 Return No:", Location = new System.Drawing.Point(20, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtReturnNumber = new TextBox { Name = "txtReturnNumber", Location = new System.Drawing.Point(20, 55), Size = new System.Drawing.Size(200, 25), Font = new System.Drawing.Font("Segoe UI", 10), ReadOnly = true, BackColor = System.Drawing.Color.FromArgb(240, 240, 240) };

            // Barcode
            Label barcodeLabel = new Label { Text = "📊 Barcode:", Location = new System.Drawing.Point(240, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtBarcode = new TextBox { Name = "txtBarcode", Location = new System.Drawing.Point(240, 55), Size = new System.Drawing.Size(200, 25), Font = new System.Drawing.Font("Segoe UI", 10), ReadOnly = true, BackColor = System.Drawing.Color.FromArgb(240, 240, 240) };

            // Supplier
            Label supplierLabel = new Label { Text = "🏢 Supplier:", Location = new System.Drawing.Point(460, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            cmbSupplier = new ComboBox { Name = "cmbSupplier", Location = new System.Drawing.Point(460, 55), Size = new System.Drawing.Size(250, 25), Font = new System.Drawing.Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };

            // Reference Purchase
            Label refPurchaseLabel = new Label { Text = "📄 Reference Purchase:", Location = new System.Drawing.Point(730, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            cmbReferencePurchase = new ComboBox { Name = "cmbReferencePurchase", Location = new System.Drawing.Point(730, 55), Size = new System.Drawing.Size(200, 25), Font = new System.Drawing.Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbReferencePurchase.SelectedIndexChanged += CmbReferencePurchase_SelectedIndexChanged;

            // Return Date
            Label returnDateLabel = new Label { Text = "📅 Return Date:", Location = new System.Drawing.Point(950, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            dtpReturnDate = new DateTimePicker { Name = "dtpReturnDate", Location = new System.Drawing.Point(950, 55), Size = new System.Drawing.Size(150, 25), Font = new System.Drawing.Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };

            // Tax Adjust
            Label taxAdjustLabel = new Label { Text = "💰 Tax Adjust:", Location = new System.Drawing.Point(20, 90), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtTaxAdjust = new TextBox { Name = "txtTaxAdjust", Location = new System.Drawing.Point(20, 115), Size = new System.Drawing.Size(100, 25), Font = new System.Drawing.Font("Segoe UI", 10) };
            txtTaxAdjust.TextChanged += TxtTaxAdjust_TextChanged;

            // Discount Adjust
            Label discountAdjustLabel = new Label { Text = "💸 Discount Adjust:", Location = new System.Drawing.Point(140, 90), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtDiscountAdjust = new TextBox { Name = "txtDiscountAdjust", Location = new System.Drawing.Point(140, 115), Size = new System.Drawing.Size(100, 25), Font = new System.Drawing.Font("Segoe UI", 10) };
            txtDiscountAdjust.TextChanged += TxtDiscountAdjust_TextChanged;

            // Freight Adjust
            Label freightAdjustLabel = new Label { Text = "🚚 Freight Adjust:", Location = new System.Drawing.Point(260, 90), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtFreightAdjust = new TextBox { Name = "txtFreightAdjust", Location = new System.Drawing.Point(260, 115), Size = new System.Drawing.Size(100, 25), Font = new System.Drawing.Font("Segoe UI", 10) };
            txtFreightAdjust.TextChanged += TxtFreightAdjust_TextChanged;

            // Net Return Amount
            Label netAmountLabel = new Label { Text = "💵 Net Return Amount:", Location = new System.Drawing.Point(380, 90), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtNetReturnAmount = new TextBox { Name = "txtNetReturnAmount", Location = new System.Drawing.Point(380, 115), Size = new System.Drawing.Size(150, 25), Font = new System.Drawing.Font("Segoe UI", 10), ReadOnly = true, BackColor = System.Drawing.Color.FromArgb(240, 240, 240) };

            // Reason
            Label reasonLabel = new Label { Text = "📝 Reason:", Location = new System.Drawing.Point(550, 90), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtReason = new TextBox { Name = "txtReason", Location = new System.Drawing.Point(550, 115), Size = new System.Drawing.Size(300, 25), Font = new System.Drawing.Font("Segoe UI", 10) };

            // Status
            Label statusLabel = new Label { Text = "📊 Status:", Location = new System.Drawing.Point(870, 90), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            cmbStatus = new ComboBox { Name = "cmbStatus", Location = new System.Drawing.Point(870, 115), Size = new System.Drawing.Size(100, 25), Font = new System.Drawing.Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new object[] { "Draft", "Posted", "Cancelled" });
            cmbStatus.SelectedIndex = 0;

            // Barcode Image
            Label barcodeImageLabel = new Label { Text = "📊 Barcode Image:", Location = new System.Drawing.Point(20, 150), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            picBarcode = new PictureBox { Name = "picBarcode", Location = new System.Drawing.Point(20, 170), Size = new System.Drawing.Size(300, 80), BorderStyle = BorderStyle.FixedSingle, BackColor = System.Drawing.Color.White };

            headerGroup.Controls.AddRange(new Control[] {
                returnNoLabel, txtReturnNumber, barcodeLabel, txtBarcode, supplierLabel, cmbSupplier,
                refPurchaseLabel, cmbReferencePurchase, returnDateLabel, dtpReturnDate,
                taxAdjustLabel, txtTaxAdjust, discountAdjustLabel, txtDiscountAdjust, freightAdjustLabel, txtFreightAdjust,
                netAmountLabel, txtNetReturnAmount, reasonLabel, txtReason, statusLabel, cmbStatus,
                barcodeImageLabel, picBarcode
            });

            // Items Group
            GroupBox itemsGroup = new GroupBox
            {
                Text = "📦 Return Items",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(1320, 200),
                Location = new System.Drawing.Point(20, 240),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Product
            Label productLabel = new Label { Text = "📦 Product:", Location = new System.Drawing.Point(20, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            cmbProduct = new ComboBox { Name = "cmbProduct", Location = new System.Drawing.Point(20, 55), Size = new System.Drawing.Size(200, 25), Font = new System.Drawing.Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;

            // Quantity
            Label quantityLabel = new Label { Text = "🔢 Quantity:", Location = new System.Drawing.Point(240, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtQuantity = new TextBox { Name = "txtQuantity", Location = new System.Drawing.Point(240, 55), Size = new System.Drawing.Size(80, 25), Font = new System.Drawing.Font("Segoe UI", 10) };
            txtQuantity.TextChanged += TxtQuantity_TextChanged;

            // Unit Price
            Label unitPriceLabel = new Label { Text = "💰 Unit Price:", Location = new System.Drawing.Point(340, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtUnitPrice = new TextBox { Name = "txtUnitPrice", Location = new System.Drawing.Point(340, 55), Size = new System.Drawing.Size(100, 25), Font = new System.Drawing.Font("Segoe UI", 10) };
            txtUnitPrice.TextChanged += TxtUnitPrice_TextChanged;

            // Line Total
            Label lineTotalLabel = new Label { Text = "💵 Line Total:", Location = new System.Drawing.Point(460, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtLineTotal = new TextBox { Name = "txtLineTotal", Location = new System.Drawing.Point(460, 55), Size = new System.Drawing.Size(100, 25), Font = new System.Drawing.Font("Segoe UI", 10), ReadOnly = true, BackColor = System.Drawing.Color.FromArgb(240, 240, 240) };

            // Batch Number
            Label batchLabel = new Label { Text = "🏷️ Batch/Expiry:", Location = new System.Drawing.Point(580, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtBatchNumber = new TextBox { Name = "txtBatchNumber", Location = new System.Drawing.Point(580, 55), Size = new System.Drawing.Size(100, 25), Font = new System.Drawing.Font("Segoe UI", 10) };

            // Expiry Date
            dtpExpiryDate = new DateTimePicker { Name = "dtpExpiryDate", Location = new System.Drawing.Point(700, 55), Size = new System.Drawing.Size(120, 25), Font = new System.Drawing.Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };

            // Item Notes
            Label itemNotesLabel = new Label { Text = "📝 Notes:", Location = new System.Drawing.Point(840, 30), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtItemNotes = new TextBox { Name = "txtItemNotes", Location = new System.Drawing.Point(840, 55), Size = new System.Drawing.Size(200, 25), Font = new System.Drawing.Font("Segoe UI", 10) };

            // Add Item Button
            Button btnAddItem = new Button
            {
                Text = "➕ Add Item",
                Location = new System.Drawing.Point(1060, 30),
                Size = new System.Drawing.Size(100, 50),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(46, 204, 113),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAddItem.Click += BtnAddItem_Click;

            // Remove Item Button
            Button btnRemoveItem = new Button
            {
                Text = "➖ Remove",
                Location = new System.Drawing.Point(1170, 30),
                Size = new System.Drawing.Size(100, 50),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(231, 76, 60),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRemoveItem.Click += BtnRemoveItem_Click;

            itemsGroup.Controls.AddRange(new Control[] {
                productLabel, cmbProduct, quantityLabel, txtQuantity, unitPriceLabel, txtUnitPrice,
                lineTotalLabel, txtLineTotal, batchLabel, txtBatchNumber, dtpExpiryDate,
                itemNotesLabel, txtItemNotes, btnAddItem, btnRemoveItem
            });

            // Items Grid
            dgvPurchaseReturnItems = new DataGridView
            {
                Name = "dgvPurchaseReturnItems",
                Location = new System.Drawing.Point(20, 90),
                Size = new System.Drawing.Size(1280, 100),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = System.Drawing.Color.White,
                GridColor = System.Drawing.Color.FromArgb(189, 195, 199),
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular),
                ColumnHeadersHeight = 35,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold),
                    BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                    ForeColor = System.Drawing.Color.White,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular),
                    SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                    SelectionForeColor = System.Drawing.Color.White
                }
            };

            itemsGroup.Controls.Add(dgvPurchaseReturnItems);

            // Purchase Return List Group
            GroupBox listGroup = new GroupBox
            {
                Text = "📋 Purchase Return List",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(1320, 200),
                Location = new System.Drawing.Point(20, 460),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            dgvPurchaseReturnList = new DataGridView
            {
                Name = "dgvPurchaseReturnList",
                Location = new System.Drawing.Point(20, 35),
                Size = new System.Drawing.Size(1280, 150),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = System.Drawing.Color.White,
                GridColor = System.Drawing.Color.FromArgb(189, 195, 199),
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular),
                ColumnHeadersHeight = 35,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold),
                    BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                    ForeColor = System.Drawing.Color.White,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular),
                    SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                    SelectionForeColor = System.Drawing.Color.White
                }
            };
            dgvPurchaseReturnList.CellClick += DgvPurchaseReturnList_CellClick;

            listGroup.Controls.Add(dgvPurchaseReturnList);

            // Add groups to content panel
            contentPanel.Controls.AddRange(new Control[] { headerGroup, itemsGroup, listGroup });

            // Action Panel
            Panel actionPanel = new Panel
            {
                Size = new System.Drawing.Size(1360, 80),
                Location = new System.Drawing.Point(20, 710),
                BackColor = System.Drawing.Color.FromArgb(236, 240, 241),
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Action Buttons
            Button btnSaveDraft = new Button
            {
                Text = "💾 Save Draft",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(120, 40),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSaveDraft.Click += BtnSaveDraft_Click;

            Button btnPost = new Button
            {
                Text = "📤 Post",
                Location = new System.Drawing.Point(160, 20),
                Size = new System.Drawing.Size(120, 40),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(46, 204, 113),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPost.Click += BtnPost_Click;

            Button btnPrint = new Button
            {
                Text = "🖨️ Print",
                Location = new System.Drawing.Point(300, 20),
                Size = new System.Drawing.Size(120, 40),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(155, 89, 182),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPrint.Click += BtnPrint_Click;

            Button btnClear = new Button
            {
                Text = "🗑️ Clear",
                Location = new System.Drawing.Point(440, 20),
                Size = new System.Drawing.Size(120, 40),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(231, 76, 60),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClear.Click += BtnClear_Click;

            actionPanel.Controls.AddRange(new Control[] { btnSaveDraft, btnPost, btnPrint, btnClear });

            // Add panels to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);
            this.Controls.Add(actionPanel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}
