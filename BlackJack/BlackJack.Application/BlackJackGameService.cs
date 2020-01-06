using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.Domain;

namespace BlackJack.Application
{
    public class BlackJackGameService: IBlackJackGameService
    {
        private readonly Dictionary<Guid, BlackJackGame> _game;

        public BlackJackGameService()
        {
            _game = new Dictionary<Guid, BlackJackGame>();
        }

        public Guid CreateGame(string playerName)
        {
            var newId = Guid.NewGuid();
            var deck = new Deck(new RandomNumberGenerator());
            var dealer = new BlackJackDealer();
            var player = new BlackJackPlayer(playerName);
            _game[newId] = new BlackJackGame(newId, deck, dealer, player);
            return newId;
        }

        public BlackJackGame[] GetAllGames()
        {
            return _game.Values.ToArray();
        }

        public BlackJackGame GetGameById(Guid id)
        {
            return _game.ContainsKey(id) ? _game[id]:null;
        }
    }
}
