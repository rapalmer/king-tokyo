using System;
using System.Windows.Forms;
using Networking;

//TODO encrypt password

namespace Views
{
    /// <summary>
    /// Form to handle exsiting user login
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Initializing variables
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On click, queries the database to check if user exsits, and input is correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            //Check input isn't empty, else error message is shown
            if(usernameBox.TextLength > 0 && passwordBox.TextLength > 0)
            {
                //Hide existing error label, if any
                errorLabel.Hide();

                //Sends input to login function, if input is good, sends user to main menu 
                //Else error message is shown
                if (NetworkClasses.Login(usernameBox.Lines[0], passwordBox.Lines[0], Helpers.GetLocalIpAddress()))
                {
                    Form form = new MainMenuForm();
                    form.Show();
                    Dispose();
                }
                else
                {
                    errorLabel.Text = "Invalid Username/Password";
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
        /// Checks if user is closing the applications, closes accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            Dispose();
            Environment.Exit(0);
        }

        /// <summary>
        /// On click, takes user to the new user form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createUser_Click(object sender, EventArgs e)
        {
            Form form = new NewUserForm();
            form.Show();
            Dispose();
        }
    }
}
