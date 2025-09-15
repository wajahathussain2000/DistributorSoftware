namespace DistributionSoftware.Presentation.Forms
{
    partial class BankStatementForm
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
            this.pnlForm = new System.Windows.Forms.Panel();
            this.chkIsReconciled = new System.Windows.Forms.CheckBox();
            this.numBalance = new System.Windows.Forms.NumericUpDown();
            this.lblBalance = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.lblAmount = new System.Windows.Forms.Label();
            this.cmbTransactionType = new System.Windows.Forms.ComboBox();
            this.lblTransactionType = new System.Windows.Forms.Label();
            this.txtReferenceNumber = new System.Windows.Forms.TextBox();
            this.lblReferenceNumber = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.lblTransactionDate = new System.Windows.Forms.Label();
            this.cmbBankAccount = new System.Windows.Forms.ComboBox();
            this.lblBankAccount = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(600, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📄 Bank Statement";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.chkIsReconciled);
            this.pnlForm.Controls.Add(this.numBalance);
            this.pnlForm.Controls.Add(this.lblBalance);
            this.pnlForm.Controls.Add(this.numAmount);
            this.pnlForm.Controls.Add(this.lblAmount);
            this.pnlForm.Controls.Add(this.cmbTransactionType);
            this.pnlForm.Controls.Add(this.lblTransactionType);
            this.pnlForm.Controls.Add(this.txtReferenceNumber);
            this.pnlForm.Controls.Add(this.lblReferenceNumber);
            this.pnlForm.Controls.Add(this.txtDescription);
            this.pnlForm.Controls.Add(this.lblDescription);
            this.pnlForm.Controls.Add(this.dtpTransactionDate);
            this.pnlForm.Controls.Add(this.lblTransactionDate);
            this.pnlForm.Controls.Add(this.cmbBankAccount);
            this.pnlForm.Controls.Add(this.lblBankAccount);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 60);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(20);
            this.pnlForm.Size = new System.Drawing.Size(600, 340);
            this.pnlForm.TabIndex = 1;
            // 
            // chkIsReconciled
            // 
            this.chkIsReconciled.AutoSize = true;
            this.chkIsReconciled.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkIsReconciled.Location = new System.Drawing.Point(150, 240);
            this.chkIsReconciled.Name = "chkIsReconciled";
            this.chkIsReconciled.Size = new System.Drawing.Size(100, 23);
            this.chkIsReconciled.TabIndex = 14;
            this.chkIsReconciled.Text = "Reconciled";
            this.chkIsReconciled.UseVisualStyleBackColor = true;
            // 
            // numBalance
            // 
            this.numBalance.DecimalPlaces = 2;
            this.numBalance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numBalance.Location = new System.Drawing.Point(450, 200);
            this.numBalance.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numBalance.Name = "numBalance";
            this.numBalance.Size = new System.Drawing.Size(100, 25);
            this.numBalance.TabIndex = 13;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBalance.Location = new System.Drawing.Point(370, 203);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(60, 19);
            this.lblBalance.TabIndex = 12;
            this.lblBalance.Text = "Balance:";
            // 
            // numAmount
            // 
            this.numAmount.DecimalPlaces = 2;
            this.numAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numAmount.Location = new System.Drawing.Point(150, 200);
            this.numAmount.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(200, 25);
            this.numAmount.TabIndex = 11;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAmount.Location = new System.Drawing.Point(20, 203);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(65, 19);
            this.lblAmount.TabIndex = 10;
            this.lblAmount.Text = "Amount:";
            // 
            // cmbTransactionType
            // 
            this.cmbTransactionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransactionType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTransactionType.FormattingEnabled = true;
            this.cmbTransactionType.Items.AddRange(new object[] {
            "DEBIT",
            "CREDIT"});
            this.cmbTransactionType.Location = new System.Drawing.Point(450, 160);
            this.cmbTransactionType.Name = "cmbTransactionType";
            this.cmbTransactionType.Size = new System.Drawing.Size(100, 25);
            this.cmbTransactionType.TabIndex = 9;
            // 
            // lblTransactionType
            // 
            this.lblTransactionType.AutoSize = true;
            this.lblTransactionType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransactionType.Location = new System.Drawing.Point(370, 163);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Size = new System.Drawing.Size(120, 19);
            this.lblTransactionType.TabIndex = 8;
            this.lblTransactionType.Text = "Transaction Type:";
            // 
            // txtReferenceNumber
            // 
            this.txtReferenceNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferenceNumber.Location = new System.Drawing.Point(150, 160);
            this.txtReferenceNumber.Name = "txtReferenceNumber";
            this.txtReferenceNumber.Size = new System.Drawing.Size(200, 25);
            this.txtReferenceNumber.TabIndex = 7;
            // 
            // lblReferenceNumber
            // 
            this.lblReferenceNumber.AutoSize = true;
            this.lblReferenceNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReferenceNumber.Location = new System.Drawing.Point(20, 163);
            this.lblReferenceNumber.Name = "lblReferenceNumber";
            this.lblReferenceNumber.Size = new System.Drawing.Size(130, 19);
            this.lblReferenceNumber.TabIndex = 6;
            this.lblReferenceNumber.Text = "Reference Number:";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(150, 120);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(400, 25);
            this.txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.Location = new System.Drawing.Point(20, 123);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(85, 19);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description:";
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTransactionDate.Location = new System.Drawing.Point(450, 80);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(100, 25);
            this.dtpTransactionDate.TabIndex = 3;
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.AutoSize = true;
            this.lblTransactionDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransactionDate.Location = new System.Drawing.Point(370, 83);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Size = new System.Drawing.Size(120, 19);
            this.lblTransactionDate.TabIndex = 2;
            this.lblTransactionDate.Text = "Transaction Date:";
            // 
            // cmbBankAccount
            // 
            this.cmbBankAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankAccount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbBankAccount.FormattingEnabled = true;
            this.cmbBankAccount.Location = new System.Drawing.Point(150, 80);
            this.cmbBankAccount.Name = "cmbBankAccount";
            this.cmbBankAccount.Size = new System.Drawing.Size(200, 25);
            this.cmbBankAccount.TabIndex = 1;
            // 
            // lblBankAccount
            // 
            this.lblBankAccount.AutoSize = true;
            this.lblBankAccount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBankAccount.Location = new System.Drawing.Point(20, 83);
            this.lblBankAccount.Name = "lblBankAccount";
            this.lblBankAccount.Size = new System.Drawing.Size(100, 19);
            this.lblBankAccount.TabIndex = 0;
            this.lblBankAccount.Text = "Bank Account:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 400);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(600, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(400, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(510, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BankStatementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(600, 460);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BankStatementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Statement";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.CheckBox chkIsReconciled;
        private System.Windows.Forms.NumericUpDown numBalance;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.ComboBox cmbTransactionType;
        private System.Windows.Forms.Label lblTransactionType;
        private System.Windows.Forms.TextBox txtReferenceNumber;
        private System.Windows.Forms.Label lblReferenceNumber;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.Label lblTransactionDate;
        private System.Windows.Forms.ComboBox cmbBankAccount;
        private System.Windows.Forms.Label lblBankAccount;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}
