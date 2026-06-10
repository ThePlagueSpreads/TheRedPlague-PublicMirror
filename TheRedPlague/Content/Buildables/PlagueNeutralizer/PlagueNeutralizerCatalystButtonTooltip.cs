using UnityEngine;

namespace TheRedPlague.Content.Buildables.PlagueNeutralizer;

public class PlagueNeutralizerCatalystButtonTooltip : MonoBehaviour, ITooltip
{
    public PlagueNeutralizerMachine machine;
    public bool insert;

    public void GetTooltip(TooltipData tooltip)
    {
        if (machine == null) return;
        tooltip.prefix.Append(GetText());
    }

    private string GetText()
    {
        if (!insert)
        {
            return Language.main.Get("PlagueNeutralizerRemoveCatalystTooltip");
        }
        
        if (machine.HasSpaceForCatalysts())
        {
            return Language.main.GetFormat("PlagueNeutralizerInsertCatalystTooltip", machine.GetCatalystsCount(),
                machine.catalysts.Capacity);
        }

        return Language.main.Get("PlagueNeutralizerInsertCatalystFullTooltip");
    }

    public bool showTooltipOnDrag => false;
}