using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Spiked_Tail : Card
    {
        public override int Cost => 5;
        public override bool OncePerTurn => true;
        public string Descrip = "Deal 1 extra damage when attacking";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.State == State.Attacking && monster.AttackPoints > 0;
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