using AutoFixture;
using BlackJack.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace BlackJack.Domain.UnitTests
{
    public class BlackJackDealerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void ShouldImplementInterfaceIPlayer()
        {
            //Arrange
            var sut = _fixture.Create<BlackJackDealer>();

            //Act
            var sutType = sut.GetType();

            //Assert
            sutType.Should().Implement<IPlayer>();
        }

        [Fact]
        public void ShouldHaveAPredefinedName()
        {
            //Arrange
            var sut = _fixture.Create<BlackJackDealer>();

            //Act
            var actualName = sut.Name;

            //Assert
            actualName.Should().Be("The Dealer");
        }


        [Fact]
        public void ShouldClearHand()
        {
            //Arrange
            var sut = _fixture.Create<BlackJackDealer>();
            sut.Hand.AddCards(new[] { new Card(Values.Ace, Suits.Hearts) });

            //Act
            sut.ClearHand();

            //Assert
            sut.Hand.Cards.Should().BeEmpty();
        }
    }
}
