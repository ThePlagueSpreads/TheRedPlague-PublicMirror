namespace TheRedPlague.Mono.StoryContent.B3NT;

public readonly struct BennetEmotionFrame
{
    public int Timestamp { get; }
    public float GlitchAmount { get; }
    public bool Open { get; }

    public BennetEmotionFrame(int timestamp, float glitchAmount, bool open)
    {
        Timestamp = timestamp;
        GlitchAmount = glitchAmount;
        Open = open;
    }
}