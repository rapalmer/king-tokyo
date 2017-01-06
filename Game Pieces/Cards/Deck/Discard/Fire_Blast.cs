using System.Linq;
using GamePieces.Monsters;
using GamePieces.Session;

namespace GamePieces.Cards.Deck.Discard
{
    public class Fire_Blast : Card
    {
        public override CardType CardType => CardType.Discard;
        public string Descrip = "Deal 2 damage to all other monsters";

        /// <summary>
        /// Deal 2 damage to all other monsters
        /// </summary>
        /// <param name="monster">Monster</param>
        protected override void UpdateLogic(Monster monster)
        {
            Game.Monsters.Where(enemy => !enemy.Equals(monster))
                .ToList()
                .ForEach(enemy => enemy.Health -= 2);
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}