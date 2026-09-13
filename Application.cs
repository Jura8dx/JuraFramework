#nullable disable

using JuraFramework.Objects;
using JuraFramework.Scenes;
using System;
using System.Collections.Generic;
using Veldrid.Sdl2;

namespace JuraFramework
{
    public abstract class Application : IDisposable
    {
        public string Title { get; set; }

        public Engine ApplicationHost { get; set; }
        public Sdl2Window Window => ApplicationHost?.Window;
        public Scene ActiveScene { get; protected set; }

        public virtual void Update() { }
        public virtual void Start() { }

        public abstract void BuildScene();
        public abstract void Dispose();
    }
}
