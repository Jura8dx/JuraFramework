using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace JuraTK
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Float4 : IEquatable<Float4>
    {
        public float x;
        public float y;
        public float z;
        public float w;

        /// <summary>
        /// Defines a unit-length Vector4 that points towards the X-axis.
        /// </summary>
        public static readonly Float4 UnitX = new Float4(1, 0, 0, 0);

        /// <summary>
        /// Defines a unit-length Vector4 that points towards the Y-axis.
        /// </summary>
        public static readonly Float4 UnitY = new Float4(0, 1, 0, 0);

        /// <summary>
        /// Defines a unit-length Vector4 that points towards the Z-axis.
        /// </summary>
        public static readonly Float4 UnitZ = new Float4(0, 0, 1, 0);

        /// <summary>
        /// Defines a unit-length Vector4 that points towards the W-axis.
        /// </summary>
        public static readonly Float4 UnitW = new Float4(0, 0, 0, 1);

        /// <summary>
        /// Defines a zero-length Vector4.
        /// </summary>
        public static readonly Float4 Zero = new Float4(0, 0, 0, 0);

        /// <summary>
        /// Defines an instance with all components set to 1.
        /// </summary>
        public static readonly Float4 One = new Float4(1, 1, 1, 1);

        /// <summary>
        /// Defines the size of the Vector4 struct in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Marshal.SizeOf(new Float4());

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="value">The value that will initialize this instance.</param>
        public Float4(float value)
        {
            x = value;
            y = value;
            z = value;
            w = value;
        }

        /// <summary>
        /// Constructs a new Vector4.
        /// </summary>
        /// <param name="x">The x component of the Vector4.</param>
        /// <param name="y">The y component of the Vector4.</param>
        /// <param name="z">The z component of the Vector4.</param>
        /// <param name="w">The w component of the Vector4.</param>
        public Float4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a new Vector4 from the given Vector2.
        /// </summary>
        /// <param name="v">The Vector2 to copy components from.</param>
        public Float4(Float2 v)
        {
            x = v.x;
            y = v.y;
            z = 0.0f;
            w = 0.0f;
        }

        /// <summary>
        /// Constructs a new Vector4 from the given Vector3.
        /// The w component is initialized to 0.
        /// </summary>
        /// <param name="v">The Vector3 to copy components from.</param>
        /// <remarks><seealso cref="Float4(Float3, float)"/></remarks>
        public Float4(Float3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            w = 0.0f;
        }

        /// <summary>
        /// Constructs a new Vector4 from the specified Vector3 and w component.
        /// </summary>
        /// <param name="v">The Vector3 to copy components from.</param>
        /// <param name="w">The w component of the new Vector4.</param>
        public Float4(Float3 v, float w)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a new Vector4 from the given Vector4.
        /// </summary>
        /// <param name="v">The Vector4 to copy components from.</param>
        public Float4(Float4 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            w = v.w;
        }

        /// <summary>
        /// Gets or sets the value at the index of the Vector.
        /// </summary>
        public float this[int index] {
            get{
                if (index == 0)
                {
                    return x;
                }
                else if (index == 1)
                {
                    return y;
                }
                else if (index == 2)
                {
                    return z;
                }
                else if (index == 3)
                {
                    return w;
                }
                throw new IndexOutOfRangeException("You tried to access this vector at index: " + index);
            } set{
                if (index == 0)
                {
                    x = value;
                }
                else if (index == 1)
                {
                    y = value;
                }
                else if (index == 2)
                {
                    z = value;
                }
                else if (index == 3)
                {
                    w = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("You tried to set this vector at index: " + index);
                }
            }
        }

        /// <summary>
        /// Gets the length (magnitude) of the vector.
        /// </summary>
        /// <see cref="LengthFast"/>
        /// <seealso cref="SqrMagnitude"/>
        public float Magnitude
        {
            get
            {
                return (float)System.Math.Sqrt(x * x + y * y + z * z + w * w);
            }
        }

        /// <summary>
        /// Gets the square of the vector length (magnitude).
        /// </summary>
        /// <remarks>
        /// This property avoids the costly square root operation required by the Length property. This makes it more suitable
        /// for comparisons.
        /// </remarks>
        /// <see cref="Magnitude"/>
        /// <seealso cref="LengthFast"/>
        public float SqrMagnitude
        {
            get
            {
                return x * x + y * y + z * z + w * w;
            }
        }

        /// <summary>
        /// Returns a copy of the Vector4 scaled to unit length.
        /// </summary>
        public Float4 Normalized()
        {
            Float4 v = this;
            v.Normalize();
            return v;
        }

        /// <summary>
        /// Scales the Vector4 to unit length.
        /// </summary>
        public void Normalize()
        {
            float scale = 1.0f / this.Magnitude;
            x *= scale;
            y *= scale;
            z *= scale;
            w *= scale;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        /// <returns>Result of operation.</returns>
        public static Float4 Add(Float4 a, Float4 b)
        {
            Add(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        /// <param name="result">Result of operation.</param>
        public static void Add(ref Float4 a, ref Float4 b, out Float4 result)
        {
            result.x = a.x + b.x;
            result.y = a.y + b.y;
            result.z = a.z + b.z;
            result.w = a.w + b.w;
        }

        /// <summary>
        /// Subtract one Vector from another
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Result of subtraction</returns>
        public static Float4 Subtract(Float4 a, Float4 b)
        {
            Subtract(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Subtract one Vector from another
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <param name="result">Result of subtraction</param>
        public static void Subtract(ref Float4 a, ref Float4 b, out Float4 result)
        {
            result.x = a.x - b.x;
            result.y = a.y - b.y;
            result.z = a.z - b.z;
            result.w = a.w - b.w;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Float4 Multiply(Float4 vector, float scale)
        {
            Multiply(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Multiply(ref Float4 vector, float scale, out Float4 result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
            result.w = vector.w * scale;
        }

        /// <summary>
        /// Multiplies a vector by the components a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Float4 Multiply(Float4 vector, Float4 scale)
        {
            Multiply(ref vector, ref scale, out vector);
            return vector;
        }

        /// <summary>
        /// Multiplies a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Multiply(ref Float4 vector, ref Float4 scale, out Float4 result)
        {
            result.x = vector.x * scale.x;
            result.y = vector.y * scale.y;
            result.z = vector.z * scale.z;
            result.w = vector.w * scale.w;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Float4 Divide(Float4 vector, float scale)
        {
            Divide(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Divide(ref Float4 vector, float scale, out Float4 result)
        {
            result.x = vector.x / scale;
            result.y = vector.y / scale;
            result.z = vector.z / scale;
            result.w = vector.w / scale;
        }

        /// <summary>
        /// Divides a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Float4 Divide(Float4 vector, Float4 scale)
        {
            Divide(ref vector, ref scale, out vector);
            return vector;
        }

        /// <summary>
        /// Divide a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Divide(ref Float4 vector, ref Float4 scale, out Float4 result)
        {
            result.x = vector.x / scale.x;
            result.y = vector.y / scale.y;
            result.z = vector.z / scale.z;
            result.w = vector.w / scale.w;
        }

        /// <summary>
        /// Calculate the component-wise minimum of two vectors
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>The component-wise minimum</returns>
        [Obsolete("Use ComponentMin() instead.")]
        public static Float4 Min(Float4 a, Float4 b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            a.w = a.w < b.w ? a.w : b.w;
            return a;
        }

        /// <summary>
        /// Calculate the component-wise minimum of two vectors
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <param name="result">The component-wise minimum</param>
        [Obsolete("Use ComponentMin() instead.")]
        public static void Min(ref Float4 a, ref Float4 b, out Float4 result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
            result.w = a.w < b.w ? a.w : b.w;
        }

        /// <summary>
        /// Calculate the component-wise maximum of two vectors
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>The component-wise maximum</returns>
        [Obsolete("Use ComponentMax() instead.")]
        public static Float4 Max(Float4 a, Float4 b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            a.w = a.w > b.w ? a.w : b.w;
            return a;
        }

        /// <summary>
        /// Calculate the component-wise maximum of two vectors
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <param name="result">The component-wise maximum</param>
        [Obsolete("Use ComponentMax() instead.")]
        public static void Max(ref Float4 a, ref Float4 b, out Float4 result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
            result.w = a.w > b.w ? a.w : b.w;
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>The component-wise minimum</returns>
        public static Float4 ComponentMin(Float4 a, Float4 b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            a.w = a.w < b.w ? a.w : b.w;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <param name="result">The component-wise minimum</param>
        public static void ComponentMin(ref Float4 a, ref Float4 b, out Float4 result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
            result.w = a.w < b.w ? a.w : b.w;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>The component-wise maximum</returns>
        public static Float4 ComponentMax(Float4 a, Float4 b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            a.w = a.w > b.w ? a.w : b.w;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <param name="result">The component-wise maximum</param>
        public static void ComponentMax(ref Float4 a, ref Float4 b, out Float4 result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
            result.w = a.w > b.w ? a.w : b.w;
        }

        /// <summary>
        /// Returns the Vector4 with the minimum magnitude. If the magnitudes are equal, the second vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>The minimum Vector4</returns>
        public static Float4 MagnitudeMin(Float4 left, Float4 right)
        {
            return left.SqrMagnitude < right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Returns the Vector4 with the minimum magnitude. If the magnitudes are equal, the second vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <param name="result">The magnitude-wise minimum</param>
        /// <returns>The minimum Vector4</returns>
        public static void MagnitudeMin(ref Float4 left, ref Float4 right, out Float4 result)
        {
            result = left.SqrMagnitude < right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Returns the Vector4 with the maximum magnitude. If the magnitudes are equal, the first vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>The maximum Vector4</returns>
        public static Float4 MagnitudeMax(Float4 left, Float4 right)
        {
            return left.SqrMagnitude >= right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Returns the Vector4 with the maximum magnitude. If the magnitudes are equal, the first vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <param name="result">The magnitude-wise maximum</param>
        /// <returns>The maximum Vector4</returns>
        public static void MagnitudeMax(ref Float4 left, ref Float4 right, out Float4 result)
        {
            result = left.SqrMagnitude >= right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Clamp a vector to the given minimum and maximum vectors
        /// </summary>
        /// <param name="vec">Input vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum vector</param>
        /// <returns>The clamped vector</returns>
        public static Float4 Clamp(Float4 vec, Float4 min, Float4 max)
        {
            vec.x = vec.x < min.x ? min.x : vec.x > max.x ? max.x : vec.x;
            vec.y = vec.y < min.y ? min.y : vec.y > max.y ? max.y : vec.y;
            vec.z = vec.z < min.z ? min.z : vec.z > max.z ? max.z : vec.z;
            vec.w = vec.w < min.w ? min.w : vec.w > max.w ? max.w : vec.w;
            return vec;
        }

        /// <summary>
        /// Clamp a vector to the given minimum and maximum vectors
        /// </summary>
        /// <param name="vec">Input vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum vector</param>
        /// <param name="result">The clamped vector</param>
        public static void Clamp(ref Float4 vec, ref Float4 min, ref Float4 max, out Float4 result)
        {
            result.x = vec.x < min.x ? min.x : vec.x > max.x ? max.x : vec.x;
            result.y = vec.y < min.y ? min.y : vec.y > max.y ? max.y : vec.y;
            result.z = vec.z < min.z ? min.z : vec.z > max.z ? max.z : vec.z;
            result.w = vec.w < min.w ? min.w : vec.w > max.w ? max.w : vec.w;
        }

        /// <summary>
        /// Scale a vector to unit length
        /// </summary>
        /// <param name="vec">The input vector</param>
        /// <returns>The normalized vector</returns>
        public static Float4 Normalize(Float4 vec)
        {
            float scale = 1.0f / vec.Magnitude;
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            vec.w *= scale;
            return vec;
        }

        /// <summary>
        /// Scale a vector to unit length
        /// </summary>
        /// <param name="vec">The input vector</param>
        /// <param name="result">The normalized vector</param>
        public static void Normalize(ref Float4 vec, out Float4 result)
        {
            float scale = 1.0f / vec.Magnitude;
            result.x = vec.x * scale;
            result.y = vec.y * scale;
            result.z = vec.z * scale;
            result.w = vec.w * scale;
        }

        /// <summary>
        /// Calculate the dot product of two vectors
        /// </summary>
        /// <param name="left">First operand</param>
        /// <param name="right">Second operand</param>
        /// <returns>The dot product of the two inputs</returns>
        public static float Dot(Float4 left, Float4 right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
        }

        /// <summary>
        /// Calculate the dot product of two vectors
        /// </summary>
        /// <param name="left">First operand</param>
        /// <param name="right">Second operand</param>
        /// <param name="result">The dot product of the two inputs</param>
        public static void Dot(ref Float4 left, ref Float4 right, out float result)
        {
            result = left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
        }

        /// <summary>
        /// Returns a new Vector that is the linear blend of the 2 given Vectors
        /// </summary>
        /// <param name="a">First input vector</param>
        /// <param name="b">Second input vector</param>
        /// <param name="blend">The blend factor. a when blend=0, b when blend=1.</param>
        /// <returns>a when blend=0, b when blend=1, and a linear combination otherwise</returns>
        public static Float4 Lerp(Float4 a, Float4 b, float blend)
        {
            a.x = blend * (b.x - a.x) + a.x;
            a.y = blend * (b.y - a.y) + a.y;
            a.z = blend * (b.z - a.z) + a.z;
            a.w = blend * (b.w - a.w) + a.w;
            return a;
        }

        /// <summary>
        /// Returns a new Vector that is the linear blend of the 2 given Vectors
        /// </summary>
        /// <param name="a">First input vector</param>
        /// <param name="b">Second input vector</param>
        /// <param name="blend">The blend factor. a when blend=0, b when blend=1.</param>
        /// <param name="result">a when blend=0, b when blend=1, and a linear combination otherwise</param>
        public static void Lerp(ref Float4 a, ref Float4 b, float blend, out Float4 result)
        {
            result.x = blend * (b.x - a.x) + a.x;
            result.y = blend * (b.y - a.y) + a.y;
            result.z = blend * (b.z - a.z) + a.z;
            result.w = blend * (b.w - a.w) + a.w;
        }

        /// <summary>
        /// Interpolate 3 Vectors using Barycentric coordinates
        /// </summary>
        /// <param name="a">First input Vector</param>
        /// <param name="b">Second input Vector</param>
        /// <param name="c">Third input Vector</param>
        /// <param name="u">First Barycentric Coordinate</param>
        /// <param name="v">Second Barycentric Coordinate</param>
        /// <returns>a when u=v=0, b when u=1,v=0, c when u=0,v=1, and a linear combination of a,b,c otherwise</returns>
        public static Float4 BaryCentric(Float4 a, Float4 b, Float4 c, float u, float v)
        {
            return a + u * (b - a) + v * (c - a);
        }

        /// <summary>Interpolate 3 Vectors using Barycentric coordinates</summary>
        /// <param name="a">First input Vector.</param>
        /// <param name="b">Second input Vector.</param>
        /// <param name="c">Third input Vector.</param>
        /// <param name="u">First Barycentric Coordinate.</param>
        /// <param name="v">Second Barycentric Coordinate.</param>
        /// <param name="result">Output Vector. a when u=v=0, b when u=1,v=0, c when u=0,v=1, and a linear combination of a,b,c otherwise</param>
        public static void BaryCentric(ref Float4 a, ref Float4 b, ref Float4 c, float u, float v, out Float4 result)
        {
            result = a; // copy

            Float4 temp = b; // copy
            Subtract(ref temp, ref a, out temp);
            Multiply(ref temp, u, out temp);
            Add(ref result, ref temp, out result);

            temp = c; // copy
            Subtract(ref temp, ref a, out temp);
            Multiply(ref temp, v, out temp);
            Add(ref result, ref temp, out result);
        }

        /// <summary>Transform a Vector by the given Matrix</summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed vector</returns>
        public static Float4 Transform(Float4 vec, Matrix4x4 mat)
        {
            Float4 result;
            Transform(ref vec, ref mat, out result);
            return result;
        }

        /// <summary>Transform a Vector by the given Matrix</summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <param name="result">The transformed vector</param>
        public static void Transform(ref Float4 vec, ref Matrix4x4 mat, out Float4 result)
        {
            result = new Float4(
                vec.x * mat.Row0.x + vec.y * mat.Row1.x + vec.z * mat.Row2.x + vec.w * mat.Row3.x,
                vec.x * mat.Row0.y + vec.y * mat.Row1.y + vec.z * mat.Row2.y + vec.w * mat.Row3.y,
                vec.x * mat.Row0.z + vec.y * mat.Row1.z + vec.z * mat.Row2.z + vec.w * mat.Row3.z,
                vec.x * mat.Row0.w + vec.y * mat.Row1.w + vec.z * mat.Row2.w + vec.w * mat.Row3.w);
        }

        /// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="vec">The vector to transform.</param>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <returns>The result of the operation.</returns>
        public static Float4 Transform(Float4 vec, Quaternion quat)
        {
            Float4 result;
            Transform(ref vec, ref quat, out result);
            return result;
        }

        /// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="vec">The vector to transform.</param>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <param name="result">The result of the operation.</param>
        public static void Transform(ref Float4 vec, ref Quaternion quat, out Float4 result)
        {
            Quaternion v = new Quaternion(vec.x, vec.y, vec.z, vec.w), i, t;
            Quaternion.Invert(ref quat, out i);
            Quaternion.Multiply(ref quat, ref v, out t);
            Quaternion.Multiply(ref t, ref i, out v);

            result.x = v.X;
            result.y = v.Y;
            result.z = v.Z;
            result.w = v.W;
        }

        /// <summary>Transform a Vector by the given Matrix using right-handed notation</summary>
        /// <param name="mat">The desired transformation</param>
        /// <param name="vec">The vector to transform</param>
        public static Float4 Transform(Matrix4x4 mat, Float4 vec)
        {
            Float4 result;
            Transform(ref mat, ref vec, out result);
            return result;
        }

        /// <summary>Transform a Vector by the given Matrix using right-handed notation</summary>
        /// <param name="mat">The desired transformation</param>
        /// <param name="vec">The vector to transform</param>
        /// <param name="result">The transformed vector</param>
        public static void Transform(ref Matrix4x4 mat, ref Float4 vec, out Float4 result)
        {
            result = new Float4(
                mat.Row0.x * vec.x + mat.Row0.y * vec.y + mat.Row0.z * vec.z + mat.Row0.w * vec.w,
                mat.Row1.x * vec.x + mat.Row1.y * vec.y + mat.Row1.z * vec.z + mat.Row1.w * vec.w,
                mat.Row2.x * vec.x + mat.Row2.y * vec.y + mat.Row2.z * vec.z + mat.Row2.w * vec.w,
                mat.Row3.x * vec.x + mat.Row3.y * vec.y + mat.Row3.z * vec.z + mat.Row3.w * vec.w);
        }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the X and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Xy { get { return new Float2(x, y); } set { x = value.x; y = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the X and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Xz { get { return new Float2(x, z); } set { x = value.x; z = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the X and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Xw { get { return new Float2(x, w); } set { x = value.x; w = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the Y and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Yx { get { return new Float2(y, x); } set { y = value.x; x = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the Y and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Yz { get { return new Float2(y, z); } set { y = value.x; z = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the Y and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Yw { get { return new Float2(y, w); } set { y = value.x; w = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the Z and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Zx { get { return new Float2(z, x); } set { z = value.x; x = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the Z and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Zy { get { return new Float2(z, y); } set { z = value.x; y = value.y; } }

        /// <summary>
        /// Gets an osuTK.Vector2 with the Z and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Zw { get { return new Float2(z, w); } set { z = value.x; w = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the W and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Wx { get { return new Float2(w, x); } set { w = value.x; x = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the W and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Wy { get { return new Float2(w, y); } set { w = value.x; y = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector2 with the W and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float2 Wz { get { return new Float2(w, z); } set { w = value.x; z = value.y; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the X, Y, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Xyz { get { return new Float3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the X, Y, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Xyw { get { return new Float3(x, y, w); } set { x = value.x; y = value.y; w = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the X, Z, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Xzy { get { return new Float3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the X, Z, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Xzw { get { return new Float3(x, z, w); } set { x = value.x; z = value.y; w = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the X, W, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Xwy { get { return new Float3(x, w, y); } set { x = value.x; w = value.y; y = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the X, W, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Xwz { get { return new Float3(x, w, z); } set { x = value.x; w = value.y; z = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Y, X, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Yxz { get { return new Float3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Y, X, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Yxw { get { return new Float3(y, x, w); } set { y = value.x; x = value.y; w = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Y, Z, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Yzx { get { return new Float3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Y, Z, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Yzw { get { return new Float3(y, z, w); } set { y = value.x; z = value.y; w = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Y, W, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Ywx { get { return new Float3(y, w, x); } set { y = value.x; w = value.y; x = value.z; } }

        /// <summary>
        /// Gets an osuTK.Vector3 with the Y, W, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Ywz { get { return new Float3(y, w, z); } set { y = value.x; w = value.y; z = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Z, X, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Zxy { get { return new Float3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Z, X, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Zxw { get { return new Float3(z, x, w); } set { z = value.x; x = value.y; w = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Z, Y, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Zyx { get { return new Float3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Z, Y, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Zyw { get { return new Float3(z, y, w); } set { z = value.x; y = value.y; w = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Z, W, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Zwx { get { return new Float3(z, w, x); } set { z = value.x; w = value.y; x = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Z, W, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Zwy { get { return new Float3(z, w, y); } set { z = value.x; w = value.y; y = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the W, X, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Wxy { get { return new Float3(w, x, y); } set { w = value.x; x = value.y; y = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the W, X, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Wxz { get { return new Float3(w, x, z); } set { w = value.x; x = value.y; z = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the W, Y, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Wyx { get { return new Float3(w, y, x); } set { w = value.x; y = value.y; x = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the W, Y, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Wyz { get { return new Float3(w, y, z); } set { w = value.x; y = value.y; z = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the W, Z, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Wzx { get { return new Float3(w, z, x); } set { w = value.x; z = value.y; x = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the W, Z, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Wzy { get { return new Float3(w, z, y); } set { w = value.x; z = value.y; y = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the X, Y, W, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Xywz { get { return new Float4(x, y, w, z); } set { x = value.x; y = value.y; w = value.z; z = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the X, Z, Y, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Xzyw { get { return new Float4(x, z, y, w); } set { x = value.x; z = value.y; y = value.z; w = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the X, Z, W, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Xzwy { get { return new Float4(x, z, w, y); } set { x = value.x; z = value.y; w = value.z; y = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the X, W, Y, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Xwyz { get { return new Float4(x, w, y, z); } set { x = value.x; w = value.y; y = value.z; z = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the X, W, Z, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Xwzy { get { return new Float4(x, w, z, y); } set { x = value.x; w = value.y; z = value.z; y = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Y, X, Z, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Yxzw { get { return new Float4(y, x, z, w); } set { y = value.x; x = value.y; z = value.z; w = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Y, X, W, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Yxwz { get { return new Float4(y, x, w, z); } set { y = value.x; x = value.y; w = value.z; z = value.w; } }

        /// <summary>
        /// Gets an osuTK.Vector4 with the Y, Y, Z, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Yyzw { get { return new Float4(y, y, z, w); } set { x = value.x; y = value.y; z = value.z; w = value.w; } }

        /// <summary>
        /// Gets an osuTK.Vector4 with the Y, Y, W, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Yywz { get { return new Float4(y, y, w, z); } set { x = value.x; y = value.y; w = value.z; z = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Y, Z, X, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Yzxw { get { return new Float4(y, z, x, w); } set { y = value.x; z = value.y; x = value.z; w = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Y, Z, W, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Yzwx { get { return new Float4(y, z, w, x); } set { y = value.x; z = value.y; w = value.z; x = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Y, W, X, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Ywxz { get { return new Float4(y, w, x, z); } set { y = value.x; w = value.y; x = value.z; z = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Y, W, Z, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Ywzx { get { return new Float4(y, w, z, x); } set { y = value.x; w = value.y; z = value.z; x = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Z, X, Y, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Zxyw { get { return new Float4(z, x, y, w); } set { z = value.x; x = value.y; y = value.z; w = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Z, X, W, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Zxwy { get { return new Float4(z, x, w, y); } set { z = value.x; x = value.y; w = value.z; y = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Z, Y, X, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Zyxw { get { return new Float4(z, y, x, w); } set { z = value.x; y = value.y; x = value.z; w = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Z, Y, W, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Zywx { get { return new Float4(z, y, w, x); } set { z = value.x; y = value.y; w = value.z; x = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Z, W, X, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Zwxy { get { return new Float4(z, w, x, y); } set { z = value.x; w = value.y; x = value.z; y = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the Z, W, Y, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Zwyx { get { return new Float4(z, w, y, x); } set { z = value.x; w = value.y; y = value.z; x = value.w; } }

        /// <summary>
        /// Gets an osuTK.Vector4 with the Z, W, Z, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Zwzy { get { return new Float4(z, w, z, y); } set { x = value.x; w = value.y; z = value.z; y = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the W, X, Y, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Wxyz { get { return new Float4(w, x, y, z); } set { w = value.x; x = value.y; y = value.z; z = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the W, X, Z, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Wxzy { get { return new Float4(w, x, z, y); } set { w = value.x; x = value.y; z = value.z; y = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the W, Y, X, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Wyxz { get { return new Float4(w, y, x, z); } set { w = value.x; y = value.y; x = value.z; z = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the W, Y, Z, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Wyzx { get { return new Float4(w, y, z, x); } set { w = value.x; y = value.y; z = value.z; x = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the W, Z, X, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Wzxy { get { return new Float4(w, z, x, y); } set { w = value.x; z = value.y; x = value.z; y = value.w; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector4 with the W, Z, Y, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Wzyx { get { return new Float4(w, z, y, x); } set { w = value.x; z = value.y; y = value.z; x = value.w; } }

        /// <summary>
        /// Gets an osuTK.Vector4 with the W, Z, Y, and W components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float4 Wzyw { get { return new Float4(w, z, y, w); } set { x = value.x; z = value.y; y = value.z; w = value.w; } }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator +(Float4 left, Float4 right)
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;
            left.w += right.w;
            return left;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator -(Float4 left, Float4 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;
            left.w -= right.w;
            return left;
        }

        /// <summary>
        /// Negates an instance.
        /// </summary>
        /// <param name="vec">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator -(Float4 vec)
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            vec.z = -vec.z;
            vec.w = -vec.w;
            return vec;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="vec">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator *(Float4 vec, float scale)
        {
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            vec.w *= scale;
            return vec;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="scale">The scalar.</param>
        /// <param name="vec">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator *(float scale, Float4 vec)
        {
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            vec.w *= scale;
            return vec;
        }

        /// <summary>
        /// Component-wise multiplication between the specified instance by a scale vector.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vec">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static Float4 operator *(Float4 vec, Float4 scale)
        {
            vec.x *= scale.x;
            vec.y *= scale.y;
            vec.z *= scale.z;
            vec.w *= scale.w;
            return vec;
        }

        /// <summary>
        /// Transform a Vector by the given Matrix.
        /// </summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed vector</returns>
        public static Float4 operator *(Float4 vec, Matrix4x4 mat)
        {
            Float4 result;
            Float4.Transform(ref vec, ref mat, out result);
            return result;
        }

        /// <summary>
        /// Transform a Vector by the given Matrix using right-handed notation
        /// </summary>
        /// <param name="mat">The desired transformation</param>
        /// <param name="vec">The vector to transform</param>
        /// <returns>The transformed vector</returns>
        public static Float4 operator *(Matrix4x4 mat, Float4 vec)
        {
            Float4 result;
            Float4.Transform(ref mat, ref vec, out result);
            return result;
        }

        /// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <param name="vec">The vector to transform.</param>
        /// <returns>The transformed vector</returns>
        public static Float4 operator *(Quaternion quat, Float4 vec)
        {
            Float4 result;
            Float4.Transform(ref vec, ref quat, out result);
            return result;
        }

        /// <summary>
        /// Divides an instance by a scalar.
        /// </summary>
        /// <param name="vec">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float4 operator /(Float4 vec, float scale)
        {
            vec.x /= scale;
            vec.y /= scale;
            vec.z /= scale;
            vec.w /= scale;
            return vec;
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left equals right; false otherwise.</returns>
        public static bool operator ==(Float4 left, Float4 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left does not equa lright; false otherwise.</returns>
        public static bool operator !=(Float4 left, Float4 right)
        {
            return !left.Equals(right);
        }

        private static string listSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        /// <summary>
        /// Returns a System.String that represents the current Vector4.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("({0}{4} {1}{4} {2}{4} {3})", x, y, z, w, listSeparator);
        }

        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A System.Int32 containing the unique hashcode for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.x.GetHashCode();
                hashCode = (hashCode * 397) ^ this.y.GetHashCode();
                hashCode = (hashCode * 397) ^ this.z.GetHashCode();
                hashCode = (hashCode * 397) ^ this.w.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is Float4))
            {
                return false;
            }

            return this.Equals((Float4)obj);
        }

        /// <summary>Indicates whether the current vector is equal to another vector.</summary>
        /// <param name="other">A vector to compare with this vector.</param>
        /// <returns>true if the current vector is equal to the vector parameter; otherwise, false.</returns>
        public bool Equals(Float4 other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w;
        }
    }
}
