using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.PhantomLeviathan;

public class PhantomLeashFix : MonoBehaviour, IScheduledUpdateBehaviour
{
    public Creature creature;
    
    private static readonly Vector3 FleshCaveLeashPosition = new Vector3(-1511, -822, 517);
    private static readonly string FleshCavePhantomID = "3f6f2c71-74e9-44c6-8c15-bf6e4cd5efef";
    
    public int scheduledUpdateIndex { get; set; }

    private bool _registered;

    private void OnEnable()
    {
        var identifier = GetComponent<PrefabIdentifier>();
        
        if (identifier == null || identifier.ClassId != FleshCavePhantomID)
        {
            return;
        }
        
        UpdateSchedulerUtils.Register(this);
        _registered = true;
    }
    
    private void OnDisable()
    {
        if (_registered)
        {
            UpdateSchedulerUtils.Deregister(this);
            _registered = false;
        }
    }

    public string GetProfileTag()
    {
        return "TRP:PhantomLeashFix";
    }

    public void ScheduledUpdate()
    {
        creature.leashPosition = FleshCaveLeashPosition;
    }
}