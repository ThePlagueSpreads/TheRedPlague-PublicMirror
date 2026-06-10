using UnityEngine;
using UnityEngine.UI;

namespace TheRedPlague.Content.UI.AlterraRadioIcon;

public class ColorImageInLateUpdate : MonoBehaviour
{
    public Image image;
    public Color newColor;

    private void LateUpdate()
    {
        image.color = new Color(newColor.r, newColor.g, newColor.b, image.color.a);
    }
}