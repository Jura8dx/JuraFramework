using JuraFramework.Core;
using Veldrid;

namespace JuraFramework.Tests.Chess
{
    internal class Program
    {
        static void Main()
        {
            new Engine().Run(new ChessGame());
        }
    }
}
