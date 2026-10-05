using System;
using JuraFramework.Core;
using JuraTK;
using Veldrid.StartupUtilities;
using Veldrid;
using System.Collections.Generic;

#nullable disable

namespace JuraFramework.Graphics.Rendering
{
    /// <summary>
    /// This renderer is a large upgrade from <see cref="RendererV1"/>. Now, every quad is drawn
    /// with an associated texture on it. Also, this renderer no longer stores arrays of 
    /// vertices and indices, like what was seen in the previous renderer. Instead, vertices are 
    /// stored in 'batches', where one batch just contains the mesh data for every object in the scene
    /// that is drawn with a specific texture atlas. So, for example, in my little chess template, the
    /// scene would be rendered in two batches - one for the squares, which use a single white pixel for
    /// their texture, and one for the pieces, which are all stored in their own texture atlas. That
    /// little scene would then be rendered in two separate batches, or draw calls. So, every texture atlas
    /// in the scene essentially gets its own draw call. 
    /// I eventually want this to work with text as well. That would be just as simple, since I can implement 
    /// my own text renderer that generates a texture atlas of every glyph, so each different font will be 
    /// rendered in its own batch.
    /// </summary>
    public class RendererV2 : Renderer, IDisposable
    {
        public DeviceBuffer VertexBuffer { get; private set; }
        public DeviceBuffer IndexBuffer { get; private set; }

        private readonly List<RenderBatch> batches;

        public CommandList Commands { get; set; }
        public Shader[] Shaders { get; set; }
        public Pipeline Pipeline { get; set; }

        public RendererV2(Engine engine)
        {
            GraphicsDeviceOptions options = new()
            {
                PreferStandardClipSpaceYDirection = true,
                PreferDepthRangeZeroToOne = true,
            };

            Device = VeldridStartup.CreateGraphicsDevice(engine.Window.MainWindow);
            Factory = Device.ResourceFactory;

            CreateResources();
        }

        public override void CreateResources()
        {
            uint vertexStride = (uint)Vertex.SizeInBytes * Renderer.J_MAXIMUM_VERTEX_COUNT;
            uint indexStride = sizeof(uint) * Renderer.J_MAXIMUM_TRIANGLE_INDEX_COUNT;

            BufferDescription vertexBufferDescription = new(vertexStride, BufferUsage.VertexBuffer);
            BufferDescription indexBufferDescription = new(indexStride, BufferUsage.IndexBuffer);

            VertexBuffer = Factory.CreateBuffer(vertexBufferDescription);
            IndexBuffer = Factory.CreateBuffer(indexBufferDescription);

            VertexLayoutDescription vertexLayout = new(
                new VertexElementDescription("POSITION", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                new VertexElementDescription("TEXCOORD0", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                new VertexElementDescription("COLOR", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4)
            );

            Shaders = GraphicsShader.GetShader("TestShader", Factory);

            // Create pipeline
            GraphicsPipelineDescription pipelineDescription = new()
            {
                BlendState = BlendStateDescription.SingleOverrideBlend,
                // DepthStencilState = new(true, true, ComparisonKind.Less),
                DepthStencilState = DepthStencilStateDescription.Disabled,
                RasterizerState = new(FaceCullMode.Back, PolygonFillMode.Solid, FrontFace.Clockwise, true, false),
                PrimitiveTopology = PrimitiveTopology.TriangleList,
                ResourceLayouts = [],
                ShaderSet = new([vertexLayout], Shaders),
                Outputs = Device.SwapchainFramebuffer.OutputDescription
            };

            Pipeline = Factory.CreateGraphicsPipeline(pipelineDescription);
            Commands = Factory.CreateCommandList();
        }

        private void BeginFrame()
        {
            Commands.Begin();
            Commands.SetFramebuffer(Device.SwapchainFramebuffer);
            Commands.ClearColorTarget(0, new RgbaFloat(0.188f, 0.180f, 0.169f, 1.0f));
            Commands.SetPipeline(Pipeline);
            Commands.SetVertexBuffer(0, VertexBuffer);
            Commands.SetIndexBuffer(IndexBuffer, IndexFormat.UInt32);
        }

        private void BuildRenderBatches(List<DrawData> sceneNodes)
        {

        }

        public override void RenderFrame(Scene scene)
        {
            if (scene.DrawNodes.Count > Renderer.J_MAXIMUM_DRAW_OBJECTS_PER_SCENE)
                throw new InvalidOperationException($"Active scene contains too many objects! ({scene.DrawNodes.Count}");

            BeginFrame();
            scene.Traverse();

            BuildRenderBatches(scene.DrawNodes);

            EndFrame();
        }

        private void EndFrame()
        {
            Commands.End();
            Device.SubmitCommands(Commands);
            Device.SwapBuffers();
        }

        public void Dispose()
        {
            Pipeline.Dispose();

            foreach (Shader s in Shaders) { s.Dispose(); }

            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
            Commands.Dispose();
            Device.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
