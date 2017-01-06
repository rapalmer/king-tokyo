using System;
using System.Collections.Generic;
using GamePieces.Cards;
using GamePieces.Session;

namespace Controllers
{
    public static class CardController
    {
        /// <summary>
        /// Get the dice packet for the current game state.
        /// </summary>
        /// <returns>Data Packets</returns>
        public static CardDataPacket CreateDataPacket(Card card)
        {
            return new CardDataPacket(card.GetType(), card.Activated);
        }

        /// <summary>
        /// Change the monsters in the game state using the given data packets.
        /// </summary>
        /// <param name="dataPacket">Data Packets</param>
        public static Card AcceptDataPacket(CardDataPacket dataPacket)
        {
            try
            {
                var card = (Card) Activator.CreateInstance(dataPacket.Type);
                card.Activated = dataPacket.Activated;
                return card;
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Cannot create card of type: " + dataPacket.Type);
                return null;
            }
        }

        /// <summary>
        /// Gets the card on top of the deck without removing it
        /// </summary>
        /// <returns>Card</returns>
        public static Card TopOfDeck()
        {
            return Game.Deck.Count != 0 ? Game.Deck.Peek() : null;
        }

        /// <summary>
        /// Gets the first card for sale
        /// </summary>
        /// <returns>Card</returns>
        public static Card CardForSaleOne()
        {
            return Game.CardsForSale.Count > 0 ? Game.CardsForSale[0] : null;
        }

        /// <summary>
        /// Gets the second card for sale
        /// </summary>
        /// <returns>Card</returns>
        public static Card CardForSaleTwo()
        {
            return Game.CardsForSale.Count > 1 ? Game.CardsForSale[1] : null;
        }

        /// <summary>
        /// Gets the third card for sale
        /// </summary>
        /// <returns>Card</returns>
        public static Card CardForSaleThree()
        {
            return Game.CardsForSale.Count > 2 ? Game.CardsForSale[2] : null;
        }

        /// <summary>
        /// Buy the first card
        /// </summary>
        public static void BuyCardOne()
        {
            if (CardForSaleOne() != null) Game.BuyCard(0);
        }

        /// <summary>
        /// Buy the second card
        /// </summary>
        public static void BuyCardTwo()
        {
            if (CardForSaleTwo() != null) Game.BuyCard(1);
        }

        /// <summary>
        /// Buy the third card
        /// </summary>
        public static void BuyCardThree()
        {
            if (CardForSaleThree() != null) Game.BuyCard(2);
        }

        public static void SetCardsForSale(List<Card> cardsForSale)
        {
            Game.CardsForSale = cardsForSale;
        }

        public static List<Card> GetCardsForSale()
        {
            return Game.CardsForSale;
        }
    }
}