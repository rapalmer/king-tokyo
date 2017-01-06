using System;

namespace GamePieces.Cards
{
    public static class DataPacketHelper
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
    }
}