using System;
using System.Runtime.InteropServices;

namespace JMath
{
    /// <summary>
    /// Simple structure representing a two integer XY vector
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Int2 : IEquatable<Int2>
    {
        /// <summary>
        /// The X component of this vector
        /// </summary>
        public int x;

        /// <summary>
        /// The Y component of this vector
        /// </summary>
        public int y;

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Int2(int v)
        {
            this.x = v;
            this.y = v;
        }

        /// <summary>
        /// Gets the length of this vector
        /// </summary>
        public readonly float Magnitude => (float)Math.Sqrt(x * x + y * y);

        /// <summary>
        /// Gets the square length of this vector. This method avoids the expensive square-root calculation, making it more suitable for quick comparisons.
        /// </summary>
        public readonly float SqrMagnitude => x * x + y * y;

        /// <summary>
        /// Short hand for (0, 0)
        /// </summary>
        public static Int2 Zero => new Int2(0, 0);

        /// <summary>
        /// Shorthand for (1, 1)
        /// </summary>
        public static Int2 One => new Int2(1, 1);

        /// <summary>
        /// Shorthand for (0, 1)
        /// </summary>
        public static Int2 Up => new Int2(0, 1);

        /// <summary>
        /// Shorthand for (1, 0)
        /// </summary>
        public static Int2 Right => new Int2(1, 0);

        /// <summary>
        /// Returns the size in bytes of the <see cref="Int2"/> struct (12 bytes)
        /// </summary>
        public static int SizeInBytes => Marshal.SizeOf(typeof(Int2));

        /// <summary>
        /// Compares two vectors for equality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(Int2 lhs, Int2 rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Compares two vectors for inequality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(Int2 lhs, Int2 rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="lhs">The first instance.</param>
        /// <param name="rhs">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Int2 operator +(Int2 lhs, Int2 rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            return lhs;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="lhs">The first instance.</param>
        /// <param name="rhs">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Int2 operator -(Int2 lhs, Int2 rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            return lhs;
        }

        /// <summary>
        /// Negates an instance.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Int2 operator -(Int2 v)
        {
            v.x = -v.x;
            v.y = -v.y;
            return v;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Int2 operator *(Int2 v, float scale)
        {
            v.x *= (int)scale;
            v.y *= (int)scale;
            return v;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="scale">The scalar.</param>
        /// <param name="v">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Int2 operator *(float scale, Int2 v)
        {
            v.x *= (int)scale;
            v.y *= (int)scale;
            return v;
        }

        /// <summary>
        /// Component-wise multiplication between the specified instance by a scale vector.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="v">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static Int2 operator *(Int2 v, Int2 scale)
        {
            v.x *= scale.x;
            v.y *= scale.y;
            return v;
        }

        /// <summary>
        /// Divides an instance by a scalar.
        /// </summary>
        /// <param name="v">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Int2 operator /(Int2 v, float scale)
        {
            v.x /= (int)scale;
            v.y /= (int)scale;
            return v;
        }

        /// <summary>
        /// Returns a formatted string representing this vector
        /// </summary>
        /// <returns></returns>
        public readonly override string ToString()
        {
            return $"({x}, {y})";
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
            if (obj is not Int2)
            {
                return false;
            }

            return this.Equals((Int2)obj);
        }

        /// <summary>Indicates whether the current vector is equal to another vector.</summary>
        /// <param name="other">A vector to compare with this vector.</param>
        /// <returns>true if the current vector is equal to the vector parameter; otherwise, false.</returns>
        public readonly bool Equals(Int2 other)
        {
            return
                x == other.x &&
                y == other.y;
        }
    }
}
