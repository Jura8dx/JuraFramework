using System.Collections.Generic;
using JuraFramework.Graphics.Textures;
using JMath;
using Veldrid;

namespace JuraFramework.Graphics;

/// <summary>
/// Drawable is a base class representing a simple screen-space textured quad
/// </summary>
public class Drawable
{
    /// <summary>
    /// 2D position of this drawable
    /// </summary>
    public Float2 Position { get; set; } = Float2.Zero;

    /// <summary>
    /// Size delta of this drawable. Screen size is assumed to be 1920x1080, 
    /// so a drawable that covers the entire screen should have a size of (1920, 1080)
    /// </summary>
    public Float2 Size { get; set; } = Float2.Zero;

    /// <summary>
    /// ID of the texture that should be drawn to this drawable
    /// </summary>
    public string TextureID { get; set; } = "white-pixel.png";

    /// <summary>
    /// ID of the sub-texture that should be drawn to this drawable. This value can be null.
    /// </summary>
    public string? SubTextureID { get; set; }

    /// <summary>
    /// Tint color is multiplied by the final fragment color in the shader
    /// </summary>
    public RgbaFloat Tint { get; set; } = RgbaFloat.White;

    /// <summary>
    /// Atlas texture that will be drawn to the render quad
    /// </summary>
    public AtlasTexture Texture => TextureStore.textures[TextureID];

    /// <summary>
    /// Texture coordinates that tell the shader which part of the texture atlas to sample from
    /// </summary>
    public TextureRegion SubTexture => string.IsNullOrEmpty(SubTextureID) ? new TextureRegion() : Texture.subTextures[SubTextureID];

    /// <summary>
    /// List of this drawables children
    /// </summary>
    public List<Drawable> Children = [];

    /// <summary>
    /// Parent drawable of this drawable. (Nullable)
    /// </summary>
    public virtual Drawable? Parent { get; set; }

    /// <summary>
    /// When false, this drawable, and all of its children, will be skipped in the rendering process
    /// </summary>
    public bool active = true;

    #region Constructors

    public Drawable()
    {
        Awake();
    }

    public Drawable(Float2 pos, Float2 size)
    {
        Position = pos;
        Size = size;
        Awake();
    }

    public Drawable(Float2 pos, Float2 size, RgbaFloat tint)
    {
        Position = pos;
        Size = size;
        Tint = tint;
        Awake();
    }

    #endregion

    /// <summary>
    /// The screen-space rectangle representing this drawables render quad
    /// </summary>
    public Rect Rectangle
    {
        get => new(Position, Size);
        set { Position = value.Position; Size = value.SizeDelta; }
    }

    /// <summary>
    /// Awake() is called when this <see cref="Drawable"/> is initialized
    /// </summary>
    public virtual void Awake()
    {

    }

    /// <summary>
    /// Start() is called when the scene is loaded
    /// </summary>
    public virtual void Start()
    {
        foreach (Drawable child in Children)
        {
            child.Start();
        }
    }

    /// <summary>
    /// Update() is called every frame
    /// </summary>
    public virtual void Update()
    {
        foreach(Drawable child in Children)
        {
            child.Update();
        }
    }

    /// <summary>
    /// Adds a new drawable to the end of this <see cref="Drawable"/>s children list
    /// </summary>
    /// <param name="child"></param>
    public virtual void Append(Drawable child)
    {
        Children.Add(child);
        child.Parent = this;
    }
}
