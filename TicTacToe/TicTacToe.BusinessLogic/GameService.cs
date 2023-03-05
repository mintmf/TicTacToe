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

        public async Task<TicTacToeGame> MakeMoveAsync(Move move)
        {
            var game = await FileRepository.GetGameAsync();

            game.UpdateBoard(move);

            await FileRepository.SaveGameAsync(game);

            return game;
        }
    }
}
