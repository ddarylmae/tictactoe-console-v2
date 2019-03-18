using System;

namespace TictactoeVer2
{
    internal class GameRunner
    {
        public static Tictactoe Tictactoe;
        
        public static void Main(string[] args)
        {
            Tictactoe = new Tictactoe(new OutputWriter());
            
            while (Tictactoe.Status != GameStatus.Ended)
            {
                var input = Console.ReadLine();
                Tictactoe.InterpretInput(input);
            }
        }
    }
}