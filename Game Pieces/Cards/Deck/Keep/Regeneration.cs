using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Regeneration : Card
    {
        public override int Cost => 4;
        public string Descrip = "+1 health when healing";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.Health > monster.PreviousHealth && monster.State == State.Healing;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.Health += 1;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}