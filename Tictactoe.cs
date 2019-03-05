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
        public MessageHandler MessageHandler { get; set; }

        public Tictactoe()
        {
            CurrentPlayer = Player.X;
            Board = new GameBoard();
            Validator = new UserInputValidator();
            MessageHandler = new MessageHandler();

            StartGame();
        }

        private void StartGame()
        {
            MessageHandler.WelcomeToGame();
            Board.DisplayBoard();

            MessageHandler.DisplayMakeMove(CurrentPlayer);
        }

        public void MakeMove(string input)
        {
            var hasUserQuit = Validator.HasUserQuit(input);
            var isInputValid = Validator.IsValidInput(input);
            
            if (isInputValid && !hasUserQuit)
            {
                var symbol = CurrentPlayer == Player.X ? 'X' : 'O';
                var isMoveSuccessful = Board.FillCoordinate(input, symbol);
                if (isMoveSuccessful)
                {
                    MessageHandler.DisplayMoveAccepted();
                    
                    Winner = Board.IsWinningMove ? CurrentPlayer : Player.None;
//                    if (Winner == Player.None && Board.IsBoardFilled)
//                    {
//                        MessageHandler.DisplayNoWinner();
//                        Status = GameStatus.Draw;
//                    }

                    if (Winner == Player.None)
                    {
                        if (Board.IsBoardFilled)
                        {
                            MessageHandler.DisplayNoWinner();
                            Status = GameStatus.Draw;
                        }
                        else
                        {
                            Board.DisplayBoard();
                            SwitchPlayer(); 
                            MessageHandler.DisplayMakeMove(CurrentPlayer);
                        }
                    }
                    else
                    {
                        Board.DisplayBoard();
                        MessageHandler.DisplayWinner(Winner);
                        Status = GameStatus.Ended;
                    }
                    
                }
                else
                {
                    MessageHandler.DisplayCoordinateFilled();
                }
            }
            if(hasUserQuit)
            {
                MessageHandler.DisplayUserHasQuit(CurrentPlayer);
                Winner = CurrentPlayer == Player.X ? Player.O : Player.X;
                MessageHandler.DisplayWinner(Winner);
                Status = GameStatus.Ended;
            }
            if(!isInputValid)
            {
                MessageHandler.DisplayMoveInvalid();
            }
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
        }
    }

    public enum GameStatus
    {
        Playing,
        Ended,
        Draw
    }

    public enum Player
    {
        None, 
        X,
        O
    }
}