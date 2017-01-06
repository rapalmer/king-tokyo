using System.Collections.Generic;
using GameEngine.GameScreens;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.ServerClasses;

namespace GameEngine.GraphicPieces

{
    internal class ServerUpdateBox
    {
        private const int Width = 450;
        private const int Height = 150;
        private readonly SpriteFont _font;
        private readonly Texture2D _backgroundRect;
        private Vector2 _positionVector;
        private List<string> _stringList;
        private const int LineSpacing = 20;

        public ServerUpdateBox(SpriteFont font)
        {
            _font = font;
            _positionVector = MainGameScreen.ScreenLocations.GetPosition("ServerUpdateBox");
            _stringList = new List<string>();
            _backgroundRect = GetBackground();
        }

        private static Texture2D GetBackground()
        {
            var bg = new Texture2D(Engine.GraffixMngr.GraphicsDevice,  Width, Height, false, SurfaceFormat.Color);
            var colorData = new Color[Width * Height];
            for (var i = 0; i < Width * Height; i++)
            {
                colorData[i] = Color.Black;
            }
            bg.SetData(colorData);
            return bg;
        }

        public void UpdateList()
        {
            _positionVector = MainGameScreen.ScreenLocations.GetPosition("ServerUpdateBox");
            _stringList = null;
            _stringList = Client.MessageHistory.Count < 8 ? Client.MessageHistory : Client.MessageHistory.GetRange(Client.MessageHistory.Count - 8, 7);
        }

        public void Draw(SpriteBatch sB)
        {
            sB.Draw(_backgroundRect, _positionVector, Color.Black);
            var textPos = _positionVector;
            foreach (var line in _stringList)
            {
                sB.DrawString(_font, line, textPos, Color.WhiteSmoke);
                textPos.Y = textPos.Y + LineSpacing;
            }
        }
    }
}