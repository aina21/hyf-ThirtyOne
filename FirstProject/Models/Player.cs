using FirstProject.Models;
using System.Collections.Generic;

namespace ThirtyOne.Models
{
    public abstract class Player
    {
        public List<Card> hand { set; get; }
        public string name { set; get; }
        public bool hasKnocked { set; get; }

        public Player()
        {
            name = "";
            hasKnocked = false;
            hand = new List<Card>();
        }

        public Player(string name)
        {
            hasKnocked = false;
            hand = new List<Card>();
            this.name = name;
        }

        public abstract void Turn(Game game);

        public void DrawFromDeck(Game game)
        {
            hand.Add(game.deck.DrawCard());
        }

        public void DrawFromTable(Game game)
        {
            Card card = game.tableCards[game.tableCards.Count - 1];
            hand.Add(card);
            game.tableCards.Remove(card);
        }

        public void DropCard(Game game, int index)
        {
            game.tableCards.Add(hand[index]);
            hand.RemoveAt(index);
        }
    }
}