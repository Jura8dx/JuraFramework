using System.Runtime.InteropServices;
using Veldrid;

namespace JuraTK
{
    /// <summary>
    /// Container for basic vertex information to be sent to shaders
    /// (32 bytes)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        /// <summary>
        /// Position of this <see cref="Vertex"/>
        /// (8 bytes)
        /// </summary>
        public Float2 position;

        /// <summary>
        /// 0-1 texture coordinate of this <see cref="Vertex"/>
        /// (8 bytes)
        /// </summary>
        public Float2 uv;

        /// <summary>
        /// Color of this <see cref="Vertex"/>
        /// (16 bytes)
        /// </summary>
        public RgbaFloat color;

        /// <summary>
        /// Empty clear-colored vertex with (0, 0) as <see cref="position"/> and <see cref="uv"/>
        /// </summary>
        public static Vertex Empty => new(Float2.Zero, Float2.Zero, RgbaFloat.Clear);

        public Vertex(Float2 position, Float2 uv, RgbaFloat color)
        {
            this.position = position;
            this.uv = uv;
            this.color = color;
        }

        public Vertex(float x, float y, Float2 uv, RgbaFloat color)
        {
            position = new Float2(x, y);
            this.uv = uv;
            this.color = color;
        }

        public Vertex(float x, float y, float u, float v, RgbaFloat color)
        {
            position = new Float2(x, y);
            uv = new Float2(u, v);
            this.color = color;
        }

        public static int SizeInBytes => Marshal.SizeOf(typeof(Vertex));
    }
}
