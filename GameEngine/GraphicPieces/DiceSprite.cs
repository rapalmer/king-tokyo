using GamePieces.Dice;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using colour = Microsoft.Xna.Framework.Color;

namespace GameEngine.GraphicPieces
{
    /// <summary>
    /// The graphic piece that holds the information to display an individual die.
    /// </summary>
    class DiceSprite
    {
        protected Texture2D CurrentFace;
        protected Die Die;
        protected Vector2 Position;
        protected int Index;
        private DiceRow _diceRow;
        private colour color;

        public bool Save => Die.Save;


        /// <summary>
        /// Creates a new DiceSprite piece
        /// </summary>
        /// <param name="die">The game die this piece will display.</param>
        /// <param name="pos">The position to display this piece at.</param>
        /// <param name="index">The index of the die in relation to the game DiceRoller.</param>
        /// <param name="diceRow">The DiceRow that displays this die.</param>
        public DiceSprite(Die die, Vector2 pos, int index, DiceRow diceRow)
        {
            Die = die;
            Index = index;
            Position = pos;
            _diceRow = diceRow;
            Update();
            color = colour.White;
        }

        public void Roll()
        {
            Die.Roll();
            Update();
        }

        /// <summary>
        /// Updates the sprite so it always displays current face of the die.
        /// </summary>
        public void Update()
        {
            switch (Die.Symbol)
            {
                case Symbol.One:
                    ChangeFace("dice1");
                    break;
                case Symbol.Two:
                    ChangeFace("dice2");
                    break;
                case Symbol.Three:
                    ChangeFace("dice3");
                    break;
                case Symbol.Heal:
                    ChangeFace("diceHealth");
                    break;
                case Symbol.Attack:
                    ChangeFace("diceAttack");
                    break;
                case Symbol.Energy:
                    ChangeFace("diceEnergy");
                    break;
            }
        }

        /// <summary>
        /// Helper function that changes the die's sprite based on string input.
        /// </summary>
        /// <param name="newFace">The new face to display.</param>
        private void ChangeFace(string newFace)
        {
            Texture2D getTexture;
            Engine.TextureList.TryGetValue(newFace, out getTexture);
            CurrentFace = getTexture;
        }

        /// <summary>
        /// Draws the die onto the screen.
        /// </summary>
        /// <param name="sb">The SpriteBatch that is doing the drawing.</param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(CurrentFace, Position, color);
        }

        /// <summary>
        /// Determines if the mouse is hovering over the die.
        /// </summary>
        /// <param name="mouse">Current state of the mouse.</param>
        /// <returns>True if mouse is over die. False otherwise.</returns>
        public bool MouseOver(MouseState mouse)
        {
            return mouse.Position.X > Position.X &&
                   mouse.Position.X < Position.X + CurrentFace.Width &&
                   mouse.Position.Y > Position.Y &&
                   mouse.Position.Y < Position.Y + CurrentFace.Height;
        }

        /// <summary>
        /// Action to take when die is clicked. Sends off save dice message to game host.
        /// </summary>
        public void Click()
        {
            if (_diceRow.Hidden) return;
            if (Die.Save)
            {
                ServerClasses.Client.SendActionPacket(Controllers.GameStateController.UnSaveDie(Index));
                color = colour.White;
            }
            else
            {
                ServerClasses.Client.SendActionPacket(Controllers.GameStateController.SaveDie(Index));
                color = colour.Red;
            }
        }
    }
}
