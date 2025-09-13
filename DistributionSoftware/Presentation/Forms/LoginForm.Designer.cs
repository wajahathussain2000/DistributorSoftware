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
            this.panelDivider = new System.Windows.Forms.Panel();
            this.lnkSignUp = new System.Windows.Forms.LinkLabel();
            this.lblSignUp = new System.Windows.Forms.Label();
            this.lnkForgotPassword = new System.Windows.Forms.LinkLabel();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.picPasswordIcon = new System.Windows.Forms.PictureBox();
            this.panelEmail = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.picEmailIcon = new System.Windows.Forms.PictureBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelLoginCard.SuspendLayout();
            this.panelPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPasswordIcon)).BeginInit();
            this.panelEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmailIcon)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelMain.Controls.Add(this.panelLoginCard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1924, 1050);
            this.panelMain.TabIndex = 0;
            // 
            // panelLoginCard
            // 
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
            this.panelLoginCard.Location = new System.Drawing.Point(662, 140);
            this.panelLoginCard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelLoginCard.Name = "panelLoginCard";
            this.panelLoginCard.Padding = new System.Windows.Forms.Padding(45, 46, 45, 46);
            this.panelLoginCard.Size = new System.Drawing.Size(600, 769);
            this.panelLoginCard.TabIndex = 0;
            // 
            // panelDivider
            // 
            this.panelDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelDivider.Location = new System.Drawing.Point(0, 569);
            this.panelDivider.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelDivider.Name = "panelDivider";
            this.panelDivider.Size = new System.Drawing.Size(510, 2);
            this.panelDivider.TabIndex = 6;
            // 
            // lnkSignUp
            // 
            this.lnkSignUp.AutoSize = true;
            this.lnkSignUp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkSignUp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lnkSignUp.Location = new System.Drawing.Point(225, 600);
            this.lnkSignUp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkSignUp.Name = "lnkSignUp";
            this.lnkSignUp.Size = new System.Drawing.Size(73, 25);
            this.lnkSignUp.TabIndex = 8;
            this.lnkSignUp.TabStop = true;
            this.lnkSignUp.Text = "Sign up";
            this.lnkSignUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSignUp_LinkClicked);
            // 
            // lblSignUp
            // 
            this.lblSignUp.AutoSize = true;
            this.lblSignUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSignUp.Location = new System.Drawing.Point(0, 600);
            this.lblSignUp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSignUp.Name = "lblSignUp";
            this.lblSignUp.Size = new System.Drawing.Size(177, 20);
            this.lblSignUp.TabIndex = 7;
            this.lblSignUp.Text = "Don\'t have an account?";
            // 
            // lnkForgotPassword
            // 
            this.lnkForgotPassword.AutoSize = true;
            this.lnkForgotPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkForgotPassword.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lnkForgotPassword.Location = new System.Drawing.Point(360, 508);
            this.lnkForgotPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkForgotPassword.Name = "lnkForgotPassword";
            this.lnkForgotPassword.Size = new System.Drawing.Size(156, 25);
            this.lnkForgotPassword.TabIndex = 4;
            this.lnkForgotPassword.TabStop = true;
            this.lnkForgotPassword.Text = "Forgot password?";
            this.lnkForgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForgotPassword_LinkClicked);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkRememberMe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkRememberMe.Location = new System.Drawing.Point(0, 508);
            this.chkRememberMe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(154, 29);
            this.chkRememberMe.TabIndex = 3;
            this.chkRememberMe.Text = "Remember me";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(0, 415);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(600, 69);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "SIGN IN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panelPassword
            // 
            this.panelPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelPassword.Controls.Add(this.txtPassword);
            this.panelPassword.Controls.Add(this.picPasswordIcon);
            this.panelPassword.Location = new System.Drawing.Point(0, 292);
            this.panelPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Padding = new System.Windows.Forms.Padding(22, 23, 22, 23);
            this.panelPassword.Size = new System.Drawing.Size(551, 92);
            this.panelPassword.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.Location = new System.Drawing.Point(75, 31);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(412, 30);
            this.txtPassword.TabIndex = 1;
            // 
            // picPasswordIcon
            // 
            this.picPasswordIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.picPasswordIcon.Location = new System.Drawing.Point(22, 28);
            this.picPasswordIcon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picPasswordIcon.Name = "picPasswordIcon";
            this.picPasswordIcon.Size = new System.Drawing.Size(36, 37);
            this.picPasswordIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPasswordIcon.TabIndex = 0;
            this.picPasswordIcon.TabStop = false;
            // 
            // panelEmail
            // 
            this.panelEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelEmail.Controls.Add(this.txtEmail);
            this.panelEmail.Controls.Add(this.picEmailIcon);
            this.panelEmail.Location = new System.Drawing.Point(0, 185);
            this.panelEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelEmail.Name = "panelEmail";
            this.panelEmail.Padding = new System.Windows.Forms.Padding(22, 23, 22, 23);
            this.panelEmail.Size = new System.Drawing.Size(551, 92);
            this.panelEmail.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmail.Location = new System.Drawing.Point(75, 31);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(412, 30);
            this.txtEmail.TabIndex = 0;
            // 
            // picEmailIcon
            // 
            this.picEmailIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.picEmailIcon.Location = new System.Drawing.Point(22, 28);
            this.picEmailIcon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picEmailIcon.Name = "picEmailIcon";
            this.picEmailIcon.Size = new System.Drawing.Size(36, 37);
            this.picEmailIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEmailIcon.TabIndex = 0;
            this.picEmailIcon.TabStop = false;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(45, 46);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(510, 123);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 77);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(263, 32);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Sign in to your account";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(237, 65);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1050);
            this.Controls.Add(this.panelMain);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Distribution Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelMain.ResumeLayout(false);
            this.panelLoginCard.ResumeLayout(false);
            this.panelLoginCard.PerformLayout();
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPasswordIcon)).EndInit();
            this.panelEmail.ResumeLayout(false);
            this.panelEmail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmailIcon)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
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
