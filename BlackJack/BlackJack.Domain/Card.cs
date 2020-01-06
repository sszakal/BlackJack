namespace BlackJack.Domain
{
    public struct Card
    {
        public Values Value { get; }

        public Suits Suit { get; }

        public Card(Values value, Suits suit)
        {
            this.Value = value;
            this.Suit = suit;
        }
    }
}