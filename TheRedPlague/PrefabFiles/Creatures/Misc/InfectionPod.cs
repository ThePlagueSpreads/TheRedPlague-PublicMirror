using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using TheRedPlague.Data;
using TheRedPlague.Mono.CreatureBehaviour.Mimics;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Creatures.Misc;

public static class InfectionPod
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("InfectionPod")
        .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("InfectionPodIcon"));

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(new CloneTemplate(Info, TechType.GasPod)
        {
            ModifyPrefab = obj =>
            {
                var gasPod = obj.GetComponent<GasPod>();
                var infectionPod = obj.AddComponent<InfectionPodBehavior>();
                infectionPod.gasEffectPrefab = gasPod.gasEffectPrefab;
                infectionPod.mainCollider = gasPod.mainCollider;
                infectionPod.model = gasPod.model;
                infectionPod.burstSound = gasPod.burstSound;
                infectionPod.releaseSound = gasPod.releaseSound;
                Object.DestroyImmediate(gasPod);

                var renderers = obj.GetComponentsInChildren<Renderer>(true);

                foreach (var renderer in renderers)
                {
                    var materials = renderer.materials;
                    foreach (var material in materials)
                    {
                        material.color = Color.red * 0.4f;
                    }

                    renderer.materials = materials;
                }
            }
        });
        prefab.Register();
        CraftDataHandler.SetBackgroundType(Info.TechType, CustomBackgroundTypes.PlagueItem);
    }
}