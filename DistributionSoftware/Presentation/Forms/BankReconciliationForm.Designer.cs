using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class BankReconciliationForm
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
            this.SuspendLayout();

            // Form settings
            this.Text = "Bank Reconciliation - Distribution Software";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new Size(1400, 900);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 58, 64),
                Padding = new Padding(20, 15, 20, 15)
            };

            Label titleLabel = new Label
            {
                Text = "Bank Reconciliation Management",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            headerPanel.Controls.Add(titleLabel);
            this.Controls.Add(headerPanel);

            // Main Content Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            // Tab Control
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ItemSize = new Size(150, 35),
                SizeMode = TabSizeMode.Fixed
            };

            // Bank Accounts Tab
            TabPage accountsTab = new TabPage("Bank Accounts");
            accountsTab.BackColor = Color.FromArgb(248, 249, 250);
            accountsTab.Padding = new Padding(20);

            // Bank Accounts Controls
            Panel accountsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Accounts DataGridView
            DataGridView accountsGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(224, 224, 224),
                Font = new Font("Segoe UI", 9F),
                RowHeadersVisible = false,
                ColumnHeadersHeight = 35,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            };

            accountsPanel.Controls.Add(accountsGrid);
            accountsTab.Controls.Add(accountsPanel);
            tabControl.TabPages.Add(accountsTab);

            // Bank Statements Tab
            TabPage statementsTab = new TabPage("Bank Statements");
            statementsTab.BackColor = Color.FromArgb(248, 249, 250);
            statementsTab.Padding = new Padding(20);

            // Bank Statements Controls
            Panel statementsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Statements DataGridView
            DataGridView statementsGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(224, 224, 224),
                Font = new Font("Segoe UI", 9F),
                RowHeadersVisible = false,
                ColumnHeadersHeight = 35,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            };

            statementsPanel.Controls.Add(statementsGrid);
            statementsTab.Controls.Add(statementsPanel);
            tabControl.TabPages.Add(statementsTab);

            // Reconciliation Tab
            TabPage reconciliationTab = new TabPage("Reconciliation");
            reconciliationTab.BackColor = Color.FromArgb(248, 249, 250);
            reconciliationTab.Padding = new Padding(20);

            // Reconciliation Controls
            Panel reconciliationPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Reconciliation DataGridView
            DataGridView reconciliationGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(224, 224, 224),
                Font = new Font("Segoe UI", 9F),
                RowHeadersVisible = false,
                ColumnHeadersHeight = 35,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            };

            reconciliationPanel.Controls.Add(reconciliationGrid);
            reconciliationTab.Controls.Add(reconciliationPanel);
            tabControl.TabPages.Add(reconciliationTab);

            mainPanel.Controls.Add(tabControl);
            this.Controls.Add(mainPanel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}
