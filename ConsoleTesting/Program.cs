using System;
using Controllers;
using GamePieces.Cards;
using GamePieces.Session;
using Newtonsoft.Json;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            LobbyController.AddPlayer(0, "0");
            LobbyController.AddPlayer(1, "1");
            LobbyController.StartGame();

        }
    }
}


