using System;

namespace TictactoeVer2
{
    internal class GameRunner
    {
        public static Tictactoe Tictactoe;
        
        public static void Main(string[] args)
        {
            Tictactoe = new Tictactoe(new OutputWriter());
            
            Tictactoe.InitializeGame();
            Tictactoe.StartActualGame(10);
            
            while (Tictactoe.Status == GameStatus.Playing)
            {
                var input = Console.ReadLine();
                Tictactoe.InterpretInput(input);
            }
        }
    }
}