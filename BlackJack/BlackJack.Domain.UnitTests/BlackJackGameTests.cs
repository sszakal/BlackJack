using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using BlackJack.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace BlackJack.Domain.UnitTests
{
    public class BlackJackGameTests
    {
        private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        [Fact]
        public void WhenCreatingANewGameTheGameShouldBeInTheCorrectState()
        {
            //Arrange
            var sut = _fixture.Create<BlackJackGame>();

            //Act
            sut.StartNewGame();

            //Assert
            sut.IsGameFinished.Should().BeFalse();
        }

        [Fact]
        public void WhenStartingANewGameItShouldClearTheHandsAndDealTwoCardsToThePlayerAndTheDealer()
        {
            //Arrange
            var handMock = _fixture.Create<Mock<IHand>>();

            var playerMock = _fixture.Freeze<Mock<IPlayer>>();
            playerMock.Setup(p => p.Hand).Returns(handMock.Object);

            var deckMock = _fixture.Freeze<Mock<IDeck>>();
            deckMock.Setup(d => d.GetCard()).ReturnsUsingFixture(_fixture);

            var sut = _fixture.Create<BlackJackGame>();

            //Act
            sut.StartNewGame();

            //Assert
            playerMock.Verify(p => p.ClearHand(), Times.Exactly(2));
            handMock.Verify(h => h.AddCards(It.IsAny<IReadOnlyList<Card>>()), Times.Exactly(2));
        }

        [Fact]
        public void WhenThePlayerHitsItShouldDealACard()
        {
            //Arrange
            var handMock = _fixture.Create<Mock<IHand>>();

            var playerMock = _fixture.Freeze<Mock<IPlayer>>();
            playerMock.Setup(p => p.Hand).Returns(handMock.Object);

            var deckMock = _fixture.Freeze<Mock<IDeck>>();
            deckMock.Setup(d => d.GetCard()).ReturnsUsingFixture(_fixture);

            var sut = _fixture.Create<BlackJackGame>();

            //Act
            sut.PlayerHits();

            //Assert
            handMock.Verify(h => h.AddCards(It.IsAny<IReadOnlyList<Card>>()), Times.Once);
        }

        [Fact]
        public void WhenThePlayerHitsItShouldThrowExceptionIfThePlayerIsAlreadyBust()
        {
            //Arrange
            var playerMock = _fixture.Freeze<Mock<IPlayer>>();
            playerMock.Setup(p => p.IsBust).Returns(true);
            var sut = _fixture.Create<BlackJackGame>();

            //Act
            Action act = () => sut.PlayerHits();

            //Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact] 
        public void WhenThePlayerHitsItShouldMarkTheGameAsFinishedIfThePlayerBecomesBust()
        {
            //Arrange
            var playerMock = _fixture.Freeze<Mock<IPlayer>>();
            playerMock.SetupSequence(p => p.IsBust).Returns(false).Returns(true);
            var sut = _fixture.Create<BlackJackGame>();

            //Act
            sut.PlayerHits();

            //Assert
            sut.IsGameFinished.Should().BeTrue();
        }

        [Fact]
        public void WhenThePlayerSticksItShouldThrowExceptionIfThePlayerIsAlreadyBust()
        {
            //Arrange
            var playerMock = _fixture.Freeze<Mock<IPlayer>>();
            playerMock.Setup(p => p.IsBust).Returns(true);
            var sut = _fixture.Create<BlackJackGame>();

            //Act
            Action act = () => sut.PlayerSticks();

            //Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenThePlayerSticksItShouldDealCardsToTheDealerUntilItsOver17ThenMarkTheGameAsFinished()
        {
            //Arrange
            var handMock = _fixture.Create<Mock<IHand>>();
            handMock.SetupSequence(h => h.Score).Returns(18).Returns(2).Returns(10).Returns(14).Returns(17);

            var playerMock = _fixture.Freeze<Mock<IPlayer>>();
            playerMock.Setup(p => p.Hand).Returns(handMock.Object);
           

            var deckMock = _fixture.Freeze<Mock<IDeck>>();
            deckMock.Setup(d => d.GetCard()).ReturnsUsingFixture(_fixture);
            var sut = _fixture.Create<BlackJackGame>();

            //Act
            sut.PlayerSticks();

            //Assert
            handMock.Verify(h => h.AddCards(It.IsAny<IReadOnlyList<Card>>()), Times.Exactly(3));
            sut.IsGameFinished.Should().BeTrue();
        }
    }
}
