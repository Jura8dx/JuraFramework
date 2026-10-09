using System.IO;
using JuraFramework.Core;
using JuraFramework.Input;
using JuraFramework.Graphics.Textures;
using JuraFramework.Tests.Chess.Core;
using JuraFramework.Tests.Chess.Drawables;
using System;

#nullable disable

namespace JuraFramework.Tests.Chess.Scenes;

public class ChessScreen(Application application) : Scene(application)
{
    private ChessLogic chess;
    private ChessBoard chessBoard;

    protected override void LoadResources()
    {
        #region white-pixel.png

        AtlasTexture whitePixel = new(Path.Combine(application.ResourcePath, "white-pixel.png"), application.Host.Renderer);
        TextureStore.textures.Add("white-pixel.png", whitePixel);

        #endregion

        #region chess-pieces.png

        AtlasTexture pieces = new(Path.Combine(application.ResourcePath, "chess-pieces.png"), application.Host.Renderer);

        // White pieces
        pieces.subTextures.Add("wk", new(0f, 0f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("wq", new(1f / 6f, 0f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("wb", new(2f / 6f, 0f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("wn", new(3f / 6f, 0f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("wr", new(4f / 6f, 0f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("wp", new(5f / 6f, 0f, 1f / 6f, 0.5f));

        // Black pieces
        pieces.subTextures.Add("bk", new(0f, 0.5f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("bq", new(1f / 6f, 0.5f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("bb", new(2f / 6f, 0.5f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("bn", new(3f / 6f, 0.5f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("br", new(4f / 6f, 0.5f, 1f / 6f, 0.5f));
        pieces.subTextures.Add("bp", new(5f / 6f, 0.5f, 1f / 6f, 0.5f));

        TextureStore.textures.Add("chess-pieces.png", pieces);

        #endregion
    }

    public override void Update()
    {
        base.Update();

        chessBoard.Position += InputHandler.ArrowKeyDelta * 10f;
    }

    protected override void BuildSceneObjects()
    {
        chessBoard = new ChessBoard();
        Root.Append(chessBoard);
    }
}
