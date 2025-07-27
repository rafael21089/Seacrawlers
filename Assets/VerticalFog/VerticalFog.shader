Shader "Fog/VerticalFog" {
    Properties
    {
        [Header(Textures and color)]
        [Space]
        _MainTex("Fog texture", 2D) = "white" {}
        [NoScaleOffset] _Mask("Mask", 2D) = "white" {}
        _Color("Color", color) = (1., 1., 1., 1.)
        [Space(10)]

        [Header(Behaviour)]
        [Space]
        _ScrollDirX("Scroll along X", Range(-1., 1.)) = 1.
        _ScrollDirY("Scroll along Y", Range(-1., 1.)) = 1.
        _Speed("Speed", float) = 1.
        _Distance("Fading distance", Range(0., 10.)) = 1.

        _Radius1("Circle 1 Radius", Range(0., 1.)) = 0.5
        _Radius2Min("Circle 2 Minimum Radius", Range(0., 1.)) = 0.2
        _Radius2Max("Circle 2 Maximum Radius", Range(0., 1.)) = 0.5

       _NoiseTex("Noise Texture", 2D) = "white" {}
        _NoiseScaleX("Noise Scale X", Range(0.01, 5.0)) = 0.1
        _NoiseScaleY("Noise Scale Y", Range(0.01, 5.0)) = 0.1
        _NoiseAmount("Noise Amount", Range(0.0, 1.0)) = 0.1
        _NoiseSpeed("Noise Speed", float) = 1.0
        _MainTextureIntensity("Main Texture Intensity", Range(0.0, 2.0)) = 1.0

    }

        SubShader
        {
            Tags { "Queue" = "Transparent+1" "RenderType" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct v2f {
                    float4 pos : SV_POSITION;
                    fixed4 vertCol : COLOR0;
                    float2 uv : TEXCOORD0;
                    float2 uv2 : TEXCOORD1;
                };

                sampler2D _MainTex;
                sampler2D _NoiseTex;
                float4 _MainTex_ST;

                v2f vert(appdata_full v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.uv2 = v.texcoord;
                    o.vertCol = v.color;
                    return o;
                }

float2 CalculateNoiseUV(float2 uv, float time, float speed) {
                float angle = time * speed;
                float radius = length(uv - 0.5);
                float2 offset = float2(
                    cos(angle) * radius,
                    sin(angle) * radius
                );
                return uv + offset;
            }

                 float _Distance;
            float _Radius1;
            float _Radius2Min;
            float _Radius2Max;
            float _NoiseScaleX;
            float _NoiseScaleY;
            float _NoiseAmount;
            float _NoiseSpeed;
            float _MainTextureIntensity;
            float _DeformationStrength;
            sampler2D _Mask;
            float _Speed;
            fixed _ScrollDirX;
            fixed _ScrollDirY;
            fixed4 _Color;
            float noiseScaleFactor;  // Corrected line

            fixed4 frag(v2f i) : SV_Target {
                float2 uv = i.uv + fixed2(_ScrollDirX, _ScrollDirY) * _Speed * _Time.x;
                fixed4 col = tex2D(_MainTex, uv) * _Color * i.vertCol;
                col.a *= tex2D(_Mask, i.uv2).r;

                 // Sample color from the main texture and adjust intensity
                fixed4 mainTexColor = tex2D(_MainTex, uv) * _MainTextureIntensity;

                // Multiply the main texture color with the calculated color
                col *= mainTexColor;

                // Calculate distance from the center of the screen
                float2 center = float2(0.5, 0.5);
                float distance1 = length(i.uv - center);

                float currentTime = _Time.y * _NoiseSpeed; // Adjusted time based on noise speed
                float currentRadius2 = lerp(_Radius2Min, _Radius2Max, sin(currentTime));

                // Add noise to UV coordinates for deformation
                float2 noiseUV = CalculateNoiseUV(i.uv, currentTime, _NoiseSpeed);
                float noiseValue = tex2D(_NoiseTex, noiseUV).r;
                currentRadius2 += noiseValue * _NoiseAmount * currentRadius2;

                float distance2 = length(i.uv - center) / currentRadius2;

                col.a *= 1.0 - smoothstep(_Radius1 - 0.01, _Radius1 + 0.01, distance1); // Circle 1 (Transparent in the center)
                col.a *= smoothstep(_Radius2Min - 0.01, _Radius2Min + 0.01, distance2); // Circle 2 (Opaque in the center)

                col.a *= 1 - ((i.pos.z / i.pos.w) * _Distance);
                return col;
            }
                ENDCG
            }
        }
}
