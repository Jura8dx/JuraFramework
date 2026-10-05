/*
Copyright (c) 2006 - 2008 The Open Toolkit library.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;

namespace JuraTK
{
    /// <summary>
    /// Represents a 2x4 matrix.
    /// </summary>
    public struct Matrix2x4 : IEquatable<Matrix2x4>
    {
        /// <summary>
        /// Top row of the matrix.
        /// </summary>
        public Float4 Row0;

        /// <summary>
        /// Bottom row of the matrix.
        /// </summary>
        public Float4 Row1;

        /// <summary>
        /// The zero matrix.
        /// </summary>
        public static readonly Matrix2x4 Zero = new Matrix2x4(Float4.Zero, Float4.Zero);

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="row0">Top row of the matrix.</param>
        /// <param name="row1">Bottom row of the matrix.</param>
        public Matrix2x4(Float4 row0, Float4 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }

        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="m00">First item of the first row of the matrix.</param>
        /// <param name="m01">Second item of the first row of the matrix.</param>
        /// <param name="m02">Third item of the first row of the matrix.</param>
        /// <param name="m03">Fourth item of the first row of the matrix.</param>
        /// <param name="m10">First item of the second row of the matrix.</param>
        /// <param name="m11">Second item of the second row of the matrix.</param>
        /// <param name="m12">Third item of the second row of the matrix.</param>
        /// <param name="m13">Fourth item of the second row of the matrix.</param>
        public Matrix2x4(
            float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13)
        {
            Row0 = new Float4(m00, m01, m02, m03);
            Row1 = new Float4(m10, m11, m12, m13);
        }

        /// <summary>
        /// Gets or sets the first column of the matrix.
        /// </summary>
        public Float2 Column0
        {
            get { return new Float2(Row0.x, Row1.x); }
            set { Row0.x = value.x; Row1.x = value.y; }
        }

        /// <summary>
        /// Gets or sets the second column of the matrix.
        /// </summary>
        public Float2 Column1
        {
            get { return new Float2(Row0.y, Row1.y); }
            set { Row0.y = value.x; Row1.y = value.y; }
        }

        /// <summary>
        /// Gets or sets the third column of the matrix.
        /// </summary>
        public Float2 Column2
        {
            get { return new Float2(Row0.z, Row1.z); }
            set { Row0.z = value.x; Row1.z = value.y; }
        }

        /// <summary>
        /// Gets or sets the fourth column of the matrix.
        /// </summary>
        public Float2 Column3
        {
            get { return new Float2(Row0.w, Row1.w); }
            set { Row0.w = value.x; Row1.w = value.y; }
        }

        /// <summary>
        /// Gets or sets the value at row 1, column 1 of this instance.
        /// </summary>
        public float M11 { get { return Row0.x; } set { Row0.x = value; } }

        /// <summary>
        /// Gets or sets the value at row 1, column 2 of this instance.
        /// </summary>
        public float M12 { get { return Row0.y; } set { Row0.y = value; } }

        /// <summary>
        /// Gets or sets the value at row 1, column 3 of this instance.
        /// </summary>
        public float M13 { get { return Row0.z; } set { Row0.z = value; } }

        /// <summary>
        /// Gets or sets the value at row 1, column 4 of this instance.
        /// </summary>
        public float M14 { get { return Row0.w; } set { Row0.w = value; } }

        /// <summary>
        /// Gets or sets the value at row 2, column 1 of this instance.
        /// </summary>
        public float M21 { get { return Row1.x; } set { Row1.x = value; } }

        /// <summary>
        /// Gets or sets the value at row 2, column 2 of this instance.
        /// </summary>
        public float M22 { get { return Row1.y; } set { Row1.y = value; } }

        /// <summary>
        /// Gets or sets the value at row 2, column 3 of this instance.
        /// </summary>
        public float M23 { get { return Row1.z; } set { Row1.z = value; } }

        /// <summary>
        /// Gets or sets the value at row 2, column 4 of this instance.
        /// </summary>
        public float M24 { get { return Row1.w; } set { Row1.w = value; } }

        /// <summary>
        /// Gets or sets the values along the main diagonal of the matrix.
        /// </summary>
        public Float2 Diagonal
        {
            get
            {
                return new Float2(Row0.x, Row1.y);
            }
            set
            {
                Row0.x = value.x;
                Row1.y = value.y;
            }
        }

        /// <summary>
        /// Gets the trace of the matrix, the sum of the values along the diagonal.
        /// </summary>
        public float Trace { get { return Row0.x + Row1.y; } }

        /// <summary>
        /// Gets or sets the value at a specified row and column.
        /// </summary>
        public float this[int rowIndex, int columnIndex]
        {
            get
            {
                if (rowIndex == 0)
                {
                    return Row0[columnIndex];
                }
                else if (rowIndex == 1)
                {
                    return Row1[columnIndex];
                }
                throw new IndexOutOfRangeException("You tried to access this matrix at: (" + rowIndex + ", " + columnIndex + ")");
            }
            set
            {
                if (rowIndex == 0)
                {
                    Row0[columnIndex] = value;
                }
                else if (rowIndex == 1)
                {
                    Row1[columnIndex] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("You tried to set this matrix at: (" + rowIndex + ", " + columnIndex + ")");
                }
            }
        }

        /// <summary>
        /// Builds a rotation matrix.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <param name="result">The resulting Matrix2x4 instance.</param>
        public static void CreateRotation(float angle, out Matrix2x4 result)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            result.Row0.x = cos;
            result.Row0.y = sin;
            result.Row0.z = 0;
            result.Row0.w = 0;
            result.Row1.x = -sin;
            result.Row1.y = cos;
            result.Row1.z = 0;
            result.Row1.w = 0;
        }

        /// <summary>
        /// Builds a rotation matrix.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <returns>The resulting Matrix2x3 instance.</returns>
        public static Matrix2x4 CreateRotation(float angle)
        {
            Matrix2x4 result;
            CreateRotation(angle, out result);
            return result;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Single scale factor for the x, y, and z axes.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(float scale, out Matrix2x4 result)
        {
            result.Row0.x = scale;
            result.Row0.y = 0;
            result.Row0.z = 0;
            result.Row0.w = 0;
            result.Row1.x = 0;
            result.Row1.y = scale;
            result.Row1.z = 0;
            result.Row1.w = 0;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Single scale factor for the x and y axes.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix2x4 CreateScale(float scale)
        {
            Matrix2x4 result;
            CreateScale(scale, out result);
            return result;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Scale factors for the x and y axes.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(Float2 scale, out Matrix2x4 result)
        {
            result.Row0.x = scale.x;
            result.Row0.y = 0;
            result.Row0.z = 0;
            result.Row0.w = 0;
            result.Row1.x = 0;
            result.Row1.y = scale.y;
            result.Row1.z = 0;
            result.Row1.w = 0;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Scale factors for the x and y axes.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix2x4 CreateScale(Float2 scale)
        {
            Matrix2x4 result;
            CreateScale(scale, out result);
            return result;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="x">Scale factor for the x axis.</param>
        /// <param name="y">Scale factor for the y axis.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(float x, float y, out Matrix2x4 result)
        {
            result.Row0.x = x;
            result.Row0.y = 0;
            result.Row0.z = 0;
            result.Row0.w = 0;
            result.Row1.x = 0;
            result.Row1.y = y;
            result.Row1.z = 0;
            result.Row1.w = 0;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="x">Scale factor for the x axis.</param>
        /// <param name="y">Scale factor for the y axis.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix2x4 CreateScale(float x, float y)
        {
            Matrix2x4 result;
            CreateScale(x, y, out result);
            return result;
        }

        /// <summary>
        /// Multiplies and instance by a scalar.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication.</param>
        public static void Mult(ref Matrix2x4 left, float right, out Matrix2x4 result)
        {
            result.Row0.x = left.Row0.x * right;
            result.Row0.y = left.Row0.y * right;
            result.Row0.z = left.Row0.z * right;
            result.Row0.w = left.Row0.w * right;
            result.Row1.x = left.Row1.x * right;
            result.Row1.y = left.Row1.y * right;
            result.Row1.z = left.Row1.z * right;
            result.Row1.w = left.Row1.w * right;
        }

        /// <summary>
        /// Multiplies and instance by a scalar.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix2x4 Mult(Matrix2x4 left, float right)
        {
            Matrix2x4 result;
            Mult(ref left, right, out result);
            return result;
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication.</param>
        public static void Mult(ref Matrix2x4 left, ref Matrix4x2 right, out Matrix2x2 result)
        {
            float lM11 = left.Row0.x, lM12 = left.Row0.y, lM13 = left.Row0.z, lM14 = left.Row0.w,
                lM21 = left.Row1.x, lM22 = left.Row1.y, lM23 = left.Row1.z, lM24 = left.Row1.w,
                rM11 = right.Row0.x, rM12 = right.Row0.y,
                rM21 = right.Row1.x, rM22 = right.Row1.y,
                rM31 = right.Row2.x, rM32 = right.Row2.y,
                rM41 = right.Row3.x, rM42 = right.Row3.y;

            result.Row0.x = (((lM11 * rM11) + (lM12 * rM21)) + (lM13 * rM31)) + (lM14 * rM41);
            result.Row0.y = (((lM11 * rM12) + (lM12 * rM22)) + (lM13 * rM32)) + (lM14 * rM42);
            result.Row1.x = (((lM21 * rM11) + (lM22 * rM21)) + (lM23 * rM31)) + (lM24 * rM41);
            result.Row1.y = (((lM21 * rM12) + (lM22 * rM22)) + (lM23 * rM32)) + (lM24 * rM42);
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix2x2 Mult(Matrix2x4 left, Matrix4x2 right)
        {
            Matrix2x2 result;
            Mult(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication.</param>
        public static void Mult(ref Matrix2x4 left, ref Matrix4x3 right, out Matrix2x3 result)
        {
            float lM11 = left.Row0.x, lM12 = left.Row0.y, lM13 = left.Row0.z, lM14 = left.Row0.w,
                lM21 = left.Row1.x, lM22 = left.Row1.y, lM23 = left.Row1.z, lM24 = left.Row1.w,
                rM11 = right.Row0.x, rM12 = right.Row0.y, rM13 = right.Row0.z,
                rM21 = right.Row1.x, rM22 = right.Row1.y, rM23 = right.Row1.z,
                rM31 = right.Row2.x, rM32 = right.Row2.y, rM33 = right.Row2.z,
                rM41 = right.Row3.x, rM42 = right.Row3.y, rM43 = right.Row3.z;

            result.Row0.x = (((lM11 * rM11) + (lM12 * rM21)) + (lM13 * rM31)) + (lM14 * rM41);
            result.Row0.y = (((lM11 * rM12) + (lM12 * rM22)) + (lM13 * rM32)) + (lM14 * rM42);
            result.Row0.z = (((lM11 * rM13) + (lM12 * rM23)) + (lM13 * rM33)) + (lM14 * rM43);
            result.Row1.x = (((lM21 * rM11) + (lM22 * rM21)) + (lM23 * rM31)) + (lM24 * rM41);
            result.Row1.y = (((lM21 * rM12) + (lM22 * rM22)) + (lM23 * rM32)) + (lM24 * rM42);
            result.Row1.z = (((lM21 * rM13) + (lM22 * rM23)) + (lM23 * rM33)) + (lM24 * rM43);
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix2x3 Mult(Matrix2x4 left, Matrix4x3 right)
        {
            Matrix2x3 result;
            Mult(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication.</param>
        public static void Mult(ref Matrix2x4 left, ref Matrix4x4 right, out Matrix2x4 result)
        {
            float lM11 = left.Row0.x, lM12 = left.Row0.y, lM13 = left.Row0.z, lM14 = left.Row0.w,
                lM21 = left.Row1.x, lM22 = left.Row1.y, lM23 = left.Row1.z, lM24 = left.Row1.w,
                rM11 = right.Row0.x, rM12 = right.Row0.y, rM13 = right.Row0.z, rM14 = right.Row0.w,
                rM21 = right.Row1.x, rM22 = right.Row1.y, rM23 = right.Row1.z, rM24 = right.Row1.w,
                rM31 = right.Row2.x, rM32 = right.Row2.y, rM33 = right.Row2.z, rM34 = right.Row2.w,
                rM41 = right.Row3.x, rM42 = right.Row3.y, rM43 = right.Row3.z, rM44 = right.Row3.w;

            result.Row0.x = (((lM11 * rM11) + (lM12 * rM21)) + (lM13 * rM31)) + (lM14 * rM41);
            result.Row0.y = (((lM11 * rM12) + (lM12 * rM22)) + (lM13 * rM32)) + (lM14 * rM42);
            result.Row0.z = (((lM11 * rM13) + (lM12 * rM23)) + (lM13 * rM33)) + (lM14 * rM43);
            result.Row0.w = (((lM11 * rM14) + (lM12 * rM24)) + (lM13 * rM34)) + (lM14 * rM44);
            result.Row1.x = (((lM21 * rM11) + (lM22 * rM21)) + (lM23 * rM31)) + (lM24 * rM41);
            result.Row1.y = (((lM21 * rM12) + (lM22 * rM22)) + (lM23 * rM32)) + (lM24 * rM42);
            result.Row1.z = (((lM21 * rM13) + (lM22 * rM23)) + (lM23 * rM33)) + (lM24 * rM43);
            result.Row1.w = (((lM21 * rM14) + (lM22 * rM24)) + (lM23 * rM34)) + (lM24 * rM44);
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix2x4 Mult(Matrix2x4 left, Matrix4x4 right)
        {
            Matrix2x4 result;
            Mult(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <param name="result">A new instance that is the result of the addition.</param>
        public static void Add(ref Matrix2x4 left, ref Matrix2x4 right, out Matrix2x4 result)
        {
            result.Row0.x = left.Row0.x + right.Row0.x;
            result.Row0.y = left.Row0.y + right.Row0.y;
            result.Row0.z = left.Row0.z + right.Row0.z;
            result.Row0.w = left.Row0.w + right.Row0.w;
            result.Row1.x = left.Row1.x + right.Row1.x;
            result.Row1.y = left.Row1.y + right.Row1.y;
            result.Row1.z = left.Row1.z + right.Row1.z;
            result.Row1.w = left.Row1.w + right.Row1.w;
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <returns>A new instance that is the result of the addition.</returns>
        public static Matrix2x4 Add(Matrix2x4 left, Matrix2x4 right)
        {
            Matrix2x4 result;
            Add(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left operand of the subtraction.</param>
        /// <param name="right">The right operand of the subtraction.</param>
        /// <param name="result">A new instance that is the result of the subtraction.</param>
        public static void Subtract(ref Matrix2x4 left, ref Matrix2x4 right, out Matrix2x4 result)
        {
            result.Row0.x = left.Row0.x - right.Row0.x;
            result.Row0.y = left.Row0.y - right.Row0.y;
            result.Row0.z = left.Row0.z - right.Row0.z;
            result.Row0.w = left.Row0.w - right.Row0.w;
            result.Row1.x = left.Row1.x - right.Row1.x;
            result.Row1.y = left.Row1.y - right.Row1.y;
            result.Row1.z = left.Row1.z - right.Row1.z;
            result.Row1.w = left.Row1.w - right.Row1.w;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left operand of the subtraction.</param>
        /// <param name="right">The right operand of the subtraction.</param>
        /// <returns>A new instance that is the result of the subtraction.</returns>
        public static Matrix2x4 Subtract(Matrix2x4 left, Matrix2x4 right)
        {
            Matrix2x4 result;
            Subtract(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Calculate the transpose of the given matrix.
        /// </summary>
        /// <param name="mat">The matrix to transpose.</param>
        /// <param name="result">The transpose of the given matrix.</param>
        public static void Transpose(ref Matrix2x4 mat, out Matrix4x2 result)
        {
            result.Row0.x = mat.Row0.x;
            result.Row0.y = mat.Row1.x;
            result.Row1.x = mat.Row0.y;
            result.Row1.y = mat.Row1.y;
            result.Row2.x = mat.Row0.z;
            result.Row2.y = mat.Row1.z;
            result.Row3.x = mat.Row0.w;
            result.Row3.y = mat.Row1.w;
        }

        /// <summary>
        /// Calculate the transpose of the given matrix.
        /// </summary>
        /// <param name="mat">The matrix to transpose.</param>
        /// <returns>The transpose of the given matrix.</returns>
        public static Matrix4x2 Transpose(Matrix2x4 mat)
        {
            Matrix4x2 result;
            Transpose(ref mat, out result);
            return result;
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2x4 which holds the result of the multiplication</returns>
        public static Matrix2x4 operator *(float left, Matrix2x4 right)
        {
            return Mult(right, left);
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2x4 which holds the result of the multiplication</returns>
        public static Matrix2x4 operator *(Matrix2x4 left, float right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
        public static Matrix2x2 operator *(Matrix2x4 left, Matrix4x2 right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2x3 which holds the result of the multiplication</returns>
        public static Matrix2x3 operator *(Matrix2x4 left, Matrix4x3 right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2x4 which holds the result of the multiplication</returns>
        public static Matrix2x4 operator *(Matrix2x4 left, Matrix4x4 right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Matrix addition
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2 which holds the result of the addition</returns>
        public static Matrix2x4 operator +(Matrix2x4 left, Matrix2x4 right)
        {
            return Add(left, right);
        }

        /// <summary>
        /// Matrix subtraction
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2x4 which holds the result of the subtraction</returns>
        public static Matrix2x4 operator -(Matrix2x4 left, Matrix2x4 right)
        {
            return Subtract(left, right);
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left equals right; false otherwise.</returns>
        public static bool operator ==(Matrix2x4 left, Matrix2x4 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left does not equal right; false otherwise.</returns>
        public static bool operator !=(Matrix2x4 left, Matrix2x4 right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns a System.String that represents the current Matrix4.
        /// </summary>
        /// <returns>The string representation of the matrix.</returns>
        public override string ToString()
        {
            return String.Format("{0}\n{1}", Row0, Row1);
        }

        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A System.Int32 containing the unique hashcode for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Row0.GetHashCode() * 397) ^ this.Row1.GetHashCode();
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is Matrix2x4))
            {
                return false;
            }

            return this.Equals((Matrix2x4)obj);
        }

        /// <summary>
        /// Indicates whether the current matrix is equal to another matrix.
        /// </summary>
        /// <param name="other">An matrix to compare with this matrix.</param>
        /// <returns>true if the current matrix is equal to the matrix parameter; otherwise, false.</returns>
        public bool Equals(Matrix2x4 other)
        {
            return
                Row0 == other.Row0 &&
                Row1 == other.Row1;
        }
    }
}
