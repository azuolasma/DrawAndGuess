using System;

namespace DrawandGuess.Models
{
    public enum PlayerRole
    {
        Drawer,
        Guesser
    }

    public class Player : IComparable<Player>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public PlayerRole Role { get; set; }

        public Player(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Score = 0;
            Role = PlayerRole.Guesser;
        }

        public int CompareTo(Player? other)
        {
            if (other == null) return 1;
            return other.Score.CompareTo(this.Score); // Descending order
        }
    }
}
