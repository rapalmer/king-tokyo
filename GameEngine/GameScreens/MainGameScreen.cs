using System.Collections.Generic;
using Controllers;
using Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using GameEngine.GraphicPieces;
using GameEngine.ServerClasses;
using GamePieces.Monsters;
using GamePieces.Session;
using Microsoft.Xna.Framework.Graphics;
using GamePieces.Dice;
using GamePieces.Cards;

namespace GameEngine.GameScreens
{
    class MainGameScreen : GameScreen
    {
        public static ScreenLocations ScreenLocations;  //This is a static list of locations to be used for where to display Playerblocks (e.g., Mid-Left, Mid-Center)
        private readonly List<PlayerBlock> _pBlocks;    //A list of PlayerBlocks used to display player images + text related to their status
        private static List<TextBlock> _textPrompts;    //A list of textBlocks that are updated to display text on the screen as needed
        private static DiceRow _diceRow;      //The dice that are updated to show what the player's real roll is
        //private DiceRow RollingDice { get; }        //The dice that are displayed during roll animation
        //private int RollAnimation { get; set; }     //int used for counting dice roll animation
        public ServerUpdateBox ServerUpdateBox;     //A box use used to display update messages from the server
        private static int _localPlayer;        //int used to store the local player's ID number
        private static Monster _localMonster;   //Copy of the local player's "monster" object used for display purposes
        private bool _firstPlay = true;     //bool for deciding if it is the first time a player has entered the StartingTurn() function
        private List<Monster> _monsterList;     //list of Monsters in the server
        private TextBlock _cardForSaleList;     //A TextBlock used specifically to show the cards currently up for sale
        public static int CardScreenChoice = -1;    //int changed based on the player's choice in the BuyCardScreen(), intialized to -1 for no choice.
        public static GameState _gameState = GameState.Waiting;       //Initialize the local gamestate to waiting to prevent conflicts
        public static Texture2D BackgroundImage;     //Will need to change based on resolution. Currently 720 only.
        private static RollButton _rollButton;
        private static string _winner = null;
        public static bool gameOver = false;
        public PlayerCardList playerCardList;

        /// <summary>
        /// The constuctor for the MainGameScreen(). Initializes the various local objects/values
        /// for display purposes. Also, detects whether a player is a spectator and acts accordingly.
        /// </summary>
        public MainGameScreen()
        {
            BackgroundImage = Engine.TextureList["background720"];
            ScreenLocations = new ScreenLocations();
            ServerUpdateBox = new ServerUpdateBox(Engine.FontList["updateFont"]);
            _localPlayer = User.PlayerId;
            if (Client.isSpectator)
            {
                _gameState = GameState.Spectating;
                _localPlayer = MonsterController.GetDataPackets()[0].PlayerId;
            }
            _localMonster = MonsterController.GetById(_localPlayer);
            _monsterList = GetMonsterList();
            _textPrompts = new List<TextBlock>();
            _pBlocks = GetPlayerBlocks();
            _diceRow = new DiceRow(ScreenLocations.GetPosition("DicePos"));
            //RollingDice = new DiceRow(ScreenLocations.GetPosition("DicePos"));
            _rollButton = new RollButton();
            playerCardList = new PlayerCardList();
        }

        /// <summary>
        /// Processes all of the games display logic. Updates local objects 
        /// based on packets received in the Client. Processes any local
        /// decisions that need to be made, displays relevent objects/info
        /// </summary>
        /// <param name="gameTime">Parameter for the game's GameTime</param>
        public override void Update(GameTime gameTime)
        {
            if (Engine.ExitGame)
            {
                ScreenManager.RemoveScreen(this);    
            }
            if (gameOver)
            {
                EndGame(_winner);
            }
            
            if (GetMonsterList().Count != _monsterList.Count)   //A change has happened in the monster list
            {
                _monsterList = GetMonsterList();
                GetPlayerBlocks();
            }
            UpdatePositions();
            UpdateGraphicsPieces();
            
            if (GamePieces.Session.Game.CardsForSale.Count > 0)     //If there are cards for sale display them.
            {
                _cardForSaleList = new TextBlock("cardList", new List<string>()
            {
                "Cards For Sale  -  Card Cost",
                " " + GamePieces.Session.Game.CardsForSale[0].Name + " - " + GamePieces.Session.Game.CardsForSale[0].Cost,
                " " + GamePieces.Session.Game.CardsForSale[1].Name + " - " + GamePieces.Session.Game.CardsForSale[1].Cost,
                " " + GamePieces.Session.Game.CardsForSale[2].Name + " - " + GamePieces.Session.Game.CardsForSale[2].Cost
            });
            }

            if (!(MonsterController.IsDead(_localPlayer)))    //If a player isn't dead check for their startOfTurn
            {
                if (_gameState != GameState.Spectating && MonsterController.State(_localPlayer) == State.StartOfTurn)
                    if(MonsterController.CanYield(_localPlayer) == false)
                        _gameState = GameState.StartTurn;
            }
            else
            {
                _gameState = GameState.IsDead;
            }

            var mouseIsOver = false;
            foreach (var playerBlock in _pBlocks)
            {
                if (playerBlock.MouseOver(Engine.InputManager.FreshMouseState))
                {
                    mouseIsOver = true;
                    playerCardList.BoxPosition = new Vector2(Engine.InputManager.FreshMouseState.X, Engine.InputManager.FreshMouseState.Y);
                    playerCardList._stringList = CardListToString(playerBlock.Monster.Cards);
                }
            }
            if (mouseIsOver)
            {
                playerCardList.Hidden = false;
            }
            else
            {
                playerCardList.Hidden = true;
            }
            

            switch (_gameState)
            {
                case GameState.StartTurn:
                    StartingTurn();
                    break;
                case GameState.Rolling:
                    Rolling();
                    break;
                case GameState.Waiting:
                    Waiting();
                    break;
                case GameState.BuyCardPrompt:
                    AskBuyCards();
                    break;
                case GameState.BuyingCards:
                    BuySomeCards();
                    break;
                case GameState.EndingTurn:
                    EndTurn();
                    break;
                case GameState.AskYield:
                    AskYield();
                    break;
                case GameState.IsDead:
                    IsDead();
                    break;
                case GameState.EndGame:
                    EndGame(_winner);
                    break;
                case GameState.Spectating:
                    Spectate();
                    break;
                default:
                    Console.Write("switch hit default.");
                    break;
            }

            if (Engine.InputManager.KeyPressed(Keys.P))
            {
                ScreenManager.AddScreen(new PauseMenu());
            }

            base.Update(gameTime);
        }

        private List<string> CardListToString(List<Card> cards)
        {
            List<string> toReturn = new List<string>();
            foreach (var card in cards)
            {
                toReturn.Add(card.Name);
                toReturn.Add(card.GetDescrip());
            }
            return toReturn;
        }

        /// <summary>
        /// Handles the calling of anything needing to be drawn to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Engine.SpriteBatch.Begin();
            Engine.SpriteBatch.Draw(BackgroundImage, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
            DrawGraphicsPieces();
            Engine.SpriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Unloads any assets to free memory when screen is no longer displayed
        /// </summary>
        public override void UnloadAssets()
        {
            _pBlocks.Clear();
            _textPrompts.Clear();
            _diceRow.Clear();
            //RollingDice.Clear();
            base.UnloadAssets();
        }

        #region GameStateFunctions

        /// <summary>
        /// A function for when a player is in the spectating gamestate
        /// </summary>
        private static void Spectate()
        {
            _textPrompts.Clear();
            _textPrompts.Add(new TextBlock("RollingText", new List<string> {
                "You're Spectating! Have fun!",
                }));
        }

        /// <summary>
        /// Function for when player is first starting their turn
        /// </summary>
        private void StartingTurn()
        {
            if (_rollButton.Hidden)
            {
                _rollButton.Hidden = false;
            }
            _diceRow.Hidden = true;
            _diceRow.Clear();

            //RollingDice.Hidden = true;
           // RollingDice.Clear();

            _textPrompts.Clear();
            if (_firstPlay)
            {
                Engine.PlaySound("StartTurn");
                _firstPlay = false;
                Client.SendMessage(_localMonster.Name + " is starting their turn!");
            }
            var cardsOwned = "";
            if (MonsterController.Cards(_localPlayer).Count > 0) cardsOwned = MonsterController.Cards(_localPlayer)[0].Name; //TODO only displays first cards owned, need to show all if this works
            _textPrompts.Add(new TextBlock("RollPrompt", new List<string> {
                "Your Turn! Rolls Left: " + MonsterController.GetById(_localPlayer).RemainingRolls,
                "Cards: " + cardsOwned
                }));

            if (_rollButton.MouseOver(Engine.InputManager.FreshMouseState) && Engine.InputManager.LeftClick())
            {   
                _gameState = GameState.Rolling;
                Client.SendActionPacket(GameStateController.Roll());
                System.Threading.Thread.Sleep(200);
                _diceRow.AddDice(DiceController.GetDice());
                _diceRow.Hidden = false;
                //RollingDice.AddDice(DiceController.GetDice());
                //RollingDice.Hidden = false;
            }
        }

        /// <summary>
        /// Function for when a player is in their Rolling gamestate. Here the dice are displayed
        /// and the player has the option to roll when pressing 'R'. They can also save the dice
        /// by clicking on them.
        /// </summary>
        private void Rolling()
        {
            _diceRow.Hidden = false;
            //RollingDice.Hidden = false;
            if (MonsterController.RollsRemaining(_localPlayer) == 0 || Engine.InputManager.KeyPressed(Keys.E))
            {
                _rollButton.Hidden = true;
                _textPrompts.Clear();
                Client.SendMessage("Rolled: " + GetDiceText(DiceController.GetDice()));
                Client.SendActionPacket(GameStateController.EndRolling());
                //Buy Cards?
                _gameState = AskForCards(MonsterController.Energy(_localPlayer)) ? GameState.BuyCardPrompt : GameState.EndingTurn;
                return;
            }

            /*
            if (Engine.InputManager.KeyPressed(Keys.R) && RollAnimation <= 0)
            {
                Client.SendActionPacket(GameStateController.Roll());
                RollAnimation = 30;
            }
            */

            if (Engine.InputManager.LeftClick())
            {
                if (_rollButton.MouseOver(Engine.InputManager.FreshMouseState))
                {
                    Client.SendActionPacket(GameStateController.Roll());
                }

                foreach (var ds in _diceRow.DiceSprites)
                {
                    if (ds.MouseOver(Engine.InputManager.FreshMouseState))
                    {
                        ds.Click();
                    }
                }

                /*
                if (_rollButton.MouseOver(Engine.InputManager.FreshMouseState) && RollAnimation <= 0)
                {
                    Client.SendActionPacket(GameStateController.Roll());
                    RollAnimation = 30;
                }
                */
            }

            if (_textPrompts.Count > 0)
            {
                _textPrompts.Remove(_textPrompts[_textPrompts.Count - 1]);
            }

            var cardsOwned = "";
            if (MonsterController.Cards(_localPlayer).Count > 0) cardsOwned = MonsterController.Cards(_localPlayer)[0].Name; //TODO only displays first cards owned, need to show all if this works
            _textPrompts.Add(new TextBlock("RollPrompt", new List<string> {
                "Your Turn! Rolls Left: " + MonsterController.GetById(_localPlayer).RemainingRolls,
                "Cards: " + cardsOwned
                }));
        }

        /// <summary>
        /// Function for when a player ends their turn. This just clears the screen and
        /// resets the _firstPlay value then puts them in the waiting gamestate
        /// </summary>
        private void EndTurn()
        {
            System.Threading.Thread.Sleep(200);
            var data = GetMonsterList();
            if (data.Any(mon => mon.CanYield)) { return; }
            _textPrompts.Clear();
            _diceRow.Clear();
            _diceRow.Hidden = true;

            //RollingDice.Clear();
            //RollingDice.Hidden = true;

            _rollButton.Hidden = true;

            _firstPlay = true;

            _gameState = GameState.Waiting;
            Client.SendActionPacket(GameStateController.EndTurn());
            Client.SendActionPacket(GameStateController.StartTurn());
        }

        /// <summary>
        /// Function for the waiting gamestate. This has a clear screen, but
        /// will prompt the player to yield if they can.
        /// </summary>
        private void Waiting()
        {
            _textPrompts.Clear();
            if (MonsterController.GetById(_localPlayer).CanYield)
            {
                _gameState = GameState.AskYield;
                AskYield();
            }
        }

        /// <summary>
        /// Function for when a player is dead. Currently just clears the screen
        /// and informs them they are dead.
        /// </summary>
        private void IsDead()
        {
            if (_winner == null)
            {
                _diceRow.Hidden = true;
                _diceRow.Clear();

                //RollingDice.Hidden = true;
                //RollingDice.Clear();

                _textPrompts.Clear();

                _textPrompts.Add(new TextBlock("RollingText", new List<string>
                {
                    "You're Dead."
                }));
            }
            else
            {
                EndGame(_winner);
            }
      
        }

        /// <summary>
        /// Function for prompting the player to Yield. Waits for either a Yes or No response
        /// before continuing the game.
        /// </summary>
        private void AskYield()
        {
            _textPrompts.Clear();
            var s = new List<string> {MonsterController.Name(_localPlayer) + ": Yield? Y/N"};
            _textPrompts.Add(new TextBlock("YieldPrompt", s));

            if (Engine.InputManager.KeyPressed(Keys.Y))
            {
                Client.SendActionPacket(GameStateController.Yield(_localPlayer));
                _gameState = GameState.Waiting;
                Waiting();

            }
            else if (Engine.InputManager.KeyPressed(Keys.N))
            {
                Client.SendActionPacket(GameStateController.NoYield(_localPlayer));
                _gameState = GameState.Waiting;
                Waiting();
            }
        }

        
        /// <summary>
        /// Function for prompting to open the buyCards() screen and buy some cards.
        /// Only asks if they player has enough energy to purchase any of the cards.
        /// </summary>
        private static void AskBuyCards()
        {
            _textPrompts.Clear();

            _gameState = GameState.BuyCardPrompt;
            var monList = GetMonsterList();
            if (monList.Any(mon => mon.CanYield)) { return; }

            _textPrompts.Add(new TextBlock("BuyCardsPrompt", new List<string>()
            {
                MonsterController.Name(_localPlayer) + ": Buy Cards? Y/N"
            }));

            if (Engine.InputManager.KeyPressed(Keys.Y))
            {
                _textPrompts.Clear();
                _gameState = GameState.BuyingCards;
                return;
            }
            if (Engine.InputManager.KeyPressed(Keys.N))
            {
                _gameState = GameState.EndingTurn;
            }
        }

        private static void BuySomeCards()
        {
            ScreenManager.AddScreen(new BuyCards(MonsterController.GetById(_localPlayer).Energy));
            if (CardScreenChoice == -1) return;
            var cfs = CardsForSale.One;
            switch (CardScreenChoice)
            {
                case 0:
                    cfs = CardsForSale.One;
                    break;
                case 1:
                    cfs = CardsForSale.Two;
                    break;
                case 2:
                    cfs = CardsForSale.Three;
                    break;
                case -2:
                    break;
                default:
                    Console.Out.WriteLine("Something went wrong with cardScreenChoice");
                    break;
            }
            if (CardScreenChoice >= 0) { Client.SendActionPacket(GameStateController.BuyCard(cfs)); }
            CardScreenChoice = -1; //reset choice for next time.
            _gameState = GameState.EndingTurn;
        }
        

        /// <summary>
        /// Function for when the game comes to completion. Clears
        /// the screen and displays the winner's name
        /// </summary>
        /// <param name="winner">The name of the game's winner</param>
        public static void EndGame(string winner)
        {
            _winner = winner;
            _diceRow.Hidden = true;
            _rollButton.Hidden = true;
            _textPrompts.Clear();
            _textPrompts.Add(new TextBlock("RollPrompt", new List<string>()
            {
                "Game Over",
                "Winner: " + winner + "!",
                "Press Escape to Close"
            }));
            _gameState = GameState.EndGame;
            if (Engine.InputManager.KeyPressed(Keys.Escape))
            {
                Engine.ExitGame = true;
            }
        }

        #endregion

        #region PrivateHelpers

        /// <summary>
        /// Private helper function to determine if a player can afford any cards
        /// </summary>
        /// <param name="playerEnergy">The amount of energy a player has</param>
        /// <returns>True if any card can be afforded, false otherwise.</returns>
        private static bool AskForCards(int playerEnergy)
        {
            var ask = false;
            foreach (var card in GamePieces.Session.Game.CardsForSale)
            {
                if (card.Cost < playerEnergy)
                {
                    ask = true;
                }
            }
            return ask;
        }

        /// <summary>
        /// Private helper function for updating positions. Should only be needed if
        /// there is a resolution change.
        /// </summary>
        private void UpdatePositions()
        {
            ScreenLocations.Update();
            foreach (var tp in _textPrompts)
            {
                tp.Position = ScreenLocations.GetPosition(tp.Name);
            }
            _diceRow.setPosition(ScreenLocations.GetPosition("DicePos"));
            //RollingDice.setPosition(ScreenLocations.GetPosition("DicePos"));
        }

        /// <summary>
        /// Private helper function for calling update on all of the local game pieces
        /// </summary>
        private void UpdateGraphicsPieces()
        {
            _diceRow.setPosition(ScreenLocations.GetPosition("DicePos"));
            foreach (var ds in _diceRow.DiceSprites)
                ds.Update();
            foreach (var pb in _pBlocks)
                pb.Update();
            ServerUpdateBox.UpdateList();
            _rollButton.Update();
        }

        /// <summary>
        /// Private helper function for calling draw on all of the local game pieces
        /// </summary>
        private void DrawGraphicsPieces()
        {
            ServerUpdateBox.Draw(Engine.SpriteBatch);
            foreach (var pb in _pBlocks)
                pb.Draw(Engine.SpriteBatch);

            if (!_diceRow.Hidden)
            {
                
                foreach(var ds in _diceRow.DiceSprites)
                    ds.Draw(Engine.SpriteBatch);
                
                
                /*
                for (var i = 0; i < _diceRow.DiceSprites.Count; i++)
                {
                    if (RollAnimation <= 0 || _diceRow.DiceSprites[i].Save)
                    {
                        _diceRow.DiceSprites[i].Draw(Engine.SpriteBatch);
                    }
                    else
                    {
                        RollingDice.DiceSprites[i].Roll();
                        RollingDice.DiceSprites[i].Draw(Engine.SpriteBatch);
                        RollAnimation--;
                    }
                }
                */
                
            }

            if (!_rollButton.Hidden)
            {
                _rollButton.Draw(Engine.SpriteBatch);
            }

            if (!playerCardList.Hidden)
            {
                playerCardList.Draw(Engine.SpriteBatch);
            }

            foreach (var tp in _textPrompts)
                tp.Draw(Engine.SpriteBatch);

            _cardForSaleList?.Draw(Engine.SpriteBatch);
        }

        /// <summary>
        /// Private helper function for getting the games MonsterList
        /// </summary>
        /// <returns>List of monsters currently in the game</returns>
        private static List<Monster> GetMonsterList()
        {
            var monList = new List<Monster>();
            Monster mon;
            if(GamePieces.Session.Game.Monsters.Any(monster => monster.PlayerId == _localPlayer))
            {
                mon = MonsterController.GetById(_localPlayer);
                monList.Add(mon);
            }
            else
            {
                return monList;
            }
            mon = mon.Next;
            while (mon != _localMonster)
            {
                monList.Add(mon);
                mon = mon.Next;
            }
            return monList;
        }

        /// <summary>
        /// Private helper function used for initializing the list of playerBlocks. Currently
        /// uses the number of players in the game to determine the locations and updates accordingly.
        /// </summary>
        /// <returns></returns>
        private static List<PlayerBlock> GetPlayerBlocks()
        {
            var monList = GetMonsterList();
            var toReturn = new List<PlayerBlock>();
            switch (monList.Count)
            {
                case 2:
                    toReturn.Add(new PlayerBlock("BottomCenter", monList[0]));
                    toReturn.Add(new PlayerBlock("TopCenter", monList[1]));
                    break;
                case 3:
                    toReturn.Add(new PlayerBlock("BottomCenter", monList[0]));
                    toReturn.Add(new PlayerBlock("TopLeft", monList[1]));
                    toReturn.Add(new PlayerBlock("TopRight", monList[2]));
                    break;
                case 4:
                    toReturn.Add(new PlayerBlock("TopLeft", monList[0]));
                    toReturn.Add(new PlayerBlock("TopRight", monList[1]));
                    toReturn.Add(new PlayerBlock("MidLeft", monList[2]));
                    toReturn.Add(new PlayerBlock("MidRight", monList[3]));
                    break;
                case 5:
                    toReturn.Add(new PlayerBlock("BottomCenter", monList[0]));
                    toReturn.Add(new PlayerBlock("TopLeft", monList[1]));
                    toReturn.Add(new PlayerBlock("TopRight", monList[2]));
                    toReturn.Add(new PlayerBlock("MidLeft", monList[3]));
                    toReturn.Add(new PlayerBlock("MidRight", monList[4]));
                    break;
                case 6:
                    toReturn.Add(new PlayerBlock("BottomCenter", monList[0]));
                    toReturn.Add(new PlayerBlock("TopLeft", monList[1]));
                    toReturn.Add(new PlayerBlock("TopCenter", monList[2]));
                    toReturn.Add(new PlayerBlock("TopRight", monList[3]));
                    toReturn.Add(new PlayerBlock("MidLeft", monList[4]));
                    toReturn.Add(new PlayerBlock("MidRight", monList[5]));
                    break;
                default:
                    Console.WriteLine("Something went wrong. Need minimum of two players, max of 6.");
                    break;
            }
            return toReturn;
        }
        
        /// <summary>
        /// Private helper function used for converting the dice roll values into
        /// text for displaying
        /// </summary>
        /// <param name="list">List of currently rolled dice</param>
        /// <returns>String containing the converted & formatted dice values</returns>
        private static string GetDiceText(List<Die> list)
        {
            return list[0].Symbol + ", " + list[1].Symbol + ", " + list[2].Symbol + ", " + list[3].Symbol + ", " +
                   list[4].Symbol + ", " + list[5].Symbol;


            /*
            var attackRolled = 0;
            var energyRolled = 0;
            var healthRolled = 0;
            var pointsRolled = new int[3];

            foreach (var die in list)
            {
                if (die.Symbol.Equals(Symbol.Attack))
                {
                    attackRolled++;
                }
                else if (die.Symbol.Equals(Symbol.Energy))
                {
                    energyRolled++;
                }
                else if (die.Symbol.Equals(Symbol.Heal))
                {
                    healthRolled++;
                }
                else
                {
                    if (die.Symbol.Equals(Symbol.One))
                    {
                        pointsRolled[0]++;
                    }
                    else if (die.Symbol.Equals(Symbol.Two))
                    {
                        pointsRolled[1]++;
                    }
                    else if (die.Symbol.Equals(Symbol.Three))
                    {
                        pointsRolled[2]++;
                    }
                }
            }

            return "Attack: " + attackRolled + " Energy: " + energyRolled + " Health: " + healthRolled + " Points: " + pointsRolled;
            */
        }

        #endregion

        internal enum GameState
        {
            StartTurn,
            Rolling,
            AskYield,
            BuyCardPrompt,
            Waiting,
            EndingTurn,
            EndGame,
            IsDead,
            Spectating,
            BuyingCards
        }
    }
}
