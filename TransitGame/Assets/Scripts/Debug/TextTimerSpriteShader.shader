Shader "Custom/TextTimerSpriteShader" {
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
                float lerp_pos = abs(_TexSize - i.pos.x) * _Threshold;
                c.a *= pow(clamp(((_TexSize - lerp_pos)) / _TexSize, 0, 1), 3);
                if (abs(_TexSize - i.pos.x) / _TexSize > 1 - _Threshold) {
                    c.a *= 0;
                }
                return c;
            }
            ENDCG
        }
    }
}