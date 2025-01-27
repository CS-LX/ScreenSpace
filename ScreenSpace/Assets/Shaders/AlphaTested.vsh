#version 300 es
#ifdef GLSL

// <Semantic Name='POSITION' Attribute='a_position' />
// <Semantic Name='COLOR' Attribute='a_color' />
// <Semantic Name='TEXCOORD' Attribute='a_texcoord' />

uniform vec2 u_origin;
uniform mat4 u_worldViewProjectionMatrix;
uniform mat4 u_viewProjectionMatrix;
uniform vec3 u_viewPosition;
uniform float u_fogYMultiplier;
uniform vec3 u_fogBottomTopDensity;
uniform vec2 u_hazeStartDensity;

in vec3 a_position;
in vec4 a_color;
in vec2 a_texcoord;

out vec4 v_color;
out vec2 v_texcoord;
out float v_fog;
out mat4 v_worldViewProjectionMatrix;
out vec3 v_position;

float fogIntegral(float y)
{
	return smoothstep(u_fogBottomTopDensity.x, u_fogBottomTopDensity.y, y) * (u_fogBottomTopDensity.y - u_fogBottomTopDensity.x) + u_fogBottomTopDensity.x;
}

float calculateFog(vec3 position)
{
	vec3 fogDelta = u_viewPosition - position;
	fogDelta.y *= u_fogYMultiplier;
	float fogDistance = length(fogDelta);
	float fogFactor = (fogIntegral(u_viewPosition.y) - fogIntegral(position.y)) / (u_viewPosition.y - position.y);
	return clamp(clamp(u_hazeStartDensity.y * (fogDistance - u_hazeStartDensity.x), 0.0, 1.0) + fogFactor * u_fogBottomTopDensity.z * fogDistance, 0.0, 1.0);
}

void main()
{
	// Texture
	v_texcoord = a_texcoord;

	// Vertex color
	v_color = a_color;
	
	// Fog
	v_fog = calculateFog(a_position);

	v_worldViewProjectionMatrix = u_worldViewProjectionMatrix;
	v_position = a_position.xyz;
	
	// Position
	gl_Position = u_viewProjectionMatrix * vec4(a_position.x - u_origin.x, a_position.y, a_position.z - u_origin.y, 1.0);

	// Fix gl_Position
	OPENGL_POSITION_FIX;
}

#endif
