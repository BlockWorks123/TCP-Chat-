namespace TCP_Client
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.NicknameLabel = new System.Windows.Forms.Label();
            this.NicknameEntry = new System.Windows.Forms.TextBox();
            this.MessageEntry = new System.Windows.Forms.TextBox();
            this.TerminalWindow = new System.Windows.Forms.RichTextBox();
            this.Send = new System.Windows.Forms.Button();
            this.PortEntry = new System.Windows.Forms.TextBox();
            this.HostEntry = new System.Windows.Forms.TextBox();
            this.Disconnect = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.PortLabel = new System.Windows.Forms.Label();
            this.HostLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordEntry = new System.Windows.Forms.TextBox();
            this.TabTerminal = new System.Windows.Forms.RichTextBox();
            this.LogoBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // NicknameLabel
            // 
            this.NicknameLabel.AutoSize = true;
            this.NicknameLabel.Location = new System.Drawing.Point(70, 11);
            this.NicknameLabel.Name = "NicknameLabel";
            this.NicknameLabel.Size = new System.Drawing.Size(58, 13);
            this.NicknameLabel.TabIndex = 29;
            this.NicknameLabel.Text = "Nickname:";
            // 
            // NicknameEntry
            // 
            this.NicknameEntry.Location = new System.Drawing.Point(134, 7);
            this.NicknameEntry.Name = "NicknameEntry";
            this.NicknameEntry.Size = new System.Drawing.Size(161, 20);
            this.NicknameEntry.TabIndex = 28;
            // 
            // MessageEntry
            // 
            this.MessageEntry.Location = new System.Drawing.Point(12, 433);
            this.MessageEntry.Name = "MessageEntry";
            this.MessageEntry.Size = new System.Drawing.Size(408, 20);
            this.MessageEntry.TabIndex = 27;
            this.MessageEntry.Text = " ";
            // 
            // TerminalWindow
            // 
            this.TerminalWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TerminalWindow.Location = new System.Drawing.Point(8, 60);
            this.TerminalWindow.Name = "TerminalWindow";
            this.TerminalWindow.ReadOnly = true;
            this.TerminalWindow.Size = new System.Drawing.Size(412, 369);
            this.TerminalWindow.TabIndex = 26;
            this.TerminalWindow.Text = "";
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(426, 432);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(84, 21);
            this.Send.TabIndex = 25;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Button_Send);
            // 
            // PortEntry
            // 
            this.PortEntry.Location = new System.Drawing.Point(253, 34);
            this.PortEntry.Name = "PortEntry";
            this.PortEntry.Size = new System.Drawing.Size(100, 20);
            this.PortEntry.TabIndex = 24;
            this.PortEntry.Text = "8000";
            // 
            // HostEntry
            // 
            this.HostEntry.Location = new System.Drawing.Point(111, 34);
            this.HostEntry.Name = "HostEntry";
            this.HostEntry.Size = new System.Drawing.Size(100, 20);
            this.HostEntry.TabIndex = 23;
            this.HostEntry.Text = "127.0.0.1";
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(435, 33);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(75, 23);
            this.Disconnect.TabIndex = 22;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Button_Disconnect);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(359, 33);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 22);
            this.Connect.TabIndex = 21;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Button_Connect);
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(218, 38);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 13);
            this.PortLabel.TabIndex = 20;
            this.PortLabel.Text = "Port:";
            // 
            // HostLabel
            // 
            this.HostLabel.AutoSize = true;
            this.HostLabel.Location = new System.Drawing.Point(70, 38);
            this.HostLabel.Name = "HostLabel";
            this.HostLabel.Size = new System.Drawing.Size(32, 13);
            this.HostLabel.TabIndex = 19;
            this.HostLabel.Text = "Host:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(297, 11);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.PasswordLabel.TabIndex = 31;
            this.PasswordLabel.Text = "Password:";
            // 
            // PasswordEntry
            // 
            this.PasswordEntry.Location = new System.Drawing.Point(359, 7);
            this.PasswordEntry.Name = "PasswordEntry";
            this.PasswordEntry.Size = new System.Drawing.Size(152, 20);
            this.PasswordEntry.TabIndex = 32;
            // 
            // TabTerminal
            // 
            this.TabTerminal.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.TabTerminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TabTerminal.Location = new System.Drawing.Point(426, 59);
            this.TabTerminal.Name = "TabTerminal";
            this.TabTerminal.ReadOnly = true;
            this.TabTerminal.Size = new System.Drawing.Size(84, 370);
            this.TabTerminal.TabIndex = 33;
            this.TabTerminal.Text = "";
            // 
            // LogoBox
            // 
            this.LogoBox.BackColor = System.Drawing.SystemColors.Control;
            this.LogoBox.Image = ((System.Drawing.Image)(resources.GetObject("LogoBox.Image")));
            this.LogoBox.Location = new System.Drawing.Point(12, 5);
            this.LogoBox.Name = "LogoBox";
            this.LogoBox.Size = new System.Drawing.Size(55, 50);
            this.LogoBox.TabIndex = 34;
            this.LogoBox.TabStop = false;
            this.LogoBox.Click += new System.EventHandler(this.About_Press);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 457);
            this.Controls.Add(this.LogoBox);
            this.Controls.Add(this.TabTerminal);
            this.Controls.Add(this.PasswordEntry);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.NicknameLabel);
            this.Controls.Add(this.NicknameEntry);
            this.Controls.Add(this.MessageEntry);
            this.Controls.Add(this.TerminalWindow);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.PortEntry);
            this.Controls.Add(this.HostEntry);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.HostLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label NicknameLabel;
        private System.Windows.Forms.TextBox NicknameEntry;
        private System.Windows.Forms.TextBox MessageEntry;
        private System.Windows.Forms.RichTextBox TerminalWindow;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.TextBox PortEntry;
        private System.Windows.Forms.TextBox HostEntry;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label HostLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PasswordEntry;
        private System.Windows.Forms.RichTextBox TabTerminal;
        private System.Windows.Forms.PictureBox LogoBox;
    }
}

