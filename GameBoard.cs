using System;
using System.Linq;

namespace TictactoeVer2
{
    public class GameBoard : IGameBoard
    {
        private Player[] _board;

        public GameBoard(int size)
        {
            InitialiseBoard(size);
        }

        private void InitialiseBoard(int size)
        {
            var coordinateCount = size * size;
            _board = new Player[coordinateCount];
            for (int i = 0; i < _board.Length; i++)
            {
                _board[i] = Player.None;
            }
        }

        private bool IsCoordinateFilled(Move move)
        {
            return _board[GetIndexFromInput(move)] != Player.None;
        }
        
        private int GetIndexFromInput(Move move)
        {
            var index = (move.Row - 1) * GetSideLength() + (move.Column - 1);
            
            return index;
        }

        public string GetFormatted()
        {
            var formattedBoard = "";
            for (int row = 0, index = 0; row < GetSideLength(); row++)
            {
                for (int column = 0; column < GetSideLength(); column++, index++)
                {
                    var current = _board[index];
                    if (current == Player.None)
                    {
                        formattedBoard += ". ";
                    }
                    else
                    {
                        formattedBoard += $"{current} ";   
                    }
                }
                formattedBoard += "\n";
            }

            return formattedBoard;
        }

        public FillResult FillCoordinate(Move move)
        {
            var index = GetIndexFromInput(move);
            if (IsCoordinateFilled(move))
            {
                return FillResult.Taken;
            }
            
            _board[index] = move.Player;

            return FillResult.Successful;
        }

        public bool IsFilled()
        {
            return _board.All(element => element != Player.None);
        }

        public Player GetPlayerAt(int row, int column)
        {
            var index = GetIndexFromInput(new Move{Row = row, Column = column});

            return _board[index];
        }

        public int GetSideLength()
        {
            return (int) Math.Sqrt(_board.Length);
        }
    }
}