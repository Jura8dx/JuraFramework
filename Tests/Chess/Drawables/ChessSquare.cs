using JMath;
using JuraFramework.Graphics;
using JuraFramework.Tests.Chess.Core;
using Veldrid;

namespace JuraFramework.Tests.Chess.Drawables
{
    public class ChessSquare(Float2 pos, Float2 size, RgbaFloat tint) : Drawable(pos, size, tint)
    {
        private int piece = Piece.None;

        public int PieceOnSquare
        {
            get => piece;
            set
            {
                piece = value;
                Children.Clear();

                if (value == Piece.None)
                    return;

                Drawable newPiece = new Drawable(Position, Size)
                {
                    TextureID = "chess-pieces.png",
                    SubTextureID = PieceTypeToTextureID(value)
                };

                Append(newPiece);
            }
        }

        private static string PieceTypeToTextureID(int pieceType)
        {
            return pieceType switch
            {
                Piece.White | Piece.Pawn => "wp",
                Piece.White | Piece.King => "wk",
                Piece.White | Piece.Knight => "wn",
                Piece.White | Piece.Bishop => "wb",
                Piece.White | Piece.Rook => "wr",
                Piece.White | Piece.Queen => "wq",

                Piece.Black | Piece.Pawn => "bp",
                Piece.Black | Piece.King => "bk",
                Piece.Black | Piece.Knight => "bn",
                Piece.Black | Piece.Bishop => "bb",
                Piece.Black | Piece.Rook => "br",
                Piece.Black | Piece.Queen => "bq",

                _ => string.Empty,
            };
        }
    }
}
