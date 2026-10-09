using System;
using System.Diagnostics;
using JuraFramework.Graphics;
using JuraFramework.Debug;
using JuraFramework.Graphics.Rendering;
using JuraFramework.Input;
using Veldrid;

#nullable disable

namespace JuraFramework.Core;

public class Engine
{
    public Window Window { get; private set; }
    public Renderer Renderer { get; private set; }
    public Application TargetApplication { get; private set; }

    private bool isRunning = false;

    public void Run(Application target)
    {
        if (isRunning)
            return;

        TargetApplication = target;
        Window = new Window(TargetApplication.Title, 1600, 900);
        Renderer = new VeldridRenderer(this);
        TargetApplication.Host = this;
        EngineLoop();

        isRunning = true;
    }

    private void EngineLoop()
    {
        TargetApplication.Start();

        while (Window.MainWindow.Exists)
        {
            InputSnapshot snapshot = Window.MainWindow.PumpEvents();
            
            if (!Window.MainWindow.Exists)
                break;

            Time.UpdateTime();
            InputHandler.CaptureInput(snapshot);
            TargetApplication.Update();
            Renderer.RenderFrame(TargetApplication);
        }

        Renderer.Dispose();
    }
}
