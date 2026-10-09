using System.Collections.Generic;
using JuraFramework.Graphics;

namespace JuraFramework.Core;

public abstract class Scene(Application application)
{
    /// <summary>
    /// Reference to this scene's encapsulating application
    /// </summary>
    public Application application = application;

    /// <summary>
    /// Root drawable for every object in the scene
    /// </summary>
    public Container Root { get; } = new Container();

    /// <summary>
    /// Objects in this scene that will be rendered
    /// </summary>
    public readonly List<Drawable> RenderObjects = [];

    /// <summary>
    /// Loads the scene resources and builds the scene obejcts
    /// </summary>
    public void Load()
    {
        LoadResources();
        BuildSceneObjects();
    }

    /// <summary>
    /// Initializes all scene objects
    /// </summary>
    protected abstract void BuildSceneObjects();

    /// <summary>
    /// Manually loads all texture atlases that will be used in this scene
    /// </summary>
    protected abstract void LoadResources();

    /// <summary>
    /// Start() is called when this scene is loaded
    /// </summary>
    public virtual void Start() => Root.Start();

    /// <summary>
    /// Update() is called once per frame
    /// </summary>
    public virtual void Update() => Root.Update();

    /// <summary>
    /// Starts building a render tree for every renderable object in the scene
    /// </summary>
    public virtual void Traverse()
    {
        RenderObjects.Clear();
        BuildRenderTree(Root);
    }

    /// <summary>
    /// Recursively loops through every active <see cref="Drawable"/> in this scene and adds it to the render queue.
    /// This function is called once per render-cycle.
    /// </summary>
    /// <param name="drawable">Parent drawable</param>
    private void BuildRenderTree(Drawable drawable)
    {
        if (!drawable.active)
            return;

        if(drawable is not Container)
            RenderObjects.Add(drawable);

        foreach(Drawable child in drawable.Children)
        {
            BuildRenderTree(child);
        }
    }
}
