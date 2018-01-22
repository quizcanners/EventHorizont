// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/LensFlare" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Stage ("Stage", Range(0.001,1)) = 0.5

	}
	Category {
	Tags {  "Queue"="Overlay+1"
	 		"IgnoreProjector"="True" 
	 		"RenderType"="Transparent" }
	 		
Blend SrcAlpha One//	Blend SrcAlpha OneMinusSrcAlpha//
	ColorMask RGB
	Cull Off
	Lighting Off
	ZWrite Off
	ZTest Off
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			  #include "UnityCG.cginc"

			fixed4 _Color;
			half _Stage;
			  
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord-0.5;
				return o;
			}

			fixed4 frag (v2f i) : COLOR//SV_Target
			{
			//fixed4 col = tex2D(_MainTex, i.texcoord);
		float x=i.texcoord.x*i.texcoord.x;
			_Color.a=saturate(1-(x*x/_Stage+i.texcoord.y*i.texcoord.y*5096*_Stage)*16-_Stage);
			_Color.a+=saturate(1-(i.texcoord.x*i.texcoord.x+i.texcoord.y*i.texcoord.y+_Stage/4)*16);

				return _Color;
			}
			ENDCG 
		}
	}	
}
}
