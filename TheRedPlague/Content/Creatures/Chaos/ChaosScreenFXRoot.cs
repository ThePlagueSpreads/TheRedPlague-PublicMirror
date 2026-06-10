using UnityEngine;

namespace TheRedPlague.Content.Creatures.Chaos;

public class ChaosScreenFXRoot : MonoBehaviour
{
    public static ChaosScreenFXRoot main;

    private void Awake()
    {
        main = this;
    }
}