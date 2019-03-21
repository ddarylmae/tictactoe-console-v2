using System;
using System.Collections.Generic;
using System.Data;
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

        public void FillCoordinate(Move move)
        {
            var index = GetIndexFromInput(move);
            var symbol = (move.Player == Player.O) ? 'O' : 'X';
            Board[index] = symbol;
            CheckWinningMove(symbol);
            CheckBoardFilled();
            
            // TODO call CountPossiblePointsFromMove
//            CheckEntireBoard(move.Player);
        }

        public bool IsCoordinateFilled(Move move)
        {
            return Board[GetIndexFromInput(move)] != '.';
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

        public int CountPossiblePointsFromMove(Move move)
        {
            var points = 0;
            var row = move.Row;
            var col = move.Column;

            return points;
        }
        
        public int GetPossiblePointsFromBoard(Player player)
        {
            var totalPoints = 0;
            var symbol = (player == Player.X) ? 'X' : 'O';

            totalPoints = GetPointsOnHorizontalLines(symbol) +
                          GetPointsOnVerticalLines(symbol);

            return totalPoints;
        }

        private int GetPointsOnDiagonals(char symbol)
        {
            var points = 0;
            
            for (int x = 0 + 2; x < GetSideLength() - 1; x++)
            {
                for (int y = x; y < Board.Length - (GetSideLength() * 2); y += GetSideLength())
                {
                    if (Board[y] == symbol && Board[y] == Board[y + GetSideLength()] &&
                        Board[y] == Board[y + (GetSideLength() * 2)])
                    {
                        points++;
                    }
                }
            }

            return points;
        }

        private int GetPointsOnVerticalLines(char symbol)
        {
            var points = 0;
            
            for (int x = 0; x < GetSideLength() - 1; x++)
            {
                for (int y = x; y < Board.Length - (GetSideLength() * 2); y += GetSideLength())
                {
                    if (Board[y] == symbol && Board[y] == Board[y + GetSideLength()] &&
                        Board[y] == Board[y + (GetSideLength() * 2)])
                    {
                        points++;
                    }
                }
            }

            return points;
        }

        private int GetPointsOnHorizontalLines(char symbol)
        {
            var points = 0;
            
            for (int x = 0; x < Board.Length; x += GetSideLength())
            {
                for (int y = x, ctr = 0; ctr < GetSideLength() - 1; ctr++, y++)
                {
                    if (Board[y] == symbol && Board[y] == Board[y + 1] && Board[y] == Board[y + 2])
                    {
                        points++;
                    }
                }
            }

            return points;
        }

        public bool IsRowAndColumnValid(int pathRow, int pathCol)
        {
            return pathRow > 0 && pathRow <= GetSideLength() && pathCol > 0 && pathCol <= GetSideLength();
        }
    }
}