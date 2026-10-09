using System.Collections.Generic;
using JuraFramework.Graphics.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Veldrid;

namespace JuraFramework.Graphics.Textures;

public class AtlasTexture
{
    public Dictionary<string, TextureRegion> subTextures = [];

    public Texture source;
    public TextureView textureView;
    public ResourceSet resourceSet;

    public AtlasTexture(string path, Renderer renderer)
    {
        using Image<Rgba32> image = Image.Load<Rgba32>(path);

        uint width = (uint)image.Width;
        uint height = (uint)image.Height;

        byte[] pixels = new byte[width * height * 4];
        image.CopyPixelDataTo(pixels);

        TextureDescription description = TextureDescription.Texture2D(
            width: width,
            height: height,
            mipLevels: 1,
            arrayLayers: 1,
            format: PixelFormat.R8_G8_B8_A8_UNorm,
            usage: TextureUsage.Sampled
        );

        source = renderer.Factory.CreateTexture(ref description);
        renderer.Device.UpdateTexture(source, pixels, 0, 0, 0, width, height, 1, 0, 0);
        textureView = renderer.Factory.CreateTextureView(source);

        resourceSet = renderer.Factory.CreateResourceSet(new ResourceSetDescription(
            renderer.TextureLayout,
            textureView,
            renderer.TextureSampler
        ));
    }
}
