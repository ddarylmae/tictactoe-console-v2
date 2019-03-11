using System;
using System.Collections.Generic;
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

            game.InterpretInput("1,1");
            
            Assert.Equal('X', game.Board.GetElementAt("1,1"));
        }

        [Fact]
        public void ShouldMarkCoordinateWithO()
        {
            var game = new Tictactoe();
            
            game.InterpretInput("1,1");
            game.InterpretInput("1,2");
            
            Assert.Equal('O', game.Board.GetElementAt("1,2"));
        }
        
        [Fact]
        public void ShouldSwitchToPlayerOWhenCurrentPlayerIsX()
        {
            var game = new Tictactoe();

            game.InterpretInput("1,1");
            
            Assert.Equal(Player.O, game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldSwitchToPlayerXWhenCurrentPlayerIsO()
        {
            var game = new Tictactoe();

            game.InterpretInput("1,1");
            game.InterpretInput("1,2");
            
            Assert.Equal(Player.X, game.CurrentPlayer);
        }

        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateFilled()
        {
            var game = new Tictactoe();
            
            game.InterpretInput("1,1");
            game.InterpretInput("1,1");
            
            Assert.Equal(Player.O, game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateInvalid()
        {
            var game = new Tictactoe();
            
            game.InterpretInput("1,4");
            
            Assert.Equal(Player.X, game.CurrentPlayer);
        }
        
        [Theory]
        [InlineData("q", GameStatus.Ended)]
        [InlineData("1,1", GameStatus.Playing)]
        [InlineData("1,4", GameStatus.Playing)]
        [InlineData("aaaa", GameStatus.Playing)]
        public void ShouldSetCorrectGameStatus(string input, GameStatus expectedStatus)
        {
            var game = new Tictactoe();

            game.InterpretInput(input);
            
            Assert.Equal(expectedStatus, game.Status);
        }

        [Fact]
        public void ShouldEndGameAndDeclarePlayerXAsWinner()
        {
            var game = new Tictactoe();
            
            PlayerMakesMove(Player.X, "1,1", game);
            PlayerMakesMove(Player.O,"1,3", game);
            PlayerMakesMove(Player.X,"2,1", game);
            PlayerMakesMove(Player.O,"3,3", game);
            PlayerMakesMove(Player.X,"3,1", game);
            
            Assert.Equal(GameStatus.Ended, game.Status);
            Assert.Equal(Player.X, game.Winner);
        }

        private static void PlayerMakesMove(Player player, string move, Tictactoe game)
        {
            Assert.Equal(player, game.CurrentPlayer);
            game.InterpretInput(move);
        }


        [Fact]
        public void ShouldEndGameAndDeclarePlayerOAsWinner()
        {
            var game = new Tictactoe();
            
            game.InterpretInput("1,1");
            game.InterpretInput("1,2");
            game.InterpretInput("1,3");
            game.InterpretInput("2,2");
            game.InterpretInput("2,3");
            game.InterpretInput("3,2");
            
            Assert.Equal(GameStatus.Ended, game.Status);
            Assert.Equal(Player.O, game.Winner);
        }
        
        [Fact]
        public void ShouldEndGameWhenBoardFilledAndNoWinner()
        {
            var game = new Tictactoe();
            
            game.InterpretInput("1,1");
            game.InterpretInput("1,3");
            game.InterpretInput("1,2");
            game.InterpretInput("2,2");
            game.InterpretInput("3,1");
            game.InterpretInput("2,1");
            game.InterpretInput("2,3");
            game.InterpretInput("3,3");
            game.InterpretInput("3,2");
            
            Assert.Equal(GameStatus.Draw, game.Status);
        }
        
        [Fact]
        public void ShouldNotEndGameWhenBoardNotFilledAndNoWinner()
        {
            var game = new Tictactoe();
            
            game.InterpretInput("1,1");
            game.InterpretInput("1,3");
            game.InterpretInput("1,2");
            game.InterpretInput("2,2");
            game.InterpretInput("3,1");
            game.InterpretInput("2,1");
            game.InterpretInput("2,3");
            game.InterpretInput("3,3");
            
            Assert.Equal(GameStatus.Playing, game.Status);
        }
    }
}