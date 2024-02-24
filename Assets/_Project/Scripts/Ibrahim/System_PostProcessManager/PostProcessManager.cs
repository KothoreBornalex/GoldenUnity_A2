using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class PostProcessManager
{
    
    public static void SetChromaticAberration(bool value)
    {
        GameAssets.instance.VolumeProfile.components.ForEach(component =>
        {
            ChromaticAberration chromaticAberrationComponent = component as ChromaticAberration;
            if(chromaticAberrationComponent) component.active = value;
        });
    }

    public static void SetVignette(bool value)
    {
        GameAssets.instance.VolumeProfile.components.ForEach(component =>
        {
            Vignette vignetteComponent = component as Vignette;
            if (vignetteComponent) component.active = value;
        });
    }

    public static void SetBloom(bool value)
    {
        GameAssets.instance.VolumeProfile.components.ForEach(component =>
        {
            Bloom bloomComponent = component as Bloom;
            if (bloomComponent) component.active = value;
        });
    }
}
