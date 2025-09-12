using Story;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheRedPlague.Mono.VFX;

public class PrecursorSatelliteOrbit : MonoBehaviour
{
    public static PrecursorSatelliteOrbit Main { get; private set; }

    public Transform ringPivot;
    public float ringRotationAnglesPerSecond = 3;
    public Vector3 offset = new(300, 3100, -1000);

    private const string SpawnAnimationStoryGoal = "PrecursorSatelliteSpawn";
    private const float SpawnAnimationStartDistance = 16000;
    private const float SpawnAnimationDuration = 4f;
    private const float SpawnAnimationCurveExponent = 8;

    private bool _spawnAnimationPlaying;
    private float _spawnAnimationStartTime;

    private float _rotation;

    private Vector3 _customOffset;
    private bool _overridingOffset;

    private bool _overridingRotationSpeed;
    private float _overrideRotationSpeed;

    public void OverrideOffset(Vector3 newOffset)
    {
        _overridingOffset = true;
        _customOffset = newOffset;
        _spawnAnimationPlaying = false;
    }

    public void StopOverridingOffset()
    {
        _overridingOffset = false;
        _customOffset = default;
    }

    public void OverrideRotationSpeed(float speed)
    {
        _overridingRotationSpeed = true;
        _overrideRotationSpeed = speed;
    }

    public void StopOverridingRotationSpeed()
    {
        _overridingRotationSpeed = false;
    }
    
    private void Start()
    {
        if (Main != null)
        {
            Plugin.Logger.LogWarning("Two PrecursorSatellites found in scene. Destroying the duplicate one.");
            Destroy(Main.gameObject);
        }

        Main = this;
        if (StoryGoalManager.main && StoryGoalManager.main.OnGoalComplete(SpawnAnimationStoryGoal))
        {
            PlaySpawnAnimation();
        }
    }

    private void PlaySpawnAnimation()
    {
        _spawnAnimationPlaying = true;
        _spawnAnimationStartTime = Time.time;
    }

    private void Update()
    {
        _rotation += (_overridingRotationSpeed ? _overrideRotationSpeed : ringRotationAnglesPerSecond) * Time.deltaTime;
        ringPivot.localEulerAngles = new Vector3(270, _rotation, 0);
    }

    private void LateUpdate()
    {
        var currentOffset = GetOffset();
        var pos = MainCamera.camera.transform.position + currentOffset;
        pos.y = Mathf.Max(pos.y, Ocean.GetOceanLevel() + currentOffset.y);
        if (_spawnAnimationPlaying)
        {
            if (Time.time > _spawnAnimationStartTime + SpawnAnimationDuration)
            {
                _spawnAnimationPlaying = false;
            }
            else
            {
                pos.x += Mathf.Pow(Mathf.Clamp01(1f - (Time.time - _spawnAnimationStartTime) / SpawnAnimationDuration),
                    SpawnAnimationCurveExponent) * SpawnAnimationStartDistance;
            }
        }

        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }

    private Vector3 GetOffset()
    {
        if (_overridingOffset)
        {
            return _customOffset;
        }
        return offset;
    }
}