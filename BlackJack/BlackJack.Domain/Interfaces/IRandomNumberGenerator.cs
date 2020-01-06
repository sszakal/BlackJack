namespace BlackJack.Domain.Interfaces
{
    public interface IRandomNumberGenerator
    {
        int Generate(int min, int max);
    }
}