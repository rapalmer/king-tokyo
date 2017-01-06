using System;
using GamePieces.Cards;
using System.Collections.Generic;
using System.Linq;

namespace GamePieces.Dice
{
    [Serializable]
    public struct DiceDataPacket
    {
        public int Size { get; set; }
        public Symbol[] Symbols { get; set; }
        public Color[] Colors { get; set; }
        public bool[] States { get; set; }

        public DiceDataPacket(List<Die> Dice)
        {
            Size = Dice.Count;
            Symbols = Dice.Select(die => die.Symbol).ToArray();
            Colors = Dice.Select(die => die.Color).ToArray();
            States = Dice.Select(die => die.Save).ToArray();
        }
    }
}