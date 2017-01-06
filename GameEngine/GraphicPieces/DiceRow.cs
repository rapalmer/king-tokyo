using System;
using System.Collections.Generic;
using GamePieces.Dice;
using Microsoft.Xna.Framework;

namespace GameEngine.GraphicPieces
{
    /// <summary>
    /// A graphics piece that is used to display text in a row onto the screen.
    /// </summary>
    class DiceRow
    {
        public List<DiceSprite> DiceSprites { get; }
        private Vector2 _position;
        private const int Padding = 75;     // Spacing between dice displayed
        public bool Hidden { get; set; }    // Hides the dice when user is not rolling

        /// <summary>
        /// Creates a new dice row at specified position.
        /// </summary>
        /// <param name="pos">The Vector2 location to begin drawing.</param>
        public DiceRow(Vector2 pos)
        {
            DiceSprites = new List<DiceSprite>();
            _position = pos;
            Hidden = true;
        }
       
        /// <summary>
        /// Adds the dice from the DiceList to the row.
        /// </summary>
        /// <param name="dL">List of dice the user is rolling.</param>
        public void AddDice(List<Die> dL)
        {
            foreach (var die in dL)
            {
                var diePos = new Vector2(_position.X + (DiceSprites.Count * Padding), _position.Y);
                DiceSprites.Add(new DiceSprite(die, diePos, DiceSprites.Count, this));
            }
        }

        public void setPosition(Vector2 pos)
        {
            _position = pos;
        }

        public void Clear()
        {
            DiceSprites.Clear();
        }
    }
}
