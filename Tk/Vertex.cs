using System.Runtime.InteropServices;

namespace JuraFramework.Tk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Float3 position;
        public Float2 uv;
    }
}
