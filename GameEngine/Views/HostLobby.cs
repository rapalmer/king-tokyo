using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Controllers;
using GameEngine.ServerClasses;
using Networking;

namespace GameEngine.Views
{
    /// <summary>
    /// Form to list the players currently in the lobby and select character w/ ability to start the game
    /// </summary>
    public partial class HostGameListForm : Form
    {
        //Timer to facilitate the updating of the view
        private readonly Timer _timer;
        private readonly Timer _gameTimer;
        private readonly Form _chat = new LobbyChat();
        private Form _profile = new Profile();
        private readonly List<DataSet> _players = new List<DataSet>();

        /// <summary>
        /// Initializing variables
        /// </summary>
        public HostGameListForm()
        {
            InitializeComponent();
            _chat.Show();
            start_game.Enabled = false;
            viewProfileToolStripMenuItem.Visible = false;
            UpdateList();
            //timer that runs to check for updated SQL values, then updates listview accordingly
            _timer = new Timer {Interval = 1000}; //Ticks every 1 seconds
            _gameTimer = new Timer {Interval = 2000};
            _timer.Tick += timer_Tick;
            _gameTimer.Tick += gameTimer_tick;
            _timer.Start();

        }

        /// <summary>
        /// Automatic update of the list of players and their characters every 1 seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (Host.Players.Count > 1)
            {
                start_game.Enabled = NetworkClasses.CheckReady(Host.Players);
            }
            UpdateList();
        }

        private void gameTimer_tick(object sender, EventArgs e)
        {
            if (!Client.gameClose) return;
            Host.ServerStop();
            _gameTimer.Stop();
            if (!_chat.IsDisposed) _chat.Dispose();
            NetworkClasses.UpdateUserValue("User_List", "Online", "Online", User.PlayerId);
            MessageBox.Show("Game Closed", "Update", MessageBoxButtons.OK);
            Form form = new MainMenuForm();
            form.Show();
        }

        private void HostGameListForm_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'c') return;
            if (_chat.Visible)
            {
                _chat.Hide();
            }
            else
            {
                _chat.Show();
            }
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
            _chat.Dispose();
            if (!_profile.IsDisposed) _profile.Dispose();   
            Dispose();
            NetworkClasses.UpdateUserValue("User_List", "Online", "Offline", User.PlayerId);
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
            NetworkClasses.UpdateUserValue("User_List", "_Character", null, User.PlayerId);
            NetworkClasses.UpdateUserValue("User_List", "Online", "Online", User.PlayerId);
            Host.ServerStop();
            Form form = new MainMenuForm();
            form.Show();
            _chat.Dispose();
            if (!_profile.IsDisposed) _profile.Dispose();
            Dispose();
        }

        /// <summary>
        /// Updates the list of players with the current information about the server via the database
        /// </summary>
        private void UpdateList()
        {
            var done = true;
            //Gets server info and puts it into a dataset
            var ds = NetworkClasses.GetServer(User.PlayerId, User.LocalIp);

            var currentNumPlayers = NetworkClasses.GetNumPlayers(int.Parse(ds.Tables[0].Rows[0]["Server_ID"].ToString()));
            if (_players.Count == currentNumPlayers && _players.Count >= 1)
            {
                //Check if characters have changed
                var newPlayerChars = new List<DataSet>
                {
                    NetworkClasses.GetPlayer(int.Parse(ds.Tables[0].Rows[0]["Host"].ToString()))
                };
                for (var i = 2; i <= 6; i++)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Player_" + i].ToString()))
                    {
                        newPlayerChars.Add(
                            NetworkClasses.GetPlayer(int.Parse(ds.Tables[0].Rows[0]["Player_" + i].ToString())));
                    }
                }
                for (var i = 0; i < newPlayerChars.Count; i++)
                {
                    if (_players[i].Tables[0].Rows[0]["_Character"].ToString() !=
                        newPlayerChars[i].Tables[0].Rows[0]["_Character"].ToString())
                    {
                        done = false;
                        break;
                    }
                }
                if (done)
                {
                    return;
                }
            }

            //Updating
            _players.Clear();
            playerList.Items.Clear();
            var row = ds.Tables[0].Rows[0];
            var grabber = NetworkClasses.GetPlayer(int.Parse(row["Host"].ToString()));
            _players.Add(grabber);
            var _character = "";

            //Host
            var listItem = new ListViewItem(grabber.Tables[0].Rows[0]["Username"].ToString());
            _character = grabber.Tables[0].Rows[0]["_Character"].ToString();
            listItem.SubItems.Add(_character);

            //Add the clients to the listview
            playerList.Items.Add(listItem);
            for(var i = 2; i <= 6; i++)
            {
                if (string.IsNullOrEmpty(row["Player_" + i].ToString())) continue;
                grabber = NetworkClasses.GetPlayer(int.Parse(row["Player_" + i].ToString()));
                _players.Add(grabber);
                listItem = new ListViewItem(grabber.Tables[0].Rows[0]["Username"].ToString());
                _character = grabber.Tables[0].Rows[0]["_Character"].ToString();
                listItem.SubItems.Add(_character);

                playerList.Items.Add(listItem);
            }
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
            NetworkClasses.UpdateServerValue("Status", "In Progress", "Host", User.PlayerId);
            NetworkClasses.UpdateUserValue("User_List", "Online", "In Game", User.PlayerId);
            LobbyController.StartGame();
            Host.StartGame();
            _timer.Stop();
            //_chat.Dispose();
            _gameTimer.Start();
            if (!_profile.IsDisposed) _profile.Dispose();
            Hide();

            //MainMenuForm waiter = new MainMenuForm();
            //waiter.Show();
        }

        private void char_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (NetworkClasses.CheckCharacterAvailable(Client.Conn, char_list.SelectedItem.ToString()))
                {
                    NetworkClasses.UpdateUserValue("User_List", "_Character", char_list.SelectedItem.ToString(), User.PlayerId);
                }
                else
                {
                    MessageBox.Show("Character Unavailable", "Character has already been selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid character", "Please choose a valid character", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _profile = new Profile(playerList.SelectedItems[0].Text);
            _profile.Show();
        }

        private void playerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewProfileToolStripMenuItem.Visible = playerList.SelectedItems.Count == 1;
        }
    }
}
