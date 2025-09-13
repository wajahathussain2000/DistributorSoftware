namespace DistributionSoftware.Presentation.Forms
{
    partial class AdminDashboardRedesigned
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
            this.components = new System.ComponentModel.Container();
            this.sessionTimer = new System.Windows.Forms.Timer(this.components);
            this.userInfoLabel = new System.Windows.Forms.Label();
            this.greetingLabel = new System.Windows.Forms.Label();
            this.dashboardBtn = new System.Windows.Forms.Button();
            this.usersBtn = new System.Windows.Forms.Button();
            this.productsBtn = new System.Windows.Forms.Button();
            this.inventoryBtn = new System.Windows.Forms.Button();
            this.salesBtn = new System.Windows.Forms.Button();
            this.purchasesBtn = new System.Windows.Forms.Button();
            this.customersBtn = new System.Windows.Forms.Button();
            this.suppliersBtn = new System.Windows.Forms.Button();
            this.reportsBtn = new System.Windows.Forms.Button();
            this.expenseBtn = new System.Windows.Forms.Button();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.salesChartPanel = new System.Windows.Forms.Panel();
            this.inventoryChartPanel = new System.Windows.Forms.Panel();
            this.revenueChartPanel = new System.Windows.Forms.Panel();
            this.customerChartPanel = new System.Windows.Forms.Panel();
            this.topProductsChartPanel = new System.Windows.Forms.Panel();
            this.purchaseVsSalesChartPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // sessionTimer
            // 
            this.sessionTimer.Interval = 30000;
            this.sessionTimer.Tick += new System.EventHandler(this.SessionTimer_Tick);
            // 
            // userInfoLabel
            // 
            this.userInfoLabel.AutoSize = true;
            this.userInfoLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userInfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.userInfoLabel.Location = new System.Drawing.Point(20, 20);
            this.userInfoLabel.Name = "userInfoLabel";
            this.userInfoLabel.Size = new System.Drawing.Size(196, 21);
            this.userInfoLabel.TabIndex = 0;
            this.userInfoLabel.Text = "Welcome, Administrator";
            // 
            // greetingLabel
            // 
            this.greetingLabel.AutoSize = true;
            this.greetingLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.greetingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.greetingLabel.Location = new System.Drawing.Point(221, 22);
            this.greetingLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.greetingLabel.Name = "greetingLabel";
            this.greetingLabel.Size = new System.Drawing.Size(103, 19);
            this.greetingLabel.TabIndex = 1;
            this.greetingLabel.Text = "Good morning!";
            // 
            // dashboardBtn
            // 
            this.dashboardBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.dashboardBtn.FlatAppearance.BorderSize = 0;
            this.dashboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashboardBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dashboardBtn.ForeColor = System.Drawing.Color.White;
            this.dashboardBtn.Location = new System.Drawing.Point(5, 65);
            this.dashboardBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dashboardBtn.Name = "dashboardBtn";
            this.dashboardBtn.Size = new System.Drawing.Size(80, 30);
            this.dashboardBtn.TabIndex = 2;
            this.dashboardBtn.Text = "Dashboard";
            this.dashboardBtn.UseVisualStyleBackColor = false;
            this.dashboardBtn.Click += new System.EventHandler(this.DashboardBtn_Click);
            // 
            // usersBtn
            // 
            this.usersBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.usersBtn.FlatAppearance.BorderSize = 0;
            this.usersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.usersBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usersBtn.ForeColor = System.Drawing.Color.White;
            this.usersBtn.Location = new System.Drawing.Point(90, 65);
            this.usersBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usersBtn.Name = "usersBtn";
            this.usersBtn.Size = new System.Drawing.Size(80, 30);
            this.usersBtn.TabIndex = 3;
            this.usersBtn.Text = "Users";
            this.usersBtn.UseVisualStyleBackColor = false;
            this.usersBtn.Click += new System.EventHandler(this.UsersBtn_Click);
            // 
            // productsBtn
            // 
            this.productsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.productsBtn.FlatAppearance.BorderSize = 0;
            this.productsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.productsBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productsBtn.ForeColor = System.Drawing.Color.White;
            this.productsBtn.Location = new System.Drawing.Point(175, 65);
            this.productsBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.productsBtn.Name = "productsBtn";
            this.productsBtn.Size = new System.Drawing.Size(80, 30);
            this.productsBtn.TabIndex = 4;
            this.productsBtn.Text = "Products";
            this.productsBtn.UseVisualStyleBackColor = false;
            this.productsBtn.Click += new System.EventHandler(this.ProductsBtn_Click);
            // 
            // inventoryBtn
            // 
            this.inventoryBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.inventoryBtn.FlatAppearance.BorderSize = 0;
            this.inventoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inventoryBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventoryBtn.ForeColor = System.Drawing.Color.White;
            this.inventoryBtn.Location = new System.Drawing.Point(260, 65);
            this.inventoryBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inventoryBtn.Name = "inventoryBtn";
            this.inventoryBtn.Size = new System.Drawing.Size(80, 30);
            this.inventoryBtn.TabIndex = 5;
            this.inventoryBtn.Text = "Inventory";
            this.inventoryBtn.UseVisualStyleBackColor = false;
            this.inventoryBtn.Click += new System.EventHandler(this.InventoryBtn_Click);
            // 
            // salesBtn
            // 
            this.salesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.salesBtn.FlatAppearance.BorderSize = 0;
            this.salesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.salesBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salesBtn.ForeColor = System.Drawing.Color.White;
            this.salesBtn.Location = new System.Drawing.Point(345, 65);
            this.salesBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.salesBtn.Name = "salesBtn";
            this.salesBtn.Size = new System.Drawing.Size(80, 30);
            this.salesBtn.TabIndex = 6;
            this.salesBtn.Text = "Sales";
            this.salesBtn.UseVisualStyleBackColor = false;
            this.salesBtn.Click += new System.EventHandler(this.SalesBtn_Click);
            // 
            // purchasesBtn
            // 
            this.purchasesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.purchasesBtn.FlatAppearance.BorderSize = 0;
            this.purchasesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.purchasesBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purchasesBtn.ForeColor = System.Drawing.Color.White;
            this.purchasesBtn.Location = new System.Drawing.Point(430, 65);
            this.purchasesBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.purchasesBtn.Name = "purchasesBtn";
            this.purchasesBtn.Size = new System.Drawing.Size(80, 30);
            this.purchasesBtn.TabIndex = 7;
            this.purchasesBtn.Text = "Purchases";
            this.purchasesBtn.UseVisualStyleBackColor = false;
            this.purchasesBtn.Click += new System.EventHandler(this.PurchasesBtn_Click);
            // 
            // customersBtn
            // 
            this.customersBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.customersBtn.FlatAppearance.BorderSize = 0;
            this.customersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customersBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customersBtn.ForeColor = System.Drawing.Color.White;
            this.customersBtn.Location = new System.Drawing.Point(515, 65);
            this.customersBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.customersBtn.Name = "customersBtn";
            this.customersBtn.Size = new System.Drawing.Size(80, 30);
            this.customersBtn.TabIndex = 8;
            this.customersBtn.Text = "Customers";
            this.customersBtn.UseVisualStyleBackColor = false;
            this.customersBtn.Click += new System.EventHandler(this.CustomersBtn_Click);
            // 
            // suppliersBtn
            // 
            this.suppliersBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.suppliersBtn.FlatAppearance.BorderSize = 0;
            this.suppliersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.suppliersBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.suppliersBtn.ForeColor = System.Drawing.Color.White;
            this.suppliersBtn.Location = new System.Drawing.Point(600, 65);
            this.suppliersBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.suppliersBtn.Name = "suppliersBtn";
            this.suppliersBtn.Size = new System.Drawing.Size(80, 30);
            this.suppliersBtn.TabIndex = 9;
            this.suppliersBtn.Text = "Suppliers";
            this.suppliersBtn.UseVisualStyleBackColor = false;
            this.suppliersBtn.Click += new System.EventHandler(this.SuppliersBtn_Click);
            // 
            // reportsBtn
            // 
            this.reportsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.reportsBtn.FlatAppearance.BorderSize = 0;
            this.reportsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportsBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsBtn.ForeColor = System.Drawing.Color.White;
            this.reportsBtn.Location = new System.Drawing.Point(685, 65);
            this.reportsBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportsBtn.Name = "reportsBtn";
            this.reportsBtn.Size = new System.Drawing.Size(80, 30);
            this.reportsBtn.TabIndex = 10;
            this.reportsBtn.Text = "Reports";
            this.reportsBtn.UseVisualStyleBackColor = false;
            this.reportsBtn.Click += new System.EventHandler(this.ReportsBtn_Click);
            // 
            // expenseBtn
            // 
            this.expenseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.expenseBtn.FlatAppearance.BorderSize = 0;
            this.expenseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expenseBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expenseBtn.ForeColor = System.Drawing.Color.White;
            this.expenseBtn.Location = new System.Drawing.Point(770, 65);
            this.expenseBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.expenseBtn.Name = "expenseBtn";
            this.expenseBtn.Size = new System.Drawing.Size(80, 30);
            this.expenseBtn.TabIndex = 11;
            this.expenseBtn.Text = "Expense";
            this.expenseBtn.UseVisualStyleBackColor = false;
            this.expenseBtn.Click += new System.EventHandler(this.ExpenseBtn_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.settingsBtn.FlatAppearance.BorderSize = 0;
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsBtn.ForeColor = System.Drawing.Color.White;
            this.settingsBtn.Location = new System.Drawing.Point(855, 65);
            this.settingsBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(80, 30);
            this.settingsBtn.TabIndex = 12;
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.UseVisualStyleBackColor = false;
            this.settingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.logoutBtn.FlatAppearance.BorderSize = 0;
            this.logoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutBtn.ForeColor = System.Drawing.Color.White;
            this.logoutBtn.Location = new System.Drawing.Point(940, 65);
            this.logoutBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(80, 30);
            this.logoutBtn.TabIndex = 13;
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.UseVisualStyleBackColor = false;
            this.logoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // salesChartPanel
            // 
            this.salesChartPanel.BackColor = System.Drawing.Color.White;
            this.salesChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salesChartPanel.Location = new System.Drawing.Point(5, 104);
            this.salesChartPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.salesChartPanel.Name = "salesChartPanel";
            this.salesChartPanel.Size = new System.Drawing.Size(320, 220);
            this.salesChartPanel.TabIndex = 13;
            // 
            // inventoryChartPanel
            // 
            this.inventoryChartPanel.BackColor = System.Drawing.Color.White;
            this.inventoryChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inventoryChartPanel.Location = new System.Drawing.Point(330, 104);
            this.inventoryChartPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inventoryChartPanel.Name = "inventoryChartPanel";
            this.inventoryChartPanel.Size = new System.Drawing.Size(250, 220);
            this.inventoryChartPanel.TabIndex = 14;
            // 
            // revenueChartPanel
            // 
            this.revenueChartPanel.BackColor = System.Drawing.Color.White;
            this.revenueChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.revenueChartPanel.Location = new System.Drawing.Point(585, 104);
            this.revenueChartPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.revenueChartPanel.Name = "revenueChartPanel";
            this.revenueChartPanel.Size = new System.Drawing.Size(400, 220);
            this.revenueChartPanel.TabIndex = 15;
            // 
            // customerChartPanel
            // 
            this.customerChartPanel.BackColor = System.Drawing.Color.White;
            this.customerChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customerChartPanel.Location = new System.Drawing.Point(5, 330);
            this.customerChartPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.customerChartPanel.Name = "customerChartPanel";
            this.customerChartPanel.Size = new System.Drawing.Size(250, 220);
            this.customerChartPanel.TabIndex = 16;
            // 
            // topProductsChartPanel
            // 
            this.topProductsChartPanel.BackColor = System.Drawing.Color.White;
            this.topProductsChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topProductsChartPanel.Location = new System.Drawing.Point(260, 330);
            this.topProductsChartPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.topProductsChartPanel.Name = "topProductsChartPanel";
            this.topProductsChartPanel.Size = new System.Drawing.Size(320, 220);
            this.topProductsChartPanel.TabIndex = 17;
            // 
            // purchaseVsSalesChartPanel
            // 
            this.purchaseVsSalesChartPanel.BackColor = System.Drawing.Color.White;
            this.purchaseVsSalesChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.purchaseVsSalesChartPanel.Location = new System.Drawing.Point(585, 330);
            this.purchaseVsSalesChartPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.purchaseVsSalesChartPanel.Name = "purchaseVsSalesChartPanel";
            this.purchaseVsSalesChartPanel.Size = new System.Drawing.Size(400, 220);
            this.purchaseVsSalesChartPanel.TabIndex = 18;
            // 
            // AdminDashboardRedesigned
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.purchaseVsSalesChartPanel);
            this.Controls.Add(this.topProductsChartPanel);
            this.Controls.Add(this.customerChartPanel);
            this.Controls.Add(this.revenueChartPanel);
            this.Controls.Add(this.inventoryChartPanel);
            this.Controls.Add(this.salesChartPanel);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.settingsBtn);
            this.Controls.Add(this.expenseBtn);
            this.Controls.Add(this.reportsBtn);
            this.Controls.Add(this.suppliersBtn);
            this.Controls.Add(this.customersBtn);
            this.Controls.Add(this.purchasesBtn);
            this.Controls.Add(this.salesBtn);
            this.Controls.Add(this.inventoryBtn);
            this.Controls.Add(this.productsBtn);
            this.Controls.Add(this.usersBtn);
            this.Controls.Add(this.dashboardBtn);
            this.Controls.Add(this.greetingLabel);
            this.Controls.Add(this.userInfoLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "AdminDashboardRedesigned";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard - Distribution Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer sessionTimer;
        private System.Windows.Forms.Label userInfoLabel;
        private System.Windows.Forms.Label greetingLabel;
        private System.Windows.Forms.Button dashboardBtn;
        private System.Windows.Forms.Button usersBtn;
        private System.Windows.Forms.Button productsBtn;
        private System.Windows.Forms.Button inventoryBtn;
        private System.Windows.Forms.Button salesBtn;
        private System.Windows.Forms.Button purchasesBtn;
        private System.Windows.Forms.Button customersBtn;
        private System.Windows.Forms.Button suppliersBtn;
        private System.Windows.Forms.Button reportsBtn;
        private System.Windows.Forms.Button expenseBtn;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.Panel salesChartPanel;
        private System.Windows.Forms.Panel inventoryChartPanel;
        private System.Windows.Forms.Panel revenueChartPanel;
        private System.Windows.Forms.Panel customerChartPanel;
        private System.Windows.Forms.Panel topProductsChartPanel;
        private System.Windows.Forms.Panel purchaseVsSalesChartPanel;
    }
}

