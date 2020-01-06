using System.Collections.Generic;

namespace BlackJack.Domain.Interfaces
{
    public interface IHand
    {
        IReadOnlyList<Card> Cards { get; }

        void AddCards(IReadOnlyList<Card> cards);
        
        int Score { get; }
    }
}