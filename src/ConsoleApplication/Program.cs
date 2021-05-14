using System;
using System.Linq;
using Game;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var gameBoard = new Board();
            var ai = new MinMaxPlayer(gameBoard, MinMaxPlayer.Difficulty.Easy);
            while (!gameBoard.IsOver)
            {
                Console.Clear();
                DisplayBoard(gameBoard);

                var playerMove = BoardPositions.Centre;
                
                if (gameBoard.TurnOwner == Players.X)
                {
                    playerMove = PromptForPlayerMove();
                }
                else if (gameBoard.TurnOwner == Players.O)
                {
                    playerMove = ai.Next();
                }
                
                gameBoard.Play(playerMove);
            }

            Console.Clear();
            DisplayBoard(gameBoard);
            Console.WriteLine($"{gameBoard.Winner.Value} won the game! Hope you had the big fun!");
        }

        private static void DisplayBoard(Board gameBoard)
        {
            char DisplayPiece(Pieces p)
            {
                if (p == Pieces.X)
                    return 'X';
                
                if (p == Pieces.O)
                    return 'O';

                return ' ';
            }

            gameBoard.Rows.Select((r, i) => (Row: r, Index: i)).ToList().ForEach(i =>
            {
                Console.WriteLine($"  {DisplayPiece(i.Row.A)}  |  {DisplayPiece(i.Row.B)}  |  {DisplayPiece(i.Row.C)}  ");
                if (i.Index is 0 or 1)
                {
                    Console.WriteLine("-----+-----+-----");
                }
            });
        }

        private static BoardPositions PromptForPlayerMove()
        {
            do
            {
                Console.WriteLine("Your move punk!");
                var move = Console.ReadKey(false).KeyChar;

                switch (move)
                {
                    case 'q':
                        return BoardPositions.LeftTop;
                    case 'w':
                        return BoardPositions.MiddleTop;
                    case 'e':
                        return BoardPositions.RightTop;
                    case 'a':
                        return BoardPositions.LeftMiddle;
                    case 's':
                        return BoardPositions.Centre;
                    case 'd':
                        return BoardPositions.RightMiddle;
                    case 'z':
                        return BoardPositions.LeftBottom;
                    case 'x':
                        return BoardPositions.MiddleBottom;
                    case 'c':
                        return BoardPositions.RightBottom;
                }
            } while (true);
        }
    }
}
