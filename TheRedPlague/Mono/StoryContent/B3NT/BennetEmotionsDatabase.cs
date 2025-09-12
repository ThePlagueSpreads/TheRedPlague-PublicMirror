using System.Collections.Generic;

namespace TheRedPlague.Mono.StoryContent.B3NT;

public static class BennetEmotionsDatabase
{
    // Key : Log entry ID
    private static readonly Dictionary<string, BennetLineEmotions> Data = new()
    {
        {
            "B3NT_Introduction", new BennetLineEmotions(new[]
            {
                new BennetEmotionFrame(0, 0f, false),
                new BennetEmotionFrame(4990, 0.1f, false),
                new BennetEmotionFrame(9082, 0f, false),
                new BennetEmotionFrame(10400, 0.2f, false),
                new BennetEmotionFrame(12000, 0.3f, false),
                new BennetEmotionFrame(1400, 0f, false),
                new BennetEmotionFrame(14500, 0.1f, false),
                new BennetEmotionFrame(18400, 0.05f, false),
                new BennetEmotionFrame(22791, 1f, false),
                new BennetEmotionFrame(23800, 0.2f, false),
                new BennetEmotionFrame(27314, 0f, false),
                new BennetEmotionFrame(30000, 0.3f, false),
                new BennetEmotionFrame(33000, 0.4f, false),
                new BennetEmotionFrame(36000, 0f, false),
                new BennetEmotionFrame(39500, 0f, true),
                new BennetEmotionFrame(41567, 0f, true),
                new BennetEmotionFrame(42100, 0.3f, true),
                new BennetEmotionFrame(46000, 1f, true),
                new BennetEmotionFrame(49000, 0.01f, false),
                new BennetEmotionFrame(54000, 0f, false),
                new BennetEmotionFrame(61433, 1f, false),
                new BennetEmotionFrame(64000, 0f, true),
                new BennetEmotionFrame(68500, 0.1f, true),
                new BennetEmotionFrame(71774, 0.3f, true),
                new BennetEmotionFrame(74000, 0f, false)
            })
        },
        {
            "B3NT_CatalystResponse", new BennetLineEmotions(new []
            {
                new BennetEmotionFrame(0, 0f, true),
                new BennetEmotionFrame(7000, 0f, false),
                new BennetEmotionFrame(8000, 0.1f, false),
                new BennetEmotionFrame(11500, 0.5f, false),
                new BennetEmotionFrame(15000, 0.8f, false),
                new BennetEmotionFrame(18000, 0.2f, false),
                new BennetEmotionFrame(19000, 0.1f, true),
                new BennetEmotionFrame(19000, 0.1f, true),
                new BennetEmotionFrame(27000, 0.3f, false),
                new BennetEmotionFrame(31000, 1f, false),
                new BennetEmotionFrame(32000, 0.3f, false)
            })
        }
    };

    public static bool TryGetEmotionsForLine(string key, out BennetLineEmotions lineEmotions)
    {
        return Data.TryGetValue(key, out lineEmotions);
    }

    public static void RegisterEmotionsForLine(string key, BennetLineEmotions lineEmotions) =>
        Data[key] = lineEmotions;

    public static void UnregisterEmotionsForLine(string key) => Data.Remove(key);
}