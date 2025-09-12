using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Mono.StoryContent;
using TheRedPlague.Mono.StoryContent.B3NT;
using TheRedPlague.Mono.Util;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Precursor;

public static class InfectedArchitectPrefab
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("B3-NT");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("InfectedArchitectPrefab"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        MaterialUtils.ApplySNShaders(obj, 5.7f, 30f, 3f);
        var destroyWhenFar = obj.AddComponent<DestroyWhenAwayFromLocation>();
        destroyWhenFar.maxDistance = 100f;
        destroyWhenFar.centerOfLocation = new Vector3(-1516, -850, 950);

        var controller = obj.AddComponent<BennetController>();
        controller.eyeTransform = obj.transform.Find("Pivot/B3NTCube/B3NTCubeEye");

        var animation = obj.AddComponent<BennetAnimations>();
        animation.rotationPivot = obj.transform.GetChild(0);
        animation.animator = obj.GetComponentInChildren<Animator>();

        var emotions = obj.AddComponent<BennetEmotionReactor>();
        emotions.controller = controller;
        controller.emotionReactor = emotions;
        
        controller.animations = animation;
        return obj;
    }
}