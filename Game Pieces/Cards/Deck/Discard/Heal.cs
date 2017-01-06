using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Discard
{
    public class Heal : Card
    {
        public override CardType CardType => CardType.Discard;
        public string Descrip = "+2 health";

        /// <summary>
        /// Heal 2 damage
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
            monster.Health += 2;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}