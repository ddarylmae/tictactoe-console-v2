using System;

namespace TictactoeVer2
{
    internal class Program
    {
        public static Tictactoe Tictactoe;
        
        public static void Main(string[] args)
        {
            Tictactoe = new Tictactoe();
            
            while (Tictactoe.Status == GameStatus.Playing)
            {
                var input = Console.ReadLine();
                Tictactoe.MakeMove(input);
            }
        }
    }
}