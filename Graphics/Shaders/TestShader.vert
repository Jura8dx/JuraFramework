#version 450

layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec2 TEXCOORD;

layout(location = 0) out vec2 fragUV;

void main()
{
    gl_Position = vec4(POSITION, 1);
    fragUV = TEXCOORD;
}