namespace TicTacToe.Common.Models
{
    /// <summary>
    /// Результат обработки хода
    /// </summary>
    public class MoveResult
    {
        /// <summary>
        /// Игра
        /// </summary>
        public TicTacToeGame Game { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
