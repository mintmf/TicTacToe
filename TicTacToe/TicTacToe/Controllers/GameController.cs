using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicTacToe.BusinessLogic;
using TicTacToe.Common.Models;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController : Controller
    {
        private IGameService GameService { get; set; }

        public GameController(IGameService gameService)
        {
            GameService = gameService;
        }

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

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> CreateNewGame()
        {
            var game = await GameService.CreateNewGameAsync();

            return Ok(game);
        }
    }
}
