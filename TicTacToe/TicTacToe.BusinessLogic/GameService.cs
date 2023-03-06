using System.Threading.Tasks;
using TicTacToe.Common.Models;
using TicTacToe.Repository;

namespace TicTacToe.BusinessLogic
{
    public class GameService : IGameService
    {
        private IFileRepository FileRepository { get; set; }

        public GameService(IFileRepository fileRepository)
        {
            FileRepository = fileRepository;
        }

        public async Task<TicTacToeGame> CreateNewGameAsync()
        {
            var game = new TicTacToeGame();

            await FileRepository.SaveGameAsync(game);

            return game;
        }

        public async Task<MoveResult> MakeMoveAsync(Move move)
        {
            var game = await FileRepository.GetGameAsync();

            var moveResult = new MoveResult
            {
                ErrorMessage = game.GetErrorMessage(move)
            };

            if (!string.IsNullOrEmpty(moveResult.ErrorMessage)) 
            {
                return moveResult;
            }

            game.ProcessMove(move);

            await FileRepository.SaveGameAsync(game);

            moveResult.Game = game;

            return moveResult;
        }
    }
}
