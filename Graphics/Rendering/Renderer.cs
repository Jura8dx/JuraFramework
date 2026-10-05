using JuraFramework.Core;
using System;
using Veldrid;

#nullable disable

namespace JuraFramework.Graphics.Rendering
{
    /// <summary>
    /// Simple class responsible for instancing the necessary quads that make up a <see cref="Scene"/>
    /// </summary>
    public abstract class Renderer
    {
        public GraphicsDevice Device { get; set; }
        public ResourceFactory Factory { get; set; }

        /// <summary>
        /// Max number of quads this <see cref="Renderer"/> supports
        /// </summary>
        protected const uint J_MAXIMUM_DRAW_OBJECTS_PER_SCENE = 1024;

        /// <summary>
        /// Maximum number of vertices supported by this <see cref="Renderer"/>
        /// Vertex counts higher than this will exceed the allocated size of the <see cref="VertexBuffer"/>
        /// </summary>
        protected const uint J_MAXIMUM_VERTEX_COUNT = J_MAXIMUM_DRAW_OBJECTS_PER_SCENE * 4;

        /// <summary>
        /// Maximum number of triangle indices supported by this <see cref="Renderer"/>
        /// Index counts higher than this will exceed the allocated size of the <see cref="IndexBuffer"/>
        /// </summary>
        protected const uint J_MAXIMUM_TRIANGLE_INDEX_COUNT = J_MAXIMUM_DRAW_OBJECTS_PER_SCENE * 6;

        /// <summary>
        /// Maximum number of triangles supported by this <see cref="Renderer"/>
        /// </summary>
        protected const uint J_MAXIMUM_TRIANGLE_COUNT = J_MAXIMUM_DRAW_OBJECTS_PER_SCENE * 2;

        public abstract void CreateResources();
        public abstract void RenderFrame(Scene scene);
    }
}
