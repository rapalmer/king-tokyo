using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class It_Has_A_Child : Card
    {
        public override int Cost => 7;
        public override bool OncePerTurn => true;
        public string Descrip = "If eliminated discard cards and lose VPs, but heal 10 health";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.Health == 0;
        }

        protected override void UpdateLogic(Monster monster)
        {
            while (monster.Cards.Count != 0)
                monster.RemoveCard(monster.Cards[0]);
            monster.VictoryPoints = 0;
            monster.Health = 10;
        }

        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}