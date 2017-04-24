Shader "Custom/Molding Shader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Normal ("Normal", 2D) = "bump" {}
		_Occlusion ("Occlusion", 2D) = "white" {}
		_MoldMask ("Mold Mask", 2D) = "white" {}
		_MoldLevel("Mold Level", Range(0,0.99)) = 0.99
		_MoldHeight("Mold height", Range(0,2)) = 0.0
		_MoldColor ("Mold Color", Color) = (1,1,1,1)
		_MoldTex ("Mold Texture", 2D) = "white" {}
		_PositionMap ( "Position Map", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_MetallicMap ("Metallic", 2D) = "white" {}
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Treshhold ("Treshhold", Range(0,1)) = 0.5
		_HeightTreshhold("Height Treshhold", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows //vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MoldMask;
		sampler2D _MoldTex;
		sampler2D _PositionMap;
		sampler2D _MetallicMap;
		sampler2D _Normal;
		sampler2D _Occlusion;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MoldTex;
			float2 uv_MoldMask;
			float2 uv_PositionMap;
			float2 uv_MetallicMap;
			float2 uv_Normal;
			float2 uv_Occlusion;
		};

		float _Treshhold;
		half _HeightTreshhold;
		half _Glossiness;
		half _Metallic;
		half _MoldLevel;
		float _MoldHeight;
		fixed4 _Color;
		fixed4 _MoldColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color

			fixed4 position = tex2D(_PositionMap, IN.uv_PositionMap);
			fixed4 occlusion = tex2D(_Occlusion, IN.uv_Occlusion);
			fixed4 moldTex = tex2D(_MoldTex, IN.uv_MoldTex) * _MoldColor;
			fixed4 m = tex2D(_MoldMask, IN.uv_MoldMask);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 metallic = tex2D(_MetallicMap, IN.uv_MetallicMap);

			if (m.r < _Treshhold) {
				m.rgb = 0;
			}

			float h = clamp(position.g * _MoldHeight, 0, 1);
			
			if (h < _HeightTreshhold) {
				h = 0;
			}


			fixed4 mold = lerp(c, m.g * moldTex, h);
			c = normalize(lerp(c, mold, _MoldLevel));
			

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = occlusion.a;
			o.Occlusion = occlusion.r;
			o.Normal = UnpackNormal( tex2D (_Normal, IN.uv_Normal));
		}

		ENDCG
	}
	FallBack "Diffuse"
}
