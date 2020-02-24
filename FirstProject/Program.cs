using FirstProject.Models;
using System;
using System.IO;
using ThirtyOne.Helpers;
using ThirtyOne.Models;

namespace FirstProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.InitialCards();
            Random random = new Random();
            deck.shuffelCards(random);

            //Game implementation
            Console.WriteLine("Let's play 31!");
            ComputerPlayer computerPlayer = new ComputerPlayer("Computer");
            Game game = null;
            string gameStatusPath = "gameStatus.json";

            if(File.Exists(gameStatusPath))
            {
                game = Game.DeserializeGame(File.ReadAllText(gameStatusPath));
            }
            if (game == null)
            { 
                game = new Game(random, computerPlayer, new ConsolePlayer("You"));
            }
            bool isGameOver = false;

            while (!isGameOver)
            {
                Console.WriteLine($"{game.currentPlayer.name} turn!");
                isGameOver = game.NextTurn();
                Console.WriteLine($"{computerPlayer.name} {computerPlayer.lastAction}");
                File.WriteAllText(gameStatusPath, game.SerializeGame());

            }
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"--- GAME OVER, {game.winner.name} WON WITH {game.winner.hand.ToListString()} ---");
            Console.ReadLine();

            File.Delete(gameStatusPath);
        }
    }
}
