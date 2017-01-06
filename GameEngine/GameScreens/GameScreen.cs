using Microsoft.Xna.Framework;

namespace GameEngine.GameScreens
{
    /// <summary>
    /// Parent class that contains the default values/functions all GameScreens inherit.
    /// </summary>
    public class GameScreen
    {
        public bool IsActive = true;
        public bool IsPopup = false;
        public Color BackgroundColor = Color.CornflowerBlue;

        public virtual void LoadAssets() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
        public virtual void UnloadAssets() { }
    }
}
