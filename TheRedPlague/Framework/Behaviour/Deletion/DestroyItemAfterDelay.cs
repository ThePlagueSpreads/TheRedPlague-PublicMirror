using System.Collections;
using TheRedPlague.Framework.Behaviour.Animation;
using TheRedPlague.Utilities;
using UnityEngine;
using UWE;

namespace TheRedPlague.Framework.Behaviour.Deletion;

public class DestroyItemAfterDelay : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float delay = 60f;
    public bool useBloodFx;
    public float bloodFxScale = 1f;
    public int bloodFxCount = 1;
    public float destroyDuration;

    private float _destroyTime;
    private bool _timedOut;

    public int scheduledUpdateIndex { get; set; }

    private Pickupable _pickupable;

    public string GetProfileTag()
    {
        return "TRP:DestroyItemAfterDelay";
    }

    private void Start()
    {
        _destroyTime = Time.time + delay;
        UpdateSchedulerUtils.Register(this);
        _pickupable = GetComponent<Pickupable>();
        if (_pickupable != null)
        {
            _pickupable.droppedEvent.AddHandler(gameObject, OnPickup);
        }
    }

    private void OnDestroy()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public void ScheduledUpdate()
    {
        if (_timedOut || Time.time < _destroyTime)
        {
            return;
        }

        _timedOut = true;

        if (ShouldDestroy())
        {
            if (useBloodFx)
            {
                CoroutineHost.StartCoroutine(SpawnBloodFx(transform.position));
            }
            Destroy(gameObject, destroyDuration);
        }
    }
    
    private void OnPickup(Pickupable p)
    {
        _timedOut = true;
    }

    private IEnumerator SpawnBloodFx(Vector3 position)
    {
        var result = new TaskResult<GameObject>();
        yield return BloodFxUtils.GetRedBloodFx(result);
        for (int i = 0; i < bloodFxCount; i++)
        {
            var blood = Instantiate(result.Get(), position, Quaternion.Euler(0, Random.value * 360, 0));
            blood.SetActive(true);
            foreach (var particleSystem in blood.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.transform.localScale *= bloodFxScale;   
            }

            blood.AddComponent<CopyObjectPosition>().target = transform;
            Destroy(blood, 5);
        }
    }

    private bool ShouldDestroy()
    {
        if (!isActiveAndEnabled)
            return false;
        var pickupable = GetComponent<Pickupable>();
        if (pickupable != null && global::Inventory.main.Contains(pickupable))
            return false;
        return true;
    }
}