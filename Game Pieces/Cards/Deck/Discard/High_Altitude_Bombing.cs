using GamePieces.Monsters;
using GamePieces.Session;

namespace GamePieces.Cards.Deck.Discard
{
    public class High_Altitude_Bombing : Card
    {
        public override int Cost => 4;
        public override CardType CardType => CardType.Discard;
        public string Descrip = "All monsters (including you) take 3 damage";

        /// <summary>
        /// All monsters (including you) take 3 damage
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
            Game.Monsters.ForEach(player => player.Health -= 3);
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}