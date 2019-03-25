using System;

namespace TictactoeVer2
{
    public class ThreeInARowScoreCalculator : IScoreCalculator
    {
        public int GetPossiblePointsFromBoard(char[] board, Player currentPlayer)
        {
            var symbol = GetSymbolFromPlayer(currentPlayer);
            return GetTotalPoints(board, symbol);
        }

        private char GetSymbolFromPlayer(Player currentPlayer)
        {
            return currentPlayer == Player.X ? 'X' : 'O';
        }

        private int GetTotalPoints(char[] board, char symbol)
        {
            return GetPointsOnVerticalLines(board, symbol) + 
                   GetPointsOnHorizontalLines(board, symbol) + 
                   GetPointsOnTopRightToBottomLeftDiagonal(board, symbol) + 
                   GetPointsFromTopLeftToBottomRightDiagonal(board, symbol);
        }

        private int GetPointsOnVerticalLines(char[] board, char symbol)
        {
            var points = 0;
            
            for (int x = 0; x < GetSideLength(board) - 1; x++)
            {
                for (int y = x; y < board.Length - (GetSideLength(board) * 2); y += GetSideLength(board))
                {
                    if (board[y] == symbol && board[y] == board[y + GetSideLength(board)] &&
                        board[y] == board[y + GetSideLength(board) * 2])
                    {
                        points++;
                    }
                }
            }

            return points;
        }

        private int GetPointsOnHorizontalLines(char[] board, char symbol)
        {
            var points = 0;
            
            for (int x = 0; x < board.Length; x += GetSideLength(board))
            {
                for (int y = x, ctr = 0; ctr < GetSideLength(board) - 2; ctr++, y++)
                {
                    if (board[y] == symbol && board[y] == board[y + 1] && board[y] == board[y + 2])
                    {
                        points++;
                    }
                }
            }

            return points;
        }
        
        public int GetPointsOnTopRightToBottomLeftDiagonal(char[] board, char symbol)
        {
            var points = 0;
            var side = GetSideLength(board);
            var increment = side - 1;
            
            // Loop for checking diagonals from column
            for (int columnIndex=2; columnIndex < side; columnIndex++)
            {
                for (int current=columnIndex; current + increment < side*columnIndex; current+=increment)
                {
                    points = AddPointsIfThreeInARow(board, symbol, current, increment, points);
                }
            }

            // Loop for checking diagonals from right edge
            for (int edgeIndex = side * 2 - 1; edgeIndex <= board.Length - (side * 2 - 1); edgeIndex += side)
            {
                for (int currentIndex=edgeIndex; currentIndex + increment * 2 < board.Length; currentIndex+=increment)
                {
                    points = AddPointsIfThreeInARow(board, symbol, currentIndex, increment, points);
                }
            }
                
            return points;
        }
        
        public int GetPointsFromTopLeftToBottomRightDiagonal(char[] board, char symbol)
        {
            var points = 0;
            var side = GetSideLength(board);
            var increment = side + 1;
            
            // Loop for checking diagonals from column
            for (int columnIndex = side-3, x=2; columnIndex >=0; columnIndex--, x++)
            {
                for (int current=columnIndex; current + increment < x*increment+columnIndex; current+=increment)
                { 
                    points = AddPointsIfThreeInARow(board, symbol, current, increment, points);
                }
            }

            for (int edgeIndex=side; edgeIndex+side <= board.Length-side*2; edgeIndex+=side)
            {
                for (int currentIndex=edgeIndex; currentIndex+increment*2 < board.Length; currentIndex+=increment)
                {
                    points = AddPointsIfThreeInARow(board, symbol, currentIndex, increment, points);
                }
            }
                
            return points;
        }

        private int AddPointsIfThreeInARow(char[] board, char symbol, int current, int indexIncrement, int points)
        {
            if (board[current] == symbol && board[current] == board[current + indexIncrement] &&
                board[current] == board[current + (indexIncrement) * 2])
            {
                points++;
            }

            return points;
        }

        private int GetSideLength(char[] board)
        {
            return (int) Math.Sqrt(board.Length);
        }
    }
}