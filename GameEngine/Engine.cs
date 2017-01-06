using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameEngine.ServerClasses;
using Microsoft.Xna.Framework.Audio;

namespace GameEngine {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Engine : Game
    {
        public static GraphicsDeviceManager GraffixMngr;

        public static bool ExitGame = false;

        public static InputManager InputManager;
        public static ScreenManager ScreenManager;
        public static SpriteBatch SpriteBatch;
        public static GraphicsDevice GraphicsDev;
        public static Dictionary<string, Texture2D> TextureList;
        public static Dictionary<string, SpriteFont> FontList;
        public static Dictionary<string, SoundEffect> SoundList;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public Engine()
        {
            GraffixMngr = new GraphicsDeviceManager(this);
            GraphicsDev = GraphicsDevice;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GraffixMngr.PreferredBackBufferHeight = 720; //1080
            GraffixMngr.PreferredBackBufferWidth = 1280; //1920
            GraffixMngr.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            ScreenWidth = GraffixMngr.GraphicsDevice.Viewport.Width;
            ScreenHeight = GraffixMngr.GraphicsDevice.Viewport.Height;

            TextureList = new Dictionary<string, Texture2D>();
            LoadTextures();
            FontList = new Dictionary<string, SpriteFont>();
            LoadFonts();
            SoundList = new Dictionary<string, SoundEffect>();
            LoadSounds();
            InputManager = new InputManager(this);
            ScreenManager = new ScreenManager(this);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            SpriteBatch = new SpriteBatch(base.GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            ScreenManager.Unload();
            ScreenManager.Dispose();
            InputManager.Dispose();
            TextureList.Clear();
            FontList.Clear();
            SoundList.Clear();
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (ExitGame)
            {
                UnloadContent();
                this.Exit();
                //Client.CloseServer();
                Client.gameClose = true;
                Client.ClientStop();
            }
            if (ScreenWidth != GraffixMngr.GraphicsDevice.Viewport.Width)
            {
                ScreenWidth = GraffixMngr.GraphicsDevice.Viewport.Width;
                ScreenHeight = GraffixMngr.GraphicsDevice.Viewport.Height;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (ScreenManager.ScreenList.Count == 0) return;
            var index = ScreenManager.ScreenList.Count - 1;
            while (ScreenManager.ScreenList[index].IsPopup)
            {
                index--;
            }
            GraphicsDevice.Clear(ScreenManager.ScreenList[index].BackgroundColor);
            for (var i = index; i < ScreenManager.ScreenList.Count; i++)
            {
                ScreenManager.ScreenList[i].Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        public static string GetResolution()
        {
            return GraffixMngr.PreferredBackBufferWidth == 1280 ? "1280x720" : "1920x1080";
        }

        public static void ChangeResolution(string res)
        {
            if (res.Equals("1280x720"))
            {
                GraffixMngr.PreferredBackBufferWidth = 1280;
                GraffixMngr.PreferredBackBufferHeight = 720;
            }
            else if (res.Equals("1920x1080"))
            {
                GraffixMngr.PreferredBackBufferWidth = 1920;
                GraffixMngr.PreferredBackBufferHeight = 1080;
            }
            GraffixMngr.ApplyChanges();
        }

        internal static void PlaySound(string v)
        {
            SoundList[v].Play();
        }

        #region ContentHelpers

        private void AddTexture(string filePath, string name)
        {
            var toAdd = Content.Load<Texture2D>(filePath);
            TextureList.Add(name, toAdd);
        }

        private void AddFont(string filePath, string name)
        {
            var toAdd = Content.Load<SpriteFont>(filePath);
            FontList.Add(name, toAdd);
        }

        private void AddSound(string filePath, string name)
        {
            var toAdd = Content.Load<SoundEffect>(filePath);
            SoundList.Add(name, toAdd);
        }

        private void LoadTextures()
        {
            //Load Background
            AddTexture("background", "background720");
            AddTexture("background1080", "background1080");
            //Load monster sprites
            AddTexture("monsterTextures\\cthulhu", "cthulhu");
            AddTexture("monsterTextures\\alienoid", "Alienoid");
            AddTexture("monsterTextures\\cyberbunny", "Cyber Bunny");
            AddTexture("monsterTextures\\gigazaur", "Giga Zaur");
            AddTexture("monsterTextures\\kraken", "Kraken");
            AddTexture("monsterTextures\\mekadragon", "Meka Dragon");
            AddTexture("monsterTextures\\pandakai", "Pandakai");
            AddTexture("monsterTextures\\theking", "The King");
            AddTexture("monsterTextures\\therealking", "The Real King");

            //Load dice sprites
            AddTexture("diceTextures\\dice1", "dice1");
            AddTexture("diceTextures\\dice2", "dice2");
            AddTexture("diceTextures\\dice3", "dice3");
            AddTexture("diceTextures\\diceAttack", "diceAttack");
            AddTexture("diceTextures\\diceHealth", "diceHealth");
            AddTexture("diceTextures\\diceEnergy", "diceEnergy");
        }

        private void LoadFonts()
        {
            AddFont("Fonts\\BigFont", "BigFont");
            AddFont("Fonts\\MenuFont", "MenuFont");
            AddFont("Fonts\\Update", "updateFont");
            AddFont("Fonts\\DescripFont", "DescripFont");
        }

        private void LoadSounds()
        {
            AddSound("Sounds\\Recording", "StartTurn");
        }

        #endregion

    }
}
