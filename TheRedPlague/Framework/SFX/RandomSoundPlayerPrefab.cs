using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Framework.SFX;

public class RandomSoundPlayerPrefab
{
    public PrefabInfo Info { get; }

    private FMODAsset SoundAsset { get; }
    private float MinDelay { get; }
    private float MaxDelay { get; }
    private float MaxDistance { get; }
    private RepeatMode RepeatMode { get; }
    private float ScreenShakeDuration { get; }

    public RandomSoundPlayerPrefab(PrefabInfo info, FMODAsset soundAsset, float minDelay, float maxDelay,
        float maxDistance, RepeatMode repeatMode = RepeatMode.Endless, float screenShakeDuration = -1f)
    {
        Info = info;
        SoundAsset = soundAsset;
        MinDelay = minDelay;
        MaxDelay = maxDelay;
        MaxDistance = maxDistance;
        RepeatMode = repeatMode;
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
        behaviour.repeatMode = RepeatMode;
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