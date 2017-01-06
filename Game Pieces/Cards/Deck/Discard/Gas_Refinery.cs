using System.Linq;
using GamePieces.Monsters;
using GamePieces.Session;

namespace GamePieces.Cards.Deck.Discard
{
    public class Gas_Refinery : Card
    {
        public override int Cost => 6;
        public override CardType CardType => CardType.Discard;
        public string Descrip = "+2 VP & 3 damage to all other monsters";

        /// <summary>
        /// Plus 2 victory oints
        /// Deal 3 damage to all other monsters
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
            monster.VictoryPoints += 2;
            Game.Monsters.Where(enemy => !enemy.Equals(monster))
                .ToList()
                .ForEach(enemy => enemy.Health -= 3);
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}