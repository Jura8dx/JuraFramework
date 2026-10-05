#version 450

layout(location = 0) in vec2 POSITION;
layout(location = 1) in vec2 TEXCOORD0;
layout(location = 2) in vec4 COLOR;

layout(location = 0) out vec4 fragColor;

void main()
{
    vec2 TRANSFORM_SCREEN_SPACE = vec2(POSITION.x / 1920, POSITION.y / 1080);
    gl_Position = vec4(TRANSFORM_SCREEN_SPACE, 0, 1);

    fragColor = COLOR;
}
