using System;
using JuraFramework.Core;
using JuraTK;
using Veldrid.StartupUtilities;
using Veldrid;

#nullable disable

namespace JuraFramework.Graphics.Rendering
{
    /// <summary>
    /// This is the first and most basic version of the renderer. It only supports
    /// drawing colored rectangles, and draws the entire scene in one single batch of vertices.
    /// I am creating <see cref="RendererV2"/> to handle drawing sprites to quads, and batching 
    /// the vertices in the scene into multiple 'draw calls', separated by the texture
    /// atlas that the sprite lives in.
    /// </summary>
    public class RendererV1 : Renderer, IDisposable
    {
        public DeviceBuffer VertexBuffer { get; private set; }
        public DeviceBuffer IndexBuffer { get; private set; }

        private Vertex[] sceneVertices = [];
        private uint[] sceneIndices = [];

        public CommandList Commands { get; set; }
        public Shader[] Shaders { get; set; }
        public Pipeline Pipeline { get; set; }

        public RendererV1(Engine engine)
        {
            GraphicsDeviceOptions options = new()
            {
                PreferStandardClipSpaceYDirection = true,
                PreferDepthRangeZeroToOne = true,
            };

            Device = VeldridStartup.CreateGraphicsDevice(engine.Window.MainWindow, options, GraphicsBackend.Vulkan);
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

        #region Render Loop

        private void BeginFrame()
        {
            Commands.Begin();
            Commands.SetFramebuffer(Device.SwapchainFramebuffer);
            Commands.ClearColorTarget(0, new RgbaFloat(0.188f, 0.180f, 0.169f, 1.0f));
            Commands.SetPipeline(Pipeline);
            Commands.SetVertexBuffer(0, VertexBuffer);
            Commands.SetIndexBuffer(IndexBuffer, IndexFormat.UInt32);
        }

        public override void RenderFrame(Scene scene)
        {
            if (scene.DrawNodes.Count > Renderer.J_MAXIMUM_DRAW_OBJECTS_PER_SCENE)
                throw new InvalidOperationException($"Active scene contains too many objects! ({scene.DrawNodes.Count}");

            BeginFrame();

            scene.Traverse();

            sceneVertices = new Vertex[scene.DrawNodes.Count * 4];
            sceneIndices = new uint[scene.DrawNodes.Count * 6];

            for (int i = 0; i < scene.DrawNodes.Count; i++)
            {
                DrawData node = scene.DrawNodes[i];
                uint objectIndex = (uint)i * 4;
                uint triangleIndex = (uint)i * 6;

                sceneVertices[objectIndex] = new(node.Source.Rectangle.TopLeft, Float2.Up, node.Source.VertexColor);
                sceneVertices[objectIndex + 1] = new(node.Source.Rectangle.TopRight, Float2.One, node.Source.VertexColor);
                sceneVertices[objectIndex + 2] = new(node.Source.Rectangle.BottomLeft, Float2.Zero, node.Source.VertexColor);
                sceneVertices[objectIndex + 3] = new(node.Source.Rectangle.BottomRight, Float2.Right, node.Source.VertexColor);

                sceneIndices[triangleIndex] = objectIndex;
                sceneIndices[triangleIndex + 1] = objectIndex + 1;
                sceneIndices[triangleIndex + 2] = objectIndex + 2;

                sceneIndices[triangleIndex + 3] = objectIndex + 1;
                sceneIndices[triangleIndex + 4] = objectIndex + 3;
                sceneIndices[triangleIndex + 5] = objectIndex + 2;
            }

            Commands.UpdateBuffer(VertexBuffer, 0, sceneVertices);
            Commands.UpdateBuffer(IndexBuffer, 0, sceneIndices);

            Commands.DrawIndexed(
                indexCount: (uint)sceneIndices.Length,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0
            );

            EndFrame();
        }

        private void EndFrame()
        {
            Commands.End();
            Device.SubmitCommands(Commands);
            Device.SwapBuffers();
        }

        #endregion

        public void Dispose()
        {
            Pipeline.Dispose();

            foreach(Shader s in Shaders) { s.Dispose(); }

            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
            Commands.Dispose();
            Device.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
