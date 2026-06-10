using UnityEngine;

namespace TheRedPlague.Framework.Behaviour.Animation;

public class ConstantMotion : MonoBehaviour
{
    public Vector3 motionPerSecond;

    private void Update()
    {
        transform.position += motionPerSecond * Time.deltaTime;
    }
}