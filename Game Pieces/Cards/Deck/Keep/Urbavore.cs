using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Urbavore : Card
    {
        public override int Cost => 4;
        public override bool OncePerTurn => true;
        public string Descrip = "+1 extra VP when starting in Tokyo & +1 damage when attacking from tokyo";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            return monster.State == State.StartOfTurn && monster.InTokyo;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.VictoryPoints += 1;
            monster.AttackPoints += 1;
        }

        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}