using System;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Mono.StoryContent;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Special;

public class SpawnAfterStoryGoalPrefab
{
    private PrefabInfo Info { get; }
    private string SpawnClassId { get; }
    private Func<string> StoryGoalKey { get; }
    private LargeWorldEntity.CellLevel CellLevel { get; }
    
    public SpawnAfterStoryGoalPrefab(PrefabInfo info, string spawnClassId, Func<string> storyGoalKey, LargeWorldEntity.CellLevel cellLevel)
    {
        Info = info;
        SpawnClassId = spawnClassId;
        CellLevel = cellLevel;
        StoryGoalKey = storyGoalKey;
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
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, CellLevel);
        var spawn = obj.AddComponent<SpawnAfterStoryGoal>();
        spawn.spawnClassId = SpawnClassId;
        spawn.storyGoalKey = StoryGoalKey.Invoke();
        return obj;
    }
}