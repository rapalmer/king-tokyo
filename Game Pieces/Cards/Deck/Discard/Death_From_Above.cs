using GamePieces.Monsters;
using GamePieces.Session;

namespace GamePieces.Cards.Deck.Discard
{
    public class Death_From_Above : Card
    {
        public override CardType CardType => CardType.Discard;
        public override int Cost => 5;
        public string Descrip = "+2 VP & take control of Tokyo";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return true;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.VictoryPoints += 2;
            if (monster.Location == Location.TokyoCity) return;
            Board.LeaveTokyo(Board.TokyoCity);
            Board.MoveIntoTokyo(monster);
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}