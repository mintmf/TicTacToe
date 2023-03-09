using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicTacToe.BusinessLogic;
using TicTacToe.Common.Models;

namespace TicTacToe.Controllers
{
    /// <summary>
    /// Контроллер игры
    /// </summary>
    [ApiController]
    [Route("game")]
    public class GameController : Controller
    {
        private IGameService GameService { get; set; }

        /// <summary>
        /// Конструктор контроллера игры
        /// </summary>
        /// <param name="gameService"></param>
        public GameController(IGameService gameService)
        {
            GameService = gameService;
        }

        /// <summary>
        /// Получение игры
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGameAsync(string gameID)
        {
            var game = await GameService.GetGameAsync(gameID);

            if (game == null)
            {
                return BadRequest();
            }

            return Ok(game);
        }

        /// <summary>
        /// Обработка хода игрока
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("move")]
        public async Task<IActionResult> MakeMoveAsync(Move move)
        {
            var moveResult = await GameService.MakeMoveAsync(move);

            if (!string.IsNullOrEmpty(moveResult.ErrorMessage))
            {
                return BadRequest(moveResult);
            }

            return Ok(moveResult);
        }

        /// <summary>
        /// Создание новой игры
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> CreateNewGame()
        {
            var game = await GameService.CreateNewGameAsync();

            return Ok(game);
        }
    }
}
