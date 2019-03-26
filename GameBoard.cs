using System;
using System.Linq;

namespace TictactoeVer2
{
    public class GameBoard : IGameBoard
    {
        private char[] Board;
//        private IScoreCalculator ScoreCalculator { get; set; }

        public GameBoard(int size)
        {
            InitialiseBoard(size);

        }
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
        }

        public bool IsCoordinateFilled(Move move)
        {
            return Board[GetIndexFromInput(move)] != '.';
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

        public FillResult FillSpecCoordinate(Move move)
        {
            var index = GetIndexFromInput(move);
            if (Board[index] != '.')
            {
                return FillResult.Taken;
            }
            
            var symbol = (move.Player == Player.O) ? 'O' : 'X';
            Board[index] = symbol;

            return FillResult.Successful;
        }

        public bool IsFilled()
        {
            return Board.All(element => element != '.');
        }

        public Player GetPlayerAt(int row, int column)
        {
            var index = GetIndexFromInput(new Move{Row = row, Column = column});
            if (Board[index] == 'X')
            {
                return Player.X;
            }
            if (Board[index] == 'O')
            {
                return Player.O;
            }

            return Player.None;
        }

        int IGameBoard.GetSideLength()
        {
            return GetSideLength();
        }

        private int GetSideLength()
        {
            return (int) Math.Sqrt(Board.Length);
        }
        
//        public int GetPossiblePointsFromBoard(Player player)
//        {
//            var totalPoints = 0;
////            var symbol = (player == Player.X) ? 'X' : 'O';
//
////            totalPoints = ScoreCalculator.GetPossiblePointsFromBoard(Board, player);
//
//            return totalPoints;
//        }
    }
}