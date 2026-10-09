#nullable disable

using JuraFramework.Graphics.Textures;
using Veldrid.Sdl2;

namespace JuraFramework.Core;

public abstract class Application
{
    public string Title { get; set; }
    public string ResourcePath { get; set; }

    public Engine Host { get; set; }
    // public Sdl2Window Window => Host?.Window.MainWindow;
    public Scene ActiveScene { get; protected set; }

    public virtual void Update()
    {
        ActiveScene.Update();
    }

    public virtual void Start()
    {
        ActiveScene.Load();
        ActiveScene.Start();
    }
}
