using System;

namespace TictactoeVer2
{
    public class MessageHandler
    {
        public void WelcomeToGame()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.WriteLine("Here's the current board: ");
        }

        public void DisplayTakeTurn(Player currentPlayer)
        {
            Console.WriteLine($"Player {currentPlayer.ToString()} please enter a coord x,y to place your move or 'q' to give up: ");
        }

        public void DisplayNoWinner()
        {
            Console.WriteLine("Game has ended. No winner.");
        }

        public void DisplayWinner(Player winner)
        {
            Console.WriteLine($"Game has ended. Player {winner.ToString()} has won!");
        }

        public void DisplayMoveInvalid()
        {
            Console.WriteLine("Move invalid. Try again.");
        }

        public void DisplayMoveAccepted()
        {
            Console.WriteLine("Move accepted.");
        }

        public void DisplayCoordinateIsFilled()
        {
            Console.WriteLine("Oh no, a piece is already at this place! Try again.");
        }

        public void DisplayUserHasQuit(Player currentPlayer)
        {
            Console.WriteLine($"Player {currentPlayer.ToString()} has quit.");
        }
    }
}