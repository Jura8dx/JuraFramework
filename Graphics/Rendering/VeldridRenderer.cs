using System;
using System.Collections.Generic;
using JuraFramework.Graphics.Textures;
using JuraFramework.Core;
using JMath;
using Veldrid;
using Veldrid.StartupUtilities;

#nullable disable

namespace JuraFramework.Graphics.Rendering;

public class VeldridRenderer : Renderer, IDisposable
{
    private List<RenderBatch> renderBatches = [];
    private Vertex[] sceneVertices = [];
    private uint[] sceneIndices = [];

    public VeldridRenderer(Engine engine)
    {
        GraphicsDeviceOptions deviceOptions = new()
        {
            PreferStandardClipSpaceYDirection = true,
            PreferDepthRangeZeroToOne = true,
            SyncToVerticalBlank = true
        };


        Device = VeldridStartup.CreateGraphicsDevice(engine.Window.MainWindow, deviceOptions);
        Factory = Device.ResourceFactory;

        CreateResources();
    }

    public override void CreateResources()
    {
        uint vertexStride = (uint)Vertex.SizeInBytes * J_MAXIMUM_VERTEX_COUNT;
        uint indexStride = sizeof(uint) * J_MAXIMUM_TRIANGLE_INDEX_COUNT;

        BufferDescription vertexBufferDescription = new(vertexStride, BufferUsage.VertexBuffer);
        BufferDescription indexBufferDescription = new(indexStride, BufferUsage.IndexBuffer);

        VertexBuffer = Factory.CreateBuffer(vertexBufferDescription);
        IndexBuffer = Factory.CreateBuffer(indexBufferDescription);

        VertexLayoutDescription vertexLayout = new(
            new VertexElementDescription("POSITION", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
            new VertexElementDescription("TEXCOORD0", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
            new VertexElementDescription("COLOR", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4)
        );

        ResourceLayoutDescription textureLayout = new(
            new ResourceLayoutElementDescription("TEXTURE", ResourceKind.TextureReadOnly, ShaderStages.Fragment),
            new ResourceLayoutElementDescription("SAMPLER", ResourceKind.Sampler, ShaderStages.Fragment)
        );

        SamplerDescription samplerDescription = new(
            SamplerAddressMode.Clamp,
            SamplerAddressMode.Clamp,
            SamplerAddressMode.Clamp,
            SamplerFilter.MinLinear_MagLinear_MipLinear,
            null, 0, 0, 0, 0, SamplerBorderColor.TransparentBlack
        );

        TextureSampler = TextureSampler = Factory.CreateSampler(samplerDescription);
        TextureLayout = Factory.CreateResourceLayout(textureLayout);

        Shaders = GraphicsShader.GetShader("TestShader", Factory);

        // Create pipeline
        GraphicsPipelineDescription pipelineDescription = new()
        {
            BlendState = BlendStateDescription.SingleAlphaBlend,
            // DepthStencilState = new(true, true, ComparisonKind.Less),
            DepthStencilState = DepthStencilStateDescription.Disabled,
            RasterizerState = new(FaceCullMode.Back, PolygonFillMode.Solid, FrontFace.Clockwise, true, false),
            PrimitiveTopology = PrimitiveTopology.TriangleList,
            ResourceLayouts = [ TextureLayout ],
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

    public override void RenderFrame(Application target)
    {
        BeginFrame();

        // Build orderer list of scene objects
        target.ActiveScene.Traverse();

        if (target.ActiveScene.RenderObjects.Count > J_MAXIMUM_DRAW_OBJECTS_PER_SCENE)
            throw new InvalidOperationException($"Active scene contains too many objects! ({target.ActiveScene.RenderObjects.Count})");

        if(target.ActiveScene.RenderObjects.Count == 0)
        {
            EndFrame();
            return;
        }

        if(sceneVertices.Length < target.ActiveScene.RenderObjects.Count * 4)
        {
            sceneVertices = new Vertex[target.ActiveScene.RenderObjects.Count * 4];
            sceneIndices = new uint[target.ActiveScene.RenderObjects.Count * 6];
        }

        // Build render batches
        renderBatches.Clear();
        AtlasTexture currentBatchTexture = target.ActiveScene.RenderObjects[0].Texture;
        uint batchIndexStart = 0;
        uint batchIndexCount = 0;

        for (int i = 0; i < target.ActiveScene.RenderObjects.Count; i++)
        {
            Drawable drawable = target.ActiveScene.RenderObjects[i];
            uint objectIndex = (uint)i * 4;
            uint triangleIndex = (uint)i * 6;

            // If the current object does not use the same texture as the previous one,
            // render the current batch and start a new one
            if (drawable.Texture != currentBatchTexture)
            {
                renderBatches.Add(new(currentBatchTexture, batchIndexStart, batchIndexCount));
                currentBatchTexture = drawable.Texture;
                batchIndexCount = 0;
                batchIndexStart = (uint)i * 6;
            }

            sceneVertices[objectIndex] = new(drawable.Rectangle.TopLeft, drawable.SubTexture.TexCoordTopLeft, drawable.Tint);
            sceneVertices[objectIndex + 1] = new(drawable.Rectangle.TopRight, drawable.SubTexture.TexCoordTopRight, drawable.Tint);
            sceneVertices[objectIndex + 2] = new(drawable.Rectangle.BottomLeft, drawable.SubTexture.TexCoordBottomLeft, drawable.Tint);
            sceneVertices[objectIndex + 3] = new(drawable.Rectangle.BottomRight, drawable.SubTexture.TexCoordBottomRight, drawable.Tint);

            sceneIndices[triangleIndex] = objectIndex;
            sceneIndices[triangleIndex + 1] = objectIndex + 1;
            sceneIndices[triangleIndex + 2] = objectIndex + 2;

            sceneIndices[triangleIndex + 3] = objectIndex + 1;
            sceneIndices[triangleIndex + 4] = objectIndex + 3;
            sceneIndices[triangleIndex + 5] = objectIndex + 2;

            batchIndexCount += 6;
        }

        // Add the final batch
        if(batchIndexCount > 0)
        {
            RenderBatch batch = new RenderBatch(currentBatchTexture, batchIndexStart, batchIndexCount);
            renderBatches.Add(batch);
        }

        Commands.UpdateBuffer(VertexBuffer, 0, sceneVertices);
        Commands.UpdateBuffer(IndexBuffer, 0, sceneIndices);

        foreach(RenderBatch batch in renderBatches)
        {
            Commands.SetGraphicsResourceSet(0, batch.texture.resourceSet);
            Commands.DrawIndexed(indexCount: batch.indexCount, 1, indexStart: batch.indexStart, 0, 0);
        }

        EndFrame();
    }

    private void EndFrame()
    {
        Commands.End();
        Device.SubmitCommands(Commands);
        Device.SwapBuffers();
    }

    public override void Dispose()
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
