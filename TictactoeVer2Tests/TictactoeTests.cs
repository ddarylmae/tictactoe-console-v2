using System;
using TictactoeVer2;
using Xunit;

namespace TictactoeVer2Tests
{
    public class TictactoeTests
    {
        [Fact]
        public void ShouldStartWithPlayerX()
        {
            var game = new Tictactoe();   
            
            Assert.Equal(Player.X, game.CurrentPlayer);
        }

        [Fact]
        public void ShouldInitialiseWithGamePlaying()
        {
            var game = new Tictactoe();
            
            Assert.Equal(GameStatus.Playing, game.Status);
        }

        [Fact]
        public void ShouldInitialiseWithNoWinner()
        {
            var game = new Tictactoe();
            
            Assert.Equal(Player.None, game.Winner);
        }

        [Fact]
        public void ShouldInitialiseWithEmptyBoard()
        {
            var game = new Tictactoe();
            
            Assert.Equal(9, game.Board.GetBoardSize());
        }

        [Fact]
        public void ShouldInitialiseBoardWithDots()
        {
            var game = new Tictactoe();
            
//            Assert.All(game.Board, x => Assert.Equal('.', x));

            Assert.Equal('.', game.Board.GetElementAt("1,1"));
            Assert.Equal('.', game.Board.GetElementAt("1,2"));
            Assert.Equal('.', game.Board.GetElementAt("1,3"));
            
            Assert.Equal('.', game.Board.GetElementAt("2,1"));
            Assert.Equal('.', game.Board.GetElementAt("2,2"));
            Assert.Equal('.', game.Board.GetElementAt("2,3"));
            
            Assert.Equal('.', game.Board.GetElementAt("3,1"));
            Assert.Equal('.', game.Board.GetElementAt("3,2"));
            Assert.Equal('.', game.Board.GetElementAt("3,3"));
        }

        // TODO REMOVE
        [Theory]
        [InlineData("1,1", 0)]
        [InlineData("1,2", 1)]
        [InlineData("1,3", 2)]
        [InlineData("2,1", 3)]
        [InlineData("2,2", 4)]
        [InlineData("2,3", 5)]
        [InlineData("3,1", 6)]
        [InlineData("3,2", 7)]
        [InlineData("3,3", 8)]
        public void ShouldMapInputToArrayIndex(string input, int expectedIndex)
        {
            var board = new GameBoard();

            var actualIndex = board.GetIndexFromInput(input);
            
            Assert.Equal(expectedIndex, actualIndex);
        }
        
        // TODO REMOVE
        [Fact]
        public void ShouldUpdateBoard()
        {
            var board = new GameBoard();
            
            board.FillCoordinate("1,1", 'X');
            
            var actualSymbol = board.GetElementAt("1,1");
            
            Assert.Equal('X', actualSymbol);
        }

        [Fact]
        public void ShouldMarkCoordinateWithX()
        {
            var game = new Tictactoe();

            game.MakeMove("1,1");
            
            Assert.Equal('X', game.Board.GetElementAt("1,1"));
        }

        [Fact]
        public void ShouldMarkCoordinateWithO()
        {
            var game = new Tictactoe();
            
            game.MakeMove("1,1");
            game.MakeMove("1,2");
            
            Assert.Equal('O', game.Board.GetElementAt("1,2"));
        }
        
        // TODO REMOVE
        [Theory]
        [InlineData(Player.X, Player.O)]
        [InlineData(Player.O, Player.X)]
        public void ShouldSwitchPlayer(Player current, Player expected)
        {
            var game = new Tictactoe();
            game.CurrentPlayer = current;

            game.SwitchPlayer();
            
            Assert.Equal(expected, game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldSwitchToPlayerOWhenCurrentPlayerIsX()
        {
            var game = new Tictactoe();

            game.MakeMove("1,1");
            
            Assert.Equal(Player.O, game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldSwitchToPlayerXWhenCurrentPlayerIsO()
        {
            var game = new Tictactoe();

            game.MakeMove("1,1");
            game.MakeMove("1,2");
            
            Assert.Equal(Player.X, game.CurrentPlayer);
        }
        
        // TODO REMOVE
        [Fact]
        public void ShouldReturnTrueWhenCoordinateNotFilled()
        {
            var board = new GameBoard();
            
            var isFilledSuccessfully = board.FillCoordinate("1,1", 'X');
            
            Assert.True(isFilledSuccessfully);
        }

        // TODO REMOVE
        [Fact]
        public void ShouldReturnFalseWhenCoordinateIsFilled()
        {
            var board = new GameBoard();
            board.FillCoordinate("1,1", 'X');
            
            var isFilledSuccessfully = board.FillCoordinate("1,1", 'O');
            
            Assert.False(isFilledSuccessfully);
        }

        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenMoveInvalid()
        {
            var game = new Tictactoe();
            
            game.MakeMove("1,1");
            game.MakeMove("1,1");
            
            Assert.Equal(Player.O, game.CurrentPlayer);
        }

        // TODO REMOVE
        [Fact]
        public void ShouldReturnTrueWhenPlayerHasQuit()
        {
            var validator = new UserInputValidator();

            Assert.True(validator.HasUserQuit("q"));
        }
        
        // TODO REMOVE
        [Fact]
        public void ShouldReturnFalseWhenPlayerHasNotQuit()
        {
            var validator = new UserInputValidator();

            Assert.False(validator.HasUserQuit("1,1"));
        }
        
        [Theory]
        [InlineData("q", GameStatus.Ended)]
        [InlineData("1,1", GameStatus.Playing)]
        [InlineData("1,4", GameStatus.Playing)]
        [InlineData("4,1", GameStatus.Playing)]
        [InlineData("a,1", GameStatus.Playing)]
        [InlineData("aaaa", GameStatus.Playing)]
        public void ShouldSetCorrectGameStatus(string input, GameStatus expectedStatus)
        {
            var game = new Tictactoe();

            game.MakeMove(input);
            
            Assert.Equal(expectedStatus, game.Status);
        }

        // TODO REMOVE
        [Fact]
        public void ShouldReturnTrueIfWinningMove()
        {
            var board = new GameBoard();

            board.FillCoordinate("1,1", 'X');
            board.FillCoordinate("1,2", 'X');
            board.FillCoordinate("1,3", 'X');

            var isWinningMove = board.IsWinningMove('X');
            
            Assert.True(isWinningMove);
        }
        
        // TODO REMOVE
        [Fact]
        public void ShouldReturnFalseIfNotWinningMove()
        {
            var board = new GameBoard();
            board.FillCoordinate("1,1", 'X');
            board.FillCoordinate("1,2", 'X');

            var isWinningMove = board.IsWinningMove('X');
            
            Assert.False(isWinningMove);
        }

        [Fact]
        public void ShouldDeclarePlayerXAsWinner()
        {
            var game = new Tictactoe();
            
            game.MakeMove("1,1");
            game.MakeMove("1,3");
            game.MakeMove("2,1");
            game.MakeMove("3,3");
            game.MakeMove("3,1");
            
            Assert.Equal(Player.X, game.Winner);
        }
        
        [Fact]
        public void ShouldDeclarePlayerOAsWinner()
        {
            var game = new Tictactoe();
            
            game.MakeMove("1,1");
            game.MakeMove("1,2");
            game.MakeMove("1,3");
            game.MakeMove("2,2");
            game.MakeMove("2,3");
            game.MakeMove("3,2");
            
            Assert.Equal(Player.O, game.Winner);
        }
        
        
        [Fact]
        public void ShouldEndGameWhenBoardFilledAndNoWinner()
        {
            var game = new Tictactoe();
            
            game.MakeMove("1,1");
            game.MakeMove("1,3");
            game.MakeMove("1,2");
            game.MakeMove("2,2");
            game.MakeMove("3,1");
            game.MakeMove("2,1");
            game.MakeMove("2,3");
            game.MakeMove("3,3");
            game.MakeMove("3,2");
            
            Assert.Equal(GameStatus.Draw, game.Status);
        }
        
        [Fact]
        public void ShouldNotEndGameWhenBoardNotFilledAndNoWinner()
        {
            var game = new Tictactoe();
            
            game.MakeMove("1,1");
            game.MakeMove("1,3");
            game.MakeMove("1,2");
            game.MakeMove("2,2");
            game.MakeMove("3,1");
            game.MakeMove("2,1");
            game.MakeMove("2,3");
            game.MakeMove("3,3");
            
            Assert.Equal(GameStatus.Playing, game.Status);
        }
    }
}