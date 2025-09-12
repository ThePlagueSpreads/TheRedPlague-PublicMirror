using System.Collections;
using System.Linq;
using Nautilus.Utility;
using TheRedPlague.Mono.Insanity.Symptoms.Bases;
using TheRedPlague.Mono.VFX;
using TheRedPlague.Mono.VFX.Flickering;
using UnityEngine;
using UWE;
using Random = UnityEngine.Random;

namespace TheRedPlague.Mono.Insanity.Symptoms;

public class FakeSignalHallucination : TimedHallucinationSymptom
{
    protected override float MinInterval => 180;
    protected override float MaxInterval => 240;
    protected override float MinInsanity => 40;
    protected override float MaxInsanity => 90;
    protected override float ChanceAtMinInsanity => 0.1f;
    protected override float ChanceAtMaxInsanity => 0.85f;

    private const float MinLingerDuration = 20f;
    private const float MaxLingerDuration = 60f;
    private const float MinLifePodLingerDuration = 80f;
    private const float MaxLifePodLingerDuration = 200f;
    private const float MinDepth = 20f;
    private const float MinRadius = 170f;
    private const float MaxRadius = 400;
    private const float DepthIncrease = 100f;
    private const float LifePodSpawnChance = 0.25f;
    private const float MaxDepthForLifePods = 200f;
    private const int MinLifePodNumber = 1;
    private const int MaxLifePodNumber = 23;
    private const float UseBlueIconChance = 0.6f;
    private const float LifePodHasNameChance = 0.5f;

    private PingType[] _pingTypes;

    private static readonly FMODAsset[] ApproachSounds =
    {
        AudioUtils.GetFmodAsset("UnlockTurretScream"),
        AudioUtils.GetFmodAsset("TrpCaveSounds"),
        AudioUtils.GetFmodAsset("CyclopsBaseLightFlicker"),
        AudioUtils.GetFmodAsset("SkeletonAmbience"),
        AudioUtils.GetFmodAsset("WarperJumpscare")
    };

    private static readonly string[] NameFormats =
    {
        // Basic
        "{GREEK} {NOUN}", // "Ω Site", "Δ Artifact"
        "{NOUN} ({DEPTH})", // "Wreck (430)", "Creature (1200)"
        "{ADJECTIVE} {NOUN}", // "Phantom Colony", "Lost Vessel"
        "{ADJECTIVE} {NOUN} ({DEPTH})", // "Forgotten Facility (850)"
        "{ADJECTIVE} {NOUN} ({PHRASE})", // "Forgotten Facility (850)"

        // Corrupted
        "{ADJECTIVE} {NOUN} ({GREEK})", // "Corrupted Transmission (ΛΨ)"
        "{GREEK}-{ADJECTIVE}", // "Ψ-Distorted", "Ω-Corrupted"
        "x{NUMBER} {NOUN}", // "x52 Object", "x912 Specimen"
        "{PHRASE} - x{NUMBER} {NOUN}", // "DANGER - x52 Object", "AVOID - x912 Specimen"
        "{NOUN}-{NUMBER}-{GREEK}", // "Vessel-88-Φ", "Entity-23-Ω"
        "{NUMBER}-{GREEK}-{ADJECTIVE}", // "43-Λ-Dormant", "102-Δ-Malformed"
        "{NUMBER}-{GREEK}-{ADJECTIVE} - {PHRASE}", // "43-Λ-Dormant", "102-Δ-Malformed"

        // Phrases
        "{NOUN} of {ADJECTIVE}", // "Artifact of Ghost", "Remnant of Silence"
        "{ADJECTIVE} {NOUN} of [{ADJECTIVE}]", // "Forgotten Vessel of [Echo]", "Corrupted Specimen of [Distortion]"
        "{NOUN}: {ADJECTIVE}", // "Transmission: Malformed", "Incident: Redacted"
        "{PHRASE}: {PHRASE}", // "HELP NEEDED: AVOID"
        "{PHRASE} - {PHRASE}", // "HELP NEEDED - AVOID"

        // Heavily corrupted
        "{ADJECTIVE}-{GREEK}-{NUMBER}", // "Echo-Ω-43", "Silent-Ψ-74"
        "{GREEK}-{GREEK}-{NUMBER}", // "Λ-Ψ-32", "Δ-Ω-77"
        "{NUMBER} {ADJECTIVE} {NOUN}", // "42 Lost Vessel", "301 Phantom Base"
        "{ADJECTIVE} {NUMBER}-{GREEK}", // "Derelict 52-Δ", "Corrupted 91-Ψ"
        "{GREEK}{NUMBER}", // "Ω91", "Δ32"
        "{GREEK}{NUMBER} ({PHRASE})", // "Ω91 (CANCELLED)"
        "{GREEK}-{ADJECTIVE}-{NOUN}", // "Ψ-Silent-Entity", "Ω-Unknown-Specimen"
    };

    private static readonly string[] RecognizableNouns =
    {
        "Site",
        "Base",
        "Wreck",
        "Creature",
        "Facility",
        "Colony",
        "Remnant",
        "Entity",
        "Vessel",
        "Artifact",
        "Husk",
        "Incident",
        "Object",
        "Specimen",
        "Transmission",
    };

    private static readonly string[] RecognizableAdjectives =
    {
        "Alterra",
        "Research",
        "Infected",
        "Corrupted",
        "Unknown",
        "Abandoned",
        "Lost",
        "Phantom",
        "Distorted",
        "Silent",
        "Forgotten",
        "Ancient",
        "Prototype",
        "Derelict",
        "Redacted",
        "Unstable",
        "Malformed",
        "Hidden",
        "Ghost",
        "Bleeding",
        "Dormant",
        "Disfigured",
        "Returned"
    };

    private static readonly string[] Phrases =
    {
        "HELP NEEDED",
        "DISCONTINUED",
        "CANCELLED",
        "AVOID",
        "DANGER",
        "FATAL"
    };

    private static readonly string GreekCharacters = "ΑαΒβΓγΔδΕεΖζΗηΘθΙιΚκΛλΜμΝνΞξΟοΠπΡρΣσςΤτΥυΦφΧχΨψΩω";
    private static readonly string Digits = "0123456789";

    protected override IEnumerator OnLoadAssets()
    {
        _pingTypes = ((PingType[])System.Enum.GetValues(typeof(PingType)))
            .Where(type => type != PingType.None && type != PingType.ControlRoom && type != PingType.Base)
            .ToArray();
        yield break;
    }

    protected override void PerformTimedAction()
    {
        if (Ocean.GetDepthOf(Player.main.gameObject) < MaxDepthForLifePods && Random.value < LifePodSpawnChance)
        {
            var lifePodPosition = GetRandomLifePodSpawnPosition();
            var lifePodName = Random.value < LifePodHasNameChance ? string.Empty : "Lifepod " + Random.Range(MinLifePodNumber, MaxLifePodNumber);
            SpawnPing(lifePodPosition, PingType.Lifepod, lifePodName, Random.Range(MinLifePodLingerDuration, MaxLifePodLingerDuration), 0);
            StartCoroutine(SpawnLifePodHallucination(lifePodPosition));
            return;
        }

        var spawnPosition = GetRandomSpawnPosition();
        SpawnPing(spawnPosition, GetRandomPingType(), GetRandomName(spawnPosition), GetLifetime(),
            Random.value < UseBlueIconChance ? 0 : GetRandomColor());
    }

    private void SpawnPing(Vector3 position, PingType pingType, string label, float duration, int color)
    {
        var signalObject = new GameObject("FakeSignal");
        signalObject.SetActive(false);
        signalObject.transform.position = position;
        var ping = signalObject.AddComponent<PingInstance>();
        ping.pingType = pingType;
        ping.origin = signalObject.transform;
        if (!string.IsNullOrEmpty(label))
        {
            ping.SetLabel(label);
        }
        ping.SetColor(color);
        signalObject.SetActive(true);
        Destroy(signalObject, duration);
        signalObject.AddComponent<FlickerSignalWhenNear>().pingInstance = ping;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        var startingPosition = MainCamera.camera.transform.position;
        var randomDirection = Random.onUnitSphere;

        var unclampedPosition = startingPosition + Vector3.down * DepthIncrease +
                                randomDirection * Random.Range(MinRadius, MaxRadius);
        return new Vector3(unclampedPosition.x, Mathf.Min(-MinDepth, unclampedPosition.y), unclampedPosition.z);
    }

    private Vector3 GetRandomLifePodSpawnPosition()
    {
        var startingPosition = MainCamera.camera.transform.position;
        var randomDirection = Random.onUnitSphere;

        var unclampedPosition = startingPosition + randomDirection * Random.Range(MinRadius, MaxRadius);
        return new Vector3(unclampedPosition.x, 0, unclampedPosition.z);
    }

    private PingType GetRandomPingType()
    {
        return _pingTypes[Random.Range(0, _pingTypes.Length)];
    }

    private float GetLifetime()
    {
        return Random.Range(MinLingerDuration, MaxLingerDuration);
    }

    private string GetRandomName(Vector3 location)
    {
        var format = NameFormats[Random.Range(0, NameFormats.Length)];

        var result = format;
        var tokens = new[] { "NOUN", "ADJECTIVE", "GREEK", "NUMBER", "DEPTH", "PHRASE" };

        foreach (var token in tokens)
        {
            result = result.Replace($"{{{token}}}", GetTextForNameSegment(token, location));
        }

        return result;
    }

    private int GetRandomColor()
    {
        return Random.Range(0, PingManager.colorOptions.Length);
    }

    private string GetTextForNameSegment(string segment, Vector3 location)
    {
        switch (segment)
        {
            case "NOUN":
                return RecognizableNouns[Random.Range(0, RecognizableNouns.Length)];
            case "ADJECTIVE":
                return RecognizableAdjectives[Random.Range(0, RecognizableAdjectives.Length)];
            case "GREEK":
                return GreekCharacters[Random.Range(0, GreekCharacters.Length)].ToString();
            case "PHRASE":
                return Phrases[Random.Range(0, Phrases.Length)];
            case "NUMBER":
                string numbers = string.Empty;
                for (int i = 0; i < Random.Range(3, 5); i++)
                {
                    numbers += Digits[Random.Range(0, Digits.Length)];
                }

                return numbers;
            case "DEPTH":
                var depthString = location.y >= 0 ? "0" : Mathf.RoundToInt(Mathf.Abs(location.y)).ToString();
                return Language.main.GetFormat("SignalDistanceFormat", depthString);
        }

        return "???";
    }

    private static IEnumerator SpawnLifePodHallucination(Vector3 position)
    {
        var task = PrefabDatabase.GetPrefabAsync("00037e80-3037-48cf-b769-dc97c761e5f6");
        yield return task;
        task.TryGetPrefab(out var original);
        var pod = UWE.Utils.InstantiateDeactivated(original);
        DestroyImmediate(pod.GetComponent<LargeWorldEntity>());
        DestroyImmediate(pod.GetComponent<PrefabIdentifier>());

        var light = pod.AddComponent<Light>();
        light.intensity = 0.5f;
        light.color = Color.red;
        light.range = 20;

        var fadeOut = pod.AddComponent<FadeOutOnApproach>();
        fadeOut.fadeDuration = 2;
        fadeOut.fadeOutRange = 60;

        pod.SetActive(true);
        pod.transform.position = position;
    }

    protected override void OnActivate()
    {
    }

    protected override void OnDeactivate()
    {
    }

    private class FlickerSignalWhenNear : MonoBehaviour, IScheduledUpdateBehaviour
    {
        public PingInstance pingInstance;

        public float distance = 42f;
        public float destroyDelay = 5f;
        public float playSoundOnApproachChance = 0.86f;

        public int scheduledUpdateIndex { get; set; }

        private bool _flickering;

        private void Start()
        {
            UpdateSchedulerUtils.Register(this);
        }

        public void ScheduledUpdate()
        {
            if (_flickering)
                return;

            if (Vector3.Distance(MainCamera.camera.transform.position, transform.position) > distance)
                return;

            if (pingInstance == null)
                return;

            _flickering = true;

            Destroy(gameObject, destroyDelay);

            var pings = uGUI.main.transform.Find("ScreenCanvas/Pings").GetComponent<uGUI_Pings>();

            foreach (var ping in pings.pings)
            {
                if (ping.Key == pingInstance.Id && ping.Value != null)
                {
                    var flicker = gameObject.AddComponent<LightFlickerEvent>();
                    flicker.SetUp(new[] { new SignalFlickerTarget(ping.Value.icon) }, destroyDelay);
                    break;
                }
            }

            if (Random.value < playSoundOnApproachChance)
            {
                FMODUWE.PlayOneShot(ApproachSounds[Random.Range(0, ApproachSounds.Length)], transform.position);
            }
        }

        private void OnDestroy()
        {
            UpdateSchedulerUtils.Deregister(this);
        }

        public string GetProfileTag()
        {
            return "TRP:FlickerSignalWhenNear";
        }
    }

    private class SignalFlickerTarget : FlickerTargetBase
    {
        public SignalFlickerTarget(uGUI_Icon image)
        {
            _image = image;
        }

        private readonly uGUI_Icon _image;

        public override void SetIntensity(float intensity)
        {
            if (_image)
                _image.color = _image.color.WithAlpha(intensity);
        }

        public override void ResetIntensity()
        {
            if (_image)
                _image.color = _image.color.WithAlpha(1f);
        }
    }
}