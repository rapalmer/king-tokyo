namespace GameEngine.Views
{
    partial class ServerListForm
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
            this.mainMenu = new System.Windows.Forms.Button();
            this.join = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            this.serverList = new System.Windows.Forms.ListView();
            this.hostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.players = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.spectateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mainMenu.Location = new System.Drawing.Point(207, 203);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(112, 23);
            this.mainMenu.TabIndex = 3;
            this.mainMenu.Text = "Main Menu";
            this.mainMenu.UseVisualStyleBackColor = true;
            this.mainMenu.Click += new System.EventHandler(this.mainMenu_Click);
            // 
            // join
            // 
            this.join.BackColor = System.Drawing.Color.DarkGray;
            this.join.Enabled = false;
            this.join.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.join.Location = new System.Drawing.Point(12, 233);
            this.join.Name = "join";
            this.join.Size = new System.Drawing.Size(152, 28);
            this.join.TabIndex = 5;
            this.join.Text = "Join Game";
            this.join.UseVisualStyleBackColor = false;
            this.join.Click += new System.EventHandler(this.join_Click);
            // 
            // refresh
            // 
            this.refresh.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.refresh.Location = new System.Drawing.Point(12, 203);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(112, 23);
            this.refresh.TabIndex = 7;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // serverList
            // 
            this.serverList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hostName,
            this.ip,
            this.players,
            this.status});
            this.serverList.FullRowSelect = true;
            this.serverList.GridLines = true;
            this.serverList.Location = new System.Drawing.Point(12, 32);
            this.serverList.MultiSelect = false;
            this.serverList.Name = "serverList";
            this.serverList.Size = new System.Drawing.Size(307, 165);
            this.serverList.TabIndex = 8;
            this.serverList.UseCompatibleStateImageBehavior = false;
            this.serverList.View = System.Windows.Forms.View.Details;
            this.serverList.SelectedIndexChanged += new System.EventHandler(this.serverList_SelectedIndexChanged);
            // 
            // hostName
            // 
            this.hostName.Text = "Host";
            this.hostName.Width = 80;
            // 
            // ip
            // 
            this.ip.Text = "IP";
            this.ip.Width = 83;
            // 
            // players
            // 
            this.players.Text = "Players";
            this.players.Width = 68;
            // 
            // status
            // 
            this.status.Text = "Status";
            this.status.Width = 112;
            // 
            // spectateButton
            // 
            this.spectateButton.BackColor = System.Drawing.Color.DarkGray;
            this.spectateButton.Enabled = false;
            this.spectateButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.spectateButton.Location = new System.Drawing.Point(167, 233);
            this.spectateButton.Name = "spectateButton";
            this.spectateButton.Size = new System.Drawing.Size(152, 28);
            this.spectateButton.TabIndex = 9;
            this.spectateButton.Text = "Spectate Game";
            this.spectateButton.UseVisualStyleBackColor = false;
            this.spectateButton.Click += new System.EventHandler(this.spectateButton_Click);
            // 
            // ServerListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(331, 273);
            this.Controls.Add(this.spectateButton);
            this.Controls.Add(this.serverList);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.join);
            this.Controls.Add(this.mainMenu);
            this.Name = "ServerListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServerList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerListForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button mainMenu;
        private System.Windows.Forms.Button join;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.ListView serverList;
        private System.Windows.Forms.ColumnHeader hostName;
        private System.Windows.Forms.ColumnHeader ip;
        private System.Windows.Forms.ColumnHeader players;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.Button spectateButton;
    }
}