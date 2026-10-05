using JuraFramework.Resources;
using JuraTK;
using Veldrid;

namespace JuraFramework.Graphics
{
    public class DrawData(Drawable source)
    {
        public Drawable Source { get; } = source;

        /// <summary>
        /// Position of this <see cref="Drawable"/>
        /// Stride -> 8 bytes
        /// </summary>
        public Float2 position;

        /// <summary>
        /// Size delta of this <see cref="Drawable"/>
        /// Stride -> 8 bytes
        /// </summary>
        public Float2 size;

        /// <summary>
        /// Name of the texture atlas this object belongs to
        /// </summary>
        public SpriteAtlas atlas;

        /// <summary>
        /// Vertex color of this <see cref="Drawable"/>
        /// Stride -> 16 bytes
        /// </summary>
        public RgbaFloat color;

        public virtual void ApplyState()
        {
            position = Source.Position;
            size = Source.Size;
            color = Source.Tint;
        }
    }
}
