using System;
using Nautilus.Utility;
using Story;
using UnityEngine;

namespace TheRedPlague.Content.Act2.B3NT;

public class BennetController : MonoBehaviour, IStoryGoalListener
{
    public static BennetController Main { get; private set; }
    
    public BennetAnimations animations;
    public BennetEmotionReactor emotionReactor;
    public Transform eyeTransform;

    private static readonly FMODAsset FirstMeetSound = AudioUtils.GetFmodAsset("B3NTFirstMeet");
    
    private void Start()
    {
        Main = this;
        animations.SetFirstMeet(!StoryGoalManager.main.IsGoalComplete(Act2Story.BennetApproach.key));
        StoryGoalManager.main.AddListener(this);
    }

    public void NotifyGoalComplete(string key)
    {
        if (string.Equals(Act2Story.BennetApproach.key, key, StringComparison.OrdinalIgnoreCase))
        {
            Utils.PlayFMODAsset(FirstMeetSound, transform.position);
            Invoke(nameof(PlayMeetAnimation), 2);
        }

        if (BennetEmotionsDatabase.TryGetEmotionsForLine(key, out var emotions))
        {
            emotionReactor.ReadLine(emotions);
        }
    }

    private void PlayMeetAnimation()
    {
        animations.SetFirstMeet(false);
        animations.PlayScanAnimation();
    }

    private void OnDestroy()
    {
        StoryGoalManager.main.RemoveListener(this);
    }
}