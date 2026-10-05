struct appdata
{
    float2 pos: POSITION;
    float2 uv : TEXCOORD;
}

struct v2f
{
    float2 pos : SV_POSITION;
    float2 uv : TEXCOORD;
}

v2f vert(appdata i)
{
    v2f o;
    o.pos = i.pos;
    o.uv = i.uv;
}

float4 frag(v2f i)
{
    
}