using System;

namespace TictactoeVer2
{
    public class MessageHandler
    {
        public IOutputWriter OutputWriter { get; set; }

        public MessageHandler(IOutputWriter outputWriter)
        {
            OutputWriter = outputWriter;
        }
        
        private void OutputMessage(string message) => OutputWriter.Write(message);
        
        public void WelcomeToGame()
        {
            OutputMessage("Welcome to Tic Tac Toe!");
            OutputMessage("Here's the current board: ");
            
            DisplayBoard();
        }

        public void DisplayTakeTurn(Player currentPlayer)
        {
            OutputMessage($"Player {currentPlayer} please enter a coord x,y to place your move or 'q' to give up: ");
        }

        public void DisplayNoWinner()
        {
            OutputMessage("Game has ended. No winner.");
        }

        public void DisplayWinner(Player winner)
        {
            OutputMessage($"Game has ended. Player {winner.ToString()} has won!");
        }

        public void DisplayMoveInvalid()
        {
            OutputMessage("Move invalid. Try again.");
        }

        public void DisplayMoveAccepted()
        {
            OutputMessage("Move accepted.");
        }

        public void DisplayCoordinateIsFilled()
        {
            OutputMessage("Oh no, a piece is already at this place! Try again.");
        }

        public void DisplayUserHasQuit(Player currentPlayer)
        {
            OutputMessage($"Player {currentPlayer.ToString()} has quit.");
        }

        public void DisplayBoard()
        {
            OutputMessage(". . .\n. . .\n. . .");
        }

        public void DisplayInputBoardSize()
        {
            OutputMessage("Please input board size (ex. 3 for 3x3 board, 10 for 10x10): ");
        }
    }
}