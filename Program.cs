using System;

namespace TictactoeVer2
{
    internal class Program
    {
        public static Tictactoe Tictactoe;
        
        public static void Main(string[] args)
        {
            Tictactoe = new Tictactoe();
            
            WelcomeToGame();
        }
        
        public static void WelcomeToGame()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.WriteLine("Here's the current board: ");
            Tictactoe.GetBoard();
        }
    }
}