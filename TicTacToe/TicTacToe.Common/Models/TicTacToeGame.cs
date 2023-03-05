namespace TicTacToe.Common.Models
{
    public class TicTacToeGame
    {
        public int[][] Board { get; set; }

        public bool IsFininshed { get; set; }

        private void InitializeBoard()
        {
            Board = new[]
            {
                new[] { 0, 0, 0 },
                new[] { 0, 0, 0 },
                new[] { 0, 0, 0 }
            };
        }

        public void UpdateBoard(Move move)
        {
            Board[move.CheckPointX][move.CheckPointY] = (int)move.PlayerType;
        }

        public TicTacToeGame()
        {
            InitializeBoard();
            IsFininshed = false;
        }
    }
}
