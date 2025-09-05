using System;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Common;
using DistributionSoftware.Business;

namespace DistributionSoftware.Presentation.Forms
{
    public abstract class BaseDashboard : Form
    {
        protected Panel navigationPanel;
        protected Panel greetingPanel;
        protected Panel contentPanel;
        protected Label userInfoLabel;
        protected Label greetingLabel;
        protected Timer sessionTimer;
        protected IUserService userService;

        public BaseDashboard()
        {
            InitializeBaseComponents();
            SetupBaseLayout();
            InitializeSessionTimer();
        }

        private void InitializeBaseComponents()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Text = "Distribution Software - Dashboard";

            userService = new UserService(new DataAccess.UserRepository(ConfigurationManager.DistributionConnectionString));
        }

        private void SetupBaseLayout()
        {
            // Navigation Panel (Top)
            navigationPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            // Greeting Panel (Below Navigation)
            greetingPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 15)
            };

            // Content Panel (Remaining Space)
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20)
            };

            // Add panels to form
            this.Controls.Add(contentPanel);
            this.Controls.Add(greetingPanel);
            this.Controls.Add(navigationPanel);

            SetupGreetingSection();
        }

        private void SetupGreetingSection()
        {
            // User Info Label
            userInfoLabel = new Label
            {
                Text = $"Welcome, {UserSession.GetDisplayName()}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Greeting Label
            greetingLabel = new Label
            {
                Text = GetDynamicGreeting(),
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(108, 117, 125),
                AutoSize = true,
                Location = new Point(20, 50)
            };

            greetingPanel.Controls.Add(greetingLabel);
            greetingPanel.Controls.Add(userInfoLabel);
        }

        private string GetDynamicGreeting()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 12)
                return "Good Morning! Have a productive day ahead.";
            else if (hour < 17)
                return "Good Afternoon! Hope your day is going well.";
            else
                return "Good Evening! Wrapping up for the day?";
        }

        private void InitializeSessionTimer()
        {
            sessionTimer = new Timer
            {
                Interval = 30000 // 30 seconds
            };
            sessionTimer.Tick += SessionTimer_Tick;
            sessionTimer.Start();
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            // Update greeting every 30 seconds
            greetingLabel.Text = GetDynamicGreeting();
        }

        protected abstract void CreateNavigationMenu();
        protected abstract void CreateDashboardContent();

        protected Button CreateNavButton(string text, string icon, EventHandler clickHandler)
        {
            Button button = new Button
            {
                Text = $"  {icon}  {text}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Height = 40,
                Width = 150,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(44, 62, 80);
            button.Click += clickHandler;

            return button;
        }

        protected Panel CreateChartPanel(string title, Color color)
        {
            Panel panel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(15),
                Margin = new Padding(10)
            };

            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = color,
                AutoSize = true,
                Location = new Point(0, 0)
            };

            panel.Controls.Add(titleLabel);
            return panel;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            sessionTimer?.Stop();
            sessionTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
