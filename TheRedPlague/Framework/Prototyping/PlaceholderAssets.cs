using System;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheRedPlague.Framework.Prototyping;

[PrefabClass]
public static class PlaceholderAssets
{
    [PrefabRegistration]
    private static void Register()
    {
        RegisterPrefab("PrototypeBlock",
            () => GameObject.CreatePrimitive(PrimitiveType.Cube));
    }

    private static void RegisterPrefab(string classId, Func<GameObject> getModel)
    {
        var prefab = new CustomPrefab(new PrefabInfo(classId, classId + "Prefab", TechType.None)
            .WithFolderPath(TrpPrefabFolders.Prototyping));
        prefab.SetGameObject(() =>
        {
            var model = getModel.Invoke();
            model.SetActive(false);
            PrefabUtils.AddBasicComponents(model, classId, TechType.None, LargeWorldEntity.CellLevel.Far);
            MaterialUtils.ApplySNShaders(model);
            return model;
        });
        prefab.Register();
    }
}