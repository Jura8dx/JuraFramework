namespace JMath
{
    public struct Rect
    {
        public float x;
        public float y;
        public float width;
        public float height;

        public Float2 Position
        {
            get => new Float2(x, y);
            set { x = value.x; y = value.y; }
        }

        public Float2 SizeDelta
        {
            get => new Float2(width, height);
            set { width = value.x; height = value.y; }
        }

        public readonly Float2 TopLeft => new(x - width * 0.5f, y + height * 0.5f);
        public readonly Float2 BottomLeft => new(x - width * 0.5f, y - height * 0.5f);
        public readonly Float2 BottomRight => new(x + width * 0.5f, y - height * 0.5f);
        public readonly Float2 TopRight => new(x + width * 0.5f, y + height * 0.5f);

        public Rect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rect(Float2 position, Float2 sizeDelta)
        {
            x = position.x;
            y = position.y;
            width = sizeDelta.x;
            height = sizeDelta.y;
        }
    }
}