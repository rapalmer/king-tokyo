using System;

namespace Networking.Actions
{
    [Serializable]
    public struct ActionPacket
    {
        public int PlayerId { get; set; }
        public Action Action { get; set; }
        public dynamic Value { get; set; }

        public ActionPacket(Action action, int playerId = 0, dynamic value = null)
        {
            Action = action;
            PlayerId = playerId;
            Value = value;
        }
    }
}