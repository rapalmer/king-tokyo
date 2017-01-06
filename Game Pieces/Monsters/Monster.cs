

using System.Collections.Generic;
using System.Linq;
using DataStructures.Observer_Pattern;
using GamePieces.Cards;
using GamePieces.Session;
using System;

namespace GamePieces.Monsters
{
    public class Monster : Observable<Monster>
    {
        //Player ID
        public int PlayerId { get; private set; }

        //Location & Neighbors
        private int Index => IndexOf(Game.Turns, this);
        public Monster Previous
        {
            get
            {
                var previous = GetNodeAt(Game.Turns, Index).Previous;
                return previous != null ? previous.Value : Game.Turns.Last();
            }
        }

        public Monster Next
        {
            get
            {
                var next = GetNodeAt(Game.Turns, Index).Next;
                return next != null ? next.Value : Game.Turns.First();
            }
        }

        //Name
        public string Name { get; private set; }

        //State
        public State State
        {
            get { return Get(); }
            set { Set(value); }
        }

        //Location
        public Location Location
        {
            get { return Get(); }
            set { Set(value); }
        }

        public bool InTokyo => Location != Location.Default;
        public bool CanYield = false; //=> InTokyo && Game.Attacked.Contains(this);

        //Cards
        public List<Card> Cards = new List<Card>();

        public int NumberOfCards
        {
            get { return Get(); }
            private set { Set(value); }
        }

        public int PreviousNumberOfCards { get; private set; }

        //Energy
        public int Energy
        {
            get { return Get(); }
            set
            {
                if (Get() != null)
                {
                    if (value < 0) value = 0;
                    State = value > Energy ? State.Energizing : State.DeEnergizing;
                    PreviousEnergy = Energy;
                }
                Set(value);
            }
        }

        public int PreviousEnergy { get; private set; }

        //Victory Points
        public int VictoryPoints
        {
            get { return Get(); }
            set
            {
                if (Get() != null)
                {
                    if (value < 0) value = 0;
                    if (value > 20) value = 20;
                    State = value > VictoryPoints ? State.Scoring : State.Losing;
                    PreviousVictoryPoints = VictoryPoints;
                }
                Set(value);
            }
        }

        public int PreviousVictoryPoints { get; private set; }

        //Health
        public int Health
        {
            get { return Get(); }
            set
            {
                if (Get() != null)
                {
                    if (value <= 0)
                    {
                        value = 0;
                        Kill();
                    }
                    else
                    {
                        if (value > MaximumHealth) value = MaximumHealth;
                        State = value > Health ? State.Healing : State.Attacked;
                        if (State == State.Healing && InTokyo) return;
                        PreviousHealth = Health;
                    }
                }
                Set(value);
            }
        }

        public int PreviousHealth { get; private set; }
        public int MaximumHealth { get; set; } = 10;
        public int AttackPoints { get; set; }


        //Dice
        public int Dice { get; set; } = 6;
        public int MaximumRolls { get; set; } = 3;

        public int RemainingRolls
        {
            get { return Get(); }
            set { Set(value); }
        }

        /// <summary>
        /// The monster game piece
        /// </summary>
        /// <param name="playerId">Player ID</param>
        /// <param name="name">The name of the monster</param>
        public Monster(int playerId, string name = "")
        {
            PlayerId = playerId;
            Name = name;
            Energy = 0;
            NumberOfCards = 0;
            VictoryPoints = 0;
            Health = MaximumHealth;
            Location = Location.Default;
            RemainingRolls = 0;
            State = State.EndOfTurn;
        }

        /// <summary>
        /// Creates a new monster from a data packet.
        /// </summary>
        /// <param name="packet">Data Packet</param>
        public Monster(MonsterDataPacket packet)
        {
            AcceptPacket(packet);
        }

        /// <summary>
        /// This needs to be called to start the turn of the monster.
        /// All values are set to the correct state for starting a turn.
        /// </summary>
        public void StartTurn()
        {
            Cards.ForEach(card => card.Reset());
            if (InTokyo) VictoryPoints += 2;
            RemainingRolls = MaximumRolls;
            DiceRoller.Setup(Dice);
            State = State.StartOfTurn;
        }

        /// <summary>
        /// Rolls all available dice in 'Game Components'
        /// </summary>
        public void Roll()
        {
            if (RemainingRolls == 0) return;
            State = State.Rolling;
            DiceRoller.Roll();
            RemainingRolls--;
            if (RemainingRolls == 0)
            {
                EndRolling();
            }
        }

        /// <summary>
        /// Tallys the score of the dice and returns them to the dice pool
        /// </summary>
        public void EndRolling()
        {
            State = State.TallyDice;
            DiceRoller.EndRolling(this);
        }

        /// <summary>
        /// Monster performs its attack
        /// </summary>
        public void Attack()
        {
            State = State.Attacking;
            Game.Attacked.Clear();
            if (AttackPoints != 0)
            {
                Game.Attacked.AddRange(Game.Monsters.Where(monster => monster.InTokyo != InTokyo).ToList());
                Game.Attacked.ForEach(monster => monster.Health -= AttackPoints);
                foreach (var mon in Game.Attacked)
                {
                    if (mon.InTokyo)
                        mon.CanYield = true;
                }
                Board.Update();
                Board.MoveIntoTokyo(this);
            }
            State = State.None;
            AttackPoints = 0;
        }

        /// <summary>
        /// If in Tokyo and just attacked, the attacker and this swap locations
        /// </summary>
        public void Yield()
        {
            if (!CanYield) return;
            State = State.Yielding;
            Board.LeaveTokyo(this);
            Board.MoveIntoTokyo(Game.Current);
            CanYield = false;
        }

        /// <summary>
        /// Add a power-up card to the monster by spending saved energy
        /// </summary>
        /// <param name="card">Card to buy</param>
        public void BuyCard(Card card)
        {
            if (Energy < card.Cost) return;
            State = State.BuyingCard;
            Energy -= card.Cost;
            if (card.CardType != CardType.Keep)
            {
                card.Update(this);
                if (card.CardType == CardType.Discard) return;
            }
            PreviousNumberOfCards = NumberOfCards;
            NumberOfCards++;
            Cards.Add(card);
            Subscribe(card);
        }

        /// <summary>
        /// Remove power-up card from the monster
        /// </summary>
        /// <param name="card">Card to remove</param>
        public void RemoveCard(Card card)
        {
            State = State.RemovingCard;
            Cards.Remove(card);
            Unsubscribe(card);
            card.UndoEffect(this);
            PreviousNumberOfCards = NumberOfCards;
            NumberOfCards--;
        }

        /// <summary>
        /// Sells the desired card to the given monster
        /// </summary>
        /// <param name="monster">Monster buying the card</param>
        /// <param name="card">Card being sold</param>
        public void SellCard(Monster monster, Card card)
        {
            RemoveCard(card);
            State = State.SellingCard;
            Energy += card.Cost;
            monster.BuyCard(card);
        }

        /// <summary>
        /// End the monster's turn and resets values
        /// </summary>
        public void EndTurn()
        {
            State = State.EndOfTurn;
        }

        /// <summary>
        /// Removes the monster from the game
        /// </summary>
        private void Kill()
        {
            if (InTokyo) Board.LeaveTokyo(this);
            Cards.Clear();
            Game.Monsters.Remove(this);
            Game.Turns.Remove(this);
            Game.Dead.Add(this);
            State = State.Dead;
        }

        /// <summary>
        /// Makes a data packet for this monster.
        /// </summary>
        /// <returns>Data Packet</returns>
        public MonsterDataPacket GetPacket()
        {
            return new MonsterDataPacket(PlayerId, Name, Location, Cards.Select(DataPacketHelper.CreateDataPacket).ToArray(), NumberOfCards,
                PreviousNumberOfCards, Energy, PreviousEnergy, VictoryPoints, PreviousVictoryPoints, Health,
                PreviousHealth, MaximumHealth, AttackPoints, Dice, MaximumRolls, RemainingRolls, CanYield, State);
        }

        /// <summary>
        /// Change the values of this monster to the values in the data packet.
        /// </summary>
        /// <param name="packet">Data Packet</param>
        public void AcceptPacket(MonsterDataPacket packet)
        {
            PlayerId = packet.PlayerId;
            Name = packet.Name;
            Location = packet.Location;
            Cards = packet.Cards.Select(DataPacketHelper.AcceptDataPacket).ToList();
            NumberOfCards = packet.NumberOfCards;
            PreviousNumberOfCards = packet.PreviousNumberOfCards;
            Energy = packet.Energy;
            PreviousEnergy = packet.PreviousEnergy;
            VictoryPoints = packet.VictoryPoints;
            PreviousVictoryPoints = packet.PreviousVictoryPoints;
            Health = packet.Health;
            PreviousHealth = packet.PreviousHealth;
            MaximumHealth = packet.MaximumHealth;
            AttackPoints = packet.AttackPoints;
            Dice = packet.Dice;
            MaximumRolls = packet.MaximumRolls;
            RemainingRolls = packet.RemainingRolls;
            CanYield = packet.CanYield;
            State = packet.State;
        }

        public static int IndexOf<T>(LinkedList<T> list, T item)
        {
            var count = 0;
            for (var node = list.First; node != null; node = node.Next, count++)
            {
                if (item.Equals(node.Value))
                    return count;
            }
            return -1;
        }

        public static LinkedListNode<T> GetNodeAt<T>(LinkedList<T> list, int position)
        {
            var mark = list.First;
            var i = 0;
            while (i < position)
            {
                mark = mark?.Next;
                i++;
            }
            return mark;
        }
    }
}