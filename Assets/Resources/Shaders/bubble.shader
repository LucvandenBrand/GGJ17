Shader "Custom/bubble" 
{
	Properties 
	{
		_MusicIntensity("Music Intensity", Range(0,1)) = 0.0
		_NumSides("Kaleidoscope Sides", Range(1,16)) = 8.0
	}
	
	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			// Controlling uniforms
			uniform float _MusicIntensity;
			uniform float _NumSides;

			// Useful Math constants
			static const float PI = 3.14159265359;

			// Vertex structure, we only require position.
			struct v2f 
			{
				float4 position : SV_POSITION;
			};

			// Vertex shader.
			v2f vert(float4 v:POSITION)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v);
				return o;
			}

			float mod(float x, float y)
			{
				return x - y * floor(x / y);
			}

			// Fragment shader
			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = -1.0 + 2.0*i.position.xy / _ScreenParams.xy;
				uv.x *= _ScreenParams.x / _ScreenParams.y;
				float2 olduv = uv;

				float angle = PI / _NumSides;
				float r = length(uv * 0.5);
				float a = atan2(uv.x, uv.y) / angle;
				a = lerp(frac(a), 1.0 - frac(a), mod(floor(a), 2.0)) * angle;
				uv = float2(cos(a), sin(a)) * r;

				// Background
				float bgHue = 0.1;
				fixed4 outColour = fixed4(bgHue + 0.2*uv.y, bgHue + 0.2*uv.y, bgHue + 0.2*uv.y,1);

				// Bubbles
				for (int i = 0; i < 40; i++) 
				{
					// Bubble seeds
					float pha = sin(float(i)*546.13 + 1.0)*0.5 + 0.5;
					float siz = pow(sin(float(i)*651.74 + 5.0)*0.5 + 0.5, _MusicIntensity);
					float pox = sin(float(i)*321.55 + 4.1);

					// Bubble size, position and color
					float rad = 0 + 0.5 * siz * 2.0 * _MusicIntensity;
				
					float2  pos = float2(pox, -1.0 - rad + (2.0 + 2.0*rad)*fmod(pha + 0.6 * _Time.y * (0.2 + 0.1 * siz), 1.0));
					float dis = length(uv - pos);

					float3 col = float3(0.0,0.0,0.0);
					col.r = lerp(0.1, 0.9, 0.5 + 0.5 * sin(float(i) * 123.0));
					col.g = lerp(0.1, 0.9, 0.5 + 0.5 * sin(float(i) * 151.0));
					col.b = lerp(0.1, 0.9, 0.5 + 0.5 * sin(float(i) * 253.0));

					// Render
					float f = length(uv - pos) / rad;
					f = sqrt(clamp(1.0 - f*f,0.0,1.0));

					outColour.rgb += col.zyx *(1.0 - smoothstep(rad*0.95, rad, dis)) * f;
				}

				// Vignetting    
				outColour *= sqrt(1.5 - 0.7*length(uv));

				return outColour;
			}

			ENDCG
		} 
	}
	FallBack "Diffuse"
}