using System;

namespace GamePieces.Dice
{
    public class Die
    {
        private readonly Random Random; //Random Number Generator
        private readonly int Faces; //Number of faces on the die

        public Color Color { get; private set; } //Black or Green
        public Symbol Symbol { get; private set; } //Showing face symbol
        public bool Save { get; set; } //Saving prevents the showing face value from  being changed

        /// <summary>
        /// An N-Sided Die
        /// </summary>
        /// <param name="color">Color of the die</param>
        /// <param name="random">Reference to a 'Random'</param>
        /// <param name="faces">Number of faces on the die</param>
        public Die(Color color, Random random, int faces)
        {
            Color = color;
            Symbol = 0;
            Save = false;
            Random = random;
            Faces = faces;
        }

        /// <summary>
        /// Randomly pick a new value for the showing face
        /// </summary>
        public void Roll()
        {
            if (!Save) Symbol = (Symbol) Random.Next(0, Faces);
        }

        internal void AcceptPacket(Symbol symbol, Color color, bool save)
        {
            Symbol = symbol;
            Color = color;
            Save = save;
        }
    }
}