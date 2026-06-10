using UnityEngine;

namespace TheRedPlague.Content.Buildables.Shuttle;

public class ShuttleFlotationModel : MonoBehaviour
{
    public GameObject flotationDeviceParent;

    private void Start()
    {
        flotationDeviceParent.SetActive(!GetIsOnFoundation());
    }

    private bool GetIsOnFoundation()
    {
        var parent = transform.parent;
        
        if (parent == null)
            return false;

        return parent.GetComponent<SubRoot>() != null;
    }
}