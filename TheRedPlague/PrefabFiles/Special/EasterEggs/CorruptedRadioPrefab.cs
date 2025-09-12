using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Special.EasterEggs;

public static class CorruptedRadioPrefab
{
    private static PrefabInfo Info { get; } = PrefabInfo.WithTechType("CorruptedRadio")
        .WithIcon(SpriteManager.Get(TechType.Radio));
    
    private static readonly FMODAsset Music = AudioUtils.GetFmodAsset("CorruptedRadioMusic");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        var template = new CloneTemplate(Info, TechType.Radio)
        {
            ModifyPrefab = obj =>
            {
                Object.DestroyImmediate(obj.GetComponent<Constructable>());
                Object.DestroyImmediate(obj.GetComponent<Radio>());
                Object.DestroyImmediate(obj.GetComponent<VoiceNotification>());
                Object.DestroyImmediate(obj.GetComponent<FMOD_StudioEventEmitter>());
                Object.DestroyImmediate(obj.GetComponent<FMODASRPlayer>());
                Object.DestroyImmediate(obj.GetComponent<FMOD_CustomEmitter>());
                var emitter = obj.AddComponent<FMOD_CustomLoopingEmitter>();
                emitter.followParent = true;
                emitter.SetAsset(Music);
                emitter.playOnAwake = true;
                obj.AddComponent<Pickupable>();
                PrefabUtils.AddWorldForces(obj, 10, isKinematic: true);
                obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            }
        };
        prefab.SetGameObject(template);
        prefab.Register();
    }
}