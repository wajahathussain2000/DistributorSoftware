using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class TaxConfigurationForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCategories = new System.Windows.Forms.TabPage();
            this.pnlCategories = new System.Windows.Forms.Panel();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.pnlCategoryForm = new System.Windows.Forms.Panel();
            this.btnCategoryDelete = new System.Windows.Forms.Button();
            this.btnCategoryEdit = new System.Windows.Forms.Button();
            this.btnCategoryNew = new System.Windows.Forms.Button();
            this.btnCategorySave = new System.Windows.Forms.Button();
            this.btnCategoryCancel = new System.Windows.Forms.Button();
            this.chkCategoryIsSystem = new System.Windows.Forms.CheckBox();
            this.chkCategoryIsActive = new System.Windows.Forms.CheckBox();
            this.txtCategoryDescription = new System.Windows.Forms.TextBox();
            this.lblCategoryDescription = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.txtCategoryCode = new System.Windows.Forms.TextBox();
            this.lblCategoryCode = new System.Windows.Forms.Label();
            this.tabRates = new System.Windows.Forms.TabPage();
            this.pnlRates = new System.Windows.Forms.Panel();
            this.dgvRates = new System.Windows.Forms.DataGridView();
            this.pnlRateForm = new System.Windows.Forms.Panel();
            this.btnRateDelete = new System.Windows.Forms.Button();
            this.btnRateEdit = new System.Windows.Forms.Button();
            this.btnRateNew = new System.Windows.Forms.Button();
            this.btnRateSave = new System.Windows.Forms.Button();
            this.btnRateCancel = new System.Windows.Forms.Button();
            this.chkRateIsInclusive = new System.Windows.Forms.CheckBox();
            this.chkRateIsCompound = new System.Windows.Forms.CheckBox();
            this.chkRateIsSystem = new System.Windows.Forms.CheckBox();
            this.chkRateIsActive = new System.Windows.Forms.CheckBox();
            this.dtpRateEffectiveTo = new System.Windows.Forms.DateTimePicker();
            this.lblRateEffectiveTo = new System.Windows.Forms.Label();
            this.dtpRateEffectiveFrom = new System.Windows.Forms.DateTimePicker();
            this.lblRateEffectiveFrom = new System.Windows.Forms.Label();
            this.txtRateDescription = new System.Windows.Forms.TextBox();
            this.lblRateDescription = new System.Windows.Forms.Label();
            this.txtRatePercentage = new System.Windows.Forms.TextBox();
            this.lblRatePercentage = new System.Windows.Forms.Label();
            this.txtRateCode = new System.Windows.Forms.TextBox();
            this.lblRateCode = new System.Windows.Forms.Label();
            this.txtRateName = new System.Windows.Forms.TextBox();
            this.lblRateName = new System.Windows.Forms.Label();
            this.cmbRateCategory = new System.Windows.Forms.ComboBox();
            this.lblRateCategory = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCategories.SuspendLayout();
            this.pnlCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.pnlCategoryForm.SuspendLayout();
            this.tabRates.SuspendLayout();
            this.pnlRates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRates)).BeginInit();
            this.pnlRateForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1400, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 Tax Configuration Management";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCategories);
            this.tabControl.Controls.Add(this.tabRates);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.ItemSize = new System.Drawing.Size(200, 35);
            this.tabControl.Location = new System.Drawing.Point(0, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1400, 840);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 1;
            // 
            // tabCategories
            // 
            this.tabCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabCategories.Controls.Add(this.pnlCategories);
            this.tabCategories.Controls.Add(this.pnlCategoryForm);
            this.tabCategories.Location = new System.Drawing.Point(4, 39);
            this.tabCategories.Name = "tabCategories";
            this.tabCategories.Padding = new System.Windows.Forms.Padding(3);
            this.tabCategories.Size = new System.Drawing.Size(1392, 797);
            this.tabCategories.TabIndex = 0;
            this.tabCategories.Text = "Tax Categories";
            // 
            // pnlCategories
            // 
            this.pnlCategories.BackColor = System.Drawing.Color.White;
            this.pnlCategories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCategories.Controls.Add(this.dgvCategories);
            this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategories.Location = new System.Drawing.Point(3, 3);
            this.pnlCategories.Name = "pnlCategories";
            this.pnlCategories.Size = new System.Drawing.Size(1386, 500);
            this.pnlCategories.TabIndex = 0;
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategories.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCategories.ColumnHeadersHeight = 35;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategories.EnableHeadersVisualStyles = false;
            this.dgvCategories.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.dgvCategories.Location = new System.Drawing.Point(0, 0);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCategories.RowHeadersWidth = 4;
            this.dgvCategories.RowTemplate.Height = 30;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(1384, 498);
            this.dgvCategories.TabIndex = 0;
            this.dgvCategories.SelectionChanged += new System.EventHandler(this.DgvCategories_SelectionChanged);
            // 
            // pnlCategoryForm
            // 
            this.pnlCategoryForm.BackColor = System.Drawing.Color.White;
            this.pnlCategoryForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCategoryForm.Controls.Add(this.btnCategoryDelete);
            this.pnlCategoryForm.Controls.Add(this.btnCategoryEdit);
            this.pnlCategoryForm.Controls.Add(this.btnCategoryNew);
            this.pnlCategoryForm.Controls.Add(this.btnCategorySave);
            this.pnlCategoryForm.Controls.Add(this.btnCategoryCancel);
            this.pnlCategoryForm.Controls.Add(this.chkCategoryIsSystem);
            this.pnlCategoryForm.Controls.Add(this.chkCategoryIsActive);
            this.pnlCategoryForm.Controls.Add(this.txtCategoryDescription);
            this.pnlCategoryForm.Controls.Add(this.lblCategoryDescription);
            this.pnlCategoryForm.Controls.Add(this.txtCategoryName);
            this.pnlCategoryForm.Controls.Add(this.lblCategoryName);
            this.pnlCategoryForm.Controls.Add(this.txtCategoryCode);
            this.pnlCategoryForm.Controls.Add(this.lblCategoryCode);
            this.pnlCategoryForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCategoryForm.Location = new System.Drawing.Point(3, 503);
            this.pnlCategoryForm.Name = "pnlCategoryForm";
            this.pnlCategoryForm.Size = new System.Drawing.Size(1386, 291);
            this.pnlCategoryForm.TabIndex = 1;
            // 
            // lblCategoryCode
            // 
            this.lblCategoryCode.AutoSize = true;
            this.lblCategoryCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCategoryCode.Location = new System.Drawing.Point(20, 20);
            this.lblCategoryCode.Name = "lblCategoryCode";
            this.lblCategoryCode.Size = new System.Drawing.Size(95, 20);
            this.lblCategoryCode.TabIndex = 0;
            this.lblCategoryCode.Text = "Category Code:";
            // 
            // txtCategoryCode
            // 
            this.txtCategoryCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCategoryCode.Location = new System.Drawing.Point(130, 17);
            this.txtCategoryCode.MaxLength = 10;
            this.txtCategoryCode.Name = "txtCategoryCode";
            this.txtCategoryCode.Size = new System.Drawing.Size(150, 27);
            this.txtCategoryCode.TabIndex = 1;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCategoryName.Location = new System.Drawing.Point(300, 20);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(100, 20);
            this.lblCategoryName.TabIndex = 2;
            this.lblCategoryName.Text = "Category Name:";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCategoryName.Location = new System.Drawing.Point(410, 17);
            this.txtCategoryName.MaxLength = 100;
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(250, 27);
            this.txtCategoryName.TabIndex = 3;
            // 
            // lblCategoryDescription
            // 
            this.lblCategoryDescription.AutoSize = true;
            this.lblCategoryDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCategoryDescription.Location = new System.Drawing.Point(20, 60);
            this.lblCategoryDescription.Name = "lblCategoryDescription";
            this.lblCategoryDescription.Size = new System.Drawing.Size(85, 20);
            this.lblCategoryDescription.TabIndex = 4;
            this.lblCategoryDescription.Text = "Description:";
            // 
            // txtCategoryDescription
            // 
            this.txtCategoryDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCategoryDescription.Location = new System.Drawing.Point(130, 57);
            this.txtCategoryDescription.MaxLength = 500;
            this.txtCategoryDescription.Multiline = true;
            this.txtCategoryDescription.Name = "txtCategoryDescription";
            this.txtCategoryDescription.Size = new System.Drawing.Size(530, 60);
            this.txtCategoryDescription.TabIndex = 5;
            // 
            // chkCategoryIsActive
            // 
            this.chkCategoryIsActive.AutoSize = true;
            this.chkCategoryIsActive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkCategoryIsActive.Location = new System.Drawing.Point(20, 140);
            this.chkCategoryIsActive.Name = "chkCategoryIsActive";
            this.chkCategoryIsActive.Size = new System.Drawing.Size(75, 24);
            this.chkCategoryIsActive.TabIndex = 6;
            this.chkCategoryIsActive.Text = "Active";
            this.chkCategoryIsActive.UseVisualStyleBackColor = true;
            // 
            // chkCategoryIsSystem
            // 
            this.chkCategoryIsSystem.AutoSize = true;
            this.chkCategoryIsSystem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkCategoryIsSystem.Location = new System.Drawing.Point(130, 140);
            this.chkCategoryIsSystem.Name = "chkCategoryIsSystem";
            this.chkCategoryIsSystem.Size = new System.Drawing.Size(80, 24);
            this.chkCategoryIsSystem.TabIndex = 7;
            this.chkCategoryIsSystem.Text = "System";
            this.chkCategoryIsSystem.UseVisualStyleBackColor = true;
            // 
            // btnCategoryCancel
            // 
            this.btnCategoryCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCategoryCancel.FlatAppearance.BorderSize = 0;
            this.btnCategoryCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCategoryCancel.ForeColor = System.Drawing.Color.White;
            this.btnCategoryCancel.Location = new System.Drawing.Point(300, 200);
            this.btnCategoryCancel.Name = "btnCategoryCancel";
            this.btnCategoryCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCategoryCancel.TabIndex = 8;
            this.btnCategoryCancel.Text = "❌ Cancel";
            this.btnCategoryCancel.UseVisualStyleBackColor = false;
            this.btnCategoryCancel.Click += new System.EventHandler(this.BtnCategoryCancel_Click);
            // 
            // btnCategorySave
            // 
            this.btnCategorySave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCategorySave.FlatAppearance.BorderSize = 0;
            this.btnCategorySave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategorySave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCategorySave.ForeColor = System.Drawing.Color.White;
            this.btnCategorySave.Location = new System.Drawing.Point(190, 200);
            this.btnCategorySave.Name = "btnCategorySave";
            this.btnCategorySave.Size = new System.Drawing.Size(100, 35);
            this.btnCategorySave.TabIndex = 9;
            this.btnCategorySave.Text = "💾 Save";
            this.btnCategorySave.UseVisualStyleBackColor = false;
            this.btnCategorySave.Click += new System.EventHandler(this.BtnCategorySave_Click);
            // 
            // btnCategoryNew
            // 
            this.btnCategoryNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCategoryNew.FlatAppearance.BorderSize = 0;
            this.btnCategoryNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCategoryNew.ForeColor = System.Drawing.Color.White;
            this.btnCategoryNew.Location = new System.Drawing.Point(20, 200);
            this.btnCategoryNew.Name = "btnCategoryNew";
            this.btnCategoryNew.Size = new System.Drawing.Size(80, 35);
            this.btnCategoryNew.TabIndex = 10;
            this.btnCategoryNew.Text = "➕ New";
            this.btnCategoryNew.UseVisualStyleBackColor = false;
            this.btnCategoryNew.Click += new System.EventHandler(this.BtnCategoryNew_Click);
            // 
            // btnCategoryEdit
            // 
            this.btnCategoryEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCategoryEdit.FlatAppearance.BorderSize = 0;
            this.btnCategoryEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCategoryEdit.ForeColor = System.Drawing.Color.White;
            this.btnCategoryEdit.Location = new System.Drawing.Point(110, 200);
            this.btnCategoryEdit.Name = "btnCategoryEdit";
            this.btnCategoryEdit.Size = new System.Drawing.Size(70, 35);
            this.btnCategoryEdit.TabIndex = 11;
            this.btnCategoryEdit.Text = "✏️ Edit";
            this.btnCategoryEdit.UseVisualStyleBackColor = false;
            this.btnCategoryEdit.Click += new System.EventHandler(this.BtnCategoryEdit_Click);
            // 
            // btnCategoryDelete
            // 
            this.btnCategoryDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCategoryDelete.FlatAppearance.BorderSize = 0;
            this.btnCategoryDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCategoryDelete.ForeColor = System.Drawing.Color.White;
            this.btnCategoryDelete.Location = new System.Drawing.Point(420, 200);
            this.btnCategoryDelete.Name = "btnCategoryDelete";
            this.btnCategoryDelete.Size = new System.Drawing.Size(80, 35);
            this.btnCategoryDelete.TabIndex = 12;
            this.btnCategoryDelete.Text = "🗑️ Delete";
            this.btnCategoryDelete.UseVisualStyleBackColor = false;
            this.btnCategoryDelete.Click += new System.EventHandler(this.BtnCategoryDelete_Click);
            // 
            // tabRates
            // 
            this.tabRates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabRates.Controls.Add(this.pnlRates);
            this.tabRates.Controls.Add(this.pnlRateForm);
            this.tabRates.Location = new System.Drawing.Point(4, 39);
            this.tabRates.Name = "tabRates";
            this.tabRates.Padding = new System.Windows.Forms.Padding(3);
            this.tabRates.Size = new System.Drawing.Size(1392, 797);
            this.tabRates.TabIndex = 1;
            this.tabRates.Text = "Tax Rates";
            // 
            // pnlRates
            // 
            this.pnlRates.BackColor = System.Drawing.Color.White;
            this.pnlRates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRates.Controls.Add(this.dgvRates);
            this.pnlRates.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRates.Location = new System.Drawing.Point(3, 3);
            this.pnlRates.Name = "pnlRates";
            this.pnlRates.Size = new System.Drawing.Size(1386, 500);
            this.pnlRates.TabIndex = 0;
            // 
            // dgvRates
            // 
            this.dgvRates.AllowUserToAddRows = false;
            this.dgvRates.AllowUserToDeleteRows = false;
            this.dgvRates.BackgroundColor = System.Drawing.Color.White;
            this.dgvRates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRates.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvRates.ColumnHeadersHeight = 35;
            this.dgvRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRates.EnableHeadersVisualStyles = false;
            this.dgvRates.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.dgvRates.Location = new System.Drawing.Point(0, 0);
            this.dgvRates.MultiSelect = false;
            this.dgvRates.Name = "dgvRates";
            this.dgvRates.ReadOnly = true;
            this.dgvRates.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvRates.RowHeadersWidth = 4;
            this.dgvRates.RowTemplate.Height = 30;
            this.dgvRates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRates.Size = new System.Drawing.Size(1384, 498);
            this.dgvRates.TabIndex = 0;
            this.dgvRates.SelectionChanged += new System.EventHandler(this.DgvRates_SelectionChanged);
            // 
            // pnlRateForm
            // 
            this.pnlRateForm.BackColor = System.Drawing.Color.White;
            this.pnlRateForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRateForm.Controls.Add(this.btnRateDelete);
            this.pnlRateForm.Controls.Add(this.btnRateEdit);
            this.pnlRateForm.Controls.Add(this.btnRateNew);
            this.pnlRateForm.Controls.Add(this.btnRateSave);
            this.pnlRateForm.Controls.Add(this.btnRateCancel);
            this.pnlRateForm.Controls.Add(this.chkRateIsInclusive);
            this.pnlRateForm.Controls.Add(this.chkRateIsCompound);
            this.pnlRateForm.Controls.Add(this.chkRateIsSystem);
            this.pnlRateForm.Controls.Add(this.chkRateIsActive);
            this.pnlRateForm.Controls.Add(this.dtpRateEffectiveTo);
            this.pnlRateForm.Controls.Add(this.lblRateEffectiveTo);
            this.pnlRateForm.Controls.Add(this.dtpRateEffectiveFrom);
            this.pnlRateForm.Controls.Add(this.lblRateEffectiveFrom);
            this.pnlRateForm.Controls.Add(this.txtRateDescription);
            this.pnlRateForm.Controls.Add(this.lblRateDescription);
            this.pnlRateForm.Controls.Add(this.txtRatePercentage);
            this.pnlRateForm.Controls.Add(this.lblRatePercentage);
            this.pnlRateForm.Controls.Add(this.txtRateCode);
            this.pnlRateForm.Controls.Add(this.lblRateCode);
            this.pnlRateForm.Controls.Add(this.txtRateName);
            this.pnlRateForm.Controls.Add(this.lblRateName);
            this.pnlRateForm.Controls.Add(this.cmbRateCategory);
            this.pnlRateForm.Controls.Add(this.lblRateCategory);
            this.pnlRateForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRateForm.Location = new System.Drawing.Point(3, 503);
            this.pnlRateForm.Name = "pnlRateForm";
            this.pnlRateForm.Size = new System.Drawing.Size(1386, 291);
            this.pnlRateForm.TabIndex = 1;
            // 
            // lblRateCategory
            // 
            this.lblRateCategory.AutoSize = true;
            this.lblRateCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRateCategory.Location = new System.Drawing.Point(20, 20);
            this.lblRateCategory.Name = "lblRateCategory";
            this.lblRateCategory.Size = new System.Drawing.Size(70, 20);
            this.lblRateCategory.TabIndex = 0;
            this.lblRateCategory.Text = "Category:";
            // 
            // cmbRateCategory
            // 
            this.cmbRateCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRateCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbRateCategory.FormattingEnabled = true;
            this.cmbRateCategory.Location = new System.Drawing.Point(100, 17);
            this.cmbRateCategory.Name = "cmbRateCategory";
            this.cmbRateCategory.Size = new System.Drawing.Size(200, 28);
            this.cmbRateCategory.TabIndex = 1;
            // 
            // lblRateName
            // 
            this.lblRateName.AutoSize = true;
            this.lblRateName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRateName.Location = new System.Drawing.Point(320, 20);
            this.lblRateName.Name = "lblRateName";
            this.lblRateName.Size = new System.Drawing.Size(50, 20);
            this.lblRateName.TabIndex = 2;
            this.lblRateName.Text = "Name:";
            // 
            // txtRateName
            // 
            this.txtRateName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRateName.Location = new System.Drawing.Point(380, 17);
            this.txtRateName.MaxLength = 100;
            this.txtRateName.Name = "txtRateName";
            this.txtRateName.Size = new System.Drawing.Size(200, 27);
            this.txtRateName.TabIndex = 3;
            // 
            // lblRateCode
            // 
            this.lblRateCode.AutoSize = true;
            this.lblRateCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRateCode.Location = new System.Drawing.Point(600, 20);
            this.lblRateCode.Name = "lblRateCode";
            this.lblRateCode.Size = new System.Drawing.Size(45, 20);
            this.lblRateCode.TabIndex = 4;
            this.lblRateCode.Text = "Code:";
            // 
            // txtRateCode
            // 
            this.txtRateCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRateCode.Location = new System.Drawing.Point(650, 17);
            this.txtRateCode.MaxLength = 20;
            this.txtRateCode.Name = "txtRateCode";
            this.txtRateCode.Size = new System.Drawing.Size(120, 27);
            this.txtRateCode.TabIndex = 5;
            // 
            // lblRatePercentage
            // 
            this.lblRatePercentage.AutoSize = true;
            this.lblRatePercentage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRatePercentage.Location = new System.Drawing.Point(20, 60);
            this.lblRatePercentage.Name = "lblRatePercentage";
            this.lblRatePercentage.Size = new System.Drawing.Size(85, 20);
            this.lblRatePercentage.TabIndex = 6;
            this.lblRatePercentage.Text = "Percentage:";
            // 
            // txtRatePercentage
            // 
            this.txtRatePercentage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRatePercentage.Location = new System.Drawing.Point(110, 57);
            this.txtRatePercentage.MaxLength = 10;
            this.txtRatePercentage.Name = "txtRatePercentage";
            this.txtRatePercentage.Size = new System.Drawing.Size(100, 27);
            this.txtRatePercentage.TabIndex = 7;
            // 
            // lblRateDescription
            // 
            this.lblRateDescription.AutoSize = true;
            this.lblRateDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRateDescription.Location = new System.Drawing.Point(230, 60);
            this.lblRateDescription.Name = "lblRateDescription";
            this.lblRateDescription.Size = new System.Drawing.Size(85, 20);
            this.lblRateDescription.TabIndex = 8;
            this.lblRateDescription.Text = "Description:";
            // 
            // txtRateDescription
            // 
            this.txtRateDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRateDescription.Location = new System.Drawing.Point(320, 57);
            this.txtRateDescription.MaxLength = 500;
            this.txtRateDescription.Multiline = true;
            this.txtRateDescription.Name = "txtRateDescription";
            this.txtRateDescription.Size = new System.Drawing.Size(450, 60);
            this.txtRateDescription.TabIndex = 9;
            // 
            // lblRateEffectiveFrom
            // 
            this.lblRateEffectiveFrom.AutoSize = true;
            this.lblRateEffectiveFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRateEffectiveFrom.Location = new System.Drawing.Point(20, 130);
            this.lblRateEffectiveFrom.Name = "lblRateEffectiveFrom";
            this.lblRateEffectiveFrom.Size = new System.Drawing.Size(80, 20);
            this.lblRateEffectiveFrom.TabIndex = 10;
            this.lblRateEffectiveFrom.Text = "From Date:";
            // 
            // dtpRateEffectiveFrom
            // 
            this.dtpRateEffectiveFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpRateEffectiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRateEffectiveFrom.Location = new System.Drawing.Point(110, 127);
            this.dtpRateEffectiveFrom.Name = "dtpRateEffectiveFrom";
            this.dtpRateEffectiveFrom.Size = new System.Drawing.Size(120, 27);
            this.dtpRateEffectiveFrom.TabIndex = 11;
            // 
            // lblRateEffectiveTo
            // 
            this.lblRateEffectiveTo.AutoSize = true;
            this.lblRateEffectiveTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRateEffectiveTo.Location = new System.Drawing.Point(250, 130);
            this.lblRateEffectiveTo.Name = "lblRateEffectiveTo";
            this.lblRateEffectiveTo.Size = new System.Drawing.Size(60, 20);
            this.lblRateEffectiveTo.TabIndex = 12;
            this.lblRateEffectiveTo.Text = "To Date:";
            // 
            // dtpRateEffectiveTo
            // 
            this.dtpRateEffectiveTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpRateEffectiveTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRateEffectiveTo.Location = new System.Drawing.Point(320, 127);
            this.dtpRateEffectiveTo.Name = "dtpRateEffectiveTo";
            this.dtpRateEffectiveTo.Size = new System.Drawing.Size(120, 27);
            this.dtpRateEffectiveTo.TabIndex = 13;
            // 
            // chkRateIsActive
            // 
            this.chkRateIsActive.AutoSize = true;
            this.chkRateIsActive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkRateIsActive.Location = new System.Drawing.Point(20, 170);
            this.chkRateIsActive.Name = "chkRateIsActive";
            this.chkRateIsActive.Size = new System.Drawing.Size(75, 24);
            this.chkRateIsActive.TabIndex = 14;
            this.chkRateIsActive.Text = "Active";
            this.chkRateIsActive.UseVisualStyleBackColor = true;
            // 
            // chkRateIsSystem
            // 
            this.chkRateIsSystem.AutoSize = true;
            this.chkRateIsSystem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkRateIsSystem.Location = new System.Drawing.Point(110, 170);
            this.chkRateIsSystem.Name = "chkRateIsSystem";
            this.chkRateIsSystem.Size = new System.Drawing.Size(80, 24);
            this.chkRateIsSystem.TabIndex = 15;
            this.chkRateIsSystem.Text = "System";
            this.chkRateIsSystem.UseVisualStyleBackColor = true;
            // 
            // chkRateIsCompound
            // 
            this.chkRateIsCompound.AutoSize = true;
            this.chkRateIsCompound.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkRateIsCompound.Location = new System.Drawing.Point(210, 170);
            this.chkRateIsCompound.Name = "chkRateIsCompound";
            this.chkRateIsCompound.Size = new System.Drawing.Size(100, 24);
            this.chkRateIsCompound.TabIndex = 16;
            this.chkRateIsCompound.Text = "Compound";
            this.chkRateIsCompound.UseVisualStyleBackColor = true;
            // 
            // chkRateIsInclusive
            // 
            this.chkRateIsInclusive.AutoSize = true;
            this.chkRateIsInclusive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkRateIsInclusive.Location = new System.Drawing.Point(330, 170);
            this.chkRateIsInclusive.Name = "chkRateIsInclusive";
            this.chkRateIsInclusive.Size = new System.Drawing.Size(90, 24);
            this.chkRateIsInclusive.TabIndex = 17;
            this.chkRateIsInclusive.Text = "Inclusive";
            this.chkRateIsInclusive.UseVisualStyleBackColor = true;
            // 
            // btnRateCancel
            // 
            this.btnRateCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnRateCancel.FlatAppearance.BorderSize = 0;
            this.btnRateCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRateCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRateCancel.ForeColor = System.Drawing.Color.White;
            this.btnRateCancel.Location = new System.Drawing.Point(300, 220);
            this.btnRateCancel.Name = "btnRateCancel";
            this.btnRateCancel.Size = new System.Drawing.Size(100, 35);
            this.btnRateCancel.TabIndex = 18;
            this.btnRateCancel.Text = "❌ Cancel";
            this.btnRateCancel.UseVisualStyleBackColor = false;
            this.btnRateCancel.Click += new System.EventHandler(this.BtnRateCancel_Click);
            // 
            // btnRateSave
            // 
            this.btnRateSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnRateSave.FlatAppearance.BorderSize = 0;
            this.btnRateSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRateSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRateSave.ForeColor = System.Drawing.Color.White;
            this.btnRateSave.Location = new System.Drawing.Point(190, 220);
            this.btnRateSave.Name = "btnRateSave";
            this.btnRateSave.Size = new System.Drawing.Size(100, 35);
            this.btnRateSave.TabIndex = 19;
            this.btnRateSave.Text = "💾 Save";
            this.btnRateSave.UseVisualStyleBackColor = false;
            this.btnRateSave.Click += new System.EventHandler(this.BtnRateSave_Click);
            // 
            // btnRateNew
            // 
            this.btnRateNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnRateNew.FlatAppearance.BorderSize = 0;
            this.btnRateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRateNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRateNew.ForeColor = System.Drawing.Color.White;
            this.btnRateNew.Location = new System.Drawing.Point(20, 220);
            this.btnRateNew.Name = "btnRateNew";
            this.btnRateNew.Size = new System.Drawing.Size(100, 35);
            this.btnRateNew.TabIndex = 20;
            this.btnRateNew.Text = "➕ New";
            this.btnRateNew.UseVisualStyleBackColor = false;
            this.btnRateNew.Click += new System.EventHandler(this.BtnRateNew_Click);
            // 
            // btnRateEdit
            // 
            this.btnRateEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnRateEdit.FlatAppearance.BorderSize = 0;
            this.btnRateEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRateEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRateEdit.ForeColor = System.Drawing.Color.White;
            this.btnRateEdit.Location = new System.Drawing.Point(130, 220);
            this.btnRateEdit.Name = "btnRateEdit";
            this.btnRateEdit.Size = new System.Drawing.Size(100, 35);
            this.btnRateEdit.TabIndex = 21;
            this.btnRateEdit.Text = "✏️ Edit";
            this.btnRateEdit.UseVisualStyleBackColor = false;
            this.btnRateEdit.Click += new System.EventHandler(this.BtnRateEdit_Click);
            // 
            // btnRateDelete
            // 
            this.btnRateDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRateDelete.FlatAppearance.BorderSize = 0;
            this.btnRateDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRateDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRateDelete.ForeColor = System.Drawing.Color.White;
            this.btnRateDelete.Location = new System.Drawing.Point(410, 220);
            this.btnRateDelete.Name = "btnRateDelete";
            this.btnRateDelete.Size = new System.Drawing.Size(100, 35);
            this.btnRateDelete.TabIndex = 22;
            this.btnRateDelete.Text = "🗑️ Delete";
            this.btnRateDelete.UseVisualStyleBackColor = false;
            this.btnRateDelete.Click += new System.EventHandler(this.BtnRateDelete_Click);
            // 
            // TaxConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1400, 900);
            this.Name = "TaxConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📊 Tax Configuration - Distribution Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabCategories.ResumeLayout(false);
            this.pnlCategories.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.pnlCategoryForm.ResumeLayout(false);
            this.pnlCategoryForm.PerformLayout();
            this.tabRates.ResumeLayout(false);
            this.pnlRates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRates)).EndInit();
            this.pnlRateForm.ResumeLayout(false);
            this.pnlRateForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCategories;
        private System.Windows.Forms.Panel pnlCategories;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.Panel pnlCategoryForm;
        private System.Windows.Forms.Label lblCategoryCode;
        private System.Windows.Forms.TextBox txtCategoryCode;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblCategoryDescription;
        private System.Windows.Forms.TextBox txtCategoryDescription;
        private System.Windows.Forms.CheckBox chkCategoryIsActive;
        private System.Windows.Forms.CheckBox chkCategoryIsSystem;
        private System.Windows.Forms.Button btnCategoryCancel;
        private System.Windows.Forms.Button btnCategorySave;
        private System.Windows.Forms.Button btnCategoryNew;
        private System.Windows.Forms.Button btnCategoryEdit;
        private System.Windows.Forms.Button btnCategoryDelete;
        private System.Windows.Forms.TabPage tabRates;
        private System.Windows.Forms.Panel pnlRates;
        private System.Windows.Forms.DataGridView dgvRates;
        private System.Windows.Forms.Panel pnlRateForm;
        private System.Windows.Forms.Label lblRateCategory;
        private System.Windows.Forms.ComboBox cmbRateCategory;
        private System.Windows.Forms.Label lblRateName;
        private System.Windows.Forms.TextBox txtRateName;
        private System.Windows.Forms.Label lblRateCode;
        private System.Windows.Forms.TextBox txtRateCode;
        private System.Windows.Forms.Label lblRatePercentage;
        private System.Windows.Forms.TextBox txtRatePercentage;
        private System.Windows.Forms.Label lblRateDescription;
        private System.Windows.Forms.TextBox txtRateDescription;
        private System.Windows.Forms.Label lblRateEffectiveFrom;
        private System.Windows.Forms.DateTimePicker dtpRateEffectiveFrom;
        private System.Windows.Forms.Label lblRateEffectiveTo;
        private System.Windows.Forms.DateTimePicker dtpRateEffectiveTo;
        private System.Windows.Forms.CheckBox chkRateIsActive;
        private System.Windows.Forms.CheckBox chkRateIsSystem;
        private System.Windows.Forms.CheckBox chkRateIsCompound;
        private System.Windows.Forms.CheckBox chkRateIsInclusive;
        private System.Windows.Forms.Button btnRateCancel;
        private System.Windows.Forms.Button btnRateSave;
        private System.Windows.Forms.Button btnRateNew;
        private System.Windows.Forms.Button btnRateEdit;
        private System.Windows.Forms.Button btnRateDelete;
    }
}
