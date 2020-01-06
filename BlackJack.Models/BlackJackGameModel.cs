using System;

namespace BlackJack.Models
{
    public class BlackJackGameModel
    {
        public Guid Id { get; set; }

        public PlayerModel Dealer { get; set; }

        public PlayerModel Player { get; set; }

        public string Winner { get; set; }
    }
}
