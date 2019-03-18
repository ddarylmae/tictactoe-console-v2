using System.Collections.Generic;

namespace TictactoeVer2
{
    public class Tictactoe
    {
        public Player CurrentPlayer { get; set; }
        public GameStatus Status { get; set; }
        private Player Winner { get; set; }
        private GameBoard Board { get; set; }
        private MessageHandler MessageHandler { get; set; }
        private UserInputHandler InputHandler { get; set; }

        public Tictactoe(IOutputWriter outputWriter)
        {
            MessageHandler = new MessageHandler(outputWriter);
            InputHandler = new UserInputHandler();

            InitializeGame();
        }

        private void InitializeGame()
        {
            MessageHandler.WelcomeToGame();
            MessageHandler.DisplayInputBoardSize();
        }

        public void InterpretInput(string input)
        {
            if (Status == GameStatus.NotStarted)
            {
                if (InputHandler.TryParseBoardSize(input, out int size))
                {
                    SetupNewGame(size);
                }
                else
                {
                    MessageHandler.DisplayEnterValidBoardSize();
                }
            }
            else
            {
                LetPlayerMakeMove(input);
            }
        }

        private void SetupNewGame(int boardSize)
        {
            SetupGameBoard(boardSize);
            SetupGameState(boardSize);
        }

        private void SetupGameState(int boardSize)
        {
            Status = GameStatus.Playing;
            InputHandler.CurrentBoardSize = boardSize;
        }

        private void LetPlayerMakeMove(string input)
        {
            if (UserHasQuit(input))
            {
                QuitGame();
            }
            else if (InputHandler.TryParseMove(input, out Move move))
            {
                PerformTurn(move);
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

        private void PerformTurn(Move move)
        {
            move.Player = CurrentPlayer;
            var isMoveSuccessful = Board.FillCoordinate(move);;

            if (isMoveSuccessful)
            {
                MessageHandler.DisplayMoveAccepted();
                MessageHandler.DisplayBoard(Board.GetFormattedBoard());

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
            Status = GameStatus.Ended;
        }

        private void DeclareWinner()
        {
            Winner = CurrentPlayer;
            MessageHandler.DisplayWinner(Winner);
            Status = GameStatus.Ended;
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
        }
        
        private bool UserHasQuit(string input)
        {
            return input == "q";
        }

        private void SetupGameBoard(int size)
        {
            Board = new GameBoard(size);
            
            MessageHandler.DisplayStartTheGame();
            MessageHandler.DisplayBoard(Board.GetFormattedBoard());
            MessageHandler.DisplayTakeTurn(CurrentPlayer);
        }
    }
}