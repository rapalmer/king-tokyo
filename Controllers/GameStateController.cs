using GamePieces.Cards;
using GamePieces.Session;
using Networking;
using Networking.Actions;

namespace Controllers
{

    public static class GameStateController
    {
        public static bool IsCurrent => Game.Current.Equals(MonsterController.GetById(User.PlayerId));
        public static bool GameOver => Game.Winner != null;

        /// <summary>
        /// Changes the game state with the given action.
        /// </summary>
        /// <param name="actionPacket">Action packet</param>
        public static void AcceptAction(ActionPacket actionPacket)
        {
            //if(!Game.Host) return;
            switch (actionPacket.Action)
            {
                case Action.StartTurn:
                    Game.StartTurn();
                    break;
                case Action.Roll:
                    DiceController.Roll();
                    break;
                case Action.EndRolling:
                    DiceController.EndRolling();
                    break;
                case Action.BuyCard:
                    switch ((CardsForSale) actionPacket.Value)
                    {
                        case CardsForSale.One:
                            CardController.BuyCardOne();
                            break;
                        case CardsForSale.Two:
                            CardController.BuyCardTwo();
                            break;
                        case CardsForSale.Three:
                            CardController.BuyCardThree();
                            break;
                        default:
                            return;
                    }
                    break;
                case Action.RemoveCard:
                    Game.RemoveCard((Card) actionPacket.Value);
                    break;
                case Action.EndTurn:
                    Game.EndTurn();
                    break;
                case Action.Yield:
                    MonsterController.GetById(actionPacket.PlayerId).Yield();
                    break;
                case Action.SaveDie:
                    DiceController.SaveDie((int) actionPacket.Value);
                    break;
                case Action.UnSaveDie:
                    DiceController.UnSaveDie((int) actionPacket.Value);
                    break;
                case Action.NoYield:
                    MonsterController.GetById(actionPacket.PlayerId).CanYield = false;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Start turn.
        /// </summary>
        /// <returns>Start turn action packet</returns>
        public static ActionPacket StartTurn()
        {
            return new ActionPacket(Action.StartTurn);
        }

        /// <summary>
        /// Roll.
        /// </summary>
        /// <returns>Roll action packet</returns>
        public static ActionPacket Roll()
        {
            return new ActionPacket(Action.Roll);
        }

        /// <summary>
        /// End Rolling.
        /// </summary>
        /// <returns>End rolling action packet</returns>
        public static ActionPacket EndRolling()
        {
            return new ActionPacket(Action.EndRolling);
        }

        /// <summary>
        /// Buy card.
        /// </summary>
        /// <param name="cardsForSale">Card for sale.</param>
        /// <returns>Buy card action packet</returns>
        public static ActionPacket BuyCard(CardsForSale cardsForSale)
        {
            return new ActionPacket(Action.BuyCard, value: cardsForSale);
        }

        /// <summary>
        /// Remove card.
        /// </summary>
        /// <returns>Remove card action packet</returns>
        public static ActionPacket RemoveCard()
        {
            return new ActionPacket(Action.RemoveCard);
        }

        /// <summary>
        /// End turn.
        /// </summary>
        /// <returns>End turn action packet</returns>
        public static ActionPacket EndTurn()
        {
            return new ActionPacket(Action.EndTurn);
        }

        /// <summary>
        /// Yield.
        /// </summary>
        /// <param name="playerId">Player Id</param>
        /// <returns>Yield action packet</returns>
        public static ActionPacket Yield(int playerId)
        {
            return new ActionPacket(Action.Yield, playerId);
        }

        /// <summary>
        /// No Yield.
        /// </summary>
        /// <param name="playerId">Player Id</param>
        /// <returns>NoYield action packet</returns>
        public static ActionPacket NoYield(int playerId)
        {
            return new ActionPacket(Action.NoYield, playerId);
        }

        /// <summary>
        /// Save die.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Save die action packet</returns>
        public static ActionPacket SaveDie(int index)
        {
            return new ActionPacket(Action.SaveDie, value: index);
        }

        /// <summary>
        /// Un-save die.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Un-save action packet</returns>
        public static ActionPacket UnSaveDie(int index)
        {
            return new ActionPacket(Action.UnSaveDie, value: index);
        }
    }
}