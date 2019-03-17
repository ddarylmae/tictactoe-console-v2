using System.Configuration;
using System.Runtime.InteropServices;
using Moq;
using TictactoeVer2;
using Xunit;

namespace TictactoeVer2Tests
{
    public class TictactoeTests
    {
        private Tictactoe Game { get; set; }
        private Mock<IOutputWriter> _mockOutputWriter;
        
        public void InitializeTictactoeGame()
        {
            _mockOutputWriter = new Mock<IOutputWriter>();
            
            Game = new Tictactoe(_mockOutputWriter.Object);
        }
        
        private void StartGameWith3X3Board()
        {
            InitializeTictactoeGame();
            
            Game.InterpretInput("3");
            
            Assert.Equal(GameStatus.Playing, Game.Status);
        }
        
        [Fact]
        public void ShouldDisplayWelcomeMessageAndInputBoardSizeWhenGameStarts()
        {
            InitializeTictactoeGame();
            
            _mockOutputWriter.Verify(writer => writer.Write("Welcome to Tic Tac Toe!"));
            _mockOutputWriter.Verify(writer => writer.Write("Please input board size (ex. 3 for 3x3 board, 10 for 10x10): "));
        }
        
        [Fact]
        public void ShouldInitializeGameWithNoBoardDisplayed()
        {
            InitializeTictactoeGame();
            
            _mockOutputWriter.Verify(writer => writer.Write(". . . \n. . . \n. . . \n"), Times.Never);
        }
        
        [Fact]
        public void ShouldDisplay4X4BoardWhen4SizeInputted()
        {
            InitializeTictactoeGame();
            
            Game.InterpretInput("4");
            
            _mockOutputWriter.Verify(writer => writer.Write(". . . . \n. . . . \n. . . . \n. . . . \n"));
        }

        [Theory]
        [InlineData("2")]
        [InlineData("aa")]
        [InlineData("11")]
        public void ShouldDisplayErrorMessageWhenBoardSizeInvalid(string choice) 
        {
            InitializeTictactoeGame();
            
            Game.InterpretInput(choice);
            
            Assert.Equal(GameStatus.NotStarted, Game.Status);
            _mockOutputWriter.Verify(writer => writer.Write("Please enter a valid board size."));
        }

        [Fact]
        public void ShouldDisplay3X3BoardWhenDefaultBoardSizeSelected() // basis for selecting default
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            _mockOutputWriter.Verify(writer => writer.Write(". . . \n. . . \n. . . \n"));
        }
        
        [Fact]
        public void ShouldDisplayStartTheGameWhenBoardSizeValid()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            _mockOutputWriter.Verify(writer => writer.Write("Let's start the game!"));
        }
        
        [Fact]
        public void ShouldSetGameStatusToPlayingWhenGameHasStarted()
        {
            InitializeTictactoeGame();

            StartGameWith3X3Board();
            
            Assert.Equal(GameStatus.Playing, Game.Status);
        }

        [Fact]
        public void ShouldInitialiseWithNoWinner()
        {
            InitializeTictactoeGame();

            Assert.Equal(Player.None, Game.Winner);
        }

        [Fact]
        public void ShouldDisplayInitialScoresWhenGameStarts()
        {
            InitializeTictactoeGame();
            
            StartGameWith3X3Board();
            
            _mockOutputWriter.Verify(writer => writer.Write("Current Scores: Player X - 0\nPlayer O - 0\n"));
        }
        
        [Fact]
        public void ShouldNotStartWithPlayerXWhenGameNotStarted()
        {
            InitializeTictactoeGame();

            _mockOutputWriter.Verify(writer => writer.Write("Player X please enter a coord x,y to place your move or 'q' to give up: "), Times.Never);
        }
        
        [Fact]
        public void ShouldStartWithPlayerXWhenGameStarts()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            _mockOutputWriter.Verify(writer => writer.Write("Player X please enter a coord x,y to place your move or 'q' to give up: "));
        }
        
        [Fact]
        public void ShouldSwitchToPlayerOWhenCurrentPlayerIsX()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            Game.InterpretInput("1,1");
            
            Assert.Equal(Player.O, Game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldSwitchToPlayerXWhenCurrentPlayerIsO()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();

            Game.InterpretInput("1,1");
            Game.InterpretInput("1,2");
            
            Assert.Equal(Player.X, Game.CurrentPlayer);
        }

        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateFilled()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();

            Game.InterpretInput("1,1");
            Game.InterpretInput("1,1");
            
            Assert.Equal(Player.O, Game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldNotChangeCurrentPlayerWhenCoordinateInvalid() // todo remove?
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();

            Game.InterpretInput("1,4");
            
            Assert.Equal(Player.X, Game.CurrentPlayer);
        }
        
        [Fact]
        public void ShouldDisplayInvalidMoveWhenCoordinateInvalid()
        {
            InitializeTictactoeGame();
            
            Game.InterpretInput("5");
            Game.InterpretInput("1,6");
            
            _mockOutputWriter.Verify(writer => writer.Write("Move invalid. Try again."));
        }
        
        [Theory]
        [InlineData("q", "Player X has quit.")]
        [InlineData("1,1", "Move accepted.")]
        [InlineData("1,4", "Move invalid. Try again.")]
        [InlineData("aaaa", "Move invalid. Try again.")]
        public void ShouldDisplayCorrectMessageBasedOnInput(string input, string expectedMessage)
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            Game.InterpretInput(input);
            
            _mockOutputWriter.Verify(writer => writer.Write(expectedMessage));
        }

        [Fact]
        public void ShouldEndGameAndDeclarePlayerXAsWinnerWhenThreeInARow()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();

            PlayerMakesMove(Player.X, "1,1");
            PlayerMakesMove(Player.O,"1,3");
            PlayerMakesMove(Player.X,"2,1");
            PlayerMakesMove(Player.O,"3,3");
            PlayerMakesMove(Player.X,"3,1");
            
            _mockOutputWriter.Verify(writer => writer.Write("Game has ended. Player X has won!"));
        }

        private void PlayerMakesMove(Player player, string move)
        {
            Assert.Equal(player, Game.CurrentPlayer);
            
            Game.InterpretInput(move);
        }

        [Fact]
        public void ShouldEndGameAndDeclarePlayerOAsWinner()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            Game.InterpretInput("1,1");
            Game.InterpretInput("1,2");
            Game.InterpretInput("1,3");
            Game.InterpretInput("2,2");
            Game.InterpretInput("2,3");
            Game.InterpretInput("3,2");
            
            _mockOutputWriter.Verify(writer => writer.Write("Game has ended. Player O has won!"));
        }
        
        [Fact]
        public void ShouldEndGameWhenBoardFilledAndNoWinner()
        {
            InitializeTictactoeGame();
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
            
            _mockOutputWriter.Verify(writer => writer.Write("Game has ended. No winner."));
        }
        
        [Fact]
        public void ShouldNotEndGameWhenBoardNotFilledAndNoWinner()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            Game.InterpretInput("1,1");
            Game.InterpretInput("1,3");
            Game.InterpretInput("1,2");
            Game.InterpretInput("2,2");
            Game.InterpretInput("3,1");
            Game.InterpretInput("2,1");
            Game.InterpretInput("2,3");
            Game.InterpretInput("3,3");
            
            _mockOutputWriter.Verify(writer => writer.Write("Player X please enter a coord x,y to place your move or 'q' to give up: "));
        }
    }
}