using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace JuraTK
{
    /// <summary>
    /// Represents a color with 4 floating-point components (R, G, B, A).
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Color : IEquatable<Color>
    {
        /// <summary>
        /// The red component of this Color4 structure.
        /// </summary>
        public float r;

        /// <summary>
        /// The green component of this Color4 structure.
        /// </summary>
        public float g;

        /// <summary>
        /// The blue component of this Color4 structure.
        /// </summary>
        public float b;

        /// <summary>
        /// The alpha component of this Color4 structure.
        /// </summary>
        public float a;

        public static Color White => new(1f, 1f, 1f, 1f);
        public static Color Black => new(0f, 0f, 0f, 1f);
        public static Color Clear => new(0f, 0f, 0f, 0f);

        /// <summary>
        /// Constructs a new Color4 structure from the specified components.
        /// </summary>
        /// <param name="r">The red component of the new Color4 structure.</param>
        /// <param name="g">The green component of the new Color4 structure.</param>
        /// <param name="b">The blue component of the new Color4 structure.</param>
        /// <param name="a">The alpha component of the new Color4 structure.</param>
        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /// <summary>
        /// Constructs a new Color4 structure from the specified components.
        /// </summary>
        /// <param name="r">The red component of the new Color4 structure.</param>
        /// <param name="g">The green component of the new Color4 structure.</param>
        /// <param name="b">The blue component of the new Color4 structure.</param>
        /// <param name="a">The alpha component of the new Color4 structure.</param>
        public Color(byte r, byte g, byte b, byte a)
        {
            this.r = r / (float)byte.MaxValue;
            this.g = g / (float)byte.MaxValue;
            this.b = b / (float)byte.MaxValue;
            this.a = a / (float)byte.MaxValue;
        }

        /// <summary>
        /// Converts this color to an integer representation with 8 bits per channel.
        /// </summary>
        /// <returns>A <see cref="System.Int32"/> that represents this instance.</returns>
        /// <remarks>This method is intended only for compatibility with System.Drawing. It compresses the color into 8 bits per channel, which means color information is lost.</remarks>
        public int ToArgb()
        {
            uint value =
                (uint)(a * Byte.MaxValue) << 24 |
                (uint)(r * Byte.MaxValue) << 16 |
                (uint)(g * Byte.MaxValue) << 8 |
                (uint)(b * Byte.MaxValue);

            return unchecked((int)value);
        }

        /// <summary>
        /// Compares the specified Color4 structures for equality.
        /// </summary>
        /// <param name="left">The left-hand side of the comparison.</param>
        /// <param name="right">The right-hand side of the comparison.</param>
        /// <returns>True if left is equal to right; false otherwise.</returns>
        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares the specified Color4 structures for inequality.
        /// </summary>
        /// <param name="left">The left-hand side of the comparison.</param>
        /// <param name="right">The right-hand side of the comparison.</param>
        /// <returns>True if left is not equal to right; false otherwise.</returns>
        public static bool operator !=(Color left, Color right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Converts the specified System.Drawing.Color to a Color4 structure.
        /// </summary>
        /// <param name="color">The System.Drawing.Color to convert.</param>
        /// <returns>A new Color4 structure containing the converted components.</returns>
        public static implicit operator Color(System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Compares whether this Color4 structure is equal to the specified object.
        /// </summary>
        /// <param name="obj">An object to compare to.</param>
        /// <returns>True obj is a Color4 structure with the same components as this Color4; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is Color))
            {
                return false;
            }

            return Equals((Color)obj);
        }

        /// <summary>
        /// Calculates the hash code for this Color4 structure.
        /// </summary>
        /// <returns>A System.Int32 containing the hashcode of this Color4 structure.</returns>
        public override int GetHashCode()
        {
            return ToArgb();
        }

        /// <summary>
        /// Creates a System.String that describes this Color4 structure.
        /// </summary>
        /// <returns>A System.String that describes this Color4 structure.</returns>
        public override string ToString()
        {
            return String.Format("{{(R, G, B, A) = ({0}, {1}, {2}, {3})}}", r.ToString(), g.ToString(), b.ToString(), a.ToString());
        }

        /// <summary>
        /// Converts sRGB color values to RGB color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// </returns>
        /// <param name="srgb">
        /// Color value to convert in sRGB.
        /// </param>
        public static Color FromSrgb(Color srgb)
        {
            float r, g, b;

            if (srgb.r <= 0.04045f)
            {
                r = srgb.r / 12.92f;
            }
            else
            {
                r = (float)Math.Pow((srgb.r + 0.055f) / (1.0f + 0.055f), 2.4f);
            }

            if (srgb.g <= 0.04045f)
            {
                g = srgb.g / 12.92f;
            }
            else
            {
                g = (float)Math.Pow((srgb.g + 0.055f) / (1.0f + 0.055f), 2.4f);
            }

            if (srgb.b <= 0.04045f)
            {
                b = srgb.b / 12.92f;
            }
            else
            {
                b = (float)Math.Pow((srgb.b + 0.055f) / (1.0f + 0.055f), 2.4f);
            }

            return new Color(r, g, b, srgb.a);
        }

        /// <summary>
        /// Converts RGB color values to sRGB color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// </returns>
        /// <param name="rgb">Color value to convert.</param>
        public static Color ToSrgb(Color rgb)
        {
            float r, g, b;

            if (rgb.r <= 0.0031308)
            {
                r = 12.92f * rgb.r;
            }
            else
            {
                r = (1.0f + 0.055f) * (float)Math.Pow(rgb.r, 1.0f / 2.4f) - 0.055f;
            }

            if (rgb.g <= 0.0031308)
            {
                g = 12.92f * rgb.g;
            }
            else
            {
                g = (1.0f + 0.055f) * (float)Math.Pow(rgb.g, 1.0f / 2.4f) - 0.055f;
            }

            if (rgb.b <= 0.0031308)
            {
                b = 12.92f * rgb.b;
            }
            else
            {
                b = (1.0f + 0.055f) * (float)Math.Pow(rgb.b, 1.0f / 2.4f) - 0.055f;
            }

            return new Color(r, g, b, rgb.a);
        }

        /// <summary>
        /// Converts HSL color values to RGB color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// </returns>
        /// <param name="hsl">
        /// Color value to convert in hue, saturation, lightness (HSL).
        /// The X element is Hue (H), the Y element is Saturation (S), the Z element is Lightness (L), and the W element is Alpha (which is copied to the output's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </param>
        public static Color FromHsl(Vector4 hsl)
        {
            var hue = hsl.X * 360.0f;
            var saturation = hsl.Y;
            var lightness = hsl.Z;

            var C = (1.0f - Math.Abs(2.0f * lightness - 1.0f)) * saturation;

            var h = hue / 60.0f;
            var X = C * (1.0f - Math.Abs(h % 2.0f - 1.0f));

            float r, g, b;
            if (0.0f <= h && h < 1.0f)
            {
                r = C;
                g = X;
                b = 0.0f;
            }
            else if (1.0f <= h && h < 2.0f)
            {
                r = X;
                g = C;
                b = 0.0f;
            }
            else if (2.0f <= h && h < 3.0f)
            {
                r = 0.0f;
                g = C;
                b = X;
            }
            else if (3.0f <= h && h < 4.0f)
            {
                r = 0.0f;
                g = X;
                b = C;
            }
            else if (4.0f <= h && h < 5.0f)
            {
                r = X;
                g = 0.0f;
                b = C;
            }
            else if (5.0f <= h && h < 6.0f)
            {
                r = C;
                g = 0.0f;
                b = X;
            }
            else
            {
                r = 0.0f;
                g = 0.0f;
                b = 0.0f;
            }

            var m = lightness - (C / 2.0f);
            return new Color(r + m, g + m, b + m, hsl.W);
        }

        /// <summary>
        /// Converts RGB color values to HSL color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// The X element is Hue (H), the Y element is Saturation (S), the Z element is Lightness (L), and the W element is Alpha (a copy of the input's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </returns>
        /// <param name="rgb">Color value to convert.</param>
        public static Vector4 ToHsl(Color rgb)
        {
            var M = Math.Max(rgb.r, Math.Max(rgb.g, rgb.b));
            var m = Math.Min(rgb.r, Math.Min(rgb.g, rgb.b));
            var C = M - m;

            float h = 0.0f;
            if (M == rgb.r)
            {
                h = ((rgb.g - rgb.b) / C);
            }
            else if (M == rgb.g)
            {
                h = ((rgb.b - rgb.r) / C) + 2.0f;
            }
            else if (M == rgb.b)
            {
                h = ((rgb.r - rgb.g) / C) + 4.0f;
            }

            var hue = h / 6.0f;
            if (hue < 0.0f)
            {
                hue += 1.0f;
            }

            var lightness = (M + m) / 2.0f;

            var saturation = 0.0f;
            if (0.0f != lightness && lightness != 1.0f)
            {
                saturation = C / (1.0f - Math.Abs(2.0f * lightness - 1.0f));
            }

            return new Vector4(hue, saturation, lightness, rgb.a);
        }

        /// <summary>
        /// Converts HSV color values to RGB color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// </returns>
        /// <param name="hsv">
        /// Color value to convert in hue, saturation, value (HSV).
        /// The X element is Hue (H), the Y element is Saturation (S), the Z element is Value (V), and the W element is Alpha (which is copied to the output's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </param>
        public static Color FromHsv(Vector4 hsv)
        {
            var hue = hsv.X * 360.0f;
            var saturation = hsv.Y;
            var value = hsv.Z;

            var C = value * saturation;

            var h = hue / 60.0f;
            var X = C * (1.0f - Math.Abs(h % 2.0f - 1.0f));

            float r, g, b;
            if (0.0f <= h && h < 1.0f)
            {
                r = C;
                g = X;
                b = 0.0f;
            }
            else if (1.0f <= h && h < 2.0f)
            {
                r = X;
                g = C;
                b = 0.0f;
            }
            else if (2.0f <= h && h < 3.0f)
            {
                r = 0.0f;
                g = C;
                b = X;
            }
            else if (3.0f <= h && h < 4.0f)
            {
                r = 0.0f;
                g = X;
                b = C;
            }
            else if (4.0f <= h && h < 5.0f)
            {
                r = X;
                g = 0.0f;
                b = C;
            }
            else if (5.0f <= h && h < 6.0f)
            {
                r = C;
                g = 0.0f;
                b = X;
            }
            else
            {
                r = 0.0f;
                g = 0.0f;
                b = 0.0f;
            }

            var m = value - C;
            return new Color(r + m, g + m, b + m, hsv.W);
        }

        /// <summary>
        /// Converts RGB color values to HSV color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// The X element is Hue (H), the Y element is Saturation (S), the Z element is Value (V), and the W element is Alpha (a copy of the input's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </returns>
        /// <param name="rgb">Color value to convert.</param>
        public static Vector4 ToHsv(Color rgb)
        {
            var M = Math.Max(rgb.r, Math.Max(rgb.g, rgb.b));
            var m = Math.Min(rgb.r, Math.Min(rgb.g, rgb.b));
            var C = M - m;

            float h = 0.0f;
            if (M == rgb.r)
            {
                h = ((rgb.g - rgb.b) / C) % 6.0f;
            }
            else if (M == rgb.g)
            {
                h = ((rgb.b - rgb.r) / C) + 2.0f;
            }
            else if (M == rgb.b)
            {
                h = ((rgb.r - rgb.g) / C) + 4.0f;
            }

            var hue = (h * 60.0f) / 360.0f;

            var saturation = 0.0f;
            if (0.0f != M)
            {
                saturation = C / M;
            }

            return new Vector4(hue, saturation, M, rgb.a);
        }

        /// <summary>
        /// Converts XYZ color values to RGB color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// </returns>
        /// <param name="xyz">
        /// Color value to convert with the trisimulus values of X, Y, and Z in the corresponding element, and the W element with Alpha (which is copied to the output's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </param>
        /// <remarks>Uses the CIE XYZ colorspace.</remarks>
        public static Color FromXyz(Vector4 xyz)
        {
            var r = 0.41847f * xyz.X + -0.15866f * xyz.Y + -0.082835f * xyz.Z;
            var g = -0.091169f * xyz.X + 0.25243f * xyz.Y + 0.015708f * xyz.Z;
            var b = 0.00092090f * xyz.X + -0.0025498f * xyz.Y + 0.17860f * xyz.Z;
            return new Color(r, g, b, xyz.W);
        }

        /// <summary>
        /// Converts RGB color values to XYZ color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value with the trisimulus values of X, Y, and Z in the corresponding element, and the W element with Alpha (a copy of the input's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </returns>
        /// <param name="rgb">Color value to convert.</param>
        /// <remarks>Uses the CIE XYZ colorspace.</remarks>
        public static Vector4 ToXyz(Color rgb)
        {
            var x = (0.49f * rgb.r + 0.31f * rgb.g + 0.20f * rgb.b) / 0.17697f;
            var y = (0.17697f * rgb.r + 0.81240f * rgb.g + 0.01063f * rgb.b) / 0.17697f;
            var z = (0.00f * rgb.r + 0.01f * rgb.g + 0.99f * rgb.b) / 0.17697f;
            return new Vector4(x, y, z, rgb.a);
        }

        /// <summary>
        /// Converts YCbCr color values to RGB color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// </returns>
        /// <param name="ycbcr">
        /// Color value to convert in Luma-Chrominance (YCbCr) aka YUV.
        /// The X element contains Luma (Y, 0.0 to 1.0), the Y element contains Blue-difference chroma (U, -0.5 to 0.5), the Z element contains the Red-difference chroma (V, -0.5 to 0.5), and the W element contains the Alpha (which is copied to the output's Alpha value).
        /// </param>
        /// <remarks>Converts using ITU-R BT.601/CCIR 601 W(r) = 0.299 W(b) = 0.114 U(max) = 0.436 V(max) = 0.615.</remarks>
        public static Color FromYcbcr(Vector4 ycbcr)
        {
            var r = 1.0f * ycbcr.X + 0.0f * ycbcr.Y + 1.402f * ycbcr.Z;
            var g = 1.0f * ycbcr.X + -0.344136f * ycbcr.Y + -0.714136f * ycbcr.Z;
            var b = 1.0f * ycbcr.X + 1.772f * ycbcr.Y + 0.0f * ycbcr.Z;
            return new Color(r, g, b, ycbcr.W);
        }

        /// <summary>
        /// Converts RGB color values to YUV color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value in Luma-Chrominance (YCbCr) aka YUV.
        /// The X element contains Luma (Y, 0.0 to 1.0), the Y element contains Blue-difference chroma (U, -0.5 to 0.5), the Z element contains the Red-difference chroma (V, -0.5 to 0.5), and the W element contains the Alpha (a copy of the input's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </returns>
        /// <param name="rgb">Color value to convert.</param>
        /// <remarks>Converts using ITU-R BT.601/CCIR 601 W(r) = 0.299 W(b) = 0.114 U(max) = 0.436 V(max) = 0.615.</remarks>
        public static Vector4 ToYcbcr(Color rgb)
        {
            var y = 0.299f * rgb.r + 0.587f * rgb.g + 0.114f * rgb.b;
            var u = -0.168736f * rgb.r + -0.331264f * rgb.g + 0.5f * rgb.b;
            var v = 0.5f * rgb.r + -0.418688f * rgb.g + -0.081312f * rgb.b;
            return new Vector4(y, u, v, rgb.a);
        }

        /// <summary>
        /// Converts HCY color values to RGB color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// </returns>
        /// <param name="hcy">
        /// Color value to convert in hue, chroma, luminance (HCY).
        /// The X element is Hue (H), the Y element is Chroma (C), the Z element is luminance (Y), and the W element is Alpha (which is copied to the output's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </param>
        public static Color FromHcy(Vector4 hcy)
        {
            var hue = hcy.X * 360.0f;
            var C = hcy.Y;
            var luminance = hcy.Z;

            var h = hue / 60.0f;
            var X = C * (1.0f - Math.Abs(h % 2.0f - 1.0f));

            float r, g, b;
            if (0.0f <= h && h < 1.0f)
            {
                r = C;
                g = X;
                b = 0.0f;
            }
            else if (1.0f <= h && h < 2.0f)
            {
                r = X;
                g = C;
                b = 0.0f;
            }
            else if (2.0f <= h && h < 3.0f)
            {
                r = 0.0f;
                g = C;
                b = X;
            }
            else if (3.0f <= h && h < 4.0f)
            {
                r = 0.0f;
                g = X;
                b = C;
            }
            else if (4.0f <= h && h < 5.0f)
            {
                r = X;
                g = 0.0f;
                b = C;
            }
            else if (5.0f <= h && h < 6.0f)
            {
                r = C;
                g = 0.0f;
                b = X;
            }
            else
            {
                r = 0.0f;
                g = 0.0f;
                b = 0.0f;
            }

            var m = luminance - (0.30f * r + 0.59f * g + 0.11f * b);
            return new Color(r + m, g + m, b + m, hcy.W);
        }

        /// <summary>
        /// Converts RGB color values to HCY color values.
        /// </summary>
        /// <returns>
        /// Returns the converted color value.
        /// The X element is Hue (H), the Y element is Chroma (C), the Z element is luminance (Y), and the W element is Alpha (a copy of the input's Alpha value).
        /// Each has a range of 0.0 to 1.0.
        /// </returns>
        /// <param name="rgb">Color value to convert.</param>
        public static Vector4 ToHcy(Color rgb)
        {
            var M = Math.Max(rgb.r, Math.Max(rgb.g, rgb.b));
            var m = Math.Min(rgb.r, Math.Min(rgb.g, rgb.b));
            var C = M - m;

            float h = 0.0f;
            if (M == rgb.r)
            {
                h = ((rgb.g - rgb.b) / C) % 6.0f;
            }
            else if (M == rgb.g)
            {
                h = ((rgb.b - rgb.r) / C) + 2.0f;
            }
            else if (M == rgb.b)
            {
                h = ((rgb.r - rgb.g) / C) + 4.0f;
            }

            var hue = (h * 60.0f) / 360.0f;

            var luminance = 0.30f * rgb.r + 0.59f * rgb.g + 0.11f * rgb.b;

            return new Vector4(hue, C, luminance, rgb.a);
        }

        /// <summary>
        /// Compares whether this Color4 structure is equal to the specified Color4.
        /// </summary>
        /// <param name="other">The Color4 structure to compare to.</param>
        /// <returns>True if both Color4 structures contain the same components; false otherwise.</returns>
        public bool Equals(Color other)
        {
            return
                this.r == other.r &&
                this.g == other.g &&
                this.b == other.b &&
                this.a == other.a;
        }
    }
}
