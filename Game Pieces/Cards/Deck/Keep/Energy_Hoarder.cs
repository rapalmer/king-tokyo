using System;
using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Energy_Hoarder : Card
    {
        public override bool OncePerTurn => true;
        public string Descrip = "Gain 1 VP for every 6 energy you have at the end of your turn";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.State == State.EndOfTurn;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.VictoryPoints += monster.Energy / 6;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}