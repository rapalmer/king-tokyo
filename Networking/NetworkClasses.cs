using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Networking
{
    /// <summary>
    /// This class contains the various functions for access the MySQL database
    /// </summary>
    public class NetworkClasses
    {
        //Information to access the MySQL database
        private const string ConnectString = "Server=10.25.71.66;Database=db309yt01;Uid=dbu309yt01;Pwd=ZuuYea5cBtZ;";

        /// <summary>
        /// Using the inputs from the signup form, opens a connection to the database, check if username exists, if not, adds user to list
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="ip"></param>
        public static bool CreateUser(string user, string pass, string ip)
        {
            var connection = new MySqlConnection(ConnectString);
            MySqlCommand command;
            connection.Open();
            pass = StringCipher.Encrypt(pass, "thomas");
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO User_List (Username, Password, Local_IP) VALUES (@user,@pass,@ip)";
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@ip", ip);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //catch exception for non-unique player name and return false
                return false;
            }
            var ds = new DataSet();
            command.CommandText = "SELECT * FROM User_List WHERE Username = @username";
            command.Parameters.AddWithValue("@username", user);
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO User_Stats (Player_ID, Games_Joined, Games_Hosted, Games_Won) VALUES (@id,0,0,0)";
            command.Parameters.AddWithValue("@id", ds.Tables[0].Rows[0]["Player_ID"]);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public static object GetUserValue(string value)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM User_List WHERE Player_ID = @user";
            command.Parameters.AddWithValue("@user", User.PlayerId);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0) return false;
            connection.Close();
            return ds.Tables[0].Rows[0][value].ToString();
        }

        public static string GetUserStat(string username, string stat)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM User_Stats WHERE Player_ID = @user";
            command.Parameters.AddWithValue("@user", GetPlayer(username).Tables[0].Rows[0]["Player_ID"]);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0) return "null";
            connection.Close();
            return ds.Tables[0].Rows[0][stat].ToString();
        }

        /// <summary>
        /// Using inputs from the login form, opens a connection to the database, checks if username exsits, if it does, checks if password is correct
        /// Returns false if any checks fail
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string Login(string user, string pass, string ip)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM User_List WHERE Username = @user";
            command.Parameters.AddWithValue("@user", user);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0) return "user";
            var dbpass = StringCipher.Decrypt(ds.Tables[0].Rows[0]["password"].ToString(), "thomas");
            if (dbpass != pass) return "pass";
            //if (ds.Tables[0].Rows[0]["Online"].ToString() != "Offline") return "online";
            //update the players ip
            UpdateUserValue("User_List", "Local_IP", ip, int.Parse(ds.Tables[0].Rows[0]["Player_ID"].ToString()));

            User.Username = ds.Tables[0].Rows[0]["Username"].ToString();
            User.LocalIp = ip;
            User.PlayerId = int.Parse(ds.Tables[0].Rows[0]["Player_ID"].ToString());
            User.Character = ds.Tables[0].Rows[0]["_Character"].ToString();

            connection.Close();
            DeleteServer(User.PlayerId);
            UpdateUserValue("User_List", "_Character", null, User.PlayerId);
            return "good";
        }

        /// <summary>
        /// Checks if the player id is the host of a server (for use by the exit function)
        /// </summary>
        /// <param name="hostid"></param>
        /// <returns></returns>
        public static bool IsHosting(int hostid)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Server_List WHERE Host = @hostid";
            command.Parameters.AddWithValue("@hostid", hostid);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            connection.Close();
            return ds.Tables[0].Rows.Count != 0;
        }

        /// <summary>
        /// Creates a new server entry in the database when a user hosts a game
        /// </summary>
        /// <param name="hostid"></param>
        /// <param name="hostip"></param>
        /// <returns></returns>
        public static bool CreateServer(int hostid, string hostip)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Server_List (Host, Host_IP, Status) VALUES (@hostid, @hostip, 'Creating')";
            command.Parameters.AddWithValue("@hostid", hostid);
            command.Parameters.AddWithValue("@hostip", hostip);
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }

        /// <summary>
        /// Deletes server entry from the database when the host closes the server
        /// </summary>
        /// <param name="hostid"></param>
        /// <returns></returns>
        public static bool DeleteServer(int hostid)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Server_List WHERE Host = '" + hostid + "'";
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }

        /// <summary>
        /// Gets the server information from the database using a host ID & IP
        /// </summary>
        /// <param name="hostid"></param>
        /// <param name="hostip"></param>
        /// <returns>Dataset containing the server ID, host ID, host IP, and connected player IDs</returns>
        public static DataSet GetServer(int hostid, string hostip)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Server_List WHERE Host = @hostid AND Host_IP = @hostip";
            command.Parameters.AddWithValue("@hostid", hostid);
            command.Parameters.AddWithValue("@hostip", hostip);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);

            connection.Close();
            return ds;
        }

        /// <summary>
        /// Gets the server information from the database using a host IP
        /// </summary>
        /// <param name="hostip"></param>
        /// <returns>>Dataset containing the server ID, host ID, host IP, and connected player IDs</returns>
        public static DataSet GetServer(string hostip)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Server_List WHERE Host_IP = @hostip";
            command.Parameters.AddWithValue("@hostip", hostip);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);

            connection.Close();
            return ds;
        }

        public static DataSet GetServerByPlayerId(string id)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Server_List WHERE Host = @id OR Player_2 = @id OR Player_3 = @id OR Player_4 = @id OR Player_5 = @id OR Player_6 = @id";
            command.Parameters.AddWithValue("@id", id);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);

            connection.Close();
            return ds;
        }

        /// <summary>
        /// Gets the server info of all servers in the database
        /// </summary>
        /// <returns>Dataset containing server info of all servers</returns>
        public static DataSet GetServers()
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();

            var command = connection.CreateCommand();
            //command.CommandText = "SELECT * FROM Server_List WHERE Status ='Creating'";
            command.CommandText = "SELECT * FROM Server_List";
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);

            connection.Close();
            return ds;
        }

        /// <summary>
        /// Gets player info using a given player ID
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>Dataset containing player info</returns>
        public static DataSet GetPlayer(int playerId)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM User_List WHERE Player_ID = @Player_ID";
            command.Parameters.AddWithValue("@Player_ID", playerId);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);

            connection.Close();
            return ds;
        }

        /// <summary>
        /// Gets player info using a given username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Dataset containing player info</returns>
        public static DataSet GetPlayer(string username)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            try
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM User_List WHERE Username = @user";
                command.Parameters.AddWithValue("@user", username);
                var adapter = new MySqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                connection.Close();
                return ds;
            }
            catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the new player into the server info
        /// </summary>
        /// <param name="hostip"></param>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public static bool JoinServer(string hostip, int playerid)
        {
            var openSpot = GetNextAvailableSpot(hostip);
            if (openSpot != -1)
            {
                var connection = new MySqlConnection(ConnectString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Server_List SET Player_" + openSpot + " = @playerid WHERE Host_IP = @hostip";
                command.Parameters.AddWithValue("@hostip", hostip);
                command.Parameters.AddWithValue("@playerid", playerid);
                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the lowest numbered available spot from the server information
        /// </summary>
        /// <param name="hostip"></param>
        /// <returns>lowest spot in server 2-6</returns>
        public static int GetNextAvailableSpot(string hostip)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Server_List WHERE Host_IP = @hostip";
            command.Parameters.AddWithValue("@hostip", hostip);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (var i = 2; i < 6; i++)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Player_" + i].ToString())) continue;
                    connection.Close();
                    return i;
                }
            }
            connection.Close();
            return -1;
        }

        /// <summary>
        /// Finds the player in the server list to be removed
        /// </summary>
        /// <param name="hostip"></param>
        /// <param name="playerid"></param>
        public static void FindRemovePlayer(string hostip, int playerid)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            var remove = -1;

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Server_List WHERE Host_IP = @hostip";
            command.Parameters.AddWithValue("@hostip", hostip);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (var i = 2; i < 6; i++)
                {
                    if (int.Parse(ds.Tables[0].Rows[0]["Player_" + i].ToString()) == playerid)
                    {
                        remove = i;
                        break;
                    }
                }
            }
            connection.Close();
            RemovePlayer(hostip, remove);
        }

        /// <summary>
        /// Removes player with matching ID from the server information
        /// </summary>
        /// <param name="hostip"></param>
        /// <param name="playerPosition"></param>
        public static void RemovePlayer(string hostip, int playerPosition)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            try
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Server_List SET Player_" + playerPosition + " = null WHERE Host_IP = @hostip";
                command.Parameters.AddWithValue("@hostip", hostip);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Can't set player to null, server no longer exist");
            }

            connection.Close();
        }

        /// <summary>
        /// Checks that every player in the server has selected a character
        /// </summary>
        /// <param name="players"></param>
        /// <returns>false if any player hasn't selected a character, true otherwise</returns>
        public static bool CheckReady(List<int> players)
        {
            return players.All(player => !string.IsNullOrEmpty(GetPlayer(player).Tables[0].Rows[0]["_Character"].ToString()));
        }

        /// <summary>
        /// Gets the current number of players in the server
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns>number of players in the server</returns>
        public static int GetNumPlayers(int serverId)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            var count = -1;

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Server_List WHERE Server_ID = @Server_ID";
            command.Parameters.AddWithValue("@Server_ID", serverId);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            connection.Close();

            if (ds.Tables[0].Rows.Count == 0) return count;
            count = 1;
            for (var i = 2; i < 6; i++)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Player_" + i].ToString())){count++;}
            }

            return count;
        }

        /// <summary>
        /// Gets an int array of the player IDs currently connected to the Host
        /// </summary>
        /// <param name="hostip"></param>
        /// <returns>int array of IDs</returns>
        public static int[] GetPlayerIDs(string hostip)
        {
            var connection = new MySqlConnection(ConnectString);
            connection.Open();
            int[] players = { -1, -1, -1, -1, -1, -1 };

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Server_List WHERE Host_IP = @hostip";
            command.Parameters.AddWithValue("@hostip", hostip);
            var adapter = new MySqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            connection.Close();

            if (ds.Tables[0].Rows.Count == 0) return players;
            players[0] = int.Parse(ds.Tables[0].Rows[0]["Host"].ToString());
            for (var i = 2; i < 6; i++)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Player_" + i].ToString()))
                {
                    players[i-1] = int.Parse(ds.Tables[0].Rows[0]["Player_" + i].ToString());
                }
            }
            return players;
        }

        public static bool CheckCharacterAvailable(string hostip, string character)
        {
            var players = GetPlayerIDs(hostip);
            for(var i = 0; i < players.Length; i++)
            {
                if(players[i] != -1 && players[i] != User.PlayerId)
                {
                    var ds = GetPlayer(players[i]);
                    if (ds.Tables[0].Rows[0]["_Character"].ToString() == character)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool UpdateUserValue(string table, string column, string value, int playerid)
        {
            try
            {
                var connection = new MySqlConnection(ConnectString);
                connection.Open();
                var command = connection.CreateCommand();
                if(value == null) { command.CommandText = "UPDATE " + table + " SET " + column + " = null WHERE Player_ID = " + playerid;}
                else if(value.Contains("+1") || value.Contains("+ 1")){ command.CommandText = "UPDATE " + table + " SET " + column + " = " + value + " WHERE Player_ID = " + playerid; }
                else { command.CommandText = "UPDATE " + table + " SET " + column + " = '" + value + "' WHERE Player_ID = " + playerid; }
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static bool UpdateServerValue(string column, object value, string searchBy, object equals)
        {
            try
            {
                var connection = new MySqlConnection(ConnectString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Server_List SET " + column + " = '" + value + "' WHERE " + searchBy + " = " + equals;
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static string AddFriend(string username)
        {
            try
            {
                var ds1 = GetPlayer(username);
                var ds2 = GetPlayer(User.PlayerId);
                var friends = ds2.Tables[0].Rows[0]["Friends"].ToString();
                if (friends.Split(',').Contains(ds1.Tables[0].Rows[0]["Player_ID"].ToString()))
                {
                    return "Preexisting";
                }
                if (friends == "0")
                {
                    UpdateUserValue("User_List", "Friends", ds1.Tables[0].Rows[0]["Player_ID"].ToString(), User.PlayerId);
                }
                else
                {
                    UpdateUserValue("User_List", "Friends", friends + "," + ds1.Tables[0].Rows[0]["Player_ID"], User.PlayerId);
                }
            }
            catch (Exception)
            {
                return "Nonexistant";
            }
            return "Done";
        }

        public static bool DelFriend(string username)
        {
            try
            {
                var ds1 = GetPlayer(username);
                var toRemove = ds1.Tables[0].Rows[0]["Player_ID"].ToString();
                var ds2 = GetPlayer(User.PlayerId);
                var friends = ds2.Tables[0].Rows[0]["Friends"].ToString().Split(',');
                var newFriends = "";
                foreach (var person in friends)
                {
                    if (person != toRemove)
                    {
                        newFriends += person + ",";
                    }
                }
                newFriends = newFriends.TrimEnd(',');
                if (newFriends.Length < 1) newFriends = "0";
                UpdateUserValue("User_List", "Friends", newFriends, User.PlayerId);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool BanPlayer(int playerid)
        {
            try
            {
                var connection = new MySqlConnection(ConnectString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE User_List SET IsBanned = 1 WHERE Player_ID = @playerid";
                command.Parameters.AddWithValue("@playerid", playerid);
                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AddWin(int playerid)
        {
            try
            {
                int wins = int.Parse(GetUserStat(User.Username, "Games_Won"));
                wins++;
                var connection = new MySqlConnection(ConnectString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE User_Stats SET Games_Won = @wins WHERE Player_ID = @playerid";
                command.Parameters.AddWithValue("@playerid", playerid);
                command.Parameters.AddWithValue("@wins", wins);
                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
