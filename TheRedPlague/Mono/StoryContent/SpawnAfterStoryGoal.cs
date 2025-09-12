using System.Collections;
using Story;
using UnityEngine;
using UWE;

namespace TheRedPlague.Mono.StoryContent;

public class SpawnAfterStoryGoal : MonoBehaviour, IStoryGoalListener
{
    public string spawnClassId;
    public string storyGoalKey;

    private bool _listener;
    private bool _spawned;

    private void Start()
    {
        OnStartOrEnable();
    }

    private void OnEnable()
    {
        OnStartOrEnable();
    }

    private void OnStartOrEnable()
    {
        if (_spawned) return;
        var story = StoryGoalManager.main;
        if (story == null) return;
        if (story.IsGoalComplete(storyGoalKey))
        {
            Spawn();
            return;
        }
        if (_listener) return;
        story.AddListener(this);
        _listener = true;
    }

    public void NotifyGoalComplete(string key)
    {
        if (!_spawned && key == storyGoalKey)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        if (_spawned)
        {
            return;
        }

        CoroutineHost.StartCoroutine(SpawnAsyncAtPos(spawnClassId, transform.position, transform.rotation));

        _spawned = true;
        Destroy(gameObject);
    }

    private static IEnumerator SpawnAsyncAtPos(string classId, Vector3 pos, Quaternion rot)
    {
        var request = PrefabDatabase.GetPrefabAsync(classId);
        yield return request;
        if (request.TryGetPrefab(out var prefab))
        {
            var spawned = Instantiate(prefab, pos, rot);
            spawned.SetActive(true);
            LargeWorld.main.streamer.cellManager.RegisterEntity(spawned);
        }
        else
        {
            Plugin.Logger.LogWarning($"Failed to load prefab with Class ID '{classId}'!");
        }
    }

    private void OnDisable()
    {
        if (!_listener) return;
        var story = StoryGoalManager.main;
        if (story)
        {
            story.RemoveListener(this);
            _listener = false;
        }
    }
}