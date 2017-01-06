using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Giant_Brain : Card
    {
        public override CardType CardType => CardType.Stats;
        public override int Cost => 5;
        private bool canUse = true;
        public string Descrip = "Max rolls increased by 1";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            if (!canUse) return false;
            canUse = false;
            return true;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.MaximumRolls += 1;
        }

        public override void UndoEffect(Monster monster)
        {
            monster.MaximumRolls -= 1;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}