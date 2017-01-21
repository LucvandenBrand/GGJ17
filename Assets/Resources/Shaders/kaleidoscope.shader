Shader "Custom/kaleidoscope" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
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

		// ---- change here kaleidoscope size and toggle on/off----
		const float USE_KALEIDOSCOPE = 1.0;
		const float NUM_SIDES = 8.0;

		// ---- Change the amount at which the visualizer reacts to music (per song different settings work best) ----
		const float MIN_SIZE = 0.01;
		const float REACT_INTENSITY = 0.3;
		const float BASS_INTENSITY = 1.1;
		const float BASS_BASE_HEIGHT = .0;
		const float ZOOM_FACTOR = 0.5;

		// math const
		const float PI = 3.14159265359;
		const float DEG_TO_RAD = 0.01745329251; //PI / 180.0;

		// -4/9(r/R)^6 + (17/9)(r/R)^4 - (22/9)(r/R)^2 + 1.0
		float field(float2 p, float2 center, float r) {
			float d = length(p - center) / r;

			float t = d  * d;
			float tt = t  * d;
			float ttt = tt * d;

			float v =
				(-10.0 / 9.0) * ttt +
				(17.0 / 9.0) * tt +
				(-22.0 / 9.0) * t +
				1.0;

			return clamp(v, 0.0, 1.0);
		}

		float2 Kaleidoscope(float2 uv, float n, float bias) {
			float angle = PI / n;

			float r = length(uv*.5);
			float a = atan2(uv.x, uv.y) / angle;

			a = lerp(frac(a), 1.0 - frac(a), fmod(floor(a), 2.0)) * angle;

			return float2(cos(a), sin(a)) * r;
		}

		void mainImage(out float4 fragColor, in float2 fragCoord)
		{
			float2 ratio = _ScreenParams.xy / min(_ScreenParams.x, _ScreenParams.y);
			float2 uv = (fragCoord.xy * 2.0 - _ScreenParams.xy) / min(_ScreenParams.x, _ScreenParams.y);

			// --- Kaleidoscope ---
			uv = lerp(uv, Kaleidoscope(uv, NUM_SIDES, _Time[1] * 10.), USE_KALEIDOSCOPE);

			uv *= ZOOM_FACTOR + (BASS_INTENSITY) * (Tex2D(_MainTex, float2(.01, 0)).x);

			float3 final_color = float3(0.0, 0.0, 0.0);
			float final_density = 0.0; 
			for (int i = 0; i < 128; i++) {
				float4 noise = Tex2D(_MainTex, float2(float(i) + 0.5, 0.5) / 256.0);
				float4 noise2 = Tex2D(_MainTex, float2(float(i) + 0.5, 9.5) / 256.0);


				//sound loudness to intensity
				float velintensity = Tex2D(iChannel1, float2(1.0 + uv.x, 0)).x;

				// velocity
				float2 vel = -abs(noise.xy) * 3.0 + float2(.003*-velintensity) - float2(2.0);
				vel *= -.5;
				 
				// center
				float2 pos = noise.xy;
				pos += iChannelTime[1] * vel * 0.2;
				pos = lerp(frac(pos), frac(pos), fmod(floor(pos), 2.0));

				//sound loudness to intensity
				float intensity = Tex2D(iChannel1, vec2(pos.y, 0)).x;

				// remap to screen
				pos = (pos * 2.0 - 1.0) * ratio;



				// radius
				float radius = clamp(noise.w, 0.3, 0.8);
				radius *= 3.*intensity;
				//radius += 0.5;
				radius *= radius * 0.4;
				radius = clamp(radius, MIN_SIZE*abs(-pos.x), REACT_INTENSITY);

				// color
				float3 color = noise2.xyz;

				// density
				float density = field(uv, pos, radius);

				// accumulate
				final_density += density;
				final_color += density * color;
			}

			final_density = clamp(final_density - 0.1, 0.0, 1.0);
			final_density = pow(final_density, 3.0);

			fragColor = float4(final_color * final_density, final_density);
		}

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
	}
	FallBack "Diffuse"
}
