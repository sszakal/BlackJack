using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.Application;
using BlackJack.Domain;
using BlackJack.Domain.Interfaces;
using BlackJack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BlackJackController : Controller
    {
        private readonly IBlackJackGameService _gameService;

        public BlackJackController(IBlackJackGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("/games")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<BlackJackGameModel> GetAll()
        {
            var allGames = _gameService.GetAllGames();
            return allGames.Select(ToModel);
        }

        [HttpGet("/games/{id}")]
        public BlackJackGameModel GetGame(Guid id)
        {
            var game = _gameService.GetGameById(id);
            return game != null ? ToModel(game) : null;
        }

        [HttpPost("/games")]
        public BlackJackGameModel CreateGame([FromBody]CreateGameModel createGameModel)
        {
            var newGameId = _gameService.CreateGame(createGameModel.PlayerName);
            var game = _gameService.GetGameById(newGameId);
            return game != null ? ToModel(game) : null;
        }

        [HttpPut("/games/{id}/start")]
        public BlackJackGameModel Start(Guid id)
        {
            var game = _gameService.GetGameById(id);
            game.StartNewGame();
            return ToModel(game);
        }

        [HttpPut("/games/{id}/hit")]
        public BlackJackGameModel Hit(Guid id)
        {
            var game = _gameService.GetGameById(id);
            game.PlayerHits();
            return ToModel(game);
        }

        [HttpPut("/games/{id}/stick")]
        public BlackJackGameModel Stick(Guid id)
        {
            var game = _gameService.GetGameById(id);
            game.PlayerSticks();
            return ToModel(game);
        }

        private BlackJackGameModel ToModel(BlackJackGame game)
        {
            var result = new BlackJackGameModel
            {
                Id = game.Id,
                Dealer = ToModel(game.Dealer),
                Player = ToModel(game.Player),
                Winner = game.GetWinnerName()
            };
            return result;
        }

        private PlayerModel ToModel(IPlayer player)
        {
            var result = new PlayerModel
            {
                Name = player.Name,
                Cards = player.Hand.Cards
                    .Select(c => new CardModel { Suit = c.Suit.ToString(), Value = c.Value.ToString() }).ToArray(),
                Busted = player.IsBust,
                Score = player.Hand.Score
            };
            return result;
        }
    }
}
