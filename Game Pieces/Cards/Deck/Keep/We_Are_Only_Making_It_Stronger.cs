using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class We_Are_Only_Making_It_Stronger : Card
    {
        public string Descrip = "+1 energy when you lose 2 or more health";
        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.PreviousHealth - monster.Health >= 2 && monster.State == State.Attacked;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.Energy += 1;
        }

        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}