Shader "Unlit/shine"
{
    Properties
    {
        _MainTex ("Image", 2D) = "white" {}
		_MaskTex ("Mask", 2D) = "white" {}

		shineColor("Shine color", Color) = (0, 0, 0, 0)
		effectPower("Effect power", Range(0, 1)) = 1
		speed("Speed of transition", Range(0,1)) = 1
		fadeAmount("Fade amount", Range(-100, 100)) = 10
		timeMultiplier("Time multiplier", Range(0, 1)) = 1
		timeConst("Constant time", Range(0, 1)) = 0
		fadeDirection("Fade direction 0 up 1 left", Range(0, 1)) = 1
    }
    SubShader
    {
		Cull Off ZWrite Off ZTest Always
        Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend One OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
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
			sampler2D _MaskTex;
			sampler2D _NoiseTex;

            float4 _MainTex_ST;
			float4 _MaskTex_ST;
			float4 _NoiseTex_ST;
			float4 shineColor;

			float speed;
			float effectPower;
			float jitter;
			float fadeAmount;
			float timeMultiplier;
			float timeConst;
			float fadeDirection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 colBase = tex2D(_MainTex, i.uv);
				fixed4 colMask = tex2D(_MaskTex, i.uv);


				float time = _Time.z * timeMultiplier;
				float timeConstant = timeConst + (1 - timeConst) * (sin(speed * _Time.z) + 1) / 2;
				float fade = (sin(((1 - fadeDirection) * i.uv.y + fadeDirection* i.uv.x) * fadeAmount + time) + 1) / 2;


				shineColor = shineColor * colMask * effectPower * fade * timeConstant;
                colBase = colBase + shineColor;
                colBase.rgb *= colBase.a;
                return colBase;
            }
            ENDCG
        }
    }
}
