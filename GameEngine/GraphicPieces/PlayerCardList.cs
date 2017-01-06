using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GraphicPieces
{
    class PlayerCardList
    {
        private const int width = 250;
        private const int height = 250;
        private readonly SpriteFont _font;
        private readonly Texture2D _background;
        public Vector2 BoxPosition;
        public List<string> _stringList;
        private const int LineSpacing = 20;
        public bool Hidden;

        public PlayerCardList()
        {
            _font = Engine.FontList["updateFont"];
            BoxPosition = Vector2.Zero;
            Hidden = true;
            _background = GetBackground();
        }

        private static Texture2D GetBackground()
        {
            var bg = new Texture2D(Engine.GraffixMngr.GraphicsDevice, width, height, false, SurfaceFormat.Color);
            var colorData = new Color[width * height];
            for (var i = 0; i < width * height; i++)
            {
                colorData[i] = Color.Black;
            }
            bg.SetData(colorData);
            return bg;
        }

        public void Draw(SpriteBatch sB)
        {
            sB.Draw(_background, BoxPosition, Color.Black);
            var textPos = BoxPosition;
            int i = 0;
            foreach (var line in _stringList)
            {
                if (i == 0)
                {
                    sB.DrawString(_font, line, textPos, Color.White);
                    i = 1;
                }
                else if (i == 1)
                {
                    sB.DrawString(_font, line, textPos, Color.AntiqueWhite);
                    i = 0;
                }
                textPos.Y = textPos.Y + LineSpacing;
            }
        }

    }
}
