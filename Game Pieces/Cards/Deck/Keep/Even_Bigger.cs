using System;
using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Even_Bigger : Card
    {
        public override CardType CardType => CardType.Stats;
        public override int Cost => 4;
        private bool canUse = true;
        public string Descrip = "Increase max health by 2 & gain 2 health when purchased";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            if (!canUse) return false;
            canUse = false;
            return true;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.MaximumHealth += 2;
            monster.Health += 2;
        }

        public override void UndoEffect(Monster monster)
        {
            monster.MaximumHealth -= 2;
            if (monster.Health > monster.MaximumHealth)
                monster.Health = monster.MaximumHealth;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}