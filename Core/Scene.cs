using System;
using System.Collections.Generic;
using JuraFramework.Graphics;

namespace JuraFramework.Core
{
    public abstract class Scene
    {
        public Container Root { get; } = new Container();

        public List<DrawData> DrawNodes { get; } = [];

        public virtual void Update()
        {
            Root.Update();
        }

        public virtual void Traverse()
        {
            DrawNodes.Clear();

            if (Root.Children.Count == 0)
                return;

            BuildDrawNodes(Root);
        }

        protected virtual void BuildDrawNodes(Drawable drawable)
        {
            if (!drawable.active) return;

            if(drawable is not Container)
                DrawNodes.Add(drawable.Node);

            foreach (Drawable child in drawable.Children)
            {
                BuildDrawNodes(child);
            }
        }
    }
}
