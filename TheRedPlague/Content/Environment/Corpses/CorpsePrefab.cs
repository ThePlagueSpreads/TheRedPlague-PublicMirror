using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Content.Creatures.Mutants;
using TheRedPlague.Content.Infection;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Corpses;

public class CorpsePrefab
{
    public CorpsePrefab(PrefabInfo info, string modelName, bool infected, bool moveTowardsPlayer = true)
    {
        Info = info;
        ModelName = modelName;
        Infected = infected;
        MoveTowardsPlayer = moveTowardsPlayer;
    }

    public PrefabInfo Info { get; }
    public string ModelName { get; }
    public bool Infected { get; }
    public bool MoveTowardsPlayer { get; }

    public void Register()
    {
        var infectedCorpse = new CustomPrefab(Info);
        infectedCorpse.SetGameObject(GetCorpsePrefab);
        infectedCorpse.Register();
    }

    private IEnumerator GetCorpsePrefab(IOut<GameObject> prefab)
    {
        var go = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>(ModelName));
        go.SetActive(false);
        PrefabUtils.AddBasicComponents(go, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(go);
        if (Infected)
        {
            var infect = go.AddComponent<InfectAnything>();
            infect.infectionAmount = 2.3f;
            infect.infectionHeightStrength = 0.01f;
        }

        foreach (var rb in go.GetComponentsInChildren<Rigidbody>(true))
        {
            rb.useGravity = false;
            var wf = rb.gameObject.EnsureComponent<WorldForces>();
            wf.useRigidbody = rb;
        }

        if (MoveTowardsPlayer)
        {
            go.AddComponent<MoveTowardsPlayerWhenOffScreen>();
        }

        yield return null;
        prefab.Set(go);
    }
}