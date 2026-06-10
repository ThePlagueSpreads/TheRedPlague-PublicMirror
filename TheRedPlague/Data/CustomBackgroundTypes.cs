using Nautilus.Handlers;
using UnityEngine;

namespace TheRedPlague.Data;

public static class CustomBackgroundTypes
{
    public static CraftData.BackgroundType PlagueItem { get; private set; }

    public static void RegisterCustomBackgroundTypes()
    {
        var itemBackground = AssetBundles.Core.LoadAsset<Sprite>("PlagueItemBackground");
        PlagueItem = EnumHandler.AddEntry<CraftData.BackgroundType>("PlagueItem")
            .WithBackground(itemBackground);
    }
}