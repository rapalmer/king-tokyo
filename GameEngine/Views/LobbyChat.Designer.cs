using System.Windows.Forms;

namespace GameEngine.Views
{
    partial class LobbyChat
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.username = new System.Windows.Forms.Label();
            this.clearChat = new System.Windows.Forms.Button();
            this.sendMessage = new System.Windows.Forms.Button();
            this.writeMessage = new System.Windows.Forms.RichTextBox();
            this.Chat = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.username);
            this.groupBox1.Controls.Add(this.clearChat);
            this.groupBox1.Controls.Add(this.sendMessage);
            this.groupBox1.Controls.Add(this.writeMessage);
            this.groupBox1.Controls.Add(this.Chat);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 259);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chat";
            this.groupBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LobbyChat_KeyPressed);
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.BackColor = System.Drawing.Color.Chocolate;
            this.username.Location = new System.Drawing.Point(6, 212);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(55, 13);
            this.username.TabIndex = 3;
            this.username.Text = "Username";
            this.username.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LobbyChat_KeyPressed);
            // 
            // clearChat
            // 
            this.clearChat.Location = new System.Drawing.Point(144, 232);
            this.clearChat.Name = "clearChat";
            this.clearChat.Size = new System.Drawing.Size(68, 23);
            this.clearChat.TabIndex = 2;
            this.clearChat.Text = "Clear";
            this.clearChat.UseVisualStyleBackColor = true;
            this.clearChat.Click += new System.EventHandler(this.clearChat_Click);
            this.clearChat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LobbyChat_KeyPressed);
            // 
            // sendMessage
            // 
            this.sendMessage.Location = new System.Drawing.Point(42, 232);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(68, 23);
            this.sendMessage.TabIndex = 1;
            this.sendMessage.Text = "Send";
            this.sendMessage.UseVisualStyleBackColor = true;
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click);
            this.sendMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LobbyChat_KeyPressed);
            // 
            // writeMessage
            // 
            this.writeMessage.Location = new System.Drawing.Point(67, 209);
            this.writeMessage.Multiline = false;
            this.writeMessage.Name = "writeMessage";
            this.writeMessage.Size = new System.Drawing.Size(187, 21);
            this.writeMessage.TabIndex = 1;
            this.writeMessage.Text = "";
            this.writeMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.wrtieMessage_KeyPressed);
            // 
            // Chat
            // 
            this.Chat.Location = new System.Drawing.Point(6, 19);
            this.Chat.Name = "Chat";
            this.Chat.ReadOnly = true;
            this.Chat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Chat.Size = new System.Drawing.Size(248, 184);
            this.Chat.TabIndex = 0;
            this.Chat.Text = "";
            this.Chat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LobbyChat_KeyPressed);
            // 
            // LobbyChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(284, 279);
            this.Controls.Add(this.groupBox1);
            this.Name = "LobbyChat";
            this.Text = "LobbyChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LobbyChat_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LobbyChat_KeyPressed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.RichTextBox writeMessage;
        private System.Windows.Forms.RichTextBox Chat;
        private System.Windows.Forms.Button clearChat;
        private System.Windows.Forms.Label username;
    }
}