using System.Collections;
using TheRedPlague.Mono.CreatureBehaviour.Chaos;
using TheRedPlague.Mono.FleshBlobs;
using TheRedPlague.Mono.StoryContent;
using TheRedPlague.Mono.StoryContent.PlagueHeart;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Mono.VFX;

public class PlagueScreenFXController : MonoBehaviour
{
    private static PlagueScreenFXController _instance;
    
    public float effectStrengthMultiplier = 0.85f;

    public float minEffect = 0f;
    public float maxEffect = 0.4f;

    public float minDistance = 20;
    public float maxDistance = 150;

    public float chaosMaxDistance = 100;
    public float plagueHeartMaxDistance = 150;

    public float fadeDuration = 5f;

    private float _prevAmount;
    private float _animTime;
    private RadiationsScreenFX _fx;
    private float _effectStrength;
    private bool _ready;

    private bool _meteorSiteEffects;

    // my attempt not to mess with the RadiationsScreenFXController class:
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        var existingRadiationScreenFx = gameObject.GetComponent<RadiationsScreenFX>();
        _fx = gameObject.AddComponent<RadiationsScreenFX>();
        _fx.shader = existingRadiationScreenFx.shader;
        _fx.color = Color.red;
        InvokeRepeating(nameof(UpdateEffect), Random.value, 0.5f);
        _ready = true;
        _instance = this;
    }

    public static void TryStartMeteorSiteEffect()
    {
        if (_instance)
            _instance._meteorSiteEffects = true;
    }

    private void Update()
    {
        if (!_ready) return;

        if (_effectStrength >= _prevAmount && _effectStrength > 0f)
        {
            _animTime += Time.deltaTime / fadeDuration;
        }
        else
        {
            _animTime -= Time.deltaTime / fadeDuration;
        }

        _animTime = Mathf.Clamp01(_animTime);
        _fx.noiseFactor = Mathf.Min(_effectStrength * effectStrengthMultiplier + minEffect * _animTime, maxEffect);
        if (_fx.noiseFactor > 0f && !_fx.enabled)
        {
            _fx.enabled = true;
        }

        _prevAmount = _effectStrength;
    }

    private void UpdateEffect()
    {
        _effectStrength = CalculateEffectStrength();
    }

    private float CalculateEffectStrength()
    {
        var plagueHeart = PlagueHeartBehaviour.Main;
        if (plagueHeart && Vector3.SqrMagnitude(plagueHeart.transform.position - transform.position) <
            plagueHeartMaxDistance * plagueHeartMaxDistance)
        {
            return GenericTrpUtils.RemapValue(Vector3.Distance(plagueHeart.transform.position, transform.position), 0,
                plagueHeartMaxDistance,
                _meteorSiteEffects ? 0.7f : 0.2f, 0f);
        }
        
        var chaos = ChaosScreenFXRoot.main;
        if (chaos && Vector3.SqrMagnitude(chaos.transform.position - transform.position) <
            chaosMaxDistance * chaosMaxDistance)
        {
            return GenericTrpUtils.RemapValue(Vector3.Distance(chaos.transform.position, transform.position), 0,
                chaosMaxDistance,
                0.2f, 0f);
        }

        var closestFleshBlob = FleshBlobGravity.GetStrongest(transform.position);
        if (closestFleshBlob == null) return 0f;
        return Mathf.InverseLerp(maxDistance * closestFleshBlob.growth.Size, minDistance * closestFleshBlob.growth.Size,
            Vector3.Distance(closestFleshBlob.transform.position, transform.position));
    }
}