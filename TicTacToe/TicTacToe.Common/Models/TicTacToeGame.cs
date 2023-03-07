using System;

namespace TicTacToe.Common.Models
{
    public class TicTacToeGame
    {
        public PlayerType?[][] Board { get; set; }

        public bool IsFininshed { get; set; }

        public PlayerType? Winner { get; set; }

        public PlayerType ActivePlayer { get; set; }

        public TicTacToeGame()
        {
            InitializeBoard();
            IsFininshed = false;
            ActivePlayer = PlayerType.X;
            Winner = null;
        }

        private void InitializeBoard()
        {
            Board = new[]
            {
                new PlayerType?[] { null, null, null },
                new PlayerType?[] { null, null, null },
                new PlayerType?[] { null, null, null }
            };
        }

        private void EndGame(PlayerType winner)
        {
            IsFininshed = true;
            Winner = winner;
        }

        private void CheckRows(PlayerType type)
        {
            for (int i = 0; i < Board[0].Length; i++)
            {
                if (Board[i][0] == type && Board[i][1] == type && Board[i][2] == type)
                {
                    EndGame(type);

                    return;
                }
            }
        }

        private void CheckColumns(PlayerType type)
        {
            for (int j = 0; j < Board[0].Length; j++)
            {
                if (Board[0][j] == type && Board[1][j] == type && Board[2][j] == type)
                {
                    EndGame(type);

                    return;
                }
            }
        }

        private void CheckDiagonals(PlayerType type)
        {
            if (Board[0][0] == type && Board[1][1] == type && Board[2][2] == type)
            {
                EndGame(type);

                return;
            }
            if (Board[2][0] == type && Board[1][1] == type && Board[0][2] == type)
            {
                EndGame(type);

                return;
            }
        }
        private void CheckIfFinished(PlayerType type)
        {
            CheckRows(type);
            CheckColumns(type);
            CheckDiagonals(type);
        }

        public string GetErrorMessage(Move move)
        {
            if (move == null)
            {
                return "Move is null";
            }
            if (IsFininshed == true)
            {
                return "The game is already finished";
            }
            if (move.PlayerType != ActivePlayer)
            {
                return "Wrong player";
            }
            if (move.CheckPointX > Board[0].Length || move.CheckPointY > Board[0].Length)
            {
                return "Invalid position";
            }
            if (Board[move.CheckPointX][move.CheckPointY] != null)
            {
                return "Position is taken";
            }

            return null;
        }

        public bool IsMoveValid(Move move)
        {
            return string.IsNullOrEmpty(GetErrorMessage(move));
        }

        private void UpdateBoard(Move move)
        {
            Board[move.CheckPointX][move.CheckPointY] = move.PlayerType;
        }

        private void UpdateActivePlayer()
        {
            ActivePlayer = ActivePlayer == PlayerType.X ? PlayerType.O : PlayerType.X;
        }

        public void ProcessMove(Move move)
        {
            if (IsMoveValid(move))
            {
                UpdateBoard(move);
                UpdateActivePlayer();
                CheckIfFinished(move.PlayerType);
            }
        }
    }
}
