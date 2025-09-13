using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class CustomerMasterForm
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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.customerGroup = new System.Windows.Forms.GroupBox();
            this.barcodeLabel = new System.Windows.Forms.Label();
            this.barcodePanel = new System.Windows.Forms.Panel();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.qrLabel = new System.Windows.Forms.Label();
            this.qrPanel = new System.Windows.Forms.Panel();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.creditLabel = new System.Windows.Forms.Label();
            this.nudCreditLimit = new System.Windows.Forms.NumericUpDown();
            this.discountLabel = new System.Windows.Forms.Label();
            this.nudDiscount = new System.Windows.Forms.NumericUpDown();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.postalLabel = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.countryLabel = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.stateLabel = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.cityLabel = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.contactLabel = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.codeLabel = new System.Windows.Forms.Label();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.customerListGroup = new System.Windows.Forms.GroupBox();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.customerGroup.SuspendLayout();
            this.barcodePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.qrPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCreditLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).BeginInit();
            this.customerListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.actionsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Controls.Add(this.closeBtn);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1284, 80);
            this.headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(20, 20);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(283, 37);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "👥 Customer Master";
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(1204, 20);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(40, 40);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "✕";
            this.closeBtn.UseVisualStyleBackColor = false;
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Controls.Add(this.customerGroup);
            this.contentPanel.Controls.Add(this.customerListGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 80);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.Size = new System.Drawing.Size(1284, 621);
            this.contentPanel.TabIndex = 1;
            // 
            // customerGroup
            // 
            this.customerGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customerGroup.Controls.Add(this.barcodeLabel);
            this.customerGroup.Controls.Add(this.barcodePanel);
            this.customerGroup.Controls.Add(this.qrLabel);
            this.customerGroup.Controls.Add(this.qrPanel);
            this.customerGroup.Controls.Add(this.creditLabel);
            this.customerGroup.Controls.Add(this.nudCreditLimit);
            this.customerGroup.Controls.Add(this.discountLabel);
            this.customerGroup.Controls.Add(this.nudDiscount);
            this.customerGroup.Controls.Add(this.categoryLabel);
            this.customerGroup.Controls.Add(this.cmbCategory);
            this.customerGroup.Controls.Add(this.postalLabel);
            this.customerGroup.Controls.Add(this.txtPostalCode);
            this.customerGroup.Controls.Add(this.countryLabel);
            this.customerGroup.Controls.Add(this.txtCountry);
            this.customerGroup.Controls.Add(this.stateLabel);
            this.customerGroup.Controls.Add(this.txtState);
            this.customerGroup.Controls.Add(this.cityLabel);
            this.customerGroup.Controls.Add(this.txtCity);
            this.customerGroup.Controls.Add(this.addressLabel);
            this.customerGroup.Controls.Add(this.txtAddress);
            this.customerGroup.Controls.Add(this.emailLabel);
            this.customerGroup.Controls.Add(this.txtEmail);
            this.customerGroup.Controls.Add(this.phoneLabel);
            this.customerGroup.Controls.Add(this.txtPhone);
            this.customerGroup.Controls.Add(this.contactLabel);
            this.customerGroup.Controls.Add(this.txtContactPerson);
            this.customerGroup.Controls.Add(this.nameLabel);
            this.customerGroup.Controls.Add(this.txtCustomerName);
            this.customerGroup.Controls.Add(this.codeLabel);
            this.customerGroup.Controls.Add(this.txtCustomerCode);
            this.customerGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.customerGroup.Location = new System.Drawing.Point(20, 6);
            this.customerGroup.Name = "customerGroup";
            this.customerGroup.Size = new System.Drawing.Size(787, 430);
            this.customerGroup.TabIndex = 0;
            this.customerGroup.TabStop = false;
            this.customerGroup.Text = "📋 Customer Information";
            // 
            // barcodeLabel
            // 
            this.barcodeLabel.AutoSize = true;
            this.barcodeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.barcodeLabel.Location = new System.Drawing.Point(500, 30);
            this.barcodeLabel.Name = "barcodeLabel";
            this.barcodeLabel.Size = new System.Drawing.Size(93, 19);
            this.barcodeLabel.TabIndex = 26;
            this.barcodeLabel.Text = "📊 Barcode:";
            // 
            // barcodePanel
            // 
            this.barcodePanel.BackColor = System.Drawing.Color.White;
            this.barcodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barcodePanel.Controls.Add(this.picBarcode);
            this.barcodePanel.Location = new System.Drawing.Point(500, 55);
            this.barcodePanel.Name = "barcodePanel";
            this.barcodePanel.Size = new System.Drawing.Size(180, 50);
            this.barcodePanel.TabIndex = 27;
            // 
            // picBarcode
            // 
            this.picBarcode.BackColor = System.Drawing.Color.White;
            this.picBarcode.Location = new System.Drawing.Point(5, 5);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(170, 40);
            this.picBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBarcode.TabIndex = 0;
            this.picBarcode.TabStop = false;
            // 
            // qrLabel
            // 
            this.qrLabel.AutoSize = true;
            this.qrLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.qrLabel.Location = new System.Drawing.Point(500, 120);
            this.qrLabel.Name = "qrLabel";
            this.qrLabel.Size = new System.Drawing.Size(96, 19);
            this.qrLabel.TabIndex = 28;
            this.qrLabel.Text = "🔲 QR Code:";
            // 
            // qrPanel
            // 
            this.qrPanel.BackColor = System.Drawing.Color.White;
            this.qrPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.qrPanel.Controls.Add(this.picQRCode);
            this.qrPanel.Location = new System.Drawing.Point(500, 145);
            this.qrPanel.Name = "qrPanel";
            this.qrPanel.Size = new System.Drawing.Size(80, 80);
            this.qrPanel.TabIndex = 29;
            // 
            // picQRCode
            // 
            this.picQRCode.BackColor = System.Drawing.Color.White;
            this.picQRCode.Location = new System.Drawing.Point(5, 5);
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.Size = new System.Drawing.Size(70, 70);
            this.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQRCode.TabIndex = 0;
            this.picQRCode.TabStop = false;
            // 
            // creditLabel
            // 
            this.creditLabel.AutoSize = true;
            this.creditLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.creditLabel.Location = new System.Drawing.Point(520, 390);
            this.creditLabel.Name = "creditLabel";
            this.creditLabel.Size = new System.Drawing.Size(115, 19);
            this.creditLabel.TabIndex = 24;
            this.creditLabel.Text = "💳 Credit Limit:";
            // 
            // nudCreditLimit
            // 
            this.nudCreditLimit.DecimalPlaces = 2;
            this.nudCreditLimit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudCreditLimit.Location = new System.Drawing.Point(670, 388);
            this.nudCreditLimit.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudCreditLimit.Name = "nudCreditLimit";
            this.nudCreditLimit.Size = new System.Drawing.Size(120, 23);
            this.nudCreditLimit.TabIndex = 25;
            // 
            // discountLabel
            // 
            this.discountLabel.AutoSize = true;
            this.discountLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.discountLabel.Location = new System.Drawing.Point(320, 390);
            this.discountLabel.Name = "discountLabel";
            this.discountLabel.Size = new System.Drawing.Size(110, 19);
            this.discountLabel.TabIndex = 22;
            this.discountLabel.Text = "💰 Discount %:";
            // 
            // nudDiscount
            // 
            this.nudDiscount.DecimalPlaces = 2;
            this.nudDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudDiscount.Location = new System.Drawing.Point(470, 388);
            this.nudDiscount.Name = "nudDiscount";
            this.nudDiscount.Size = new System.Drawing.Size(80, 23);
            this.nudDiscount.TabIndex = 23;
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.categoryLabel.Location = new System.Drawing.Point(20, 390);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(100, 19);
            this.categoryLabel.TabIndex = 20;
            this.categoryLabel.Text = "🏷️ Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(150, 388);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(150, 23);
            this.cmbCategory.TabIndex = 21;
            // 
            // postalLabel
            // 
            this.postalLabel.AutoSize = true;
            this.postalLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.postalLabel.Location = new System.Drawing.Point(290, 350);
            this.postalLabel.Name = "postalLabel";
            this.postalLabel.Size = new System.Drawing.Size(117, 19);
            this.postalLabel.TabIndex = 18;
            this.postalLabel.Text = "📮 Postal Code:";
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPostalCode.Location = new System.Drawing.Point(450, 348);
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(100, 23);
            this.txtPostalCode.TabIndex = 19;
            // 
            // countryLabel
            // 
            this.countryLabel.AutoSize = true;
            this.countryLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.countryLabel.Location = new System.Drawing.Point(20, 350);
            this.countryLabel.Name = "countryLabel";
            this.countryLabel.Size = new System.Drawing.Size(91, 19);
            this.countryLabel.TabIndex = 16;
            this.countryLabel.Text = "🌍 Country:";
            // 
            // txtCountry
            // 
            this.txtCountry.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCountry.Location = new System.Drawing.Point(150, 348);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(120, 23);
            this.txtCountry.TabIndex = 17;
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.stateLabel.Location = new System.Drawing.Point(290, 310);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(71, 19);
            this.stateLabel.TabIndex = 14;
            this.stateLabel.Text = "🗺️ State:";
            // 
            // txtState
            // 
            this.txtState.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtState.Location = new System.Drawing.Point(400, 308);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(120, 23);
            this.txtState.TabIndex = 15;
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cityLabel.Location = new System.Drawing.Point(20, 310);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(63, 19);
            this.cityLabel.TabIndex = 12;
            this.cityLabel.Text = "🏙️ City:";
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCity.Location = new System.Drawing.Point(150, 308);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(120, 23);
            this.txtCity.TabIndex = 13;
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.addressLabel.Location = new System.Drawing.Point(20, 230);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(91, 19);
            this.addressLabel.TabIndex = 10;
            this.addressLabel.Text = "🏠 Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAddress.Location = new System.Drawing.Point(150, 228);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(300, 60);
            this.txtAddress.TabIndex = 11;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.emailLabel.Location = new System.Drawing.Point(20, 190);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(73, 19);
            this.emailLabel.TabIndex = 8;
            this.emailLabel.Text = "📧 Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(150, 188);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 23);
            this.txtEmail.TabIndex = 9;
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.phoneLabel.Location = new System.Drawing.Point(20, 150);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(79, 19);
            this.phoneLabel.TabIndex = 6;
            this.phoneLabel.Text = "📱 Phone:";
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPhone.Location = new System.Drawing.Point(150, 148);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(150, 23);
            this.txtPhone.TabIndex = 7;
            // 
            // contactLabel
            // 
            this.contactLabel.AutoSize = true;
            this.contactLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.contactLabel.Location = new System.Drawing.Point(20, 110);
            this.contactLabel.Name = "contactLabel";
            this.contactLabel.Size = new System.Drawing.Size(138, 19);
            this.contactLabel.TabIndex = 4;
            this.contactLabel.Text = "📞 Contact Person:";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtContactPerson.Location = new System.Drawing.Point(200, 108);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(200, 23);
            this.txtContactPerson.TabIndex = 5;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(20, 70);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(145, 19);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "👤 Customer Name:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCustomerName.Location = new System.Drawing.Point(200, 68);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(200, 23);
            this.txtCustomerName.TabIndex = 3;
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.codeLabel.Location = new System.Drawing.Point(20, 30);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(140, 19);
            this.codeLabel.TabIndex = 0;
            this.codeLabel.Text = "🔢 Customer Code:";
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtCustomerCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCustomerCode.Location = new System.Drawing.Point(200, 28);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.ReadOnly = true;
            this.txtCustomerCode.Size = new System.Drawing.Size(150, 23);
            this.txtCustomerCode.TabIndex = 1;
            // 
            // customerListGroup
            // 
            this.customerListGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customerListGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.customerListGroup.Controls.Add(this.dgvCustomers);
            this.customerListGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.customerListGroup.Location = new System.Drawing.Point(790, 100);
            this.customerListGroup.Name = "customerListGroup";
            this.customerListGroup.Size = new System.Drawing.Size(0, 450);
            this.customerListGroup.TabIndex = 1;
            this.customerListGroup.TabStop = false;
            this.customerListGroup.Text = "📋 Customers List";
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomers.ColumnHeadersHeight = 30;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCustomers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvCustomers.Location = new System.Drawing.Point(20, 30);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.Size = new System.Drawing.Size(460, 400);
            this.dgvCustomers.TabIndex = 0;
            // 
            // actionsGroup
            // 
            this.actionsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionsGroup.Controls.Add(this.btnSave);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Controls.Add(this.btnDelete);
            this.actionsGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.actionsGroup.Location = new System.Drawing.Point(20, 470);
            this.actionsGroup.Name = "actionsGroup";
            this.actionsGroup.Size = new System.Drawing.Size(371, 80);
            this.actionsGroup.TabIndex = 2;
            this.actionsGroup.TabStop = false;
            this.actionsGroup.Text = "⚡ Actions";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(140, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "🗑️ Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(260, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // CustomerMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1284, 701);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1278, 670);
            this.Name = "CustomerMasterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Master - Distribution Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.customerGroup.ResumeLayout(false);
            this.customerGroup.PerformLayout();
            this.barcodePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.qrPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCreditLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).EndInit();
            this.customerListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.actionsGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox customerGroup;
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label contactLabel;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label countryLabel;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label postalLabel;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label discountLabel;
        private System.Windows.Forms.NumericUpDown nudDiscount;
        private System.Windows.Forms.Label creditLabel;
        private System.Windows.Forms.NumericUpDown nudCreditLimit;
        private System.Windows.Forms.Label barcodeLabel;
        private System.Windows.Forms.Panel barcodePanel;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.Label qrLabel;
        private System.Windows.Forms.Panel qrPanel;
        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.GroupBox customerListGroup;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
    }
}
