using System;
using System.Runtime.InteropServices;

namespace JMath
{
    /// <summary>
    /// Simple structure representing a three-component (XYZ) vector
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Float3 : IEquatable<Float3>
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

        public Float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Float3(float v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
        }

        /// <summary>
        /// Gets the length of this vector
        /// </summary>
        public readonly float Magnitude => (float)Math.Sqrt(x * x + y * y + z * z);

        /// <summary>
        /// Gets the square length of this vector. This method avoids the expensive square-root calculation, making it more suitable for quick comparisons.
        /// </summary>
        public readonly float SqrMagnitude => x * x + y * y + z * z;

        /// <summary>
        /// Scales this Vec3 to unit length
        /// </summary>
        public void Normalize()
        {
            float scale = 1f / Magnitude;
            x *= scale;
            y *= scale;
            z *= scale;
        }

        /// <summary>
        /// Returns a copy of this vector scaled to unit length
        /// </summary>
        public readonly Float3 Normalized()
        {
            Float3 v = this;
            v.Normalize();
            return v;
        }

        /// <summary>
        /// Short hand for Vec3(0, 0, 0)
        /// </summary>
        public static Float3 Zero => new Float3(0f, 0f, 0f);

        /// <summary>
        /// Shorthand for Vec3(1, 1, 1)
        /// </summary>
        public static Float3 One => new Float3(1f, 1f, 1f);

        /// <summary>
        /// Shorthand for Vec3(0, 1, 0)
        /// </summary>
        public static Float3 Up => new Float3(0f, 1f, 0f);

        /// <summary>
        /// Shorthand for Vec3(1, 0, 0)
        /// </summary>
        public static Float3 Right => new Float3(1f, 0f, 0f);

        /// <summary>
        /// Shorthand for Vec3(0, 0, 1)
        /// </summary>
        public static Float3 Forwards => new Float3(0f, 0f, -1f);

        /// <summary>
        /// Returns the size in bytes of the <see cref="Float3"/> struct (12 bytes)
        /// </summary>
        public static int SizeInBytes => Marshal.SizeOf(typeof(Float3));

        /// <summary>
        /// The distance between two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Distance(Float3 v1, Float3 v2)
        {
            return (float)Math.Sqrt(SqrDistance(v1, v2));
        }

        /// <summary>
        /// The squared distance between two vectors. This method avoids the expensive square-root calculation, making it more suitable for quick comparisons.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float SqrDistance(Float3 v1, Float3 v2)
        {
            return (v2.x - v1.x) * (v2.x - v1.x) + (v2.y - v1.y) * (v2.y - v1.y) + (v2.z - v1.z) * (v2.z - v1.z);
        }

        /// <summary>
        /// Calculates the dot product of two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Dot(Float3 v1, Float3 v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        /// <summary>
        /// Calculates the cross product of two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Float3 Cross(Float3 v1, Float3 v2)
        {
            Float3 result;
            result.x = v1.y * v2.z - v1.z * v2.y;
            result.y = v1.z * v2.x - v1.x * v2.z;
            result.z = v1.x * v2.y - v1.y * v2.x;
            return result;
        }

        /// <summary>
        /// Returns a new vector that is a linear blend between A and B
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns>A when T = 0, and B when T = 1</returns>
        public static Float3 Lerp(Float3 a, Float3 b, float t)
        {
            Float3 result;
            result.x = t * (b.x - a.x) + a.x;
            result.y = t * (b.y - a.y) + a.y;
            result.z = t * (b.z - a.z) + a.z;
            return result;
        }

        /// <summary>
        /// Compares two vectors for equality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(Float3 lhs, Float3 rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Compares two vectors for inequality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(Float3 lhs, Float3 rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="lhs">The first instance.</param>
        /// <param name="rhs">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator +(Float3 lhs, Float3 rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            lhs.z += rhs.z;
            return lhs;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="lhs">The first instance.</param>
        /// <param name="rhs">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator -(Float3 lhs, Float3 rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            lhs.z -= rhs.z;
            return lhs;
        }

        /// <summary>
        /// Negates an instance.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator -(Float3 v)
        {
            v.x = -v.x;
            v.y = -v.y;
            v.z = -v.z;
            return v;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator *(Float3 v, float scale)
        {
            v.x *= scale;
            v.y *= scale;
            v.z *= scale;
            return v;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="scale">The scalar.</param>
        /// <param name="v">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator *(float scale, Float3 v)
        {
            v.x *= scale;
            v.y *= scale;
            v.z *= scale;
            return v;
        }

        /// <summary>
        /// Component-wise multiplication between the specified instance by a scale vector.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="v">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static Float3 operator *(Float3 v, Float3 scale)
        {
            v.x *= scale.x;
            v.y *= scale.y;
            v.z *= scale.z;
            return v;
        }

        /// <summary>
        /// Divides an instance by a scalar.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator /(Float3 v, float scale)
        {
            v.x /= scale;
            v.y /= scale;
            v.z /= scale;
            return v;
        }

        /// <summary>
        /// Returns a formatted string representing this vector
        /// </summary>
        /// <returns></returns>
        public readonly override string ToString()
        {
            return $"({x}, {y}, {z})";
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
            if (obj is not Float3)
            {
                return false;
            }

            return this.Equals((Float3)obj);
        }

        /// <summary>Indicates whether the current vector is equal to another vector.</summary>
        /// <param name="other">A vector to compare with this vector.</param>
        /// <returns>true if the current vector is equal to the vector parameter; otherwise, false.</returns>
        public readonly bool Equals(Float3 other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z;
        }
    }
}
