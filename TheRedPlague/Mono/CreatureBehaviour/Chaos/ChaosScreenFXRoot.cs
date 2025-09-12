using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Chaos;

public class ChaosScreenFXRoot : MonoBehaviour
{
    public static ChaosScreenFXRoot main;

    private void Awake()
    {
        main = this;
    }
}