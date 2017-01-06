using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameEngine.GameScreens;

namespace GameEngine.GraphicPieces
{
    /// <summary>
    /// Class for displaying text blocks onto the screen.
    /// Used for displaying prompts to the user
    /// </summary>
    internal class TextBlock
    {
        private const int LineSpacing = 30;
        private readonly List<string> _text;
        public string Name { get; }
        public Vector2 Position { get; set; }

        public TextBlock(string name, List<string> text)
        {
            Name = name;
            _text = text;
            Position = MainGameScreen.ScreenLocations.GetPosition(name);
        }

        public void Draw(SpriteBatch sb)
        {
            SpriteFont font;
            Engine.FontList.TryGetValue("BigFont", out font);
            var pos = Position;
            foreach (string line in _text)
            {
                pos.Y = pos.Y + LineSpacing;
                sb.DrawString(font, line, pos, Color.Black);
            }
        }
    }
}
