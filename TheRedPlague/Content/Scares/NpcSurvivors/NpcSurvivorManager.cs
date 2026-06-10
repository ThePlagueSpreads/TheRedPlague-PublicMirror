using UnityEngine;

namespace TheRedPlague.Content.Scares.NpcSurvivors;

public class NpcSurvivorManager : MonoBehaviour
{
    public static NpcSurvivorManager main;

    private void Awake()
    {
        main = this;
    }
}