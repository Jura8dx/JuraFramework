using JuraFramework.Graphics.Rendering;
using JuraFramework.Tk;
using System;
using System.Collections.Generic;
using Veldrid;
using Veldrid.SPIRV;

namespace JuraFramework.Objects
{
    public class Mesh : GameObject, IDisposable
    {
        public List<Float3> vertices = [];
        public List<uint> indices = [];

        public DeviceBuffer vertexBuffer = null!;
        public DeviceBuffer indexBuffer = null!;

        public void CreateGpuResources(GraphicsDevice device, ResourceFactory factory)
        {
            BufferDescription vbd = new((uint)(Float3.SizeInBytes * vertices.Count), BufferUsage.VertexBuffer);
            BufferDescription ibd = new((uint)(sizeof(uint) * indices.Count), BufferUsage.IndexBuffer);

            vertexBuffer = factory.CreateBuffer(vbd);
            indexBuffer = factory.CreateBuffer(ibd);

            device.UpdateBuffer(vertexBuffer, 0, vertices.ToArray());
            device.UpdateBuffer(indexBuffer, 0, indices.ToArray());
        }

        public void Dispose()
        {
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
