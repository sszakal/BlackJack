using System.Collections.Generic;
using AutoFixture;
using BlackJack.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace BlackJack.Domain.UnitTests
{
    public class BlackJackHandEvaluatorTests
    {
        [Fact]
        public void ShouldImplementInterfaceIHandEvaluator()
        {
            //Arrange
            var sut = new Fixture().Create<BlackJackHandEvaluator>();

            //Act
            var sutType = sut.GetType();

            //Assert
            sutType.Should().Implement<IHandEvaluator>();
        }

        [Theory]
        [MemberData(nameof(HandData))]
        public void ShouldEvaluateHand(Card[] cards, int expectedScore)
        {
            //Arrange
            var sut = new Fixture().Create<BlackJackHandEvaluator>();

            //Act
            var actualScore = sut.CalculateScore(cards);

            //Assert
            actualScore.Should().Be(expectedScore);
        }

        public static IEnumerable<object[]> HandData
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new []
                        {
                            new Card(Values.Ace, Suits.Hearts),
                            new Card(Values.Ace, Suits.Diamonds),
                            new Card(Values.Ace, Suits.Clubs),
                            new Card(Values.Ace, Suits.Spades),
                        }, 
                        14
                    },
                    new object[]
                    {
                        new []
                        {
                            new Card(Values.Ace, Suits.Hearts),
                            new Card(Values.Ace, Suits.Diamonds),
                            new Card(Values.Ace, Suits.Clubs),
                            new Card(Values.Ace, Suits.Spades),
                            new Card(Values.Ten, Suits.Hearts)
                        },
                        14
                    },
                    new object[]
                    {
                        new []
                        {
                            new Card(Values.Two, Suits.Hearts),
                            new Card(Values.Two, Suits.Diamonds),
                            new Card(Values.Two, Suits.Clubs),
                            new Card(Values.Two, Suits.Spades),
                            new Card(Values.Ten, Suits.Hearts)
                        },
                        18
                    },
                    new object[]
                    {
                        new []
                        {
                            new Card(Values.Ace, Suits.Hearts)
                        },
                        11
                    },
                    new object[]
                    {
                        new []
                        {
                            new Card(Values.Ace, Suits.Hearts),
                            new Card(Values.Ten, Suits.Hearts)
                        },
                        21
                    }
                };
            }
        }
    }
}
