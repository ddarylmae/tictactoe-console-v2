using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace TictactoeVer2
{
    public class GameBoard
    {
        private char[] Board;

        public GameBoard(int size)
        {
            InitialiseBoard(size);
        }
        public bool IsBoardFilled { get; set; }
        public bool IsWinningMove { get; set; }

        public void InitialiseBoard(int size)
        {
            var coordinateCount = size * size;
            Board = new char[coordinateCount];
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
            if (IsCoordinateNotFilled(index))
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

        private void CheckBoardFilled()
        {
            IsBoardFilled = Board.All(element => element != '.');
        }
        
        public int GetIndexFromInput(string input)
        {
            var index = -1;
            
            var coordinates = input.Split(',');
            if (coordinates.Length == 2)
            {
                var row = int.Parse(coordinates[0]);
                var column = int.Parse(coordinates[1]);

                if (row > 0 && row <= GetSideLength() && column > 0 && column <= GetSideLength())
                {
                    index = (row - 1) * GetSideLength() + (column - 1);
                }
            }
            
            return index;
        }
        
        private bool IsElementANumberAndWithinRange(string element)
        {
            try
            {
                var numValue = Convert.ToInt32(element);
                return numValue > 0 && numValue < 4;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in converting");
                return false;
            }
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

        public bool IsValidCoordinate(string input)
        {
            var index = GetIndexFromInput(input);
            return index != -1;
        }

        public string GetFormattedBoard()
        {
            var result = "";
            for (int row = 0, index = 0; row < GetSideLength(); row++)
            {
                for (int column = 0; column < GetSideLength(); column++, index++)
                {
                    result += $"{Board[index]} ";
                }
                result += "\n";
            }

            return result;
        }

        private int GetSideLength()
        {
            return (int) Math.Sqrt(Board.Length);
        }
    }
}