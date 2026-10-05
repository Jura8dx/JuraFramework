#nullable disable

using Veldrid.Sdl2;

namespace JuraFramework.Core
{
    public abstract class Application
    {
        public string Title { get; set; }
        public Engine ApplicationHost { get; set; }
        public Sdl2Window Window => ApplicationHost?.Window.MainWindow;
        public Scene ActiveScene { get; protected set; }

        public virtual void Update()
        {
            ActiveScene?.Update();
        }

        public virtual void Start()
        {
            
        }
    }
}
