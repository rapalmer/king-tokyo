using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using GameEngine.ServerClasses;
using Networking;

namespace GameEngine.Views
{
    public partial class FriendsList : Form
    {
        private readonly Timer _timer;
        private List<DataSet> _friendInfo = new List<DataSet>();
        private readonly Form _add;
        private Form _profile;

        public FriendsList()
        {
            InitializeComponent();
            delFriend.Enabled = false;
            deleteToolStripMenuItem.Visible = false;
            joinGameToolStripMenuItem.Visible = false;
            spectateToolStripMenuItem.Visible = false;
            viewProfileToolStripMenuItem.Visible = false;
            _add = new AddFriendForm();
            GetFriends();
            _timer = new Timer {Interval = 2000};
            _timer.Tick += timer_tick;
            _timer.Start();
        }

        public void timer_tick(object sender, EventArgs e)
        {
            GetFriends();
        }

        public void GetFriends()
        {
            var playerFriends =
                NetworkClasses.GetPlayer(User.PlayerId).Tables[0].Rows[0]["Friends"].ToString().Split(',');
            if (playerFriends[0] == "0")
            {
                if (BoxOFriends.Items.Count == 1)
                {
                    BoxOFriends.Items.Clear();
                    delFriend.Enabled = false;
                    addToolStripMenuItem.Visible = true;
                    viewProfileToolStripMenuItem.Visible = false;
                    deleteToolStripMenuItem.Visible = false;
                    joinGameToolStripMenuItem.Visible = false;
                    spectateToolStripMenuItem.Visible = false;
                    _friendInfo = new List<DataSet>();
                }
                return;
            }
            var datasets = playerFriends.Select(friend => NetworkClasses.GetPlayer(int.Parse(friend))).ToList();
            if (datasets.Count != _friendInfo.Count)
            {
                BoxOFriends.Items.Clear();
                foreach (var player in datasets)
                {
                    var listItem = new ListViewItem(player.Tables[0].Rows[0]["Username"].ToString());
                    listItem.SubItems.Add(player.Tables[0].Rows[0]["Online"].ToString());
                    BoxOFriends.Items.Add(listItem);
                }
                _friendInfo = datasets;
                return;
            }
            for (var i = 0; i < datasets.Count; i++)
            {
                if (datasets[i].Tables[0].Rows[0]["Online"].ToString() ==
                    _friendInfo[i].Tables[0].Rows[0]["Online"].ToString()) continue;
                BoxOFriends.Items.Clear();
                foreach (var player in datasets)
                {
                    var listItem = new ListViewItem(player.Tables[0].Rows[0]["Username"].ToString());
                    listItem.SubItems.Add(player.Tables[0].Rows[0]["Online"].ToString());
                    BoxOFriends.Items.Add(listItem);
                }
                _friendInfo = datasets;
                if (BoxOFriends.Items.Count == 10)
                {
                    addToolStripMenuItem.Visible = false;
                    addFriend.Enabled = false;
                }
                return;
            }
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            _add.Show();
        }

        private void FriendsList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                _timer.Stop();
                return;
            }
            _add.Hide();
            e.Cancel = true;
            Hide();
        }

        private void FriendsList_Disposed(object sender, EventArgs e)
        {
            _add.Dispose();
        }

        private void BoxOFriends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BoxOFriends.SelectedItems.Count == 1 && BoxOFriends.Items.Count > 0)
            {
                delFriend.Enabled = true;
                deleteToolStripMenuItem.Visible = true;
                addToolStripMenuItem.Visible = false;
                viewProfileToolStripMenuItem.Visible = true;
                if (BoxOFriends.SelectedItems[0].SubItems[1].Text == "In Lobby")
                    joinGameToolStripMenuItem.Visible = true;
                if (BoxOFriends.SelectedItems[0].SubItems[1].Text == "In Game")
                    spectateToolStripMenuItem.Visible = true;
            }
            else
            {
                delFriend.Enabled = false;
                if(BoxOFriends.Items.Count < 10) addToolStripMenuItem.Visible = true;
                viewProfileToolStripMenuItem.Visible = false;
                deleteToolStripMenuItem.Visible = false;
                joinGameToolStripMenuItem.Visible = false;
                spectateToolStripMenuItem.Visible = false;
            }
        }

        private void delFriend_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?","Confirm",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                NetworkClasses.DelFriend(BoxOFriends.SelectedItems[0].Text);
            }
        }

        private void FriendsList_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'f') return;
            _add.Hide();
            Hide();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _add.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                NetworkClasses.DelFriend(BoxOFriends.SelectedItems[0].Text);
            }
        }

        private void joinGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ds = NetworkClasses.GetServerByPlayerId(NetworkClasses.GetPlayer(BoxOFriends.SelectedItems[0].Text).Tables[0].Rows[0]["Player_ID"].ToString());
            //Console.WriteLine(ds.Tables[0].Rows[0]["Host"]);
            Client.Conn = ds.Tables[0].Rows[0]["Host_IP"].ToString();
            Client.NetClient.Start();
            Client.Connect();
            NetworkClasses.JoinServer(Client.Conn, User.PlayerId);
            //Increments games joined
            NetworkClasses.UpdateUserValue("User_Stats", "Games_Joined", "Games_Joined+1", User.PlayerId);
            NetworkClasses.UpdateUserValue("User_List", "Online", "In Lobby", User.PlayerId);
            Form form = new PlayerLobby();
            form.Show();
            Dispose();
        }

        private void spectateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ds = NetworkClasses.GetServerByPlayerId(NetworkClasses.GetPlayer(BoxOFriends.SelectedItems[0].Text).Tables[0].Rows[0]["Player_ID"].ToString());
            Client.Conn = ds.Tables[0].Rows[0]["Host_IP"].ToString();
            Client.NetClient.Start();
            Client.Connect(false);
            NetworkClasses.UpdateUserValue("User_List", "Online", "Spectating", User.PlayerId);
            //Dispose();
        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _profile = new Profile(BoxOFriends.SelectedItems[0].Text);
            _profile.Show();
        }
    }
}
