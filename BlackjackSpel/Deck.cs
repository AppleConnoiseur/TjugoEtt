using System;
using System.Collections.Generic;
using System.Text;

namespace BlackjackSpel
{
    /// <summary>
    /// Representerar en kortlek med kort i.
    /// </summary>
    class Deck
    {
        /// <summary>
        /// Alla kort som nuvarande finns i kortleken.
        /// </summary>
        public List<Card> cards = new List<Card>();

        /// <summary>
        /// Skapar en kortlek ifrån grunden med alla fyra "färger".
        /// </summary>
        public void Create()
        {
            for (int i = 0; i < 4; i++)
            {
                Card.Color color = (Card.Color)i;

                for (int y = 2; y <= 14; y++)
                {
                    Card card = new Card();
                    card.color = color;
                    card.number = y;
                    cards.Add(card);
                }
            }
        }

        /// <summary>
        /// En oerhört dålig kortleksblandare :D.
        /// </summary>
        public void ShuffleDeck()
        {
            Random rand = new Random();
            int numberOfCards = cards.Count;

            for(int i = 0; i < numberOfCards; i++)
            {
                Card card = cards[0];
                cards.Remove(card);
                cards.Insert(rand.Next(0, numberOfCards), card);
            }
        }

        /// <summary>
        /// Skriver ut hela kortleken i människoläsbart format.
        /// </summary>
        /// <param name="showValue">Om kortens värde ska visas.</param>
        public void PrintOutDeck(bool showValue = false)
        {
            foreach (var card in cards)
            {
                Console.WriteLine($"Kort: {card.ToHumanReadable(showValue)}");
            }
        }

        /// <summary>
        /// Dra ett kort ur kortleken.
        /// </summary>
        /// <returns>Ett kort ifrån kortleken.</returns>
        public Card DrawCard()
        {
            if(cards.Count <= 0)
            {
                return null;
            }
            Card card = cards[0];
            cards.Remove(card);
            return card;
        }
    }
}
