using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class DatabaseBackupRestoreForm
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
            this.Text = "Database Backup & Restore - Distribution Software";
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
                Text = "Database Backup & Restore Management",
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

            // Backup Schedules Tab
            TabPage schedulesTab = new TabPage("Backup Schedules");
            schedulesTab.BackColor = Color.FromArgb(248, 249, 250);
            schedulesTab.Padding = new Padding(20);

            // Backup Schedules Controls
            Panel schedulesPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Schedules DataGridView
            DataGridView schedulesGrid = new DataGridView
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

            schedulesPanel.Controls.Add(schedulesGrid);
            schedulesTab.Controls.Add(schedulesPanel);
            tabControl.TabPages.Add(schedulesTab);

            // Backup History Tab
            TabPage historyTab = new TabPage("Backup History");
            historyTab.BackColor = Color.FromArgb(248, 249, 250);
            historyTab.Padding = new Padding(20);

            // Backup History Controls
            Panel historyPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // History DataGridView
            DataGridView historyGrid = new DataGridView
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

            historyPanel.Controls.Add(historyGrid);
            historyTab.Controls.Add(historyPanel);
            tabControl.TabPages.Add(historyTab);

            // Manual Backup Tab
            TabPage manualTab = new TabPage("Manual Backup");
            manualTab.BackColor = Color.FromArgb(248, 249, 250);
            manualTab.Padding = new Padding(20);

            // Manual Backup Controls
            Panel manualPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20)
            };

            // Manual backup controls
            GroupBox backupGroupBox = new GroupBox
            {
                Text = "Create Manual Backup",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 200,
                Margin = new Padding(0, 0, 0, 20)
            };

            Button createBackupButton = new Button
            {
                Text = "Create Backup Now",
                Size = new Size(150, 40),
                Location = new Point(20, 50),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 }
            };

            backupGroupBox.Controls.Add(createBackupButton);
            manualPanel.Controls.Add(backupGroupBox);

            // Restore GroupBox
            GroupBox restoreGroupBox = new GroupBox
            {
                Text = "Restore Database",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 200,
                Margin = new Padding(0, 0, 0, 20)
            };

            Button browseFileButton = new Button
            {
                Text = "Browse Backup File",
                Size = new Size(150, 40),
                Location = new Point(20, 50),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 }
            };

            Button restoreButton = new Button
            {
                Text = "Restore Database",
                Size = new Size(150, 40),
                Location = new Point(190, 50),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 }
            };

            restoreGroupBox.Controls.Add(browseFileButton);
            restoreGroupBox.Controls.Add(restoreButton);
            manualPanel.Controls.Add(restoreGroupBox);

            manualTab.Controls.Add(manualPanel);
            tabControl.TabPages.Add(manualTab);

            mainPanel.Controls.Add(tabControl);
            this.Controls.Add(mainPanel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}
