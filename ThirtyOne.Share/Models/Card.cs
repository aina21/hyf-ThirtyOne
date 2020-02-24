using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProject.Models
{
    public class Card
    {
        //prop double tap
        public Suits suit { get; set; }
        public int rank { get; set; } //1-13
        public int value
        {
            get
            {
                return (rank == 1) ? 11 :
                    (rank >= 10 && rank < 14) ? 10 : rank;
            }
        }

        public Card(Suits suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }
        public override string ToString()
        {
            return rank.ToString() + " Of " + suit.ToString();
        }

    }

  
}