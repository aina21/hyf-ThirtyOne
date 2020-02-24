using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProject.Models
{
    public class Deck
    {
        public List<Card> cards { get; set; }
        public int cardsLeft { get {
                return cards.Count;
            } }

        public Deck()
        {
            cards = new List<Card>();
        }

        public void InitialCards() {

            foreach (Suits suit in (Suits[])Enum.GetValues(typeof(Suits)))
            {
                for (int rank = 0; rank < 13; rank++)
                {
                    Card card = new Card(suit, (rank + 1));
                    cards.Add(card);
                }
            }
        }
        public void shuffelCards(Random random)
        {
            List<Card> shuffleDeck = new List<Card>();
            int selectedCard = 0;
            while (cards.Count > 0)
            {
                selectedCard = random.Next(0, cards.Count);
                shuffleDeck.Add(cards[selectedCard]);
                cards.Remove(cards[selectedCard]);
            }
            cards = shuffleDeck;
        }

        public Card DrawCard()
        {
            if(cards.Count == 0)
            {
                return null;
            } else {
                Card card = cards[0];
                cards.Remove(card);
                return card;
            }
        }
    }
}
