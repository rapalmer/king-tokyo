using System;
using System.Windows.Forms;
using GameEngine.ServerClasses;
using Networking;

namespace GameEngine.Views
{
    /// <summary>
    /// Form to handle user navigation to the various pre-game menus, options, and account info
    /// </summary>
    public partial class MainMenuForm : Form
    {
        private readonly Form _friends;
        private readonly Timer _timer;

        /// <summary>
        /// Intializing variables
        /// </summary>
        public MainMenuForm()
        {
            InitializeComponent();
            _friends = new FriendsList();
            _timer = new Timer() {Interval = 1000};
            _timer.Tick += timer_tick;
            _timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            if (_friends.IsDisposed)
            {
                _timer.Stop();
                Dispose();
            }
        }

        /// <summary>
        /// Checks if user is closing the application, closes accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainMenuForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            _friends.Dispose();
            _timer.Stop();
            Dispose();
            NetworkClasses.UpdateUserValue("User_List", "Online", "Offline", User.PlayerId);
            Environment.Exit(0);
        }

        /// <summary>
        /// On click, starts the NetHost and takes user to the host lobby 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostButton_Click(object sender, EventArgs e)
        {
            Host.ServerStart();
            NetworkClasses.UpdateUserValue("User_Stats", "Games_Hosted", "Games_Hosted + 1", User.PlayerId);
            NetworkClasses.UpdateUserValue("User_List", "Online", "In Lobby", User.PlayerId);
            _timer.Stop();
            Form gameList = new HostGameListForm();
            _friends.Dispose();
            gameList.Show();
            Dispose();
        }

        /// <summary>
        /// On click, starts the NetClient and takes user to the server list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JoinButton_Click(object sender, EventArgs e)
        {
            Client.NetClient.Start();
            _timer.Stop();
            Form serverList = new ServerListForm();
            _friends.Dispose();
            serverList.Show();
            Dispose();
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            Form option = new Options();
            _friends.Dispose();
            option.Show();
            Dispose();
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            Form profile = new Profile();
            _friends.Dispose();
            profile.Show();
            Dispose();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            NetworkClasses.UpdateUserValue("User_List", "Online", "Offline", User.PlayerId);
            Form login = new LoginForm();
            _friends.Dispose();
            login.Show();
            Dispose();
        }

        private void MainMenuForm_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'f') return;
            if (_friends.Visible)
            {
                _friends.Hide();
            }
            else
            {
                _friends.Show();
            }
        }
    }
}
