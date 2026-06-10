using Nautilus.Utility;
using Story;
using TheRedPlague.Content.Creatures.Mutants;
using TheRedPlague.Framework.API.Saving;
using TheRedPlague.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Content.Scares;

public class LifePodScare : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float hatchHeight = 4;
    public float killWaitDuration = 2.9f;
    public float delayMultiplierAfterDeath = 2f;
    public float maxKillDistance = 13f;
    public int maxKnocksPerSave = 3;
    public int maxSightingsPerSave = 8;
    public float knockPercent = 0.4f;

    public float minInterval = 2.5f * 60;
    public float maxInterval = 4f * 60;

    private float _timerUntilNextScare = -1f;

    private bool _heldForSpawn;
    private bool _propExists;
    private GameObject _propInstance;
    private bool _propSeen;
    private float _timePropSeen;

    private static readonly FMODAsset KnockSound1 = AudioUtils.GetFmodAsset("TrpDoorKnockA");
    private static readonly FMODAsset KnockSound2 = AudioUtils.GetFmodAsset("TrpDoorKnockB");

    private const string DieFromLifePodScareGoal = "DieFromLifepodScare";

    private readonly Vector3[] _startPositions = new Vector3[]
    {
        new Vector3(-0.140f, 4.500f, 2.757f),
        new Vector3(-0.140f, 4.500f, -2.481f),
        // new Vector3(0.700f, 3.850f, 0.000f),
    };

    private Vector3[] _startAngles = new Vector3[]
    {
        new Vector3(270, 0, 0),
        new Vector3(270, 180, 0),
        // new Vector3(5, 90, 0),
    };

    private float _lastUpdateTime;

    public int scheduledUpdateIndex { get; set; }

    private void OnEnable()
    {
        if (_timerUntilNextScare < 0)
        {
            SetNewDelay();
        }

        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    private void Scare(int index)
    {
        if (_propInstance != null)
        {
            Destroy(_propInstance);
        }

        var obj = Instantiate(AssetBundles.Core.LoadAsset<GameObject>("MutantProp"), EscapePod.main.transform, true);
        obj.transform.localPosition = _startPositions[index];
        obj.transform.localEulerAngles = _startAngles[index];
        obj.AddComponent<SkyApplier>().renderers = obj.GetComponentsInChildren<Renderer>();
        MaterialUtils.ApplySNShaders(obj);
        _propInstance = obj;
        _propExists = true;
        _propSeen = false;
        
        // Try playing the knock sound
        var escapePod = Player.main.currentEscapePod;
        if (escapePod != null && CommonSaveCache.Data.LifepodKnockCount < maxKnocksPerSave)
        {
            var knockRandom = Random.value;
            if (knockRandom > knockPercent)
            {
                var knockSound = knockRandom < 0.66f ? KnockSound1 : KnockSound2;
                Utils.PlayFMODAsset(knockSound, escapePod.transform.position + Random.onUnitSphere * 10f);
                CommonSaveCache.Data.IncrementLifepodKnockCount();
            }
        }
    }

    public string GetProfileTag()
    {
        return "TRP:LifePodScare";
    }

    public void ScheduledUpdate()
    {
        if (IsDisabled())
            return;
        
        if (_propExists)
        {
            if (!_propSeen)
            {
                if (GenericTrpUtils.IsPositionOnScreen(GetHatchPosition(), 0.5f))
                {
                    _propSeen = true;
                    _timePropSeen = Time.time;
                    CommonSaveCache.Data.IncrementLifepodScareCount();
                }
            }
            else
            {
                if (!GenericTrpUtils.IsPositionOnScreen(GetHatchPosition(), 0.4f))
                {
                    Destroy(_propInstance);
                    _propExists = false;
                    _propSeen = false;
                }
                else if (Time.time > _timePropSeen + killWaitDuration && Vector3.Distance(MainCamera.camera.transform.position, _propInstance.transform.position) < maxKillDistance)
                {
                    if (!Plugin.Options.DisableJumpScares)
                    {
                        DeathScare.PlayMutantDeathScare("MutatedDiver3",
                            Mutant.Settings.HeavilyMutated | Mutant.Settings.Large);
                    }
                    Destroy(_propInstance, 0.2f);
                    _propExists = false;
                    _propSeen = false;

                    if (StoryGoalManager.main != null)
                    {
                        StoryGoalManager.main.OnGoalComplete(DieFromLifePodScareGoal);
                    }
                }
            }

            return;
        }

        if (_heldForSpawn)
        {
            TrySpawn();
            return;
        }

        if (Player.main.currentEscapePod == null)
        {
            _lastUpdateTime = Time.time;
            return;
        }

        // if that check fails, the player must be in the life pod
        _timerUntilNextScare -= Time.time - _lastUpdateTime;
        _lastUpdateTime = Time.time;
        if (_timerUntilNextScare >= 0)
            return;
        SetNewDelay();
        _heldForSpawn = true;
    }

    private void SetNewDelay()
    {
        float multiplier = 1f;
        if (StoryGoalManager.main != null && StoryGoalManager.main.IsGoalComplete(DieFromLifePodScareGoal))
        {
            multiplier = delayMultiplierAfterDeath;
        }
        _timerUntilNextScare = Random.Range(minInterval, maxInterval) * multiplier;
    }

    private Vector3 GetHatchPosition()
    {
        return transform.position + Vector3.up * hatchHeight;
    }

    private void TrySpawn()
    {
        if (Player.main.currentEscapePod != null &&
            GenericTrpUtils.IsPositionOnScreen(GetHatchPosition(), 0.1f))
            return;

        Scare(Random.Range(0, _startPositions.Length));
        _heldForSpawn = false;
    }

    private bool IsDisabled()
    {
        if (WaitScreen.IsWaiting)
            return true;
        if (CommonSaveCache.Data.LifepodScareCount > maxSightingsPerSave)
            return true;
        if (Plugin.Options.DisableJumpScares)
            return true;
        if (Player.main != null && Player.main.cinematicModeActive)
            return true;
        var lifepod = EscapePod.main;
        if (lifepod != null && lifepod.IsPlayingIntroCinematic())
            return true;
        return false;
    }
}