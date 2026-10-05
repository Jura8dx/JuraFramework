// Made by Jura, part of the JuraTK math library

using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE0130

namespace JuraTK
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Int2 : IEquatable<Int2>
    {
        public int x;
        public int y;

        public Int2(int v)
        {
            x = v;
            y = v;
        }

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public readonly float Magnitude => (float)Math.Sqrt(x * x + y * y);

        public readonly float SqrMagnitude => x * x + y * y;

        public static Float2 Zero => new(0, 0);
        public static Float2 One => new(1, 1);
        public static Float2 Up => new(0, 1);
        public static Float2 Right => new(1, 0);

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Float2));

        public static float Distance(Int2 vec1, Int2 vec2) => (float)Math.Sqrt((vec2.x - vec1.x) * (vec2.x - vec1.x) + (vec2.y - vec1.y) * (vec2.y - vec1.y));

        public static float DistanceSquared(Int2 vec1, Int2 vec2) => (vec2.x - vec1.x) * (vec2.x - vec1.x) + (vec2.y - vec1.y) * (vec2.y - vec1.y);

        public static float Dot(Int2 left, Int2 right)
        {
            return left.x * right.x + left.y * right.y;
        }

        public static Int2 operator +(Int2 left, Int2 right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }

        public static Int2 operator -(Int2 left, Int2 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }

        public static Int2 operator -(Int2 vec)
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            return vec;
        }

        public static Int2 operator *(Int2 vec, int scale)
        {
            vec.x *= scale;
            vec.y *= scale;
            return vec;
        }

        public static Int2 operator *(int scale, Int2 vec)
        {
            vec.x *= scale;
            vec.y *= scale;
            return vec;
        }

        public static Float2 operator *(Int2 vec, float scale)
        {
            return scale * vec;
        }

        public static Float2 operator *(float scale, Int2 vec)
        {
            float xS = vec.x * scale;
            float yS = vec.y * scale;
            return new(xS, yS);
        }

        public static Int2 operator *(Int2 vec, Int2 scale)
        {
            vec.x *= scale.x;
            vec.y *= scale.y;
            return vec;
        }

        public static Float2 operator /(Int2 vec, float scale)
        {
            float xS = vec.x / scale;
            float yS = vec.y / scale;
            return new(xS, yS);
        }

        public static bool operator ==(Int2 left, Int2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Int2 left, Int2 right)
        {
            return !left.Equals(right);
        }

        public readonly override string ToString() => $"({x}, {y})";
        public readonly override int GetHashCode() => unchecked((x.GetHashCode() * 397) ^ y.GetHashCode());

        public readonly override bool Equals(object? obj)
        {
            if (obj is not Int2)
            {
                return false;
            }

            return Equals((Int2)obj);
        }

        public readonly bool Equals(Int2 other)
        {
            return
                x == other.x &&
                y == other.y;
        }
    }
}
