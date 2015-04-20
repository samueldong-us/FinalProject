float4x4 World;
float4x4 View;
float4x4 Projection;

sampler TextureSampler : register(s0);
texture2D Gradient;
sampler GradientSampler = sampler_state{ Texture = Gradient; };
float Amount;
struct PixelShaderInput
{
	float4 Color : COLOR0;
	float2 TextureCoordinate : TEXCOORD0;
};
float4 PixelShaderFunction(PixelShaderInput input) : COLOR0
{
	float4 textureColor = tex2D(TextureSampler, input.TextureCoordinate);
	float4 gradientColor = tex2D(GradientSampler, input.TextureCoordinate);
	if (gradientColor.r < Amount){
		textureColor.rgba = 0;
	}
	return textureColor * input.Color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}