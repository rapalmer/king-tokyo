using System;
using System.Windows.Forms;

namespace GameEngine.Views
{
    partial class FriendsList
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BoxOFriends = new System.Windows.Forms.ListView();
            this.playerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cm1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFriend = new System.Windows.Forms.Button();
            this.delFriend = new System.Windows.Forms.Button();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.groupBox1.SuspendLayout();
            this.cm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BoxOFriends);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Friends";
            this.groupBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FriendsList_KeyPressed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Right-Click friends for more options";
            // 
            // BoxOFriends
            // 
            this.BoxOFriends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.playerName,
            this.status});
            this.BoxOFriends.ContextMenuStrip = this.cm1;
            this.BoxOFriends.FullRowSelect = true;
            this.BoxOFriends.GridLines = true;
            this.BoxOFriends.Location = new System.Drawing.Point(15, 19);
            this.BoxOFriends.Name = "BoxOFriends";
            this.BoxOFriends.Size = new System.Drawing.Size(232, 202);
            this.BoxOFriends.TabIndex = 1;
            this.BoxOFriends.UseCompatibleStateImageBehavior = false;
            this.BoxOFriends.View = System.Windows.Forms.View.Details;
            this.BoxOFriends.SelectedIndexChanged += new System.EventHandler(this.BoxOFriends_SelectedIndexChanged);
            this.BoxOFriends.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FriendsList_KeyPressed);
            // 
            // playerName
            // 
            this.playerName.Text = "Player Name";
            this.playerName.Width = 89;
            // 
            // status
            // 
            this.status.Text = "Status";
            this.status.Width = 120;
            // 
            // cm1
            // 
            this.cm1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.viewProfileToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.joinGameToolStripMenuItem,
            this.spectateToolStripMenuItem});
            this.cm1.Name = "cm1";
            this.cm1.ShowImageMargin = false;
            this.cm1.Size = new System.Drawing.Size(112, 114);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.addToolStripMenuItem.Text = "Add Friend";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // viewProfileToolStripMenuItem
            // 
            this.viewProfileToolStripMenuItem.Name = "viewProfileToolStripMenuItem";
            this.viewProfileToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.viewProfileToolStripMenuItem.Text = "View Profile";
            this.viewProfileToolStripMenuItem.Click += new System.EventHandler(this.viewProfileToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // joinGameToolStripMenuItem
            // 
            this.joinGameToolStripMenuItem.Name = "joinGameToolStripMenuItem";
            this.joinGameToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.joinGameToolStripMenuItem.Text = "Join Game";
            this.joinGameToolStripMenuItem.Click += new System.EventHandler(this.joinGameToolStripMenuItem_Click);
            // 
            // spectateToolStripMenuItem
            // 
            this.spectateToolStripMenuItem.Name = "spectateToolStripMenuItem";
            this.spectateToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.spectateToolStripMenuItem.Text = "Spectate";
            this.spectateToolStripMenuItem.Click += new System.EventHandler(this.spectateToolStripMenuItem_Click);
            // 
            // addFriend
            // 
            this.addFriend.Location = new System.Drawing.Point(25, 255);
            this.addFriend.Name = "addFriend";
            this.addFriend.Size = new System.Drawing.Size(83, 23);
            this.addFriend.TabIndex = 2;
            this.addFriend.Text = "Add Friend";
            this.addFriend.UseVisualStyleBackColor = true;
            this.addFriend.Click += new System.EventHandler(this.addFriend_Click);
            this.addFriend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FriendsList_KeyPressed);
            // 
            // delFriend
            // 
            this.delFriend.Location = new System.Drawing.Point(179, 255);
            this.delFriend.Name = "delFriend";
            this.delFriend.Size = new System.Drawing.Size(78, 23);
            this.delFriend.TabIndex = 3;
            this.delFriend.Text = "Delete Friend";
            this.delFriend.UseVisualStyleBackColor = true;
            this.delFriend.Click += new System.EventHandler(this.delFriend_Click);
            this.delFriend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FriendsList_KeyPressed);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 150);
            // 
            // FriendsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(284, 285);
            this.Controls.Add(this.delFriend);
            this.Controls.Add(this.addFriend);
            this.Controls.Add(this.groupBox1);
            this.Name = "FriendsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "King of Ames";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FriendsList_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FriendsList_KeyPressed);
            this.Disposed += new System.EventHandler(this.FriendsList_Disposed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addFriend;
        private System.Windows.Forms.Button delFriend;
        private ListView BoxOFriends;
        private ColumnHeader playerName;
        private ColumnHeader status;
        private ToolStripPanel BottomToolStripPanel;
        private ToolStripPanel TopToolStripPanel;
        private ToolStripPanel RightToolStripPanel;
        private ToolStripPanel LeftToolStripPanel;
        private ToolStripContentPanel ContentPanel;
        private ContextMenuStrip cm1;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem joinGameToolStripMenuItem;
        private ToolStripMenuItem spectateToolStripMenuItem;
        private Label label1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem viewProfileToolStripMenuItem;
    }
}