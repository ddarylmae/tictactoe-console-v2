using System;
using System.Linq;

namespace TictactoeVer2
{
    public class GameBoard : IGameBoard
    {
        private Player[] Board;

        public GameBoard(int size)
        {
            InitialiseBoard(size);

        }
        public bool IsWinningMove { get; set; }

        public void InitialiseBoard(int size)
        {
            var coordinateCount = size * size;
            Board = new Player[coordinateCount];
            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] = Player.None;
            }
        }

        public bool IsCoordinateFilled(Move move)
        {
            return Board[GetIndexFromInput(move)] != Player.None;
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
                    var current = Board[index];
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
            
            Board[index] = move.Player;

            return FillResult.Successful;
        }

        public bool IsFilled()
        {
            return Board.All(element => element != Player.None);
        }

        public Player GetPlayerAt(int row, int column)
        {
            var index = GetIndexFromInput(new Move{Row = row, Column = column});

            return Board[index];
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