namespace DistributionSoftware.Presentation.Forms
{
    partial class BankReconciliationReportForm
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
            this.lblBankAccount = new System.Windows.Forms.Label();
            this.cmbBankAccount = new System.Windows.Forms.ComboBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBankAccount
            // 
            this.lblBankAccount.AutoSize = true;
            this.lblBankAccount.Location = new System.Drawing.Point(12, 15);
            this.lblBankAccount.Name = "lblBankAccount";
            this.lblBankAccount.Size = new System.Drawing.Size(78, 13);
            this.lblBankAccount.TabIndex = 0;
            this.lblBankAccount.Text = "Bank Account:";
            // 
            // cmbBankAccount
            // 
            this.cmbBankAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankAccount.FormattingEnabled = true;
            this.cmbBankAccount.Location = new System.Drawing.Point(96, 12);
            this.cmbBankAccount.Name = "cmbBankAccount";
            this.cmbBankAccount.Size = new System.Drawing.Size(200, 21);
            this.cmbBankAccount.TabIndex = 1;
            this.cmbBankAccount.SelectedIndexChanged += new System.EventHandler(this.cmbBankAccount_SelectedIndexChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(310, 15);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Start Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(374, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(100, 20);
            this.dtpStartDate.TabIndex = 3;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(490, 15);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 4;
            this.lblEndDate.Text = "End Date:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(551, 12);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(100, 20);
            this.dtpEndDate.TabIndex = 5;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(660, 10);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(80, 25);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPDF.ForeColor = System.Drawing.Color.White;
            this.btnExportPDF.Location = new System.Drawing.Point(746, 10);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(80, 25);
            this.btnExportPDF.TabIndex = 7;
            this.btnExportPDF.Text = "Export PDF";
            this.btnExportPDF.UseVisualStyleBackColor = false;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(832, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 25);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BankReconciliationReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.cmbBankAccount);
            this.Controls.Add(this.lblBankAccount);
            this.Name = "BankReconciliationReportForm";
            this.Text = "Bank Reconciliation Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BankReconciliationReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblBankAccount;
        private System.Windows.Forms.ComboBox cmbBankAccount;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button btnClose;
    }
}
