using TheRedPlague.Mono.Util;
using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Sucker;

public class AuroraSuckerBreachBlocker : MonoBehaviour
{
    private void OnEnable()
    {
        RadiationLeakSuckerObscuring.RegisterSucker(transform);
    }

    private void OnDisable()
    {
        RadiationLeakSuckerObscuring.UnregisterSucker(transform);
    }
}