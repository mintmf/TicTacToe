using Xunit;
using TicTacToe.BusinessLogic;
using NSubstitute;
using TicTacToe.Repository;
using TicTacToe.Common.Models;

namespace TicTacToe.UnitTests
{
    public class GameServiceTests
    {
        private IFileRepository mockRepository { get; set; }

        GameService gameService { get; set; }

        public GameServiceTests()
        {
            mockRepository = Substitute.For<IFileRepository>();

            gameService = new GameService(mockRepository);
        }

        [Fact]
        public async void TestMakeMoveAsync_MoveIsNull()
        {
            // Arrange
            Move move = null;

            // Act
            var moveResult = await gameService.MakeMoveAsync(move);

            // Assert
            Assert.Equal("Move is null", moveResult.ErrorMessage);
        }

        [Fact]
        public async void TestMakeMoveAsync_GameIsNull()
        {
            // Arrange
            Move move = new Move() { GameID = "1" };

            TicTacToeGame game = null;

            mockRepository
                .GetGameAsync(Arg.Any<string>())
                .Returns<TicTacToeGame>(game);

            // Act
            var moveResult = await gameService.MakeMoveAsync(move);

            // Assert
            Assert.Equal("Game not found", moveResult.ErrorMessage);
        }

        [Fact]
        public async void TestMakeMoveAsync_InvalidGameID()
        {
            // Arrange
            Move move = new Move() { GameID = "1" };

            TicTacToeGame game = new TicTacToeGame { GameID = "0" };

            mockRepository
                .GetGameAsync(Arg.Any<string>())
                .Returns<TicTacToeGame>(game);

            // Act
            var moveResult = await gameService.MakeMoveAsync(move);
            
            // Assert
            Assert.Equal("Invalid GameID", moveResult.ErrorMessage);
        }

        [Fact]
        public async void TestMakeMoveAsync_GameIsFinished()
        {
            // Arrange
            Move move = new Move { GameID = "0" };

            TicTacToeGame game = new TicTacToeGame { IsFininshed = true , GameID = "0"};

            mockRepository
                .GetGameAsync(Arg.Any<string>())
                .Returns<TicTacToeGame>(game);

            // Act
            var moveResult = await gameService.MakeMoveAsync(move);

            // Assert
            Assert.Equal("The game is already finished", moveResult.ErrorMessage);
        }

        [Fact]
        public async void TestMakeMoveAsync_WrongPlayer()
        {
            // Arrange
            Move move = new Move { PlayerType = PlayerType.X, GameID = "0"};

            TicTacToeGame game = new TicTacToeGame { ActivePlayer = PlayerType.O , GameID = "0"};

            mockRepository
                .GetGameAsync(Arg.Any<string>())
                .Returns<TicTacToeGame>(game);

            // Act
            var moveResult = await gameService.MakeMoveAsync(move);

            // Assert
            Assert.Equal("Wrong player", moveResult.ErrorMessage);
        }

        [Fact]
        public async void TestMakeMoveAsync_InvalidPosition()
        {
            // Arrange
            Move move = new Move { PlayerType = PlayerType.O, GameID = "0", CheckPointX = 4, CheckPointY = 0 };

            TicTacToeGame game = new TicTacToeGame { ActivePlayer = PlayerType.O, GameID = "0" };

            mockRepository
                .GetGameAsync(Arg.Any<string>())
                .Returns<TicTacToeGame>(game);

            // Act
            var moveResult = await gameService.MakeMoveAsync(move);

            // Assert
            Assert.Equal("Invalid position", moveResult.ErrorMessage);
        }

        [Fact]
        public async void TestMakeMoveAsync_PosisitionIsTaken()
        {
            // Arrange
            Move move = new Move { PlayerType = PlayerType.X, GameID = "0" , CheckPointX = 0, CheckPointY = 0};

            var board = new[]
            {
                new PlayerType?[] { PlayerType.O, null, null },
                new PlayerType?[] { null, null, null },
                new PlayerType?[] { null, null, null }
            };
            TicTacToeGame game = new TicTacToeGame { ActivePlayer = PlayerType.X, GameID = "0" , Board = board};

            mockRepository
                .GetGameAsync(Arg.Any<string>())
                .Returns<TicTacToeGame>(game);

            // Act
            var moveResult = await gameService.MakeMoveAsync(move);

            // Assert
            Assert.Equal("Position is taken", moveResult.ErrorMessage);
        }
    }
}
