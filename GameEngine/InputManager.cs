using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    /// <summary>
    /// A GameComponent class for all input related functions in the game.
    /// </summary>
    public class InputManager : GameComponent
    {
        private KeyboardState _freshKeyboardState, _oldKeyboardState;
        private MouseState _oldMouseState;
        public MouseState FreshMouseState { get; private set; }

        public InputManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public bool KeyPressed(Keys key)
        {
            return (_freshKeyboardState.IsKeyDown(key) && 
                    _oldKeyboardState.IsKeyUp(key));
        }

        public bool LeftClick()
        {
            return (FreshMouseState.LeftButton == ButtonState.Pressed &&
                    _oldMouseState.LeftButton == ButtonState.Released);
        }

        public override void Initialize()
        {
            _oldKeyboardState = _freshKeyboardState = Keyboard.GetState();
            _oldMouseState = FreshMouseState = Mouse.GetState();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Keyboard Updates
            _oldKeyboardState = _freshKeyboardState;
            _freshKeyboardState = Keyboard.GetState();
            //Mouse Updates
            _oldMouseState = FreshMouseState;
            FreshMouseState = Mouse.GetState();
            //base
            base.Update(gameTime);
        }

    }
}
