using AutoFixture;
using BlackJack.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace BlackJack.Domain.UnitTests
{
    public class BlackJackPlayerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void ShouldImplementInterfaceIPlayer()
        {
            //Arrange
            var sut = _fixture.Create<BlackJackPlayer>();

            //Act
            var sutType = sut.GetType();

            //Assert
            sutType.Should().Implement<IPlayer>();
        }

        [Fact]
        public void ShouldHaveAName()
        {
            //Arrange
            var expectedName = "Player Name";
            _fixture.Register(() => expectedName);
            var sut = _fixture.Create<BlackJackPlayer>();

            //Act
            var actualName = sut.Name;

            //Assert
            actualName.Should().Be(expectedName);
        }


        [Fact]
        public void ShouldClearHand()
        {
            //Arrange
            var sut = _fixture.Create<BlackJackPlayer>();
            sut.Hand.AddCards(new[] { new Card(Values.Ace, Suits.Hearts) });

            //Act
            sut.ClearHand();

            //Assert
            sut.Hand.Cards.Should().BeEmpty();
        }
    }
}
