using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using Story;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent;

public class DestroyPlagueHeartTimer : MonoBehaviour, IManagedUpdateBehaviour, IStoryGoalListener
{
    public static DestroyPlagueHeartTimer Main { get; private set; }

    public float maxDuration = 60 * 60; // One hour
    public float speedUpScale = 60f;
    public float speedUpTimeTargetMax = 8f * 60f;
    public float speedUpMinDuration = 6f * 60f;

    private static SaveData _data;

    public int managedUpdateIndex { get; set; }

    private DestroyPlagueHeartCountdownUI _countdownUI;

    private bool _speedingUp;

    private float _speedUpTarget;

    public static void RegisterSaveData()
    {
        _data = SaveDataHandler.RegisterSaveDataCache<SaveData>();
    }

    public static void SpeedUpTimer()
    {
        if (Main == null) return;
        Main.SpeedUp();
    }

    private void Start()
    {
        Main = this;
        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.Act2EndingEvent.key))
        {
            Destroy(gameObject);
            return;
        }

        _countdownUI = DestroyPlagueHeartCountdownUI.Create();
        StoryGoalManager.main.AddListener(this);
    }

    private void SpeedUp()
    {
        _speedingUp = true;
        _speedUpTarget = Mathf.Max(maxDuration - speedUpTimeTargetMax, _data.timePassed + speedUpMinDuration);
    }

    private void EndSpeedUp()
    {
        _speedingUp = false;
    }

    private void OnDestroy()
    {
        StoryGoalManager.main.RemoveListener(this);
        if (_countdownUI != null)
        {
            Destroy(_countdownUI.gameObject);
        }
    }

    private void OnEnable()
    {
        BehaviourUpdateUtils.Register(this);
    }

    private void OnDisable()
    {
        BehaviourUpdateUtils.Deregister(this);
    }

    public void ManagedUpdate()
    {
        if (_speedingUp)
        {
            _data.timePassed += Time.deltaTime * speedUpScale;
            if (_data.timePassed >= _speedUpTarget)
            {
                EndSpeedUp();
            }
        }
        else
        {
            _data.timePassed += Time.deltaTime;
        }

        if (_countdownUI) _countdownUI.UpdateDisplay(Mathf.RoundToInt(maxDuration - _data.timePassed));

        if (_data.timePassed >= maxDuration)
        {
            OnTimerEnded();
        }
    }

    private void OnTimerEnded()
    {
        BehaviourUpdateUtils.Deregister(this);
        if (!StoryGoalManager.main.IsGoalComplete(StoryUtils.ObeyBennetEvent.key))
        {
            StoryUtils.DisobeyBennetEvent.Trigger();
        }

        Destroy(gameObject);
    }

    public string GetProfileTag()
    {
        return "TRP:DestroyPlagueHeartTimer";
    }

    [FileName("DestroyPlagueHeartTimer")]
    private class SaveData : SaveDataCache
    {
        public float timePassed;
    }

    public void NotifyGoalComplete(string key)
    {
        if (key == StoryUtils.Act2EndingEvent.key)
        {
            Destroy(gameObject);
        }
    }
}