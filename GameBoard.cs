using System;
using System.Linq;

namespace TictactoeVer2
{
    public class GameBoard : IGameBoard
    {
        private char[] Board;

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

        public bool IsCoordinateFilled(Move move)
        {
            return Board[GetIndexFromInput(move)] != '.';
        }
        
        private int GetIndexFromInput(Move move)
        {
            var index = (move.Row - 1) * GetSideLength() + (move.Column - 1);
            
            return index;
        }

        public string GetFormatted()
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

        public FillResult FillCoordinate(Move move)
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
    }
}