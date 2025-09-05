namespace DistributionSoftware.Presentation.Forms
{
    partial class LoginForm
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelLoginCard = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.panelEmail = new System.Windows.Forms.Panel();
            this.picEmailIcon = new System.Windows.Forms.PictureBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.picPasswordIcon = new System.Windows.Forms.PictureBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.lnkForgotPassword = new System.Windows.Forms.LinkLabel();
            this.panelDivider = new System.Windows.Forms.Panel();

            this.lblSignUp = new System.Windows.Forms.Label();
            this.lnkSignUp = new System.Windows.Forms.LinkLabel();
            this.panelMain.SuspendLayout();
            this.panelLoginCard.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmailIcon)).BeginInit();
            this.panelPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPasswordIcon)).BeginInit();
            this.SuspendLayout();
            
            // panelMain - Light blue background
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.panelMain.Controls.Add(this.panelLoginCard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1920, 1080);
            this.panelMain.TabIndex = 0;
            
            // panelLoginCard - White card with shadow effect
            this.panelLoginCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelLoginCard.BackColor = System.Drawing.Color.White;
                         this.panelLoginCard.Controls.Add(this.panelDivider);
            this.panelLoginCard.Controls.Add(this.lnkSignUp);
            this.panelLoginCard.Controls.Add(this.lblSignUp);
            this.panelLoginCard.Controls.Add(this.lnkForgotPassword);
            this.panelLoginCard.Controls.Add(this.chkRememberMe);
            this.panelLoginCard.Controls.Add(this.btnLogin);
            this.panelLoginCard.Controls.Add(this.panelPassword);
            this.panelLoginCard.Controls.Add(this.panelEmail);
            this.panelLoginCard.Controls.Add(this.panelHeader);
            this.panelLoginCard.Location = new System.Drawing.Point(760, 290);
            this.panelLoginCard.Name = "panelLoginCard";
            this.panelLoginCard.Padding = new System.Windows.Forms.Padding(30);
            this.panelLoginCard.Size = new System.Drawing.Size(400, 500);
            this.panelLoginCard.TabIndex = 0;
            
            // panelHeader - Title section
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(30, 30);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(340, 80);
            this.panelHeader.TabIndex = 0;
            
            // lblTitle - Main title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome";
            
            // lblSubtitle - Subtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblSubtitle.Location = new System.Drawing.Point(0, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(200, 21);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Sign in to your account";
            
                         // panelEmail - Email input with icon
             this.panelEmail.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
             this.panelEmail.Controls.Add(this.txtEmail);
             this.panelEmail.Controls.Add(this.picEmailIcon);
             this.panelEmail.Location = new System.Drawing.Point(0, 120);
             this.panelEmail.Name = "panelEmail";
             this.panelEmail.Padding = new System.Windows.Forms.Padding(15);
             this.panelEmail.Size = new System.Drawing.Size(340, 60);
             this.panelEmail.TabIndex = 1;
            
            // picEmailIcon - Email icon
            this.picEmailIcon.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.picEmailIcon.Location = new System.Drawing.Point(15, 18);
            this.picEmailIcon.Name = "picEmailIcon";
            this.picEmailIcon.Size = new System.Drawing.Size(24, 24);
            this.picEmailIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEmailIcon.TabIndex = 0;
            this.picEmailIcon.TabStop = false;
            
            // txtEmail - Email textbox
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmail.Location = new System.Drawing.Point(50, 20);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(275, 20);
            this.txtEmail.TabIndex = 0;
            
                         // panelPassword - Password input with icon
             this.panelPassword.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
             this.panelPassword.Controls.Add(this.txtPassword);
             this.panelPassword.Controls.Add(this.picPasswordIcon);
             this.panelPassword.Location = new System.Drawing.Point(0, 190);
             this.panelPassword.Name = "panelPassword";
             this.panelPassword.Padding = new System.Windows.Forms.Padding(15);
             this.panelPassword.Size = new System.Drawing.Size(340, 60);
             this.panelPassword.TabIndex = 2;
            
            // picPasswordIcon - Password icon
            this.picPasswordIcon.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.picPasswordIcon.Location = new System.Drawing.Point(15, 18);
            this.picPasswordIcon.Name = "picPasswordIcon";
            this.picPasswordIcon.Size = new System.Drawing.Size(24, 24);
            this.picPasswordIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPasswordIcon.TabIndex = 0;
            this.picPasswordIcon.TabStop = false;
            
            // txtPassword - Password textbox
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.Location = new System.Drawing.Point(50, 20);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(275, 20);
            this.txtPassword.TabIndex = 1;
            
                         // btnLogin - Login button
             this.btnLogin.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
             this.btnLogin.FlatAppearance.BorderSize = 0;
             this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
             this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
             this.btnLogin.ForeColor = System.Drawing.Color.White;
             this.btnLogin.Location = new System.Drawing.Point(0, 270);
             this.btnLogin.Name = "btnLogin";
             this.btnLogin.Size = new System.Drawing.Size(340, 45);
             this.btnLogin.TabIndex = 2;
             this.btnLogin.Text = "SIGN IN";
             this.btnLogin.UseVisualStyleBackColor = false;
             this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            
                         // chkRememberMe - Remember me checkbox
             this.chkRememberMe.AutoSize = true;
             this.chkRememberMe.Font = new System.Drawing.Font("Segoe UI", 9F);
             this.chkRememberMe.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
             this.chkRememberMe.Location = new System.Drawing.Point(0, 330);
             this.chkRememberMe.Name = "chkRememberMe";
             this.chkRememberMe.Size = new System.Drawing.Size(100, 19);
             this.chkRememberMe.TabIndex = 3;
             this.chkRememberMe.Text = "Remember me";
             this.chkRememberMe.UseVisualStyleBackColor = true;
             
             // lnkForgotPassword - Forgot password link
             this.lnkForgotPassword.AutoSize = true;
             this.lnkForgotPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
             this.lnkForgotPassword.LinkColor = System.Drawing.Color.FromArgb(70, 130, 180);
             this.lnkForgotPassword.Location = new System.Drawing.Point(240, 330);
             this.lnkForgotPassword.Name = "lnkForgotPassword";
             this.lnkForgotPassword.Size = new System.Drawing.Size(100, 15);
             this.lnkForgotPassword.TabIndex = 4;
             this.lnkForgotPassword.TabStop = true;
             this.lnkForgotPassword.Text = "Forgot password?";
             this.lnkForgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForgotPassword_LinkClicked);
            
                                      // panelDivider - Visual separator
             this.panelDivider.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
             this.panelDivider.Location = new System.Drawing.Point(0, 370);
             this.panelDivider.Name = "panelDivider";
             this.panelDivider.Size = new System.Drawing.Size(340, 1);
             this.panelDivider.TabIndex = 6;
             
             // lblSignUp - Sign up text
             this.lblSignUp.AutoSize = true;
             this.lnkSignUp.Font = new System.Drawing.Font("Segoe UI", 9F);
             this.lblSignUp.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
             this.lblSignUp.Location = new System.Drawing.Point(0, 390);
             this.lblSignUp.Name = "lblSignUp";
             this.lblSignUp.Size = new System.Drawing.Size(150, 15);
             this.lblSignUp.TabIndex = 7;
             this.lblSignUp.Text = "Don\'t have an account?";
             
             // lnkSignUp - Sign up link
             this.lnkSignUp.AutoSize = true;
             this.lnkSignUp.Font = new System.Drawing.Font("Segoe UI", 9F);
             this.lnkSignUp.LinkColor = System.Drawing.Color.FromArgb(70, 130, 180);
             this.lnkSignUp.Location = new System.Drawing.Point(150, 390);
             this.lnkSignUp.Name = "lnkSignUp";
             this.lnkSignUp.Size = new System.Drawing.Size(50, 15);
             this.lnkSignUp.TabIndex = 8;
             this.lnkSignUp.TabStop = true;
             this.lnkSignUp.Text = "Sign up";
             this.lnkSignUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSignUp_LinkClicked);
            
            // Form properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Distribution Software";
            
            // Resume layout
            this.panelMain.ResumeLayout(false);
            this.panelLoginCard.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelEmail.ResumeLayout(false);
            this.panelEmail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmailIcon)).EndInit();
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPasswordIcon)).EndInit();
            this.ResumeLayout(false);
        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelLoginCard;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelEmail;
        private System.Windows.Forms.PictureBox picEmailIcon;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.PictureBox picPasswordIcon;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.LinkLabel lnkForgotPassword;
                 private System.Windows.Forms.Panel panelDivider;
         private System.Windows.Forms.Label lblSignUp;
        private System.Windows.Forms.LinkLabel lnkSignUp;

        #endregion
    }
}
