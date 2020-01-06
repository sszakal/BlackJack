using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using BlackJack.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace BlackJack.Domain.UnitTests
{
    public class HandTests
    {
        private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        [Fact]
        public void ShouldImplementInterfaceIHand()
        {
            //Arrange
            var sut = _fixture.Create<Hand>();

            //Act
            var sutType = sut.GetType();

            //Assert
            sutType.Should().Implement<IHand>();
        }

        [Fact]
        public void ANewHandShouldHaveNoCards()
        {
            // Arrange
            var sut = _fixture.Create<Hand>();

            // Act
            var cards = sut.Cards;

            // Assert
            cards.Should().BeEmpty();
        }

        [Fact]
        public void AHandCanReceiveCards()
        {
            // Arrange
            var sut = _fixture.Create<Hand>();

            // Act
            sut.AddCards(new[]
            {
                new Card(Values.Ace, Suits.Hearts), 
                new Card(Values.Eight, Suits.Clubs)
            });

            sut.AddCards(new[]
            {
                new Card(Values.Ace, Suits.Spades),
                new Card(Values.Seven, Suits.Clubs)
            });

            // Assert
            sut.Cards.Should().ContainInOrder(new[]
            {
                new Card(Values.Ace, Suits.Hearts),
                new Card(Values.Eight, Suits.Clubs),
                new Card(Values.Ace, Suits.Spades),
                new Card(Values.Seven, Suits.Clubs)
            });
        }

        [Fact]
        public void ShouldCalculateScoreUsingEvaluator()
        {
            //Arrange
            var handEvaluatorMock = _fixture.Freeze<Mock<IHandEvaluator>>();
            var sut = _fixture.Create<Hand>();
            handEvaluatorMock.Setup(e => e.CalculateScore(It.IsAny<IReadOnlyList<Card>>()))
                             .Returns(21);

            //Act
            var actualScore = sut.Score;

            //Assert
            actualScore.Should().Be(21);
        }
    }
}
