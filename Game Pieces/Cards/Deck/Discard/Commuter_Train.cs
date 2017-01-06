using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Discard
{
    public class Commuter_Train : Card
    {
        public override int Cost => 4;
        public override CardType CardType => CardType.Discard;
        public string Descrip = "+2 VP";

        /// <summary>
        /// Plus 2 victory points
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
           monster.VictoryPoints += 2;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}