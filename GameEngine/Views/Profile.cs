using System;
using System.Windows.Forms;
using Networking;

namespace GameEngine.Views
{
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
            InitializeStats(User.Username);
            groupBox1.Text = User.Username;
        }

        public Profile(string username)
        {
            InitializeComponent();
            InitializeStats(username);
            groupBox1.Text = username;
            BackButton.Visible = false;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Form menu = new MainMenuForm();
            menu.Show();
            Dispose();
        }

        private void InitializeStats(string user)
        {
            joinedGames.Text = NetworkClasses.GetUserStat(user, "Games_Joined");
            hostedGames.Text = NetworkClasses.GetUserStat(user, "Games_Hosted");
            wonGames.Text = NetworkClasses.GetUserStat(user, "Games_Won");
        }

        /// <summary>
        /// Checks if user is closing the application, closes accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Profile_Closing(object sender, FormClosingEventArgs e)
        {
            Dispose();
            if (BackButton.Visible) { Environment.Exit(0);}
        }
    }
}
