Shader "Unlit/Water Shader"
{
    Properties
    {
        _Color("Color", Color) = (0, 1, 1, 0.39)
        _Color2("Color2", Color) = (1, 1, 1, 0.75)
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {


            CGPROGRAM

            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc

            #include "UnityCG.cginc"


            float4 _Color;
            float4 _Color2;

            sampler2D _CameraDepthTexture;

            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float3 tangent : TANGENT;
            };

            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float4 worldPos : SV_Target;
                float3 normals : TEXCOORD0;
            };

            vertexOutput vertexFunc(vertexInput IN) 
            {

                vertexOutput o;

                float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);

                float displacement = (cos(worldPos.y) + cos(worldPos.x + (5 * _Time.y)) + cos(worldPos.z + (5 * _Time.y)));

                worldPos.y = worldPos.y + (displacement * 0.3);

                o.pos = mul(UNITY_MATRIX_VP, worldPos);

                o.worldPos = worldPos;

                float3 posPlusTangent = IN.vertex + IN.tangent * 0.01;
                posPlusTangent = posPlusTangent + ((cos(posPlusTangent.y) + cos(posPlusTangent.x + (5 * _Time.y)) + cos(posPlusTangent.z + (5 * _Time.y)))*0.3);

                float3 bitangent = cross(IN.normal, IN.tangent);
                float3 posPlusBitangent = IN.vertex + bitangent * 0.01;
                posPlusBitangent = posPlusBitangent + ((cos(posPlusBitangent.y) + cos(posPlusBitangent.x + (5 * _Time.y)) + cos(posPlusBitangent.z + (5 * _Time.y)))*0.3);

                float3 modifiedTangent = posPlusTangent - worldPos;
                float3 modifiedBitangent = posPlusBitangent - worldPos;

                float3 modifiedNormal = cross(modifiedTangent, modifiedBitangent);
                o.normals = normalize(modifiedNormal);

                return o;
            }

            float4 fragmentFunc(vertexOutput IN) : COLOR
            {
                float4 o = _Color;
                float t = clamp((IN.worldPos.y+0.3)/0.6, 0, 1);

                //o = lerp(o, _Color2, t);
                //o.a = lerp(o.a, 0.75, t);
                o.a = 0.5;

                float4 op; 
                op.rgb = (IN.normals*0.5+0.5); 
                op.a = 1;
                
                op.rgb = op.bbb*0.5 + op.rrr*0.5 + op.ggg*0;

                o.rgb = o.rgb * 0.5 + op.rgb*1.75;

                return o;
            }

        ENDCG

        }
    }
}