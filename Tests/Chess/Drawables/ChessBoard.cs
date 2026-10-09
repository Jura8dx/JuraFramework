using JuraFramework.Graphics;
using JMath;
using Veldrid;
using JuraFramework.Tests.Chess.Core;

namespace JuraFramework.Tests.Chess.Drawables
{
    public class ChessBoard : Container
    {
        public const float BOARD_SIZE = 1500f;
        public const float SQUARE_SIZE = BOARD_SIZE / 8f;

        static RgbaFloat LightSquareColor => new(0.91f, 0.91f, 0.82f, 1.0f);
        static RgbaFloat DarkSquareColor => new(0.29f, 0.45f, 0.60f, 1.0f);

        public ChessBoard()
        {
            // Draw squares
            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    bool isLightSquare = (file + rank) % 2 != 0;

                    RgbaFloat squareColor = isLightSquare ? LightSquareColor : DarkSquareColor;
                    Float2 squarePosition = new Float2(-3.5f + file, -3.5f + rank) * SQUARE_SIZE;

                    ChessSquare square = new(squarePosition, new Float2(SQUARE_SIZE), squareColor)
                    {
                        PieceOnSquare = Piece.White | Piece.Pawn
                    };

                    Append(square);
                }
            }
        }
    }
}
