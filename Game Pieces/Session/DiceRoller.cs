using System;
using System.Collections.Generic;
using System.Linq;
using GamePieces.Dice;
using GamePieces.Monsters;

namespace GamePieces.Session
{
    public static class DiceRoller
    {
        private static readonly Random Random = new Random(); //Random Number Generator
        private static readonly int Faces = Enum.GetNames(typeof(Symbol)).Length; //Number of faces for each die

        private static readonly Stack<Die>  //Black and green dice
            Black = new Stack<Die>(Enumerable.Range(0, 6).Select(die => new Die(Color.Black, Random, Faces))),
            Green = new Stack<Die>(Enumerable.Range(0, 2).Select(die => new Die(Color.Green, Random, Faces)));

        private static readonly int[] Tally = new int[Faces]; //The score of the currently rolled dice

        public static List<Die> Rolling = new List<Die>(); //All of the dice being rolled

        public static int Attack => Tally[(int) Symbol.Attack]; //Attack points total
        public static int Energy => Tally[(int) Symbol.Energy]; //Energy points total
        public static int Heal => Tally[(int) Symbol.Heal]; //Heal points total

        public static int VictoryPoints => //Victory points total
            (Tally[(int) Symbol.One] >= 3 ? 1 + Tally[(int) Symbol.One] - 3 : 0) +
            (Tally[(int) Symbol.Two] >= 3 ? 2 + Tally[(int) Symbol.Two] - 3 : 0) +
            (Tally[(int) Symbol.Three] >= 3 ? 3 + Tally[(int) Symbol.Three] - 3 : 0);

        /// <summary>
        /// Setup the dice for the turn
        /// </summary>
        /// <param name="dice">Number of dice to roll</param>
        internal static void Setup(int dice)
        {
            Array.Clear(Tally, 0, Tally.Length);
            Rolling.ForEach(die => die.Save = false);
            if (Rolling.Count == dice) return;
            while (Rolling.Count != 0)
            {
                if (Rolling[0].Color == Color.Black) Black.Push(Rolling[0]);
                else Green.Push(Rolling[0]);
                Rolling.RemoveAt(0);
            }
            while (dice-- >= 0 && Black.Count != 0) Rolling.Add(Black.Pop());
            while (dice-- >= 0 && Green.Count != 0) Rolling.Add(Green.Pop());
        }

        /// <summary>
        /// Roll all of the dice
        /// </summary>
        internal static void Roll()
        {
            Array.Clear(Tally, 0, Tally.Length);
            foreach (var die in Rolling)
            {
                die.Roll();
                Tally[(int) die.Symbol]++;
            }
        }

        /// <summary>
        /// Stop rolling for this turn and transfer the tally to the rolling monster
        /// </summary>
        /// <param name="monster">Rolling monster</param>
        internal static void EndRolling(Monster monster)
        {
            monster.AttackPoints += Attack;
            monster.Energy += Energy;
            monster.Health += Heal;
            monster.VictoryPoints += VictoryPoints;
            Array.Clear(Tally, 0, Tally.Length);
        }

        public static void AcceptDataPacket(DiceDataPacket dataPacket)
        {
            //TODO
            if(Rolling.Count != dataPacket.Size)
            {
                Setup(dataPacket.Size);
                //throw new Exception("Data packet does not match the current rolling state");
            }
            //TODO
            for(var i = 0; i < Rolling.Count; i++)
            {
                Rolling[i].AcceptPacket(dataPacket.Symbols[i], dataPacket.Colors[i], dataPacket.States[i]);
            }
        }
    }
}