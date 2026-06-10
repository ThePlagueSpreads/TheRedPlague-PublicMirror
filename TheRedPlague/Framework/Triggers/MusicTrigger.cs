using TheRedPlague.Framework.SFX;

namespace TheRedPlague.Framework.Triggers;

public class MusicTrigger : PlayerTrigger
{
    public FMODAsset music;
    public float musicDuration;
    public bool highPriority;

    public void SetUp(FMODAsset music, float musicDuration)
    {
        this.music = music;
        this.musicDuration = musicDuration;
    }
    
    protected override void OnTriggerActivated()
    {
        TrpEventMusicPlayer.PlayMusic(music, musicDuration, highPriority);
    }
}