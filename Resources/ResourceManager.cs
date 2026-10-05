using System;
using System.Collections.Generic;
using System.IO;
using JuraFramework.Graphics.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Veldrid;

namespace JuraFramework.Resources
{
    /// <summary>
    /// Static class responsible for loading sprite resources from a folder into 
    /// valid <see cref="Texture"/> objects to be used in shaders
    /// </summary>
    public static class ResourceManager
    {
        /// <summary>
        /// List of supported file extensions supported by this <see cref="ResourceManager"/>
        /// </summary>
        private static readonly HashSet<string> SupportedFileFormats = [ ".png", ".jpg", ".jpeg" ];

        /// <summary>
        /// Contains all resource information loaded in at runtime
        /// </summary>
        public static readonly Dictionary<string, SpriteAtlas> Resources = [];

        /// <summary>
        /// Loads all .png files from a folder into <see cref="Sprites"/> as a <see cref="Texture"/>
        /// </summary>
        /// <param name="containingFolder"></param>
        /// <param name="factory"></param>
        public static void GenerateResources(string containingFolder, Renderer renderer)
        {

            Console.WriteLine("Generating resources");

            foreach(string file in Directory.EnumerateFiles(containingFolder, "*.*", SearchOption.AllDirectories))
            {
                if (!SupportedFileFormats.Contains(Path.GetExtension(file)))
                {
                    Console.WriteLine($"{Path.GetFileName(file)} is not in a supported file format, skipping...");
                    continue;
                }

                // Generate textures from all sprites in this directory
            }
        }

        public static void GenerateResource(string path, Renderer renderer)
        {
            using Image<Rgba32> image = Image.Load<Rgba32>(path);

            SpriteAtlas atlas = new SpriteAtlas(image, renderer.Factory);
        }
    }
}
