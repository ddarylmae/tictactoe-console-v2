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
            
//            Assert.Equal(Player.X);
        }
    }
}