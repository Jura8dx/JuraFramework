using System;
using System.Runtime.InteropServices;

namespace JuraFramework.Tk
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Size = 12)]
    public struct Float3 : IEquatable<Float3>
    {
        public float x;
        public float y;
        public float z;

        public readonly Float3 xyz => this;
        public readonly Float3 xxx => new Float3(x, x, x);
        public readonly Float3 yyy => new Float3(y, y, y);

        public readonly Float2 xx => new Float2(x, x);
        public readonly Float2 xy => new Float2(x, y);
        public readonly Float2 xz => new Float2(x, z);

        public readonly Float2 yx => new Float2(y, z);
        public readonly Float2 yy => new Float2(y, y);
        public readonly Float2 yz => new Float2(y, z);

        public readonly Float2 zx => new Float2(z, x);
        public readonly Float2 zy => new Float2(z, y);
        public readonly Float2 zz => new Float2(z, z);

        public static Float3 zero => new Float3(0f, 0f, 0f);
        public static Float3 one => new Float3(1f, 1f, 1f);

        public static Float3 up => new Float3(0f, 1f, 0f);
        public static Float3 down => new Float3(0f, -1f, 0f);
        public static Float3 right => new Float3(1f, 0f, 0f);
        public static Float3 left => new Float3(-1f, 0f, 0f);
        public static Float3 forward => new Float3(0f, 0f, 1f);
        public static Float3 backward => new Float3(0f, 0f, -1f);

        public Float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Float3(Float2 value)
        {
            x = value.x;
            y = value.y;
            x = 0f;
        }

        public Float3(float value)
        {
            x = value;
            y = value;
            z = value;
        }

        public readonly float Magnitude => (float)Math.Sqrt(x * x + y * y + z * z);
        public readonly float MagnitudeSqr => x * x + y * y + z * z;

        public Float3 Normalized()
        {
            Float3 v = this;
            v.Normalize();
            return v;
        }

        public void Normalize()
        {
            float scale = 1f / Magnitude;
            x *= scale;
            y *= scale;
            z *= scale;
        }

        #region Operators

        public static Float3 operator +(Float3 lhs, Float3 rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            lhs.z += rhs.z;
            return lhs;
        }

        public static Float3 operator -(Float3 lhs, Float3 rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            lhs.z -= rhs.z;
            return lhs;
        }

        public static Float3 operator *(Float3 lhs, Float3 rhs)
        {
            lhs.x *= rhs.x;
            lhs.y *= rhs.y;
            lhs.z *= rhs.z;
            return lhs;
        }

        public static Float3 operator *(Float3 lhs, float rhs)
        {
            lhs.x *= rhs;
            lhs.y *= rhs;
            lhs.z *= rhs;
            return lhs;
        }

        public static Float3 operator *(float lhs, Float3 rhs)
        {
            rhs.x *= lhs;
            rhs.y *= lhs;
            rhs.z *= lhs;
            return rhs;
        }

        public static bool operator ==(Float3 lhs, Float3 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Float3 lhs, Float3 rhs)
        {
            return !lhs.Equals(rhs);
        }

        #endregion

        public override readonly string ToString()
        {
            return $"({x}, {y})";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Float3) return false;

            return Equals((Float3)obj);
        }

        public bool Equals(Float3 other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z;
        }

        public const int SizeInBytes = 12;
    }
}
