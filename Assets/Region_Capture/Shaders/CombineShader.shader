Shader "RegionCapture/CombineShader"
{
    Properties
    {
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
		[NoScaleOffset] _UVTex1("UV Texture 1", 2D) = "white" {}
		[NoScaleOffset] _UVTex2("UV Texture 2", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
		[Space(20)][Toggle] _transoutofbounds("Set transparent if out of bounds", Float) = 0		
    }

    SubShader
    {
		Tags {"Queue" = "geometry-10" "RenderType" = "opaque" }

		UsePass "Custom/VideoBackground/VUFORIA"
		GrabPass { "_GrabTexture" }
		UsePass "Hidden/RegionCapture/GrabPass/REGIONCAPTURE"
    }
	Fallback "Legacy Shaders/Diffuse"
}
