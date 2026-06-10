using UnityEngine;

namespace TheRedPlague.Framework.UI;

public class SetBeaconColor : MonoBehaviour
{
    public int colorIndex;
    
    private void Start()
    {
        GetComponent<PingInstance>().SetColor(colorIndex);
    }
}