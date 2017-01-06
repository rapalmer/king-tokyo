using GamePieces.Monsters;

namespace GamePieces.Session
{
    public static class Board
    {
        //Game Components
        public static int Monsters => Game.Monsters.Count;
        public static bool UseTokyoBay => Monsters > 4;

        //State
        public static Monster TokyoCity { get; private set; }
        public static Monster TokyoBay { get; private set; }

        public static bool TokyoCityIsOccupied => TokyoCity != null;
        public static bool TokyoBayIsOccupied => TokyoBay != null;

        /// <summary>
        /// Move the given monster into Tokyo
        /// </summary>
        /// <param name="monster">Monster</param>
        internal static void MoveIntoTokyo(Monster monster)
        {
            if(monster.InTokyo) return;
            if (!TokyoCityIsOccupied)
            {
                TokyoCity = monster;
                monster.Location = Location.TokyoCity;
                monster.VictoryPoints += 1;
            }
            else if (UseTokyoBay && !TokyoBayIsOccupied)
            {
                TokyoBay = monster;
                monster.Location = Location.TokyoBay;
                monster.VictoryPoints += 1;
            }
        }

        /// <summary>
        /// Have the given monster leave Tokyo
        /// </summary>
        /// <param name="monster">Monster</param>
        internal static void LeaveTokyo(Monster monster)
        {
            if (TokyoCityIsOccupied && monster.Equals(TokyoCity))
            {
                TokyoCity = null;
                monster.Location = Location.Default;
            }
            else if (TokyoBayIsOccupied && monster.Equals(TokyoBay))
            {
                TokyoBay = null;
                monster.Location = Location.Default;
            }
        }

        /// <summary>
        /// Update the board based on the number of players
        /// Tokyo Bay cannot be used if there are less than five players
        /// </summary>
        internal static void Update()
        {
            if (UseTokyoBay || !TokyoBayIsOccupied) return;
            TokyoBay.Location = Location.Default;
            TokyoBay = null;
        }

        /// <summary>
        /// Reset the board to the default state
        /// </summary>
        internal static void Reset()
        {
            LeaveTokyo(TokyoCity);
            LeaveTokyo(TokyoBay);
        }
    }
}