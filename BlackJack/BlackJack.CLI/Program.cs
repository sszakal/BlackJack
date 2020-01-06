using System;
using BlackJack.Domain;
using BlackJack.Domain.Interfaces;

namespace BlackJack.CLI
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Black Jack");
            Console.WriteLine();
            var deck = new Deck(new RandomNumberGenerator()); 
            var player = new BlackJackPlayer("Stefan");
            var dealer = new BlackJackDealer(); 
            var game = new BlackJackGame(Guid.NewGuid(), deck, dealer, player);

            Console.WriteLine("Do you want to play Black Jack? (y/n):");

            while (Console.ReadLine() == "y")
            {
                game.StartNewGame();

                DisplayPlayerInfo(game.Player);
                DisplayPlayerInfo(game.Dealer);

                Console.WriteLine($"{game.Player.Name}, do you want a hit?");

                while (!game.Player.IsBust && Console.ReadLine() == "y")
                {
                    game.PlayerHits();

                    DisplayPlayerInfo(game.Player);
                }

                if (!game.Player.IsBust)
                {
                    game.PlayerSticks();
                }

                DisplayPlayerInfo(game.Dealer);
                
                Console.WriteLine($"...and the winner is: {game.GetWinnerName()}");

                Console.WriteLine("You want to play again? (y/n)");
            }
        }

        private static void DisplayPlayerInfo(IPlayer player)
        {
            Console.WriteLine($"{player.Name}'s hand:\n");

            foreach (var card in player.Hand.Cards)
            {
                Console.WriteLine($"\t {card.Value} of {card.Suit}");
            }

            Console.WriteLine($"The score is {player.Hand.Score}\n");
        }
    }
}
