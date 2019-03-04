using System.Collections.Generic;

namespace TictactoeVer2
{
    public class Tictactoe
    {
        public Player CurrentPlayer { get; set; }
        public GameStatus Status { get; set; }
        public Player Winner { get; set; }
        public GameBoard Board { get; set; }
        public UserInputValidator Validator { get; set; }

        public Tictactoe()
        {
            CurrentPlayer = Player.X;
            Board = new GameBoard();
            Validator = new UserInputValidator();
        }

        public void MakeMove(string input)
        {
            var hasUserQuit = Validator.HasUserQuit(input);
            if (Validator.IsValidInput(input) && !hasUserQuit)
            {
                var symbol = CurrentPlayer == Player.X ? 'X' : 'O';
                var isMoveSuccessful = Board.FillCoordinate(input, symbol);
                if (isMoveSuccessful)
                {
                    Winner = Board.IsWinningMove(symbol) ? CurrentPlayer : Player.None;
                    SwitchPlayer();
                }
            }
            if(hasUserQuit)
            {
                Status = GameStatus.Ended;
            }
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
        }

        
    }

    public enum GameStatus
    {
        Playing,
        Ended
    }

    public enum Player
    {
        None, 
        X,
        O
    }
}