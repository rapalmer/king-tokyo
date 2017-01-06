
using System.Windows.Forms;
using GameEngine.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;


namespace GameEngine.GameScreens
{
    /// <summary>
    /// The GameScreen that displays when the user hits Escape/"Pause".
    /// </summary>
    class PauseMenu : GameScreen
    {
        private const int OptionPadding = 60;   // The amount of space to leave between options.
        private string[] _menuOptions;
        private Vector2 _position;
        private SpriteFont _font;
        private int _stateIndex;

        public new bool IsPopup = true;     // Changes the default setting so this screen displays over others.

        /// <summary>
        /// Allows the game to run logic such as updating the game objects,
        /// gathering input, and playing sound.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Engine.InputManager.KeyPressed(Keys.Down))
            {
                if (_stateIndex == (_menuOptions.Length - 1))
                {
                    _stateIndex = 0;
                }
                else
                {
                    _stateIndex++;
                }
            }

            if (Engine.InputManager.KeyPressed(Keys.Up))
            {
                if (_stateIndex == 0)
                {
                    _stateIndex = _menuOptions.Length - 1;
                }
                else
                {
                    _stateIndex--;
                }
            }

            if (Engine.InputManager.KeyPressed(Keys.Escape))
            {
                ScreenManager.RemoveScreen(this);
            }

            if (Engine.InputManager.KeyPressed(Keys.Enter))
            {
                switch (_stateIndex)
                {
                    case 0:
                        ScreenManager.RemoveScreen(this);
                        break;
                    case 1:
                       ScreenManager.AddScreen(new OptionsMenu());
                        break;
                    case 2:
                        Engine.ExitGame = true;
                        ScreenManager.RemoveScreen(this);
                        break;
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
            for(var i = 0; i < _menuOptions.Length; i++)
            {
                var text = _menuOptions[i];
                var pos = new Vector2(GetCenter(text, _font), _position.Y + (OptionPadding * i));
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
            _menuOptions = new[] {"Resume", "Options", "Exit"};
            _font = Engine.FontList["MenuFont"];
            _position = new Vector2(200);
            _stateIndex = 0;
        }

        // TODO: Extract to remove duplicate function (also found in optionsMenu
        private static float GetCenter(string text, SpriteFont sF)
        {
            return ((float)Engine.ScreenWidth/2) - (sF.MeasureString(text).X/2);
        }
    }
}
