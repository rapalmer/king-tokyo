namespace Views
{
    partial class PlayerLobby
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
            this.leaveGame = new System.Windows.Forms.Button();
            this.playerList = new System.Windows.Forms.ListView();
            this.playerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.character = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ping = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.char_list = new System.Windows.Forms.ComboBox();
            this.select_char = new System.Windows.Forms.Button();
            this.char_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // leaveGame
            // 
            this.leaveGame.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.leaveGame.Location = new System.Drawing.Point(160, 239);
            this.leaveGame.Name = "leaveGame";
            this.leaveGame.Size = new System.Drawing.Size(112, 23);
            this.leaveGame.TabIndex = 4;
            this.leaveGame.Text = "Leave Game";
            this.leaveGame.UseVisualStyleBackColor = true;
            this.leaveGame.Click += new System.EventHandler(this.leaveGame_Click);
            // 
            // playerList
            // 
            this.playerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.playerName,
            this.character,
            this.ping});
            this.playerList.FullRowSelect = true;
            this.playerList.GridLines = true;
            this.playerList.Location = new System.Drawing.Point(12, 12);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(260, 194);
            this.playerList.TabIndex = 3;
            this.playerList.UseCompatibleStateImageBehavior = false;
            this.playerList.View = System.Windows.Forms.View.Details;
            // 
            // playerName
            // 
            this.playerName.Text = "Player Name";
            // 
            // character
            // 
            this.character.Text = "Character";
            // 
            // ping
            // 
            this.ping.Text = "Ping";
            // 
            // char_list
            // 
            this.char_list.FormattingEnabled = true;
            this.char_list.Items.AddRange(new object[] {
            "Alienoid",
            "Cyber Bunny",
            "Giga Zaur",
            "Kraken",
            "Mecha Dragon",
            "The King"});
            this.char_list.Location = new System.Drawing.Point(88, 212);
            this.char_list.Name = "char_list";
            this.char_list.Size = new System.Drawing.Size(131, 21);
            this.char_list.TabIndex = 5;
            // 
            // select_char
            // 
            this.select_char.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.select_char.Location = new System.Drawing.Point(12, 239);
            this.select_char.Name = "select_char";
            this.select_char.Size = new System.Drawing.Size(112, 23);
            this.select_char.TabIndex = 6;
            this.select_char.Text = "Select Character";
            this.select_char.UseVisualStyleBackColor = true;
            this.select_char.Click += new System.EventHandler(this.select_char_Click);
            // 
            // char_label
            // 
            this.char_label.AutoSize = true;
            this.char_label.Location = new System.Drawing.Point(26, 215);
            this.char_label.Name = "char_label";
            this.char_label.Size = new System.Drawing.Size(56, 13);
            this.char_label.TabIndex = 7;
            this.char_label.Text = "Character:";
            // 
            // PlayerLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 272);
            this.Controls.Add(this.char_label);
            this.Controls.Add(this.select_char);
            this.Controls.Add(this.char_list);
            this.Controls.Add(this.leaveGame);
            this.Controls.Add(this.playerList);
            this.Name = "PlayerLobby";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlayerLobby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerLobby_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button leaveGame;
        private System.Windows.Forms.ListView playerList;
        private System.Windows.Forms.ColumnHeader playerName;
        private System.Windows.Forms.ColumnHeader character;
        private System.Windows.Forms.ComboBox char_list;
        private System.Windows.Forms.Button select_char;
        private System.Windows.Forms.Label char_label;
        private System.Windows.Forms.ColumnHeader ping;
    }
}