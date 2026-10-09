using JuraFramework.Graphics.Textures;

namespace JuraFramework.Graphics.Rendering;

public struct RenderBatch(AtlasTexture texture, uint indexStart, uint indexCount)
{
    public AtlasTexture texture = texture;
    public uint indexStart = indexStart;
    public uint indexCount = indexCount;
}
