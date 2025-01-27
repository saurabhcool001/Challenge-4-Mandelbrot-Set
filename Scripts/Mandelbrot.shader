Shader "Custom/MandelbrotShader"
{
    Properties
    {
        _Iterations ("Iterations", Range(1, 500)) = 100
        _Zoom ("Zoom", Float) = 1.0
        _Offset ("Offset", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Iterations;
            float _Zoom;
            float2 _Offset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float2 c = (i.uv - 0.5) * 4.0 / _Zoom + _Offset;
                float2 z = float2(0.0, 0.0);

                int iter;
                for (iter = 0; iter < _Iterations; iter++)
                {
                    if (dot(z, z) > 4.0) break;
                    z = float2(z.x * z.x - z.y * z.y, 2.0 * z.x * z.y) + c;
                }

                float t = iter / _Iterations;
                return lerp(float4(0, 0, 0, 1), float4(t, t * 0.5, t * 0.8, 1), t);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}