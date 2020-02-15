using FirstProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThirtyOne.Helpers;

namespace ThirtyOne.Models
{
    class ComputerPlayer : Player
    {
        Random randomNumber;
        
        public ComputerPlayer()
        {
            randomNumber = new Random();
        }

        public ComputerPlayer(string name) : base(name)
        {
            randomNumber = new Random();
        }

        public override void Turn(Game game)
        {
            //First, decide on action: Draw from deck, draw from table, knock
            if (hand.CalculateScore() > 25 && !game.players.Any(Player => Player.hasKnocked) &&
                randomNumber.Next(3) == 1)
            {
                Console.WriteLine($"{name} knocks on the table");
                //Knock
                hasKnocked = true;
            }
            else
            {
                //Decide if I should draw from table or from deck
                if (game.tableCards.Any() && game.tableCards.Last().value >= 10 && randomNumber.Next(2) == 1)
                {
                    Console.WriteLine($"{name} draws a card from the table");
                    DrawFromTable(game);
                }
                else
                {
                    Console.WriteLine($"{name} draws a card from the deck");
                    DrawFromDeck(game);
                }

                //Drop card that'll give highest score
                List<Tuple<Card, int>> lst = new List<Tuple<Card, int>>();
                foreach (var card in hand)
                {
                    lst.Add(new Tuple<Card, int>(card, hand.Except(new Card[] { card }).CalculateScore()));
                }

                int index = hand.IndexOf(lst.OrderByDescending(l => l.Item2).First().Item1);
                Console.WriteLine($"{name} drops {hand[index].ToString()}");
                DropCard(game, index);
            }
        }
    }
}
