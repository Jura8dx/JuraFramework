using System;
using System.Runtime.InteropServices;

namespace JuraFramework.Tk
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Float2 : IEquatable<Float2>
    {
        public float x;
        public float y;

        public readonly Float2 xy => this;
        public readonly Float2 yx => new Float2(y, x);
        public readonly Float2 xx => new Float2(x, x);
        public readonly Float2 yy => new Float2(y, y);

        public Float2(float value)
        {
            x = value;
            y = value;
        }

        public Float2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public readonly float Magnitude => (float)Math.Sqrt(x * x + y * y);
        public readonly float MagnitudeSqr => x * x + y * y;

        public Float2 Normalized()
        {
            Float2 v = this;
            v.Normalize();
            return v;
        }

        public void Normalize()
        {
            float scale = 1f / Magnitude;
            x *= scale;
            y *= scale;
        }

        #region Operators

        public static Float2 operator +(Float2 lhs, Float2 rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            return lhs;
        }

        public static Float2 operator -(Float2 lhs, Float2 rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            return lhs;
        }

        public static Float2 operator *(Float2 lhs, Float2 rhs)
        {
            lhs.x *= rhs.x;
            lhs.y *= rhs.y;
            return lhs;
        }

        public static Float2 operator *(Float2 lhs, float rhs)
        {
            lhs.x *= rhs;
            lhs.y *= rhs;
            return lhs;
        }

        public static Float2 operator *(float lhs, Float2 rhs)
        {
            rhs.x *= lhs;
            rhs.y *= lhs;
            return rhs;
        }

        public static bool operator ==(Float2 lhs, Float2 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Float2 lhs, Float2 rhs)
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
                return x.GetHashCode() ^ y.GetHashCode();
            }
        }

        public override bool Equals(object? obj)
        {
            if(obj is not Float2) return false;

            return Equals((Float2)obj);
        }

        public bool Equals(Float2 other)
        {
            return
                x == other.x &&
                y == other.y;
        }
    }
}
