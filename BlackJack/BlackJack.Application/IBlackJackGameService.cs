using System;
using BlackJack.Domain;

namespace BlackJack.Application
{
    public interface IBlackJackGameService
    {
        Guid CreateGame(string playerName);
        BlackJackGame[] GetAllGames();
        BlackJackGame GetGameById(Guid id);
    }
}