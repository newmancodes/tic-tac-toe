using System;

namespace Game
{
    public class MinMaxPlayer
    {
        private readonly Random random = new Random();

        private Board GameBoard { get; }

        public MinMaxPlayer(Board gameBoard)
        {
            this.GameBoard = gameBoard;
        }

        public BoardPositions Next()
        {
            var move = this.random.Next(0, 9);
            return (BoardPositions)move;
        }
    }
}