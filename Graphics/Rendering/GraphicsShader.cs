using System;
using System.IO;
using System.Text;
using Veldrid;
using Veldrid.SPIRV;

namespace JuraFramework.Graphics.Rendering
{
    public class GraphicsShader
    {
        static readonly string shaderDirectory = Path.Combine(AppContext.BaseDirectory, "Graphics", "Shaders");

        public static Shader[] GetShader(string name, ResourceFactory factory)
        {
            string vertexPath = Path.Combine(shaderDirectory, $"{name}.vert");
            string fragmentPath = Path.Combine(shaderDirectory, $"{name}.frag");

            string vertexCode = File.ReadAllText(vertexPath);
            string fragmentCode = File.ReadAllText(fragmentPath);

            ShaderDescription vertexShader = new(
                ShaderStages.Vertex,
                Encoding.UTF8.GetBytes(vertexCode),
                "main");

            ShaderDescription fragmentShader = new(
                ShaderStages.Fragment,
                Encoding.UTF8.GetBytes(fragmentCode),
                "main");

            return factory.CreateFromSpirv(vertexShader, fragmentShader);
        }
    }
}
