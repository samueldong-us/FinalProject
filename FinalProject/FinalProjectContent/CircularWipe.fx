float4x4 World;
float4x4 View;
float4x4 Projection;

sampler TextureSampler : register(s0);
texture2D Gradient;
sampler GradientSampler = sampler_state{ Texture = Gradient; };
float Amount;

float4 PixelShaderFunction(float2 TextureCoordinate : TEXCOORD0) : COLOR0
{
	float4 textureColor = tex2D(TextureSampler, TextureCoordinate);
	float4 gradientColor = tex2D(GradientSampler, TextureCoordinate);
	if (gradientColor.r < Amount){
		textureColor.a = 0;
	}
	return textureColor;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}