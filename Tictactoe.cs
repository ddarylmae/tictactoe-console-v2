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

            MessageHandler.DisplayTakeTurn(CurrentPlayer);
        }

        public void InterpretInput(string input)
        {
            if (CanTakeTurn(input))
            {
                PerformTurn(input);
            }
            
            if(Validator.HasUserQuit(input))
            {
                QuitGame();
            }
            
            if(!Board.IsValidCoordinate(input))
            {
                MessageHandler.DisplayMoveInvalid();
            }
        }

        private void QuitGame()
        {
            MessageHandler.DisplayUserHasQuit(CurrentPlayer);
            Winner = CurrentPlayer == Player.X ? Player.O : Player.X;
            MessageHandler.DisplayWinner(Winner);
            Status = GameStatus.Ended;
        }

        private void PerformTurn(string input)
        {
            var isMoveSuccessful = MakeMove(input);

            if (isMoveSuccessful)
            {
                MessageHandler.DisplayMoveAccepted();
                Board.DisplayBoard();

                if (Board.IsWinningMove)
                {
                    DeclareWinner();
                }
                else
                {
                    if (Board.IsBoardFilled)
                    {
                        DeclareDraw();
                    }
                    else
                    {
                        ContinueGame();
                    }
                }
            }
            else
            {
                MessageHandler.DisplayCoordinateIsFilled();
            }
        }

        private void ContinueGame()
        {
            SwitchPlayer();
            MessageHandler.DisplayTakeTurn(CurrentPlayer);
        }

        private void DeclareDraw()
        {
            MessageHandler.DisplayNoWinner();
            Status = GameStatus.Draw;
        }

        private void DeclareWinner()
        {
            Winner = CurrentPlayer;
            MessageHandler.DisplayWinner(Winner);
            Status = GameStatus.Ended;
        }

        private bool MakeMove(string input)
        {
            var symbol = CurrentPlayer == Player.X ? 'X' : 'O';
            var isMoveSuccessful = Board.FillCoordinate(input, symbol);
            return isMoveSuccessful;
        }

        private bool CanTakeTurn(string input)
        {
            var hasUserQuit = Validator.HasUserQuit(input);
            var isInputValid = Board.IsValidCoordinate(input);

            return isInputValid && !hasUserQuit;
//            return !hasUserQuit;
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