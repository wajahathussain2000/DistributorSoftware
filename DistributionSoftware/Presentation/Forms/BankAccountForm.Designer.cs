namespace DistributionSoftware.Presentation.Forms
{
    partial class BankAccountForm
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
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.numCurrentBalance = new System.Windows.Forms.NumericUpDown();
            this.lblCurrentBalance = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.txtAccountType = new System.Windows.Forms.TextBox();
            this.lblAccountType = new System.Windows.Forms.Label();
            this.txtAccountHolder = new System.Windows.Forms.TextBox();
            this.lblAccountHolder = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.lblAccountNumber = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.lblBankName = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrentBalance)).BeginInit();
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
            this.lblTitle.Text = "🏦 Bank Account";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.chkIsActive);
            this.pnlForm.Controls.Add(this.numCurrentBalance);
            this.pnlForm.Controls.Add(this.lblCurrentBalance);
            this.pnlForm.Controls.Add(this.txtEmail);
            this.pnlForm.Controls.Add(this.lblEmail);
            this.pnlForm.Controls.Add(this.txtPhone);
            this.pnlForm.Controls.Add(this.lblPhone);
            this.pnlForm.Controls.Add(this.txtAddress);
            this.pnlForm.Controls.Add(this.lblAddress);
            this.pnlForm.Controls.Add(this.txtBranch);
            this.pnlForm.Controls.Add(this.lblBranch);
            this.pnlForm.Controls.Add(this.txtAccountType);
            this.pnlForm.Controls.Add(this.lblAccountType);
            this.pnlForm.Controls.Add(this.txtAccountHolder);
            this.pnlForm.Controls.Add(this.lblAccountHolder);
            this.pnlForm.Controls.Add(this.txtAccountNumber);
            this.pnlForm.Controls.Add(this.lblAccountNumber);
            this.pnlForm.Controls.Add(this.txtBankName);
            this.pnlForm.Controls.Add(this.lblBankName);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 60);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(20);
            this.pnlForm.Size = new System.Drawing.Size(600, 340);
            this.pnlForm.TabIndex = 1;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkIsActive.Location = new System.Drawing.Point(150, 280);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(70, 23);
            this.chkIsActive.TabIndex = 19;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // numCurrentBalance
            // 
            this.numCurrentBalance.DecimalPlaces = 2;
            this.numCurrentBalance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCurrentBalance.Location = new System.Drawing.Point(150, 240);
            this.numCurrentBalance.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numCurrentBalance.Name = "numCurrentBalance";
            this.numCurrentBalance.Size = new System.Drawing.Size(200, 25);
            this.numCurrentBalance.TabIndex = 18;
            // 
            // lblCurrentBalance
            // 
            this.lblCurrentBalance.AutoSize = true;
            this.lblCurrentBalance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentBalance.Location = new System.Drawing.Point(20, 243);
            this.lblCurrentBalance.Name = "lblCurrentBalance";
            this.lblCurrentBalance.Size = new System.Drawing.Size(110, 19);
            this.lblCurrentBalance.TabIndex = 17;
            this.lblCurrentBalance.Text = "Current Balance:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(150, 200);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 25);
            this.txtEmail.TabIndex = 16;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmail.Location = new System.Drawing.Point(20, 203);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(45, 19);
            this.lblEmail.TabIndex = 15;
            this.lblEmail.Text = "Email:";
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location = new System.Drawing.Point(150, 160);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 25);
            this.txtPhone.TabIndex = 14;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhone.Location = new System.Drawing.Point(20, 163);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(52, 19);
            this.lblPhone.TabIndex = 13;
            this.lblPhone.Text = "Phone:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.Location = new System.Drawing.Point(150, 120);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(400, 25);
            this.txtAddress.TabIndex = 12;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAddress.Location = new System.Drawing.Point(20, 123);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(62, 19);
            this.lblAddress.TabIndex = 11;
            this.lblAddress.Text = "Address:";
            // 
            // txtBranch
            // 
            this.txtBranch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBranch.Location = new System.Drawing.Point(150, 80);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(200, 25);
            this.txtBranch.TabIndex = 10;
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBranch.Location = new System.Drawing.Point(20, 83);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(55, 19);
            this.lblBranch.TabIndex = 9;
            this.lblBranch.Text = "Branch:";
            // 
            // txtAccountType
            // 
            this.txtAccountType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAccountType.Location = new System.Drawing.Point(450, 40);
            this.txtAccountType.Name = "txtAccountType";
            this.txtAccountType.Size = new System.Drawing.Size(100, 25);
            this.txtAccountType.TabIndex = 8;
            // 
            // lblAccountType
            // 
            this.lblAccountType.AutoSize = true;
            this.lblAccountType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAccountType.Location = new System.Drawing.Point(370, 43);
            this.lblAccountType.Name = "lblAccountType";
            this.lblAccountType.Size = new System.Drawing.Size(95, 19);
            this.lblAccountType.TabIndex = 7;
            this.lblAccountType.Text = "Account Type:";
            // 
            // txtAccountHolder
            // 
            this.txtAccountHolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAccountHolder.Location = new System.Drawing.Point(150, 40);
            this.txtAccountHolder.Name = "txtAccountHolder";
            this.txtAccountHolder.Size = new System.Drawing.Size(200, 25);
            this.txtAccountHolder.TabIndex = 6;
            // 
            // lblAccountHolder
            // 
            this.lblAccountHolder.AutoSize = true;
            this.lblAccountHolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAccountHolder.Location = new System.Drawing.Point(20, 43);
            this.lblAccountHolder.Name = "lblAccountHolder";
            this.lblAccountHolder.Size = new System.Drawing.Size(110, 19);
            this.lblAccountHolder.TabIndex = 5;
            this.lblAccountHolder.Text = "Account Holder:";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAccountNumber.Location = new System.Drawing.Point(450, 0);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(100, 25);
            this.txtAccountNumber.TabIndex = 4;
            // 
            // lblAccountNumber
            // 
            this.lblAccountNumber.AutoSize = true;
            this.lblAccountNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAccountNumber.Location = new System.Drawing.Point(370, 3);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.Size = new System.Drawing.Size(120, 19);
            this.lblAccountNumber.TabIndex = 3;
            this.lblAccountNumber.Text = "Account Number:";
            // 
            // txtBankName
            // 
            this.txtBankName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBankName.Location = new System.Drawing.Point(150, 0);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(200, 25);
            this.txtBankName.TabIndex = 2;
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBankName.Location = new System.Drawing.Point(20, 3);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(85, 19);
            this.lblBankName.TabIndex = 1;
            this.lblBankName.Text = "Bank Name:";
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
            // BankAccountForm
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
            this.Name = "BankAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Account";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrentBalance)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.NumericUpDown numCurrentBalance;
        private System.Windows.Forms.Label lblCurrentBalance;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.TextBox txtAccountType;
        private System.Windows.Forms.Label lblAccountType;
        private System.Windows.Forms.TextBox txtAccountHolder;
        private System.Windows.Forms.Label lblAccountHolder;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label lblAccountNumber;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}
