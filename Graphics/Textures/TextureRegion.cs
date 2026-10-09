using JMath;

namespace JuraFramework.Graphics.Textures
{
    /// <summary>
    /// A texture region holds the UV coordinates for a single sub-texture within a texture atlas
    /// </summary>
    public class TextureRegion
    {
        /// <summary>
        /// X position of the origin for this texture region
        /// </summary>
        public float x = 0f;

        /// <summary>
        /// Y position of the origin for this texture region
        /// </summary>
        public float y = 0f;

        /// <summary>
        /// UV width of this texture region
        /// </summary>
        public float width = 1f;

        /// <summary>
        /// UV height of this texture region
        /// </summary>
        public float height = 1f;

        /// <summary>
        /// Texture coordinate representing the top left of this texture region
        /// </summary>
        public Float2 TexCoordTopLeft => new(x, y);

        /// <summary>
        /// Texture coordinate representing the top right of this texture region
        /// </summary>
        public Float2 TexCoordTopRight => new(x + width, y);

        /// <summary>
        /// Texture coordinate representing the bottom left of this texture region
        /// </summary>
        public Float2 TexCoordBottomLeft => new(x, y + height);

        /// <summary>
        /// Texture coordinate representing the bottom right of this texture region
        /// </summary>
        public Float2 TexCoordBottomRight => new Float2(x, y) + new Float2(width, height);

        public TextureRegion()
        {
        }

        public TextureRegion(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}
