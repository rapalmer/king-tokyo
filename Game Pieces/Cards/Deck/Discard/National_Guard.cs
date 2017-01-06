using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Discard
{
    public class National_Guard : Card
    {
        public override CardType CardType => CardType.Discard;
        public string Descrip = "+2 VP & take 2 damage";

        /// <summary>
        /// Plus 2 victory points
        /// Take 2 damage
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
            monster.VictoryPoints += 2;
            monster.Health -= 2;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}