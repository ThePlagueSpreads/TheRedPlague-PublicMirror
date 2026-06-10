using System;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheRedPlague.Framework.CommonPrefabs;

public class StaticProp
{
    public PrefabInfo Info { get; }

    private readonly Func<GameObject> _model;
    private readonly LargeWorldEntity.CellLevel _cellLevel;

    public StaticProp(PrefabInfo info, Func<GameObject> model, LargeWorldEntity.CellLevel cellLevel)
    {
        Info = info;
        _model  = model;
        _cellLevel = cellLevel;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.Register();
    }

    private GameObject CreatePrefab()
    {
        var prefab = Object.Instantiate(_model.Invoke());
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, _cellLevel);
        MaterialUtils.ApplySNShaders(prefab);
        prefab.AddComponent<ConstructionObstacle>();
        return prefab;
    }
}