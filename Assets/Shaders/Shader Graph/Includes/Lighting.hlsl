#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_UNCLUDED

void CalculateMainLight_float(float3 WorldPos, out float3 Direction, out float3 Color,
    out half distanceAttenuation, out half shadowAttenuation) {
    #if defined(SHADERGRAPH_PREVIEW)
        Direction = float3(0.5, 0.5, 0);
        Color = 1;
        distanceAttenuation = 1;
        shadowAttenuation = 1;
    #else
        #if SHADOWS_SCREEN
            half4 clipPos = TransformWorldToHClip(WorldPos);
            half4 shadowCoord = ComputeScreenPos(clipPos);
        #else
            half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
        #endif
        Light mainLight = GetMainLight(shadowCoord);
        Direction = mainLight.direction;
        Color = mainLight.color;
        distanceAttenuation = mainLight.distanceAttenuation;
        shadowAttenuation = mainLight.shadowAttenuation;
    #endif
}

void AddAdditionalLights_float(float Smoothness, float3 WorldPosition, float3 WorldNormal, float3 WorldView,
    float MainDiffuse, float MainSpecular, float3 MainColor, out float Diffuse, out float Specular, out float3 Color) {
    Diffuse = MainDiffuse;
    Specular = MainSpecular;
    Color = MainColor * (MainDiffuse + MainSpecular);

#ifndef SHADERGRAPH_PREVIEW
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; i++) {
        Light light = GetAdditionalLight(i, WorldPosition);
        half NdotL = saturate(dot(WorldNormal, light.direction));
        half attenuation = light.distanceAttenuation * light.shadowAttenuation;
        half thisDiffuse = attenuation * NdotL;
        half thisSpecular = LightingSpecular(thisDiffuse, light.direction, WorldNormal, WorldView, 1, Smoothness);
        Diffuse += thisDiffuse;
        Specular += thisSpecular;
        Color += light.color * (thisDiffuse + thisSpecular);
    }
    #endif
    half total = Diffuse + Specular;
    Color = total <= 0 ? MainColor : Color / total;
}
#endif