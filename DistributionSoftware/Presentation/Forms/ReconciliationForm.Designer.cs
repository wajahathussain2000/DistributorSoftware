namespace DistributionSoftware.Presentation.Forms
{
    partial class ReconciliationForm
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
            this.chkIsCompleted = new System.Windows.Forms.CheckBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.numDifference = new System.Windows.Forms.NumericUpDown();
            this.lblDifference = new System.Windows.Forms.Label();
            this.numBookBalance = new System.Windows.Forms.NumericUpDown();
            this.lblBookBalance = new System.Windows.Forms.Label();
            this.numStatementBalance = new System.Windows.Forms.NumericUpDown();
            this.lblStatementBalance = new System.Windows.Forms.Label();
            this.dtpReconciliationDate = new System.Windows.Forms.DateTimePicker();
            this.lblReconciliationDate = new System.Windows.Forms.Label();
            this.cmbBankAccount = new System.Windows.Forms.ComboBox();
            this.lblBankAccount = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDifference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBookBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatementBalance)).BeginInit();
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
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚖️ Reconciliation";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.chkIsCompleted);
            this.pnlForm.Controls.Add(this.txtNotes);
            this.pnlForm.Controls.Add(this.lblNotes);
            this.pnlForm.Controls.Add(this.cmbStatus);
            this.pnlForm.Controls.Add(this.lblStatus);
            this.pnlForm.Controls.Add(this.numDifference);
            this.pnlForm.Controls.Add(this.lblDifference);
            this.pnlForm.Controls.Add(this.numBookBalance);
            this.pnlForm.Controls.Add(this.lblBookBalance);
            this.pnlForm.Controls.Add(this.numStatementBalance);
            this.pnlForm.Controls.Add(this.lblStatementBalance);
            this.pnlForm.Controls.Add(this.dtpReconciliationDate);
            this.pnlForm.Controls.Add(this.lblReconciliationDate);
            this.pnlForm.Controls.Add(this.cmbBankAccount);
            this.pnlForm.Controls.Add(this.lblBankAccount);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 60);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(20);
            this.pnlForm.Size = new System.Drawing.Size(600, 340);
            this.pnlForm.TabIndex = 1;
            // 
            // chkIsCompleted
            // 
            this.chkIsCompleted.AutoSize = true;
            this.chkIsCompleted.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkIsCompleted.Location = new System.Drawing.Point(150, 280);
            this.chkIsCompleted.Name = "chkIsCompleted";
            this.chkIsCompleted.Size = new System.Drawing.Size(90, 23);
            this.chkIsCompleted.TabIndex = 14;
            this.chkIsCompleted.Text = "Completed";
            this.chkIsCompleted.UseVisualStyleBackColor = true;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.Location = new System.Drawing.Point(150, 240);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(400, 25);
            this.txtNotes.TabIndex = 13;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNotes.Location = new System.Drawing.Point(20, 243);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(50, 19);
            this.lblNotes.TabIndex = 12;
            this.lblNotes.Text = "Notes:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "PENDING",
            "IN_PROGRESS",
            "COMPLETED",
            "DISCREPANCY"});
            this.cmbStatus.Location = new System.Drawing.Point(450, 200);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 25);
            this.cmbStatus.TabIndex = 11;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(370, 203);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 19);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status:";
            // 
            // numDifference
            // 
            this.numDifference.DecimalPlaces = 2;
            this.numDifference.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numDifference.Location = new System.Drawing.Point(450, 160);
            this.numDifference.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numDifference.Minimum = new decimal(new int[] {
            -999999999,
            0,
            0,
            -2147483648});
            this.numDifference.Name = "numDifference";
            this.numDifference.ReadOnly = true;
            this.numDifference.Size = new System.Drawing.Size(100, 25);
            this.numDifference.TabIndex = 9;
            // 
            // lblDifference
            // 
            this.lblDifference.AutoSize = true;
            this.lblDifference.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDifference.Location = new System.Drawing.Point(370, 163);
            this.lblDifference.Name = "lblDifference";
            this.lblDifference.Size = new System.Drawing.Size(75, 19);
            this.lblDifference.TabIndex = 8;
            this.lblDifference.Text = "Difference:";
            // 
            // numBookBalance
            // 
            this.numBookBalance.DecimalPlaces = 2;
            this.numBookBalance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numBookBalance.Location = new System.Drawing.Point(150, 160);
            this.numBookBalance.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numBookBalance.Name = "numBookBalance";
            this.numBookBalance.Size = new System.Drawing.Size(200, 25);
            this.numBookBalance.TabIndex = 7;
            this.numBookBalance.ValueChanged += new System.EventHandler(this.NumBookBalance_ValueChanged);
            // 
            // lblBookBalance
            // 
            this.lblBookBalance.AutoSize = true;
            this.lblBookBalance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBookBalance.Location = new System.Drawing.Point(20, 163);
            this.lblBookBalance.Name = "lblBookBalance";
            this.lblBookBalance.Size = new System.Drawing.Size(95, 19);
            this.lblBookBalance.TabIndex = 6;
            this.lblBookBalance.Text = "Book Balance:";
            // 
            // numStatementBalance
            // 
            this.numStatementBalance.DecimalPlaces = 2;
            this.numStatementBalance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numStatementBalance.Location = new System.Drawing.Point(450, 120);
            this.numStatementBalance.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numStatementBalance.Name = "numStatementBalance";
            this.numStatementBalance.Size = new System.Drawing.Size(100, 25);
            this.numStatementBalance.TabIndex = 5;
            this.numStatementBalance.ValueChanged += new System.EventHandler(this.NumStatementBalance_ValueChanged);
            // 
            // lblStatementBalance
            // 
            this.lblStatementBalance.AutoSize = true;
            this.lblStatementBalance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatementBalance.Location = new System.Drawing.Point(370, 123);
            this.lblStatementBalance.Name = "lblStatementBalance";
            this.lblStatementBalance.Size = new System.Drawing.Size(130, 19);
            this.lblStatementBalance.TabIndex = 4;
            this.lblStatementBalance.Text = "Statement Balance:";
            // 
            // dtpReconciliationDate
            // 
            this.dtpReconciliationDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpReconciliationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReconciliationDate.Location = new System.Drawing.Point(150, 120);
            this.dtpReconciliationDate.Name = "dtpReconciliationDate";
            this.dtpReconciliationDate.Size = new System.Drawing.Size(200, 25);
            this.dtpReconciliationDate.TabIndex = 3;
            // 
            // lblReconciliationDate
            // 
            this.lblReconciliationDate.AutoSize = true;
            this.lblReconciliationDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReconciliationDate.Location = new System.Drawing.Point(20, 123);
            this.lblReconciliationDate.Name = "lblReconciliationDate";
            this.lblReconciliationDate.Size = new System.Drawing.Size(140, 19);
            this.lblReconciliationDate.TabIndex = 2;
            this.lblReconciliationDate.Text = "Reconciliation Date:";
            // 
            // cmbBankAccount
            // 
            this.cmbBankAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankAccount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbBankAccount.FormattingEnabled = true;
            this.cmbBankAccount.Location = new System.Drawing.Point(150, 80);
            this.cmbBankAccount.Name = "cmbBankAccount";
            this.cmbBankAccount.Size = new System.Drawing.Size(400, 25);
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
            // ReconciliationForm
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
            this.Name = "ReconciliationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reconciliation";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDifference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBookBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStatementBalance)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.CheckBox chkIsCompleted;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.NumericUpDown numDifference;
        private System.Windows.Forms.Label lblDifference;
        private System.Windows.Forms.NumericUpDown numBookBalance;
        private System.Windows.Forms.Label lblBookBalance;
        private System.Windows.Forms.NumericUpDown numStatementBalance;
        private System.Windows.Forms.Label lblStatementBalance;
        private System.Windows.Forms.DateTimePicker dtpReconciliationDate;
        private System.Windows.Forms.Label lblReconciliationDate;
        private System.Windows.Forms.ComboBox cmbBankAccount;
        private System.Windows.Forms.Label lblBankAccount;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}
