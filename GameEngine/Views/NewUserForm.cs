using System;
using System.Linq;
using System.Windows.Forms;
using Networking;

namespace GameEngine.Views
{
    /// <summary>
    /// Form to handle creation of a new user
    /// </summary>
    public partial class NewUserForm : Form
    {
        /// <summary>
        /// Initializing variables
        /// </summary>
        public NewUserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On click, sends username,password, and IP to createUser function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newUserButton_Click(object sender, EventArgs e)
        {
            Create();
        }

        /// <summary>
        /// On click, sends user back to sign-in menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toLogin_Click(object sender, EventArgs e)
        {
            Form form = new LoginForm();
            form.Show();
            Dispose();
        }

        /// <summary>
        /// Checks if user is closing the program, closes accordingly 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewUserForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            Dispose();
            Environment.Exit(0);
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

        /// <summary>
        /// Same as clicking create user button, but happens when user presses enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewUserForm_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13) { Create();}
        }

        /// <summary>
        /// Checks validity of username and password and sends info to database
        ///     i.e. Length must be between 1 and 20, must contain letters and numbers only, and username can't already exist
        /// </summary>
        private void Create()
        {
            //Check that the inputs are not empty
            if (newUsername.TextLength > 0 && newPassword.TextLength >= 5 && newUsername.TextLength <= 20 && newPassword.TextLength < 20)
            {
                if (ContainsVaildChars(newUsername.Text) && ContainsVaildChars(newPassword.Text))
                {
                    var good = NetworkClasses.CreateUser(newUsername.Lines[0], newPassword.Lines[0],
                        Helpers.GetLocalIpAddress());
                    if (good)
                    {
                        Form form = new LoginForm();
                        form.Show();
                        Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Username already exists.", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("LETTERS AND NUMBERS ONLY", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (newUsername.Text.Length < 1 || newUsername.Text.Length > 20)
                {
                    MessageBox.Show(
                        "Username must be between 1 and 20 characters",
                        "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                if (newPassword.Text.Length < 5 || newPassword.Text.Length < 20)
                {
                    MessageBox.Show(
                        "Password must be between 5 and 20 characters",
                        "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
