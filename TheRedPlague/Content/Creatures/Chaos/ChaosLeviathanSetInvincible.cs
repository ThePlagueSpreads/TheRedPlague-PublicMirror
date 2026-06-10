using UnityEngine;

namespace TheRedPlague.Content.Creatures.Chaos;

public class ChaosLeviathanSetInvincible : MonoBehaviour
{
    public LiveMixin liveMixin;

    private void Start()
    {
        liveMixin.invincible = true;
    }
}