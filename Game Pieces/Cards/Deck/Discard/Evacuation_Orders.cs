using System.Linq;
using GamePieces.Monsters;
using GamePieces.Session;

namespace GamePieces.Cards.Deck.Discard
{
    public class Evacuation_Orders : Card
    {
        public override int Cost => 7;
        public override CardType CardType => CardType.Discard;
        public override int CardsPerDeck => 2;
        public string Descrip = "All other monsters lose 5 VP";

        /// <summary>
        /// All other monsters lose 5 victory points
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
            Game.Monsters.Where(enemy => !enemy.Equals(monster))
                .ToList()
                .ForEach(enemy => enemy.VictoryPoints -= 5);
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}