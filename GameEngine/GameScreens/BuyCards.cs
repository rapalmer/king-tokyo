
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GamePieces.Cards;
using GamePieces.Cards.Deck.Discard;
using GamePieces.Cards.Deck.Keep;
using GamePieces.Session;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GameScreens
{
    internal class BuyCards : GameScreen
    {
        private const int OptionPadding = 120;
        private readonly List<Card> _cardList;
        private readonly SpriteFont _font;
        private readonly SpriteFont _descripFont;
        private readonly int _energy;
        private int _selected;
        private Vector2 _position;
        public new bool IsPopup = true;

        public BuyCards(int energy)
        {
            _cardList = GamePieces.Session.Game.CardsForSale;
            _energy = energy;
            _font = Engine.FontList["MenuFont"];
            _descripFont = Engine.FontList["DescripFont"];
            _selected = 0;
            _position = new Vector2(200);
        }

        public override void Update(GameTime gameTime)
        {
            if (Engine.InputManager.KeyPressed(Keys.Down))
            {
                if (_selected == (_cardList.Count - 1))
                {
                    _selected = 0;
                }
                else
                {
                    _selected++;
                }
            }

            if (Engine.InputManager.KeyPressed(Keys.Up))
            {
                if (_selected == 0)
                {
                    _selected = _cardList.Count - 1;
                }
                else
                {
                    _selected--;
                }
            }

            if (Engine.InputManager.KeyPressed(Keys.Enter))
            {
                if (_cardList[_selected].Cost <= _energy)
                {
                    MainGameScreen.CardScreenChoice = _selected;
                    ScreenManager.RemoveScreen(this);
                }
                else
                {
                    Console.WriteLine("You do not have enough energy for this! Try again, or press escape to leave.");
                }
            }

            if (Engine.InputManager.KeyPressed(Keys.Escape))
            {
                MainGameScreen.CardScreenChoice = -2;
                ScreenManager.RemoveScreen(this);
            }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var energyPos = new Vector2(_position.X, _position.Y - 15);
            Engine.SpriteBatch.Begin();
            var energyText = "Current Energy: " + _energy;
            var posE = new Vector2(GetCenter(energyText, _font), 50);
            Engine.SpriteBatch.DrawString(_font, "Current Energy: " + _energy, posE, Color.DarkRed);
            for (var i = 0; i < _cardList.Count; i++)
            {
                var text = _cardList[i].Name + " cost: " + _cardList[i].Cost + " type: " + _cardList[i].CardType;
                var pos = new Vector2(GetCenter(text, _font), _position.Y + (OptionPadding * i));
                Engine.SpriteBatch.DrawString(_font, text, pos, _selected == i ? Color.Yellow : Color.Black);
                var text2 = _cardList[i].GetDescrip();
                var pos2 = new Vector2(GetCenter(text2, _descripFont), pos.Y + 60);
                Engine.SpriteBatch.DrawString(_descripFont, text2, pos2, Color.Black);
            }

            Engine.SpriteBatch.End();

            base.Draw(gameTime);
        }

        private static float GetCenter(string text, SpriteFont sF)
        {
            return ((float)Engine.ScreenWidth / 2) - (sF.MeasureString(text).X / 2);
        }

    }
}
