#version 450

layout(location = 0) in vec2 fragUV;
layout(location = 0) out vec4 fsout_Color;

void main()
{
    fsout_Color = vec4(fragUV, 0, 1);
}