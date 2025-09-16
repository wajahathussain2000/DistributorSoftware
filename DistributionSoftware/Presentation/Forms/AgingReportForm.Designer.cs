namespace DistributionSoftware.Presentation.Forms
{
    partial class AgingReportForm
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
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblOverdueDays = new System.Windows.Forms.Label();
            this.cmbOverdueDays = new System.Windows.Forms.ComboBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.panelFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.Location = new System.Drawing.Point(12, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(60, 15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer:";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(78, 12);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(200, 23);
            this.cmbCustomer.TabIndex = 1;
            // 
            // lblOverdueDays
            // 
            this.lblOverdueDays.AutoSize = true;
            this.lblOverdueDays.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOverdueDays.Location = new System.Drawing.Point(300, 15);
            this.lblOverdueDays.Name = "lblOverdueDays";
            this.lblOverdueDays.Size = new System.Drawing.Size(85, 15);
            this.lblOverdueDays.TabIndex = 2;
            this.lblOverdueDays.Text = "Overdue Days:";
            // 
            // cmbOverdueDays
            // 
            this.cmbOverdueDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOverdueDays.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbOverdueDays.FormattingEnabled = true;
            this.cmbOverdueDays.Location = new System.Drawing.Point(391, 12);
            this.cmbOverdueDays.Name = "cmbOverdueDays";
            this.cmbOverdueDays.Size = new System.Drawing.Size(120, 23);
            this.cmbOverdueDays.TabIndex = 3;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGenerateReport.FlatAppearance.BorderSize = 0;
            this.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerateReport.ForeColor = System.Drawing.Color.White;
            this.btnGenerateReport.Location = new System.Drawing.Point(12, 50);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(120, 35);
            this.btnGenerateReport.TabIndex = 4;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = false;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnExportPDF.FlatAppearance.BorderSize = 0;
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPDF.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportPDF.ForeColor = System.Drawing.Color.White;
            this.btnExportPDF.Location = new System.Drawing.Point(150, 50);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(100, 35);
            this.btnExportPDF.TabIndex = 5;
            this.btnExportPDF.Text = "Export PDF";
            this.btnExportPDF.UseVisualStyleBackColor = false;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(270, 50);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 35);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.lblCustomer);
            this.panelFilters.Controls.Add(this.cmbCustomer);
            this.panelFilters.Controls.Add(this.lblOverdueDays);
            this.panelFilters.Controls.Add(this.cmbOverdueDays);
            this.panelFilters.Controls.Add(this.btnGenerateReport);
            this.panelFilters.Controls.Add(this.btnExportPDF);
            this.panelFilters.Controls.Add(this.btnClose);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 0);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1200, 100);
            this.panelFilters.TabIndex = 7;
            // 
            // AgingReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelFilters);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "AgingReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Aging Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblOverdueDays;
        private System.Windows.Forms.ComboBox cmbOverdueDays;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelFilters;
    }
}
