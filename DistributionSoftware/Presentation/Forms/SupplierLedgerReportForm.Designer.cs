namespace DistributionSoftware.Presentation.Forms
{
    partial class SupplierLedgerReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ComboBox cmbSupplierFilter;
        private System.Windows.Forms.ComboBox cmbTransactionTypeFilter;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnExportToPDF;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnClose;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblSupplierFilter;
        private System.Windows.Forms.Label lblTransactionTypeFilter;

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
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.cmbSupplierFilter = new System.Windows.Forms.ComboBox();
            this.cmbTransactionTypeFilter = new System.Windows.Forms.ComboBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.btnExportToPDF = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblSupplierFilter = new System.Windows.Forms.Label();
            this.lblTransactionTypeFilter = new System.Windows.Forms.Label();
            this.groupBoxFilters.SuspendLayout();
            this.SuspendLayout();

            // dtpStartDate
            this.dtpStartDate.Location = new System.Drawing.Point(120, 30);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 0;

            // dtpEndDate
            this.dtpEndDate.Location = new System.Drawing.Point(120, 60);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 1;

            // cmbSupplierFilter
            this.cmbSupplierFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierFilter.FormattingEnabled = true;
            this.cmbSupplierFilter.Location = new System.Drawing.Point(120, 90);
            this.cmbSupplierFilter.Name = "cmbSupplierFilter";
            this.cmbSupplierFilter.Size = new System.Drawing.Size(200, 21);
            this.cmbSupplierFilter.TabIndex = 2;

            // cmbTransactionTypeFilter
            this.cmbTransactionTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransactionTypeFilter.FormattingEnabled = true;
            this.cmbTransactionTypeFilter.Location = new System.Drawing.Point(120, 120);
            this.cmbTransactionTypeFilter.Name = "cmbTransactionTypeFilter";
            this.cmbTransactionTypeFilter.Size = new System.Drawing.Size(200, 21);
            this.cmbTransactionTypeFilter.TabIndex = 3;

            // btnGenerateReport
            this.btnGenerateReport.Location = new System.Drawing.Point(350, 30);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(100, 30);
            this.btnGenerateReport.TabIndex = 4;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);

            // btnExportToPDF
            this.btnExportToPDF.Location = new System.Drawing.Point(350, 70);
            this.btnExportToPDF.Name = "btnExportToPDF";
            this.btnExportToPDF.Size = new System.Drawing.Size(100, 30);
            this.btnExportToPDF.TabIndex = 5;
            this.btnExportToPDF.Text = "Export to PDF";
            this.btnExportToPDF.UseVisualStyleBackColor = true;
            this.btnExportToPDF.Click += new System.EventHandler(this.btnExportToPDF_Click);

            // btnExportToExcel
            this.btnExportToExcel.Location = new System.Drawing.Point(350, 110);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(100, 30);
            this.btnExportToExcel.TabIndex = 6;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(350, 150);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // reportViewer1
            this.reportViewer1.Location = new System.Drawing.Point(12, 200);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1000, 500);
            this.reportViewer1.TabIndex = 8;

            // lblStartDate
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(20, 35);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(60, 13);
            this.lblStartDate.TabIndex = 9;
            this.lblStartDate.Text = "Start Date:";

            // lblEndDate
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(20, 65);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(57, 13);
            this.lblEndDate.TabIndex = 10;
            this.lblEndDate.Text = "End Date:";

            // lblSupplierFilter
            this.lblSupplierFilter.AutoSize = true;
            this.lblSupplierFilter.Location = new System.Drawing.Point(20, 95);
            this.lblSupplierFilter.Name = "lblSupplierFilter";
            this.lblSupplierFilter.Size = new System.Drawing.Size(80, 13);
            this.lblSupplierFilter.TabIndex = 11;
            this.lblSupplierFilter.Text = "Supplier Filter:";

            // lblTransactionTypeFilter
            this.lblTransactionTypeFilter.AutoSize = true;
            this.lblTransactionTypeFilter.Location = new System.Drawing.Point(20, 125);
            this.lblTransactionTypeFilter.Name = "lblTransactionTypeFilter";
            this.lblTransactionTypeFilter.Size = new System.Drawing.Size(120, 13);
            this.lblTransactionTypeFilter.TabIndex = 12;
            this.lblTransactionTypeFilter.Text = "Transaction Type Filter:";

            // groupBoxFilters
            this.groupBoxFilters.Controls.Add(this.lblStartDate);
            this.groupBoxFilters.Controls.Add(this.dtpStartDate);
            this.groupBoxFilters.Controls.Add(this.lblEndDate);
            this.groupBoxFilters.Controls.Add(this.dtpEndDate);
            this.groupBoxFilters.Controls.Add(this.lblSupplierFilter);
            this.groupBoxFilters.Controls.Add(this.cmbSupplierFilter);
            this.groupBoxFilters.Controls.Add(this.lblTransactionTypeFilter);
            this.groupBoxFilters.Controls.Add(this.cmbTransactionTypeFilter);
            this.groupBoxFilters.Location = new System.Drawing.Point(12, 12);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(330, 160);
            this.groupBoxFilters.TabIndex = 13;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Report Filters";

            // SupplierLedgerReportForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.btnExportToPDF);
            this.Controls.Add(this.btnGenerateReport);
            this.Name = "SupplierLedgerReportForm";
            this.Text = "Supplier Ledger Report";
            this.Load += new System.EventHandler(this.SupplierLedgerReportForm_Load);
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
