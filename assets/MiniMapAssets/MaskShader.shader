Shader "Custom/MaskShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Mask ("Mask Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 200
		Lighting On
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			SetTexture [_Mask] {combine texture}
			SetTexture [_MainTex] {combine texture, previous}
		}
	} 
}
