using System;
using System.Linq;
using System.Windows.Forms;
using Networking;

namespace Views
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
        /// Also checks validity of username and password
        ///     i.e. Can't be blank, must contain letters and numbers only, and username can't already exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newUserButton_Click(object sender, EventArgs e)
        {
            errorLabel.Hide();
            //Check that the inputs are not empty
            if (newUsername.TextLength > 0 && newPassword.TextLength > 0)
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
                        errorLabel.Text = "Username already exists.";
                        errorLabel.Show();
                    }
                }
                else
                {
                    errorLabel.Text = "LETTERS AND NUMBERS ONLY";
                    errorLabel.Show();
                }
            }
            else
            {
                errorLabel.Text = "Username/Password cannot be blank.";
                errorLabel.Show();
            }
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
    }
}
