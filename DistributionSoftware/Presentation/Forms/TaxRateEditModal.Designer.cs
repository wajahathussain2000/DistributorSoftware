namespace DistributionSoftware.Presentation.Forms
{
    partial class TaxRateEditModal
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkIsInclusive = new System.Windows.Forms.CheckBox();
            this.chkIsCompound = new System.Windows.Forms.CheckBox();
            this.chkIsSystem = new System.Windows.Forms.CheckBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.dtpEffectiveTo = new System.Windows.Forms.DateTimePicker();
            this.lblEffectiveTo = new System.Windows.Forms.Label();
            this.dtpEffectiveFrom = new System.Windows.Forms.DateTimePicker();
            this.lblEffectiveFrom = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtPercentage = new System.Windows.Forms.TextBox();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(600, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "✏️ Edit Tax Rate";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.btnCancel);
            this.pnlForm.Controls.Add(this.btnSave);
            this.pnlForm.Controls.Add(this.chkIsInclusive);
            this.pnlForm.Controls.Add(this.chkIsCompound);
            this.pnlForm.Controls.Add(this.chkIsSystem);
            this.pnlForm.Controls.Add(this.chkIsActive);
            this.pnlForm.Controls.Add(this.dtpEffectiveTo);
            this.pnlForm.Controls.Add(this.lblEffectiveTo);
            this.pnlForm.Controls.Add(this.dtpEffectiveFrom);
            this.pnlForm.Controls.Add(this.lblEffectiveFrom);
            this.pnlForm.Controls.Add(this.txtDescription);
            this.pnlForm.Controls.Add(this.lblDescription);
            this.pnlForm.Controls.Add(this.txtPercentage);
            this.pnlForm.Controls.Add(this.lblPercentage);
            this.pnlForm.Controls.Add(this.txtCode);
            this.pnlForm.Controls.Add(this.lblCode);
            this.pnlForm.Controls.Add(this.txtName);
            this.pnlForm.Controls.Add(this.lblName);
            this.pnlForm.Controls.Add(this.cmbCategory);
            this.pnlForm.Controls.Add(this.lblCategory);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 50);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(20);
            this.pnlForm.Size = new System.Drawing.Size(600, 400);
            this.pnlForm.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(300, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(190, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // chkIsInclusive
            // 
            this.chkIsInclusive.AutoSize = true;
            this.chkIsInclusive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkIsInclusive.Location = new System.Drawing.Point(330, 300);
            this.chkIsInclusive.Name = "chkIsInclusive";
            this.chkIsInclusive.Size = new System.Drawing.Size(90, 24);
            this.chkIsInclusive.TabIndex = 17;
            this.chkIsInclusive.Text = "Inclusive";
            this.chkIsInclusive.UseVisualStyleBackColor = true;
            // 
            // chkIsCompound
            // 
            this.chkIsCompound.AutoSize = true;
            this.chkIsCompound.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkIsCompound.Location = new System.Drawing.Point(210, 300);
            this.chkIsCompound.Name = "chkIsCompound";
            this.chkIsCompound.Size = new System.Drawing.Size(100, 24);
            this.chkIsCompound.TabIndex = 16;
            this.chkIsCompound.Text = "Compound";
            this.chkIsCompound.UseVisualStyleBackColor = true;
            // 
            // chkIsSystem
            // 
            this.chkIsSystem.AutoSize = true;
            this.chkIsSystem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkIsSystem.Location = new System.Drawing.Point(110, 300);
            this.chkIsSystem.Name = "chkIsSystem";
            this.chkIsSystem.Size = new System.Drawing.Size(80, 24);
            this.chkIsSystem.TabIndex = 15;
            this.chkIsSystem.Text = "System";
            this.chkIsSystem.UseVisualStyleBackColor = true;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkIsActive.Location = new System.Drawing.Point(20, 300);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(75, 24);
            this.chkIsActive.TabIndex = 14;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // dtpEffectiveTo
            // 
            this.dtpEffectiveTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpEffectiveTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffectiveTo.Location = new System.Drawing.Point(320, 250);
            this.dtpEffectiveTo.Name = "dtpEffectiveTo";
            this.dtpEffectiveTo.Size = new System.Drawing.Size(120, 27);
            this.dtpEffectiveTo.TabIndex = 13;
            // 
            // lblEffectiveTo
            // 
            this.lblEffectiveTo.AutoSize = true;
            this.lblEffectiveTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEffectiveTo.Location = new System.Drawing.Point(250, 253);
            this.lblEffectiveTo.Name = "lblEffectiveTo";
            this.lblEffectiveTo.Size = new System.Drawing.Size(60, 20);
            this.lblEffectiveTo.TabIndex = 12;
            this.lblEffectiveTo.Text = "To Date:";
            // 
            // dtpEffectiveFrom
            // 
            this.dtpEffectiveFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpEffectiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffectiveFrom.Location = new System.Drawing.Point(110, 250);
            this.dtpEffectiveFrom.Name = "dtpEffectiveFrom";
            this.dtpEffectiveFrom.Size = new System.Drawing.Size(120, 27);
            this.dtpEffectiveFrom.TabIndex = 11;
            // 
            // lblEffectiveFrom
            // 
            this.lblEffectiveFrom.AutoSize = true;
            this.lblEffectiveFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEffectiveFrom.Location = new System.Drawing.Point(20, 253);
            this.lblEffectiveFrom.Name = "lblEffectiveFrom";
            this.lblEffectiveFrom.Size = new System.Drawing.Size(80, 20);
            this.lblEffectiveFrom.TabIndex = 10;
            this.lblEffectiveFrom.Text = "From Date:";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescription.Location = new System.Drawing.Point(320, 200);
            this.txtDescription.MaxLength = 500;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(250, 40);
            this.txtDescription.TabIndex = 9;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescription.Location = new System.Drawing.Point(230, 203);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(85, 20);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "Description:";
            // 
            // txtPercentage
            // 
            this.txtPercentage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPercentage.Location = new System.Drawing.Point(110, 200);
            this.txtPercentage.MaxLength = 10;
            this.txtPercentage.Name = "txtPercentage";
            this.txtPercentage.Size = new System.Drawing.Size(100, 27);
            this.txtPercentage.TabIndex = 7;
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPercentage.Location = new System.Drawing.Point(20, 203);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(85, 20);
            this.lblPercentage.TabIndex = 6;
            this.lblPercentage.Text = "Percentage:";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCode.Location = new System.Drawing.Point(650, 150);
            this.txtCode.MaxLength = 20;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(120, 27);
            this.txtCode.TabIndex = 5;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCode.Location = new System.Drawing.Point(600, 153);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(45, 20);
            this.lblCode.TabIndex = 4;
            this.lblCode.Text = "Code:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtName.Location = new System.Drawing.Point(380, 150);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 27);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblName.Location = new System.Drawing.Point(320, 153);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(50, 20);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(100, 150);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 28);
            this.cmbCategory.TabIndex = 1;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCategory.Location = new System.Drawing.Point(20, 153);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(70, 20);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Category:";
            // 
            // TaxRateEditModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaxRateEditModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "✏️ Edit Tax Rate";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.TextBox txtPercentage;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblEffectiveFrom;
        private System.Windows.Forms.DateTimePicker dtpEffectiveFrom;
        private System.Windows.Forms.Label lblEffectiveTo;
        private System.Windows.Forms.DateTimePicker dtpEffectiveTo;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.CheckBox chkIsSystem;
        private System.Windows.Forms.CheckBox chkIsCompound;
        private System.Windows.Forms.CheckBox chkIsInclusive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
