Shader "Lit/Flat"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NCels ("Cels", Integer) = 1
        _MaxIntensity ("Max Intensity", Range(0, 1)) = 0.659
        _Ambient ("Ambient", Range(0, 1)) = 0.408
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf FlatShading noambient

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #include "UnityPBSLighting.cginc"

        int _NCels;
        float _Ambient;
        float _MaxIntensity;

        half4 LightingFlatShading(SurfaceOutputStandard s, half3 lightDir, half atten){
            half intensity = dot(normalize(s.Normal), normalize(lightDir));
            intensity = ceil(intensity * _NCels) / _NCels;
            intensity = _Ambient + (_MaxIntensity - _Ambient) * intensity;
            return half4(
                s.Albedo * intensity,
                s.Alpha
            );
            return half4(0, 0, 0, 1);
        }

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
