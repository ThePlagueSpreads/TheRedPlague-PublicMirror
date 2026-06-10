using UnityEngine;

namespace TheRedPlague.Framework.CreatureBehaviours;

public class WalkerGravity : MonoBehaviour
{
    public OnSurfaceTracker onSurface;
    public LiveMixin liveMixin;
    public Rigidbody rigidbody;

    private void FixedUpdate()
    {
        rigidbody.useGravity = false;
        bool isAboveWater = transform.position.y >= Ocean.GetOceanLevel();
        bool isAlive;
        if (onSurface.onSurface)
        {
            isAlive = liveMixin.IsAlive();
            if (isAlive)
            {
                var surfaceNormal = onSurface.surfaceNormal;
                rigidbody.AddForce(-surfaceNormal * 10f);
                // not even sure how to do this better
                goto IL_00a8;
            }
        }
        else
        {
            isAlive = false;
        }
        var gravity = isAboveWater ? 9.81f : 2.7f;
        rigidbody.AddForce(Vector3.down * (Time.fixedDeltaTime * gravity), ForceMode.VelocityChange);
        IL_00a8:
        var drag = isAlive ? 1.6f : 0.03f;
        if (!isAboveWater)
        {
            drag += 0.3f;
        }
        rigidbody.drag = drag;
    }
}