using System;
using System.Collections.Generic;
using JuraTK;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Veldrid;


namespace JuraFramework.Resources
{
    public struct AtlasEntry(Float2 bl, Float2 tl, Float2 tr, Float2 br)
    {
        /// <summary>
        /// Bottom left corner of the rectangle
        /// </summary>
        public Float2 UV_00 = bl;

        /// <summary>
        /// Top left corner of the rectangle
        /// </summary>
        public Float2 UV_01 = tl;

        /// <summary>
        /// Top right corner of the rectangle
        /// </summary>
        public Float2 UV_11 = tr;

        /// <summary>
        /// Bottom right corner of the rectangle
        /// </summary>
        public Float2 UV_10 = br;
    }

    public class SpriteAtlas : IDisposable
    {
        /// <summary>
        /// List of sprite entries in this texture atlas
        /// </summary>
        public Dictionary<string, AtlasEntry> sprites = [];

        /// <summary>
        /// Source texture atlas
        /// </summary>
        public Texture texture;
        public TextureView textureView;
        public ResourceLayout resourceLayout;
        public ResourceSet resourceSet;
        public Sampler sampler;

        public SpriteAtlas(Image<Rgba32> image, ResourceFactory factory)
        {
            uint width = (uint)image.Width;
            uint height = (uint)image.Height;

            byte[] pixels = new byte[width * height * 4];
            image.CopyPixelDataTo(pixels);

            TextureDescription textureDescription = TextureDescription.Texture2D(
                width: width,
                height: height,
                mipLevels: 1,
                arrayLayers: 1,
                format: PixelFormat.R8_G8_B8_A8_UNorm,
                usage: TextureUsage.Sampled
            );

            texture = factory.CreateTexture(ref textureDescription);
            textureView = factory.CreateTextureView(texture);
            sampler = factory.CreateSampler(SamplerDescription.Linear);

            // ResourceLayout?
            // ResourceSet?

            // This is now where I am lost.
            // I want to create everything that this sprite atlas needs right here.
            // I am not sure how to decide what to create here, and what to create per-draw call in the renderer
        }

        public void AddEntry(string name, AtlasEntry entry)
        {
            sprites.Add(name, entry);
        }

        public void Dispose()
        {
            resourceLayout.Dispose();
            textureView.Dispose();
            texture.Dispose();
            sampler.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
