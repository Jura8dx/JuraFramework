#nullable disable

using JuraFramework.Objects;
using System;
using Veldrid;
using Veldrid.StartupUtilities;

namespace JuraFramework.Graphics.Rendering
{
    public class Renderer : IDisposable
    {
        public GraphicsDevice Device { get; set; }
        public ResourceFactory Factory { get; private set; }
        public Swapchain MainSwapchain { get; private set; }
        public CommandList Commands { get; set; }
        public Shader[] Shaders { get; set; }
        public Pipeline Pipeline { get; set; }

        public Renderer(Engine engine)
        {
            GraphicsDeviceOptions options = new GraphicsDeviceOptions
            {
                PreferStandardClipSpaceYDirection = true,
                PreferDepthRangeZeroToOne = true,
            };

            Device = VeldridStartup.CreateGraphicsDevice(engine.Window, options);
            Factory = Device.ResourceFactory;
        }

        public void CreateResources()
        {
            // Initialize shaders
            VertexElementDescription positionSemantic = new("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float3);
            VertexLayoutDescription vertexLayout = new(positionSemantic);

            Shaders = GraphicsShader.GetShader("TestShader", Factory);

            // Create pipeline
            GraphicsPipelineDescription pipelineDescription = new();
            pipelineDescription.BlendState = BlendStateDescription.SingleOverrideBlend;

            pipelineDescription.DepthStencilState = new(
                depthTestEnabled: true,
                depthWriteEnabled: true,
                comparisonKind: ComparisonKind.Less);

            pipelineDescription.RasterizerState = new(
                cullMode: FaceCullMode.Back,
                fillMode: PolygonFillMode.Solid,
                frontFace: FrontFace.Clockwise,
                depthClipEnabled: true,
                scissorTestEnabled: false);

            pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleList;
            pipelineDescription.ResourceLayouts = [];

            pipelineDescription.ShaderSet = new(
                vertexLayouts: [vertexLayout],
                shaders: Shaders);

            pipelineDescription.Outputs = Device.SwapchainFramebuffer.OutputDescription;

            Pipeline = Factory.CreateGraphicsPipeline(pipelineDescription);
            Commands = Factory.CreateCommandList();
        }

        public void BeginFrame()
        {
            Commands.Begin();
            Commands.SetFramebuffer(Device.SwapchainFramebuffer);
            Commands.ClearColorTarget(0, RgbaFloat.Black);
            Commands.SetPipeline(Pipeline);
        }

        public void EndFrame()
        {
            Commands.End();
            Device.SubmitCommands(Commands);
            Device.SwapBuffers();
        }

        public void RenderFrame(Engine engine)
        {
            foreach(Mesh mesh in engine.TargetApplication.ActiveScene.renderableObjects)
            {
                Commands.SetVertexBuffer(0, mesh.vertexBuffer);
                Commands.SetIndexBuffer(mesh.indexBuffer, IndexFormat.UInt32);

                Commands.DrawIndexed(
                    indexCount: (uint)mesh.indices.Count,
                    instanceCount: 1,
                    indexStart: 0,
                    vertexOffset: 0,
                    instanceStart: 0);
            }
        }

        public void Dispose()
        {
            Pipeline.Dispose();

            foreach(Shader s in Shaders)
            {
                s.Dispose();
            }

            Commands.Dispose();
            Device.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
