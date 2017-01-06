using Networking;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GameEngine.Views
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            groupBox2.Visible = (NetworkClasses.GetPlayer(User.PlayerId).Tables[0].Rows[0]["IsAdmin"].ToString() == "1");
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Form menu = new MainMenuForm();
            menu.Show();
            Dispose();
        }

        /// <summary>
        /// Checks if user is closing the application, closes accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Options_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            Dispose();
            Environment.Exit(0);
        }

        private void nameChange_Click(object sender, EventArgs e)
        {
            if (nameChangeText.TextLength > 0 && nameChangeText.TextLength <= 20)
            {
                if (ContainsVaildChars(nameChangeText.Text))
                {
                    if (NetworkClasses.UpdateUserValue("User_List", "_Character", nameChangeText.Text, User.PlayerId))
                    {
                        MessageBox.Show("Username has been changed to " + nameChangeText.Text + ".", "Username Updated",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nameChangeText.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Username is already taken.", "Username Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Username must contain only letters and numbers.", "Username Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Username must be between 1 and 20 characters.", "Username Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void passChange_Click(object sender, EventArgs e)
        {
            if (passChangeText.TextLength >= 5 && passChangeText.TextLength <= 20)
            {
                if (ContainsVaildChars(passChangeText.Text))
                {
                    if (NetworkClasses.UpdateUserValue("User_List","Password",StringCipher.Encrypt(passChangeText.Text, "thomas").ToString(),User.PlayerId))
                    {
                        MessageBox.Show("Password has been changed", "Password Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        passChangeText.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Password must contain only letters and numbers.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Password must be between 5 and 20 characters.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Checks if all characters in the given string are valid
        /// Valid chars include 0-9, A-Z, and a-z
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true if valid, false otherwise</returns>
        private static bool ContainsVaildChars(string s)
        {
            return s.All(t => (t > 47 && t < 58) || (t > 64 && t < 91) || (t > 96 && t < 123));
        }

        private void banPlayer_Click(object sender, EventArgs e)
        {
            var n = -1;

            //Checks if user is admin
            if (NetworkClasses.GetPlayer(User.PlayerId).Tables[0].Rows[0]["IsAdmin"].ToString() == "1")
            {
                //Checks for empty textbox and if the value is an int
                if (banPlayerText.TextLength > 0 && int.TryParse(banPlayerText.Lines[0], out n))
                {
                    //Bans the selected player id
                    if (NetworkClasses.BanPlayer(n))
                    {
                        MessageBox.Show("Successfully Banned Player ID: " + banPlayerText.Lines[0], "Player Banned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        banPlayerText.Lines[0] = "";
                    }
                    else
                    {
                        MessageBox.Show("Invalid Player ID", "Ban Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Player ID must be numeric.", "Ban Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("You are not an admin, contact an admin for privileges.", "Admin Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
