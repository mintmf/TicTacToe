namespace TicTacToe.Common.Models
{
    /// <summary>
    /// Ход игрока
    /// </summary>
    public class Move
    {
        /// <summary>
        /// ID Игры
        /// </summary>
        public string GameID { get; set; }

        /// <summary>
        /// Тип игрока (крестик или нолик)
        /// </summary>
        public PlayerType PlayerType { get; set; }

        /// <summary>
        /// X-координата
        /// </summary>
        public int CheckPointX { get; set; }


        /// <summary>
        /// Y-координата
        /// </summary>
        public int CheckPointY { get; set; }
    }
}
