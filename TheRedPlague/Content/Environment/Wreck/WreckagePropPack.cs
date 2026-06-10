using System;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Utility;

namespace TheRedPlague.Content.Environment.Wreck;

[PrefabClass]
public static class WreckagePropPack
{
    [PrefabRegistration]
    private static void Register()
    {
        RegisterWreckProps();
    }
    
    private static void RegisterWreckProps()
    {
        var assetNames = AssetBundles.Core.GetAllAssetNames();
        foreach (var assetName in assetNames)
        {
            if (!assetName.EndsWith(".prefab") || !GetFileName(assetName).StartsWith("pf_")) continue;
            var classId = "RPWreck_" +
                          assetName.Substring(2, assetName.LastIndexOf(".prefab", StringComparison.Ordinal) - 2);
            var info = PrefabInfo.WithTechType(classId);
            var prefab = new CustomPrefab(info);
            var template = new AssetBundleTemplate(AssetBundles.Core, assetName, info);
            PrefabUtils.AddBasicComponents(template.Prefab, info.ClassID, info.TechType,
                LargeWorldEntity.CellLevel.Near);
            MaterialUtils.ApplySNShaders(template.Prefab, 7.34f);
            prefab.SetGameObject(template);
            prefab.Register();
        }
    }

    private static string GetFileName(string path)
    {
        var indexOfSlash = path.LastIndexOf("/", StringComparison.Ordinal);
        return indexOfSlash == -1 ? string.Empty : path.Substring(indexOfSlash + 1);
    }
}