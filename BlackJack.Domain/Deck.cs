using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BlackJack.Domain.Interfaces;

namespace BlackJack.Domain
{
    public class Deck: IDeck
    {
        private readonly Card[] _cards;
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private int _currentCardIndex;

        public Deck(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _cards = BuildCards().ToArray();
            _currentCardIndex = 0;
        }

        public void ShuffleDeck()
        {
            _currentCardIndex = 0;
            for (var i = 0; i < _cards.Length; ++i)
            {
                int j = _randomNumberGenerator.Generate(0, _cards.Length);
                var temp = _cards[j];
                _cards[i] = _cards[j];
                _cards[j] = temp;
            }
        }

        public Card GetCard()
        {
            if (_currentCardIndex == _cards.Length) _currentCardIndex = 0;
            return _cards[_currentCardIndex++];
        }

        private static IEnumerable<Card> BuildCards()
        {
            return Enum.GetValues(typeof(Suits))
                .OfType<Suits>()
                .SelectMany(suit => Enum.GetValues(typeof(Values))
                    .OfType<Values>()
                    .Select(value => new Card(value, suit)));
        }

        public IReadOnlyList<Card> Cards => _cards.ToImmutableList();
    }
}