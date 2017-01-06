using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace GameEngine.GameScreens
{
    internal class ScreenLocations
    {
        private const int PlayerBlockLength = 300;
        private const int PlayerBlockHeight = 200;
        private const int DefaultPadding = 10;
        private const int LazyPaddingForDice = 230;
        private Dictionary<string, Vector2> _positions;
        private int _screenWidth;
        private int _screenHeight;

        public ScreenLocations()
        {
            _screenWidth = 0;
            _screenHeight = 0;
            Update();
        }

        public void Update()
        {
            if (_screenWidth == Engine.GraffixMngr.GraphicsDevice.Viewport.Width) return;
            _screenWidth = Engine.GraffixMngr.GraphicsDevice.Viewport.Width;
            _screenHeight = Engine.GraffixMngr.GraphicsDevice.Viewport.Height;
            if (_screenHeight == 720)
            {
                MainGameScreen.BackgroundImage = Engine.TextureList["background720"];
            }
            else
            {
                MainGameScreen.BackgroundImage = Engine.TextureList["background1080"];
            }
            _positions = new Dictionary<string, Vector2>()
            {
                {"TopLeft", new Vector2(DefaultPadding, DefaultPadding)},
                {"TopCenter", new Vector2((_screenWidth/2) - (PlayerBlockLength/2), DefaultPadding)},
                {"TopRight", new Vector2(_screenWidth - DefaultPadding - PlayerBlockLength, DefaultPadding)},
                {"MidLeft", new Vector2(DefaultPadding, ((_screenHeight/2) - (PlayerBlockHeight/2)) - 50)},
                {"MidRight", new Vector2(_screenWidth - DefaultPadding - PlayerBlockLength, ((_screenHeight/2) - (PlayerBlockHeight/2)) - 50)},
                {"BottomCenter", new Vector2((_screenWidth/2) - (PlayerBlockLength/2), _screenHeight - PlayerBlockHeight)},
                {"TokyoCity", new Vector2(400, 225) },
                {"TokyoBay", new Vector2(650, 300) },
                {"DicePos", new Vector2(DefaultPadding, _screenHeight - LazyPaddingForDice)},
                {"TextPrompt1", new Vector2(_screenWidth - 400, _screenHeight - 100) },
                {"TextPrompt2", new Vector2(_screenWidth - 400, _screenHeight - 75) },
                {"RollPrompt", new Vector2(DefaultPadding, _screenHeight - 350) },
                {"RollsLeft", new Vector2(_screenWidth - 400, _screenHeight - 200) },
                {"WinText", new Vector2(_screenWidth - 400, _screenHeight - 100) },
                {"YieldPrompt", new Vector2(DefaultPadding, _screenHeight - 350) },
                {"RollingText", new Vector2(DefaultPadding, _screenHeight - 350) },
                {"BuyCardsPrompt", new Vector2(DefaultPadding, _screenHeight - 350) },
                {"GameOver", new Vector2(_screenWidth - 400, _screenHeight - 100) },
                {"ServerUpdateBox", new Vector2(10, _screenHeight - 160)  },
                {"cardList", new Vector2(_screenWidth - 400, _screenHeight - 200)  },
                {"RollButton", new Vector2(10, _screenHeight - 265) }
            };
        }

        public Vector2 GetPosition(string key)
        {
            return _positions[key];
        }
    }
}
