using JuraFramework.Core;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace JuraFramework.Graphics;

public class Window
{
    public Sdl2Window MainWindow { get; }
    // public Swapchain MainSwapchain { get; }
    public SwapchainDescription Swapchain { get; }

    const int SDL_WINDOWPOS_CENTERED = 0x2FFF0000;

    public Window(string title, int width, int height)
    {
        WindowCreateInfo windowCI = new()
        {
            X = SDL_WINDOWPOS_CENTERED,
            Y = SDL_WINDOWPOS_CENTERED,
            WindowWidth = width,
            WindowHeight = height,
            WindowTitle = title
        };

        MainWindow = VeldridStartup.CreateWindow(windowCI);
        MainWindow.Resizable = false;

        Swapchain = new()
        {
            Source = VeldridStartup.GetSwapchainSource(MainWindow),
            Width = (uint)MainWindow.Width,
            Height = (uint)MainWindow.Height,
            DepthFormat = PixelFormat.D24_UNorm_S8_UInt,
            SyncToVerticalBlank = true
        };
    }
}
