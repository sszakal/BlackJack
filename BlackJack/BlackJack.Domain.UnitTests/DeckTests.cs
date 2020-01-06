using System.Linq;
using AutoFixture;
using BlackJack.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace BlackJack.Domain.UnitTests
{
    public class DeckTests
    {
        private readonly Fixture _fixture;

        public DeckTests()
        {
            _fixture = new Fixture();
            _fixture.Register<IRandomNumberGenerator>(() => new RandomNumberGenerator());
        }

        [Fact]
        public void ShouldImplementInterfaceIDeck()
        {
            //Arrange
            var sut = _fixture.Create<Deck>();

            //Act
            var sutType = sut.GetType();

            //Assert
            sutType.Should().Implement<IDeck>();
        }

        [Fact]
        public void NewDeckShouldContainAllCards()
        {
            // Arrange
            var expectedSuits = new[] { Suits.Clubs, Suits.Diamonds, Suits.Hearts, Suits.Spades };
            var expectedValues = new[]
            {
                Values.Two, Values.Three, Values.Four, Values.Five, Values.Six, Values.Seven,
                Values.Eight, Values.Nine, Values.Ten, Values.Jack, Values.Queen, Values.King, Values.Ace,
            };
            var sut = _fixture.Create<Deck>();

            // Act
            var cards = sut.Cards;

            // Assert
            cards.Should().OnlyHaveUniqueItems().And.HaveCount(52);
            var cardsBySuit = cards.GroupBy(c => c.Suit)
                                                           .ToDictionary(g => g.Key, g => g.Select(c => c.Value).ToArray());
            cardsBySuit.Should().NotBeEmpty().And.HaveCount(4);
            cardsBySuit.Keys.Should().BeEquivalentTo(expectedSuits);
            cardsBySuit[Suits.Clubs].Should().BeEquivalentTo(expectedValues);
            cardsBySuit[Suits.Diamonds].Should().BeEquivalentTo(expectedValues);
            cardsBySuit[Suits.Hearts].Should().BeEquivalentTo(expectedValues);
            cardsBySuit[Suits.Spades].Should().BeEquivalentTo(expectedValues);
        }

        [Fact]
        public void ShouldShuffleCards()
        {
            // Arrange
            _fixture.Register<IRandomNumberGenerator>(() => new RandomNumberGenerator(1));
            var sut = _fixture.Create<Deck>();

            // Act
            sut.ShuffleDeck();
            var cards = sut.Cards;

            // Assert
            cards.Take(5).Should().ContainInOrder(new[]
            {
                new Card(Values.Ace, Suits.Spades),
                new Card(Values.Seven, Suits.Spades),
                new Card(Values.King, Suits.Hearts),
                new Card(Values.Three, Suits.Clubs),
                new Card(Values.Ten, Suits.Diamonds)
            });
        }

        [Fact]
        public void ShouldDealCards()
        {
            // Arrange
            var sut = _fixture.Create<Deck>();
            sut.ShuffleDeck();
            var expectedCards = sut.Cards;

            // Act
            var actualCards = Enumerable.Range(0, 52).Select(i => sut.GetCard());

            // Assert
            actualCards.Should().ContainInOrder(expectedCards);
        }
    }
}
