using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Friend_Of_Children : Card
    {
        public string Descrip = "Gain 1 extra energy when you earn any energy";
        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.Energy > monster.PreviousEnergy && monster.State == State.Energizing;
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