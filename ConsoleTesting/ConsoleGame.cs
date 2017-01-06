using System;
using System.Collections.Generic;
using System.Linq;
using GamePieces.Session;

namespace ConsoleTesting
{
    public class ConsoleGame
    {
        public void Play()
        {
            while (true)
            {
                Game.StartGame(new List<int>(){0, 1}, new List<string>() {"Godzilla", "King Kong"});

                while (Game.Winner == null)
                {
                    var n = 0;
                    Game.StartTurn();
                    Console.WriteLine("\r\n" + Game.Current.Name + ", select Dice to Switch Save State or Exit Rolling");
                    while (true)
                    {
                        Game.Roll();
                        DiceRoller.Rolling.ForEach(
                            die => Console.Write(die.Symbol + "(" + (die.Save ? "S" : "R") + ") "));
                        Console.WriteLine();
                        if (Game.Current.RemainingRolls == 0)
                        {
                            Console.WriteLine();
                            break;
                        }
                        var input = Console.ReadLine();
                        Console.WriteLine();
                        if (input == null) continue;
                        if (input.ToUpper().Equals("EXIT"))
                        {
                            break;
                        }
                        var numbers = input.Split(' ').ToList();
                        foreach (var number in numbers)
                        {
                            if (!int.TryParse(number, out n)) continue;
                            n -= 1;
                            if (n >= 0 && n < DiceRoller.Rolling.Count)
                                DiceRoller.Rolling[n].Save = !DiceRoller.Rolling[n].Save;
                        }
                    }
                    Game.EndRolling();
                    if (Board.TokyoCityIsOccupied && Board.TokyoCity.CanYield)
                    {
                        Console.WriteLine(Board.TokyoCity.Name + ": Yield? Y/N");
                        var yield = Console.ReadLine();
                        Console.WriteLine();
                        if (yield != null && yield.ToUpper().Equals("Y")) Board.TokyoCity.Yield();
                    }
                    if (Board.TokyoBayIsOccupied && Board.TokyoBay.CanYield)
                    {
                        Console.WriteLine(Board.TokyoBay.Name + ": Yield? Y/N");
                        var yield = Console.ReadLine();
                        Console.WriteLine();
                        if (yield != null && yield.ToUpper().Equals("Y")) Board.TokyoBay.Yield();
                    }
                    if (Game.CardsForSale.Where(card => card.Cost <= Game.Current.Energy).ToList().Count != 0)
                    {
                        Console.WriteLine("Select Card to Buy or Exits (You have " + Game.Current.Energy + " energy)");
                        Game.CardsForSale.ForEach(card => Console.Write(card.Name + " (cost: " + card.Cost + ")   "));
                        Console.WriteLine();
                        var cardSelection = Console.ReadLine();
                        if (cardSelection != null && !cardSelection.ToUpper().Equals("EXIT") &&
                            int.TryParse(cardSelection, out n))
                        {
                            n -= 1;
                            Game.BuyCard(n);
                        }
                    }

                    Console.WriteLine(Game.Current.Name + ", your turn is over!");
                    Console.WriteLine("Victory Points: " + Game.Current.VictoryPoints);
                    Console.WriteLine("Health: " + Game.Current.Health);
                    Console.WriteLine("Energy: " + Game.Current.Energy);
                    Console.WriteLine("Location: " + Game.Current.Location);
                    Game.Current.Cards.ForEach(card => Console.Write(card.Name + " "));
                    Console.WriteLine();
                    Game.EndTurn();
                }

                if (Game.Winner != null) Console.WriteLine(Game.Winner.Name + " Wins!!!");
                Console.WriteLine("Play Again? Y/N");
                var playAgain = Console.ReadLine();
                if (playAgain != null && playAgain.ToUpper().Equals("Y")) continue;
                break;
            }
        }
    }
}