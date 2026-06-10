using UnityEngine;

namespace TheRedPlague.Content.Act3.DomeBase;

public class ForcePlayerScaleWhileInDome : MonoBehaviour
{
    public Vector3 scale;

    private void Update()
    {
        transform.localScale = scale;
    }
}