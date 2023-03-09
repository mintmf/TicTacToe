using System.Threading.Tasks;
using TicTacToe.Common.Models;
using TicTacToe.Repository;

namespace TicTacToe.BusinessLogic
{
    /// <summary>
    /// Сервис игры
    /// </summary>
    public class GameService : IGameService
    {
        /// <summary>
        /// Репозиторий
        /// </summary>
        private IFileRepository FileRepository { get; set; }

        /// <summary>
        /// Конструктор сервиса игры
        /// </summary>
        /// <param name="fileRepository"></param>
        public GameService(IFileRepository fileRepository)
        {
            FileRepository = fileRepository;
        }

        /// <summary>
        /// Создание игры
        /// </summary>
        /// <returns></returns>
        public async Task<TicTacToeGame> CreateNewGameAsync()
        {
            var game = new TicTacToeGame();

            await FileRepository.SaveGameAsync(game);

            return game;
        }

        /// <summary>
        /// Получение игры
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public async Task<TicTacToeGame> GetGameAsync(string gameID)
        {
            var game = await FileRepository.GetGameAsync(gameID);

            return game;
        }

        /// <summary>
        /// Обработка хода
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public async Task<MoveResult> MakeMoveAsync(Move move)
        {
            if (move == null)
            {
                return new MoveResult
                {
                    Game = null,
                    ErrorMessage = "Move is null"
                };
            }

            var game = await FileRepository.GetGameAsync(move.GameID);

            if (game == null)
            {
                return new MoveResult
                {
                    Game = null,
                    ErrorMessage = "Game not found"
                };
            }

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
