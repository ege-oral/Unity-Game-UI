Shader "Hidden/UI/KawaseBlur"
{
    Properties
    {
        _MainTex ("Source", 2D) = "white" {}
        _Offset ("Offset", Float) = 1.5
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            Name "KawaseBlur"

            HLSLPROGRAM
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
            float4 _MainTex_TexelSize;
            float _Offset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 texel = _MainTex_TexelSize.xy;
                float2 o = texel * _Offset;

                half4 col = tex2D(_MainTex, i.uv + float2( o.x,  o.y));
                col     += tex2D(_MainTex, i.uv + float2(-o.x,  o.y));
                col     += tex2D(_MainTex, i.uv + float2( o.x, -o.y));
                col     += tex2D(_MainTex, i.uv + float2(-o.x, -o.y));
                return col * 0.25;
            }
            ENDHLSL
        }
    }
}
