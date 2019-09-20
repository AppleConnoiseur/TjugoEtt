using System;
using System.Collections.Generic;
using System.Threading;

namespace BlackjackSpel
{
    /*
     * Medverkande: Jesper Eriksson, Vahid Daliri, Sureerat Poonsuk & Lydia Tengblad
     * Denna version är slutförd av Jesper Eriksson 2019-09-19 22:06
     */
    class Program
    {
        static void Main(string[] args)
        {
            //Initialiserar en lista med spelare som kommer att användas under spelets gång.
            List<Player> players = new List<Player>();
            //Player dealer = null;
            Deck deck = new Deck();

            //Skriv en välkomstskärm.
            Console.WriteLine("Välkommen till spelet Tjugoett!");
            Console.WriteLine();
            Console.Write("Hur många spelare ska spela: ");
            //Fråga spelaren om hur många som kommer att spela.
            string inputText = Console.ReadLine();
            int amountOfPlayers = int.Parse(inputText);
            Console.WriteLine();

            //Börja med att lägga till spelarna efter antalet som skrevs in.
            for (int i = 0; i < amountOfPlayers; i++)
            {
                //Skapa en ny spela.
                Player newPlayer = new Player();
                /*if(i == 0)
                {
                    newPlayer.dealer = true;
                    dealer = newPlayer;
                }*/

                //Spelaren får skriva in sitt namn.
                bool nameEntered = false;
                //Spelaren tvingas att skriva ett namn som INTE är tomt.
                while (nameEntered == false)
                {
                    //Console.Write($"Ange namn för {(newPlayer.dealer ? "banken" : "spelare")} {i + 1}: ");
                    Console.Write($"Ange namn för spelare {i + 1}: ");
                    string inputName = Console.ReadLine();
                    newPlayer.name = inputName;
                    if(inputName.Length > 0)
                    {
                        nameEntered = true;
                    }
                    else
                    {
                        Console.WriteLine("Namnet får ej vara tomt. Skriv om igen.");
                    }
                }
                
                //if(newPlayer.dealer == false)
                {
                    players.Add(newPlayer);
                }
            }

            deck.Create();

            //Blanda korten så gott det går med min dåliga algorithm.
            deck.ShuffleDeck();
            deck.ShuffleDeck();
            deck.ShuffleDeck();

            Console.WriteLine("Spelet startar!");
            while (deck.cards.Count > 0)
            {
                //Radera alla kort från förra rundan för spelarna.
                foreach (var player in players)
                {
                    player.cards.Clear();
                }

                //Första fasen, alla spelare får ett kort.
                foreach (var player in players)
                {
                    player.cards.Add(deck.DrawCard());
                }

                bool roundIsOver = false;
                Player roundWinner = null;
                //Låt varje spelare ha sin tur.
                foreach (var player in players)
                {
                    if(roundIsOver)
                    {
                        //Rundan blir automatiskt över om en spelare får 21.
                        Console.WriteLine($"Spelare {player.name} vann rundan. Skippar resterande spelare.");
                        break;
                    }
                    else
                    {
                        //Kolla på kortet. Kan få mer om de vill.
                        Console.WriteLine($"{player.name} har:");
                        player.PrintOutCards();

                        bool happyWithCards = false;
                        while (happyWithCards == false)
                        {
                            Console.WriteLine($"Du har {player.TotalCardsValue()} totalt i poäng.");

                            //Om spelaren har lyckats att få 5 kort så räknas det som 21 och de vinner.
                            if (player.cards.Count >= 5)
                            {
                                Console.WriteLine("Du har 5 kort! Du vann denna runda!");
                                happyWithCards = true;
                                player.wins++;
                                roundIsOver = true;
                                roundWinner = player;
                            }
                            else
                            {
                                Console.Write("Är du inte nöjd? Tryck på 'N'. ");
                                if (Console.ReadKey().KeyChar != 'n')
                                {
                                    happyWithCards = true;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Card card = deck.DrawCard();
                                    if (card == null)
                                    {
                                        break;
                                    }
                                    Console.WriteLine($"Du drog: {card.ToHumanReadable()}");
                                    player.cards.Add(card);
                                    int totalCardsValue = player.TotalCardsValue();
                                    if (totalCardsValue == 21)
                                    {
                                        Console.WriteLine("Du har exakt 21 i kortvärde! Du vann denna runda!");
                                        happyWithCards = true;
                                        player.wins++;
                                        roundIsOver = true;
                                        roundWinner = player;
                                    }
                                    else if (totalCardsValue > 21)
                                    {
                                        Console.WriteLine("Du har över 21 poäng, du blev tjock!");
                                        happyWithCards = true;
                                    }

                                    Console.WriteLine($"{player.name} har:");
                                    player.PrintOutCards();
                                }
                            }
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }

                if (!roundIsOver)
                {
                    //Ingen spelare har vunnit än. Kolla vilken som är närmast 21.
                    Player closestTo21 = players[0];    //Första spelaren blir automatiskt närmast.
                    Player tiedPlayer = null;
                    bool roundEndsInTie = false;
                    foreach (var player in players)
                    {
                        int playerTotalCardsValue = player.TotalCardsValue();
                        //"Tjocka" spelare räknas inte med så den närmsta spelaren byts ut.
                        if (playerTotalCardsValue > 21)
                        {
                            closestTo21 = player;
                        }
                        //Spelar som jämförs får inte vara den samma.
                        else if (player != closestTo21)
                        {
                            //Spelaren med minst skillnad i differens ifrån 21 blir närmast.
                            int playerValueDifference = 21 - playerTotalCardsValue;
                            int closestPlayerValueDifference = 21 - closestTo21.TotalCardsValue();
                            if (playerValueDifference == closestPlayerValueDifference)
                            {
                                //Its a tie!
                                tiedPlayer = player;
                                roundEndsInTie = true;
                            }
                            else if (playerValueDifference < closestPlayerValueDifference)
                            {
                                closestTo21 = player;
                                roundEndsInTie = false;
                            }
                        }
                    }

                    //Det blev oavgjort med ingen vinnare.
                    if (roundEndsInTie)
                    {
                        Console.WriteLine($"Det slutade oavgjort mellan {closestTo21.name} och {tiedPlayer.name}.");
                        Console.Read();
                        Console.WriteLine();
                    }
                    //Det blev en vinnare!
                    else
                    {
                        Console.WriteLine($"Spelaren {closestTo21.name} vann rundan!");
                        Console.Read();
                        Console.WriteLine();
                        closestTo21.wins++;
                    }
                }
            }

            Console.WriteLine("Korten är nu slut och vinnaren kommer att krönas...");
            Thread.Sleep(3000);

            //Skriv ut slutgiltiga vinsttavlan innan spelet avslutas.
            WriteScoreboard(players);
            Console.Read();
        }

        public static void WriteScoreboard(List<Player> players)
        {
            Player topPlayer = players[0];
            Player tiePlayer = null;
            bool itsATie = false;
            Console.WriteLine($"{"Spelare",-20}Vinster");
            foreach (var player in players)
            {
                //Passa på att skriv ut spelaren medans vinnaren letas efter.
                Console.WriteLine($"{player.name, -20}{player.wins}");
                //Leta efter vinnaren.
                if (player != topPlayer)
                {
                    if (player.wins > topPlayer.wins)
                    {
                        topPlayer = player;
                        itsATie = false;
                    }
                    else if(player.wins == topPlayer.wins)
                    {
                        tiePlayer = player;
                        itsATie = true;
                    }
                }
            }

            if (itsATie)
            {
                Console.WriteLine($"Det är oavgjort mellan {topPlayer.name} och {tiePlayer.name}. Ingen vann.");
            }
            else
            {
                Console.WriteLine($"Utav {players.Count} spelare så vann {topPlayer.name}!");
            }
        }
    }
}
