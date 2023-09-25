// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/VColorTransparent" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
}
	
Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	Lighting Off ZWrite Off

	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float3 uv0 : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float3 uv0 : TEXCOORD0;
};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.uv0 = v.uv0;
				return o;
			}
			
sampler2D _MainTex;
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, i.uv0.xy);
				i.color.rgb *= tex.rgb;
				i.color.a = tex.a;
	
				return i.color;
			}
			ENDCG 
		}
	}	
}
}
