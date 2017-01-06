using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Observer_Pattern;
using GamePieces.Monsters;

namespace GamePieces.Cards
{
    public abstract class Card : Observer<Monster>
    {

        /// <summary>
        /// Gets all of the cards in the current namespace and shuffle them
        /// </summary>
        /// <returns>List of cards</returns>
        public static List<Card> GetCards()
        {
            var cards = typeof(Card)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Card)) && !t.IsAbstract)
                .Select(t => (Card) Activator.CreateInstance(t)).ToList();

            var duplicates = new List<Card>();
            foreach (var card in cards)
            {
                if (card.CardsPerDeck <= 1) continue;
                for (var counter = 0; counter < card.CardsPerDeck - 1; counter++)
                    duplicates.Add((Card) Activator.CreateInstance(card.GetType()));
            }
            cards.AddRange(duplicates);
            return new List<Card>(cards.OrderBy(card => Guid.NewGuid()));
        }

        public string Name => GetType().Name.Replace("_", " "); //Name of the card
        public virtual int Cost => 3; //Cost (in energy) for the card
        public virtual CardType CardType => CardType.Keep; //Keep or Discard attribute
        public virtual int CardsPerDeck => 1; //Instances of the card in the deck
        public virtual bool OncePerTurn => CardType == CardType.Discard; //Can only be used once per turn
        public bool Activated { get; set; } //Has been used once this turn

        /// <summary>
        /// Checks whether the given monster meets the update condition for the card and if the card can be used
        /// </summary>
        /// <param name="monster">Monster</param>
        /// <returns>True if conditions are met</returns>
        public override bool UpdateCondition(Monster monster)
        {
            if (!MonsterShouldUpdate(monster) || Activated) return false;
            Activated = OncePerTurn;
            return true;
        }

        /// <summary>
        /// Checks the card condition against the monster's current state
        /// </summary>
        /// <param name="monster">Monster</param>
        /// <returns>True if conditions are met</returns>
        protected virtual bool MonsterShouldUpdate(Monster monster)
        {
            return true;
        }

        /// <summary>
        /// Undoes any lasting effects on the given monster
        /// </summary>
        /// <param name="monster">Monster</param>
        public virtual void UndoEffect(Monster monster)
        {
        }

        /// <summary>
        /// Resets 'Activated'
        /// </summary>
        public void Reset()
        {
            Activated = false;
        }

        public abstract string GetDescrip();
    }
}