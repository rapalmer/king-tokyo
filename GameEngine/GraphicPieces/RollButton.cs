using GameEngine.GameScreens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GraphicPieces
{
    class RollButton
    {
        private const int Width = 75;
        private const int Height = 30;
        private readonly SpriteFont _font;
        private readonly Texture2D _background;
        private Vector2 _position;
        private Color _color;
        private Vector2 _textPosition;
        private const string Text = "Roll";
        public bool Hidden { get; set; }

        public RollButton()
        {
            Hidden = true;
            _color = Color.DarkRed;
            _background = GetBackground();
            _font = Engine.FontList["updateFont"];
            _position = MainGameScreen.ScreenLocations.GetPosition("RollButton");
            _textPosition = new Vector2(_position.X + 25, _position.Y + 10);
        }

        public void Draw(SpriteBatch sB)
        {
            sB.Draw(_background, _position, _color);
            sB.DrawString(_font, Text, _textPosition, Color.White);
        }

        public void Update()
        {
            _position = MainGameScreen.ScreenLocations.GetPosition("RollButton");
            _textPosition = new Vector2(_position.X + 25, _position.Y + 10);
            MouseState mouseState = Engine.InputManager.FreshMouseState;
            if (MouseOver(mouseState))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    _color = Color.Blue;
                }
                else
                {
                    _color = Color.DarkRed;
                }
            }
            else
            {
                _color = Color.DarkRed;
            }
        }

    /// <summary>
        /// Determines if the mouse is hovering over the die.
        /// </summary>
        /// <param name="mouse">Current state of the mouse.</param>
        /// <returns>True if mouse is over die. False otherwise.</returns>
        public bool MouseOver(MouseState mouse)
        {
            return mouse.Position.X > _position.X &&
                   mouse.Position.X < _position.X + Width &&
                   mouse.Position.Y > _position.Y &&
                   mouse.Position.Y < _position.Y + Height;
        }

        private static Texture2D GetBackground()
        {
            var bg = new Texture2D(Engine.GraffixMngr.GraphicsDevice, Width, Height, false, SurfaceFormat.Color);
            var colorData = new Color[Width * Height];
            for (var i = 0; i < Width * Height; i++)
            {
                colorData[i] = Color.DarkRed;
            }
            bg.SetData(colorData);
            return bg;
        }

    }
}
