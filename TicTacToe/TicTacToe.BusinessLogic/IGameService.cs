using System.Threading.Tasks;
using TicTacToe.Common.Models;

namespace TicTacToe.BusinessLogic
{
    public interface IGameService
    {
        Task<TicTacToeGame> CreateNewGameAsync();
        Task<TicTacToeGame> GetGameAsync(string gameID);
        Task<MoveResult> MakeMoveAsync(Move move);
    }
}
