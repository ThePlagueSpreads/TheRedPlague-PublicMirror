using UnityEngine;

namespace TheRedPlague.Content.Infection;

public class InfectOnStart : MonoBehaviour
{
    private void Start()
    {
        var infection = GetComponent<InfectedMixin>();
        if (infection.IsInfected())
            return;
        infection.SetInfectedAmount(4);
    }
}