namespace DistributionSoftware.Presentation.Forms
{
    partial class SupplierBalanceReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DateTimePicker dtpAsOfDate;
        private System.Windows.Forms.ComboBox cmbSupplierFilter;
        private System.Windows.Forms.CheckBox chkShowOnlyOutstanding;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnExportToPDF;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnClose;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.Label lblAsOfDate;
        private System.Windows.Forms.Label lblSupplierFilter;

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
            this.dtpAsOfDate = new System.Windows.Forms.DateTimePicker();
            this.cmbSupplierFilter = new System.Windows.Forms.ComboBox();
            this.chkShowOnlyOutstanding = new System.Windows.Forms.CheckBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.btnExportToPDF = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.lblAsOfDate = new System.Windows.Forms.Label();
            this.lblSupplierFilter = new System.Windows.Forms.Label();
            this.groupBoxFilters.SuspendLayout();
            this.SuspendLayout();

            // dtpAsOfDate
            this.dtpAsOfDate.Location = new System.Drawing.Point(120, 30);
            this.dtpAsOfDate.Name = "dtpAsOfDate";
            this.dtpAsOfDate.Size = new System.Drawing.Size(200, 20);
            this.dtpAsOfDate.TabIndex = 0;

            // cmbSupplierFilter
            this.cmbSupplierFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierFilter.FormattingEnabled = true;
            this.cmbSupplierFilter.Location = new System.Drawing.Point(120, 60);
            this.cmbSupplierFilter.Name = "cmbSupplierFilter";
            this.cmbSupplierFilter.Size = new System.Drawing.Size(200, 21);
            this.cmbSupplierFilter.TabIndex = 1;

            // chkShowOnlyOutstanding
            this.chkShowOnlyOutstanding.AutoSize = true;
            this.chkShowOnlyOutstanding.Location = new System.Drawing.Point(120, 90);
            this.chkShowOnlyOutstanding.Name = "chkShowOnlyOutstanding";
            this.chkShowOnlyOutstanding.Size = new System.Drawing.Size(150, 17);
            this.chkShowOnlyOutstanding.TabIndex = 2;
            this.chkShowOnlyOutstanding.Text = "Show Only Outstanding";
            this.chkShowOnlyOutstanding.UseVisualStyleBackColor = true;

            // btnGenerateReport
            this.btnGenerateReport.Location = new System.Drawing.Point(350, 30);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(100, 30);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);

            // btnExportToPDF
            this.btnExportToPDF.Location = new System.Drawing.Point(350, 70);
            this.btnExportToPDF.Name = "btnExportToPDF";
            this.btnExportToPDF.Size = new System.Drawing.Size(100, 30);
            this.btnExportToPDF.TabIndex = 4;
            this.btnExportToPDF.Text = "Export to PDF";
            this.btnExportToPDF.UseVisualStyleBackColor = true;
            this.btnExportToPDF.Click += new System.EventHandler(this.btnExportToPDF_Click);

            // btnExportToExcel
            this.btnExportToExcel.Location = new System.Drawing.Point(350, 110);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(100, 30);
            this.btnExportToExcel.TabIndex = 5;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(350, 150);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // reportViewer1
            this.reportViewer1.Location = new System.Drawing.Point(12, 200);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1000, 500);
            this.reportViewer1.TabIndex = 7;

            // lblAsOfDate
            this.lblAsOfDate.AutoSize = true;
            this.lblAsOfDate.Location = new System.Drawing.Point(20, 35);
            this.lblAsOfDate.Name = "lblAsOfDate";
            this.lblAsOfDate.Size = new System.Drawing.Size(60, 13);
            this.lblAsOfDate.TabIndex = 8;
            this.lblAsOfDate.Text = "As Of Date:";

            // lblSupplierFilter
            this.lblSupplierFilter.AutoSize = true;
            this.lblSupplierFilter.Location = new System.Drawing.Point(20, 65);
            this.lblSupplierFilter.Name = "lblSupplierFilter";
            this.lblSupplierFilter.Size = new System.Drawing.Size(80, 13);
            this.lblSupplierFilter.TabIndex = 9;
            this.lblSupplierFilter.Text = "Supplier Filter:";

            // groupBoxFilters
            this.groupBoxFilters.Controls.Add(this.lblAsOfDate);
            this.groupBoxFilters.Controls.Add(this.dtpAsOfDate);
            this.groupBoxFilters.Controls.Add(this.lblSupplierFilter);
            this.groupBoxFilters.Controls.Add(this.cmbSupplierFilter);
            this.groupBoxFilters.Controls.Add(this.chkShowOnlyOutstanding);
            this.groupBoxFilters.Location = new System.Drawing.Point(12, 12);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(330, 120);
            this.groupBoxFilters.TabIndex = 10;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Report Filters";

            // SupplierBalanceReportForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.btnExportToPDF);
            this.Controls.Add(this.btnGenerateReport);
            this.Name = "SupplierBalanceReportForm";
            this.Text = "Supplier Balance Report";
            this.Load += new System.EventHandler(this.SupplierBalanceReportForm_Load);
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
