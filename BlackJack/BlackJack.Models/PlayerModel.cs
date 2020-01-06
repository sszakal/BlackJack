namespace BlackJack.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }

        public CardModel[] Cards { get; set; }

        public int Score { get; set; }

        public bool Busted { get; set; }
    }
}