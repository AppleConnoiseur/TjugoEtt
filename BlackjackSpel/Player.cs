using System;
using System.Collections.Generic;
using System.Text;

namespace BlackjackSpel
{
    /// <summary>
    /// Representarar en spelare.
    /// </summary>
    class Player
    {
        /// <summary>
        /// Spelarens namn.
        /// </summary>
        public string name;

        /// <summary>
        /// Hur många gånger spelaren har vunnit.
        /// </summary>
        public int wins = 0;

        /// <summary>
        /// Är spelaren banken?
        /// </summary>
        //public bool dealer;

        /// <summary>
        /// Alla kort som nuvarande finns i handen.
        /// </summary>
        public List<Card> cards = new List<Card>();

        /// <summary>
        /// Räknar ut totala summan av alla kort.
        /// </summary>
        /// <param name="dealer">Om den är inte tom så räknar bankens kort in.</param>
        /// <returns>Totala summan av alla kort.</returns>
        public int TotalCardsValue(Player dealer = null, int currentValue = 0)
        {
            int value = currentValue;
            if(dealer != null)
            {
                value += dealer.TotalCardsValue(dealer, value);
            }

            foreach (var card in cards)
            {
                value += card.GetBestCardValue(value);
            }
            return value;
        }

        /// <summary>
        /// Skriver ut alla kort på hand.
        /// </summary>
        /// <param name="showValue">Om sant så skrivs även kortens värden ut.</param>
        public void PrintOutCards(bool showValue = false)
        {
            foreach (var card in cards)
            {
                Console.WriteLine($"Kort: {card.ToHumanReadable(showValue)}");
            }
        }
    }
}
