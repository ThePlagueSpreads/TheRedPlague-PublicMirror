using System;
using System.Collections;
using ECCLibrary;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Mono.StoryContent.PlagueHeart;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheRedPlague.PrefabFiles.StoryProps.MeteorSite;

public class MeteorBloodEmitterPrefab
{
    private static readonly int ColorStrengthAtNight = Shader.PropertyToID("_ColorStrengthAtNight");
    
    public PrefabInfo Info { get; }
    
    private Func<GameObject> GetModel { get; }
    private float BuryDepth { get; }

    public MeteorBloodEmitterPrefab(string classId, Func<GameObject> model, float buryDepth)
    {
        Info = PrefabInfo.WithTechType(classId);
        GetModel = model;
        BuryDepth = buryDepth;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private IEnumerator GetPrefab(IOut<GameObject> result)
    {
        var prefab = new GameObject(Info.ClassID);
        prefab.SetActive(false);

        var mover = new GameObject("Mover");
        mover.transform.SetParent(prefab.transform);
        
        var model = Object.Instantiate(GetModel.Invoke(), mover.transform);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Far);
        MaterialUtils.ApplySNShaders(model);
        
        model.transform.ZeroTransform();
        
        var gasopodTask = CraftData.GetPrefabForTechTypeAsync(TechType.Gasopod);
        yield return gasopodTask;
        var gasopodGas = gasopodTask.GetResult().GetComponent<GasoPod>().gasFXprefab;
        var mistPrefab = Object.Instantiate(gasopodGas, prefab.transform);
        mistPrefab.SetActive(false);
        foreach (var renderer in mistPrefab.GetComponentsInChildren<Renderer>(true))
        {
            renderer.material.color = new Color(0.3f, 0.04f, 0.04f, 0.65f);
            renderer.material.SetColor(ColorStrengthAtNight, Color.gray);
        }

        foreach (var ps in mistPrefab.GetComponentsInChildren<ParticleSystem>(true))
        {
            var main = ps.main;
            main.startSizeMultiplier *= 14f;
            main.startLifetimeMultiplier *= 11f;
            main.gravityModifier = 0.5f;
            main.gravityModifierMultiplier = -3;
            var sizeOverLifetime = ps.sizeOverLifetime;
            sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f,
                new AnimationCurve(new(0, 0.3f), new(1, 1)));
        }

        foreach (var trail in mistPrefab.GetComponentsInChildren<Trail_v2>(true))
        {
            trail.gameObject.SetActive(false);
        }

        var destroyAfterSeconds = mistPrefab.GetComponent<VFXDestroyAfterSeconds>();
        destroyAfterSeconds.lifeTime = 20f;

        mistPrefab.transform.Find("xGasopodSmoke/xSmkMesh").gameObject.SetActive(false);
        
        // end of mist set up
        
        mover.transform.localPosition = Vector3.down * BuryDepth;

        var emitter = prefab.AddComponent<MeteorMistSpawner>();
        emitter.mistPrefab = mistPrefab;
        emitter.moverRoot = mover.transform;
        
        result.Set(prefab);
    }
}