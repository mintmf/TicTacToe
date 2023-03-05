using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicTacToe.Common.Models;

namespace TicTacToe.Repository
{
    public class FileRepository : IFileRepository
    {
        private static readonly string path = "database.txt";

        public async Task SaveGameAsync(TicTacToeGame game)
        {
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                await outputFile.WriteAsync(JsonConvert.SerializeObject(game));
            }
        }

        public async Task<TicTacToeGame> GetGameAsync()
        {
            using (StreamReader inputFile = new StreamReader(path))
            {
                var gameJson = await inputFile.ReadToEndAsync();

                return JsonConvert.DeserializeObject<TicTacToeGame>(gameJson);
            }
        }
    }
}
