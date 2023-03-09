using System;

namespace TicTacToe.Common.Models
{
    /// <summary>
    /// Игра
    /// </summary>
    public class TicTacToeGame
    {
        /// <summary>
        /// Поле игры.
        /// Задается массивом из массивов из 3-х элементов.
        /// </summary>
        public PlayerType?[][] Board { get; set; }

        /// <summary>
        /// Флаг окончания игры
        /// </summary>
        public bool IsFininshed { get; set; }

        /// <summary>
        /// Победитель
        /// </summary>
        public PlayerType? Winner { get; set; }

        /// <summary>
        /// Игрок, который должен делать текущий ход
        /// </summary>
        public PlayerType ActivePlayer { get; set; }

        /// <summary>
        /// ID игры
        /// </summary>
        public string GameID { get; set; }

        /// <summary>
        /// Конструктор игры
        /// </summary>
        public TicTacToeGame()
        {
            InitializeBoard();
            IsFininshed = false;
            ActivePlayer = PlayerType.X;
            Winner = null;
            GameID = GenerateGameID();
        }

        /// <summary>
        /// Создание ID игры
        /// </summary>
        /// <returns></returns>
        private string GenerateGameID()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Инициализация поля игры
        /// </summary>
        private void InitializeBoard()
        {
            Board = new[]
            {
                new PlayerType?[] { null, null, null },
                new PlayerType?[] { null, null, null },
                new PlayerType?[] { null, null, null }
            };
        }

        /// <summary>
        /// Завершение игры
        /// </summary>
        /// <param name="winner"></param>
        private void EndGame(PlayerType? winner)
        {
            IsFininshed = true;
            Winner = winner;
        }

        /// <summary>
        /// Проверка условия окончания игры (строки)
        /// </summary>
        /// <param name="type"></param>
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

        /// <summary>
        /// Проверка условия окончания игры (столбцы)
        /// </summary>
        /// <param name="type"></param>
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

        /// <summary>
        /// Проверка условия окончания игры (диагонали)
        /// </summary>
        /// <param name="type"></param>
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

        /// <summary>
        /// Проверка условия окончания игры (закончились пустые клетки)
        /// </summary>
        private void CheckEmptyCells()
        {
            foreach (var x in Board)
            {
                foreach (var y in x)
                {
                    if (y == null)
                    {
                        return;
                    }
                }
            }

            EndGame(null);
        }

        /// <summary>
        /// Проверка условий окончания игры
        /// </summary>
        /// <param name="type"></param>
        private void CheckIsFinished(PlayerType type)
        {
            CheckEmptyCells();
            CheckRows(type);
            CheckColumns(type);
            CheckDiagonals(type);
        }

        /// <summary>
        /// Получение сообщения об ошибке
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public string GetErrorMessage(Move move)
        {
            if (move == null)
            {
                return "Move is null";
            }
            if (move.GameID != GameID)
            {
                return "Invalid GameID";
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

        /// <summary>
        /// Проверка правильности хода
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool IsMoveValid(Move move)
        {
            return string.IsNullOrEmpty(GetErrorMessage(move));
        }

        /// <summary>
        /// Обновление поля игры
        /// </summary>
        /// <param name="move"></param>
        private void UpdateBoard(Move move)
        {
            Board[move.CheckPointX][move.CheckPointY] = move.PlayerType;
        }

        /// <summary>
        /// Обновление игрока, который должен ходить
        /// </summary>
        private void UpdateActivePlayer()
        {
            ActivePlayer = ActivePlayer == PlayerType.X ? PlayerType.O : PlayerType.X;
        }

        /// <summary>
        /// Обработка хода
        /// </summary>
        /// <param name="move"></param>
        public void ProcessMove(Move move)
        {
            if (IsMoveValid(move))
            {
                UpdateBoard(move);
                UpdateActivePlayer();
                CheckIsFinished(move.PlayerType);
            }
        }
    }
}
