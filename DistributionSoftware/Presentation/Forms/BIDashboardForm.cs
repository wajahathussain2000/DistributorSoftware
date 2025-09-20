using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class BIDashboardForm : Form
    {
        private ISalesInvoiceService _salesInvoiceService;
        private IPurchaseInvoiceService _purchaseInvoiceService;
        private ICustomerService _customerService;
        private IProductService _productService;

        public BIDashboardForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }

        private void InitializeServices()
        {
            _salesInvoiceService = new SalesInvoiceService();
            _purchaseInvoiceService = new PurchaseInvoiceService();
            
            var connectionString = DistributionSoftware.Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var customerRepository = new DistributionSoftware.DataAccess.CustomerRepository(connectionString);
            _customerService = new CustomerService(customerRepository);
            
            _productService = new ProductService();
        }

        private void InitializeForm()
        {
            SetupOverviewTab();
            SetupAnalyticsTab();
            SetupForecastingTab();
            SetupCustomDashboardTab();
        }

        private void SetupOverviewTab()
        {
            // Create overview dashboard with key metrics
            var overviewPanel = new Panel();
            overviewPanel.Dock = DockStyle.Fill;
            overviewPanel.Padding = new Padding(10);

            // Add key metrics cards
            var metricsPanel = new FlowLayoutPanel();
            metricsPanel.Dock = DockStyle.Top;
            metricsPanel.Height = 150;
            metricsPanel.FlowDirection = FlowDirection.LeftToRight;

            // Sales Revenue Card
            var salesCard = CreateMetricCard("Sales Revenue", GetSalesRevenue(), Color.FromArgb(46, 204, 113));
            metricsPanel.Controls.Add(salesCard);

            // Purchase Cost Card
            var purchaseCard = CreateMetricCard("Purchase Cost", GetPurchaseCost(), Color.FromArgb(231, 76, 60));
            metricsPanel.Controls.Add(purchaseCard);

            // Customer Count Card
            var customerCard = CreateMetricCard("Total Customers", GetCustomerCount(), Color.FromArgb(52, 152, 219));
            metricsPanel.Controls.Add(customerCard);

            // Product Count Card
            var productCard = CreateMetricCard("Total Products", GetProductCount(), Color.FromArgb(155, 89, 182));
            metricsPanel.Controls.Add(productCard);

            overviewPanel.Controls.Add(metricsPanel);

            // Add charts panel
            var chartsPanel = new Panel();
            chartsPanel.Dock = DockStyle.Fill;
            chartsPanel.BackColor = Color.White;
            chartsPanel.Padding = new Padding(10);

            var chartsLabel = new Label();
            chartsLabel.Text = "Sales Trend (Last 12 Months)";
            chartsLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chartsLabel.Location = new Point(10, 10);
            chartsLabel.AutoSize = true;
            chartsPanel.Controls.Add(chartsLabel);

            // Add a simple chart representation
            var chartPanel = new Panel();
            chartPanel.Location = new Point(10, 40);
            chartPanel.Size = new Size(800, 300);
            chartPanel.BackColor = Color.LightGray;
            chartPanel.BorderStyle = BorderStyle.FixedSingle;
            
            var chartLabel = new Label();
            chartLabel.Text = "📊 Sales Trend Chart\n(Chart visualization would be implemented here)";
            chartLabel.Font = new Font("Segoe UI", 10);
            chartLabel.Location = new Point(50, 50);
            chartLabel.AutoSize = true;
            chartPanel.Controls.Add(chartLabel);
            
            chartsPanel.Controls.Add(chartPanel);

            overviewPanel.Controls.Add(chartsPanel);
            tabOverview.Controls.Add(overviewPanel);
        }

        private void SetupAnalyticsTab()
        {
            var analyticsPanel = new Panel();
            analyticsPanel.Dock = DockStyle.Fill;
            analyticsPanel.Padding = new Padding(10);

            var analyticsLabel = new Label();
            analyticsLabel.Text = "Advanced Analytics";
            analyticsLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            analyticsLabel.Location = new Point(10, 10);
            analyticsLabel.AutoSize = true;
            analyticsPanel.Controls.Add(analyticsLabel);

            // Customer Analytics
            var customerAnalyticsGroup = new GroupBox();
            customerAnalyticsGroup.Text = "Customer Analytics";
            customerAnalyticsGroup.Location = new Point(10, 50);
            customerAnalyticsGroup.Size = new Size(500, 200);
            customerAnalyticsGroup.Font = new Font("Segoe UI", 10);

            var customerAnalyticsText = new Label();
            customerAnalyticsText.Text = "• Top 10 Customers by Revenue\n• Customer Lifetime Value Analysis\n• Customer Segmentation\n• Churn Prediction";
            customerAnalyticsText.Location = new Point(10, 25);
            customerAnalyticsText.Size = new Size(480, 150);
            customerAnalyticsText.Font = new Font("Segoe UI", 9);
            customerAnalyticsGroup.Controls.Add(customerAnalyticsText);

            analyticsPanel.Controls.Add(customerAnalyticsGroup);

            // Product Analytics
            var productAnalyticsGroup = new GroupBox();
            productAnalyticsGroup.Text = "Product Analytics";
            productAnalyticsGroup.Location = new Point(520, 50);
            productAnalyticsGroup.Size = new Size(500, 200);
            productAnalyticsGroup.Font = new Font("Segoe UI", 10);

            var productAnalyticsText = new Label();
            productAnalyticsText.Text = "• Best Selling Products\n• Product Performance Analysis\n• Inventory Turnover\n• Price Optimization";
            productAnalyticsText.Location = new Point(10, 25);
            productAnalyticsText.Size = new Size(480, 150);
            productAnalyticsText.Font = new Font("Segoe UI", 9);
            productAnalyticsGroup.Controls.Add(productAnalyticsText);

            analyticsPanel.Controls.Add(productAnalyticsGroup);

            // Sales Analytics
            var salesAnalyticsGroup = new GroupBox();
            salesAnalyticsGroup.Text = "Sales Analytics";
            salesAnalyticsGroup.Location = new Point(10, 260);
            salesAnalyticsGroup.Size = new Size(500, 200);
            salesAnalyticsGroup.Font = new Font("Segoe UI", 10);

            var salesAnalyticsText = new Label();
            salesAnalyticsText.Text = "• Sales Trend Analysis\n• Seasonal Patterns\n• Sales Performance by Region\n• Conversion Rate Analysis";
            salesAnalyticsText.Location = new Point(10, 25);
            salesAnalyticsText.Size = new Size(480, 150);
            salesAnalyticsText.Font = new Font("Segoe UI", 9);
            salesAnalyticsGroup.Controls.Add(salesAnalyticsText);

            analyticsPanel.Controls.Add(salesAnalyticsGroup);

            // Financial Analytics
            var financialAnalyticsGroup = new GroupBox();
            financialAnalyticsGroup.Text = "Financial Analytics";
            financialAnalyticsGroup.Location = new Point(520, 260);
            financialAnalyticsGroup.Size = new Size(500, 200);
            financialAnalyticsGroup.Font = new Font("Segoe UI", 10);

            var financialAnalyticsText = new Label();
            financialAnalyticsText.Text = "• Profit Margin Analysis\n• Cash Flow Analysis\n• ROI Analysis\n• Cost Center Analysis";
            financialAnalyticsText.Location = new Point(10, 25);
            financialAnalyticsText.Size = new Size(480, 150);
            financialAnalyticsText.Font = new Font("Segoe UI", 9);
            financialAnalyticsGroup.Controls.Add(financialAnalyticsText);

            analyticsPanel.Controls.Add(financialAnalyticsGroup);

            tabAnalytics.Controls.Add(analyticsPanel);
        }

        private void SetupForecastingTab()
        {
            var forecastingPanel = new Panel();
            forecastingPanel.Dock = DockStyle.Fill;
            forecastingPanel.Padding = new Padding(10);

            var forecastingLabel = new Label();
            forecastingLabel.Text = "Predictive Analytics & Forecasting";
            forecastingLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            forecastingLabel.Location = new Point(10, 10);
            forecastingLabel.AutoSize = true;
            forecastingPanel.Controls.Add(forecastingLabel);

            // Sales Forecasting
            var salesForecastGroup = new GroupBox();
            salesForecastGroup.Text = "Sales Forecasting";
            salesForecastGroup.Location = new Point(10, 50);
            salesForecastGroup.Size = new Size(500, 200);
            salesForecastGroup.Font = new Font("Segoe UI", 10);

            var salesForecastText = new Label();
            salesForecastText.Text = "• Next 3 Months Sales Prediction\n• Seasonal Demand Forecasting\n• Product Demand Prediction\n• Revenue Projection";
            salesForecastText.Location = new Point(10, 25);
            salesForecastText.Size = new Size(480, 150);
            salesForecastText.Font = new Font("Segoe UI", 9);
            salesForecastGroup.Controls.Add(salesForecastText);

            forecastingPanel.Controls.Add(salesForecastGroup);

            // Inventory Forecasting
            var inventoryForecastGroup = new GroupBox();
            inventoryForecastGroup.Text = "Inventory Forecasting";
            inventoryForecastGroup.Location = new Point(520, 50);
            inventoryForecastGroup.Size = new Size(500, 200);
            inventoryForecastGroup.Font = new Font("Segoe UI", 10);

            var inventoryForecastText = new Label();
            inventoryForecastText.Text = "• Stock Level Prediction\n• Reorder Point Optimization\n• Demand-Supply Gap Analysis\n• Inventory Turnover Forecast";
            inventoryForecastText.Location = new Point(10, 25);
            inventoryForecastText.Size = new Size(480, 150);
            inventoryForecastText.Font = new Font("Segoe UI", 9);
            inventoryForecastGroup.Controls.Add(inventoryForecastText);

            forecastingPanel.Controls.Add(inventoryForecastGroup);

            // Financial Forecasting
            var financialForecastGroup = new GroupBox();
            financialForecastGroup.Text = "Financial Forecasting";
            financialForecastGroup.Location = new Point(10, 260);
            financialForecastGroup.Size = new Size(500, 200);
            financialForecastGroup.Font = new Font("Segoe UI", 10);

            var financialForecastText = new Label();
            financialForecastText.Text = "• Cash Flow Projection\n• Profit Margin Prediction\n• Break-even Analysis\n• Financial Risk Assessment";
            financialForecastText.Location = new Point(10, 25);
            financialForecastText.Size = new Size(480, 150);
            financialForecastText.Font = new Font("Segoe UI", 9);
            financialForecastGroup.Controls.Add(financialForecastText);

            forecastingPanel.Controls.Add(financialForecastGroup);

            // Customer Forecasting
            var customerForecastGroup = new GroupBox();
            customerForecastGroup.Text = "Customer Forecasting";
            customerForecastGroup.Location = new Point(520, 260);
            customerForecastGroup.Size = new Size(500, 200);
            customerForecastGroup.Font = new Font("Segoe UI", 10);

            var customerForecastText = new Label();
            customerForecastText.Text = "• Customer Growth Prediction\n• Churn Rate Forecasting\n• Customer Lifetime Value\n• Market Share Prediction";
            customerForecastText.Location = new Point(10, 25);
            customerForecastText.Size = new Size(480, 150);
            customerForecastText.Font = new Font("Segoe UI", 9);
            customerForecastGroup.Controls.Add(customerForecastText);

            forecastingPanel.Controls.Add(customerForecastGroup);

            tabForecasting.Controls.Add(forecastingPanel);
        }

        private void SetupCustomDashboardTab()
        {
            var customPanel = new Panel();
            customPanel.Dock = DockStyle.Fill;
            customPanel.Padding = new Padding(10);

            var customLabel = new Label();
            customLabel.Text = "Custom Dashboard Builder";
            customLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            customLabel.Location = new Point(10, 10);
            customLabel.AutoSize = true;
            customPanel.Controls.Add(customLabel);

            // Dashboard Builder Features
            var builderGroup = new GroupBox();
            builderGroup.Text = "Dashboard Builder Features";
            builderGroup.Location = new Point(10, 50);
            builderGroup.Size = new Size(1000, 300);
            builderGroup.Font = new Font("Segoe UI", 10);

            var builderText = new Label();
            builderText.Text = "• Drag & Drop Widget Builder\n• Custom Chart Creation\n• Real-time Data Visualization\n• Personalized Dashboard Views\n• Export Dashboard as PDF/Image\n• Share Dashboards with Team\n• Scheduled Report Generation\n• Mobile-Responsive Design";
            builderText.Location = new Point(10, 25);
            builderText.Size = new Size(980, 250);
            builderText.Font = new Font("Segoe UI", 9);
            builderGroup.Controls.Add(builderText);

            customPanel.Controls.Add(builderGroup);

            // Sample Dashboard Templates
            var templatesGroup = new GroupBox();
            templatesGroup.Text = "Dashboard Templates";
            templatesGroup.Location = new Point(10, 360);
            templatesGroup.Size = new Size(1000, 200);
            templatesGroup.Font = new Font("Segoe UI", 10);

            var templatesText = new Label();
            templatesText.Text = "• Executive Dashboard\n• Sales Manager Dashboard\n• Operations Dashboard\n• Financial Dashboard\n• Marketing Dashboard\n• Customer Service Dashboard";
            templatesText.Location = new Point(10, 25);
            templatesText.Size = new Size(980, 150);
            templatesText.Font = new Font("Segoe UI", 9);
            templatesGroup.Controls.Add(templatesText);

            customPanel.Controls.Add(templatesGroup);

            tabCustomDashboard.Controls.Add(customPanel);
        }

        private Panel CreateMetricCard(string title, string value, Color color)
        {
            var card = new Panel();
            card.Size = new Size(200, 120);
            card.BackColor = color;
            card.Margin = new Padding(10);
            card.Padding = new Padding(10);

            var titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleLabel.Location = new Point(10, 10);
            titleLabel.AutoSize = true;
            card.Controls.Add(titleLabel);

            var valueLabel = new Label();
            valueLabel.Text = value;
            valueLabel.ForeColor = Color.White;
            valueLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            valueLabel.Location = new Point(10, 40);
            valueLabel.AutoSize = true;
            card.Controls.Add(valueLabel);

            return card;
        }

        private string GetSalesRevenue()
        {
            try
            {
                var sales = _salesInvoiceService.GetAllSalesInvoices();
                var total = sales.Sum(s => s.TotalAmount);
                return total.ToString("C");
            }
            catch
            {
                return "$0.00";
            }
        }

        private string GetPurchaseCost()
        {
            try
            {
                var purchases = _purchaseInvoiceService.GetAllPurchaseInvoices();
                var total = purchases.Sum(p => p.TotalAmount);
                return total.ToString("C");
            }
            catch
            {
                return "$0.00";
            }
        }

        private string GetCustomerCount()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();
                return customers.Count.ToString();
            }
            catch
            {
                return "0";
            }
        }

        private string GetProductCount()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return products.Count.ToString();
            }
            catch
            {
                return "0";
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

