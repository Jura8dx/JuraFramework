using JuraFramework.Graphics;
using JuraFramework.Input;
using JuraTK;
using Veldrid;

namespace JuraFramework.Tests.Chess.Objects
{
    public class ChessBoard : Container
    {
        const float BOARD_SIZE = 1500f;
        const float SQUARE_SIZE = BOARD_SIZE / 8f;

        static RgbaFloat LightSquareColor => new(0.91f, 0.91f, 0.82f, 1.0f);
        static RgbaFloat DarkSquareColor => new(0.29f, 0.45f, 0.60f, 1.0f);

        public override void Update()
        {
            foreach(Drawable child in Children)
            {
                child.Position += InputHandler.ArrowKeyDelta * 0.5f;
            }
        }

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

                    Drawable square = new(squarePosition, new Float2(SQUARE_SIZE), Quaternion.Identity, squareColor);
                    AppendChild(square);
                }
            }

            // Draw piece sprites
        }
    }
}
