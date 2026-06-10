using UnityEngine;

namespace TheRedPlague.Content.Unused;

public class PlayEmperorDeathAnimation : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().Play("dead_idle", 0, 0);
    }
    
    private void OnEnable()
    {
        GetComponent<Animator>().Play("dead_idle", 0, 0);
    }
}