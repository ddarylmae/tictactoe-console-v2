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
            
            _mockOutputWriter.Verify(writer => writer.Write(". . . \n. . . \n. . . \n"));
        }
        
        [Fact]
        public void ShouldDisplayWelcomeMessageAndInputBoardSizeWhenGameStarts()
        {
            InitializeTictactoeGame();
            
            _mockOutputWriter.Verify(writer => writer.Write(It.IsAny<string>()), Times.Exactly(2));
            _mockOutputWriter.Verify(writer => writer.Write("Welcome to Tic Tac Toe!"));
            _mockOutputWriter.Verify(writer => writer.Write("Please input board size (ex. 3 for 3x3 board, 10 for 10x10): "));
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
            
            _mockOutputWriter.Verify(writer => writer.Write("Please enter a valid board size."));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void ShouldDisplay3X3BoardWhenDefaultBoardSizeSelected(string blankInput)
        {
            InitializeTictactoeGame();
            
            Game.InterpretInput(blankInput);
            
            _mockOutputWriter.Verify(writer => writer.Write(". . . \n. . . \n. . . \n"));
        }
        
        [Fact]
        public void ShouldDisplayStartTheGameWhenBoardSizeValid()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            _mockOutputWriter.Verify(writer => writer.Write("Let's start the game!"));
        }
        
//        [Fact]
//        public void ShouldReturnTwoPointsWhenTwo3InARowLineFound()
//        {
//            var board = new GameBoard(6);
//            var moveWithAnticipatedPoint = new Move {Row = 1, Column = 3, Player = Player.X};
//            
//            board.FillCoordinate(new Move{ Row = 1, Column = 1, Player = Player.X});
//            board.FillCoordinate(new Move{ Row = 1, Column = 2, Player = Player.X});
//            board.FillCoordinate(moveWithAnticipatedPoint);
//
//            var points = board.CountPossiblePointsFromMove(moveWithAnticipatedPoint);
//            
//            Assert.Equal(2, points);
//        }

        [Fact]
        public void ShouldReturnZeroWhenNoThreeInARowLineFound()
        {
            var board = new GameBoard(6);
            var move = new Move {Row = 2, Column = 3, Player = Player.X};
            
            board.FillCoordinate(move);

            var points = board.CountPossiblePointsFromMove(move);
            
            Assert.Equal(0, points);
        }

        [Fact]
        public void ShouldReturnOnePointWhenOneThreeInARowLineFound()
        {
            var board = new GameBoard(6);
            var moveWithAnticipatedPoint = new Move {Row = 4, Column = 4, Player = Player.X};
            
            board.FillCoordinate(new Move{ Row = 4, Column = 2, Player = Player.X});
            board.FillCoordinate(new Move{ Row = 4, Column = 3, Player = Player.X});
            board.FillCoordinate(moveWithAnticipatedPoint);

            var points = board.GetPossiblePointsFromBoard(Player.X);
            
            Assert.Equal(1, points);
        }
        
        [Fact]
        public void ShouldReturnOnePointWhenThreeInAColumnFound()
        {
            var board = new GameBoard(6);
            var moveWithAnticipatedPoint = new Move {Row = 3, Column = 1, Player = Player.X};
            
            board.FillCoordinate(new Move{ Row = 3, Column = 2, Player = Player.X});
            board.FillCoordinate(new Move{ Row = 3, Column = 3, Player = Player.X});
            board.FillCoordinate(moveWithAnticipatedPoint);

            var points = board.GetPossiblePointsFromBoard(Player.X);
            
            Assert.Equal(1, points);
        }

        [Fact]
        public void ShouldReturnTrueWhenRowValid()
        {
            var board = new GameBoard(8);

            var result = board.IsRowAndColumnValid(2, 9);
            
            Assert.False(result);
        }
        
        [Fact]
        public void ShouldDisplayUpdatedScoreWhenPlayerGainsPoint()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();
            
            Game.InterpretInput("1,1");
            Game.InterpretInput("2,1");
            Game.InterpretInput("1,2");
            Game.InterpretInput("2,2");
            Game.InterpretInput("1,3");
            
            _mockOutputWriter.Verify(writer => writer.Write("Current Scores:\nPlayer X - 1 \nPlayer Y - 0 \n"));
        }

        [Fact]
        public void ShouldDisplayInitialScoresWhenGameStarts()
        {
            InitializeTictactoeGame();
            
            StartGameWith3X3Board();
            
            _mockOutputWriter.Verify(writer => writer.Write("Current Scores:\nPlayer X - 0 \nPlayer Y - 0 \n"));
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
            
            _mockOutputWriter.Verify(writer => writer.Write("Player O please enter a coord x,y to place your move or 'q' to give up: "));
        }
        
        [Fact]
        public void ShouldSwitchToPlayerXWhenCurrentPlayerIsO()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();

            Game.InterpretInput("1,1");
            Game.InterpretInput("1,2");
            
            _mockOutputWriter.Verify(writer => writer.Write("Player X please enter a coord x,y to place your move or 'q' to give up: "));
        }

        [Fact]
        public void ShouldDisplayInvalidMoveWhenCoordinateFilled()
        {
            InitializeTictactoeGame();
            StartGameWith3X3Board();

            Game.InterpretInput("1,1");
            Game.InterpretInput("1,1");
            
            _mockOutputWriter.Verify(writer => writer.Write("Oh no, a piece is already at this place! Try again."));
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

        [Fact]
        public void ShouldUpdateBoardWhenMoveValid()
        {
            InitializeTictactoeGame();
            
            Game.InterpretInput("5");
            Game.InterpretInput("1,5");
            
            _mockOutputWriter.Verify(writer => writer.Write(". . . . X \n. . . . . \n. . . . . \n. . . . . \n. . . . . \n"));
        }
    }
}