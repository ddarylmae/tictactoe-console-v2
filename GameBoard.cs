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

        public bool FillCoordinate(Move move)
        {
            var index = GetIndexFromInput(move);
            if (!IsCoordinateFilled(index))
            {
                var symbol = (move.Player == Player.O) ? 'O' : 'X';
                Board[index] = symbol;
                CheckWinningMove(symbol);
                CheckBoardFilled();
                return true;    
            }

            return false;
        }

        public bool IsCoordinateFilled(int index)
        {
            return Board[index] != '.';
        }

        private void CheckBoardFilled()
        {
            IsBoardFilled = Board.All(element => element != '.');
        }
        
        private int GetIndexFromInput(Move move)
        {
            var index = (move.Row - 1) * GetSideLength() + (move.Column - 1);
            
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

        public bool IsValidCoordinate(Move move)
        {
            var index = GetIndexFromInput(move);
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

        public int GetSideLength()
        {
            return (int) Math.Sqrt(Board.Length);
        }
    }
}