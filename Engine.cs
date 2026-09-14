#nullable disable

using JuraFramework.Graphics.Display;
using JuraFramework.Graphics.Rendering;
using Veldrid.StartupUtilities;

namespace JuraFramework
{
    public class Engine
    {
        public Window Window { get; private set; }
        public Renderer Renderer { get; private set; }
        public Application TargetApplication { get; private set; }

        public Engine(Application target)
        {
            TargetApplication = target;
        }

        // Initialize new window and Renderer
        public void Run()
        {
            Window = new Window(TargetApplication.Title, 1280, 720);
            Renderer = new Renderer(this);
            TargetApplication.ApplicationHost = this;
            EngineLoop();
        }

        private void EngineLoop()
        {
            Renderer.CreateResources();
            TargetApplication.Start();

            while (Window.MainWindow.Exists)
            {
                Window.MainWindow.PumpEvents();
                Renderer.BeginFrame();
                TargetApplication.Update();
                Renderer.DrawScene(TargetApplication.ActiveScene);
                Renderer.EndFrame();
            }

            TargetApplication.Dispose();
            Renderer.Dispose();
        }
    }
}
