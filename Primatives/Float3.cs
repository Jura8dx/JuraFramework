using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace JuraTK
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Float3 : IEquatable<Float3>
    {
        public float x;
        public float y;
        public float z;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="value">The value that will initialize this instance.</param>
        public Float3(float value)
        {
            x = value;
            y = value;
            z = value;
        }

        /// <summary>
        /// Constructs a new Vector3.
        /// </summary>
        /// <param name="x">The x component of the Vector3.</param>
        /// <param name="y">The y component of the Vector3.</param>
        /// <param name="z">The z component of the Vector3.</param>
        public Float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Constructs a new Vector3 from the given Vector2.
        /// </summary>
        /// <param name="v">The Vector2 to copy components from.</param>
        public Float3(Float2 v)
        {
            x = v.x;
            y = v.y;
            z = 0.0f;
        }

        /// <summary>
        /// Constructs a new Vector3 from the given Vector3.
        /// </summary>
        /// <param name="v">The Vector3 to copy components from.</param>
        public Float3(Float3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        /// <summary>
        /// Constructs a new Vector3 from the given Vector4.
        /// </summary>
        /// <param name="v">The Vector4 to copy components from.</param>
        public Float3(Float4 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
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
                return (float)System.Math.Sqrt(x * x + y * y + z * z);
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
                return x * x + y * y + z * z;
            }
        }

        /// <summary>
        /// Returns a copy of the Vector3 scaled to unit length.
        /// </summary>
        public Float3 Normalized()
        {
            Float3 v = this;
            v.Normalize();
            return v;
        }

        /// <summary>
        /// Scales the Vector3 to unit length.
        /// </summary>
        public void Normalize()
        {
            float scale = 1.0f / this.Magnitude;
            x *= scale;
            y *= scale;
            z *= scale;
        }
        
        /// <summary>
        /// Defines a unit-length Vector3 that points towards the X-axis.
        /// </summary>
        public static readonly Float3 UnitX = new Float3(1, 0, 0);

        /// <summary>
        /// Defines a unit-length Vector3 that points towards the Y-axis.
        /// </summary>
        public static readonly Float3 UnitY = new Float3(0, 1, 0);

        /// <summary>
        /// Defines a unit-length Vector3 that points towards the Z-axis.
        /// </summary>
        public static readonly Float3 UnitZ = new Float3(0, 0, 1);

        /// <summary>
        /// Defines a zero-length Vector3.
        /// </summary>
        public static readonly Float3 Zero = new Float3(0, 0, 0);

        /// <summary>
        /// Defines an instance with all components set to 1.
        /// </summary>
        public static readonly Float3 One = new Float3(1, 1, 1);

        /// <summary>
        /// Defines the size of the Vector3 struct in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Marshal.SizeOf(new Float3());

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        /// <returns>Result of operation.</returns>
        public static Float3 Add(Float3 a, Float3 b)
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
        public static void Add(ref Float3 a, ref Float3 b, out Float3 result)
        {
            result.x = a.x + b.x;
            result.y = a.y + b.y;
            result.z = a.z + b.z;
        }

        /// <summary>
        /// Subtract one Vector from another
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Result of subtraction</returns>
        public static Float3 Subtract(Float3 a, Float3 b)
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
        public static void Subtract(ref Float3 a, ref Float3 b, out Float3 result)
        {
            result.x = a.x - b.x;
            result.y = a.y - b.y;
            result.z = a.z - b.z;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Float3 Multiply(Float3 vector, float scale)
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
        public static void Multiply(ref Float3 vector, float scale, out Float3 result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
        }

        /// <summary>
        /// Multiplies a vector by the components a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Float3 Multiply(Float3 vector, Float3 scale)
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
        public static void Multiply(ref Float3 vector, ref Float3 scale, out Float3 result)
        {
            result.x = vector.x * scale.x;
            result.y = vector.y * scale.y;
            result.z = vector.z * scale.z;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Float3 Divide(Float3 vector, float scale)
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
        public static void Divide(ref Float3 vector, float scale, out Float3 result)
        {
            result.x = vector.x / scale;
            result.y = vector.y / scale;
            result.z = vector.z / scale;
        }

        /// <summary>
        /// Divides a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Float3 Divide(Float3 vector, Float3 scale)
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
        public static void Divide(ref Float3 vector, ref Float3 scale, out Float3 result)
        {
            result.x = vector.x / scale.x;
            result.y = vector.y / scale.y;
            result.z = vector.z / scale.z;
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>The component-wise minimum</returns>
        public static Float3 ComponentMin(Float3 a, Float3 b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <param name="result">The component-wise minimum</param>
        public static void ComponentMin(ref Float3 a, ref Float3 b, out Float3 result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>The component-wise maximum</returns>
        public static Float3 ComponentMax(Float3 a, Float3 b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <param name="result">The component-wise maximum</param>
        public static void ComponentMax(ref Float3 a, ref Float3 b, out Float3 result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
        }

        /// <summary>
        /// Returns the Vector3 with the minimum magnitude. If the magnitudes are equal, the second vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>The minimum Vector3</returns>
        public static Float3 MagnitudeMin(Float3 left, Float3 right)
        {
            return left.SqrMagnitude < right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Returns the Vector3 with the minimum magnitude. If the magnitudes are equal, the second vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <param name="result">The magnitude-wise minimum</param>
        /// <returns>The minimum Vector3</returns>
        public static void MagnitudeMin(ref Float3 left, ref Float3 right, out Float3 result)
        {
            result = left.SqrMagnitude < right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Returns the Vector3 with the maximum magnitude. If the magnitudes are equal, the first vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>The maximum Vector3</returns>
        public static Float3 MagnitudeMax(Float3 left, Float3 right)
        {
            return left.SqrMagnitude >= right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Returns the Vector3 with the maximum magnitude. If the magnitudes are equal, the first vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <param name="result">The magnitude-wise maximum</param>
        /// <returns>The maximum Vector3</returns>
        public static void MagnitudeMax(ref Float3 left, ref Float3 right, out Float3 result)
        {
            result = left.SqrMagnitude >= right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Returns the Vector3 with the minimum magnitude
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>The minimum Vector3</returns>
        [Obsolete("Use MagnitudeMin() instead.")]
        public static Float3 Min(Float3 left, Float3 right)
        {
            return left.SqrMagnitude < right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Returns the Vector3 with the minimum magnitude
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>The minimum Vector3</returns>
        [Obsolete("Use MagnitudeMax() instead.")]
        public static Float3 Max(Float3 left, Float3 right)
        {
            return left.SqrMagnitude >= right.SqrMagnitude ? left : right;
        }

        /// <summary>
        /// Clamp a vector to the given minimum and maximum vectors
        /// </summary>
        /// <param name="vec">Input vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum vector</param>
        /// <returns>The clamped vector</returns>
        public static Float3 Clamp(Float3 vec, Float3 min, Float3 max)
        {
            vec.x = vec.x < min.x ? min.x : vec.x > max.x ? max.x : vec.x;
            vec.y = vec.y < min.y ? min.y : vec.y > max.y ? max.y : vec.y;
            vec.z = vec.z < min.z ? min.z : vec.z > max.z ? max.z : vec.z;
            return vec;
        }

        /// <summary>
        /// Clamp a vector to the given minimum and maximum vectors
        /// </summary>
        /// <param name="vec">Input vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum vector</param>
        /// <param name="result">The clamped vector</param>
        public static void Clamp(ref Float3 vec, ref Float3 min, ref Float3 max, out Float3 result)
        {
            result.x = vec.x < min.x ? min.x : vec.x > max.x ? max.x : vec.x;
            result.y = vec.y < min.y ? min.y : vec.y > max.y ? max.y : vec.y;
            result.z = vec.z < min.z ? min.z : vec.z > max.z ? max.z : vec.z;
        }

        /// <summary>
        /// Compute the euclidean distance between two vectors.
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>The distance</returns>
        public static float Distance(Float3 vec1, Float3 vec2)
        {
            float result;
            Distance(ref vec1, ref vec2, out result);
            return result;
        }

        /// <summary>
        /// Compute the euclidean distance between two vectors.
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <param name="result">The distance</param>
        public static void Distance(ref Float3 vec1, ref Float3 vec2, out float result)
        {
            result = (float)Math.Sqrt((vec2.x - vec1.x) * (vec2.x - vec1.x) + (vec2.y - vec1.y) * (vec2.y - vec1.y) + (vec2.z - vec1.z) * (vec2.z - vec1.z));
        }

        /// <summary>
        /// Compute the squared euclidean distance between two vectors.
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>The squared distance</returns>
        public static float DistanceSquared(Float3 vec1, Float3 vec2)
        {
            float result;
            DistanceSquared(ref vec1, ref vec2, out result);
            return result;
        }

        /// <summary>
        /// Compute the squared euclidean distance between two vectors.
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <param name="result">The squared distance</param>
        public static void DistanceSquared(ref Float3 vec1, ref Float3 vec2, out float result)
        {
            result = (vec2.x - vec1.x) * (vec2.x - vec1.x) + (vec2.y - vec1.y) * (vec2.y - vec1.y) + (vec2.z - vec1.z) * (vec2.z - vec1.z);
        }

        /// <summary>
        /// Scale a vector to unit length
        /// </summary>
        /// <param name="vec">The input vector</param>
        /// <returns>The normalized vector</returns>
        public static Float3 Normalize(Float3 vec)
        {
            float scale = 1.0f / vec.Magnitude;
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            return vec;
        }

        /// <summary>
        /// Scale a vector to unit length
        /// </summary>
        /// <param name="vec">The input vector</param>
        /// <param name="result">The normalized vector</param>
        public static void Normalize(ref Float3 vec, out Float3 result)
        {
            float scale = 1.0f / vec.Magnitude;
            result.x = vec.x * scale;
            result.y = vec.y * scale;
            result.z = vec.z * scale;
        }

        /// <summary>
        /// Calculate the dot (scalar) product of two vectors
        /// </summary>
        /// <param name="left">First operand</param>
        /// <param name="right">Second operand</param>
        /// <returns>The dot product of the two inputs</returns>
        public static float Dot(Float3 left, Float3 right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z;
        }

        /// <summary>
        /// Calculate the dot (scalar) product of two vectors
        /// </summary>
        /// <param name="left">First operand</param>
        /// <param name="right">Second operand</param>
        /// <param name="result">The dot product of the two inputs</param>
        public static void Dot(ref Float3 left, ref Float3 right, out float result)
        {
            result = left.x * right.x + left.y * right.y + left.z * right.z;
        }

        /// <summary>
        /// Caclulate the cross (vector) product of two vectors
        /// </summary>
        /// <param name="left">First operand</param>
        /// <param name="right">Second operand</param>
        /// <returns>The cross product of the two inputs</returns>
        public static Float3 Cross(Float3 left, Float3 right)
        {
            Float3 result;
            Cross(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Caclulate the cross (vector) product of two vectors
        /// </summary>
        /// <remarks>
        /// It is incorrect to call this method passing the same variable for
        /// <paramref name="result"/> as for <paramref name="left"/> or
        /// <paramref name="right"/>.
        /// </remarks>
        /// <param name="left">First operand</param>
        /// <param name="right">Second operand</param>
        /// <returns>The cross product of the two inputs</returns>
        /// <param name="result">The cross product of the two inputs</param>
        public static void Cross(ref Float3 left, ref Float3 right, out Float3 result)
        {
            result.x = left.y * right.z - left.z * right.y;
            result.y = left.z * right.x - left.x * right.z;
            result.z = left.x * right.y - left.y * right.x;
        }

        /// <summary>
        /// Returns a new Vector that is the linear blend of the 2 given Vectors
        /// </summary>
        /// <param name="a">First input vector</param>
        /// <param name="b">Second input vector</param>
        /// <param name="blend">The blend factor. a when blend=0, b when blend=1.</param>
        /// <returns>a when blend=0, b when blend=1, and a linear combination otherwise</returns>
        public static Float3 Lerp(Float3 a, Float3 b, float blend)
        {
            a.x = blend * (b.x - a.x) + a.x;
            a.y = blend * (b.y - a.y) + a.y;
            a.z = blend * (b.z - a.z) + a.z;
            return a;
        }

        /// <summary>
        /// Returns a new Vector that is the linear blend of the 2 given Vectors
        /// </summary>
        /// <param name="a">First input vector</param>
        /// <param name="b">Second input vector</param>
        /// <param name="blend">The blend factor. a when blend=0, b when blend=1.</param>
        /// <param name="result">a when blend=0, b when blend=1, and a linear combination otherwise</param>
        public static void Lerp(ref Float3 a, ref Float3 b, float blend, out Float3 result)
        {
            result.x = blend * (b.x - a.x) + a.x;
            result.y = blend * (b.y - a.y) + a.y;
            result.z = blend * (b.z - a.z) + a.z;
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
        public static Float3 BaryCentric(Float3 a, Float3 b, Float3 c, float u, float v)
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
        public static void BaryCentric(ref Float3 a, ref Float3 b, ref Float3 c, float u, float v, out Float3 result)
        {
            result = a; // copy

            Float3 temp = b; // copy
            Subtract(ref temp, ref a, out temp);
            Multiply(ref temp, u, out temp);
            Add(ref result, ref temp, out result);

            temp = c; // copy
            Subtract(ref temp, ref a, out temp);
            Multiply(ref temp, v, out temp);
            Add(ref result, ref temp, out result);
        }

        /// <summary>Transform a direction vector by the given Matrix
        /// Assumes the matrix has a bottom row of (0,0,0,1), that is the translation part is ignored.
        /// </summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed vector</returns>
        public static Float3 TransformVector(Float3 vec, Matrix4x4 mat)
        {
            Float3 result;
            TransformVector(ref vec, ref mat, out result);
            return result;
        }

        /// <summary>Transform a direction vector by the given Matrix
        /// Assumes the matrix has a bottom row of (0,0,0,1), that is the translation part is ignored.
        /// </summary>
        /// <remarks>
        /// It is incorrect to call this method passing the same variable for
        /// <paramref name="result"/> as for <paramref name="vec"/>.
        /// </remarks>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <param name="result">The transformed vector</param>
        public static void TransformVector(ref Float3 vec, ref Matrix4x4 mat, out Float3 result)
        {
            result.x = vec.x * mat.Row0.x +
                       vec.y * mat.Row1.x +
                       vec.z * mat.Row2.x;

            result.y = vec.x * mat.Row0.y +
                       vec.y * mat.Row1.y +
                       vec.z * mat.Row2.y;

            result.z = vec.x * mat.Row0.z +
                       vec.y * mat.Row1.z +
                       vec.z * mat.Row2.z;
        }

        /// <summary>Transform a Normal by the given Matrix</summary>
        /// <remarks>
        /// This calculates the inverse of the given matrix, use TransformNormalInverse if you
        /// already have the inverse to avoid this extra calculation
        /// </remarks>
        /// <param name="norm">The normal to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed normal</returns>
        public static Float3 TransformNormal(Float3 norm, Matrix4x4 mat)
        {
            Float3 result;
            TransformNormal(ref norm, ref mat, out result);
            return result;
        }

        /// <summary>Transform a Normal by the given Matrix</summary>
        /// <remarks>
        /// This calculates the inverse of the given matrix, use TransformNormalInverse if you
        /// already have the inverse to avoid this extra calculation
        /// </remarks>
        /// <param name="norm">The normal to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <param name="result">The transformed normal</param>
        public static void TransformNormal(ref Float3 norm, ref Matrix4x4 mat, out Float3 result)
        {
            Matrix4x4 Inverse = Matrix4x4.Invert(mat);
            Float3.TransformNormalInverse(ref norm, ref Inverse, out result);
        }

        /// <summary>Transform a Normal by the (transpose of the) given Matrix</summary>
        /// <remarks>
        /// This version doesn't calculate the inverse matrix.
        /// Use this version if you already have the inverse of the desired transform to hand
        /// </remarks>
        /// <param name="norm">The normal to transform</param>
        /// <param name="invMat">The inverse of the desired transformation</param>
        /// <returns>The transformed normal</returns>
        public static Float3 TransformNormalInverse(Float3 norm, Matrix4x4 invMat)
        {
            Float3 result;
            TransformNormalInverse(ref norm, ref invMat, out result);
            return result;
        }

        /// <summary>Transform a Normal by the (transpose of the) given Matrix</summary>
        /// <remarks>
        /// This version doesn't calculate the inverse matrix.
        /// Use this version if you already have the inverse of the desired transform to hand
        /// </remarks>
        /// <param name="norm">The normal to transform</param>
        /// <param name="invMat">The inverse of the desired transformation</param>
        /// <param name="result">The transformed normal</param>
        public static void TransformNormalInverse(ref Float3 norm, ref Matrix4x4 invMat, out Float3 result)
        {
            result.x = norm.x * invMat.Row0.x +
                       norm.y * invMat.Row0.y +
                       norm.z * invMat.Row0.z;

            result.y = norm.x * invMat.Row1.x +
                       norm.y * invMat.Row1.y +
                       norm.z * invMat.Row1.z;

            result.z = norm.x * invMat.Row2.x +
                       norm.y * invMat.Row2.y +
                       norm.z * invMat.Row2.z;
        }

        /// <summary>Transform a Position by the given Matrix</summary>
        /// <param name="pos">The position to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed position</returns>
        public static Float3 TransformPosition(Float3 pos, Matrix4x4 mat)
        {
            Float3 result;
            TransformPosition(ref pos, ref mat, out result);
            return result;
        }

        /// <summary>Transform a Position by the given Matrix</summary>
        /// <param name="pos">The position to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <param name="result">The transformed position</param>
        public static void TransformPosition(ref Float3 pos, ref Matrix4x4 mat, out Float3 result)
        {
            result.x = pos.x * mat.Row0.x +
                       pos.y * mat.Row1.x +
                       pos.z * mat.Row2.x +
                       mat.Row3.x;

            result.y = pos.x * mat.Row0.y +
                       pos.y * mat.Row1.y +
                       pos.z * mat.Row2.y +
                       mat.Row3.y;

            result.z = pos.x * mat.Row0.z +
                       pos.y * mat.Row1.z +
                       pos.z * mat.Row2.z +
                       mat.Row3.z;
        }

        /// <summary>Transform a Vector by the given Matrix</summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed vector</returns>
        public static Float3 Transform(Float3 vec, Matrix3x3 mat)
        {
            Float3 result;
            Transform(ref vec, ref mat, out result);
            return result;
        }

        /// <summary>Transform a Vector by the given Matrix</summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <param name="result">The transformed vector</param>
        public static void Transform(ref Float3 vec, ref Matrix3x3 mat, out Float3 result)
        {
            result.x = vec.x * mat.Row0.x + vec.y * mat.Row1.x + vec.z * mat.Row2.x;
            result.y = vec.x * mat.Row0.y + vec.y * mat.Row1.y + vec.z * mat.Row2.y;
            result.z = vec.x * mat.Row0.z + vec.y * mat.Row1.z + vec.z * mat.Row2.z;
        }

        /// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="vec">The vector to transform.</param>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <returns>The result of the operation.</returns>
        public static Float3 Transform(Float3 vec, Quaternion quat)
        {
            Float3 result;
            Transform(ref vec, ref quat, out result);
            return result;
        }

        /// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="vec">The vector to transform.</param>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <param name="result">The result of the operation.</param>
        public static void Transform(ref Float3 vec, ref Quaternion quat, out Float3 result)
        {
            // Since vec.W == 0, we can optimize quat * vec * quat^-1 as follows:
            // vec + 2.0 * cross(quat.xyz, cross(quat.xyz, vec) + quat.w * vec)
            Float3 xyz = quat.Xyz, temp, temp2;
            Float3.Cross(ref xyz, ref vec, out temp);
            Float3.Multiply(ref vec, quat.W, out temp2);
            Float3.Add(ref temp, ref temp2, out temp);
            Float3.Cross(ref xyz, ref temp, out temp);
            Float3.Multiply(ref temp, 2, out temp);
            Float3.Add(ref vec, ref temp, out result);
        }

        /// <summary>Transform a Vector by the given Matrix using right-handed notation</summary>
        /// <param name="mat">The desired transformation</param>
        /// <param name="vec">The vector to transform</param>
        public static Float3 Transform(Matrix3x3 mat, Float3 vec)
        {
            Float3 result;
            Transform(ref vec, ref mat, out result);
            return result;
        }

        /// <summary>Transform a Vector by the given Matrix using right-handed notation</summary>
        /// <param name="mat">The desired transformation</param>
        /// <param name="vec">The vector to transform</param>
        /// <param name="result">The transformed vector</param>
        public static void Transform(ref Matrix3x3 mat, ref Float3 vec, out Float3 result)
        {
            result.x = mat.Row0.x * vec.x + mat.Row0.y * vec.y + mat.Row0.z * vec.z;
            result.y = mat.Row1.x * vec.x + mat.Row1.y * vec.y + mat.Row1.z * vec.z;
            result.z = mat.Row2.x * vec.x + mat.Row2.y * vec.y + mat.Row2.z * vec.z;
        }

        /// <summary>Transform a Vector3 by the given Matrix, and project the resulting Vector4 back to a Vector3</summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed vector</returns>
        public static Float3 TransformPerspective(Float3 vec, Matrix4x4 mat)
        {
            Float3 result;
            TransformPerspective(ref vec, ref mat, out result);
            return result;
        }

        /// <summary>Transform a Vector3 by the given Matrix, and project the resulting Vector4 back to a Vector3</summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <param name="result">The transformed vector</param>
        public static void TransformPerspective(ref Float3 vec, ref Matrix4x4 mat, out Float3 result)
        {
            Float4 v = new Float4(vec.x, vec.y, vec.z, 1);
            Float4.Transform(ref v, ref mat, out v);
            result.x = v.x / v.w;
            result.y = v.y / v.w;
            result.z = v.z / v.w;
        }

        /// <summary>
        /// Calculates the angle (in radians) between two vectors.
        /// </summary>
        /// <param name="first">The first vector.</param>
        /// <param name="second">The second vector.</param>
        /// <returns>Angle (in radians) between the vectors.</returns>
        /// <remarks>Note that the returned angle is never bigger than the constant Pi.</remarks>
        public static float CalculateAngle(Float3 first, Float3 second)
        {
            float result;
            CalculateAngle(ref first, ref second, out result);
            return result;
        }

        /// <summary>Calculates the angle (in radians) between two vectors.</summary>
        /// <param name="first">The first vector.</param>
        /// <param name="second">The second vector.</param>
        /// <param name="result">Angle (in radians) between the vectors.</param>
        /// <remarks>Note that the returned angle is never bigger than the constant Pi.</remarks>
        public static void CalculateAngle(ref Float3 first, ref Float3 second, out float result)
        {
            float temp;
            Float3.Dot(ref first, ref second, out temp);
            result = (float)System.Math.Acos(MathHelper.Clamp(temp / (first.Magnitude * second.Magnitude), -1.0, 1.0));
        }

        /// <summary>
        /// Projects a vector from object space into screen space.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="x">The X coordinate of the viewport.</param>
        /// <param name="y">The Y coordinate of the viewport.</param>
        /// <param name="width">The width of the viewport.</param>
        /// <param name="height">The height of the viewport.</param>
        /// <param name="minZ">The minimum depth of the viewport.</param>
        /// <param name="maxZ">The maximum depth of the viewport.</param>
        /// <param name="worldViewProjection">The world-view-projection matrix.</param>
        /// <returns>The vector in screen space.</returns>
        /// <remarks>
        /// To project to normalized device coordinates (NDC) use the following parameters:
        /// Project(vector, -1, -1, 2, 2, -1, 1, worldViewProjection).
        /// </remarks>
        public static Float3 Project(Float3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix4x4 worldViewProjection)
        {
            Float4 result;

            result.x =
                vector.x * worldViewProjection.M11 +
                vector.y * worldViewProjection.M21 +
                vector.z * worldViewProjection.M31 +
                worldViewProjection.M41;

            result.y =
                vector.x * worldViewProjection.M12 +
                vector.y * worldViewProjection.M22 +
                vector.z * worldViewProjection.M32 +
                worldViewProjection.M42;

            result.z =
                vector.x * worldViewProjection.M13 +
                vector.y * worldViewProjection.M23 +
                vector.z * worldViewProjection.M33 +
                worldViewProjection.M43;

            result.w =
                vector.x * worldViewProjection.M14 +
                vector.y * worldViewProjection.M24 +
                vector.z * worldViewProjection.M34 +
                worldViewProjection.M44;

            result /= result.w;

            result.x = x + (width * ((result.x + 1.0f) / 2.0f));
            result.y = y + (height * ((result.y + 1.0f) / 2.0f));
            result.z = minZ + ((maxZ - minZ) * ((result.z + 1.0f) / 2.0f));

            return new Float3(result.x, result.y, result.z);
        }

        /// <summary>
        /// Projects a vector from screen space into object space.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="x">The X coordinate of the viewport.</param>
        /// <param name="y">The Y coordinate of the viewport.</param>
        /// <param name="width">The width of the viewport.</param>
        /// <param name="height">The height of the viewport.</param>
        /// <param name="minZ">The minimum depth of the viewport.</param>
        /// <param name="maxZ">The maximum depth of the viewport.</param>
        /// <param name="inverseWorldViewProjection">The inverse of the world-view-projection matrix.</param>
        /// <returns>The vector in object space.</returns>
        /// <remarks>
        /// To project from normalized device coordinates (NDC) use the following parameters:
        /// Project(vector, -1, -1, 2, 2, -1, 1, inverseWorldViewProjection).
        /// </remarks>
        public static Float3 Unproject(Float3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix4x4 inverseWorldViewProjection)
        {
            Float4 result;

            result.x = ((((vector.x - x) / width) * 2.0f) - 1.0f);
            result.y = ((((vector.y - y) / height) * 2.0f) - 1.0f);
            result.z = (((vector.z / (maxZ - minZ)) * 2.0f) - 1.0f);

            result.x =
                result.x * inverseWorldViewProjection.M11 +
                result.y * inverseWorldViewProjection.M21 +
                result.z * inverseWorldViewProjection.M31 +
                inverseWorldViewProjection.M41;

            result.y =
                result.x * inverseWorldViewProjection.M12 +
                result.y * inverseWorldViewProjection.M22 +
                result.z * inverseWorldViewProjection.M32 +
                inverseWorldViewProjection.M42;

            result.z =
                result.x * inverseWorldViewProjection.M13 +
                result.y * inverseWorldViewProjection.M23 +
                result.z * inverseWorldViewProjection.M33 +
                inverseWorldViewProjection.M43;

            result.w =
                result.x * inverseWorldViewProjection.M14 +
                result.y * inverseWorldViewProjection.M24 +
                result.z * inverseWorldViewProjection.M34 +
                inverseWorldViewProjection.M44;

            result /= result.w;

            return new Float3(result.x, result.y, result.z);
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
        /// Gets or sets an osuTK.Vector3 with the X, Z, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Xzy { get { return new Float3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Y, X, and Z components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Yxz { get { return new Float3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Y, Z, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Yzx { get { return new Float3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Z, X, and Y components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Zxy { get { return new Float3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }

        /// <summary>
        /// Gets or sets an osuTK.Vector3 with the Z, Y, and X components of this instance.
        /// </summary>
        [XmlIgnore]
        public Float3 Zyx { get { return new Float3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator +(Float3 left, Float3 right)
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;
            return left;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator -(Float3 left, Float3 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;
            return left;
        }

        /// <summary>
        /// Negates an instance.
        /// </summary>
        /// <param name="vec">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator -(Float3 vec)
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            vec.z = -vec.z;
            return vec;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="vec">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator *(Float3 vec, float scale)
        {
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            return vec;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="scale">The scalar.</param>
        /// <param name="vec">The instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator *(float scale, Float3 vec)
        {
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;
            return vec;
        }

        /// <summary>
        /// Component-wise multiplication between the specified instance by a scale vector.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vec">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static Float3 operator *(Float3 vec, Float3 scale)
        {
            vec.x *= scale.x;
            vec.y *= scale.y;
            vec.z *= scale.z;
            return vec;
        }

        /// <summary>
        /// Transform a Vector by the given Matrix.
        /// </summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed vector</returns>
        public static Float3 operator *(Float3 vec, Matrix3x3 mat)
        {
            Float3 result;
            Float3.Transform(ref vec, ref mat, out result);
            return result;
        }

        /// <summary>
        /// Transform a Vector by the given Matrix using right-handed notation
        /// </summary>
        /// <param name="mat">The desired transformation</param>
        /// <param name="vec">The vector to transform</param>
        /// <returns>The transformed vector</returns>
        public static Float3 operator *(Matrix3x3 mat, Float3 vec)
        {
            Float3 result;
            Float3.Transform(ref mat, ref vec, out result);
            return result;
        }

        /// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="vec">The vector to transform.</param>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <returns></returns>
        public static Float3 operator *(Quaternion quat, Float3 vec)
        {
            Float3 result;
            Float3.Transform(ref vec, ref quat, out result);
            return result;
        }

        /// <summary>
        /// Divides an instance by a scalar.
        /// </summary>
        /// <param name="vec">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The result of the calculation.</returns>
        public static Float3 operator /(Float3 vec, float scale)
        {
            vec.x /= scale;
            vec.y /= scale;
            vec.z /= scale;
            return vec;
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left equals right; false otherwise.</returns>
        public static bool operator ==(Float3 left, Float3 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left does not equal right; false otherwise.</returns>
        public static bool operator !=(Float3 left, Float3 right)
        {
            return !left.Equals(right);
        }

        private static string listSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        /// <summary>
        /// Returns a System.String that represents the current Vector3.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("({0}{3} {1}{3} {2})", x, y, z, listSeparator);
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
            if (!(obj is Float3))
            {
                return false;
            }

            return this.Equals((Float3)obj);
        }

        /// <summary>Indicates whether the current vector is equal to another vector.</summary>
        /// <param name="other">A vector to compare with this vector.</param>
        /// <returns>true if the current vector is equal to the vector parameter; otherwise, false.</returns>
        public bool Equals(Float3 other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z;
        }
    }
}
