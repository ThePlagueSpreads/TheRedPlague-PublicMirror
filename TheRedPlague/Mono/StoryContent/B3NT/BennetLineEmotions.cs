using System.Collections.Generic;

namespace TheRedPlague.Mono.StoryContent.B3NT;

public class BennetLineEmotions
{
    public BennetLineEmotions(IEnumerable<BennetEmotionFrame> emotions)
    {
        Emotions = emotions;
    }

    public IEnumerable<BennetEmotionFrame> Emotions { get; }
}