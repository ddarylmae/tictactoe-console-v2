using System.Collections.Generic;

namespace TictactoeVer2
{
    public class Tictactoe
    {
        public Player CurrentPlayer { get; set; }
        public GameStatus Status { get; set; }
        public Player Winner { get; set; }
        public GameBoard Board { get; set; }
        private MessageHandler MessageHandler { get; set; }

        public Tictactoe(IOutputWriter outputWriter)
        {
            CurrentPlayer = Player.X;
            Board = new GameBoard();
            MessageHandler = new MessageHandler(outputWriter);

            InitializeGame();
        }

        private void InitializeGame()
        {
            MessageHandler.WelcomeToGame();

            MessageHandler.DisplayInputBoardSize();
            MessageHandler.DisplayTakeTurn(CurrentPlayer);
        }

        public void InterpretInput(string input)
        {
            if (CanTakeTurn(input))
            {
                PerformTurn(input);
            } 
            else if(HasUserQuit(input))
            {
                QuitGame();
            }
            else
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
            var hasUserQuit = HasUserQuit(input);
            var isInputValid = Board.IsValidCoordinate(input);

            return isInputValid && !hasUserQuit;
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
        }
        
        private bool HasUserQuit(string input)
        {
            return input == "q";
        }

        public void StartActualGame(int boardSize)
        {
            Status = GameStatus.Playing;
            MessageHandler.DisplayBoard();
        }
    }
}