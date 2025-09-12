using System;
using Story;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Mono.StoryContent.PlagueHeart;

public class MeteorMistSpawner : MonoBehaviour, IManagedUpdateBehaviour, IStoryGoalListener
{
    public int managedUpdateIndex { get; set; }
    public Transform moverRoot;
    public GameObject mistPrefab;
    public float unBuryMetersPerSecond = 1f;

    public float spawnDelayMin = 0.9f;
    public float spawnDelayMax = 2.3f;

    private float _timeNextSpawn;

    private bool _updateRegistered;
    private bool _canEmit;
    private bool _listenerRegistered;
    private bool _resurfaced;
    
    private string GoalKey => StoryUtils.OpenPlagueHeartHatchGoal.key;
    
    private void Start()
    {
        _canEmit = StoryGoalManager.main.IsGoalComplete(GoalKey);
        if (_canEmit)
        {
            moverRoot.localPosition = Vector3.zero;
            _resurfaced = true;
            if (!_updateRegistered)
            {
                BehaviourUpdateUtils.Register(this);
            
                _updateRegistered = true;
            }
        }
        else
        {
            if (!_listenerRegistered)
            {
                StoryGoalManager.main.AddListener(this);
                _listenerRegistered = true;
            }
        }
    }
    
    public void NotifyGoalComplete(string key)
    {
        if (!_canEmit && string.Equals(key, GoalKey, StringComparison.OrdinalIgnoreCase))
        {
            _canEmit = true;
            if (!_updateRegistered)
            {
                BehaviourUpdateUtils.Register(this);
                _updateRegistered = true;
            }
        }
    }

    private void OnDestroy()
    {
        if (_updateRegistered)
        {
            BehaviourUpdateUtils.Deregister(this);
            _updateRegistered = false;
        }
        if (_listenerRegistered)
        {
            StoryGoalManager.main.RemoveListener(this);
            _listenerRegistered = false;
        }
    }

    public void ManagedUpdate()
    {
        if (!isActiveAndEnabled) return;

        if (!_resurfaced)
        {
            moverRoot.localPosition = Vector3.MoveTowards(moverRoot.localPosition, Vector3.zero, unBuryMetersPerSecond * Time.deltaTime);
            _resurfaced = Vector3.SqrMagnitude(moverRoot.localPosition) <= Mathf.Epsilon;
            return;
        }
        
        if (Time.time > _timeNextSpawn)
        {
            var mist = Instantiate(mistPrefab, transform.position, Quaternion.identity);
            mist.SetActive(true);
            _timeNextSpawn = Time.time + Random.Range(spawnDelayMin, spawnDelayMax);
        }
    }

    public string GetProfileTag()
    {
        return "TRP:MeteorMistSpawner";
    }
}