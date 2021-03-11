Shader "Unlit/Water Shader"
{
    Properties
    {
        _Color("Color", Color) = (0, 1, 1, 0.39)
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


            float4 _Color;

            struct vertexInput
            {
                float4 vertex : POSITION;
            };

            struct vertexOutput
            {
                float4 pos : SV_POSITION;
            };

            vertexOutput vertexFunc(vertexInput IN) 
            {
                vertexOutput o;

                float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);

                float displacement = (cos(worldPos.y) + cos(worldPos.x + (5 * _Time.y)) + cos(worldPos.z + (5 * _Time.y)));

                worldPos.y = worldPos.y + (displacement * 0.3);

                o.pos = mul(UNITY_MATRIX_VP, worldPos);

                return o;
            }

            float4 fragmentFunc(vertexOutput IN) : COLOR
            {
                return _Color;
            }

        ENDCG

        }
    }
}