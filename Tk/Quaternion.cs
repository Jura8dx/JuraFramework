using System;
using System.Runtime.InteropServices;

namespace JuraFramework.Tk
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion
    {
        public Float3 xyz;
        public float w;

        public float x { readonly get => xyz.x; set { xyz.x = value; } }
        public float y { readonly get => xyz.y; set { xyz.y = value; } }
        public float z { readonly get => xyz.z; set { xyz.z = value; } }

        public static Quaternion identity => new Quaternion(0f, 0f, 0f, 1f);

        public Quaternion(Float3 v, float w)
        {
            xyz = v;
            this.w = w;
        }

        public Quaternion(float rx, float ry, float rz)
        {
            rx *= 0.5f;
            ry *= 0.5f;
            rz *= 0.5f;

            float c1 = (float)Math.Cos(rx);
            float c2 = (float)Math.Cos(ry);
            float c3 = (float)Math.Cos(rz);
            float s1 = (float)Math.Sin(rx);
            float s2 = (float)Math.Sin(ry);
            float s3 = (float)Math.Sin(rz);

            w = c1 * c2 * c3 - s1 * s2 * s3;
            xyz.x = s1 * c2 * c3 + c1 * s2 * s3;
            xyz.y = c1 * s2 * c3 - s1 * c2 * s3;
            xyz.z = c1 * c2 * s3 + s1 * s2 * c3;
        }

        public Quaternion(float x, float y, float z, float w)
            : this(new Float3(x, y, z), w)
        { }

        public Quaternion(Float3 eulerAngles)
            : this(eulerAngles.x, eulerAngles.y, eulerAngles.z)
        { }
    }
}
