Shader "Planet Select/Locked Planet Grayscale"
{
    Properties
    {
        _MainTex ("Planet Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Metallic ("Metallic", Range(0,1)) = 0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        [HideInInspector] _OcclusionMap ("Occlusion", 2D) = "white" {}
        [HideInInspector] _OcclusionStrength ("Occlusion Strength", Float) = 1
        [HideInInspector] _SrcBlend ("Source Blend", Float) = 1
        [HideInInspector] _DstBlend ("Destination Blend", Float) = 0
        [HideInInspector] _ZWrite ("Depth Write", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 300

        CGINCLUDE
        #define UNITY_SETUP_BRDF_INPUT MetallicSetup
        #include "UnityStandardCore.cginc"

        half4 Grayscale(half4 lit)
        {
            half gray = Luminance(lit.rgb);
            return half4(gray, gray, gray, lit.a);
        }
        ENDCG

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode"="ForwardBase" }
            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]

            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vertForwardBase
            #pragma fragment fragLockedBase
            #pragma shader_feature_local _ _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog

            half4 fragLockedBase(VertexOutputForwardBase input) : SV_Target
            {
                return Grayscale(fragForwardBaseInternal(input));
            }
            ENDCG
        }

        Pass
        {
            Name "FORWARD_DELTA"
            Tags { "LightMode"="ForwardAdd" }
            Blend [_SrcBlend] One
            ZWrite Off
            Fog { Color (0,0,0,0) }

            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vertForwardAdd
            #pragma fragment fragLockedAdd
            #pragma shader_feature_local _ _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog

            half4 fragLockedAdd(VertexOutputForwardAdd input) : SV_Target
            {
                return Grayscale(fragForwardAddInternal(input));
            }
            ENDCG
        }

        UsePass "Standard/SHADOWCASTER"
    }
    Fallback Off
}
