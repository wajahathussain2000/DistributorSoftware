using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class BankReconciliationForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBankAccounts = new System.Windows.Forms.TabPage();
            this.pnlBankAccounts = new System.Windows.Forms.Panel();
            this.dgvBankAccounts = new System.Windows.Forms.DataGridView();
            this.tabBankStatements = new System.Windows.Forms.TabPage();
            this.pnlBankStatements = new System.Windows.Forms.Panel();
            this.dgvBankStatements = new System.Windows.Forms.DataGridView();
            this.tabReconciliation = new System.Windows.Forms.TabPage();
            this.pnlReconciliation = new System.Windows.Forms.Panel();
            this.dgvReconciliation = new System.Windows.Forms.DataGridView();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabBankAccounts.SuspendLayout();
            this.pnlBankAccounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankAccounts)).BeginInit();
            this.tabBankStatements.SuspendLayout();
            this.pnlBankStatements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankStatements)).BeginInit();
            this.tabReconciliation.SuspendLayout();
            this.pnlReconciliation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReconciliation)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlHeader.Size = new System.Drawing.Size(1400, 80);
            this.pnlHeader.TabIndex = 0;
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🏦 Bank Reconciliation Management";
            
            // pnlMain
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlMain.Controls.Add(this.tabControl);
            this.pnlMain.Controls.Add(this.pnlActions);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 80);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1400, 820);
            this.pnlMain.TabIndex = 1;
            
            // tabControl
            this.tabControl.Controls.Add(this.tabBankAccounts);
            this.tabControl.Controls.Add(this.tabBankStatements);
            this.tabControl.Controls.Add(this.tabReconciliation);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ItemSize = new System.Drawing.Size(150, 35);
            this.tabControl.Location = new System.Drawing.Point(20, 20);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1360, 720);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            
            // tabBankAccounts
            this.tabBankAccounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabBankAccounts.Controls.Add(this.pnlBankAccounts);
            this.tabBankAccounts.Location = new System.Drawing.Point(4, 39);
            this.tabBankAccounts.Name = "tabBankAccounts";
            this.tabBankAccounts.Padding = new System.Windows.Forms.Padding(20);
            this.tabBankAccounts.Size = new System.Drawing.Size(1352, 677);
            this.tabBankAccounts.TabIndex = 0;
            this.tabBankAccounts.Text = "🏦 Bank Accounts";
            
            // pnlBankAccounts
            this.pnlBankAccounts.BackColor = System.Drawing.Color.White;
            this.pnlBankAccounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBankAccounts.Controls.Add(this.dgvBankAccounts);
            this.pnlBankAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBankAccounts.Location = new System.Drawing.Point(20, 20);
            this.pnlBankAccounts.Name = "pnlBankAccounts";
            this.pnlBankAccounts.Size = new System.Drawing.Size(1310, 635);
            this.pnlBankAccounts.TabIndex = 0;
            
            // dgvBankAccounts
            this.dgvBankAccounts.AllowUserToAddRows = false;
            this.dgvBankAccounts.AllowUserToDeleteRows = false;
            this.dgvBankAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBankAccounts.BackgroundColor = System.Drawing.Color.White;
            this.dgvBankAccounts.ColumnHeadersHeight = 35;
            this.dgvBankAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBankAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBankAccounts.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBankAccounts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvBankAccounts.Location = new System.Drawing.Point(0, 0);
            this.dgvBankAccounts.MultiSelect = false;
            this.dgvBankAccounts.Name = "dgvBankAccounts";
            this.dgvBankAccounts.ReadOnly = true;
            this.dgvBankAccounts.RowHeadersVisible = false;
            this.dgvBankAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBankAccounts.Size = new System.Drawing.Size(1308, 633);
            this.dgvBankAccounts.TabIndex = 0;
            this.dgvBankAccounts.SelectionChanged += new System.EventHandler(this.DgvBankAccounts_SelectionChanged);
            
            // tabBankStatements
            this.tabBankStatements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabBankStatements.Controls.Add(this.pnlBankStatements);
            this.tabBankStatements.Location = new System.Drawing.Point(4, 39);
            this.tabBankStatements.Name = "tabBankStatements";
            this.tabBankStatements.Padding = new System.Windows.Forms.Padding(20);
            this.tabBankStatements.Size = new System.Drawing.Size(1352, 677);
            this.tabBankStatements.TabIndex = 1;
            this.tabBankStatements.Text = "📄 Bank Statements";
            
            // pnlBankStatements
            this.pnlBankStatements.BackColor = System.Drawing.Color.White;
            this.pnlBankStatements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBankStatements.Controls.Add(this.dgvBankStatements);
            this.pnlBankStatements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBankStatements.Location = new System.Drawing.Point(20, 20);
            this.pnlBankStatements.Name = "pnlBankStatements";
            this.pnlBankStatements.Size = new System.Drawing.Size(1310, 635);
            this.pnlBankStatements.TabIndex = 0;
            
            // dgvBankStatements
            this.dgvBankStatements.AllowUserToAddRows = false;
            this.dgvBankStatements.AllowUserToDeleteRows = false;
            this.dgvBankStatements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBankStatements.BackgroundColor = System.Drawing.Color.White;
            this.dgvBankStatements.ColumnHeadersHeight = 35;
            this.dgvBankStatements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBankStatements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBankStatements.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBankStatements.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvBankStatements.Location = new System.Drawing.Point(0, 0);
            this.dgvBankStatements.MultiSelect = false;
            this.dgvBankStatements.Name = "dgvBankStatements";
            this.dgvBankStatements.ReadOnly = true;
            this.dgvBankStatements.RowHeadersVisible = false;
            this.dgvBankStatements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBankStatements.Size = new System.Drawing.Size(1308, 633);
            this.dgvBankStatements.TabIndex = 0;
            this.dgvBankStatements.SelectionChanged += new System.EventHandler(this.DgvBankStatements_SelectionChanged);
            
            // tabReconciliation
            this.tabReconciliation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabReconciliation.Controls.Add(this.pnlReconciliation);
            this.tabReconciliation.Location = new System.Drawing.Point(4, 39);
            this.tabReconciliation.Name = "tabReconciliation";
            this.tabReconciliation.Padding = new System.Windows.Forms.Padding(20);
            this.tabReconciliation.Size = new System.Drawing.Size(1352, 677);
            this.tabReconciliation.TabIndex = 2;
            this.tabReconciliation.Text = "⚖️ Reconciliation";
            
            // pnlReconciliation
            this.pnlReconciliation.BackColor = System.Drawing.Color.White;
            this.pnlReconciliation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReconciliation.Controls.Add(this.dgvReconciliation);
            this.pnlReconciliation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReconciliation.Location = new System.Drawing.Point(20, 20);
            this.pnlReconciliation.Name = "pnlReconciliation";
            this.pnlReconciliation.Size = new System.Drawing.Size(1310, 635);
            this.pnlReconciliation.TabIndex = 0;
            
            // dgvReconciliation
            this.dgvReconciliation.AllowUserToAddRows = false;
            this.dgvReconciliation.AllowUserToDeleteRows = false;
            this.dgvReconciliation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReconciliation.BackgroundColor = System.Drawing.Color.White;
            this.dgvReconciliation.ColumnHeadersHeight = 35;
            this.dgvReconciliation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReconciliation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReconciliation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReconciliation.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvReconciliation.Location = new System.Drawing.Point(0, 0);
            this.dgvReconciliation.MultiSelect = false;
            this.dgvReconciliation.Name = "dgvReconciliation";
            this.dgvReconciliation.ReadOnly = true;
            this.dgvReconciliation.RowHeadersVisible = false;
            this.dgvReconciliation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReconciliation.Size = new System.Drawing.Size(1308, 633);
            this.dgvReconciliation.TabIndex = 0;
            this.dgvReconciliation.SelectionChanged += new System.EventHandler(this.DgvReconciliation_SelectionChanged);
            
            // pnlActions
            this.pnlActions.BackColor = System.Drawing.Color.White;
            this.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActions.Controls.Add(this.btnNew);
            this.pnlActions.Controls.Add(this.btnEdit);
            this.pnlActions.Controls.Add(this.btnDelete);
            this.pnlActions.Controls.Add(this.btnSave);
            this.pnlActions.Controls.Add(this.btnCancel);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(20, 740);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(20);
            this.pnlActions.Size = new System.Drawing.Size(1360, 60);
            this.pnlActions.TabIndex = 1;
            
            // btnNew
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(136)))), ((int)(((byte)(56)))));
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(191)))), ((int)(((byte)(78)))));
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(20, 15);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 30);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "➕ New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            
            // btnEdit
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(132)))), ((int)(((byte)(150)))));
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(184)))), ((int)(((byte)(207)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(130, 15);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "✏️ Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            
            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(43)))), ((int)(((byte)(56)))));
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(62)))), ((int)(((byte)(78)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(240, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            
            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(136)))), ((int)(((byte)(56)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(191)))), ((int)(((byte)(78)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1150, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(94)))), ((int)(((byte)(101)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(137)))), ((int)(((byte)(144)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1260, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // BankReconciliationForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1400, 900);
            this.Name = "BankReconciliationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🏦 Bank Reconciliation - Distribution Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabBankAccounts.ResumeLayout(false);
            this.pnlBankAccounts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankAccounts)).EndInit();
            this.tabBankStatements.ResumeLayout(false);
            this.pnlBankStatements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankStatements)).EndInit();
            this.tabReconciliation.ResumeLayout(false);
            this.pnlReconciliation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReconciliation)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabBankAccounts;
        private System.Windows.Forms.Panel pnlBankAccounts;
        private System.Windows.Forms.DataGridView dgvBankAccounts;
        private System.Windows.Forms.TabPage tabBankStatements;
        private System.Windows.Forms.Panel pnlBankStatements;
        private System.Windows.Forms.DataGridView dgvBankStatements;
        private System.Windows.Forms.TabPage tabReconciliation;
        private System.Windows.Forms.Panel pnlReconciliation;
        private System.Windows.Forms.DataGridView dgvReconciliation;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
