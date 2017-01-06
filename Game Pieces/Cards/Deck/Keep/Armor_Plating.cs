using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Armor_Plating : Card
    {
        public override int Cost => 4;
        public string Descrip = "Ignore damage of 1";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.PreviousHealth > monster.Health && monster.State == State.Attacked;
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