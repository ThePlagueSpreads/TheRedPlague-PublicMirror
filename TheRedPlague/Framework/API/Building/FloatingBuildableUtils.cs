using System.Collections.Generic;

namespace TheRedPlague.Framework.API.Building;

public static class FloatingBuildableUtils
{
    internal static HashSet<TechType> FloatingTechTypes = new();
    internal static Dictionary<TechType, float> FloatingTechTypeOffsets = new();

    public static void RegisterBuildableAsFloating(TechType techType, float offset)
    {
        FloatingTechTypes.Add(techType);
        FloatingTechTypeOffsets.Add(techType, offset);
    }

    public static void UnregisterBuildableAsFloating(TechType techType)
    {
        FloatingTechTypes.Remove(techType);
        FloatingTechTypeOffsets.Remove(techType);
    }

    public static float GetSeaLevelForTechType(TechType techType)
    {
        var seaLevel = Ocean.GetOceanLevel();
        if (FloatingTechTypeOffsets.TryGetValue(Builder.constructableTechType, out var offset))
            seaLevel += offset;
        return seaLevel;
    }
}