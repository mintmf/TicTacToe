using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicTacToe.Common.Models;

namespace TicTacToe.Repository
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    public class FileRepository : IFileRepository
    {
        private static readonly string path = "Games/";

        /// <summary>
        /// Путь до файла с игрой
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        private string GetGamePath(string gameID)
        {
            return path + gameID + ".txt";
        }

        private void CreateDirectoryIfNotExists()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Сохранение игры
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public async Task SaveGameAsync(TicTacToeGame game)
        {
            CreateDirectoryIfNotExists();

            var gamePath = GetGamePath(game.GameID);

            using (StreamWriter outputFile = new StreamWriter(gamePath))
            {
                await outputFile.WriteAsync(JsonConvert.SerializeObject(game));
            }
        }

        /// <summary>
        /// Получние игры
        /// </summary>
        /// <param name="GameID"></param>
        /// <returns></returns>
        public async Task<TicTacToeGame> GetGameAsync(string GameID)
        {
            var gamePath = GetGamePath(GameID);

            if (!File.Exists(gamePath))
            {
                return null;
            }

            using (StreamReader inputFile = new StreamReader(gamePath))
            {
                var gameJson = await inputFile.ReadToEndAsync();

                return JsonConvert.DeserializeObject<TicTacToeGame>(gameJson);
            }
        }
    }
}
