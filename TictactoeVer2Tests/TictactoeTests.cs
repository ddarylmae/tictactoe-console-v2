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
        
        private void StartGameWith3X3Board()
        {
            Game.StartActualGame(3);
        }
        
        [Fact]
        public void ShouldInitialiseGameToNotStarted()
        {
            Assert.Equal(GameStatus.NotStarted, Game.Status);
        }

        [Fact]
        public void ShouldSetGameStatusToPlayingWhenGameIsStarted()
        {
            Assert.Equal(GameStatus.NotStarted, Game.Status);
            
            StartGameWith3X3Board();
            
            Assert.Equal(GameStatus.Playing, Game.Status);
        }

        [Fact]
        public void ShouldReturn16BoardSizeWhen4X4BoardSizeSelected() // TODO REMOVE implementation test
        {
            Game.StartActualGame(4);
            
            Assert.Equal(16, Game.Board.GetBoardSize());
        }

        [Fact]
        public void ShouldReturnFormatted3X3BoardWhenBoardSize3X3()
        {
            StartGameWith3X3Board();
            
            Assert.Equal(". . . \n. . . \n. . . \n", Game.Board.GetFormattedBoard());
        }
        
        [Fact]
        public void ShouldReturnFormatted4X4BoardWhenBoardSize4X4()
        {
            Game.StartActualGame(4);
            Assert.Equal(". . . . \n. . . . \n. . . . \n. . . . \n", Game.Board.GetFormattedBoard());
        }

        [Fact]
        public void ShouldDisplay4X4BoardWhen4X4BoardSizeSelected()
        {
            Game.StartActualGame(4);
            
            _mockOutputWriter.Verify(output => output.Write(". . . . \n. . . . \n. . . . \n. . . . \n"));
        }
        
        [Fact]
        public void ShouldInitializeBoardToNullWhenGameNotStarted() // TODO REMOVE, implementation test
        {
            Assert.Equal(GameStatus.NotStarted, Game.Status);
            
            Assert.Null(Game.Board);
        }

        [Fact]
        public void ShouldInitializeGameWithNoBoardDisplayed()
        {
            _mockOutputWriter.Verify(output => output.Write(". . . \n. . . \n. . . \n. . . \n"), Times.Never);
        }

        [Fact]
        public void ShouldDisplay3X3BoardWhenDefaultBoardSizeSelected() // TODO how to select default?
        {
            StartGameWith3X3Board();
            
            _mockOutputWriter.Verify(output => output.Write(". . . \n. . . \n. . . \n"));
        }
        
        [Fact]
        public void ShouldSetGameStatusToPlayingWhenGameHasStarted()
        {
            StartGameWith3X3Board();
            
            Assert.Equal(GameStatus.Playing, Game.Status);
        }

        [Fact]
        public void ShouldInitialiseWithNoWinner()
        {
            Assert.Equal(Player.None, Game.Winner);
        }
        
        [Fact]
        public void ShouldStartWithPlayerX()
        {
            Assert.Equal(Player.X, Game.CurrentPlayer);
            _mockOutputWriter.Verify(output => output.Write("Player X please enter a coord x,y to place your move or 'q' to give up: "));
        }
        
        [Fact]
        public void ShouldSwitchToPlayerOWhenCurrentPlayerIsX()
        {
            StartGameWith3X3Board();

            Game.InterpretInput("1,1");
            
            Assert.Equal(Player.O, Game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldSwitchToPlayerXWhenCurrentPlayerIsO()
        {
            StartGameWith3X3Board();

            Game.InterpretInput("1,1");
            Game.InterpretInput("1,2");
            
            Assert.Equal(Player.X, Game.CurrentPlayer);
        }

        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateFilled()
        {
            StartGameWith3X3Board();

            Game.InterpretInput("1,1");
            Game.InterpretInput("1,1");
            
            Assert.Equal(Player.O, Game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateInvalid()
        {
            StartGameWith3X3Board();

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
            StartGameWith3X3Board();
            
            Game.InterpretInput(input);
            
            Assert.Equal(expectedStatus, Game.Status);
        }

        [Fact]
        public void ShouldEndGameAndDeclarePlayerXAsWinner()
        {
            StartGameWith3X3Board();

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
            StartGameWith3X3Board();
            
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
            StartGameWith3X3Board();
            
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
            StartGameWith3X3Board();
            
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
        
        [Fact]
        public void ShouldDisplayWelcomeMessageAndInputBoardSizeWhenGameStarts()
        {
            _mockOutputWriter.Verify(output => output.Write("Welcome to Tic Tac Toe!"));
            _mockOutputWriter.Verify(output => output.Write("Please input board size (ex. 3 for 3x3 board, 10 for 10x10): "));
        }
    }
}