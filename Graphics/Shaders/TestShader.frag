#version 450

layout(set = 0, binding = 0) uniform texture2D TEXTURE;
layout(set = 0, binding = 1) uniform sampler SAMPLER;

layout(location = 0) in vec2 uv;
layout(location = 1) in vec4 color;

layout(location = 0) out vec4 fsout_Color;

void main()
{
    vec4 textureColor = texture(
        sampler2D(TEXTURE, SAMPLER),
        uv);

    fsout_Color = textureColor * color;
}