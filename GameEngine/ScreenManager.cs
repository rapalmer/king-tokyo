using GameEngine.GameScreens;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    /// <summary>
    /// A GameComponent class for managing the different screens of the game.
    /// </summary>
    public class ScreenManager : GameComponent
    {
        public static List<GameScreen> ScreenList;

        /// <summary>
        /// Creates a new ScreenManager and adds it to the Game's components.
        /// </summary>
        /// <param name="game">The game this ScreenManager belongs to.</param>
        public ScreenManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        /// <summary>
        /// Called once when ScreenManager is created. Adds the default screen of the game.
        /// </summary>
        public override void Initialize()
        {
            AddScreen(new MainGameScreen());
            base.Initialize();
        }

        /// <summary>
        /// Update logic for the game screens. Determines which screens are currently displayed and updates
        /// that are currently running.
        /// </summary>
        /// <param name="gameTime">Snapshot of the game time</param>
        public override void Update(GameTime gameTime)
        {
            if (ScreenList.Count == 0) return;
            var index = ScreenList.Count - 1;
            while (ScreenList[index].IsPopup &&
                   ScreenList[index].IsActive)
            {
                index--;
            }

            for (var i = index; i < ScreenList.Count; i++)
            {
                ScreenList[i].Update(gameTime);
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Adds a screen to the ScreenManager.
        /// </summary>
        /// <param name="newScreen">Screen to be added.</param>
        public static void AddScreen(GameScreen newScreen)
        {
            if (ScreenList == null) { ScreenList = new List<GameScreen>(); }
            if (ScreenList.Any(screen => screen.GetType() == newScreen.GetType())) { return; }
            ScreenList.Add(newScreen);
            newScreen.LoadAssets();
        }

        /// <summary>
        /// Removes a screen from the ScreenManager.
        /// </summary>
        /// <param name="screen">Screen to be removed.</param>
        public static void RemoveScreen(GameScreen screen)
        {
            screen.UnloadAssets();
            ScreenList.Remove(screen);
            if (ScreenList.Count < 1) { AddScreen(new TestScreen()); }
        }

        /// <summary>
        /// Function to swap from one screen to another.
        /// </summary>
        /// <param name="currentScreen">Currently displayed screen.</param>
        /// <param name="nextScreen">Screen that needs to be displayed.</param>
        public static void ChangeScreens(GameScreen currentScreen, GameScreen nextScreen)
        {
            RemoveScreen(currentScreen);
            AddScreen(nextScreen);
        }

        /// <summary>
        /// Unloads the assets of all managed screens.
        /// </summary>
        public void Unload()
        {
            foreach(var screen in ScreenList)
            {
                screen.UnloadAssets();
            }
        }

        /// <summary>
        /// Called when closing the game. Unloads all content and cleans ScreenManager.
        /// </summary>
        public void Exit()
        {
            Unload();
            ScreenList.Clear();
        }
    }
}
