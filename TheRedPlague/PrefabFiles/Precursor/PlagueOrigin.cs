using Nautilus.Assets;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Data;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Precursor;

public static class PlagueOrigin
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PlagueOrigin");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
        PDAHandler.AddCustomScannerEntry(Info.TechType, TechType.None, false, 1,
            5f, false, Info.ClassID);
        PDAHandler.AddEncyclopediaEntry(Info.ClassID, CustomPdaPaths.RedPlagueResearchPath, null, null,
            null, Plugin.AssetBundle.LoadAsset<Sprite>("PlagueOrigin_Popup"));
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("PlagueOriginPrefab"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(obj, 8f, 2f, 3f,
            new DoubleSidedModifier(MaterialUtils.MaterialType.Transparent));
        return obj;
    }

}