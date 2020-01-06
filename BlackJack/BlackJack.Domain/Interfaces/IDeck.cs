using System.Collections.Generic;

namespace BlackJack.Domain.Interfaces
{
    public interface IDeck
    {
        IReadOnlyList<Card> Cards { get; }

        void ShuffleDeck();
        Card GetCard();
    }
}