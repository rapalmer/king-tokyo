using System.Collections.Generic;
using GamePieces.Dice;
using GamePieces.Session;
using System;

namespace Controllers
{
    public static class DiceController
    {
        /// <summary>
        /// Get the dice packet for the current game state.
        /// </summary>
        /// <returns>Data Packets</returns>
        public static DiceDataPacket GetDataPacket()
        {
            var dicePacket = new DiceDataPacket(DiceRoller.Rolling);
            return dicePacket;
        }

        /// <summary>
        /// Change the monsters in the game state using the given data packets.
        /// </summary>
        /// <param name="dataPackets">Data Packets</param>
        public static void AcceptDataPacket(DiceDataPacket dataPacket)
        {
            DiceRoller.AcceptDataPacket(dataPacket);
        }

        /// <summary>
        /// Gets all of the dice being rolled
        /// </summary>
        /// <returns>Dice</returns>
        public static List<Die> GetDice()
        {
            return DiceRoller.Rolling;
        }

        /// <summary>
        /// Save the die at the given index
        /// </summary>
        /// <param name="index">Index</param>
        public static void SaveDie(int index)
        {
            if (index < 0 || index > DiceRoller.Rolling.Count) return;
            DiceRoller.Rolling[index].Save = true;
        }

        /// <summary>
        /// Un-save the die at the given index
        /// </summary>
        /// <param name="index">Index</param>
        public static void UnSaveDie(int index)
        {
            if (index < 0 || index > DiceRoller.Rolling.Count) return;
            DiceRoller.Rolling[index].Save = false;
        }

        /// <summary>
        /// Roll the dice
        /// </summary>
        public static void Roll()
        {
            Game.Roll();
        }

        /// <summary>
        /// End rolling dice
        /// </summary>
        public static void EndRolling()
        {
            Game.EndRolling();
        }
    }
}