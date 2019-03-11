using System;
using System.Collections.Generic;
using Moq;
using TictactoeVer2;
using Xunit;

namespace TictactoeVer2Tests
{
    public class TictactoeTests
    {
        private Tictactoe Game { get; set; }
        private Mock<IOutputWriter> _mockOutputWriter;
        
        public TictactoeTests()
        {
            _mockOutputWriter = new Mock<IOutputWriter>();
            
            Game = new Tictactoe(_mockOutputWriter.Object);
        }
        
        [Fact]
        public void ShouldStartWithPlayerX()
        {
            Assert.Equal(Player.X, Game.CurrentPlayer);
            _mockOutputWriter.Verify(output => output.Write("Here's the current board: "));
        }

        [Fact]
        public void ShouldInitialiseWithGamePlaying()
        {
            Assert.Equal(GameStatus.Playing, Game.Status);
        }

        [Fact]
        public void ShouldInitialiseWithNoWinner()
        {
            Assert.Equal(Player.None, Game.Winner);
        }

        [Fact]
        public void ShouldInitialiseWithEmptyBoard()
        {
            Assert.Equal(9, Game.Board.GetBoardSize());
        }

        [Fact]
        public void ShouldInitialiseBoardWithDots()
        {
            Assert.Equal('.', Game.Board.GetElementAt("1,1"));
            Assert.Equal('.', Game.Board.GetElementAt("1,2"));
            Assert.Equal('.', Game.Board.GetElementAt("1,3"));
            
            Assert.Equal('.', Game.Board.GetElementAt("2,1"));
            Assert.Equal('.', Game.Board.GetElementAt("2,2"));
            Assert.Equal('.', Game.Board.GetElementAt("2,3"));
            
            Assert.Equal('.', Game.Board.GetElementAt("3,1"));
            Assert.Equal('.', Game.Board.GetElementAt("3,2"));
            Assert.Equal('.', Game.Board.GetElementAt("3,3"));
        }

        [Fact]
        public void ShouldMarkCoordinateWithX()
        {
            Game.InterpretInput("1,1");
            
            Assert.Equal('X', Game.Board.GetElementAt("1,1"));
        }

        [Fact]
        public void ShouldMarkCoordinateWithO()
        {
            Game.InterpretInput("1,1");
            Game.InterpretInput("1,2");
            
            Assert.Equal('O', Game.Board.GetElementAt("1,2"));
        }
        
        [Fact]
        public void ShouldSwitchToPlayerOWhenCurrentPlayerIsX()
        {
            Game.InterpretInput("1,1");
            
            Assert.Equal(Player.O, Game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldSwitchToPlayerXWhenCurrentPlayerIsO()
        {
            Game.InterpretInput("1,1");
            Game.InterpretInput("1,2");
            
            Assert.Equal(Player.X, Game.CurrentPlayer);
        }

        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateFilled()
        {
            Game.InterpretInput("1,1");
            Game.InterpretInput("1,1");
            
            Assert.Equal(Player.O, Game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateInvalid()
        {
            Game.InterpretInput("1,4");
            
            Assert.Equal(Player.X, Game.CurrentPlayer);
        }
        
        [Theory]
        [InlineData("q", GameStatus.Ended)]
        [InlineData("1,1", GameStatus.Playing)]
        [InlineData("1,4", GameStatus.Playing)]
        [InlineData("aaaa", GameStatus.Playing)]
        public void ShouldSetCorrectGameStatus(string input, GameStatus expectedStatus)
        {
            Game.InterpretInput(input);
            
            Assert.Equal(expectedStatus, Game.Status);
        }

        [Fact]
        public void ShouldEndGameAndDeclarePlayerXAsWinner()
        {
            PlayerMakesMove(Player.X, "1,1", Game);
            PlayerMakesMove(Player.O,"1,3", Game);
            PlayerMakesMove(Player.X,"2,1", Game);
            PlayerMakesMove(Player.O,"3,3", Game);
            PlayerMakesMove(Player.X,"3,1", Game);
            
            Assert.Equal(GameStatus.Ended, Game.Status);
            Assert.Equal(Player.X, Game.Winner);
        }

        private static void PlayerMakesMove(Player player, string move, Tictactoe Game)
        {
            Assert.Equal(player, Game.CurrentPlayer);
            Game.InterpretInput(move);
        }


        [Fact]
        public void ShouldEndGameAndDeclarePlayerOAsWinner()
        {
            Game.InterpretInput("1,1");
            Game.InterpretInput("1,2");
            Game.InterpretInput("1,3");
            Game.InterpretInput("2,2");
            Game.InterpretInput("2,3");
            Game.InterpretInput("3,2");
            
            Assert.Equal(GameStatus.Ended, Game.Status);
            Assert.Equal(Player.O, Game.Winner);
        }
        
        [Fact]
        public void ShouldEndGameWhenBoardFilledAndNoWinner()
        {
            Game.InterpretInput("1,1");
            Game.InterpretInput("1,3");
            Game.InterpretInput("1,2");
            Game.InterpretInput("2,2");
            Game.InterpretInput("3,1");
            Game.InterpretInput("2,1");
            Game.InterpretInput("2,3");
            Game.InterpretInput("3,3");
            Game.InterpretInput("3,2");
            
            Assert.Equal(GameStatus.Draw, Game.Status);
        }
        
        [Fact]
        public void ShouldNotEndGameWhenBoardNotFilledAndNoWinner()
        {
            Game.InterpretInput("1,1");
            Game.InterpretInput("1,3");
            Game.InterpretInput("1,2");
            Game.InterpretInput("2,2");
            Game.InterpretInput("3,1");
            Game.InterpretInput("2,1");
            Game.InterpretInput("2,3");
            Game.InterpretInput("3,3");
            
            Assert.Equal(GameStatus.Playing, Game.Status);
        }
    }
}