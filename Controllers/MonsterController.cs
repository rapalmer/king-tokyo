using System;
using System.Collections.Generic;
using System.Linq;
using GamePieces.Cards;
using GamePieces.Monsters;
using GamePieces.Session;

namespace Controllers
{
    public static class MonsterController
    {

        /// <summary>
        /// Get all monster data packets for the current game state.
        /// </summary>
        /// <returns>Data Packets</returns>
        public static MonsterDataPacket[] GetDataPackets()
        {
            var dataPackets = new MonsterDataPacket[Game.Monsters.Count];
            for (var i = 0; i < dataPackets.Length; i++)
            {
                dataPackets[i] = Game.Monsters[i].GetPacket();
            }
            return dataPackets;
        }

        /// <summary>
        /// Change the monsters in the game state using the given data packets.
        /// </summary>
        /// <param name="dataPackets">Data Packets</param>
        public static void AcceptDataPackets(MonsterDataPacket[] dataPackets)
        {
            for (var i = 0; i < dataPackets.Length; i++)
            {
                Game.Monsters[i].AcceptPacket(dataPackets[i]);
            }
        }

        /// <summary>
        /// Player's monster name
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Monster Name</returns>
        public static string Name(int playerId)
        {
            return GetById(playerId).Name;
        }

        /// <summary>
        /// Monster before player's monster
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Previous Monster</returns>
        public static Monster Previous(int playerId)
        {
            return GetById(playerId).Previous;
        }

        /// <summary>
        /// Monster after player's monster
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Next Monster</returns>
        public static Monster Next(int playerId)
        {
            return GetById(playerId).Next;
        }

        /// <summary>
        /// State of the player's monster
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>State</returns>
        public static State State(int playerId)
        {
            return GetById(playerId).State;
        }

        /// <summary>
        /// Location of the player's monster
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Location</returns>
        public static Location Location(int playerId)
        {
            return GetById(playerId).Location;
        }

        /// <summary>
        /// Is the player's monster in Tokyo?
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>In Tokyo?</returns>
        public static bool InTokyo(int playerId)
        {
            return GetById(playerId).InTokyo;
        }

        /// <summary>
        /// Can the player's monster yield?
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Can Yield?</returns>
        public static bool CanYield(int playerId)
        {
            return GetById(playerId).CanYield;
        }

        /// <summary>
        /// The player's cards
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Cards</returns>
        public static List<Card> Cards(int playerId)
        {
            return GetById(playerId).Cards;
        }

        /// <summary>
        /// Number of cards in the player's hand
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Number of Cards</returns>
        public static int NumberOfCards(int playerId)
        {
            return GetById(playerId).NumberOfCards;
        }

        /// <summary>
        /// Total energy the player's monster has
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Energy</returns>
        public static int Energy(int playerId)
        {
            return GetById(playerId).Energy;
        }

        /// <summary>
        /// Total victory points the player's monster has
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Victory Points</returns>
        public static int VictoryPoints(int playerId)
        {
            return GetById(playerId).VictoryPoints;
        }

        /// <summary>
        /// Health of the player's monster
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Health</returns>
        public static int Health(int playerId)
        {
            return GetById(playerId).Health;
        }

        /// <summary>
        /// Player's monster's maximum health
        /// </summary>
        /// <param name="playerId">Plater ID</param>
        /// <returns>Maximum Health</returns>
        public static int MaximumHealth(int playerId)
        {
            return GetById(playerId).MaximumHealth;
        }

        /// <summary>
        /// The number of dice the player can roll
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Dice</returns>
        public static int Dice(int playerId)
        {
            return GetById(playerId).Dice;
        }

        /// <summary>
        /// Maximum rolls the player can do per turn
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Maximum Rolls</returns>
        public static int MaximumRolls(int playerId)
        {
            return GetById(playerId).MaximumRolls;
        }

        /// <summary>
        /// Rolls remaining for the player's turn
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Rolls Remaining</returns>
        public static int RollsRemaining(int playerId)
        {
            return GetById(playerId).RemainingRolls;
        }

        /// <summary>
        /// The player yields their monster
        /// </summary>
        /// <param name="playerId">Player ID</param>
        public static void Yield(int playerId)
        {
            GetById(playerId).Yield();
        }

        /// <summary>
        /// Get monster by player ID
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>Monster</returns>
        /// <exception cref="Exception"></exception>
        public static Monster GetById(int playerId)
        {
            if (!Game.Monsters.Exists(monster => monster.PlayerId == playerId))
            {
                throw new Exception("No monster with player ID: " + playerId + " exists in the game");
            }
            return Game.Monsters.First(monster => monster.PlayerId == playerId);
        }

        /// <summary>
        /// Checks if a monster is dead.
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <returns>If the monster is dead.</returns>
        public static bool IsDead(int playerId)
        {
            return !Game.Monsters.Any(monster => monster.PlayerId == playerId);
        }

        
    }
}