using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Dedicated_News_Team : Card
    {
        public string Descrip = "Gain 1 VP when you buy a card";
        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.NumberOfCards > monster.PreviousNumberOfCards && monster.State == State.BuyingCard;
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