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
using System.Runtime.InteropServices;

namespace JuraTK
{
    /// <summary>
    /// Represents a 3x4 matrix.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4x3 : IEquatable<Matrix4x3>
    {
        /// <summary>
        /// Top row of the matrix
        /// </summary>
        public Float3 Row0;

        /// <summary>
        /// 2nd row of the matrix
        /// </summary>
        public Float3 Row1;

        /// <summary>
        /// 3rd row of the matrix
        /// </summary>
        public Float3 Row2;

        /// <summary>
        /// Bottom row of the matrix
        /// </summary>
        public Float3 Row3;

        /// <summary>
        /// The zero matrix
        /// </summary>
        public static readonly Matrix4x3 Zero = new Matrix4x3(Float3.Zero, Float3.Zero, Float3.Zero, Float3.Zero);

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="row0">Top row of the matrix</param>
        /// <param name="row1">Second row of the matrix</param>
        /// <param name="row2">Third row of the matrix</param>
        /// <param name="row3">Bottom row of the matrix</param>
        public Matrix4x3(Float3 row0, Float3 row1, Float3 row2, Float3 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="m00">First item of the first row of the matrix.</param>
        /// <param name="m01">Second item of the first row of the matrix.</param>
        /// <param name="m02">Third item of the first row of the matrix.</param>
        /// <param name="m10">First item of the second row of the matrix.</param>
        /// <param name="m11">Second item of the second row of the matrix.</param>
        /// <param name="m12">Third item of the second row of the matrix.</param>
        /// <param name="m20">First item of the third row of the matrix.</param>
        /// <param name="m21">Second item of the third row of the matrix.</param>
        /// <param name="m22">Third item of the third row of the matrix.</param>
        /// <param name="m30">First item of the fourth row of the matrix.</param>
        /// <param name="m31">Second item of the fourth row of the matrix.</param>
        /// <param name="m32">Third item of the fourth row of the matrix.</param>
        public Matrix4x3(
            float m00, float m01, float m02,
            float m10, float m11, float m12,
            float m20, float m21, float m22,
            float m30, float m31, float m32)
        {
            Row0 = new Float3(m00, m01, m02);
            Row1 = new Float3(m10, m11, m12);
            Row2 = new Float3(m20, m21, m22);
            Row3 = new Float3(m30, m31, m32);
        }

        /// <summary>
        /// Gets the first column of this matrix.
        /// </summary>
        public Float4 Column0
        {
            get { return new Float4(Row0.x, Row1.x, Row2.x, Row3.x); }
        }

        /// <summary>
        /// Gets the second column of this matrix.
        /// </summary>
        public Float4 Column1
        {
            get { return new Float4(Row0.y, Row1.y, Row2.y, Row3.y); }
        }

        /// <summary>
        /// Gets the third column of this matrix.
        /// </summary>
        public Float4 Column2
        {
            get { return new Float4(Row0.z, Row1.z, Row2.z, Row3.z); }
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
        /// Gets or sets the value at row 3, column 1 of this instance.
        /// </summary>
        public float M31 { get { return Row2.x; } set { Row2.x = value; } }

        /// <summary>
        /// Gets or sets the value at row 3, column 2 of this instance.
        /// </summary>
        public float M32 { get { return Row2.y; } set { Row2.y = value; } }

        /// <summary>
        /// Gets or sets the value at row 3, column 3 of this instance.
        /// </summary>
        public float M33 { get { return Row2.z; } set { Row2.z = value; } }

        /// <summary>
        /// Gets or sets the value at row 4, column 1 of this instance.
        /// </summary>
        public float M41 { get { return Row3.x; } set { Row3.x = value; } }

        /// <summary>
        /// Gets or sets the value at row 4, column 2 of this instance.
        /// </summary>
        public float M42 { get { return Row3.y; } set { Row3.y = value; } }

        /// <summary>
        /// Gets or sets the value at row 4, column 3 of this instance.
        /// </summary>
        public float M43 { get { return Row3.z; } set { Row3.z = value; } }

        /// <summary>
        /// Gets or sets the values along the main diagonal of the matrix.
        /// </summary>
        public Float3 Diagonal
        {
            get
            {
                return new Float3(Row0.x, Row1.y, Row2.z);
            }
            set
            {
                Row0.x = value.x;
                Row1.y = value.y;
                Row2.z = value.z;
            }
        }

        /// <summary>
        /// Gets the trace of the matrix, the sum of the values along the diagonal.
        /// </summary>
        public float Trace { get { return Row0.x + Row1.y + Row2.z; } }

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
                else if (rowIndex == 2)
                {
                    return Row2[columnIndex];
                }
                else if (rowIndex == 3)
                {
                    return Row3[columnIndex];
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
                else if (rowIndex == 2)
                {
                    Row2[columnIndex] = value;
                }
                else if (rowIndex == 3)
                {
                    Row3[columnIndex] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("You tried to set this matrix at: (" + rowIndex + ", " + columnIndex + ")");
                }
            }
        }

        /// <summary>
        /// Converts this instance into it's inverse by inverting the upper-left 3x3 and replacing Row3.
        /// </summary>
        public void Invert()
        {
            this = Matrix4x3.Invert(this);
        }

        /// <summary>
        /// Build a rotation matrix from the specified axis/angle rotation.
        /// </summary>
        /// <param name="axis">The axis to rotate about.</param>
        /// <param name="angle">Angle in radians to rotate counter-clockwise (looking in the direction of the given axis).</param>
        /// <param name="result">A matrix instance.</param>
        public static void CreateFromAxisAngle(Float3 axis, float angle, out Matrix4x3 result)
        {
            axis.Normalize();
            float axisX = axis.x, axisY = axis.y, axisZ = axis.z;

            float cos = (float)System.Math.Cos(-angle);
            float sin = (float)System.Math.Sin(-angle);
            float t = 1.0f - cos;

            float tXX = t * axisX * axisX,
                tXY = t * axisX * axisY,
                tXZ = t * axisX * axisZ,
                tYY = t * axisY * axisY,
                tYZ = t * axisY * axisZ,
                tZZ = t * axisZ * axisZ;

            float sinX = sin * axisX,
                sinY = sin * axisY,
                sinZ = sin * axisZ;

            result.Row0.x = tXX + cos;
            result.Row0.y = tXY - sinZ;
            result.Row0.z = tXZ + sinY;
            result.Row1.x = tXY + sinZ;
            result.Row1.y = tYY + cos;
            result.Row1.z = tYZ - sinX;
            result.Row2.x = tXZ - sinY;
            result.Row2.y = tYZ + sinX;
            result.Row2.z = tZZ + cos;
            result.Row3.x = 0;
            result.Row3.y = 0;
            result.Row3.z = 0;
        }

        /// <summary>
        /// Build a rotation matrix from the specified axis/angle rotation.
        /// </summary>
        /// <param name="axis">The axis to rotate about.</param>
        /// <param name="angle">Angle in radians to rotate counter-clockwise (looking in the direction of the given axis).</param>
        /// <returns>A matrix instance.</returns>
        public static Matrix4x3 CreateFromAxisAngle(Float3 axis, float angle)
        {
            Matrix4x3 result;
            CreateFromAxisAngle(axis, angle, out result);
            return result;
        }

        /// <summary>
        /// Builds a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="q">The quaternion to rotate by.</param>
        /// <param name="result">A matrix instance.</param>
        public static void CreateFromQuaternion(ref Quaternion q, out Matrix4x3 result)
        {
            float x = q.X, y = q.Y, z = q.Z, w = q.W,
                tx = 2 * x, ty = 2 * y, tz = 2 * z,
                txx = tx * x, tyy = ty * y, tzz = tz * z,
                txy = tx * y, txz = tx * z, tyz = ty * z,
                twx = w * tx, twy = w * ty, twz = w * tz;

            result.Row0.x = 1f - tyy - tzz;
            result.Row0.y = txy - twz;
            result.Row0.z = txz + twy;
            result.Row1.x = txy + twz;
            result.Row1.y = 1f - txx - tzz;
            result.Row1.z = tyz - twx;
            result.Row2.x = txz - twy;
            result.Row2.y = tyz + twx;
            result.Row2.z = 1f - txx - tyy;
            result.Row3.x = 0;
            result.Row3.y = 0;
            result.Row3.z = 0;

            /*Vector3 axis;
            float angle;
            q.ToAxisAngle(out axis, out angle);
            CreateFromAxisAngle(axis, angle, out result);*/
        }

        /// <summary>
        /// Builds a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="q">The quaternion to rotate by.</param>
        /// <returns>A matrix instance.</returns>
        public static Matrix4x3 CreateFromQuaternion(Quaternion q)
        {
            Matrix4x3 result;
            CreateFromQuaternion(ref q, out result);
            return result;
        }

        /// <summary>
        /// Builds a rotation matrix for a rotation around the x-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateRotationX(float angle, out Matrix4x3 result)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            result.Row0.x = 1;
            result.Row0.y = 0;
            result.Row0.z = 0;
            result.Row1.x = 0;
            result.Row1.y = cos;
            result.Row1.z = sin;
            result.Row2.x = 0;
            result.Row2.y = -sin;
            result.Row2.z = cos;
            result.Row3.x = 0;
            result.Row3.y = 0;
            result.Row3.z = 0;
        }

        /// <summary>
        /// Builds a rotation matrix for a rotation around the x-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4x3 CreateRotationX(float angle)
        {
            Matrix4x3 result;
            CreateRotationX(angle, out result);
            return result;
        }

        /// <summary>
        /// Builds a rotation matrix for a rotation around the y-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateRotationY(float angle, out Matrix4x3 result)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            result.Row0.x = cos;
            result.Row0.y = 0;
            result.Row0.z = -sin;
            result.Row1.x = 0;
            result.Row1.y = 1;
            result.Row1.z = 0;
            result.Row2.x = sin;
            result.Row2.y = 0;
            result.Row2.z = cos;
            result.Row3.x = 0;
            result.Row3.y = 0;
            result.Row3.z = 0;
        }

        /// <summary>
        /// Builds a rotation matrix for a rotation around the y-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4x3 CreateRotationY(float angle)
        {
            Matrix4x3 result;
            CreateRotationY(angle, out result);
            return result;
        }

        /// <summary>
        /// Builds a rotation matrix for a rotation around the z-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateRotationZ(float angle, out Matrix4x3 result)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            result.Row0.x = cos;
            result.Row0.y = sin;
            result.Row0.z = 0;
            result.Row1.x = -sin;
            result.Row1.y = cos;
            result.Row1.z = 0;
            result.Row2.x = 0;
            result.Row2.y = 0;
            result.Row2.z = 1;
            result.Row3.x = 0;
            result.Row3.y = 0;
            result.Row3.z = 0;
        }

        /// <summary>
        /// Builds a rotation matrix for a rotation around the z-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4x3 CreateRotationZ(float angle)
        {
            Matrix4x3 result;
            CreateRotationZ(angle, out result);
            return result;
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="x">X translation.</param>
        /// <param name="y">Y translation.</param>
        /// <param name="z">Z translation.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateTranslation(float x, float y, float z, out Matrix4x3 result)
        {
            result.Row0.x = 1;
            result.Row0.y = 0;
            result.Row0.z = 0;
            result.Row1.x = 0;
            result.Row1.y = 1;
            result.Row1.z = 0;
            result.Row2.x = 0;
            result.Row2.y = 0;
            result.Row2.z = 1;
            result.Row3.x = x;
            result.Row3.y = y;
            result.Row3.z = z;
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="vector">The translation vector.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateTranslation(ref Float3 vector, out Matrix4x3 result)
        {
            result.Row0.x = 1;
            result.Row0.y = 0;
            result.Row0.z = 0;
            result.Row1.x = 0;
            result.Row1.y = 1;
            result.Row1.z = 0;
            result.Row2.x = 0;
            result.Row2.y = 0;
            result.Row2.z = 1;
            result.Row3.x = vector.x;
            result.Row3.y = vector.y;
            result.Row3.z = vector.z;
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="x">X translation.</param>
        /// <param name="y">Y translation.</param>
        /// <param name="z">Z translation.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4x3 CreateTranslation(float x, float y, float z)
        {
            Matrix4x3 result;
            CreateTranslation(x, y, z, out result);
            return result;
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="vector">The translation vector.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4x3 CreateTranslation(Float3 vector)
        {
            Matrix4x3 result;
            CreateTranslation(vector.x, vector.y, vector.z, out result);
            return result;
        }

        /// <summary>
        /// Build a scaling matrix
        /// </summary>
        /// <param name="scale">Single scale factor for x,y and z axes</param>
        /// <returns>A scaling matrix</returns>
        public static Matrix4x3 CreateScale(float scale)
        {
            return CreateScale(scale, scale, scale);
        }

        /// <summary>
        /// Build a scaling matrix
        /// </summary>
        /// <param name="scale">Scale factors for x,y and z axes</param>
        /// <returns>A scaling matrix</returns>
        public static Matrix4x3 CreateScale(Float3 scale)
        {
            return CreateScale(scale.x, scale.y, scale.z);
        }

        /// <summary>
        /// Build a scaling matrix
        /// </summary>
        /// <param name="x">Scale factor for x-axis</param>
        /// <param name="y">Scale factor for y-axis</param>
        /// <param name="z">Scale factor for z-axis</param>
        /// <returns>A scaling matrix</returns>
        public static Matrix4x3 CreateScale(float x, float y, float z)
        {
            Matrix4x3 result;
            result.Row0.x = x;
            result.Row0.y = 0;
            result.Row0.z = 0;
            result.Row1.x = 0;
            result.Row1.y = y;
            result.Row1.z = 0;
            result.Row2.x = 0;
            result.Row2.y = 0;
            result.Row2.z = z;
            result.Row3.x = 0;
            result.Row3.y = 0;
            result.Row3.z = 0;
            return result;
        }

        /// <summary>
        /// This isn't quite a multiply, but the result may be useful in some situations.
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication</returns>
        public static Matrix4x4 Mult(Matrix4x3 left, Matrix3x4 right)
        {
            Matrix4x4 result;
            Mult(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// This isn't quite a multiply, but the result may be useful in some situations.
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication</param>
        public static void Mult(ref Matrix4x3 left, ref Matrix3x4 right, out Matrix4x4 result)
        {
            float lM11 = left.Row0.x, lM12 = left.Row0.y, lM13 = left.Row0.z,
                lM21 = left.Row1.x, lM22 = left.Row1.y, lM23 = left.Row1.z,
                lM31 = left.Row2.x, lM32 = left.Row2.y, lM33 = left.Row2.z,
                lM41 = left.Row3.x, lM42 = left.Row3.y, lM43 = left.Row3.z,
                rM11 = right.Row0.x, rM12 = right.Row0.y, rM13 = right.Row0.z, rM14 = right.Row0.w,
                rM21 = right.Row1.x, rM22 = right.Row1.y, rM23 = right.Row1.z, rM24 = right.Row1.w,
                rM31 = right.Row2.x, rM32 = right.Row2.y, rM33 = right.Row2.z, rM34 = right.Row2.w;

            result.Row0.x = (lM11 * rM11) + (lM12 * rM21) + (lM13 * rM31);
            result.Row0.y = (lM11 * rM12) + (lM12 * rM22) + (lM13 * rM32);
            result.Row0.z = (lM11 * rM13) + (lM12 * rM23) + (lM13 * rM33);
            result.Row0.w = (lM11 * rM14) + (lM12 * rM24) + (lM13 * rM34);
            result.Row1.x = (lM21 * rM11) + (lM22 * rM21) + (lM23 * rM31);
            result.Row1.y = (lM21 * rM12) + (lM22 * rM22) + (lM23 * rM32);
            result.Row1.z = (lM21 * rM13) + (lM22 * rM23) + (lM23 * rM33);
            result.Row1.w = (lM21 * rM14) + (lM22 * rM24) + (lM23 * rM34);
            result.Row2.x = (lM31 * rM11) + (lM32 * rM21) + (lM33 * rM31);
            result.Row2.y = (lM31 * rM12) + (lM32 * rM22) + (lM33 * rM32);
            result.Row2.z = (lM31 * rM13) + (lM32 * rM23) + (lM33 * rM33);
            result.Row2.w = (lM31 * rM14) + (lM32 * rM24) + (lM33 * rM34);
            result.Row3.x = (lM41 * rM11) + (lM42 * rM21) + (lM43 * rM31);
            result.Row3.y = (lM41 * rM12) + (lM42 * rM22) + (lM43 * rM32);
            result.Row3.z = (lM41 * rM13) + (lM42 * rM23) + (lM43 * rM33);
            result.Row3.w = (lM41 * rM14) + (lM42 * rM24) + (lM43 * rM34);
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication</returns>
        public static Matrix4x3 Mult(Matrix4x3 left, Matrix4x3 right)
        {
            Matrix4x3 result;
            Mult(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// This isn't quite a multiply, but the result may be useful in some situations.
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication</param>
        public static void Mult(ref Matrix4x3 left, ref Matrix4x3 right, out Matrix4x3 result)
        {
            float lM11 = left.Row0.x, lM12 = left.Row0.y, lM13 = left.Row0.z,
                lM21 = left.Row1.x, lM22 = left.Row1.y, lM23 = left.Row1.z,
                lM31 = left.Row2.x, lM32 = left.Row2.y, lM33 = left.Row2.z,
                lM41 = left.Row3.x, lM42 = left.Row3.y, lM43 = left.Row3.z,
                rM11 = right.Row0.x, rM12 = right.Row0.y, rM13 = right.Row0.z,
                rM21 = right.Row1.x, rM22 = right.Row1.y, rM23 = right.Row1.z,
                rM31 = right.Row2.x, rM32 = right.Row2.y, rM33 = right.Row2.z,
                rM41 = right.Row3.x, rM42 = right.Row3.y, rM43 = right.Row3.z;

            result.Row0.x = (lM11 * rM11) + (lM12 * rM21) + (lM13 * rM31) + rM41;
            result.Row0.y = (lM11 * rM12) + (lM12 * rM22) + (lM13 * rM32) + rM42;
            result.Row0.z = (lM11 * rM13) + (lM12 * rM23) + (lM13 * rM33) + rM43;
            result.Row1.x = (lM21 * rM11) + (lM22 * rM21) + (lM23 * rM31) + rM41;
            result.Row1.y = (lM21 * rM12) + (lM22 * rM22) + (lM23 * rM32) + rM42;
            result.Row1.z = (lM21 * rM13) + (lM22 * rM23) + (lM23 * rM33) + rM43;
            result.Row2.x = (lM31 * rM11) + (lM32 * rM21) + (lM33 * rM31) + rM41;
            result.Row2.y = (lM31 * rM12) + (lM32 * rM22) + (lM33 * rM32) + rM42;
            result.Row2.z = (lM31 * rM13) + (lM32 * rM23) + (lM33 * rM33) + rM43;
            result.Row3.x = (lM41 * rM11) + (lM42 * rM21) + (lM43 * rM31) + rM41;
            result.Row3.y = (lM41 * rM12) + (lM42 * rM22) + (lM43 * rM32) + rM42;
            result.Row3.z = (lM41 * rM13) + (lM42 * rM23) + (lM43 * rM33) + rM43;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication</returns>
        public static Matrix4x3 Mult(Matrix4x3 left, float right)
        {
            Matrix4x3 result;
            Mult(ref left, right, out result);
            return result;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication</param>
        public static void Mult(ref Matrix4x3 left, float right, out Matrix4x3 result)
        {
            result.Row0 = left.Row0 * right;
            result.Row1 = left.Row1 * right;
            result.Row2 = left.Row2 * right;
            result.Row3 = left.Row3 * right;
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <returns>A new instance that is the result of the addition.</returns>
        public static Matrix4x3 Add(Matrix4x3 left, Matrix4x3 right)
        {
            Matrix4x3 result;
            Add(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <param name="result">A new instance that is the result of the addition.</param>
        public static void Add(ref Matrix4x3 left, ref Matrix4x3 right, out Matrix4x3 result)
        {
            result.Row0 = left.Row0 + right.Row0;
            result.Row1 = left.Row1 + right.Row1;
            result.Row2 = left.Row2 + right.Row2;
            result.Row3 = left.Row3 + right.Row3;
        }

        /// <summary>
        /// Subtracts one instance from another.
        /// </summary>
        /// <param name="left">The left operand of the subraction.</param>
        /// <param name="right">The right operand of the subraction.</param>
        /// <returns>A new instance that is the result of the subraction.</returns>
        public static Matrix4x3 Subtract(Matrix4x3 left, Matrix4x3 right)
        {
            Matrix4x3 result;
            Subtract(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        /// Subtracts one instance from another.
        /// </summary>
        /// <param name="left">The left operand of the subraction.</param>
        /// <param name="right">The right operand of the subraction.</param>
        /// <param name="result">A new instance that is the result of the subraction.</param>
        public static void Subtract(ref Matrix4x3 left, ref Matrix4x3 right, out Matrix4x3 result)
        {
            result.Row0 = left.Row0 - right.Row0;
            result.Row1 = left.Row1 - right.Row1;
            result.Row2 = left.Row2 - right.Row2;
            result.Row3 = left.Row3 - right.Row3;
        }

        /// <summary>
        /// Calculate the inverse of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to invert</param>
        /// <returns>The inverse of the given matrix if it has one, or the input if it is singular</returns>
        /// <exception cref="InvalidOperationException">Thrown if the Matrix4 is singular.</exception>
        public static Matrix4x3 Invert(Matrix4x3 mat)
        {
            Matrix4x3 result;
            Invert(ref mat, out result);
            return result;
        }

        /// <summary>
        /// Calculate the inverse of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to invert</param>
        /// <param name="result">The inverse of the given matrix if it has one, or the input if it is singular</param>
        /// <exception cref="InvalidOperationException">Thrown if the Matrix4 is singular.</exception>
        public static void Invert(ref Matrix4x3 mat, out Matrix4x3 result)
        {
            Matrix3x3 inverseRotation = new Matrix3x3(mat.Column0.Xyz, mat.Column1.Xyz, mat.Column2.Xyz);
            inverseRotation.Row0 /= inverseRotation.Row0.SqrMagnitude;
            inverseRotation.Row1 /= inverseRotation.Row1.SqrMagnitude;
            inverseRotation.Row2 /= inverseRotation.Row2.SqrMagnitude;

            Float3 translation = mat.Row3;

            result.Row0 = inverseRotation.Row0;
            result.Row1 = inverseRotation.Row1;
            result.Row2 = inverseRotation.Row2;
            result.Row3 = new Float3(-Float3.Dot(inverseRotation.Row0, translation), -Float3.Dot(inverseRotation.Row1, translation), -Float3.Dot(inverseRotation.Row2, translation));
        }
        /// <summary>
        /// Calculate the transpose of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to transpose</param>
        /// <returns>The transpose of the given matrix</returns>
        public static Matrix3x4 Transpose(Matrix4x3 mat)
        {
            return new Matrix3x4(mat.Column0, mat.Column1, mat.Column2);
        }

        /// <summary>
        /// Calculate the transpose of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to transpose</param>
        /// <param name="result">The result of the calculation</param>
        public static void Transpose(ref Matrix4x3 mat, out Matrix3x4 result)
        {
            result.Row0 = mat.Column0;
            result.Row1 = mat.Column1;
            result.Row2 = mat.Column2;
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix4 which holds the result of the multiplication</returns>
        public static Matrix4x4 operator *(Matrix4x3 left, Matrix3x4 right)
        {
            return Matrix4x3.Mult(left, right);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix4x3 which holds the result of the multiplication</returns>
        public static Matrix4x3 operator *(Matrix4x3 left, Matrix4x3 right)
        {
            return Matrix4x3.Mult(left, right);
        }

        /// <summary>
        /// Matrix-scalar multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix4x3 which holds the result of the multiplication</returns>
        public static Matrix4x3 operator *(Matrix4x3 left, float right)
        {
            return Matrix4x3.Mult(left, right);
        }

        /// <summary>
        /// Matrix addition
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix4x3 which holds the result of the addition</returns>
        public static Matrix4x3 operator +(Matrix4x3 left, Matrix4x3 right)
        {
            return Matrix4x3.Add(left, right);
        }

        /// <summary>
        /// Matrix subtraction
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix4x3 which holds the result of the subtraction</returns>
        public static Matrix4x3 operator -(Matrix4x3 left, Matrix4x3 right)
        {
            return Matrix4x3.Subtract(left, right);
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left equals right; false otherwise.</returns>
        public static bool operator ==(Matrix4x3 left, Matrix4x3 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left does not equal right; false otherwise.</returns>
        public static bool operator !=(Matrix4x3 left, Matrix4x3 right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns a System.String that represents the current Matrix4x3.
        /// </summary>
        /// <returns>The string representation of the matrix.</returns>
        public override string ToString()
        {
            return string.Format("{0}\n{1}\n{2}", Row0, Row1, Row2);
        }

        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A System.Int32 containing the unique hashcode for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Row0.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Row1.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Row2.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Row3.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare tresult.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is Matrix4x3))
            {
                return false;
            }

            return this.Equals((Matrix4x3)obj);
        }

        /// <summary>Indicates whether the current matrix is equal to another matrix.</summary>
        /// <param name="other">An matrix to compare with this matrix.</param>
        /// <returns>true if the current matrix is equal to the matrix parameter; otherwise, false.</returns>
        public bool Equals(Matrix4x3 other)
        {
            return
                Row0 == other.Row0 &&
                Row1 == other.Row1 &&
                Row2 == other.Row2 &&
                Row3 == other.Row3;
        }
    }
}
