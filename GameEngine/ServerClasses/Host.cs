using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Controllers;
using GamePieces.Session;
using Lidgren.Network;
using Networking;
using Networking.Actions;
using Newtonsoft.Json;

namespace GameEngine.ServerClasses
{
    public static class Host
    {
        //List of player ID's connected to the server
        public static List<int> Players = new List<int>();
        public static List<NetConnection> Spectators = new List<NetConnection>();
        //Variable for the Lidgren server stuff
        private static NetServer _server;
        private static bool _shouldStop = false;

        /// <summary>
        /// Initializes the server, starts the reiceve loop, creates a NetClient and connects it to the server
        /// </summary>
        public static void ServerStart()
        {
            //Server Setup
            var config = new NetPeerConfiguration("King of Ames") {Port = 6969};
            config.DisableMessageType(NetIncomingMessageType.DebugMessage);
            config.DisableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.DisableMessageType(NetIncomingMessageType.DiscoveryResponse);
            config.DisableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            config.DisableMessageType(NetIncomingMessageType.Receipt);
            config.DisableMessageType(NetIncomingMessageType.ErrorMessage);
            config.DisableMessageType(NetIncomingMessageType.WarningMessage);
            config.DisableMessageType(NetIncomingMessageType.UnconnectedData);
            config.DisableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            _server = new NetServer(config);
            _server.Start();
            Console.WriteLine("Server started...");

            //add server to the SQL database with the current details
            NetworkClasses.CreateServer(User.PlayerId, User.LocalIp);

            // Starts thread to handle input from clients
            _shouldStop = false;
            var recieve = new Thread(RecieveLoop);
            recieve.Start();

            //Setting up Client 
            Client.Conn = User.LocalIp;
            Client.NetClient.Start();
            Client.Connect();
        }

        /// <summary>
        /// Deletes the server from the database, and stop the NetServer
        /// </summary>
        public static void ServerStop()
        {
            //Disconnects local Client
            Client.ClientStop();
            //Sends message to clients that the server is closing
            var outMsg = _server.CreateMessage();
            outMsg.Write((byte) PacketTypes.Closed);
            _server.SendToAll(outMsg, NetDeliveryMethod.ReliableOrdered);
            //Shuts down server and deletes it from the database
            _shouldStop = true;
            _server.Shutdown("Closed");
            NetworkClasses.DeleteServer(User.PlayerId);
            NetworkClasses.UpdateUserValue("User_List", "_Character", null, User.PlayerId);
        }

        /// <summary>
        /// Main loop to recieve messages from clients
        /// </summary>
        public static void RecieveLoop()
        {
            NetIncomingMessage inc;
            while (!_shouldStop)
            {
                while ((inc = _server.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.StatusChanged:
                            if (inc.SenderConnection.Status == NetConnectionStatus.Disconnected)
                            {
                                Console.WriteLine("Client " + inc.SenderConnection.ToString() + " status changed: " +
                                                  inc.SenderConnection.Status);
                            }
                            break;
                        case NetIncomingMessageType.ConnectionApproval:
                            //Initially approves connecting clients based on their login byte
                            var connectType = inc.ReadByte();
                            if (connectType == (byte)PacketTypes.Login)
                            {
                                inc.SenderConnection.Approve();
                                Players.Add(inc.ReadInt32());
                                if (Players.Count == 6)
                                {
                                    NetworkClasses.UpdateServerValue("Status", "Starting", "Host", User.PlayerId);
                                }

                                Console.WriteLine("Approved new connection");
                                Console.WriteLine(inc.SenderConnection + " has connected");
                            }
                            else if(connectType == (byte)PacketTypes.Spectate)
                            {
                                //TODO
                                inc.SenderConnection.Approve();

                                Console.WriteLine("Approved new spectator");
                                Console.WriteLine(inc.SenderConnection + " has connected");
                                Spectators.Add(inc.SenderConnection);
                                Thread.Sleep(50);
                                SendSpecatorStart(inc.SenderConnection);
                            }

                            break;
                        //The data message type encompasses all messages that aren't related to the running
                        //of the lidgren library, to differentiate, we pass different PacketTypes
                        case NetIncomingMessageType.Data:
                            //can only call readByte once, otherwise it continues reading the following bytes
                            var type = inc.ReadByte();

                            if (type == (byte)PacketTypes.Leave)
                            {
                                if (Players.Count == 6) { NetworkClasses.UpdateServerValue("Status", "Creating", "Host", User.PlayerId); }
                                Players.Remove(inc.ReadInt32());
                            }
                            else if (type == (byte)PacketTypes.Action)
                            {
                                var json = inc.ReadString();
                                var packet = JsonConvert.DeserializeObject<ActionPacket>(json);
                                ReceiveActionUpdate(packet);
                            }

                            else if (type == (byte)PacketTypes.Chat)
                            {
                                var outMsg = _server.CreateMessage();
                                outMsg.Write((byte)PacketTypes.Chat);
                                outMsg.Write(inc.ReadString());
                                _server.SendToAll(outMsg, NetDeliveryMethod.ReliableOrdered);
                            }
                            else if (type == (byte)PacketTypes.Message)
                            {
                                var message = inc.ReadString();
                                PassMessageAlong(message);
                            }
                            else if (type == (byte)PacketTypes.Closed)
                            {
                                ServerStop();
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    _server.Recycle(inc);
                }
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Sends the monter packets to all connected users
        /// </summary>
        public static void StartGame()
        {
            Game.StartTurn();
            SendMonsterPackets(true);
        }

        /// <summary>
        /// Takes in action from client, updates the rest of the clients
        /// </summary>
        /// <param name="packet"></param>
        public static void ReceiveActionUpdate(ActionPacket packet)
        {
            Console.WriteLine(packet.Action);
            GameStateController.AcceptAction(packet);
            //Checks if anyone has won the game
            if (GameStateController.GameOver)
            {
                DeclareWinner();
            }
            else
            {
                SendMonsterPackets(sendDice:
                    packet.Action == Networking.Actions.Action.Roll ||
                    packet.Action == Networking.Actions.Action.EndRolling ||
                    packet.Action == Networking.Actions.Action.SaveDie ||
                    packet.Action == Networking.Actions.Action.UnSaveDie, sendCards: true);
            }
        }

        /// <summary>
        /// Sends packets to clients 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="sendDice"></param>
        /// <param name="sendCards"></param>
        public static void SendMonsterPackets(bool start = false, bool sendDice = false, bool sendCards = false)
        {
            var outMsg = _server.CreateMessage();
            if (start)
            {
                outMsg.Write((byte) PacketTypes.Start);
            }
            else
            {
                outMsg.Write((byte) PacketTypes.Update);
            }
            var packets = MonsterController.GetDataPackets();
            outMsg.Write(packets.Length);
            foreach (var packet in packets)
            {
                var json = JsonConvert.SerializeObject(packet);
                outMsg.Write(json);
            }

            if (sendDice)
            {
                outMsg.Write((byte) PacketTypes.Dice);
                var dice = DiceController.GetDataPacket();
                outMsg.Write(JsonConvert.SerializeObject(dice));
            }
            else if (!start)
            {
                outMsg.Write((byte) PacketTypes.NoDice);
            }

            if (sendCards)
            {
                outMsg.Write((byte) PacketTypes.Cards);
                var cards = CardController.GetCardsForSale().Select(CardController.CreateDataPacket).ToArray();
                outMsg.Write(JsonConvert.SerializeObject(cards));
            }
            else if (!start)
            {
                outMsg.Write((byte) PacketTypes.NoCards);
            }

            _server.SendToAll(outMsg, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendSpecatorStart(NetConnection recipient)
        {
            var outMsg = _server.CreateMessage();
            outMsg.Write((byte)PacketTypes.Spectate);
            var packets = MonsterController.GetDataPackets();
            outMsg.Write(packets.Length);
            foreach (var packet in packets)
            {
                var json = JsonConvert.SerializeObject(packet);
                outMsg.Write(json);
            }

            _server.SendMessage(outMsg, recipient, NetDeliveryMethod.ReliableOrdered);
            Thread.Sleep(50);
        }

        public static void DeclareWinner()
        {
            //send packet type game over, update player stats, show final scores
            if (Game.Winner != null)
            {
                var outMsg = _server.CreateMessage();
                outMsg.Write((byte)PacketTypes.GameOver);
                outMsg.Write(Game.Winner.Name);
                _server.SendToAll(outMsg, NetDeliveryMethod.ReliableOrdered);
            }
        }

        public static void PassMessageAlong(string message)
        {
            var timeStamp = DateTime.Now.ToString("hh:mm");
            message = "[" + timeStamp + "] " + message;
            var outMsg = _server.CreateMessage();
            outMsg.Write((byte) PacketTypes.Message);
            outMsg.Write(message);
            _server.SendToAll(outMsg, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
