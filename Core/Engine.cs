using JuraFramework.Graphics;
using JuraFramework.Graphics.Rendering;
using JuraFramework.Input;
using Veldrid;

#nullable disable

namespace JuraFramework.Core
{
    public class Engine
    {
        public Window Window { get; private set; }
        public Renderer Renderer { get; private set; }
        public Application TargetApplication { get; private set; }

        private bool isRunning = false;

        // Initialize new window and Renderer
        public void Run(Application target)
        {
            if (isRunning)
                return;

            TargetApplication = target;
            Window = new Window(TargetApplication.Title, 1280, 720);
            Renderer = new RendererV1(this);
            TargetApplication.ApplicationHost = this;
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

                InputHandler.Update(snapshot);
                TargetApplication.Update();
                Renderer.RenderFrame(TargetApplication.ActiveScene);
            }

            Renderer.Dispose();
        }
    }
}
