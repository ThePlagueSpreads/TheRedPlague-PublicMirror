using System.Collections.Generic;

namespace TheRedPlague.Content.Act2.B3NT;

public class BennetLineEmotions
{
    public BennetLineEmotions(IEnumerable<BennetEmotionFrame> emotions)
    {
        Emotions = emotions;
    }

    public IEnumerable<BennetEmotionFrame> Emotions { get; }
}