using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Networking;
using Controllers;

namespace Views
{
    /// <summary>
    /// Form to list the players currently in the lobby and select character w/ ability to start the game
    /// </summary>
    public partial class HostGameListForm : Form
    {
        //Timer to facilitate the updating of the view
        private readonly Timer _timer;

        /// <summary>
        /// Initializing variables
        /// </summary>
        public HostGameListForm()
        {
            InitializeComponent();
            start_game.Enabled = false;
            UpdateList();
            //timer that runs to check for updated SQL values, then updates listview accordingly
            _timer = new Timer {Interval = (2*1000)}; //Ticks every 2 seconds
            _timer.Tick += timer_Tick;
            _timer.Start();

        }

        /// <summary>
        /// Automatic update of the list of players and their characters every 2 seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            start_game.Enabled = NetworkClasses.CheckReady(Host.Players);
            UpdateList();
        }

        /// <summary>
        /// When the window is closed, the server is stopped, and the application is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HostGameListForm_Closing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
                _timer.Stop();
                Dispose();
                Host.ServerStop();
                Environment.Exit(0);
            //}
        }

        /// <summary>
        /// On click, resets character to null, stops NetServer, and takes user back to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leaveGame_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            NetworkClasses.UpdateCharacter(User.PlayerId, null);
            Host.ServerStop();
            var main = new MainMenuForm();
            main.Show();
            Dispose();
        }

        /// <summary>
        /// Updates the list of players with the current information about the server via the database
        /// </summary>
        private void UpdateList()
        {
            //Resets the view
            playerList.Items.Clear();

            //Gets server info and puts it into a dataset
            var ds = NetworkClasses.GetServer(User.PlayerId, User.LocalIp);
            var row = ds.Tables[0].Rows[0];
            var grabber = NetworkClasses.GetPlayer(int.Parse(row["Host"].ToString()));
            
            //Gets ping values for all players
            var pings = new List<int>();
            while(pings.Count < 1)
            {
                pings = Host.GetPing();
            }

            //Host
            var listItem = new ListViewItem(grabber.Tables[0].Rows[0]["Username"].ToString());
            listItem.SubItems.Add(grabber.Tables[0].Rows[0]["_Character"].ToString());
            listItem.SubItems.Add(pings[0].ToString() + " ms");

            //Add the clients to the listview
            playerList.Items.Add(listItem);
            for(var i = 2; i <= 6; i++)
            {
                if (string.IsNullOrEmpty(row["Player_" + i].ToString())) continue;
                grabber = NetworkClasses.GetPlayer(int.Parse(row["Player_" + i].ToString()));
                listItem = new ListViewItem(grabber.Tables[0].Rows[0]["Username"].ToString());
                listItem.SubItems.Add(grabber.Tables[0].Rows[0]["_Character"].ToString());
                listItem.SubItems.Add(pings[i-1].ToString() + " ms");
                playerList.Items.Add(listItem);
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

        /// <summary>
        /// Once all players are ready, adds players to the Game controller and starts the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_game_Click(object sender, EventArgs e)
        {
            var ds = NetworkClasses.GetServer(User.PlayerId, User.LocalIp);
            var row = ds.Tables[0].Rows[0];

            var grabber = NetworkClasses.GetPlayer(int.Parse(row["Host"].ToString()));

            //Host
            LobbyController.AddPlayer(int.Parse(grabber.Tables[0].Rows[0]["Player_ID"].ToString()), grabber.Tables[0].Rows[0]["_Character"].ToString());

            for (var i = 2; i <= 6; i++)
            {
                if (string.IsNullOrEmpty(row["Player_" + i].ToString())) continue;
                grabber = NetworkClasses.GetPlayer(int.Parse(row["Player_" + i].ToString()));
                LobbyController.AddPlayer(int.Parse(grabber.Tables[0].Rows[0]["Player_ID"].ToString()), grabber.Tables[0].Rows[0]["_Character"].ToString());
            }
            NetworkClasses.UpdateServerStatus("In Progress", User.PlayerId);
            LobbyController.StartGame();
            Host.StartGame();
        }
    }
}
