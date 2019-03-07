using System;
using System.Collections.Generic;
using System.Linq;

namespace TictactoeVer2
{
    public class GameBoard
    {
        private char[] Board;
        private readonly Dictionary<string, int> IndexMapper;

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
        public bool IsBoardFilled { get; set; }
        public bool IsWinningMove { get; set; }

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
            if (IsValidIndex(index) && IsCoordinateNotFilled(index))
            {
                Board[index] = symbol;
                CheckWinningMove(symbol);
                CheckBoardFilled();
                return true;    
            }

            return false;
        }

        private bool IsCoordinateNotFilled(int index)
        {
            return Board[index] == '.';
        }

        private static bool IsValidIndex(int index)
        {
            return index > -1;
        }

        private void CheckBoardFilled()
        {
            IsBoardFilled = Board.All(element => element != '.');
        }

        public int GetBoardSize()
        {
            return Board.Length;
        }

        public int GetIndexFromInput(string input)
        {
            var index = -1;
            try
            {
                index = IndexMapper[input];
            }
            catch(KeyNotFoundException e)
            {
                
            }
            return index;
        }

        public void CheckWinningMove(char symbol)
        {
            if (Board[0] == symbol && Board[1] == symbol && Board[2] == symbol ||
                Board[3] == symbol && Board[4]== symbol && Board[5] == symbol ||
                Board[6] == symbol && Board[7]== symbol && Board[8] == symbol ||
                Board[0] == symbol && Board[3]== symbol && Board[6] == symbol ||
                Board[1] == symbol && Board[4]== symbol && Board[7] == symbol ||
                Board[2] == symbol && Board[5]== symbol && Board[8] == symbol ||
                Board[0] == symbol && Board[4]== symbol && Board[8] == symbol ||
                Board[2] == symbol && Board[4]== symbol && Board[6] == symbol)
            {
                IsWinningMove = true;
            }
        }

        public void DisplayBoard()
        {
            for (int row = 0, index = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++, index++)
                {
                    Console.Write($"{Board[index]} ");
                }
                Console.Write("\n");
            }
        }

        public bool IsValidCoordinate(string input)
        {
            var index = -1;
            try
            {
                index = IndexMapper[input];
            }
            catch(KeyNotFoundException e)
            {
                
            }
            return index != -1;
        }
    }
}