using System.Threading.Tasks;
using TicTacToe.Common.Models;

namespace TicTacToe.BusinessLogic
{
    /// <summary>
    /// Сервис игры
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Создание игры
        /// </summary>
        /// <returns></returns>
        Task<TicTacToeGame> CreateNewGameAsync();

        /// <summary>
        /// Получение игры
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        Task<TicTacToeGame> GetGameAsync(string gameID);

        /// <summary>
        /// Обработка хода
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        Task<MoveResult> MakeMoveAsync(Move move);
    }
}
