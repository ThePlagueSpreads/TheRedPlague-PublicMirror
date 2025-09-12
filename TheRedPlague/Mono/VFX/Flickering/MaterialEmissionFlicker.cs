using UnityEngine;

namespace TheRedPlague.Mono.VFX.Flickering;

public class MaterialEmissionFlicker : FlickerTargetBase
{
    private static readonly int SpecInt = Shader.PropertyToID("_SpecInt");
    
    private readonly Material[] _materials;
    private readonly float[] _defaultIntensitiesDay;
    private readonly float[] _defaultIntensitiesNight;
    private readonly float[] _defaultSpecInt;

    private bool _useSpecInt;
    
    public MaterialEmissionFlicker(Material[] materials, bool useSpecInt)
    {
        _materials = materials;
        _useSpecInt = useSpecInt;
        
        _defaultIntensitiesDay = new float[_materials.Length];
        _defaultIntensitiesNight = new float[_materials.Length];
        
        if (_useSpecInt)
            _defaultSpecInt = new float[_materials.Length];
        
        for (int i = 0; i < _materials.Length; i++)
        {
            _defaultIntensitiesDay[i] = _materials[i].GetFloat(ShaderPropertyID._GlowStrength);
            _defaultIntensitiesNight[i] = _materials[i].GetFloat(ShaderPropertyID._GlowStrengthNight);
            if (_useSpecInt)
                _defaultSpecInt[i] = _materials[i].GetFloat(SpecInt);
        }
    }
    
    public override void SetIntensity(float intensity)
    {
        for (var i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetFloat(ShaderPropertyID._GlowStrength, _defaultIntensitiesDay[i] * intensity);
            _materials[i].SetFloat(ShaderPropertyID._GlowStrengthNight, _defaultIntensitiesNight[i] * intensity);
            if (_useSpecInt)
                _materials[i].SetFloat(SpecInt, _defaultSpecInt[i] * intensity);
        }
    }

    public override void ResetIntensity()
    {
        SetIntensity(1);
    }
}