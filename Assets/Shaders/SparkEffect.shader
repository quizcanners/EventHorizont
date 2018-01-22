// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SparkEffect" {
		Properties {
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_portion ("_portion", Range(0,1)) = 0.02
		_TintColor ("Tint Color", Color) = (1,.5,.5,1)	
		//mater.SetColor("_TintColor", holder);
}

Category {
	Tags {  "Queue"="Overlay+1"
	 		"IgnoreProjector"="True" 
	 		"RenderType"="Transparent" }
	 		
	Blend SrcAlpha One//Blend SrcAlpha OneMinusSrcAlpha//Blend SrcAlpha One//
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

			sampler2D _MainTex;
			float _portion;
			fixed4 _TintColor;
			uniform sampler2D _CameraDepthTexture;
			  
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 projPos : TEXCOORD1;
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.projPos = ComputeScreenPos(o.vertex);
				return o;
			}

			fixed4 frag (v2f i) : COLOR//SV_Target
			{
			fixed4 col = tex2D(_MainTex, i.texcoord);
			
			fixed2 disp;
			disp.x=(col.r);
			disp.y=(col.g);
			disp=(disp-0.5)*(_portion)*col.a*4;
			col = tex2D(_MainTex, i.texcoord+disp);

 fixed test=tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(i.projPos)).r;
           	test=(LinearEyeDepth(test)-i.projPos.z-col.b*24);

 	 	_TintColor.a=col.a*(saturate(1-test*test/128)+saturate(test)/8);
 

			_TintColor.rgb*=(1-col.b-_portion);
			_TintColor.gb*=col.a;

				return _TintColor;
			}
			ENDCG 
		}
	}	
}
}
