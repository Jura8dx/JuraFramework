using JuraFramework.Core;
using JuraFramework.Tests.Chess.Objects;

namespace JuraFramework.Tests.Chess.Scenes
{
    public class ChessScreen : Scene
    {
        public ChessScreen()
        {
            Root.AppendChild(new ChessBoard());
        }
    }
}
