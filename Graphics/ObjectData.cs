using JuraTK;
using System.Runtime.InteropServices;
using Veldrid;

namespace JuraFramework.Graphics
{
    /// <summary>
    /// 32-byte structure containing information about how to render a <see cref="Drawable"/>;
    /// Sent to a shader as a uniform buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ObjectData(Float2 position, Float2 size, string atlas, RgbaFloat color)
    {
        /// <summary>
        /// Position of this <see cref="Drawable"/>
        /// Stride -> 8 bytes
        /// </summary>
        public Float2 position = position;

        /// <summary>
        /// Size delta of this <see cref="Drawable"/>
        /// Stride -> 8 bytes
        /// </summary>
        public Float2 size = size;

        /// <summary>
        /// Name of the texture atlas this object belongs to
        /// </summary>
        public string atlas = atlas;

        /// <summary>
        /// Vertex color of this <see cref="Drawable"/>
        /// Stride -> 16 bytes
        /// </summary>
        public RgbaFloat color = color;

        // 8 + 8 + 16 = 32 bytes
        public static int SizeInBytes => Marshal.SizeOf(typeof(ObjectData));
    }
}
