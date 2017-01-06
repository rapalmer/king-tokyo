using System.Linq;
using GamePieces.Monsters;

namespace GamePieces.Cards.Deck.Keep
{
    public class Extra_Head : Card
    {
        public override CardType CardType => CardType.Stats;
        public override int Cost => 7;
        public override int CardsPerDeck => 2;
        private bool _canUse = true;
        public string Descrip = "Get 1 extra die";

        protected override bool MonsterShouldUpdate(Monster monster)
        {
            if (!_canUse) return false;
            _canUse = false;
            return true;
        }

        protected override void UpdateLogic(Monster monster)
        {
            monster.Dice += 1;
        }

        public override void UndoEffect(Monster monster)
        {
            monster.Dice -= 1;
            _canUse = true;
        }
        public override string GetDescrip()
        {
            return Descrip;
        }
    }
}