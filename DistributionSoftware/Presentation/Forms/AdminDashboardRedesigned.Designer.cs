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
            this.navigationPanel = new System.Windows.Forms.Panel();
            this.greetingPanel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.userInfoLabel = new System.Windows.Forms.Label();
            this.greetingLabel = new System.Windows.Forms.Label();
            this.sessionTimer = new System.Windows.Forms.Timer(this.components);
            
            // Dashboard navigation buttons
            this.dashboardBtn = new System.Windows.Forms.Button();
            this.usersBtn = new System.Windows.Forms.Button();
            this.productsBtn = new System.Windows.Forms.Button();
            this.inventoryBtn = new System.Windows.Forms.Button();
            this.salesBtn = new System.Windows.Forms.Button();
            this.purchasesBtn = new System.Windows.Forms.Button();
            this.customersBtn = new System.Windows.Forms.Button();
            this.suppliersBtn = new System.Windows.Forms.Button();
            this.reportsBtn = new System.Windows.Forms.Button();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            
            // Chart panels with proper sizing
            this.salesChartPanel = new System.Windows.Forms.Panel();
            this.inventoryChartPanel = new System.Windows.Forms.Panel();
            this.revenueChartPanel = new System.Windows.Forms.Panel();
            
            // New chart panels
            this.customerChartPanel = new System.Windows.Forms.Panel();
            this.topProductsChartPanel = new System.Windows.Forms.Panel();
            this.purchaseVsSalesChartPanel = new System.Windows.Forms.Panel();
            
            this.SuspendLayout();
            
            // 
            // navigationPanel
            // 
            this.navigationPanel.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.navigationPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationPanel.Location = new System.Drawing.Point(0, 0);
            this.navigationPanel.Name = "navigationPanel";
            this.navigationPanel.Size = new System.Drawing.Size(1920, 70);
            this.navigationPanel.TabIndex = 0;
            
            // 
            // greetingPanel
            // 
            this.greetingPanel.BackColor = System.Drawing.Color.White;
            this.greetingPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.greetingPanel.Location = new System.Drawing.Point(0, 70);
            this.greetingPanel.Name = "greetingPanel";
            this.greetingPanel.Padding = new System.Windows.Forms.Padding(25, 20, 25, 20);
            this.greetingPanel.Size = new System.Drawing.Size(1920, 90);
            this.greetingPanel.TabIndex = 1;
            
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 160);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(30);
            this.contentPanel.Size = new System.Drawing.Size(1920, 920);
            this.contentPanel.TabIndex = 2;
            
            // 
            // userInfoLabel
            // 
            this.userInfoLabel.AutoSize = true;
            this.userInfoLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.userInfoLabel.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.userInfoLabel.Location = new System.Drawing.Point(25, 20);
            this.userInfoLabel.Name = "userInfoLabel";
            this.userInfoLabel.Size = new System.Drawing.Size(250, 30);
            this.userInfoLabel.TabIndex = 0;
            this.userInfoLabel.Text = "Welcome, User";
            
            // 
            // greetingLabel
            // 
            this.greetingLabel.AutoSize = true;
            this.greetingLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular);
            this.greetingLabel.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.greetingLabel.Location = new System.Drawing.Point(25, 55);
            this.greetingLabel.Name = "greetingLabel";
            this.greetingLabel.Size = new System.Drawing.Size(180, 25);
            this.greetingLabel.TabIndex = 1;
            this.greetingLabel.Text = "Good morning!";
            
            // 
            // sessionTimer
            // 
            this.sessionTimer.Interval = 60000;
            this.sessionTimer.Tick += new System.EventHandler(this.SessionTimer_Tick);
            
            // 
            // dashboardBtn
            // 
            this.dashboardBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.dashboardBtn.FlatAppearance.BorderSize = 0;
            this.dashboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashboardBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.dashboardBtn.ForeColor = System.Drawing.Color.Black;
            this.dashboardBtn.Location = new System.Drawing.Point(15, 15);
            this.dashboardBtn.Name = "dashboardBtn";
            this.dashboardBtn.Size = new System.Drawing.Size(100, 40);
            this.dashboardBtn.TabIndex = 0;
            this.dashboardBtn.Text = "📊 Dashboard";
            this.dashboardBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dashboardBtn.UseVisualStyleBackColor = false;
            this.dashboardBtn.Click += new System.EventHandler(this.DashboardBtn_Click);
            
            // 
            // usersBtn
            // 
            this.usersBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.usersBtn.FlatAppearance.BorderSize = 0;
            this.usersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.usersBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.usersBtn.ForeColor = System.Drawing.Color.Black;
            this.usersBtn.Location = new System.Drawing.Point(115, 15);
            this.usersBtn.Name = "usersBtn";
            this.usersBtn.Size = new System.Drawing.Size(100, 40);
            this.usersBtn.TabIndex = 1;
            this.usersBtn.Text = "👥 Users";
            this.usersBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.usersBtn.UseVisualStyleBackColor = false;
            this.usersBtn.Click += new System.EventHandler(this.UsersBtn_Click);
            
            // 
            // productsBtn
            // 
            this.productsBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.productsBtn.FlatAppearance.BorderSize = 0;
            this.productsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.productsBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.productsBtn.ForeColor = System.Drawing.Color.Black;
            this.productsBtn.Location = new System.Drawing.Point(225, 15);
            this.productsBtn.Name = "productsBtn";
            this.productsBtn.Size = new System.Drawing.Size(120, 40);
            this.productsBtn.TabIndex = 2;
            this.productsBtn.Text = "📦 Products";
            this.productsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.productsBtn.UseVisualStyleBackColor = false;
            this.productsBtn.Click += new System.EventHandler(this.ProductsBtn_Click);
            
            // 
            // inventoryBtn
            // 
            this.inventoryBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.inventoryBtn.FlatAppearance.BorderSize = 0;
            this.inventoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inventoryBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.inventoryBtn.ForeColor = System.Drawing.Color.Black;
            this.inventoryBtn.Location = new System.Drawing.Point(355, 15);
            this.inventoryBtn.Name = "inventoryBtn";
            this.inventoryBtn.Size = new System.Drawing.Size(100, 40);
            this.inventoryBtn.TabIndex = 3;
            this.inventoryBtn.Text = "🏪 Inventory";
            this.inventoryBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.inventoryBtn.UseVisualStyleBackColor = false;
            this.inventoryBtn.Click += new System.EventHandler(this.InventoryBtn_Click);
            
            // 
            // salesBtn
            // 
            this.salesBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.salesBtn.FlatAppearance.BorderSize = 0;
            this.salesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.salesBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.salesBtn.ForeColor = System.Drawing.Color.Black;
            this.salesBtn.Location = new System.Drawing.Point(465, 15);
            this.salesBtn.Name = "salesBtn";
            this.salesBtn.Size = new System.Drawing.Size(100, 40);
            this.salesBtn.TabIndex = 4;
            this.salesBtn.Text = "💰 Sales";
            this.salesBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.salesBtn.UseVisualStyleBackColor = false;
            this.salesBtn.Click += new System.EventHandler(this.SalesBtn_Click);
            
            // 
            // purchasesBtn
            // 
            this.purchasesBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.purchasesBtn.FlatAppearance.BorderSize = 0;
            this.purchasesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.purchasesBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.purchasesBtn.ForeColor = System.Drawing.Color.Black;
            this.purchasesBtn.Location = new System.Drawing.Point(575, 15);
            this.purchasesBtn.Name = "purchasesBtn";
            this.purchasesBtn.Size = new System.Drawing.Size(100, 40);
            this.purchasesBtn.TabIndex = 5;
            this.purchasesBtn.Text = "🛒 Purchases";
            this.purchasesBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.purchasesBtn.UseVisualStyleBackColor = false;
            this.purchasesBtn.Click += new System.EventHandler(this.PurchasesBtn_Click);
            
            // 
            // customersBtn
            // 
            this.customersBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.customersBtn.FlatAppearance.BorderSize = 0;
            this.customersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customersBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.customersBtn.ForeColor = System.Drawing.Color.Black;
            this.customersBtn.Location = new System.Drawing.Point(685, 15);
            this.customersBtn.Name = "customersBtn";
            this.customersBtn.Size = new System.Drawing.Size(100, 40);
            this.customersBtn.TabIndex = 6;
            this.customersBtn.Text = "👤 Customer";
            this.customersBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.customersBtn.UseVisualStyleBackColor = false;
            this.customersBtn.Click += new System.EventHandler(this.CustomersBtn_Click);
            
            // 
            // suppliersBtn
            // 
            this.suppliersBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.suppliersBtn.FlatAppearance.BorderSize = 0;
            this.suppliersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.suppliersBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.suppliersBtn.ForeColor = System.Drawing.Color.Black;
            this.suppliersBtn.Location = new System.Drawing.Point(795, 15);
            this.suppliersBtn.Name = "suppliersBtn";
            this.suppliersBtn.Size = new System.Drawing.Size(100, 40);
            this.suppliersBtn.TabIndex = 7;
            this.suppliersBtn.Text = "🏭 Suppliers";
            this.suppliersBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.suppliersBtn.UseVisualStyleBackColor = false;
            this.suppliersBtn.Click += new System.EventHandler(this.SuppliersBtn_Click);
            
            // 
            // reportsBtn
            // 
            this.reportsBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.reportsBtn.FlatAppearance.BorderSize = 0;
            this.reportsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportsBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.reportsBtn.ForeColor = System.Drawing.Color.Black;
            this.reportsBtn.Location = new System.Drawing.Point(905, 15);
            this.reportsBtn.Name = "reportsBtn";
            this.reportsBtn.Size = new System.Drawing.Size(100, 40);
            this.reportsBtn.TabIndex = 8;
            this.reportsBtn.Text = "📈 Reports";
            this.reportsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.reportsBtn.UseVisualStyleBackColor = false;
            this.reportsBtn.Click += new System.EventHandler(this.ReportsBtn_Click);
            
            // 
            // settingsBtn
            // 
            this.settingsBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.settingsBtn.FlatAppearance.BorderSize = 0;
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.settingsBtn.ForeColor = System.Drawing.Color.Black;
            this.settingsBtn.Location = new System.Drawing.Point(1015, 15);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(100, 40);
            this.settingsBtn.TabIndex = 9;
            this.settingsBtn.Text = "⚙️ Settings";
            this.settingsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.settingsBtn.UseVisualStyleBackColor = false;
            this.settingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.logoutBtn.FlatAppearance.BorderSize = 0;
            this.logoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.logoutBtn.ForeColor = System.Drawing.Color.Black;
            this.logoutBtn.Location = new System.Drawing.Point(1125, 15);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(100, 40);
            this.logoutBtn.TabIndex = 10;
            this.logoutBtn.Text = "🚪 Logout";
            this.logoutBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.logoutBtn.UseVisualStyleBackColor = false;
            this.logoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            
            // 
            // salesChartPanel
            // 
            this.salesChartPanel.BackColor = System.Drawing.Color.White;
            this.salesChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.salesChartPanel.Location = new System.Drawing.Point(30, 30);
            this.salesChartPanel.Name = "salesChartPanel";
            this.salesChartPanel.Size = new System.Drawing.Size(350, 250);
            this.salesChartPanel.TabIndex = 0;
            
            // 
            // inventoryChartPanel
            // 
            this.inventoryChartPanel.BackColor = System.Drawing.Color.White;
            this.inventoryChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inventoryChartPanel.Location = new System.Drawing.Point(400, 30);
            this.inventoryChartPanel.Name = "inventoryChartPanel";
            this.inventoryChartPanel.Size = new System.Drawing.Size(350, 250);
            this.inventoryChartPanel.TabIndex = 1;
            
            // 
            // revenueChartPanel
            // 
            this.revenueChartPanel.BackColor = System.Drawing.Color.White;
            this.revenueChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.revenueChartPanel.Location = new System.Drawing.Point(770, 30);
            this.revenueChartPanel.Name = "revenueChartPanel";
            this.revenueChartPanel.Size = new System.Drawing.Size(350, 250);
            this.revenueChartPanel.TabIndex = 2;
            
            // 
            // customerChartPanel
            // 
            this.customerChartPanel.BackColor = System.Drawing.Color.White;
            this.customerChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.customerChartPanel.Location = new System.Drawing.Point(30, 300);
            this.customerChartPanel.Name = "customerChartPanel";
            this.customerChartPanel.Size = new System.Drawing.Size(350, 250);
            this.customerChartPanel.TabIndex = 3;
            
            // 
            // topProductsChartPanel
            // 
            this.topProductsChartPanel.BackColor = System.Drawing.Color.White;
            this.topProductsChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.topProductsChartPanel.Location = new System.Drawing.Point(400, 300);
            this.topProductsChartPanel.Name = "topProductsChartPanel";
            this.topProductsChartPanel.Size = new System.Drawing.Size(350, 250);
            this.topProductsChartPanel.TabIndex = 4;
            
            // 
            // purchaseVsSalesChartPanel
            // 
            this.purchaseVsSalesChartPanel.BackColor = System.Drawing.Color.White;
            this.purchaseVsSalesChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.purchaseVsSalesChartPanel.Location = new System.Drawing.Point(770, 300);
            this.purchaseVsSalesChartPanel.Name = "purchaseVsSalesChartPanel";
            this.purchaseVsSalesChartPanel.Size = new System.Drawing.Size(350, 250);
            this.purchaseVsSalesChartPanel.TabIndex = 5;
            
            // 
            // AdminDashboardRedesigned
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.greetingPanel);
            this.Controls.Add(this.navigationPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AdminDashboardRedesigned";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distribution Software - Admin Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            // Add controls to panels
            this.navigationPanel.Controls.Add(this.dashboardBtn);
            this.navigationPanel.Controls.Add(this.usersBtn);
            this.navigationPanel.Controls.Add(this.productsBtn);
            this.navigationPanel.Controls.Add(this.inventoryBtn);
            this.navigationPanel.Controls.Add(this.salesBtn);
            this.navigationPanel.Controls.Add(this.purchasesBtn);
            this.navigationPanel.Controls.Add(this.customersBtn);
            this.navigationPanel.Controls.Add(this.suppliersBtn);
            this.navigationPanel.Controls.Add(this.reportsBtn);
            this.navigationPanel.Controls.Add(this.settingsBtn);
            this.navigationPanel.Controls.Add(this.logoutBtn);
            
            this.greetingPanel.Controls.Add(this.userInfoLabel);
            this.greetingPanel.Controls.Add(this.greetingLabel);
            
            this.contentPanel.Controls.Add(this.salesChartPanel);
            this.contentPanel.Controls.Add(this.inventoryChartPanel);
            this.contentPanel.Controls.Add(this.revenueChartPanel);
            this.contentPanel.Controls.Add(this.customerChartPanel);
            this.contentPanel.Controls.Add(this.topProductsChartPanel);
            this.contentPanel.Controls.Add(this.purchaseVsSalesChartPanel);
            
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel navigationPanel;
        private System.Windows.Forms.Panel greetingPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label userInfoLabel;
        private System.Windows.Forms.Label greetingLabel;
        private System.Windows.Forms.Timer sessionTimer;
        
        // Navigation buttons
        private System.Windows.Forms.Button dashboardBtn;
        private System.Windows.Forms.Button usersBtn;
        private System.Windows.Forms.Button productsBtn;
        private System.Windows.Forms.Button inventoryBtn;
        private System.Windows.Forms.Button salesBtn;
        private System.Windows.Forms.Button purchasesBtn;
        private System.Windows.Forms.Button customersBtn;
        private System.Windows.Forms.Button suppliersBtn;
        private System.Windows.Forms.Button reportsBtn;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Button logoutBtn;
        
        // Chart panels with proper sizing
        private System.Windows.Forms.Panel salesChartPanel;
        private System.Windows.Forms.Panel inventoryChartPanel;
        private System.Windows.Forms.Panel revenueChartPanel;
        
        // New chart panels
        private System.Windows.Forms.Panel customerChartPanel;
        private System.Windows.Forms.Panel topProductsChartPanel;
        private System.Windows.Forms.Panel purchaseVsSalesChartPanel;
    }
}
