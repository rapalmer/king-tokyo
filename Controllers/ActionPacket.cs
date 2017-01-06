namespace Controllers.Actions
{
    public struct ActionPacket
    {
        public int PlayerId { get; }
        public Action Action { get; }
        public dynamic Value { get; }

        public ActionPacket(Action action, int playerId = 0, dynamic value = null)
        {
            Action = action;
            PlayerId = playerId;
            Value = value;
        }
    }
}