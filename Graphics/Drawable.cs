using JuraFramework.Resources;
using JuraTK;
using System.Collections.Generic;
using Veldrid;

namespace JuraFramework.Graphics
{
    public class Drawable
    {
        public Float2 Position { get; set; } = Float2.Zero;
        public Float2 Size { get; set; } = Float2.Zero;
        public Quaternion Rotation { get; set; } = Quaternion.Identity;
        public RgbaFloat VertexColor { get; set; } = RgbaFloat.White;
        public RgbaFloat Tint { get; set; } = RgbaFloat.White;

        public string Texture { get; set; } = "white-pixel";

        public bool active = true;

        public Drawable()
        {
        }

        public Drawable(Float2 pos, Float2 size, Quaternion rotation)
        {
            Position = pos;
            Size = size;
            Rotation = rotation;
        }

        public Drawable(Float2 pos, Float2 size)
        {
            Position = pos;
            Size = size;
        }

        public Drawable(Float2 pos, Float2 size, Quaternion rotation, RgbaFloat vertexColor)
        {
            Position = pos;
            Size = size;
            Rotation = rotation;
            VertexColor = vertexColor;
        }

        protected DrawData? drawNode;

        public DrawData Node
        {
            get
            {
                drawNode ??= CreateDrawData();
                drawNode.ApplyState();
                return drawNode;
            }
        }

        public Rect Rectangle
        {
            get => new(Position, Size);
            set { Position = value.Position; Size = value.SizeDelta; }
        }

        public Float2? LocalPosition => Position - Parent?.Position;

        public List<Drawable> Children = [];
        public virtual Drawable? Parent { get; set; }

        public virtual void Update()
        {
            foreach(Drawable child in Children)
            {
                child.Update();
            }
        }

        protected virtual DrawData CreateDrawData() => new(this);
        

        public virtual void AppendChild(Drawable child)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }
}
