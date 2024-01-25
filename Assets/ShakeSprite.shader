Shader "Custom/ShakeSprite"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _ShakeAmount ("Shake Amount", Range(0, 1)) = 0.1
        _Speed ("Shake Speed", Range(1, 10)) = 5
    }

    SubShader
    {
        Tags {"Queue"="Overlay" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _ShakeAmount;
        fixed _Speed;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed2 shakeOffset = _ShakeAmount * sin(_Time.y * _Speed);
            IN.uv_MainTex += shakeOffset;
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
