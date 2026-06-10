using UnityEngine;

namespace TheRedPlague.Content.Environment.Lights;

[PrefabClass]
public static class RedPlagueLights
{
    [PrefabRegistration]
    private static void RegisterLights()
    {
        var lights = new LightPrefab[]
        {
            new("RedPlagueLight1Low", Color.red, 1, 6),
            new("RedPlagueLight1LowBright", Color.red, 2, 6),
            new("RedPlagueLight1Medium", Color.red, 1, 10),
            new("RedPlagueLight1MediumBright", Color.red, 2, 10),
            new("RedPlagueLight1High", Color.red, 1, 14),
            new("RedPlagueLight1HighBright", Color.red, 2, 14),
            new("RedPlagueLight1Massive", Color.red, 1, 40),
            new("RedPlagueLight1MassiveBright", Color.red, 2, 40),

            new("RedPlagueLight2Low", Color.magenta, 1, 6),
            new("RedPlagueLight2LowBright", Color.magenta, 2, 6),
            new("RedPlagueLight2Medium", Color.magenta, 1, 10),
            new("RedPlagueLight2MediumBright", Color.magenta, 2, 10),
            new("RedPlagueLight2High", Color.magenta, 1, 14),
            new("RedPlagueLight2HighBright", Color.magenta, 2, 14),
            new("RedPlagueLight2Massive", Color.magenta, 1, 40),
            new("RedPlagueLight2MassiveBright", Color.magenta, 2, 40)
        };

        foreach (var light in lights)
        {
            light.Register();
        }
    }
}