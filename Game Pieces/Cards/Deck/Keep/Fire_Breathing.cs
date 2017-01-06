using System;
using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Fire_Breathing : Card
    {
        public override bool OncePerTurn => true;
        public string Descrip = "Neighbors take 1 extra damage when you deal damage";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.State == State.Attacking && monster.AttackPoints > 0;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.Previous.Health -= 1;
            if (monster.Previous.Equals(monster.Next)) return;
            monster.Next.Health -= 1;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}