﻿namespace BenpilsBarcodeSystem
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.CloseCb = new System.Windows.Forms.CheckBox();
            this.MinimizeCb = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PasswordTxt = new System.Windows.Forms.TextBox();
            this.ShowPassword = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.PasswordPanel = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.testPrint = new System.Drawing.Printing.PrintDocument();
            this.testPrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UsernameBorder = new System.Windows.Forms.Panel();
            this.UsernamePanel = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.UsernameTxt = new System.Windows.Forms.TextBox();
            this.PasswordBorder = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.PasswordPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.UsernameBorder.SuspendLayout();
            this.UsernamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.PasswordBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panelHeader.Controls.Add(this.flowLayoutPanel1);
            this.panelHeader.Controls.Add(this.label3);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(287, 38);
            this.panelHeader.TabIndex = 12;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDown);
            this.panelHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseMove);
            this.panelHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseUp);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.CloseCb);
            this.flowLayoutPanel1.Controls.Add(this.MinimizeCb);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(218, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(69, 38);
            this.flowLayoutPanel1.TabIndex = 28;
            // 
            // CloseCb
            // 
            this.CloseCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.CloseCb.AutoCheck = false;
            this.CloseCb.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_close_30;
            this.CloseCb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseCb.FlatAppearance.BorderSize = 0;
            this.CloseCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.CloseCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.CloseCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseCb.Location = new System.Drawing.Point(41, 8);
            this.CloseCb.Name = "CloseCb";
            this.CloseCb.Padding = new System.Windows.Forms.Padding(5);
            this.CloseCb.Size = new System.Drawing.Size(20, 20);
            this.CloseCb.TabIndex = 26;
            this.CloseCb.TabStop = false;
            this.CloseCb.UseVisualStyleBackColor = true;
            this.CloseCb.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // MinimizeCb
            // 
            this.MinimizeCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.MinimizeCb.AutoCheck = false;
            this.MinimizeCb.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_subtract_30;
            this.MinimizeCb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MinimizeCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimizeCb.FlatAppearance.BorderSize = 0;
            this.MinimizeCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MinimizeCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.MinimizeCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeCb.Location = new System.Drawing.Point(15, 8);
            this.MinimizeCb.Name = "MinimizeCb";
            this.MinimizeCb.Padding = new System.Windows.Forms.Padding(5);
            this.MinimizeCb.Size = new System.Drawing.Size(20, 20);
            this.MinimizeCb.TabIndex = 24;
            this.MinimizeCb.TabStop = false;
            this.MinimizeCb.UseVisualStyleBackColor = true;
            this.MinimizeCb.Click += new System.EventHandler(this.minimizeBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Login";
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordTxt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTxt.Location = new System.Drawing.Point(35, 11);
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.Size = new System.Drawing.Size(175, 14);
            this.PasswordTxt.TabIndex = 2;
            this.PasswordTxt.TextChanged += new System.EventHandler(this.PasswordTxt_TextChanged);
            this.PasswordTxt.Enter += new System.EventHandler(this.PasswordTxt_Enter);
            this.PasswordTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PasswordTxt_KeyPress);
            this.PasswordTxt.Leave += new System.EventHandler(this.PasswordTxt_Leave);
            // 
            // ShowPassword
            // 
            this.ShowPassword.AutoSize = true;
            this.ShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowPassword.ForeColor = System.Drawing.Color.White;
            this.ShowPassword.Location = new System.Drawing.Point(38, 241);
            this.ShowPassword.Name = "ShowPassword";
            this.ShowPassword.Size = new System.Drawing.Size(105, 18);
            this.ShowPassword.TabIndex = 3;
            this.ShowPassword.TabStop = false;
            this.ShowPassword.Text = "Show Password";
            this.ShowPassword.UseVisualStyleBackColor = true;
            this.ShowPassword.CheckedChanged += new System.EventHandler(this.Showpassword_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Enabled = false;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(38, 280);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(219, 40);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.TabStop = false;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // PasswordPanel
            // 
            this.PasswordPanel.BackColor = System.Drawing.Color.White;
            this.PasswordPanel.Controls.Add(this.pictureBox5);
            this.PasswordPanel.Controls.Add(this.PasswordTxt);
            this.PasswordPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PasswordPanel.Location = new System.Drawing.Point(1, 1);
            this.PasswordPanel.Name = "PasswordPanel";
            this.PasswordPanel.Size = new System.Drawing.Size(218, 38);
            this.PasswordPanel.TabIndex = 24;
            this.PasswordPanel.Click += new System.EventHandler(this.PasswordPanel_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.White;
            this.pictureBox5.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_password_30;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(8, 7);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(20, 20);
            this.pictureBox5.TabIndex = 20;
            this.pictureBox5.TabStop = false;
            // 
            // testPrint
            // 
            this.testPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.testPrint_PrintPage);
            // 
            // testPrintPreview
            // 
            this.testPrintPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.testPrintPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.testPrintPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.testPrintPreview.Enabled = true;
            this.testPrintPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("testPrintPreview.Icon")));
            this.testPrintPreview.Name = "testPrintPreview";
            this.testPrintPreview.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.xconvert_com;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(59, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 77);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // UsernameBorder
            // 
            this.UsernameBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.UsernameBorder.Controls.Add(this.UsernamePanel);
            this.UsernameBorder.Location = new System.Drawing.Point(37, 148);
            this.UsernameBorder.Name = "UsernameBorder";
            this.UsernameBorder.Padding = new System.Windows.Forms.Padding(1);
            this.UsernameBorder.Size = new System.Drawing.Size(220, 40);
            this.UsernameBorder.TabIndex = 25;
            // 
            // UsernamePanel
            // 
            this.UsernamePanel.BackColor = System.Drawing.Color.White;
            this.UsernamePanel.Controls.Add(this.pictureBox4);
            this.UsernamePanel.Controls.Add(this.UsernameTxt);
            this.UsernamePanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UsernamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsernamePanel.Location = new System.Drawing.Point(1, 1);
            this.UsernamePanel.Name = "UsernamePanel";
            this.UsernamePanel.Size = new System.Drawing.Size(218, 38);
            this.UsernamePanel.TabIndex = 24;
            this.UsernamePanel.Click += new System.EventHandler(this.UsernamePanel_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.White;
            this.pictureBox4.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_user_30__2_;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(8, 8);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 20);
            this.pictureBox4.TabIndex = 19;
            this.pictureBox4.TabStop = false;
            // 
            // UsernameTxt
            // 
            this.UsernameTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UsernameTxt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTxt.Location = new System.Drawing.Point(35, 12);
            this.UsernameTxt.Name = "UsernameTxt";
            this.UsernameTxt.Size = new System.Drawing.Size(175, 14);
            this.UsernameTxt.TabIndex = 1;
            this.UsernameTxt.Enter += new System.EventHandler(this.UsernameTxt_Enter);
            this.UsernameTxt.Leave += new System.EventHandler(this.UsernameTxt_Leave);
            // 
            // PasswordBorder
            // 
            this.PasswordBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.PasswordBorder.Controls.Add(this.PasswordPanel);
            this.PasswordBorder.Location = new System.Drawing.Point(37, 194);
            this.PasswordBorder.Name = "PasswordBorder";
            this.PasswordBorder.Padding = new System.Windows.Forms.Padding(1);
            this.PasswordBorder.Size = new System.Drawing.Size(220, 40);
            this.PasswordBorder.TabIndex = 26;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(288, 349);
            this.Controls.Add(this.PasswordBorder);
            this.Controls.Add(this.UsernameBorder);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.ShowPassword);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.PasswordPanel.ResumeLayout(false);
            this.PasswordPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.UsernameBorder.ResumeLayout(false);
            this.UsernamePanel.ResumeLayout(false);
            this.UsernamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.PasswordBorder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.TextBox PasswordTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ShowPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel PasswordPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Drawing.Printing.PrintDocument testPrint;
        private System.Windows.Forms.PrintPreviewDialog testPrintPreview;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox CloseCb;
        private System.Windows.Forms.CheckBox MinimizeCb;
        private System.Windows.Forms.Panel UsernameBorder;
        private System.Windows.Forms.Panel UsernamePanel;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox UsernameTxt;
        private System.Windows.Forms.Panel PasswordBorder;
    }
}

