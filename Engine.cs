#nullable disable

using JuraFramework.Graphics.Rendering;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace JuraFramework
{
    public class Engine
    {
        public Sdl2Window Window { get; private set; }
        public Renderer Renderer { get; private set; }
        public Application TargetApplication { get; private set; }

        public Engine(Application target)
        {
            TargetApplication = target;
        }

        // Initialize new window and Renderer
        public void Run()
        {
            WindowCreateInfo windowCI = new WindowCreateInfo
            {
                X = 100,
                Y = 100,
                WindowWidth = 1366,
                WindowHeight = 768,
                WindowTitle = TargetApplication.Title
            };

            Window = VeldridStartup.CreateWindow(ref windowCI);
            Renderer = new Renderer(this);
            TargetApplication.ApplicationHost = this;

            EngineLoop();
        }

        private void EngineLoop()
        {
            Renderer.CreateResources();
            TargetApplication.Start();

            while (Window.Exists)
            {
                Window.PumpEvents();
                Renderer.BeginFrame();
                TargetApplication.Update();
                Renderer.RenderFrame(this);
                Renderer.EndFrame();
            }

            TargetApplication.Dispose();
            Renderer.Dispose();
        }
    }
}
