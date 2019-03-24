using System;
using System.Linq;

namespace TictactoeVer2
{
    public class GameBoard
    {
        private char[] Board;
        private IScoreCalculator ScoreCalculator { get; set; }

        public GameBoard(int size)
        {
            InitialiseBoard(size);
            
            ScoreCalculator = new ThreeInARowScoreCalculator(Board);
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
        
        public int GetPossiblePointsFromBoard(Player player)
        {
            var totalPoints = 0;
//            var symbol = (player == Player.X) ? 'X' : 'O';

            totalPoints = ScoreCalculator.GetPossiblePointsFromBoard(player);

            return totalPoints;
        }
    }
}