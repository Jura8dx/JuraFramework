using JuraFramework.Core;
using JuraFramework.Tests.Chess.Scenes;
using System;
using System.IO;

#nullable disable

namespace JuraFramework.Tests.Chess;

public sealed class ChessGame : Application
{
    public ChessGame()
    {
        Title = "Hello Chess";
        ResourcePath = Path.Combine(AppContext.BaseDirectory, "Tests", "Chess", "Resources");
        ActiveScene = new ChessScreen(this);
    }

    public override void Start()
    {
        base.Start();
    }
}
