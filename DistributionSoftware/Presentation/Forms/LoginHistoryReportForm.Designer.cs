namespace DistributionSoftware.Presentation.Forms
{
    partial class LoginHistoryReportForm
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
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblLoginStatus = new System.Windows.Forms.Label();
            this.cmbLoginStatus = new System.Windows.Forms.ComboBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.groupBoxFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(30, 46);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(43, 20);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "User:";
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(150, 45);
            this.cmbUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(298, 28);
            this.cmbUser.TabIndex = 1;
            // 
            // lblLoginStatus
            // 
            this.lblLoginStatus.AutoSize = true;
            this.lblLoginStatus.Location = new System.Drawing.Point(480, 46);
            this.lblLoginStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoginStatus.Name = "lblLoginStatus";
            this.lblLoginStatus.Size = new System.Drawing.Size(95, 20);
            this.lblLoginStatus.TabIndex = 2;
            this.lblLoginStatus.Text = "Login Status:";
            // 
            // cmbLoginStatus
            // 
            this.cmbLoginStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoginStatus.FormattingEnabled = true;
            this.cmbLoginStatus.Location = new System.Drawing.Point(600, 45);
            this.cmbLoginStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbLoginStatus.Name = "cmbLoginStatus";
            this.cmbLoginStatus.Size = new System.Drawing.Size(298, 28);
            this.cmbLoginStatus.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(30, 90);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(85, 20);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(150, 89);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(298, 28);
            this.dtpStartDate.TabIndex = 5;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(480, 90);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(79, 20);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(600, 89);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(298, 28);
            this.dtpEndDate.TabIndex = 7;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGenerateReport.FlatAppearance.BorderSize = 0;
            this.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateReport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateReport.ForeColor = System.Drawing.Color.White;
            this.btnGenerateReport.Location = new System.Drawing.Point(480, 133);
            this.btnGenerateReport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(140, 35);
            this.btnGenerateReport.TabIndex = 10;
            this.btnGenerateReport.Text = "Generate";
            this.btnGenerateReport.UseVisualStyleBackColor = false;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnExportPDF.FlatAppearance.BorderSize = 0;
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPDF.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPDF.ForeColor = System.Drawing.Color.White;
            this.btnExportPDF.Location = new System.Drawing.Point(640, 133);
            this.btnExportPDF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(120, 35);
            this.btnExportPDF.TabIndex = 11;
            this.btnExportPDF.Text = "Export";
            this.btnExportPDF.UseVisualStyleBackColor = false;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(780, 133);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBoxFilters
            // 
            this.groupBoxFilters.Controls.Add(this.btnClose);
            this.groupBoxFilters.Controls.Add(this.btnExportPDF);
            this.groupBoxFilters.Controls.Add(this.btnGenerateReport);
            this.groupBoxFilters.Controls.Add(this.dtpEndDate);
            this.groupBoxFilters.Controls.Add(this.lblEndDate);
            this.groupBoxFilters.Controls.Add(this.dtpStartDate);
            this.groupBoxFilters.Controls.Add(this.lblStartDate);
            this.groupBoxFilters.Controls.Add(this.cmbLoginStatus);
            this.groupBoxFilters.Controls.Add(this.lblLoginStatus);
            this.groupBoxFilters.Controls.Add(this.cmbUser);
            this.groupBoxFilters.Controls.Add(this.lblUser);
            this.groupBoxFilters.Location = new System.Drawing.Point(15, 15);
            this.groupBoxFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFilters.Size = new System.Drawing.Size(1380, 180);
            this.groupBoxFilters.TabIndex = 13;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Report Filters";
            // 
            // LoginHistoryReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1425, 900);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Controls.Add(this.groupBoxFilters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginHistoryReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login History Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label lblLoginStatus;
        private System.Windows.Forms.ComboBox cmbLoginStatus;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBoxFilters;
    }
}
