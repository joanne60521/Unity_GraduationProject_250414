Shader "Unlit/UnlitCrosshairShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // 仍然使用 UI 圖片的紋理
        _EffectColor ("Effect Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline" = "UniversalPipeline" "UniversalMaterialType" = "UI" }
        Blend SrcAlpha OneMinusSrcAlpha   // 確保透明度正常

        ZTest Always  // 忽略遮擋
        ZWrite Off    // 不寫入深度緩衝
        Cull Off      // 雙面渲染

        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR; // UI 顏色
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            fixed4 _EffectColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);  // 保持 UI 原本顏色
                col.rgb *= _EffectColor.rgb; // 可選的顏色效果
                return col;
            }
            ENDCG
        }
    }
}
