using System.Threading.Tasks;
using TicTacToe.Common.Models;

namespace TicTacToe.Repository
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Сохранение игры
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        Task SaveGameAsync(TicTacToeGame game);

        /// <summary>
        /// Получение игры
        /// </summary>
        /// <param name="GameID"></param>
        /// <returns></returns>
        Task<TicTacToeGame> GetGameAsync(string GameID);
    }
}