Shader "Inno/DissolveSpriteShader"
{
    Properties {
        [PerRendererData] _MainTex ("Main texture", 2D) = "white" {}
        _Threshold ("Threshold", Range(0., 1.01)) = 0.
        _TexSize ("Texture Size", Float) = 1000
    }
 
    SubShader {
 
        Tags { "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass {
           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
           
            sampler2D _MainTex;
 
            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
 
            float _Threshold;
            float _TexSize;
 
            fixed4 frag(v2f i) : SV_Target {
                float4 c = tex2D(_MainTex, i.uv);
 
                float _TexSizeB = _Threshold * _TexSize;
                c.a *= clamp((i.pos.x - _TexSizeB) / _TexSizeB, 0, 1);
                return c;
            }
            ENDCG
        }
    }
}