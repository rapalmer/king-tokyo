using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Solar_Powered : Card
    {
        public override int Cost => 2;
        public string Descrip = "Gain 1 energy if none at end of turn";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.State == State.EndOfTurn && monster.Energy == 0;
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