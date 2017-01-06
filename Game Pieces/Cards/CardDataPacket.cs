using System;

namespace GamePieces.Cards
{
    [Serializable]
    public struct CardDataPacket
    {
        public Type Type { get; set; }
        public bool Activated { get; set; }

        public CardDataPacket(Type type, bool activated)
        {
            Type = type;
            Activated = activated;
        }
    }
}