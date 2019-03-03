using System.Collections.Generic;

namespace TictactoeVer2
{
    public class Tictactoe
    {
        public Player CurrentPlayer { get; set; }
        public bool Ended { get; set; }
        public GameStatus Status { get; set; }
    }

    public enum GameStatus
    {
        Playing
    }

    public enum Player
    {
        X
    }
}