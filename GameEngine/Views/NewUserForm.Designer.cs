using System.Windows.Forms;

namespace GameEngine.Views
{
    partial class NewUserForm
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
            this.newUsername = new System.Windows.Forms.TextBox();
            this.newPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.newUserButton = new System.Windows.Forms.Button();
            this.toLogin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newUsername
            // 
            this.newUsername.Location = new System.Drawing.Point(170, 61);
            this.newUsername.Name = "newUsername";
            this.newUsername.Size = new System.Drawing.Size(100, 20);
            this.newUsername.TabIndex = 0;
            this.newUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUserForm_KeyPressed);
            // 
            // newPassword
            // 
            this.newPassword.Location = new System.Drawing.Point(170, 109);
            this.newPassword.Name = "newPassword";
            this.newPassword.PasswordChar = '*';
            this.newPassword.Size = new System.Drawing.Size(100, 20);
            this.newPassword.TabIndex = 1;
            this.newPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUserForm_KeyPressed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // newUserButton
            // 
            this.newUserButton.Location = new System.Drawing.Point(181, 153);
            this.newUserButton.Name = "newUserButton";
            this.newUserButton.Size = new System.Drawing.Size(104, 23);
            this.newUserButton.TabIndex = 5;
            this.newUserButton.Text = "Create Profile";
            this.newUserButton.UseVisualStyleBackColor = false;
            this.newUserButton.Click += new System.EventHandler(this.newUserButton_Click);
            // 
            // toLogin
            // 
            this.toLogin.Location = new System.Drawing.Point(71, 153);
            this.toLogin.Name = "toLogin";
            this.toLogin.Size = new System.Drawing.Size(104, 23);
            this.toLogin.TabIndex = 6;
            this.toLogin.Text = "Back to Login";
            this.toLogin.UseVisualStyleBackColor = false;
            this.toLogin.Click += new System.EventHandler(this.toLogin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(300, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Username and password can only contain letters and numbers";
            // 
            // NewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(350, 262);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toLogin);
            this.Controls.Add(this.newUserButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPassword);
            this.Controls.Add(this.newUsername);
            this.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Name = "NewUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New User";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewUserForm_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUserForm_KeyPressed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox newUsername;
        private System.Windows.Forms.TextBox newPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button newUserButton;
        private System.Windows.Forms.Button toLogin;
        private System.Windows.Forms.Label label3;
    }
}