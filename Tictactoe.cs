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
        public PlayerModel Player1 { get; set; }
        public PlayerModel Player2 { get; set; }
        private UserInputValidator InputValidator { get; set; }

        public Tictactoe(IOutputWriter outputWriter)
        {
            Player1 = new PlayerModel();
            Player2 = new PlayerModel();
            MessageHandler = new MessageHandler(outputWriter);
            InputValidator = new UserInputValidator();
            
            CurrentPlayer = Player.X;

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
                if (InputValidator.IsBoardSizeValid(input))
                {
                    SetupGameBoard(int.Parse(input));
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

        private void LetPlayerMakeMove(string input)
        {
            if (UserHasQuit(input))
            {
                QuitGame();
            }
            else if (CanTakeTurn(input))
            {
                PerformTurn(input);
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
            return Board.IsValidCoordinate(input);
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
        }
        
        private bool UserHasQuit(string input)
        {
            return input == "q";
        }

        public void SetupGameBoard(int size)
        {
            Status = GameStatus.Playing;
            Board = new GameBoard(size);
            
            MessageHandler.DisplayStartTheGame();
            MessageHandler.DisplayBoard(Board.GetFormattedBoard());
            MessageHandler.DisplayTakeTurn(CurrentPlayer);
        }
    }
}