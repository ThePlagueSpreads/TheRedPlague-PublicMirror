using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheRedPlague.Content.Act1.DomeDrones;

public class DroneProgressBar : MonoBehaviour
{
    public Image maskImage;
    public TextMeshProUGUI text;

    private void Update()
    {
        var domeDeconstructionManager = DomeDeconstructionManager.main;
        var progress = domeDeconstructionManager != null ? domeDeconstructionManager.CurrentProgress : 0;
        maskImage.fillAmount = Mathf.Clamp01(progress);
        text.text = Language.main.GetFormat("DomeDroneDeconstructProgress",
            Mathf.FloorToInt(progress * 100));
    }
}