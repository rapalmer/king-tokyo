using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Acid_Attack : Card
    {
        public override int Cost => 6;
        public override bool OncePerTurn => true;
        public string Descrip = "Deal 1 extra damage each turn";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.State == State.Attacking;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.AttackPoints += 1;
        }

        public override string GetDescrip()
        {
            return Descrip;
        }

    }
}