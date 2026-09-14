using System.Runtime.InteropServices;

namespace JuraFramework.Tk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex(Float3 position, Float2 uv)
    {
        public Float3 position = position;
        public Float2 uv = uv;

        public const int SizeInBytes = 20;
    }
}
