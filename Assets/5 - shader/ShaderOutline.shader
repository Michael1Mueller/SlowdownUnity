Shader "Custom/RingOutline"
{
    Properties
    {
        _RimColor ("Ring Color", Color) = (1,0,0,1)
        _RimPower ("Ring Sharpness", Range(0.5, 8.0)) = 3.0
        _EmissionStrength ("Emission Strength", Range(0, 10)) = 3
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha One
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _RimColor;
            float _RimPower;
            float _EmissionStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float rim = 1.0 - saturate(dot(normalize(i.viewDir), normalize(i.worldNormal)));
                rim = pow(rim, _RimPower);

                float4 color = _RimColor * rim * _EmissionStrength;
                color.a = rim;

                return color;
            }

            ENDCG
        }
    }
}
