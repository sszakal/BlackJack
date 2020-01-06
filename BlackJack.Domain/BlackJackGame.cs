using System;
using BlackJack.Domain.Interfaces;

namespace BlackJack.Domain
{
    public class BlackJackGame
    {
        public readonly Guid Id;
        public IPlayer Dealer { get; }
        public IPlayer Player { get; }
        public bool IsGameFinished { get; private set; }

        private readonly IDeck _deck;
      

        public BlackJackGame(Guid id, IDeck deck, IPlayer dealer, IPlayer player)
        {
            Id = id;
            Dealer = dealer;
            Player = player;
            _deck = deck;
            IsGameFinished = false;
        }

        public void StartNewGame()
        {
            Dealer.ClearHand();
            Player.ClearHand();

            _deck.ShuffleDeck();

            Player.Hand.AddCards(new[] { _deck.GetCard(), _deck.GetCard() });
            Dealer.Hand.AddCards(new[] { _deck.GetCard(), _deck.GetCard() });
        }

        public void PlayerHits()
        {
            if (!Player.IsBust)
            {
                DealCard(Player);
                if (Player.IsBust)
                {
                    IsGameFinished = true;
                }
            }
            else throw new InvalidOperationException();
        }

        public void PlayerSticks()
        {
            if (!Player.IsBust)
            {
                var playerScore = Player.Hand.Score;
                var dealerScore = Dealer.Hand.Score;
                
                while (dealerScore < 17 && dealerScore <= playerScore)
                {
                    DealCard(Dealer);
                    dealerScore = Dealer.Hand.Score;
                }

                IsGameFinished = true;
            }
            else throw new InvalidOperationException();
        }

        public string GetWinnerName()
        {
            if (!IsGameFinished)
            {
                return string.Empty;
            }

            if (!Player.IsBust && (Dealer.IsBust || Dealer.Hand.Score < Player.Hand.Score))
            {
                return Player.Name;
            }

            if (Player.Hand.Score == Dealer.Hand.Score || Dealer.IsBust)
            {
                return string.Empty;
            }

            return Dealer.Name;
        }

        private void DealCard(IPlayer player)
        {
            player.Hand.AddCards(new[] { _deck.GetCard() });
        }
    }
}