using System.Collections.Generic;

namespace TictactoeVer2
{
    public class Tictactoe
    {
        public Player CurrentPlayer { get; set; }
        public GameStatus Status { get; set; }
        public Player Winner { get; set; }
        public GameBoard Board { get; set; }

        public Tictactoe()
        {
            CurrentPlayer = Player.X;
            Board = new GameBoard();
        }

        public void MakeMove(string input)
        {
            var symbol = CurrentPlayer == Player.X ? 'X' : 'O';
            Board.FillCoordinate(input, symbol);
            SwitchPlayer();
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
        }
    }

    public enum GameStatus
    {
        Playing
    }

    public enum Player
    {
        None, 
        X,
        O
    }
}