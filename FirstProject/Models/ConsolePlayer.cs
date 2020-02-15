using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThirtyOne.Helpers;

namespace ThirtyOne.Models
{
    public class ConsolePlayer : Player
    {
        public ConsolePlayer(string name) : base(name)
        {
        }

        public override void Turn(Game game)
        {
            Console.WriteLine("Your turn. Your hand: ");

            foreach (var card in hand)
            {
                Console.WriteLine("\t" + card.ToString());
            }

            Console.WriteLine($"Hand score: {hand.CalculateScore()}\n");

            if (game.tableCards.Count > 0)
            {
                Console.WriteLine("On the table there is " + game.tableCards.Last().ToString() +
                                  ". Do you want to draw from the Table (T) or the Deck (D) or Call/Knock (C)?");
                var key = Console.ReadLine().ToUpper();

                switch (key)
                {
                    case "T":
                        DrawFromTable(game);
                        break;

                    case "D":
                        DrawFromDeck(game);
                        break;

                    default:
                        hasKnocked = true;
                        return;
                }
            }
            else
            {
                DrawFromDeck(game);
            }

            Console.WriteLine("You drew a card. Your hand: ");

            for (int i = 0; i < hand.Count; i++)
            {
                Console.WriteLine("\t" + (i + 1).ToString() + "\t" + hand[i].ToString());
            }

            Console.WriteLine("Which card to drop? (1-4)");

            string input = Console.ReadLine();
            int action = int.Parse(input);

            DropCard(game, action - 1);
            Console.WriteLine("Your score: " + hand.CalculateScore());
        }
    }
}
