using Nautilus.Utility;
using Story;
using TheRedPlague.Mono.CreatureBehaviour.Mutants;
using TheRedPlague.PrefabFiles.Creatures;
using TheRedPlague.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Mono.VFX;

public class LifePodScare : MonoBehaviour, IScheduledUpdateBehaviour
{
    private const float HatchHeight = 4;
    private const float KillWaitDuration = 2.9f;
    private const float DelayMultiplierAfterDeath = 2f;
    private const float MaxKillDistance = 13f;

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

        var obj = Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("MutantProp"), EscapePod.main.transform, true);
        obj.transform.localPosition = _startPositions[index];
        obj.transform.localEulerAngles = _startAngles[index];
        obj.AddComponent<SkyApplier>().renderers = obj.GetComponentsInChildren<Renderer>();
        MaterialUtils.ApplySNShaders(obj);
        _propInstance = obj;
        _propExists = true;
        _propSeen = false;
        
        // Try playing the knock sound
        var escapePod = Player.main.currentEscapePod;
        if (escapePod != null)
        {
            var knockRandom = Random.value;
            if (knockRandom > 0.33f)
            {
                var knockSound = knockRandom < 0.66f ? KnockSound1 : KnockSound2;
                Utils.PlayFMODAsset(knockSound, escapePod.transform.position + Random.onUnitSphere * 10f);
            }
        }
    }

    public string GetProfileTag()
    {
        return "TRP:LifePodScare";
    }

    public void ScheduledUpdate()
    {
        if (WaitScreen.IsWaiting || (Player.main != null && Player.main.cinematicModeActive))
            return;
        
        if (_propExists)
        {
            if (!_propSeen)
            {
                if (GenericTrpUtils.IsPositionOnScreen(GetHatchPosition(), 0.5f))
                {
                    _propSeen = true;
                    _timePropSeen = Time.time;
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
                else if (Time.time > _timePropSeen + KillWaitDuration && Vector3.Distance(MainCamera.camera.transform.position, _propInstance.transform.position) < MaxKillDistance)
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
            multiplier = DelayMultiplierAfterDeath;
        }
        _timerUntilNextScare = Random.Range(minInterval, maxInterval) * multiplier;
    }

    private Vector3 GetHatchPosition()
    {
        return transform.position + Vector3.up * HatchHeight;
    }

    private void TrySpawn()
    {
        if (Player.main.currentEscapePod != null &&
            GenericTrpUtils.IsPositionOnScreen(GetHatchPosition(), 0.1f))
            return;

        Scare(Random.Range(0, _startPositions.Length));
        _heldForSpawn = false;
    }
}