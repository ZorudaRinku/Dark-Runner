Shader "Custom/InvertVertexColor" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                float invert = i.worldPos.y < 0 ? 1 : 0;
                col.rgb = lerp(col.rgb, 1 - col.rgb, invert);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}