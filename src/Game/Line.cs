using System;
using System.Linq;

namespace Game
{
    public record Line(Pieces A, Pieces B, Pieces C)
    {
        /// <summary>
        /// Calculates the score for this line.
        /// </summary>
        /// <param name="turnOwner">The player who has advantage.</param>
        /// <returns>Using the scoring mechanism described at https://www.codeproject.com/Articles/43622/Solve-Tic-Tac-Toe-with-the-MiniMax-algorithm</returns>
        internal int CalculateScore(Players turnOwner)
        {
            var score = 0;

            var counts = new[] { A, B, C }.Aggregate(
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
                    score = advantage * (int)Math.Pow(10, counts.XCount);
                }
                else if (counts.XCount == 0)
                {
                    score = -advantage * (int)Math.Pow(10, counts.OCount);
                }
            }

            return score;
        }
    }
}