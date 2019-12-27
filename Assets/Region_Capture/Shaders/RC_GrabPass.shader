Shader "Hidden/RegionCapture/GrabPass" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
}

SubShader {
	
	Pass {  
		Name "REGIONCAPTURE"
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD3;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD3;
			};

			sampler2D _GrabTexture;
			float4 _GrabTexture_ST;
			float _transoutofbounds;

			uniform half4 _Color;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _GrabTexture);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				half4 col = tex2D(_GrabTexture, i.texcoord);

				col *= _Color;
				if (_transoutofbounds > 0)
					if (i.texcoord.x < 0 || i.texcoord.x > 1 || i.texcoord.y < 0 || i.texcoord.y > 1) { col.a = 0; }
					else col.a = 1;

				return col;
			}
		ENDCG
		}
	}
}