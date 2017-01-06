using System;
using System.Windows.Forms;
using Networking;

namespace GameEngine.Views
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
            Login();
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

        private void LoginForm_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13) { Login();}
        }

        private void Login()
        {
            //Check input isn't empty, else error message is shown
            if (usernameBox.TextLength > 0 && passwordBox.TextLength > 0)
            {

                //Sends input to login function, if input is good, sends user to main menu 
                //Else error message is shown
                var result = NetworkClasses.Login(usernameBox.Lines[0], passwordBox.Lines[0],
                    Helpers.GetLocalIpAddress());
                if (result == "good" || result == "online")
                {
                    NetworkClasses.UpdateUserValue("User_List", "Online", "Online", User.PlayerId);
                    Form form = new MainMenuForm();
                    form.Show();
                    Dispose();
                }
                else if(result == "pass" || result == "user")
                {
                    MessageBox.Show("Invalid Username/Password", "Login error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                /*else if (result == "online")
                {
                    MessageBox.Show("User already logged in", "Login error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }*/
            }
            else
            {
                MessageBox.Show("Username and password cannot be blank.", "Login error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
    }
}
