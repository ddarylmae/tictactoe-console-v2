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

//            for (var x = row-1; x > 0 && x <= row+1; x++)
//            {
//                for (var y = col-1; y > 0 && y <= col+1; y++)
//                {
//                    var neighborIndex = GetIndexFromCoordinates(new Move {Row = x, Column = y});
//                    
//                    var coordinateNotEqualToLastMove = (x != move.Row || y != move.Column);
//                    
//                    if (coordinateNotEqualToLastMove && Board[neighborIndex] == 'X')
//                    {
//                        
//                    }
//                }
//            }
            
            

            return points;
        }

        public enum Path
        {
            UpperDiagonalLeft,
            UpperCenter,
            UpperDiagonalRight,
            InnerLeft,
            InnerRight,
            LowerDiagonalLeft,
            LowerCenter,
            LowerDiagonalRight,
        }
        
        public IEnumerable<KeyValuePair<Path, bool>> GetPossiblePaths(Move move)
        {
            // possible paths
            var adjacentPaths = new Dictionary<Path, bool>
            {
                {Path.UpperDiagonalLeft, false},
                {Path.UpperCenter, false},
                {Path.UpperDiagonalRight, false},
                {Path.InnerLeft, false},
                {Path.InnerRight, false},
                {Path.LowerDiagonalLeft, false},
                {Path.LowerCenter, false},
                {Path.LowerDiagonalRight, false},
            };

            var side = GetSideLength();
            var row = move.Row;
            var column = move.Column;
            var moveIndex = GetIndexFromInput(move);

            var pathRow = row - 2;
            var pathCol = column - 2;
            adjacentPaths[Path.UpperDiagonalLeft] = IsRowAndColumnValid(pathRow, pathCol);
            
            pathRow = row - 2;
            pathCol = column;
            adjacentPaths[Path.UpperCenter] = IsRowAndColumnValid(pathRow, pathCol);
            
            pathRow = row - 2;
            pathCol = column + 2;
            adjacentPaths[Path.UpperDiagonalRight] = IsRowAndColumnValid(pathRow, pathCol);
            
            pathRow = row;
            pathCol = column - 2;
            adjacentPaths[Path.InnerLeft] = IsRowAndColumnValid(pathRow, pathCol);
            
            pathRow = row;
            pathCol = column + 2;
            adjacentPaths[Path.InnerRight] = IsRowAndColumnValid(pathRow, pathCol);
            
            pathRow = row + 2;
            pathCol = column - 2;
            adjacentPaths[Path.LowerDiagonalLeft] = IsRowAndColumnValid(pathRow, pathCol);
            
            pathRow = row + 2;
            pathCol = column;
            adjacentPaths[Path.LowerCenter] = IsRowAndColumnValid(pathRow, pathCol);
            
            pathRow = row + 2;
            pathCol = column + 2;
            adjacentPaths[Path.LowerDiagonalRight] = IsRowAndColumnValid(pathRow, pathCol);

            // Get all "true" paths and check each if equal to 'X'
            var existingPaths = adjacentPaths.Select(i => i).Where(i => i.Value);
            
            foreach (var path in existingPaths)
            {
                Console.WriteLine(path.Key.ToString());
//                var pathString = "";
//                switch (path.Key)
//                {
//                    case Path.UpperDiagonalLeft:
//                        pathString = $"{Board[moveIndex]}{Board}";
//                        break;
//                }
            }

            
            return existingPaths;
        }

        public bool IsRowAndColumnValid(int pathRow, int pathCol)
        {
            return pathRow > 0 && pathRow <= GetSideLength() && pathCol > 0 && pathCol <= GetSideLength();
        }
    }
}