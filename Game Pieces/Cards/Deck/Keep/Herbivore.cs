using System;
using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Herbivore : Card
    {
        public override int Cost => 5;
        public override bool OncePerTurn => true;
        public string Descrip = "Gain 1 VP if no damage dealt during your turn";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.State == State.Attacking && monster.AttackPoints == 0;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.VictoryPoints += 1;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}