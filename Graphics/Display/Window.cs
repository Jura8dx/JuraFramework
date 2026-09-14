using System;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace JuraFramework.Graphics.Display
{
    public class Window
    {
        public Sdl2Window MainWindow { get; }

        const int SDL_WINDOWPOS_CENTERED = 0x2FFF0000;

        public Window(string title, int width, int height)
        {
            WindowCreateInfo windowCI = new WindowCreateInfo
            {
                X = SDL_WINDOWPOS_CENTERED,
                Y = SDL_WINDOWPOS_CENTERED,
                WindowWidth = width,
                WindowHeight = height,
                WindowTitle = title
            };

            MainWindow = VeldridStartup.CreateWindow(windowCI);
            MainWindow.Resizable = false;
        }
    }
}
