using FirstProject.Models;
using System;
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
            Game game = new Game(random, computerPlayer, new ConsolePlayer("You"));
            bool isGameOver = false;
            while (!isGameOver)
            {
                Console.WriteLine($"{game.currentPlayer.name} turn!");
                isGameOver = game.NextTurn();
            }
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"--- GAME OVER, {game.winner.name} WON WITH {game.winner.hand.ToListString()} ---");
            Console.ReadLine();
        }
    }
}
