using System;
using Story;
using TheRedPlague.Content.TitleScreen;
using UnityEngine;

namespace TheRedPlague.Content.Act1.Dome;

public class InfectionDomeController : MonoBehaviour
{
    public static InfectionDomeController main;

    public Renderer domeRenderer;
    public LightingController lightingController;
    
    private bool _rocketAnimation;
    private float _timeStartRocketAnimation;
    private float _moveUpwardsDelay = 26;
    private float _rocketVelocity;
    private float _rocketAccel = 40;

    public Collider[] domeBaseColliders;
    public Transform baseCenterTransform;
    public float collisionsOptimizationUpdateInterval = 0.5f;
    public float maxDistanceForCollisions = 350f;
    private float _timeUpdateCollisionsAgain;
    public float domeFoundationRadius = 2239;

    private bool _collisionsActive = true;
    
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        GlobalRedPlagueProgressTracker.OnProgressAchieved(GlobalRedPlagueProgressTracker.ProgressStatus.DomeConstructed);
    }

    private void SetDomeColor(Color color)
    {
        var materials = domeRenderer.materials;
        materials[0].color = color;
    }

    public void OnBeginRocketAnimation()
    {
        _timeStartRocketAnimation = Time.time;
        _rocketAnimation = true;
    }

    private void Update()
    {
        if (_rocketAnimation && Time.time > _timeStartRocketAnimation + _moveUpwardsDelay)
        {
            _rocketVelocity += Time.deltaTime * _rocketAccel;
            transform.position += Vector3.down * (_rocketVelocity * Time.deltaTime);
        }

        if (Time.time > _timeUpdateCollisionsAgain)
        {
            _timeUpdateCollisionsAgain = Time.time + collisionsOptimizationUpdateInterval;
            UpdateCollisionOptimization();
        }
    }

    private void UpdateCollisionOptimization()
    {
        var distanceFromDome = Mathf.Min(
            Vector3.SqrMagnitude(Player.main.transform.position - baseCenterTransform.position),
            Vector3.SqrMagnitude(MainCamera.camera.transform.position - baseCenterTransform.position));

        var active = distanceFromDome <= maxDistanceForCollisions * maxDistanceForCollisions;

        if (active == _collisionsActive)
            return;
        
        _collisionsActive = active;
        foreach (var collider in domeBaseColliders)
        {
            collider.enabled = active;
        }
    }
    
    private void SetDomeDisabled() => SetDomeColor(new Color(0, 0, 0, 0));
    private void SetDomeGreen() => SetDomeColor(new Color(0.5f, 1, 0.5f));

    public void ShatterDome()
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        var gibs = transform.GetChild(0).GetChild(1).gameObject;
        gibs.SetActive(true);
        foreach (var rb in gibs.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddExplosionForce(100000f, Vector3.zero, 5000, 0.2f);
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    private void SetLightingState(LightingController.LightingState state)
    {
        lightingController.state = state;
    }
    
    public bool GetIsPositionInsideDome(Vector3 position)
    {
        return Vector2.Distance(new Vector2(position.x, position.z), new Vector2(transform.position.x, transform.position.z)) < domeFoundationRadius;
    }
}