using System;
using BlackJack.Domain.Interfaces;

namespace BlackJack.Domain
{
    public class RandomNumberGenerator : Random, IRandomNumberGenerator
    {
        public RandomNumberGenerator()
        {
        }

        public RandomNumberGenerator(int seed): base(seed)
        {
        }

        public int Generate(int min, int max)
        {
            return this.Next(min, max);
        }
    }
}