using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Chaos;

public class ChaosLeviathanSetInvincible : MonoBehaviour
{
    public LiveMixin liveMixin;

    private void Start()
    {
        liveMixin.invincible = true;
    }
}