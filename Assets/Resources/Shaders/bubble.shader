Shader "Custom/bubble" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_MusicIntensity("Music Intensity", Range(0,1)) = 0.0
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
		uniform float _MusicIntensity;

		struct v2f {
			float4 position : SV_POSITION;
		};

		v2f vert(float4 v:POSITION) : SV_POSITION{
			v2f o;
		o.position = mul(UNITY_MATRIX_MVP, v);
		return o;
		}

		// ---- change here kaleidoscope size and toggle on/off----
		const float USE_KALEIDOSCOPE = 1.0;
		const float NUM_SIDES = 8.0;

		// math const
		const float PI = 3.14159265359;
		const float DEG_TO_RAD = 0.01745329251; //PI / 180.0;


		float2 Kaleidoscope(float2 uv, float n, float bias) {
			float angle = PI / n;

			float r = length(uv*.5);
			float a = atan2(uv.x, uv.y) / angle;

			a = lerp(frac(a), 1.0 - frac(a), fmod(floor(a), 2.0)) * angle;

			return float2(cos(a), sin(a)) * r;
		}

		float mod(float x, float y)
		{
			return x - y * floor(x / y);
		}

			fixed4 frag(v2f i) : SV_Target{

			float2 uv = -1.0 + 2.0*i.position.xy / _ScreenParams.xy;
			uv.x *= _ScreenParams.x / _ScreenParams.y;
			float2 olduv = uv;
			//uv = lerp(uv, Kaleidoscope(olduv, NUM_SIDES, _Time.y * 10.0), 0.5);
			//uv = lerp(uv, float2(1.0,0.5), USE_KALEIDOSCOPE);
			float angle = 3.1415926535 / 8.0;
			float r = length(uv * 0.5);
			float a = atan2(uv.x, uv.y) / angle;
			a = lerp(frac(a), 1.0 - frac(a), mod(floor(a), 2.0)) * angle;
			uv = float2(cos(a), sin(a)) * r;
			//uv.y = 1.0 - uv.y;

			// Background
			float bgHue = 0.1;
			fixed4 outColour = fixed4(bgHue + 0.2*uv.y, bgHue + 0.2*uv.y, bgHue + 0.2*uv.y,1);

			// Bubbles
			for (int i = 0; i < 40; i++) {

				// Bubble seeds
				float pha = sin(float(i)*546.13 + 1.0)*0.5 + 0.5;
				float siz = pow(sin(float(i)*651.74 + 5.0)*0.5 + 0.5, 4.0);
				float pox = sin(float(i)*321.55 + 4.1);

				// Bubble size, position and color
				float rad = 0.1 + 0.5*siz * 4.0 * _MusicIntensity;
				

				float2  pos = float2(pox, -1.0 - rad + (2.0 + 2.0*rad)*fmod(pha + 0.6*_Time.y*(0.2 + 0.8*siz),1.0));
				float dis = length(uv - pos);
				//float3 col = lerp(float3(0.94,0.3,0.0), float3(0.1,0.4,0.8), 0.5 + 0.5*sin(float(i)*1.2 + 1.9));
				///float3 col = lerp(float3(0.9, 0.9, 0.9), float3(0.1, 0.1, 0.1), 0.5 + 0.5*sin(float(i)*1.2 + 1.9));
				float3 col = float3(0.0,0.0,0.0);
				col.r = lerp(0.1, 0.9, 0.5 + 0.5 * sin(float(i) * 123.0));
				col.g = lerp(0.1, 0.9, 0.5 + 0.5 * sin(float(i) * 151.0));
				col.b = lerp(0.1, 0.9, 0.5 + 0.5 * sin(float(i) * 253.0));

				// Add a black outline around each bubble
				//col += 8.0*smoothstep(rad*0.95, rad, dis);

				// Render
				float f = length(uv - pos) / rad;
				f = sqrt(clamp(1.0 - f*f,0.0,1.0));


				outColour.rgb += col.zyx *(1.0 - smoothstep(rad*0.95, rad, dis)) * f;
			}

			// Vignetting    
			outColour *= sqrt(1.5 - 0.7*length(uv));
			;

			return outColour;
		}

			ENDCG
		} 
	}
	FallBack "Diffuse"
}
