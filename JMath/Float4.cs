using System;
using System.Runtime.InteropServices;

namespace JMath
{
    /// <summary>
    /// Simple structure representing a four-component (XYZW) vector
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Float4 : IEquatable<Float4>
    {
        /// <summary>
        /// The X component of this vector
        /// </summary>
        public float x;

        /// <summary>
        /// The Y component of this vector
        /// </summary>
        public float y;

        /// <summary>
        /// The Z component of this vector
        /// </summary>
        public float z;

        /// <summary>
        /// The W component of this vector
        /// </summary>
        public float w;

        public Float4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Float4(float v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
        }

        /// <summary>
        /// Gets the length of this vector
        /// </summary>
        public readonly float Magnitude => (float)Math.Sqrt(x * x + y * y + z * z + w * w);

        /// <summary>
        /// Gets the square length of this vector. This method avoids the expensive square-root calculation, making it more suitable for quick comparisons.
        /// </summary>
        public readonly float SqrMagnitude => x * x + y * y + z * z + w * w;

        /// <summary>
        /// Scales this Vec3 to unit length
        /// </summary>
        public void Normalize()
        {
            float scale = 1f / Magnitude;
            x *= scale;
            y *= scale;
            z *= scale;
            w *= scale;
        }

        /// <summary>
        /// Returns a copy of this vector scaled to unit length
        /// </summary>
        public readonly Float4 Normalized()
        {
            Float4 v = this;
            v.Normalize();
            return v;
        }

        /// <summary>
        /// Short hand for Vec3(0, 0, 0)
        /// </summary>
        public static Float4 Zero => new Float4(0f, 0f, 0f, 0f);

        /// <summary>
        /// Shorthand for Vec3(1, 1, 1)
        /// </summary>
        public static Float4 One => new Float4(1f, 1f, 1f, 0f);

        /// <summary>
        /// Returns the size in bytes of the <see cref="Float4"/> struct (16 bytes)
        /// </summary>
        public static int SizeInBytes => Marshal.SizeOf(typeof(Float4));

        /// <summary>
        /// Returns a new vector that is a linear blend between A and B
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns>A when T = 0, and B when T = 1</returns>
        public static Float4 Lerp(Float4 a, Float4 b, float t)
        {
            Float4 result;
            result.x = t * (b.x - a.x) + a.x;
            result.y = t * (b.y - a.y) + a.y;
            result.z = t * (b.z - a.z) + a.z;
            result.w = t * (b.w - a.w) + a.w;
            return result;
        }

        /// <summary>
        /// Compares two vectors for equality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(Float4 lhs, Float4 rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Compares two vectors for inequality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(Float4 lhs, Float4 rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="lhs">The first instance.</param>
        /// <param name="rhs">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator +(Float4 lhs, Float4 rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            lhs.z += rhs.z;
            lhs.w += rhs.w;
            return lhs;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="lhs">The first instance.</param>
        /// <param name="rhs">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator -(Float4 lhs, Float4 rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            lhs.z -= rhs.z;
            lhs.w -= rhs.w;
            return lhs;
        }

        /// <summary>
        /// Negates an instance.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator -(Float4 v)
        {
            v.x = -v.x;
            v.y = -v.y;
            v.z = -v.z;
            v.w = -v.w;
            return v;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator *(Float4 v, float scale)
        {
            v.x *= scale;
            v.y *= scale;
            v.z *= scale;
            v.w *= scale;
            return v;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="scale">The scalar.</param>
        /// <param name="v">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator *(float scale, Float4 v)
        {
            v.x *= scale;
            v.y *= scale;
            v.z *= scale;
            v.w *= scale;
            return v;
        }

        /// <summary>
        /// Component-wise multiplication between the specified instance by a scale vector.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="v">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static Float4 operator *(Float4 v, Float4 scale)
        {
            v.x *= scale.x;
            v.y *= scale.y;
            v.z *= scale.z;
            v.w *= scale.w;
            return v;
        }

        /// <summary>
        /// Divides an instance by a scalar.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator /(Float4 v, float scale)
        {
            v.x /= scale;
            v.y /= scale;
            v.z /= scale;
            v.w /= scale;
            return v;
        }

        /// <summary>
        /// Returns a formatted string representing this vector
        /// </summary>
        /// <returns></returns>
        public readonly override string ToString()
        {
            return $"({x}, {y}, {z}, {w})";
        }

        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A System.Int32 containing the unique hashcode for this instance.</returns>
        public readonly override int GetHashCode()
        {
            unchecked
            {
                int hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                hashCode = (hashCode * 397) ^ w.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public readonly override bool Equals(object? obj)
        {
            if (obj is not Float4)
            {
                return false;
            }

            return this.Equals((Float4)obj);
        }

        /// <summary>Indicates whether the current vector is equal to another vector.</summary>
        /// <param name="other">A vector to compare with this vector.</param>
        /// <returns>true if the current vector is equal to the vector parameter; otherwise, false.</returns>
        public readonly bool Equals(Float4 other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w;
        }
    }
}
