using System.Collections.Generic;
using BlackJack.Domain.Interfaces;

namespace BlackJack.Domain
{
    public class Hand : IHand
    {
        private readonly List<Card> _cards;
        private readonly IHandEvaluator _handEvaluator;

        public Hand(IHandEvaluator handEvaluator)
        {
            _handEvaluator = handEvaluator;
            _cards = new List<Card>();
        }

        public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

        public void AddCards(IReadOnlyList<Card> cards)
        {
            _cards.AddRange(cards);
        }

        public int Score => _handEvaluator.CalculateScore(_cards);
    }
}