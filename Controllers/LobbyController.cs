﻿using System;
using System.Collections.Generic;
using GamePieces.Monsters;
using GamePieces.Session;

namespace Controllers
{
    public static class LobbyController
    {
        public static List<Tuple<int, string>> Players { get; }= new List<Tuple<int, string>>();

        /// <summary>
        /// Add player to lobby
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <param name="monsterName">Monster Name</param>
        public static void AddPlayer(int playerId, string monsterName)
        {
            Players.Add(new Tuple<int, string>(playerId, monsterName));
        }

        /// <summary>
        /// Remove player from lobby
        /// </summary>
        /// <param name="playerId">Player ID</param>
        public static void RemovePlayer(int playerId)
        {
            for (var i = 0; i < Players.Count; i++)
            {
                if (Players[i].Item1 != playerId) continue;
                Players.RemoveAt(i);
                return;
            }
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public static void StartGame(MonsterDataPacket[] dataPackets = null)
        {
            if (dataPackets == null)
            {
                var playerIds = new List<int>();
                var monsterNames = new List<string>();
                foreach (var player in Players)
                {
                    playerIds.Add(player.Item1);
                    monsterNames.Add(player.Item2);
                }
                Players.Clear();
                Game.StartGame(playerIds, monsterNames);
            }
            else
            {
                Game.StartGame(dataPackets);
            }
        }
    }
}