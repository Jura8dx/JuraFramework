using System;

namespace JuraFramework.Tests.Chess.Core
{
    public static class Chess
    {
        public static class Piece
        {
            public const int None = 0;
            public const int King = 1;
            public const int Pawn = 2;
            public const int Knight = 3;
            public const int Bishop = 4;
            public const int Rook = 5;
            public const int Queen = 6;

            public const int White = 8;
            public const int Black = 16;

            public static int Color(int piece)
            {
                if (piece >> 4 == 1) 
                    return Black;
                return White;
            }
        }

        public class ChessBoard
        {
            private readonly int[] squares = new int[64];

            public int this[string square]
            {
                get
                {
                    int index = SquareToBoardIndex(square);
                    return squares[index];
                }
                set
                {
                    int index = SquareToBoardIndex(square);
                    squares[index] = value;
                }
            }

            public static int SquareToBoardIndex(string square)
            {
                square = square.ToLower();
                int file = square[0] - 'a';
                int rank = square[1] - '1';

                return (rank * 8) + file;
            }
        }

        public static readonly ChessBoard Board = new ChessBoard();

        public static void InitializeBoard()
        {
            // White pieces - Starting position
            Board["a1"] = Piece.White | Piece.Rook;
            Board["b1"] = Piece.White | Piece.Knight;
            Board["c1"] = Piece.White | Piece.Bishop;
            Board["d1"] = Piece.White | Piece.Queen;
            Board["e1"] = Piece.White | Piece.King;
            Board["f1"] = Piece.White | Piece.Bishop;
            Board["g1"] = Piece.White | Piece.Knight;
            Board["h1"] = Piece.White | Piece.Rook;

            Board["a2"] = Piece.White | Piece.Pawn;
            Board["b2"] = Piece.White | Piece.Pawn;
            Board["c2"] = Piece.White | Piece.Pawn;
            Board["d2"] = Piece.White | Piece.Pawn;
            Board["e2"] = Piece.White | Piece.Pawn;
            Board["f2"] = Piece.White | Piece.Pawn;
            Board["g2"] = Piece.White | Piece.Pawn;
            Board["h2"] = Piece.White | Piece.Pawn;

            // Black pieces - Starting position
            Board["a8"] = Piece.Black | Piece.Rook;
            Board["b8"] = Piece.Black | Piece.Knight;
            Board["c8"] = Piece.Black | Piece.Bishop;
            Board["d8"] = Piece.Black | Piece.Queen;
            Board["e8"] = Piece.Black | Piece.King;
            Board["f8"] = Piece.Black | Piece.Bishop;
            Board["g8"] = Piece.Black | Piece.Knight;
            Board["h8"] = Piece.Black | Piece.Rook;

            Board["a7"] = Piece.Black | Piece.Pawn;
            Board["b7"] = Piece.Black | Piece.Pawn;
            Board["c7"] = Piece.Black | Piece.Pawn;
            Board["d7"] = Piece.Black | Piece.Pawn;
            Board["e7"] = Piece.Black | Piece.Pawn;
            Board["f7"] = Piece.Black | Piece.Pawn;
            Board["g7"] = Piece.Black | Piece.Pawn;
            Board["h7"] = Piece.Black | Piece.Pawn;
        }
    }
}
