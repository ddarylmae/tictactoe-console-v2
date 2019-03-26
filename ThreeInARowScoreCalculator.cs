using System;

namespace TictactoeVer2
{
    public class ThreeInARowScoreCalculator : IScoreCalculator
    {
        public int CalculatePoints(IGameBoard board, Player currentPlayer)
        {
            return GetTotalPointsFromLines(board, currentPlayer);
        }

        private int GetTotalPointsFromLines(IGameBoard board, Player player)
        {
            return GetPointsOnVerticalLines(board, player) + 
                   GetPointsOnHorizontalLines(board, player) + 
                   GetPointsOnTopRightToBottomLeftDiagonal(board, player) + 
                   GetPointsFromTopLeftToBottomRightDiagonal(board, player);
        }

        public int GetPointsOnVerticalLines(IGameBoard board, Player currentPlayer)
        {
            var points = 0;

            for (int column = 1; column <= board.GetSideLength(); column++)
            {
                for (int row = 1; row < board.GetSideLength() - 1; row++)
                {
                    var currentCoordinate = board.GetPlayerAt(row, column);
                    var below = board.GetPlayerAt(row + 1, column);
                    var nextToBelow = board.GetPlayerAt(row + 2, column);

                    if (currentCoordinate == currentPlayer && currentCoordinate == below && currentCoordinate == nextToBelow)
                    {
                        points++;
                    }
                }
            }

            return points;
        }

        public int GetPointsOnHorizontalLines(IGameBoard board, Player player)
        {
            var points = 0;
            
            for (int row = 1; row <= board.GetSideLength(); row++)
            {
                for (int column = 1; column < board.GetSideLength() - 1; column++)
                {
                    var currentCoordinate = board.GetPlayerAt(row, column);
                    var below = board.GetPlayerAt(row, column + 1);
                    var nextToBelow = board.GetPlayerAt(row, column + 2);

                    points = AddPointsIfThreeInARow(player, currentCoordinate, below, nextToBelow, points);
                }
            }

            return points;
        }
        
        public int GetPointsOnTopRightToBottomLeftDiagonal(IGameBoard board, Player player)
        {
            var points = 0;

            // Checking diagonals from column
            for (int column = 3; column <= board.GetSideLength(); column++)
            {
                for (int row = 1, curCol = column; curCol > 2; row++, curCol--)
                {
                    var currentCoord = board.GetPlayerAt(row, curCol);
                    var bottomLeftBelow = board.GetPlayerAt(row + 1, curCol - 1);
                    var bottomLeftBelowBelow = board.GetPlayerAt(row + 2, curCol - 2);

                    points = AddPointsIfThreeInARow(player, currentCoord, bottomLeftBelow, bottomLeftBelowBelow, points);
                }
            }
            
            // Checking diagonals from right edge
            var edgeColumn = board.GetSideLength();
            for (int row = 2; row < board.GetSideLength() - 1; row++)
            {
                for (int column = edgeColumn, curRow = row; curRow < board.GetSideLength() - 1; column--, curRow++)
                {
                    var currentCoord = board.GetPlayerAt(curRow, column);
                    var bottomLeftBelow = board.GetPlayerAt(curRow + 1, column - 1);
                    var bottomLeftBelowBelow = board.GetPlayerAt(curRow + 2, column - 2);
                    
                    points = AddPointsIfThreeInARow(player, currentCoord, bottomLeftBelow, bottomLeftBelowBelow, points);
                }
                
            }
                
            return points;
        }

        private int AddPointsIfThreeInARow(Player player, Player currentCoord, Player bottomLeftBelow,
            Player bottomLeftBelowBelow, int points)
        {
            if (currentCoord == player &&
                currentCoord == bottomLeftBelow &&
                currentCoord == bottomLeftBelowBelow)
            {
                points++;
            }

            return points;
        }

        public int GetPointsFromTopLeftToBottomRightDiagonal(IGameBoard board, Player player)
        {
            var points = 0;

            // Checking diagonals from columns
            for (int column = 1; column < board.GetSideLength() - 1; column++)
            {
                for (int row = 1, curCol = column; curCol < board.GetSideLength() - 1; row++, curCol++)
                {
                    var current = board.GetPlayerAt(row, curCol);
                    var bottomRight = board.GetPlayerAt(row + 1, curCol + 1);
                    var bottomRightNext = board.GetPlayerAt(row + 2, curCol + 2);
                    
                    points = AddPointsIfThreeInARow(player, current, bottomRight, bottomRightNext, points);
                }
            }
            
            // Checking diagonals from left edge
            var columnEdge = 1;
            for (int row = 2; row < board.GetSideLength() - 1; row++)
            {
                for (int curRow = row, col = columnEdge; curRow < board.GetSideLength() - 1; col++, curRow++)
                {
                    var current = board.GetPlayerAt(curRow, col);
                    var bottomRight = board.GetPlayerAt(curRow + 1, col + 1);
                    var bottomRightRight = board.GetPlayerAt(curRow + 2, col + 2);
                    
                    points = AddPointsIfThreeInARow(player, current, bottomRight, bottomRightRight, points);
                }
            }
                
            return points;
        }
    }
}