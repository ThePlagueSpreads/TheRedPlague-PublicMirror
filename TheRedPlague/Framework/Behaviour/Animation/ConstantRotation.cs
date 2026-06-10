using UnityEngine;

namespace TheRedPlague.Framework.Behaviour.Animation;

public class ConstantRotation : MonoBehaviour
{
    public Transform target;
    public Vector3 eulerRotation;
    public Space space;

    private void Update()
    {
        target.Rotate(eulerRotation * Time.deltaTime, space);
    }
}