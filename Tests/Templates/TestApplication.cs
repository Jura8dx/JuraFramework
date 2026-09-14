using JuraFramework.Objects;
using JuraFramework.Scenes;
using JuraFramework.Tk;
using System;

namespace JuraFramework.Tests.Templates
{
    public sealed class TestApplication : Application
    {
        public TestApplication(string title)
        {
            Title = title;
        }

        public override void Start()
        {
            Mesh mesh = new Mesh();
            mesh.vertices.Add(new Vertex(new Float3(-0.2f, 0.2f, 0f), new Float2(0f, 1f))); // Top left
            mesh.vertices.Add(new Vertex(new Float3(0.2f, -0.2f, 0f), new Float2(1f, 0f))); // Bottom right
            mesh.vertices.Add(new Vertex(new Float3(-0.2f, -0.2f, 0f), new Float2(0f, 0f))); // Bottom left
            mesh.vertices.Add(new Vertex(new Float3(0.2f, 0.2f, 0f), new Float2(1f, 1f))); // Top right

            mesh.indices = [0, 1, 2, 0, 3, 1];

            ActiveScene = new Scene();
            ActiveScene.renderableObjects.Add(mesh);

            BuildScene();
        }

        public override void BuildScene()
        {
            foreach(Mesh mesh in ActiveScene.renderableObjects)
            {
                mesh.CreateGpuResources(ApplicationHost.Renderer.Device, ApplicationHost.Renderer.Factory);
            }
        }

        public override void Dispose()
        {
            foreach(Mesh m in ActiveScene.renderableObjects)
            {
                m.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
