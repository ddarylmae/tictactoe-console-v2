using System.Collections.Generic;

namespace TictactoeVer2
{
    public class GameBoard
    {
        private char[] Board;
        private Dictionary<string, int> IndexMapper;

        public GameBoard()
        {
            InitialiseBoard();
            IndexMapper = new Dictionary<string, int>
            {
                {"1,1", 0},
                {"1,2", 1},
                {"1,3", 2},
                {"2,1", 3},
                {"2,2", 4},
                {"2,3", 5},
                {"3,1", 6},
                {"3,2", 7},
                {"3,3", 8}
            };
        }

        public void InitialiseBoard()
        {
            Board = new char[9];
            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] = '.';
            }
        }

        public char GetElementAt(string input)
        {
            return Board[GetIndexFromInput(input)];
        }

        public bool FillCoordinate(string input, char symbol)
        {
            var index = GetIndexFromInput(input);
            if (Board[index] == '.')
            {
                Board[index] = symbol;
                return true;    
            }

            return false;
        }

        public int GetBoardSize()
        {
            return Board.Length;
        }

        public int GetIndexFromInput(string input)
        {
            return IndexMapper[input];
        }
    }
}