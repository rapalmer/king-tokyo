using Controllers;
using GamePieces.Dice;
using GamePieces.Monsters;
using Lidgren.Network;
using Networking;
using Networking.Actions;
using Newtonsoft.Json;
using System;
using System.Threading;
using GameEngine.GameScreens;
using GamePieces.Cards;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.ServerClasses
{
    /// <summary>
    /// Client class holds the NetClient from Lidgren
    /// </summary>
    public static class Client
    {
        public static string Conn = "";
        public static NetClient NetClient { get; } = new NetClient(new NetPeerConfiguration("King of Ames"));
        private static Thread _loop;
        private static Thread GameLoop;
        private static bool _shouldStop;
        public static MonsterDataPacket[] MonsterPackets;
        public static bool CanContinue = true;
        public static bool IsStart = false;
        public static bool isSpectator = false;
        public static bool gameClose = false;
        public static List<string> MessageHistory = new List<string>();
        public static List<string> ChatHistory = new List<string>();

        /// <summary>
        /// Connects the client to the server using the current ip
        /// </summary>
        /// <returns>returns true if connected, false otherwise</returns>
        public static bool Connect(bool login = true)
        {
            //Sends login request to Host, with player ID attached
            var outMsg = NetClient.CreateMessage();
            if (login)
            {
                outMsg.Write((byte)PacketTypes.Login);
                isSpectator = false;
            }
            else
            {
                outMsg.Write((byte)PacketTypes.Spectate);
                isSpectator = true;
            }
            outMsg.Write(User.PlayerId);

            //resets the receive thread
            _shouldStop = false;
            _loop = new Thread(RecieveLoop);
            _loop.Start();

            NetClient.Connect(Conn, 6969, outMsg);
            return true;
        }

        /// <summary>
        /// Main loop to recieve messages from the server
        /// </summary>
        public static void RecieveLoop()
        {
            NetIncomingMessage inc;
            while (!_shouldStop)
            {
                while ((inc = NetClient.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.Error:
                            Console.WriteLine(inc.ToString());
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            Console.WriteLine("Status changed: " + inc.SenderConnection.Status);
                            if (inc.SenderConnection.Status == NetConnectionStatus.Disconnected)
                            {
                                NetClient.Shutdown("Closed");
                                //ends the receive loop
                                _shouldStop = true;
                                Conn = "";
                            }
                            break;
                        case NetIncomingMessageType.Data:
                            var type = inc.ReadByte();
                            if (type == (byte)PacketTypes.Start)
                            {
                                var end = inc.ReadInt32();
                                MonsterPackets = new MonsterDataPacket[end];
                                for (var i = 0; i < end; i++)
                                {
                                    var json = inc.ReadString();
                                    MonsterPackets[i] = JsonConvert.DeserializeObject<MonsterDataPacket>(json);
                                }

                                LobbyController.StartGame(MonsterPackets);
                                //Makes this thread a STAThread, not sure if necessary...
                                GameLoop = new Thread(Program.Run);
                                GameLoop.SetApartmentState(ApartmentState.STA);
                                GameLoop.Start();
                            }
                            else if (type == (byte)PacketTypes.Spectate)//The initial message to catch the new spectator up
                            {
                                var end = inc.ReadInt32();
                                MonsterPackets = new MonsterDataPacket[end];
                                for (var i = 0; i < end; i++)
                                {
                                    var json = inc.ReadString();
                                    MonsterPackets[i] = JsonConvert.DeserializeObject<MonsterDataPacket>(json);
                                }

                                LobbyController.StartGame(MonsterPackets);
                                //Makes this thread a STAThread, not sure if necessary...
                                GameLoop = new Thread(Program.Run);
                                GameLoop.SetApartmentState(ApartmentState.STA);
                                GameLoop.Start();
                            }
                            else if (type == (byte)PacketTypes.Update)
                            {
                                var end = inc.ReadInt32();
                                MonsterPackets = new MonsterDataPacket[end];
                                for (var i = 0; i < end; i++)
                                {
                                    var json = inc.ReadString();
                                    MonsterPackets[i] = JsonConvert.DeserializeObject<MonsterDataPacket>(json);
                                }

                                MonsterController.AcceptDataPackets(MonsterPackets);

                                if (inc.ReadByte() == (byte)PacketTypes.Dice)
                                {
                                    var diceJson = inc.ReadString();
                                    var dice = JsonConvert.DeserializeObject<DiceDataPacket>(diceJson);
                                    DiceController.AcceptDataPacket(dice);
                                }
                                else
                                {
                                    Console.Error.WriteLine("No Dice! (╯°□°）╯︵ ┻━┻");
                                }

                                if (inc.ReadByte() == (byte)PacketTypes.Cards)
                                {
                                    var cardJson = inc.ReadString();
                                    var cardsDataPackets = JsonConvert.DeserializeObject<CardDataPacket[]>(cardJson);
                                    CardController.SetCardsForSale(cardsDataPackets.ToList()
                                        .Select(CardController.AcceptDataPacket)
                                        .ToList());
                                }
                                else
                                {
                                    Console.Error.WriteLine("No Cards! (╯°□°）╯︵ ┻━┻");
                                }

                                CanContinue = true;
                            }
                            else if (type == (byte)PacketTypes.Closed)
                            {
                                NetClient.Shutdown("Closed");
                                isSpectator = false;
                                Conn = "";
                                break;
                            }
                            else if (type == (byte)PacketTypes.GameOver)
                            {
                                Console.WriteLine("Game Over!");
                                MainGameScreen.gameOver = true;
                                var winnerName = inc.ReadString();
                                if (winnerName == NetworkClasses.GetUserValue("_Character").ToString())
                                {
                                    NetworkClasses.AddWin(User.PlayerId);
                                }
                                MainGameScreen.EndGame(winnerName);
                            }
                            else if (type == (byte)PacketTypes.Message)
                            {
                                var message = inc.ReadString();
                                MessageHistory.Add(message);
                            }
                            else if (type == (byte)PacketTypes.Chat)
                            {
                                ChatHistory.Add(inc.ReadString());
                            }
                            break;
                        case NetIncomingMessageType.WarningMessage:
                            Console.WriteLine("WARNING");
                            break;
                        default://TODO catch attempts to connect non-existing servers
                            Console.WriteLine("Unhandled message of type: " + inc.MessageType);
                            //throw new ArgumentOutOfRangeException();
                            break;
                    }
                    NetClient.Recycle(inc);
                }
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Sends local action to server to update the game status
        /// </summary>
        /// <param name="packet"></param>
        public static void SendActionPacket(ActionPacket packet)
        {
            if (!MainGameScreen.gameOver)
            {
                while (!CanContinue)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Sleeping packet type: " + packet.Action);
                }
            }

            var outMsg = NetClient.CreateMessage();
            outMsg.Write((byte) PacketTypes.Action);
            var json = JsonConvert.SerializeObject(packet);
            JsonConvert.DeserializeObject<ActionPacket>(json);
            outMsg.Write(json);
            NetClient.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
            CanContinue = false;
        }

        //Sends a message to the server, the server will return the message to all clients, which will then add it to their message history
        public static void SendMessage(string message)
        {
            var outMsg = NetClient.CreateMessage();
            outMsg.Write((byte) PacketTypes.Message);
            outMsg.Write(message);
            NetClient.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendChatMessage(string message)
        {
            var outMsg = NetClient.CreateMessage();
            outMsg.Write((byte)PacketTypes.Chat);
            outMsg.Write(message);
            NetClient.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }

        public static void CloseServer()
        {
            var outMsg = NetClient.CreateMessage();
            outMsg.Write((byte)PacketTypes.Closed);
            NetClient.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// Tells the server to delete it from list, stops loop and shuts down NetClient
        /// </summary>
        public static void ClientStop()
        {
            var outMsg = NetClient.CreateMessage();
            outMsg.Write((byte) PacketTypes.Leave);
            outMsg.Write(User.PlayerId);
            NetClient.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
            NetClient.WaitMessage(1000);
            NetClient.Shutdown("Closed");
            //ends the receive loop
            _shouldStop = true;
            Conn = "";
            isSpectator = false;
            NetworkClasses.UpdateUserValue("User_List", "_Character", null, User.PlayerId);
            GameLoop = new Thread(Program.Run);
            if (GameLoop.IsAlive) { GameLoop.Abort(); }
        }
    }
}
