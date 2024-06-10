// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
 
Shader "Custom/testLightShader"
{
    Properties
    {
        _Diffuse("Diffuse", Color) = (1,1,1,1)
        _Specular("Specular", Color) = (1,1,1,1)
        _Gloss("Gloss", Range(8.0,256)) = 20
    }
    SubShader
    {
        Tags {"RenderType"="Opaque" "LightMode" = "UniversalForward"}
 
        HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            CBUFFER_START(UnityPerMaterial)
                half4 _Diffuse;
                half4 _Specular;
                half _Gloss;
            CBUFFER_END
        ENDHLSL
 
        LOD 100
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
 
            struct v2f
            {
                float3 color : COLOR;
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };
 
 
            v2f vert (appdata v)
            {
                v2f o;
                ZERO_INITIALIZE(v2f, o);
                
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
 
                o.worldNormal = TransformObjectToWorldDir(v.normal);
 
                o.worldPos = TransformObjectToWorldDir(v.vertex.xyz);
 
                return o;
            }
 
            half4 frag (v2f i) : SV_TARGET
            {
                half3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
 
                half3 worldNormal = normalize(i.worldNormal);
 
                Light light = GetMainLight();
 
                half3 worldLightDir = normalize(light.direction);
 
                half3 halfLambert = dot(worldNormal, worldLightDir) * 0.5 + 0.5;
 
                half3 lambert = saturate(dot(worldNormal, worldLightDir));
                
                //half3 diffuse = light.color * _Diffuse.rgb * lambert;//兰伯特
 
                half3 diffuse = light.color * _Diffuse.rgb * halfLambert;//半兰伯特
 
                half3 reflectDir = normalize(reflect(-worldLightDir, worldNormal));
 
                half3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
 
                half3 halfDir = normalize(worldLightDir + viewDir);
 
                //half3 specular = light.color.rgb * _Specular.rgb * pow(saturate(dot(reflectDir, viewDir)), _Gloss);   //phong模型 
                half3 specular = light.color.rgb * _Specular.rgb * pow(saturate(dot(worldNormal, halfDir)), _Gloss);    //blinnPhong模型
 
                half3 color = ambient + diffuse + specular;
 
                return half4(color,1.0);
            }
            ENDHLSL
        }
    }
    FallBack "Packages/com.unity.render-pipelines.universal/FallbackError"
}