using System.Collections.Generic;
using System.Linq;
using BlackJack.Domain.Interfaces;

namespace BlackJack.Domain
{
    public class BlackJackHandEvaluator: IHandEvaluator
    {
        private static readonly Dictionary<Values, int> Mapper = new Dictionary<Values, int>
        {
            [Values.Two] = 2,
            [Values.Three] = 3,
            [Values.Four] = 4,
            [Values.Five] = 5,
            [Values.Six] = 6,
            [Values.Seven] = 7,
            [Values.Eight] = 8,
            [Values.Nine] = 9,
            [Values.Ten] = 10,
            [Values.Jack] = 10,
            [Values.Queen] = 10,
            [Values.King] = 10,
            [Values.Ace] = 10
        };

        public int CalculateScore(IReadOnlyList<Card> cards)
        {
            var total = cards
                .Where(c => c.Value != Values.Ace)
                .Select(c => Mapper[c.Value]).Sum();

            var aces = cards.Where(c => c.Value == Values.Ace).ToArray();

            if (!aces.Any()) return total;

            total += aces.Skip(1).Select(a => 1).Sum();

            if (total + 11 > 21) total += 1;
            else total += 11;

            return total;
        }
    }
}
