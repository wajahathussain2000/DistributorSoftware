using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class SupplierMasterForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.headerLabel = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.supplierGroup = new System.Windows.Forms.GroupBox();
            this.lblSupplierCode = new System.Windows.Forms.Label();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblZipCode = new System.Windows.Forms.Label();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.lblGST = new System.Windows.Forms.Label();
            this.txtGST = new System.Windows.Forms.TextBox();
            this.lblNTN = new System.Windows.Forms.Label();
            this.txtNTN = new System.Windows.Forms.TextBox();
            this.lblPaymentTermsFrom = new System.Windows.Forms.Label();
            this.dtpPaymentTermsFrom = new System.Windows.Forms.DateTimePicker();
            this.lblPaymentTermsTo = new System.Windows.Forms.Label();
            this.dtpPaymentTermsTo = new System.Windows.Forms.DateTimePicker();
            this.lblPaymentTermsDays = new System.Windows.Forms.Label();
            this.txtPaymentTermsDays = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.suppliersGroup = new System.Windows.Forms.GroupBox();
            this.dgvSuppliers = new System.Windows.Forms.DataGridView();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.supplierGroup.SuspendLayout();
            this.suppliersGroup.SuspendLayout();
            this.actionsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).BeginInit();
            this.SuspendLayout();
            
            // Form setup
            this.Text = "Supplier Master - Distribution Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Size = new Size(1200, 700);
            this.MinimumSize = new Size(1100, 600);
            
            // Header Panel
            this.headerPanel.BackColor = Color.FromArgb(52, 73, 94);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 80;
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Controls.Add(this.closeBtn);
            
            this.headerLabel.Text = "🏢 Supplier Master";
            this.headerLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            this.headerLabel.ForeColor = Color.White;
            this.headerLabel.Location = new Point(20, 20);
            this.headerLabel.AutoSize = true;
            
            this.closeBtn.Text = "✕";
            this.closeBtn.Size = new Size(40, 40);
            this.closeBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.closeBtn.Location = new Point(this.Width - 80, 20);
            this.closeBtn.FlatStyle = FlatStyle.Flat;
            this.closeBtn.BackColor = Color.FromArgb(231, 76, 60);
            this.closeBtn.ForeColor = Color.White;
            this.closeBtn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.closeBtn.Click += (s, e) => this.Close();
            
            // Content Panel
            this.contentPanel.Dock = DockStyle.Fill;
            this.contentPanel.Padding = new Padding(20);
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Controls.Add(this.supplierGroup);
            this.contentPanel.Controls.Add(this.suppliersGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            
            // Supplier Group
            this.supplierGroup.Text = "📝 Supplier Information";
            this.supplierGroup.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.supplierGroup.Location = new Point(20, 20);
            this.supplierGroup.Size = new Size(1160, 300);
            this.supplierGroup.Controls.Add(this.lblSupplierCode);
            this.supplierGroup.Controls.Add(this.txtSupplierCode);
            this.supplierGroup.Controls.Add(this.lblSupplierName);
            this.supplierGroup.Controls.Add(this.txtSupplierName);
            this.supplierGroup.Controls.Add(this.lblContactPerson);
            this.supplierGroup.Controls.Add(this.txtContactPerson);
            this.supplierGroup.Controls.Add(this.lblPhone);
            this.supplierGroup.Controls.Add(this.txtPhone);
            this.supplierGroup.Controls.Add(this.lblEmail);
            this.supplierGroup.Controls.Add(this.txtEmail);
            this.supplierGroup.Controls.Add(this.lblAddress);
            this.supplierGroup.Controls.Add(this.txtAddress);
            this.supplierGroup.Controls.Add(this.lblCity);
            this.supplierGroup.Controls.Add(this.txtCity);
            this.supplierGroup.Controls.Add(this.lblState);
            this.supplierGroup.Controls.Add(this.txtState);
            this.supplierGroup.Controls.Add(this.lblZipCode);
            this.supplierGroup.Controls.Add(this.txtZipCode);
            this.supplierGroup.Controls.Add(this.lblCountry);
            this.supplierGroup.Controls.Add(this.txtCountry);
            this.supplierGroup.Controls.Add(this.lblGST);
            this.supplierGroup.Controls.Add(this.txtGST);
            this.supplierGroup.Controls.Add(this.lblNTN);
            this.supplierGroup.Controls.Add(this.txtNTN);
            this.supplierGroup.Controls.Add(this.lblPaymentTermsFrom);
            this.supplierGroup.Controls.Add(this.dtpPaymentTermsFrom);
            this.supplierGroup.Controls.Add(this.lblPaymentTermsTo);
            this.supplierGroup.Controls.Add(this.dtpPaymentTermsTo);
            this.supplierGroup.Controls.Add(this.lblPaymentTermsDays);
            this.supplierGroup.Controls.Add(this.txtPaymentTermsDays);
            this.supplierGroup.Controls.Add(this.chkIsActive);
            this.supplierGroup.Controls.Add(this.lblNotes);
            this.supplierGroup.Controls.Add(this.txtNotes);
            this.supplierGroup.Controls.Add(this.picBarcode);
            this.supplierGroup.Controls.Add(this.picQRCode);
            
            // Add all the control definitions here...
            this.lblSupplierCode.Text = "Supplier Code:";
            this.lblSupplierCode.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblSupplierCode.Location = new Point(20, 30);
            this.lblSupplierCode.AutoSize = true;
            
            this.txtSupplierCode.Font = new Font("Segoe UI", 10);
            this.txtSupplierCode.Location = new Point(20, 55);
            this.txtSupplierCode.Size = new Size(200, 26);
            
            this.lblSupplierName.Text = "Supplier Name:";
            this.lblSupplierName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblSupplierName.Location = new Point(250, 30);
            this.lblSupplierName.AutoSize = true;
            
            this.txtSupplierName.Font = new Font("Segoe UI", 10);
            this.txtSupplierName.Location = new Point(250, 55);
            this.txtSupplierName.Size = new Size(300, 26);
            
            this.lblContactPerson.Text = "Contact Person:";
            this.lblContactPerson.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblContactPerson.Location = new Point(580, 30);
            this.lblContactPerson.AutoSize = true;
            
            this.txtContactPerson.Font = new Font("Segoe UI", 10);
            this.txtContactPerson.Location = new Point(580, 55);
            this.txtContactPerson.Size = new Size(200, 26);
            
            this.lblPhone.Text = "Phone:";
            this.lblPhone.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblPhone.Location = new Point(20, 100);
            this.lblPhone.AutoSize = true;
            
            this.txtPhone.Font = new Font("Segoe UI", 10);
            this.txtPhone.Location = new Point(20, 125);
            this.txtPhone.Size = new Size(200, 26);
            
            this.lblEmail.Text = "Email:";
            this.lblEmail.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblEmail.Location = new Point(250, 100);
            this.lblEmail.AutoSize = true;
            
            this.txtEmail.Font = new Font("Segoe UI", 10);
            this.txtEmail.Location = new Point(250, 125);
            this.txtEmail.Size = new Size(300, 26);
            
            this.lblAddress.Text = "Address:";
            this.lblAddress.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblAddress.Location = new Point(580, 100);
            this.lblAddress.AutoSize = true;
            
            this.txtAddress.Font = new Font("Segoe UI", 10);
            this.txtAddress.Location = new Point(580, 125);
            this.txtAddress.Multiline = true;
            this.txtAddress.ScrollBars = ScrollBars.Vertical;
            this.txtAddress.Size = new Size(200, 50);
            
            this.lblCity.Text = "City:";
            this.lblCity.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblCity.Location = new Point(20, 190);
            this.lblCity.AutoSize = true;
            
            this.txtCity.Font = new Font("Segoe UI", 10);
            this.txtCity.Location = new Point(20, 215);
            this.txtCity.Size = new Size(150, 26);
            
            this.lblState.Text = "State:";
            this.lblState.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblState.Location = new Point(190, 190);
            this.lblState.AutoSize = true;
            
            this.txtState.Font = new Font("Segoe UI", 10);
            this.txtState.Location = new Point(190, 215);
            this.txtState.Size = new Size(150, 26);
            
            this.lblZipCode.Text = "Zip Code:";
            this.lblZipCode.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblZipCode.Location = new Point(360, 190);
            this.lblZipCode.AutoSize = true;
            
            this.txtZipCode.Font = new Font("Segoe UI", 10);
            this.txtZipCode.Location = new Point(360, 215);
            this.txtZipCode.Size = new Size(100, 26);
            
            this.lblCountry.Text = "Country:";
            this.lblCountry.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblCountry.Location = new Point(480, 190);
            this.lblCountry.AutoSize = true;
            
            this.txtCountry.Font = new Font("Segoe UI", 10);
            this.txtCountry.Location = new Point(480, 215);
            this.txtCountry.Size = new Size(150, 26);
            
            this.lblGST.Text = "GST:";
            this.lblGST.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblGST.Location = new Point(650, 190);
            this.lblGST.AutoSize = true;
            
            this.txtGST.Font = new Font("Segoe UI", 10);
            this.txtGST.Location = new Point(650, 215);
            this.txtGST.Size = new Size(150, 26);
            
            this.lblNTN.Text = "NTN:";
            this.lblNTN.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblNTN.Location = new Point(820, 190);
            this.lblNTN.AutoSize = true;
            
            this.txtNTN.Font = new Font("Segoe UI", 10);
            this.txtNTN.Location = new Point(820, 215);
            this.txtNTN.Size = new Size(150, 26);
            
            this.lblPaymentTermsFrom.Text = "Payment Terms From:";
            this.lblPaymentTermsFrom.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblPaymentTermsFrom.Location = new Point(20, 250);
            this.lblPaymentTermsFrom.AutoSize = true;
            
            this.dtpPaymentTermsFrom.Font = new Font("Segoe UI", 10);
            this.dtpPaymentTermsFrom.Format = DateTimePickerFormat.Short;
            this.dtpPaymentTermsFrom.Location = new Point(20, 275);
            this.dtpPaymentTermsFrom.Size = new Size(120, 26);
            
            this.lblPaymentTermsTo.Text = "Payment Terms To:";
            this.lblPaymentTermsTo.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblPaymentTermsTo.Location = new Point(160, 250);
            this.lblPaymentTermsTo.AutoSize = true;
            
            this.dtpPaymentTermsTo.Font = new Font("Segoe UI", 10);
            this.dtpPaymentTermsTo.Format = DateTimePickerFormat.Short;
            this.dtpPaymentTermsTo.Location = new Point(160, 275);
            this.dtpPaymentTermsTo.Size = new Size(120, 26);
            
            this.lblPaymentTermsDays.Text = "Payment Terms Days:";
            this.lblPaymentTermsDays.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblPaymentTermsDays.Location = new Point(300, 250);
            this.lblPaymentTermsDays.AutoSize = true;
            
            this.txtPaymentTermsDays.Font = new Font("Segoe UI", 10);
            this.txtPaymentTermsDays.Location = new Point(300, 275);
            this.txtPaymentTermsDays.Size = new Size(100, 26);
            
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.chkIsActive.Location = new Point(450, 250);
            this.chkIsActive.AutoSize = true;
            
            this.lblNotes.Text = "Notes:";
            this.lblNotes.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblNotes.Location = new Point(580, 250);
            this.lblNotes.AutoSize = true;
            
            this.txtNotes.Font = new Font("Segoe UI", 10);
            this.txtNotes.Location = new Point(580, 275);
            this.txtNotes.Multiline = true;
            this.txtNotes.ScrollBars = ScrollBars.Vertical;
            this.txtNotes.Size = new Size(200, 50);
            
            this.picBarcode.BorderStyle = BorderStyle.FixedSingle;
            this.picBarcode.Location = new Point(800, 250);
            this.picBarcode.Size = new Size(100, 50);
            
            this.picQRCode.BorderStyle = BorderStyle.FixedSingle;
            this.picQRCode.Location = new Point(920, 250);
            this.picQRCode.Size = new Size(100, 50);
            
            // Suppliers Group
            this.suppliersGroup.Text = "📋 Suppliers List";
            this.suppliersGroup.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.suppliersGroup.Location = new Point(20, 340);
            this.suppliersGroup.Size = new Size(1160, 300);
            this.suppliersGroup.Controls.Add(this.dgvSuppliers);
            
            this.dgvSuppliers.AllowUserToAddRows = false;
            this.dgvSuppliers.AllowUserToDeleteRows = false;
            this.dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuppliers.BackgroundColor = Color.White;
            this.dgvSuppliers.BorderStyle = BorderStyle.Fixed3D;
            this.dgvSuppliers.Font = new Font("Segoe UI", 9);
            this.dgvSuppliers.Location = new Point(20, 30);
            this.dgvSuppliers.MultiSelect = false;
            this.dgvSuppliers.ReadOnly = true;
            this.dgvSuppliers.RowHeadersVisible = false;
            this.dgvSuppliers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuppliers.Size = new Size(1120, 250);
            this.dgvSuppliers.CellClick += new DataGridViewCellEventHandler(this.SuppliersGrid_CellClick);
            
            // Actions Group
            this.actionsGroup.Text = "⚡ Actions";
            this.actionsGroup.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.actionsGroup.Location = new Point(20, 660);
            this.actionsGroup.Size = new Size(1160, 80);
            this.actionsGroup.Controls.Add(this.btnSave);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Controls.Add(this.btnDelete);
            
            this.btnSave.Text = "💾 Save";
            this.btnSave.BackColor = Color.FromArgb(46, 204, 113);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(20, 30);
            this.btnSave.Size = new Size(100, 35);
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);
            
            this.btnClear.Text = "🗑️ Clear";
            this.btnClear.BackColor = Color.FromArgb(241, 196, 15);
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnClear.ForeColor = Color.White;
            this.btnClear.Location = new Point(140, 30);
            this.btnClear.Size = new Size(100, 35);
            this.btnClear.Click += new EventHandler(this.BtnClear_Click);
            
            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Location = new Point(260, 30);
            this.btnDelete.Size = new Size(100, 35);
            this.btnDelete.Click += new EventHandler(this.BtnDelete_Click);
            
            // Add controls to form
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox supplierGroup;
        private System.Windows.Forms.Label lblSupplierCode;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.Label lblContactPerson;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label lblZipCode;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label lblGST;
        private System.Windows.Forms.TextBox txtGST;
        private System.Windows.Forms.Label lblNTN;
        private System.Windows.Forms.TextBox txtNTN;
        private System.Windows.Forms.Label lblPaymentTermsFrom;
        private System.Windows.Forms.DateTimePicker dtpPaymentTermsFrom;
        private System.Windows.Forms.Label lblPaymentTermsTo;
        private System.Windows.Forms.DateTimePicker dtpPaymentTermsTo;
        private System.Windows.Forms.Label lblPaymentTermsDays;
        private System.Windows.Forms.TextBox txtPaymentTermsDays;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.GroupBox suppliersGroup;
        private System.Windows.Forms.DataGridView dgvSuppliers;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
    }
}
