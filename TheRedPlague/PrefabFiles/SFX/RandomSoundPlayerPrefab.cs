using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Mono.SFX;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.SFX;

public class RandomSoundPlayerPrefab
{
    public PrefabInfo Info { get; }

    private FMODAsset SoundAsset { get; }
    private float MinDelay { get; }
    private float MaxDelay { get; }
    private float MaxDistance { get; }
    private bool PlayOnce { get; }
    private float ScreenShakeDuration { get; }

    public RandomSoundPlayerPrefab(PrefabInfo info, FMODAsset soundAsset, float minDelay, float maxDelay,
        float maxDistance, bool playOnce = false, float screenShakeDuration = -1f)
    {
        Info = info;
        SoundAsset = soundAsset;
        MinDelay = minDelay;
        MaxDelay = maxDelay;
        MaxDistance = maxDistance;
        PlayOnce = playOnce;
        ScreenShakeDuration = screenShakeDuration;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private GameObject GetPrefab()
    {
        var obj = new GameObject(Info.ClassID);
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        var behaviour = obj.AddComponent<RandomSoundPlayer>();
        behaviour.minDelay = MinDelay;
        behaviour.maxDelay = MaxDelay;
        behaviour.maxDistance = MaxDistance;
        behaviour.playOnce = PlayOnce;
        behaviour.useScreenShake = ScreenShakeDuration > 0;
        behaviour.screenShakeDuration = ScreenShakeDuration;
        var emitter = obj.AddComponent<FMOD_CustomEmitter>();
        emitter.SetAsset(SoundAsset);
        emitter.playOnAwake = false;
        emitter.SetAsset(SoundAsset);
        behaviour.emitter = emitter;
        return obj;
    }
}