
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GameScreens
{
    /// <summary>
    /// The GameScreen to display various options for the game that the user can change.
    /// Currently listen options: 
    /// </summary>
    class OptionsMenu : GameScreen
    {
        private const int OptionPadding = 60;   // The amount of space to leave between options.
        private string[] _menuItems;
        private Vector2 _position;
        private SpriteFont _font;
        private int _stateIndex;
        private int _resolutionState;

        public new bool IsPopup = true;     // Changes default setting so the screen will display over others.

        /// <summary>
        /// Allows the game to run logic such as updating the game objects,
        /// gathering input, and playing sound.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Down cycles downwards through the options displayed.
            if (Engine.InputManager.KeyPressed(Keys.Down))
            {
                if (_stateIndex == (_menuItems.Length - 1))
                {
                    _stateIndex = 0;
                }
                else
                {
                    _stateIndex++;
                }
            }

            // Up cycles upwards through the options dispalyed.
            if (Engine.InputManager.KeyPressed(Keys.Up))
            {
                if (_stateIndex == 0)
                {
                    _stateIndex = _menuItems.Length - 1;
                }
                else
                {
                    _stateIndex--;
                }
            }

            // Escape key is pressed.
            if (Engine.InputManager.KeyPressed(Keys.Escape))
            {
                ScreenManager.RemoveScreen(this);
            }

            // Lets left/right change the resolution value when option is highlighted 
            if ((Engine.InputManager.KeyPressed(Keys.Left) || Engine.InputManager.KeyPressed(Keys.Right)) && _stateIndex == 0)
            {
                if (_resolutionState == 0)
                {
                    _resolutionState = 1;
                    _menuItems[0] = "1920x1080";
                }
                else
                {
                    _resolutionState = 0;
                    _menuItems[0] = "1280x720";
                }
            }

            // Logic for when Enter key is pressed.
            if (Engine.InputManager.KeyPressed(Keys.Enter))
            {
                if (_stateIndex == 1)
                {
                    Engine.ChangeResolution(_menuItems[0]);
                    ScreenManager.RemoveScreen(this);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Called whenever the game should draw.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Engine.SpriteBatch.Begin();
            for (var i = 0; i < _menuItems.Length; i++)
            {
                var text = _menuItems[i];
                var pos = new Vector2(CenterTextX(text, _font), _position.Y + (OptionPadding * i));
                Engine.SpriteBatch.DrawString(_font, text, pos, _stateIndex == i ? Color.Yellow : Color.Black);
            }
            Engine.SpriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Loads the necessary assets for this screen. 
        /// Runs once when calling the screen.
        /// </summary>
        public override void LoadAssets()
        {
            _menuItems = new[] { Engine.GetResolution(), "Back" };
            _font = Engine.FontList["MenuFont"];
            _position = new Vector2(200);
            _stateIndex = 0;
            _resolutionState = 0;
        }

        /// <summary>
        /// Helper function to center text on the screen.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="sF">The SpriteFont that will be used.</param>
        /// <returns>The X value that will center the text horizontally.</returns>
        private static float CenterTextX(string text, SpriteFont sF)
        {
            return ((float)Engine.ScreenWidth / 2) - (sF.MeasureString(text).X / 2);
        }
    }
}
