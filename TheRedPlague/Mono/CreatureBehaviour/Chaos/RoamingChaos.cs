namespace TheRedPlague.Mono.CreatureBehaviour.Chaos;

public class RoamingChaos : CreatureAction
{
    public float swimVelocity = 10f;

    private void Start()
    {
        LargeWorld.main.streamer.cellManager.UnregisterEntity(gameObject);
        Destroy(GetComponent<LargeWorldEntity>());
        transform.parent = null;

        RoamingChaosLeviathanManager.RegisterLeviathan(this);
        RoamingChaosLeviathanManager.CreateManagerIfNoneExists();
    }

    public override void Perform(Creature creature, float time, float deltaTime)
    {
        swimBehaviour.SwimTo(RoamingChaosLeviathanManager.GetFutureSwimPosition(transform.position, time),
            swimVelocity);
    }

    private void OnDestroy()
    {
        RoamingChaosLeviathanManager.UnregisterLeviathan(this);
    }
}