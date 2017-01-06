using Networking;
using System;
using System.Windows.Forms;

namespace Views
{
    /// <summary>
    /// Form to list the players currently in the lobby and select character
    /// </summary>
    public partial class PlayerLobby : Form
    {
        //Timer to handle view updates
        private readonly Timer _timer;

        /// <summary>
        /// Initializing variables
        /// </summary>
        public PlayerLobby()
        {
            InitializeComponent();
            UpdateList();

            _timer = new Timer {Interval = (2*1000)};//Ticks every 2 seconds
            _timer.Tick += timer_Tick;
            _timer.Start();
        }

        /// <summary>
        /// Updates the list of players and their characters every 2 seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateList();
        }

        /// <summary>
        /// On click, resets character to null, removes player from server, stops NetClient, and takes user back to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leaveGame_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            NetworkClasses.UpdateCharacter(User.PlayerId, null);
            NetworkClasses.FindRemovePlayer(Client.Conn, User.PlayerId);
            Client.ClientStop();
            var form = new MainMenuForm();
            form.Show();
            Dispose();
        }

        /// <summary>
        /// Checks if user is closing the application, clsoes accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlayerLobby_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            _timer.Stop();
            Dispose();
            NetworkClasses.UpdateCharacter(User.PlayerId, null);
            NetworkClasses.FindRemovePlayer(Client.Conn, User.PlayerId);
            Client.ClientStop();
            Environment.Exit(0);
        }

        /// <summary>
        /// Updates the list of players with the current information about the server via the database
        /// </summary>
        private void UpdateList()
        {
            playerList.Items.Clear();
            try
            {
                var ds = NetworkClasses.GetServer(Client.Conn);
                var row = ds.Tables[0].Rows[0];

                var grabber = NetworkClasses.GetPlayer(int.Parse(row["Host"].ToString()));

                //Host
                var listItem = new ListViewItem(grabber.Tables[0].Rows[0]["Username"].ToString());
                listItem.SubItems.Add(grabber.Tables[0].Rows[0]["_Character"].ToString());
                //Add the row entry to the listview
                playerList.Items.Add(listItem);

                for (var i = 2; i <= 6; i++)
                {
                    if (string.IsNullOrEmpty(row["Player_" + i].ToString())) continue;
                    grabber = NetworkClasses.GetPlayer(int.Parse(row["Player_" + i].ToString()));
                    listItem = new ListViewItem(grabber.Tables[0].Rows[0]["Username"].ToString());
                    listItem.SubItems.Add(grabber.Tables[0].Rows[0]["_Character"].ToString());
                    playerList.Items.Add(listItem);
                }
            }
            catch (Exception) //Thrown if server no longer exists
            {
                Form form = new MainMenuForm();
                form.Show();
                _timer.Stop();
                Client.ClientStop();
                NetworkClasses.UpdateCharacter(User.PlayerId, null);
                MessageBox.Show("Host left the game", "Server Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispose();
            }
        }

        /// <summary>
        /// Sends the selected character to the database update function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_char_Click(object sender, EventArgs e)
        {
            NetworkClasses.UpdateCharacter(User.PlayerId, char_list.SelectedItem.ToString());
        }
    }
}
