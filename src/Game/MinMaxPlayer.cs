using System;
using System.Linq;

namespace Game
{
    public class MinMaxPlayer
    {
        private readonly Random random = new Random();

        private Board GameBoard { get; }
        
        private int MaxSearchDepth { get; }

        public MinMaxPlayer(Board gameBoard, MinMaxPlayer.Difficulty difficulty = MinMaxPlayer.Difficulty.Easy)
        {
            this.GameBoard = gameBoard;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    this.MaxSearchDepth = 3;
                    break;
                
                case Difficulty.Medium:
                    this.MaxSearchDepth = 5;
                    break;

                case Difficulty.Hard:
                    this.MaxSearchDepth = 7;
                    break;
            }
        }

        public BoardPositions Next()
        {
            var move = this.random.Next(0, 9);
            return (BoardPositions)move;
        }
        
        private int CalculateScore()
        {
            return this.GameBoard.Lines.Aggregate(
                0,
                (s, r) => s + this.CalculateScore(r, this.GameBoard.TurnOwner));
        }

        /// <summary>
        /// Calculates the score for this line.
        /// </summary>
        /// <param name="turnOwner">The player who has advantage.</param>
        /// <returns>Using the scoring mechanism described at https://www.codeproject.com/Articles/43622/Solve-Tic-Tac-Toe-with-the-MiniMax-algorithm</returns>
        private int CalculateScore(Line line, Players turnOwner)
        {
            var score = 0;

            var counts = new[] { line.A, line.B, line.C }.Aggregate(
                (BlankCount: 0, XCount: 0, OCount: 0),
                (s, p) =>
                {
                    switch (p)
                    {
                        case Pieces.Blank:
                            s.BlankCount++;
                            break;
                        case Pieces.X:
                            s.XCount++;
                            break;
                        case Pieces.O:
                            s.OCount++;
                            break;
                    }

                    return s;
                });

            if (counts.XCount == 3 || counts.OCount == 3)
            {
                score = 1000;
            }
            else
            {
                var advantage = turnOwner == Players.X ? 3 : 1;
                if (counts.OCount == 0)
                {
                    score = advantage * (int) Math.Pow(10, counts.XCount);
                }
                else if (counts.XCount == 0)
                {
                    score = -advantage * (int) Math.Pow(10, counts.OCount);
                }
            }

            return score;
        }

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard,
        }
    }
}