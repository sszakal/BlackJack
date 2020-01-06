using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.Domain;

namespace BlackJack.Application
{
    public class BlackJackGameService: IBlackJackGameService
    {
        private readonly Dictionary<Guid, BlackJackGame> _games;

        public BlackJackGameService()
        {
            _games = new Dictionary<Guid, BlackJackGame>();
        }

        public Guid CreateGame(string playerName)
        {
            var newId = Guid.NewGuid();
            var deck = new Deck(new RandomNumberGenerator());
            var dealer = new BlackJackDealer();
            var player = new BlackJackPlayer(playerName);
            _games[newId] = new BlackJackGame(newId, deck, dealer, player);
            return newId;
        }

        public BlackJackGame[] GetAllGames()
        {
            return _games.Values.ToArray();
        }

        public BlackJackGame GetGameById(Guid id)
        {
            return _games.ContainsKey(id) ? _games[id]:null;
        }
    }
}
