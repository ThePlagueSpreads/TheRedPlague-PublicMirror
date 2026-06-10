using UnityEngine;

namespace TheRedPlague.Framework.Extensions;

public static class GameObjectExtensions
{
    public static SphereCollider AddTriggerCollider(this GameObject go, float radius)
    {
        go.layer = LayerID.Trigger;
        var trigger = go.AddComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = radius;
        return trigger;
    }
}