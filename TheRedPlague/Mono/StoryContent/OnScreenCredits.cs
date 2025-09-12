using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent;

public class OnScreenCredits : MonoBehaviour
{
    public float speed = 160;
    
    public static void Play()
    {
        var credits = Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("ScrollingCredits"));
        var rectTransform = credits.GetComponent<RectTransform>();
        rectTransform.SetParent(uGUI.main.transform.Find("ScreenCanvas"));
        rectTransform.localScale = Vector3.one;
        rectTransform.localPosition = new Vector3(-500, -750);
        credits.AddComponent<OnScreenCredits>();
    }

    private void Update()
    {
        transform.localPosition += Vector3.up * speed * Time.deltaTime;
    }
}