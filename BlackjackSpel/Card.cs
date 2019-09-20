using System;
using System.Collections.Generic;
using System.Text;

namespace BlackjackSpel
{
    /// <summary>
    /// Representerar ett kort i en kortlek.
    /// </summary>
    class Card
    {
        /// <summary>
        /// Kortets nummer.
        /// </summary>
        public int number;
        /// <summary>
        /// Kortets färg.
        /// </summary>
        public Color color;

        /// <summary>
        /// Kortest "färg" i spelet.
        /// </summary>
        public enum Color
        {
            Hjärter,
            Spader,
            Ruter,
            Klöver
        }

        /// <summary>
        /// Ger kortets värde. Mest för att hantera essen så att de blir det bästa värdet för spelaren.
        /// </summary>
        /// <param name="currentValue">Nuvarande totalvärde som räknas.</param>
        /// <returns>Kortets värde. Om ess så ges ett värde beroende på om det överstiger 21 eller ej.</returns>
        public int GetBestCardValue(int currentValue = 0)
        {

            if (number == 14 && currentValue + 14 > 21)
            {
                return 1;
            }

            return number;
        }

        /// <summary>
        /// Ger en människoläsbar sträng om vilket kort det här är.
        /// </summary>
        /// <param name="showValue">Om sant så visas dess värde i paranteser också.</param>
        /// <returns>Människoläsbar sträng av kortet.</returns>
        public string ToHumanReadable(bool showValue = false)
        {
            string result = color.ToString() + " ";
            if (number == 11)
            {
                result += "Knekt";
            }
            else if (number == 12)
            {
                result += "Dam";
            }
            else if (number == 13)
            {
                result += "Kung";
            }
            else if (number == 14)
            {
                result += "Ess";
            }
            else
            {
                result += number.ToString();
            }

            if(showValue == true)
            {
                result += $" ({number.ToString()})";
            }

            return result;
        }
    }
}
