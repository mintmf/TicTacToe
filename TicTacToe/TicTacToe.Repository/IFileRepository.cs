using System.Threading.Tasks;
using TicTacToe.Common.Models;

namespace TicTacToe.Repository
{
    public interface IFileRepository
    {
        Task SaveGameAsync(TicTacToeGame game);
        Task<TicTacToeGame> GetGameAsync();
    }
}
