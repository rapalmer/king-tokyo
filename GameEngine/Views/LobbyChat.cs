using System;
using System.Linq;
using System.Windows.Forms;
using GameEngine.ServerClasses;
using Networking;

namespace GameEngine.Views
{
    public partial class LobbyChat : Form
    {

        private readonly Timer _timer = new Timer { Interval = (1000) };

        public LobbyChat()
        {
            InitializeComponent();
            username.Text = User.Username + ": ";
            _timer.Tick += CheckUpdate;
            _timer.Start();
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void clearChat_Click(object sender, EventArgs e)
        {
            Chat.Text = "";
        }

        private void wrtieMessage_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13) { Send();}
        }

        private void Send()
        {
            if (writeMessage.TextLength <= 0 || writeMessage.TextLength >= 100) return;
            if (!ContainsVaildChars(writeMessage.Text)) return;
            Client.SendChatMessage(User.Username + ": " + writeMessage.Text + "\n");
            writeMessage.Text = "";
        }

        private void CheckUpdate(object sender, EventArgs e)
        {
            if (Client.ChatHistory.Count <= 0) return;
            foreach (var mess in Client.ChatHistory)
            {
                Chat.Text += mess;
            }
            Client.ChatHistory.Clear();
        }

        private void LobbyChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                _timer.Stop();
                return;
            }
            e.Cancel = true;
            Hide();
        }

        private void LobbyChat_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'c') return;
            Hide();
        }

        /// <summary>
        /// Checks if all characters in the given string are valid
        /// Valid chars include 0-9, A-Z, a-z, and spaces
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true if valid, false otherwise</returns>
        private static bool ContainsVaildChars(string s)
        {
            return s.All(t => (t > 47 && t < 58) || (t > 64 && t < 91) || (t > 96 && t < 123) || t == 32);
        }
    }
}
