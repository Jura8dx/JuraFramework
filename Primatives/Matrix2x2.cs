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
    /// Represents a 2x2 matrix
    /// </summary>
    public struct Matrix2x2 : IEquatable<Matrix2x2>
    {
        /// <summary>
        /// Top row of the matrix.
        /// </summary>
        public Float2 Row0;

        /// <summary>
        /// Bottom row of the matrix.
        /// </summary>
        public Float2 Row1;

        /// <summary>
        /// The identity matrix.
        /// </summary>
        public static readonly Matrix2x2 Identity = new Matrix2x2(Float2.UnitX, Float2.UnitY);

        /// <summary>
        /// The zero matrix.
        /// </summary>
        public static readonly Matrix2x2 Zero = new Matrix2x2(Float2.Zero, Float2.Zero);

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="row0">Top row of the matrix.</param>
        /// <param name="row1">Bottom row of the matrix.</param>
        public Matrix2x2(Float2 row0, Float2 row1)
        {
            Row0 = row0;
            Row1 = row1;
        }

        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="m00">First item of the first row of the matrix.</param>
        /// <param name="m01">Second item of the first row of the matrix.</param>
        /// <param name="m10">First item of the second row of the matrix.</param>
        /// <param name="m11">Second item of the second row of the matrix.</param>
        public Matrix2x2(
            float m00, float m01,
            float m10, float m11)
        {
            Row0 = new Float2(m00, m01);
            Row1 = new Float2(m10, m11);
        }

        /// <summary>
        /// Gets the determinant of this matrix.
        /// </summary>
        public float Determinant
        {
            get
            {
                float m11 = Row0.x, m12 = Row0.y,
                      m21 = Row1.x, m22 = Row1.y;

                return m11 * m22 - m12 * m21;
            }
        }

        /// <summary>
        /// Gets or sets the first column of this matrix.
        /// </summary>
        public Float2 Column0
        {
            get { return new Float2(Row0.x, Row1.x); }
            set { Row0.x = value.x; Row1.x = value.y; }
        }

        /// <summary>
        /// Gets or sets the second column of this matrix.
        /// </summary>
        public Float2 Column1
        {
            get { return new Float2(Row0.y, Row1.y); }
            set { Row0.y = value.x; Row1.y = value.y; }
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
        /// Gets or sets the value at row 2, column 1 of this instance.
        /// </summary>
        public float M21 { get { return Row1.x; } set { Row1.x = value; } }

        /// <summary>
        /// Gets or sets the value at row 2, column 2 of this instance.
        /// </summary>
        public float M22 { get { return Row1.y; } set { Row1.y = value; } }

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
        /// Converts this instance to it's transpose.
        /// </summary>
        public void Transpose()
        {
            this = Matrix2x2.Transpose(this);
        }

        /// <summary>
        /// Converts this instance into its inverse.
        /// </summary>
        public void Invert()
        {
            this = Matrix2x2.Invert(this);
        }

        /// <summary>
        /// Builds a rotation matrix.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <param name="result">The resulting Matrix2 instance.</param>
        public static void CreateRotation(float angle, out Matrix2x2 result)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            result.Row0.x = cos;
            result.Row0.y = sin;
            result.Row1.x = -sin;
            result.Row1.y = cos;
        }

        /// <summary>
        /// Builds a rotation matrix.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <returns>The resulting Matrix2 instance.</returns>
        public static Matrix2x2 CreateRotation(float angle)
        {
            Matrix2x2 result;
            CreateRotation(angle, out result);
            return result;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Single scale factor for the x, y, and z axes.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(float scale, out Matrix2x2 result)
        {
            result.Row0.x = scale;
            result.Row0.y = 0;
            result.Row1.x = 0;
            result.Row1.y = scale;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Single scale factor for the x and y axes.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix2x2 CreateScale(float scale)
        {
            Matrix2x2 result;
            CreateScale(scale, out result);
            return result;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Scale factors for the x and y axes.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(Float2 scale, out Matrix2x2 result)
        {
            result.Row0.x = scale.x;
            result.Row0.y = 0;
            result.Row1.x = 0;
            result.Row1.y = scale.y;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Scale factors for the x and y axes.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix2x2 CreateScale(Float2 scale)
        {
            Matrix2x2 result;
            CreateScale(scale, out result);
            return result;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="x">Scale factor for the x axis.</param>
        /// <param name="y">Scale factor for the y axis.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(float x, float y, out Matrix2x2 result)
        {
            result.Row0.x = x;
            result.Row0.y = 0;
            result.Row1.x = 0;
            result.Row1.y = y;
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="x">Scale factor for the x axis.</param>
        /// <param name="y">Scale factor for the y axis.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix2x2 CreateScale(float x, float y)
        {
            Matrix2x2 result;
            CreateScale(x, y, out result);
            return result;
        }

        /// <summary>
        /// Multiplies and instance by a scalar.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication.</param>
        public static void Mult(ref Matrix2x2 left, float right, out Matrix2x2 result)
        {
            result.Row0.x = left.Row0.x * right;
            result.Row0.y = left.Row0.y * right;
            result.Row1.x = left.Row1.x * right;
            result.Row1.y = left.Row1.y * right;
        }

        /// <summary>
        /// Multiplies and instance by a scalar.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix2x2 Mult(Matrix2x2 left, float right)
        {
            Matrix2x2 result;
            Mult(ref left, right, out result);
            return result;
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication.</param>
        public static void Mult(ref Matrix2x2 left, ref Matrix2x2 right, out Matrix2x2 result)
        {
            float lM11 = left.Row0.x, lM12 = left.Row0.y,
                lM21 = left.Row1.x, lM22 = left.Row1.y,
                rM11 = right.Row0.x, rM12 = right.Row0.y,
                rM21 = right.Row1.x, rM22 = right.Row1.y;

            result.Row0.x = (lM11 * rM11) + (lM12 * rM21);
            result.Row0.y = (lM11 * rM12) + (lM12 * rM22);
            result.Row1.x = (lM21 * rM11) + (lM22 * rM21);
            result.Row1.y = (lM21 * rM12) + (lM22 * rM22);
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix2x2 Mult(Matrix2x2 left, Matrix2x2 right)
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
        public static void Mult(ref Matrix2x2 left, ref Matrix2x3 right, out Matrix2x3 result)
        {
            float lM11 = left.Row0.x, lM12 = left.Row0.y,
                lM21 = left.Row1.x, lM22 = left.Row1.y,
                rM11 = right.Row0.x, rM12 = right.Row0.y, rM13 = right.Row0.z,
                rM21 = right.Row1.x, rM22 = right.Row1.y, rM23 = right.Row1.z;

            result.Row0.x = (lM11 * rM11) + (lM12 * rM21);
            result.Row0.y = (lM11 * rM12) + (lM12 * rM22);
            result.Row0.z = (lM11 * rM13) + (lM12 * rM23);
            result.Row1.x = (lM21 * rM11) + (lM22 * rM21);
            result.Row1.y = (lM21 * rM12) + (lM22 * rM22);
            result.Row1.z = (lM21 * rM13) + (lM22 * rM23);
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix2x3 Mult(Matrix2x2 left, Matrix2x3 right)
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
        public static void Mult(ref Matrix2x2 left, ref Matrix2x4 right, out Matrix2x4 result)
        {
            float lM11 = left.Row0.x, lM12 = left.Row0.y,
                lM21 = left.Row1.x, lM22 = left.Row1.y,
                rM11 = right.Row0.x, rM12 = right.Row0.y, rM13 = right.Row0.z, rM14 = right.Row0.w,
                rM21 = right.Row1.x, rM22 = right.Row1.y, rM23 = right.Row1.z, rM24 = right.Row1.w;

            result.Row0.x = (lM11 * rM11) + (lM12 * rM21);
            result.Row0.y = (lM11 * rM12) + (lM12 * rM22);
            result.Row0.z = (lM11 * rM13) + (lM12 * rM23);
            result.Row0.w = (lM11 * rM14) + (lM12 * rM24);
            result.Row1.x = (lM21 * rM11) + (lM22 * rM21);
            result.Row1.y = (lM21 * rM12) + (lM22 * rM22);
            result.Row1.z = (lM21 * rM13) + (lM22 * rM23);
            result.Row1.w = (lM21 * rM14) + (lM22 * rM24);
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix2x4 Mult(Matrix2x2 left, Matrix2x4 right)
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
        public static void Add(ref Matrix2x2 left, ref Matrix2x2 right, out Matrix2x2 result)
        {
            result.Row0.x = left.Row0.x + right.Row0.x;
            result.Row0.y = left.Row0.y + right.Row0.y;
            result.Row1.x = left.Row1.x + right.Row1.x;
            result.Row1.y = left.Row1.y + right.Row1.y;
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <returns>A new instance that is the result of the addition.</returns>
        public static Matrix2x2 Add(Matrix2x2 left, Matrix2x2 right)
        {
            Matrix2x2 result;
            Add(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left operand of the subtraction.</param>
        /// <param name="right">The right operand of the subtraction.</param>
        /// <param name="result">A new instance that is the result of the subtraction.</param>
        public static void Subtract(ref Matrix2x2 left, ref Matrix2x2 right, out Matrix2x2 result)
        {
            result.Row0.x = left.Row0.x - right.Row0.x;
            result.Row0.y = left.Row0.y - right.Row0.y;
            result.Row1.x = left.Row1.x - right.Row1.x;
            result.Row1.y = left.Row1.y - right.Row1.y;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left operand of the subtraction.</param>
        /// <param name="right">The right operand of the subtraction.</param>
        /// <returns>A new instance that is the result of the subtraction.</returns>
        public static Matrix2x2 Subtract(Matrix2x2 left, Matrix2x2 right)
        {
            Matrix2x2 result;
            Subtract(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Calculate the inverse of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to invert</param>
        /// <param name="result">The inverse of the given matrix if it has one, or the input if it is singular</param>
        /// <exception cref="InvalidOperationException">Thrown if the Matrix2 is singular.</exception>
        public static void Invert(ref Matrix2x2 mat, out Matrix2x2 result)
        {
            float det = mat.Determinant;

            if (det == 0)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            float invDet = 1f / det;

            result.Row0.x = mat.Row1.y * invDet;
            result.Row0.y = -mat.Row0.y * invDet;
            result.Row1.x = -mat.Row1.x * invDet;
            result.Row1.y = mat.Row0.x * invDet;
        }

        /// <summary>
        /// Calculate the inverse of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to invert</param>
        /// <returns>The inverse of the given matrix if it has one, or the input if it is singular</returns>
        /// <exception cref="InvalidOperationException">Thrown if the Matrix2 is singular.</exception>
        public static Matrix2x2 Invert(Matrix2x2 mat)
        {
            Matrix2x2 result;
            Invert(ref mat, out result);
            return result;
        }


        /// <summary>
        /// Calculate the transpose of the given matrix.
        /// </summary>
        /// <param name="mat">The matrix to transpose.</param>
        /// <param name="result">The transpose of the given matrix.</param>
        public static void Transpose(ref Matrix2x2 mat, out Matrix2x2 result)
        {
            result.Row0.x = mat.Row0.x;
            result.Row0.y = mat.Row1.x;
            result.Row1.x = mat.Row0.y;
            result.Row1.y = mat.Row1.y;
        }

        /// <summary>
        /// Calculate the transpose of the given matrix.
        /// </summary>
        /// <param name="mat">The matrix to transpose.</param>
        /// <returns>The transpose of the given matrix.</returns>
        public static Matrix2x2 Transpose(Matrix2x2 mat)
        {
            Matrix2x2 result;
            Transpose(ref mat, out result);
            return result;
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
        public static Matrix2x2 operator *(float left, Matrix2x2 right)
        {
            return Mult(right, left);
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
        public static Matrix2x2 operator *(Matrix2x2 left, float right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
        public static Matrix2x2 operator *(Matrix2x2 left, Matrix2x2 right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2x3 which holds the result of the multiplication</returns>
        public static Matrix2x3 operator *(Matrix2x2 left, Matrix2x3 right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2x4 which holds the result of the multiplication</returns>
        public static Matrix2x4 operator *(Matrix2x2 left, Matrix2x4 right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Matrix addition
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2 which holds the result of the addition</returns>
        public static Matrix2x2 operator +(Matrix2x2 left, Matrix2x2 right)
        {
            return Add(left, right);
        }

        /// <summary>
        /// Matrix subtraction
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix2 which holds the result of the subtraction</returns>
        public static Matrix2x2 operator -(Matrix2x2 left, Matrix2x2 right)
        {
            return Subtract(left, right);
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left equals right; false otherwise.</returns>
        public static bool operator ==(Matrix2x2 left, Matrix2x2 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left does not equal right; false otherwise.</returns>
        public static bool operator !=(Matrix2x2 left, Matrix2x2 right)
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
            if (!(obj is Matrix2x2))
            {
                return false;
            }

            return this.Equals((Matrix2x2)obj);
        }

        /// <summary>Indicates whether the current matrix is equal to another matrix.</summary>
        /// <param name="other">An matrix to compare with this matrix.</param>
        /// <returns>true if the current matrix is equal to the matrix parameter; otherwise, false.</returns>
        public bool Equals(Matrix2x2 other)
        {
            return
                Row0 == other.Row0 &&
                Row1 == other.Row1;
        }
    }
}
