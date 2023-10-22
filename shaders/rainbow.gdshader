shader_type canvas_item;

vec3 rainbow(float level) {
	float r = float(level <= 2.0) + float(level > 4.0) * 0.5;
	float g = max(1.0 - abs(level - 2.0) * 0.5, 0.0);
	float b = (1.0 - (level - 4.0) * 0.5) * float(level >= 4.0);
	return vec3(r, g, b);
}

vec3 smoothRainbow(float y) {
	float yTime = y + fract(TIME);
	if (yTime >= 1.0) {
		yTime -= 1.0;
	}
	
	float level1 = floor(yTime * 6.0);
	float level2 = min(6.0, floor(yTime * 6.0) + 1.0);
	
	vec3 a = rainbow(level1);
	vec3 b = rainbow(level2);
	
	return mix(a, b, fract(yTime * 6.0));
}

void fragment() {
	COLOR = texture(TEXTURE, UV);
	vec3 rainbow = smoothRainbow(UV.y);
	COLOR.r = mix(rainbow.r, COLOR.r, 0.5);
	COLOR.g = mix(rainbow.g, COLOR.g, 0.5);
	COLOR.b = mix(rainbow.b, COLOR.b, 0.5);
}
