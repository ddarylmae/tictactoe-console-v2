using System;

namespace TictactoeVer2
{
    public class ThreeInARowScoreCalculator : IScoreCalculator
    {
        private char[] Board { get; set; }
        public ThreeInARowScoreCalculator(char[] board)
        {
            Board = board;
        }
        
        public int GetPossiblePointsFromBoard(Player currentPlayer)
        {
            var symbol = currentPlayer == Player.X ? 'X' : 'O';
            return GetPointsOnVerticalLines(symbol) + 
                   GetPointsOnHorizontalLines(symbol) + 
                   GetPointsOnTopRightToBottomLeftDiagonal(symbol) + 
                   GetPointsFromTopLeftToBottomRightDiagonal(symbol);
        }
        
        private int GetPointsOnVerticalLines(char symbol)
        {
            var points = 0;
            
            for (int x = 0; x < GetSideLength() - 1; x++)
            {
                for (int y = x; y < Board.Length - (GetSideLength() * 2); y += GetSideLength())
                {
                    if (Board[y] == symbol && Board[y] == Board[y + GetSideLength()] &&
                        Board[y] == Board[y + GetSideLength() * 2])
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
                for (int y = x, ctr = 0; ctr < GetSideLength() - 2; ctr++, y++)
                {
                    if (Board[y] == symbol && Board[y] == Board[y + 1] && Board[y] == Board[y + 2])
                    {
                        points++;
                    }
                }
            }

            return points;
        }
        
        public int GetPointsOnTopRightToBottomLeftDiagonal(char symbol)
        {
            var points = 0;
            var side = GetSideLength();
            var increment = side - 1;
            
            // Loop for checking diagonals from column
            for (int columnIndex=2; columnIndex < side; columnIndex++)
            {
                for (int current=columnIndex; current + increment < side*columnIndex; current+=increment)
                {
                    points = AddPointsIfThreeInARow(symbol, current, increment, points);
                }
            }

            // Loop for checking diagonals from right edge
            for (int edgeIndex = side * 2 - 1; edgeIndex <= Board.Length - (side * 2 - 1); edgeIndex += side)
            {
                for (int currentIndex=edgeIndex; currentIndex + increment * 2 < Board.Length; currentIndex+=increment)
                {
                    points = AddPointsIfThreeInARow(symbol, currentIndex, increment, points);
                }
            }
                
            return points;
        }
        
        public int GetPointsFromTopLeftToBottomRightDiagonal(char symbol)
        {
            var points = 0;
            var side = GetSideLength();
            var increment = side + 1;
            
            // Loop for checking diagonals from column
            for (int columnIndex = side-3, x=2; columnIndex >=0; columnIndex--, x++)
            {
                for (int current=columnIndex; current + increment < x*increment+columnIndex; current+=increment)
                { 
                    points = AddPointsIfThreeInARow(symbol, current, increment, points);
                }
            }

            for (int edgeIndex=side; edgeIndex+side <= Board.Length-side*2; edgeIndex+=side)
            {
                for (int currentIndex=edgeIndex; currentIndex+increment*2 < Board.Length; currentIndex+=increment)
                {
                    points = AddPointsIfThreeInARow(symbol, currentIndex, increment, points);
                }
            }
                
            return points;
        }

        private int AddPointsIfThreeInARow(char symbol, int current, int indexIncrement, int points)
        {
            if (Board[current] == symbol && Board[current] == Board[current + indexIncrement] &&
                Board[current] == Board[current + (indexIncrement) * 2])
            {
                points++;
            }

            return points;
        }

        private int GetSideLength()
        {
            return (int) Math.Sqrt(Board.Length);
        }
    }
}