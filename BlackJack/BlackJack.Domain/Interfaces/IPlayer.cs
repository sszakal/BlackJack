namespace BlackJack.Domain.Interfaces
{
    public interface IPlayer
    {
        string Name { get; }
        IHand Hand { get; }
        bool IsBust { get; }
        void ClearHand();
    }
}