using JuraFramework.Core;
using JuraFramework.Tests.Chess.Scenes;

namespace JuraFramework.Tests.Chess
{
    public sealed class ChessGame : Application
    {
        public ChessGame()
        {
            Title = "Test Application";
            ActiveScene = new ChessScreen();
        }
    }
}
