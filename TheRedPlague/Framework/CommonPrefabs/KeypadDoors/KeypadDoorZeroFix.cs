using System.Collections;
using TMPro;
using UnityEngine;

namespace TheRedPlague.Framework.CommonPrefabs.KeypadDoors;

public class KeypadDoorZeroFix : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        var fiveButton = gameObject.transform.Find("KeypadUI/Keypad/ButtonHolder").GetChild(4);
        fiveButton.GetComponent<KeypadDoorConsoleButton>().index = 0;
        fiveButton.GetComponentInChildren<TextMeshProUGUI>().text = "0";
    }
}