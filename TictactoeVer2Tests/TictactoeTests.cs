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
            
            _mockOutputWriter.Verify(output => output.Write("Welcome to Tic Tac Toe!"));
            _mockOutputWriter.Verify(output => output.Write("Please input board size (ex. 3 for 3x3 board, 10 for 10x10): "));
        }
        
        [Fact]
        public void ShouldInitializeGameWithNoBoardDisplayed()
        {
            InitializeTictactoeGame();
            
            _mockOutputWriter.Verify(output => output.Write(". . . \n. . . \n. . . \n"), Times.Never);
        }
        
        [Fact]
        public void ShouldDisplayStartGameWith4X4BoardWhen4SizeInputted()
        {
            InitializeTictactoeGame();
            
            Game.InterpretInput("4");
            
            _mockOutputWriter.Verify(output => output.Write("Let's start the game!"));
            _mockOutputWriter.Verify(output => output.Write(". . . . \n. . . . \n. . . . \n. . . . \n"));
        }
        
        [Theory]
        [InlineData("aa")]
        [InlineData("2")]
        [InlineData("11")]
        public void ShouldReturnFalseWhenBoardSizeInvalid(string choice)
        {
            InitializeTictactoeGame();

            Assert.False(Game.InputValidator.IsBoardSizeValid(choice));
        }
        
        [Theory]
        [InlineData("3")]
        [InlineData("4")]
        [InlineData("9")]
        public void ShouldReturnTrueWhenBoardSizeValid(string choice)
        {
            InitializeTictactoeGame();

            Assert.True(Game.InputValidator.IsBoardSizeValid(choice));
        }

//        [Theory]
//        [InlineData(2)]
//        [InlineData(11)]
//        public void ShoulNotStartGameWhenBoardSizeInputInvalid(int choice) // invalid test at some point, start game with valid board size already
//        {
//            InitializeTictactoeGame();
//            Game.StartActualGame(choice);
//            
//            Assert.Equal(GameStatus.NotStarted, Game.Status);
//        }

        [Fact]
        public void ShouldDisplay3X3BoardWhenDefaultBoardSizeSelected() // basis for selecting default
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            _mockOutputWriter.Verify(output => output.Write(". . . \n. . . \n. . . \n"));
        }
        
        [Fact]
        public void ShouldInitialiseGameToNotStarted() // TODO Remove, implementation?
        {
            InitializeTictactoeGame();

            Assert.Equal(GameStatus.NotStarted, Game.Status);
        }

        [Fact]
        public void ShouldSetGameStatusToPlayingWhenGameIsStarted() // TODO Remove, implementation?
        {
            InitializeTictactoeGame();

            Assert.Equal(GameStatus.NotStarted, Game.Status);
            
            StartGameWith3X3Board();
            
            Assert.Equal(GameStatus.Playing, Game.Status);
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
        public void ShouldStartGameWithPlayer1AndPlayer2()
        {
            InitializeTictactoeGame();

            StartGameWith3X3Board();
            
            Assert.NotNull(Game.Player1);
            Assert.NotNull(Game.Player2);
        }
        
        [Fact]
        public void ShouldStartGameWithZeroInitialScores() // TODO change test to behavioural test
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            Assert.Equal(0, Game.Player1.Score);
            Assert.Equal(0, Game.Player2.Score);
        }
        
        [Fact]
        public void ShouldNotStartWithPlayerXWhenGameNotStarted()
        {
            InitializeTictactoeGame();

            _mockOutputWriter.Verify(output => output.Write("Player X please enter a coord x,y to place your move or 'q' to give up: "), Times.Never);
        }
        
        [Fact]
        public void ShouldStartWithPlayerXWhenGameStarts() // TODO modify to start game when a board size has been chosen
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            Assert.Equal(Player.X, Game.CurrentPlayer);
            _mockOutputWriter.Verify(output => output.Write("Player X please enter a coord x,y to place your move or 'q' to give up: "));
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
        public void ShouldNotChangeCurrentPlayerWhenCoordinateInvalid()
        {
            InitializeTictactoeGame();
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
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            Game.InterpretInput(input);
            
            Assert.Equal(expectedStatus, Game.Status);
        }

        [Fact]
        public void ShouldEndGameAndDeclarePlayerXAsWinner()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();

            PlayerMakesMove(Player.X, "1,1");
            PlayerMakesMove(Player.O,"1,3");
            PlayerMakesMove(Player.X,"2,1");
            PlayerMakesMove(Player.O,"3,3");
            PlayerMakesMove(Player.X,"3,1");
            
            Assert.Equal(GameStatus.Ended, Game.Status);
            Assert.Equal(Player.X, Game.Winner);
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
            
            Assert.Equal(GameStatus.Ended, Game.Status);
            Assert.Equal(Player.O, Game.Winner);
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
            
            Assert.Equal(GameStatus.Draw, Game.Status);
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
            
            Assert.Equal(GameStatus.Playing, Game.Status);
        }
    }
}