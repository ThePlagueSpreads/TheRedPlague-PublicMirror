using System.Collections;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Utilities;

public static class BloodFxUtils
{
    private static GameObject _bloodFxCached;
    private static GameObject _redBloodFxCached;
    
    static BloodFxUtils()
    {
        SaveUtils.RegisterOnQuitEvent(OnQuit);
    }
    
    // Returns a DEACTIVATED instance of the vfx
    public static IEnumerator GetBloodFx(IOut<GameObject> result)
    {
        if (_bloodFxCached != null)
        {
            result.Set(_bloodFxCached);
        }

        var peeperTask = CraftData.GetPrefabForTechTypeAsync(TechType.Peeper);
        yield return peeperTask;
        var fx = UWE.Utils.InstantiateDeactivated(peeperTask.GetResult().GetComponent<LiveMixin>().data.damageEffect);
        foreach (var ps in fx.GetComponentsInChildren<ParticleSystem>())
        {
            var main = ps.main;
            main.scalingMode = ParticleSystemScalingMode.Local;
        }

        _bloodFxCached = fx;
        result.Set(_bloodFxCached);
    }
    
    // Returns a DEACTIVATED instance of the vfx
    public static IEnumerator GetRedBloodFx(IOut<GameObject> result)
    {
        if (_redBloodFxCached != null)
        {
            result.Set(_redBloodFxCached);
        }

        var peeperTask = CraftData.GetPrefabForTechTypeAsync(TechType.Peeper);
        yield return peeperTask;
        var fx = UWE.Utils.InstantiateDeactivated(peeperTask.GetResult().GetComponent<LiveMixin>().data.damageEffect);
        foreach (var ps in fx.GetComponentsInChildren<ParticleSystem>())
        {
            var main = ps.main;
            main.scalingMode = ParticleSystemScalingMode.Local;
            main.startColor = new Color(0.08f, 0, 0, 1.2f);
        }

        _redBloodFxCached = fx;
        result.Set(_redBloodFxCached);
    }
    
    private static void OnQuit()
    {
        Object.Destroy(_bloodFxCached);
        _bloodFxCached = null;
        
        Object.Destroy(_redBloodFxCached);
        _redBloodFxCached = null;
    }
}