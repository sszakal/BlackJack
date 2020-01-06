using BlackJack.Domain.Interfaces;

namespace BlackJack.Domain
{
    public class BlackJackPlayer : IPlayer
    {
        public string Name { get; }
        public IHand Hand { get; private set; }
        public bool IsBust => Hand.Score >= 21;

        public void ClearHand()
        {
            Hand = new Hand(new BlackJackHandEvaluator());
        }

        public BlackJackPlayer(string name)
        {
            Name = name;
            Hand = new Hand(new BlackJackHandEvaluator());
        }
    }
}