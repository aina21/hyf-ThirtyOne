using FirstProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThirtyOne.Helpers;

namespace ThirtyOne.Models
{
    public class Game
    {
        public Deck deck { set; get; }
        public List<Card> tableCards { set; get; }
        public List<Player> players { set; get; }
        public Random random;
        public int currentTurn;
        
        [JsonIgnore]
        public Player currentPlayer { get { return players[currentTurn]; } }
        
        public Player winner { set; get; }
        GameState state { set; get; }

        public Game()
        {
            state = GameState.WatingToStart;
            players = new List<Player>();
            random = new Random();
        }

        public Game(Random random, params Player[] players) 
        {
            this.players = players.ToList();
            this.random = random;
            StartGame();
        }
        
        private void StartGame()
        {
            deck = new Deck();
            tableCards = new List<Card>();
            currentTurn = 0;

            state = GameState.InProgress;

            deck.InitialCards();
            deck.shuffelCards(random);
            InitialDeal();

        }

        private void InitialDeal()
        {
            foreach(Player player in players)
            {
                for(int card=0; card<3; card++)
                {
                    player.hand.Add(deck.DrawCard());
                }
            }

            tableCards.Add(deck.DrawCard());
        }

        public bool EvaluateIfGameOver(bool called)
        {
            var winPlayer = (called) ?
                players.Where(player => player.hand.Count == 3).OrderByDescending(player => player.hand.CalculateScore()).First() //The game has been called, highest score is the winner
                : players.Where(player => player.hand.Count == 3 && player.hand.CalculateScore() == 31).FirstOrDefault(); //Game has not been called, but a player has 31 and wins.

            if (winPlayer != null)
            {
                this.winner = winPlayer;
                this.state = GameState.GameOver;
                return true;
            }
            return false;
        }

        public bool NextTurn()
        {
            //Ask player to do their turn
            currentPlayer.Turn(this);

            if (EvaluateIfGameOver(false))
            {
                //Player won, report
                return true;
            }

            //Move to the next player
            currentTurn++;
            if (currentTurn >= players.Count) currentTurn = 0;
            if (currentPlayer.hasKnocked)
            {
                //Next player had already knocked - let's evaluate the call
                EvaluateIfGameOver(true);
                //Game over!
                return true;
            }

            if (deck.cardsLeft == 0)
            {
                //If there's no more cards in the deck, let's take those from the table
                deck.cards.AddRange(tableCards);
                tableCards.Clear();
            }

            if (currentPlayer is ComputerPlayer) return NextTurn(); //If the next player is the computer, execute that turn right away.
            else return false;
        }

        public string SerializeGame()
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            return JsonConvert.SerializeObject(this, jsonSerializerSettings);
        }

        public static Game DeserializeGame(string json)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            return JsonConvert.DeserializeObject<Game>(json, jsonSerializerSettings);
        }
    }
}
