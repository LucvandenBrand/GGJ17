Shader "Custom/bubble" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	/*SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}*/

	SubShader{

			Pass{
			CGPROGRAM

#pragma vertex vert
#pragma fragment frag

			struct v2f {
			float4 position : SV_POSITION;
		};

		v2f vert(float4 v:POSITION) : SV_POSITION{
			v2f o;
		o.position = mul(UNITY_MATRIX_MVP, v);
		return o;
		}

			fixed4 frag(v2f i) : SV_Target{

			float2 uv = -1.0 + 2.0*i.position.xy / _ScreenParams.xy;
			uv.x *= _ScreenParams.x / _ScreenParams.y;

			// Background
			fixed4 outColour = fixed4(0.8 + 0.2*uv.y,0.8 + 0.2*uv.y,0.8 + 0.2*uv.y,1);

			// Bubbles
			for (int i = 0; i < 40; i++) {

				// Bubble seeds
				float pha = sin(float(i)*546.13 + 1.0)*0.5 + 0.5;
				float siz = pow(sin(float(i)*651.74 + 5.0)*0.5 + 0.5, 4.0);
				float pox = sin(float(i)*321.55 + 4.1);

				// Bubble size, position and color
				float rad = 0.1 + 0.5*siz;
				float2  pos = float2(pox, -1.0 - rad + (2.0 + 2.0*rad)*fmod(pha + 0.1*_Time.y*(0.2 + 0.8*siz),1.0));
				float dis = length(uv - pos);
				float3 col = lerp(float3(0.94,0.3,0.0), float3(0.1,0.4,0.8), 0.5 + 0.5*sin(float(i)*1.2 + 1.9));

				// Add a black outline around each bubble
				col += 8.0*smoothstep(rad*0.95, rad, dis);

				// Render
				float f = length(uv - pos) / rad;
				f = sqrt(clamp(1.0 - f*f,0.0,1.0));

				outColour.rgb -= col.zyx *(1.0 - smoothstep(rad*0.95, rad, dis)) * f;
			}

			// Vignetting    
			outColour *= sqrt(1.5 - 0.5*length(uv));

			return outColour;
		}

			ENDCG
		} 
	}
	FallBack "Diffuse"
}
