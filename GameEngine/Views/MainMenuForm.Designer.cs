using System.Windows.Forms;

namespace GameEngine.Views
{
    partial class MainMenuForm
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
            this.HostButton = new System.Windows.Forms.Button();
            this.JoinButton = new System.Windows.Forms.Button();
            this.OptionsButton = new System.Windows.Forms.Button();
            this.ProfileButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HostButton
            // 
            this.HostButton.Location = new System.Drawing.Point(34, 19);
            this.HostButton.Name = "HostButton";
            this.HostButton.Size = new System.Drawing.Size(75, 23);
            this.HostButton.TabIndex = 0;
            this.HostButton.Text = "Host Game";
            this.HostButton.UseVisualStyleBackColor = true;
            this.HostButton.Click += new System.EventHandler(this.HostButton_Click);
            this.HostButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainMenuForm_KeyPressed);
            // 
            // JoinButton
            // 
            this.JoinButton.Location = new System.Drawing.Point(34, 48);
            this.JoinButton.Name = "JoinButton";
            this.JoinButton.Size = new System.Drawing.Size(75, 23);
            this.JoinButton.TabIndex = 1;
            this.JoinButton.Text = "Join Game";
            this.JoinButton.UseVisualStyleBackColor = true;
            this.JoinButton.Click += new System.EventHandler(this.JoinButton_Click);
            this.JoinButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainMenuForm_KeyPressed);
            // 
            // OptionsButton
            // 
            this.OptionsButton.Location = new System.Drawing.Point(34, 77);
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(75, 23);
            this.OptionsButton.TabIndex = 2;
            this.OptionsButton.Text = "Options";
            this.OptionsButton.UseVisualStyleBackColor = true;
            this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            this.OptionsButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainMenuForm_KeyPressed);
            // 
            // ProfileButton
            // 
            this.ProfileButton.Location = new System.Drawing.Point(34, 106);
            this.ProfileButton.Name = "ProfileButton";
            this.ProfileButton.Size = new System.Drawing.Size(75, 23);
            this.ProfileButton.TabIndex = 3;
            this.ProfileButton.Text = "View Profile";
            this.ProfileButton.UseVisualStyleBackColor = true;
            this.ProfileButton.Click += new System.EventHandler(this.ProfileButton_Click);
            this.ProfileButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainMenuForm_KeyPressed);
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(34, 135);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 4;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            this.logoutButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainMenuForm_KeyPressed);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.HostButton);
            this.groupBox1.Controls.Add(this.logoutButton);
            this.groupBox1.Controls.Add(this.JoinButton);
            this.groupBox1.Controls.Add(this.ProfileButton);
            this.groupBox1.Controls.Add(this.OptionsButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 185);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainMenuForm_KeyPressed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Press \'f\' for friends list";
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(165, 215);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "King of Ames";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenuForm_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainMenuForm_KeyPressed);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HostButton;
        private System.Windows.Forms.Button JoinButton;
        private System.Windows.Forms.Button OptionsButton;
        private System.Windows.Forms.Button ProfileButton;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private Label label1;
    }
}