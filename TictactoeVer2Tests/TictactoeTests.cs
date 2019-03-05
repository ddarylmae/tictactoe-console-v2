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

        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateFilled()
        {
            var game = new Tictactoe();
            
            game.MakeMove("1,1");
            game.MakeMove("1,1");
            
            Assert.Equal(Player.O, game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateOutOfBounds()
        {
            var game = new Tictactoe();
            
            game.MakeMove("1,4");
            
            Assert.Equal(Player.X, game.CurrentPlayer);
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