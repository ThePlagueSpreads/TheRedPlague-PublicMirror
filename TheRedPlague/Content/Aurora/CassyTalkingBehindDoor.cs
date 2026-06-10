using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using Nautilus.Utility;
using Story;
using UnityEngine;

namespace TheRedPlague.Content.Aurora;

public class CassyTalkingBehindDoor : MonoBehaviour, IScheduledUpdateBehaviour
{
    private static readonly CassyLine[] AllLines = {
        new(AudioUtils.GetFmodAsset("CassyBehindDoors0"), 20),
        new(AudioUtils.GetFmodAsset("CassyBehindDoors1"), 33),
        new(AudioUtils.GetFmodAsset("CassyBehindDoors2"), 16),
        new(AudioUtils.GetFmodAsset("CassyBehindDoors3"), 22),
        new(AudioUtils.GetFmodAsset("CassyBehindDoors4"), 12)
    };

    public float maxDistanceToPlayer = 15f;
    
    public int scheduledUpdateIndex { get; set; }

    private static SaveData _saveData;

    private float _timeCanPlayNextLine;

    public static void RegisterSaveData()
    {
        _saveData = SaveDataHandler.RegisterSaveDataCache<SaveData>();
    }
    
    public string GetProfileTag()
    {
        return "TRP:CassyTalkingBehindDoor";
    }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public void ScheduledUpdate()
    {
        if (!ShouldPlayNewLine())
        {
            return;
        }
        PlayNextLine();
    }

    private bool ShouldPlayNewLine()
    {
        // return if the event is already completed
        if (_saveData.linesPlayed >= AllLines.Length)
            return false;
        // return if a line has already just played or is still playing
        if (Time.time < _timeCanPlayNextLine)
            return false;
        // return if the player is out of the distance
        if (Vector3.SqrMagnitude(Player.main.transform.position - transform.position) >
            maxDistanceToPlayer * maxDistanceToPlayer)
            return false;
        // return if cassy has not talked to the player at all yet
        if (!StoryGoalManager.main.IsGoalComplete(Act2Story.CassyEncounter1.key))
            return false;
        return true;
    }

    private void PlayNextLine()
    {
        var nextLineToPlay = AllLines[_saveData.linesPlayed];
        Utils.PlayFMODAsset(nextLineToPlay.Sound, transform.position);
        _timeCanPlayNextLine = Time.time + nextLineToPlay.Duration;
        _saveData.linesPlayed++;
    }

    private class CassyLine
    {
        public FMODAsset Sound { get; }
        public float Duration { get; }

        public CassyLine(FMODAsset sound, float duration)
        {
            Sound = sound;
            Duration = duration;
        }
    }

    [FileName("CassyLinesBehindDoors")]
    private class SaveData : SaveDataCache
    {
        public int linesPlayed;
    }
}