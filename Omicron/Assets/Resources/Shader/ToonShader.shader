Shader "Omicron/Toon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		[HDR]
		_AmbientColour("Ambient Colour", Color) = (0.4,0.4,0.4,1)
		[HDR]
		_SpecularColour("Specular Colour", Color) = (0.9,0.9,0.9,2)
		_Glossiness("Glossiness", Float) = 32
		[HDR]
		_RimColour("Rim Colour", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0,1)) = 0.716
		_RimThreshold("Rim Threshold", Range(0,1)) = 0.1
	}
	SubShader
	{
		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct appdata
			{
				float3 normal : NORMAL;
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				return o;
			}
			
			float4 _Color;
			float4 _AmbientColour;
			float _Glossiness;
			float4 _SpecularColour;
			float4 _RimColour;
			float _RimAmount;
			float _RimThreshold;

			float4 frag (v2f i) : SV_Target
			{
				// Light intensity
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				float lightIntensity = smoothstep(0, 0.01, NdotL);
				float4 light = lightIntensity * _LightColor0;
				
				// Specular light
				float3 viewDir = normalize(i.viewDir);

				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);

				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColour;

				// Rim light
				float4 rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float rim = rimIntensity * _RimColour;

				float4 sample = tex2D(_MainTex, i.uv);

				return _Color * sample * (_AmbientColour + light + specular + rim);
			}
			ENDCG
		}
	}
}