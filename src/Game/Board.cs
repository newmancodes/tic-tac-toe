using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Board
    {
        private readonly IDictionary<BoardPositions, Pieces> placedPieces;
        private Line[] lines;

        public Players TurnOwner { get; private set; }

        public Players? Winner
        {
            get
            {
                if (this.IsOver)
                {
                    if (this.TurnOwner == Players.O)
                        return Players.X;
                    
                    return Players.O;
                }
                
                return null;
            }
        }

        internal IEnumerable<Line> Lines
        {
            get
            {
                if (this.lines == null)
                {
                    this.lines = new Line[8];
                    this.lines[0] = new Line( // Top Row
                        this.placedPieces[BoardPositions.LeftTop],
                        this.placedPieces[BoardPositions.MiddleTop],
                        this.placedPieces[BoardPositions.RightTop]);
                    this.lines[1] = new Line( // Middle Row
                        this.placedPieces[BoardPositions.LeftMiddle],
                        this.placedPieces[BoardPositions.Centre],
                        this.placedPieces[BoardPositions.RightMiddle]);
                    this.lines[2] = new Line( // Bottom Row
                        this.placedPieces[BoardPositions.LeftBottom],
                        this.placedPieces[BoardPositions.MiddleBottom],
                        this.placedPieces[BoardPositions.RightBottom]);
                    this.lines[3] = new Line( // Left Column
                        this.placedPieces[BoardPositions.LeftTop],
                        this.placedPieces[BoardPositions.LeftMiddle],
                        this.placedPieces[BoardPositions.LeftBottom]);
                    this.lines[4] = new Line( // Middle Column
                        this.placedPieces[BoardPositions.MiddleTop],
                        this.placedPieces[BoardPositions.Centre],
                        this.placedPieces[BoardPositions.MiddleBottom]);
                    this.lines[5] = new Line( // Right Column
                        this.placedPieces[BoardPositions.RightTop],
                        this.placedPieces[BoardPositions.RightMiddle],
                        this.placedPieces[BoardPositions.RightBottom]);
                    this.lines[6] = new Line( // Descending Diagonal LTR 
                        this.placedPieces[BoardPositions.LeftTop],
                        this.placedPieces[BoardPositions.Centre],
                        this.placedPieces[BoardPositions.RightBottom]);
                    this.lines[7] = new Line( // Ascending Diagonal LTR
                        this.placedPieces[BoardPositions.LeftBottom],
                        this.placedPieces[BoardPositions.Centre],
                        this.placedPieces[BoardPositions.RightTop]);
                }

                return this.lines;
            }
        }

        public IEnumerable<Line> Rows => this.Lines.Take(3);

        public bool IsOver => this.Lines.Any(l => l.A == l.B && l.B == l.C && l.C != Pieces.Blank);

        public Board()
        {
            this.TurnOwner = Players.X;
            this.placedPieces = new Dictionary<BoardPositions, Pieces>
            {
                {BoardPositions.LeftTop, Pieces.Blank},
                {BoardPositions.MiddleTop, Pieces.Blank},
                {BoardPositions.RightTop, Pieces.Blank},
                {BoardPositions.LeftMiddle, Pieces.Blank},
                {BoardPositions.Centre, Pieces.Blank},
                {BoardPositions.RightMiddle, Pieces.Blank},
                {BoardPositions.LeftBottom, Pieces.Blank},
                {BoardPositions.MiddleBottom, Pieces.Blank},
                {BoardPositions.RightBottom, Pieces.Blank},
            };
        }

        public void Play(BoardPositions boardPosition)
        {
            if (this.placedPieces[boardPosition] == Pieces.Blank)
            {
                if (this.TurnOwner == Players.X)
                {
                    this.placedPieces[boardPosition] = Pieces.X;
                    this.TurnOwner = Players.O;
                }
                else
                {
                    this.placedPieces[boardPosition] = Pieces.O;
                    this.TurnOwner = Players.X;
                }

                this.lines = null;
            }
        }
    }
}
