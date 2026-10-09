// Made by Jura, part of the JuraTK math library

using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE0130

namespace JMath
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Float2 : IEquatable<Float2>
    {
        public float x;
        public float y;

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

        public readonly float SqrMagnitude => x * x + y * y;

        public readonly Float2 Normalized()
        {
            Float2 v = this;
            v.Normalize();
            return v;
        }

        public void Normalize()
        {
            float scale = 1.0f / Magnitude;
            x *= scale;
            y *= scale;
        }

        public static Float2 Zero => new(0, 0);
        public static Float2 One => new(1, 1);
        public static Float2 Up => new(0, 1);
        public static Float2 Right => new(1, 0);

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Float2));

        public static float Distance(Float2 vec1, Float2 vec2) => (float)Math.Sqrt((vec2.x - vec1.x) * (vec2.x - vec1.x) + (vec2.y - vec1.y) * (vec2.y - vec1.y));

        public static float DistanceSquared(Float2 vec1, Float2 vec2) => (vec2.x - vec1.x) * (vec2.x - vec1.x) + (vec2.y - vec1.y) * (vec2.y - vec1.y);

        public static Float2 Normalize(Float2 vec)
        {
            float scale = 1.0f / vec.Magnitude;
            vec.x *= scale;
            vec.y *= scale;
            return vec;
        }

        public static float Dot(Float2 left, Float2 right)
        {
            return left.x * right.x + left.y * right.y;
        }

        public static Float2 Lerp(Float2 a, Float2 b, float t)
        {
            a.x = t * (b.x - a.x) + a.x;
            a.y = t * (b.y - a.y) + a.y;
            return a;
        }

        public static Float2 Barycentric(Float2 a, Float2 b, Float2 c, float u, float v)
        {
            return a + u * (b - a) + v * (c - a);
        }

        public static Float2 operator +(Float2 left, Float2 right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }

        public static Float2 operator -(Float2 left, Float2 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }

        public static Float2 operator -(Float2 vec)
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            return vec;
        }

        public static Float2 operator *(Float2 vec, float scale)
        {
            vec.x *= scale;
            vec.y *= scale;
            return vec;
        }

        public static Float2 operator *(float scale, Float2 vec)
        {
            vec.x *= scale;
            vec.y *= scale;
            return vec;
        }

        public static Float2 operator *(Float2 vec, Float2 scale)
        {
            vec.x *= scale.x;
            vec.y *= scale.y;
            return vec;
        }

        public static Float2 operator /(Float2 vec, float scale)
        {
            vec.x /= scale;
            vec.y /= scale;
            return vec;
        }

        public static bool operator ==(Float2 left, Float2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Float2 left, Float2 right)
        {
            return !left.Equals(right);
        }

        public readonly override string ToString() => $"({x}, {y})";
        public readonly override int GetHashCode() => unchecked((x.GetHashCode() * 397) ^ y.GetHashCode());

        public readonly override bool Equals(object? obj)
        {
            if (obj is not Float2)
            {
                return false;
            }

            return Equals((Float2)obj);
        }

        public readonly bool Equals(Float2 other)
        {
            return
                x == other.x &&
                y == other.y;
        }
    }
}
