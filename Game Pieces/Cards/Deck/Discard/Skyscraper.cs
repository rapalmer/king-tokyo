using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Discard
{
    public class Skyscraper : Card
    {
        public override int Cost => 6;
        public override CardType CardType => CardType.Discard;
        public string Descrip = "+4 VP";

        /// <summary>
        /// Plus 4 victory points
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
            monster.VictoryPoints += 4;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}