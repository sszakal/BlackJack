using System.Collections.Generic;

namespace BlackJack.Domain.Interfaces
{
    public interface IHandEvaluator
    {
        int CalculateScore(IReadOnlyList<Card> cards);
    }
}