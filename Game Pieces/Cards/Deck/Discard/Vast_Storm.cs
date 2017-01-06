using System.Linq;
using GamePieces.Monsters;
using GamePieces.Session;

namespace GamePieces.Cards.Deck.Discard
{
    public class Vast_Storm : Card
    {
        public override int Cost => 6;
        public override CardType CardType => CardType.Discard;
        public string Descrip = "+2 VP & other monsters lose 1 energy for every 2 they have";

        /// <summary>
        /// Plus 2 victory points
        /// All other monsters lose 1 energy for every 2 energy they have
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
            monster.VictoryPoints += 2;
            Game.Monsters.Where(enemy => !enemy.Equals(monster))
                .ToList()
                .ForEach(enemy => enemy.Energy -= enemy.Energy / 2);
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}