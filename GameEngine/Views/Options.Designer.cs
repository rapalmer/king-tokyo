using System.Windows.Forms;

namespace GameEngine.Views
{
    partial class Options
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
            this.BackButton = new System.Windows.Forms.Button();
            this.banPlayerText = new System.Windows.Forms.TextBox();
            this.banPlayer = new System.Windows.Forms.Button();
            this.nameChange = new System.Windows.Forms.Button();
            this.passChange = new System.Windows.Forms.Button();
            this.nameChangeText = new System.Windows.Forms.TextBox();
            this.passChangeText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(84, 269);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 0;
            this.BackButton.Text = "Main Menu";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // banPlayerText
            // 
            this.banPlayerText.Location = new System.Drawing.Point(14, 32);
            this.banPlayerText.Name = "banPlayerText";
            this.banPlayerText.Size = new System.Drawing.Size(100, 20);
            this.banPlayerText.TabIndex = 1;
            // 
            // banPlayer
            // 
            this.banPlayer.Location = new System.Drawing.Point(120, 32);
            this.banPlayer.Name = "banPlayer";
            this.banPlayer.Size = new System.Drawing.Size(75, 23);
            this.banPlayer.TabIndex = 2;
            this.banPlayer.Text = "Submit";
            this.banPlayer.UseVisualStyleBackColor = true;
            this.banPlayer.Click += new System.EventHandler(this.banPlayer_Click);
            // 
            // nameChange
            // 
            this.nameChange.Location = new System.Drawing.Point(120, 32);
            this.nameChange.Name = "nameChange";
            this.nameChange.Size = new System.Drawing.Size(75, 23);
            this.nameChange.TabIndex = 3;
            this.nameChange.Text = "Submit";
            this.nameChange.UseVisualStyleBackColor = true;
            this.nameChange.Click += new System.EventHandler(this.nameChange_Click);
            // 
            // passChange
            // 
            this.passChange.Location = new System.Drawing.Point(120, 83);
            this.passChange.Name = "passChange";
            this.passChange.Size = new System.Drawing.Size(75, 23);
            this.passChange.TabIndex = 4;
            this.passChange.Text = "Submit";
            this.passChange.UseVisualStyleBackColor = true;
            this.passChange.Click += new System.EventHandler(this.passChange_Click);
            // 
            // nameChangeText
            // 
            this.nameChangeText.Location = new System.Drawing.Point(14, 32);
            this.nameChangeText.Name = "nameChangeText";
            this.nameChangeText.Size = new System.Drawing.Size(100, 20);
            this.nameChangeText.TabIndex = 5;
            // 
            // passChangeText
            // 
            this.passChangeText.Location = new System.Drawing.Point(14, 83);
            this.passChangeText.Name = "passChangeText";
            this.passChangeText.Size = new System.Drawing.Size(100, 20);
            this.passChangeText.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ban Player (by ID)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Change Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Change Password";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.passChange);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.passChangeText);
            this.groupBox1.Controls.Add(this.nameChange);
            this.groupBox1.Controls.Add(this.nameChangeText);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 115);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit User Info";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.banPlayerText);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.banPlayer);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 73);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Admin Options";
            // 

            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(251, 304);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BackButton);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.TextBox banPlayerText;
        private System.Windows.Forms.Button banPlayer;
        private System.Windows.Forms.Button nameChange;
        private System.Windows.Forms.Button passChange;
        private System.Windows.Forms.TextBox nameChangeText;
        private System.Windows.Forms.TextBox passChangeText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}