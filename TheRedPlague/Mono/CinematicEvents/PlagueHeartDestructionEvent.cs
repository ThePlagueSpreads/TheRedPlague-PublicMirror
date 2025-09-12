using System;
using System.Collections;
using System.Collections.Generic;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using Story;
using TheRedPlague.Mono.StoryContent;
using TheRedPlague.Mono.StoryContent.PlagueHeart;
using TheRedPlague.Mono.Systems;
using TheRedPlague.Mono.VFX;
using TheRedPlague.PrefabFiles.StoryProps;
using TheRedPlague.Utilities;
using UnityEngine;
using UWE;

namespace TheRedPlague.Mono.CinematicEvents;

public class PlagueHeartDestructionEvent : MonoBehaviour
{
    private static PlagueHeartDestructionEvent _instance;

    private static readonly Vector3 SatelliteCloseOffset = new(250, 500, 25);

    // only for obey
    private static readonly Vector3 DomeTeleportPosition = new(28, 2124, 0);
    private static readonly Vector3 BennetFleshFormSpawnPositionSky = new(146, 2119, 0);
    private static readonly Vector3 BennetFleshFormSpawnAngleSky = new(0, -90, 0);

    // only for disobey
    private static readonly Vector3 FleshCaveTeleportPosition = new(-1440, -886, 677);
    private static readonly Vector3 BennetFleshFormSpawnPositionCave = new(-1445, -888.5f, 677.2f);
    private static readonly Vector3 BennetFleshFormSpawnAngleCave = new(0, 90, 0);

    // shared
    private static readonly Vector3 TeleportPositionAboveDome = new(-1100, 3200, 1000);
    private static readonly Vector3 MeteorTeleportPosition = new(-1092, -339, 1068);

    private WarpScreenFXController _warpScreenFXController;
    private PlagueHeartBehaviour _plagueHeart;
    private Material _scrapMetalMaterial;
    private GameObject _laserPrefab;
    private FMOD_CustomLoopingEmitter _teleportSound;
    private GameObject _planetPrefab;
    private GameObject _chaosPrefab;
    private GameObject _playerMusicObject;
    private GameObject _shuttlePrefab;
    private GameObject _bennetPrefab;

    private static readonly int ColorOuter = Shader.PropertyToID("_ColorOuter");
    private static readonly int ColorCenter = Shader.PropertyToID("_ColorCenter");
    private static readonly int ColorStrength = Shader.PropertyToID("_ColorStrength");
    private static readonly FMODAsset PlagueHeartExplosion = AudioUtils.GetFmodAsset("PlagueHeartExplosion");
    private static readonly FMODAsset Music = AudioUtils.GetFmodAsset("LuesRubra");
    private static readonly FMODAsset SpaceSfx = AudioUtils.GetFmodAsset("TrpSpaceCinematicSFX");
    private static readonly int ReactToDebrisAnimParam = Animator.StringToHash("seadragon_attack");

    private PlayerCinematicController _spaceAnimationController;
    private GameObject _spaceAnimation;

    private int _restoreQuickSlot;
    private bool _wasInvincible;

    public static void StartEvent()
    {
        if (_instance != null)
        {
            Plugin.Logger.LogWarning("A Plague Heart Destruction Event instance already exists!");
            return;
        }

        var obj = new GameObject("PlagueHeartDestructionEvent");
        obj.SetActive(false);
        obj.AddComponent<PlagueHeartDestructionEvent>();
        obj.SetActive(true);
    }

    private IEnumerator Start()
    {
        if (_instance != null)
        {
            Plugin.Logger.LogWarning("There are multiple instances of the plague heart destruction event!");
            yield break;
        }

        _instance = this;

        PreventSavingUtils.AddSavingPreventer();

        yield return EnsureSatelliteExists(); // If this throws an exception, that's fine, good even

        yield return FetchReferences(); // same with this, but let's try to let it still run

        Player.main.FreezeStats();
        HideHudUtils.AddHudHider();
        HolsterTool();
        _wasInvincible = Player.main.liveMixin.invincible;
        Player.main.liveMixin.invincible = true;
        Player.main.pda.SetIgnorePDAInput(true);
        HandReticle.main.RequestCrosshairHide();

        if (_playerMusicObject)
            _playerMusicObject.SetActive(false);

        bool obeyed = StoryGoalManager.main.IsGoalComplete(StoryUtils.ObeyBennetEvent.key);

        // START THE EVENT!

        StoryUtils.Act2EndingEvent.Trigger();

        TrpEventMusicPlayer.PlayMusic(Music, 207, true, true);

        Utils.PlayFMODAsset(Music, Player.main.transform.position);

        yield return new WaitForSeconds(16);

        // b3-nt flesh form reveal (flesh cave or dome base)

        yield return Teleport(obeyed ? DomeTeleportPosition : FleshCaveTeleportPosition, 2, () =>
        {
            MainCameraControl.main.rotationX = 90;
            MainCameraControl.main.rotationY = 0;
            StartCoroutine(StartCinematicBeginning(obeyed));
        });

        yield return new WaitForSeconds(10.8f);

        // teleport to satellite

        yield return Teleport(TeleportPositionAboveDome, 2,
            () =>
            {
                OuterSpaceUtils.SetPlayerInSpace(true);
                PrecursorSatelliteOrbit.Main.OverrideOffset(SatelliteCloseOffset);
                StartSpaceAnimation();
                StoryUtils.TeleportToSpaceEvent.Trigger();
            });

        yield return PlaySpaceAnimation();

        // satellite laser

        // teleport to plague heart

        yield return Teleport(MeteorTeleportPosition, 3,
            () =>
            {
                OuterSpaceUtils.SetPlayerInSpace(false);
                if (_spaceAnimationController) _spaceAnimationController.OnPlayerCinematicModeEnd();
                Destroy(_spaceAnimation);
                PrecursorSatelliteOrbit.Main.StopOverridingOffset();
                var cameraControl = MainCameraControl.main;
                if (cameraControl)
                {
                    cameraControl.rotationY = -36;
                    cameraControl.rotationX = -28;
                }
            });

        // satellite laser and EPIC plague heart explosion

        var groundLaser = CreateMassiveLaserAtGround(_plagueHeart.transform.position, 3000);
        groundLaser.SetBrightness(0);
        groundLaser.brightnessTransitionDuration = 2.5f;
        groundLaser.TransitionToNewBrightness(0.2f);

        yield return new WaitForSeconds(3);

        var strikeParticle = Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("SatelliteStrikeParticle"));
        strikeParticle.transform.position = _plagueHeart.transform.position;
        Destroy(strikeParticle, 6);

        yield return new WaitForSeconds(0.5f);

        groundLaser.brightnessTransitionDuration = 2.5f;
        groundLaser.TransitionToNewBrightness(1f);
        Utils.PlayFMODAsset(PlagueHeartExplosion, _plagueHeart.transform.position);

        yield return new WaitForSeconds(2.5f);

        PlayExplosionVFX(_plagueHeart.transform.position + Vector3.up * 10);
        _plagueHeart.ExplodeMeteor();
        WorldForces.AddExplosion(_plagueHeart.transform.position, DayNightCycle.main.timePassed, 100, 500);
        Destroy(groundLaser.gameObject, 0.5f);

        yield return new WaitForSeconds(10);

        _plagueHeart.DisableForceField();

        yield return new WaitForSeconds(1);

        _plagueHeart.OpenHatch();

        yield return new WaitForSeconds(8);

        _plagueHeart.HatchEgg();
        yield return ChaosLeviathanHatch.PlaySequence(_plagueHeart.transform, _chaosPrefab);

        yield return new WaitForSeconds(5);

        // Reaction to chaos leviathan being released
        StoryUtils.HiveMindReleasedGoal.Trigger();

        yield return new WaitForSeconds(30);
        
        // Play credits
        OnScreenCredits.Play();

        // End cinematic mode (See OnDestroy)
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PreventSavingUtils.RemoveSavingPreventer();
        Player.main.UnfreezeStats();
        Player.main.liveMixin.invincible = _wasInvincible;
        Player.main.pda.SetIgnorePDAInput(false);
        HandReticle.main.UnrequestCrosshairHide();
        var farPlaneChanger = AdjustFarPlane.Main;
        if (farPlaneChanger)
        {
            farPlaneChanger.StopOverridingFarClipPlane();
        }

        var satellite = PrecursorSatelliteOrbit.Main;
        if (satellite)
        {
            satellite.StopOverridingOffset();
            satellite.StopOverridingRotationSpeed();
        }

        HideHudUtils.RemoveHudHider();

        if (_restoreQuickSlot != -1)
        {
            Inventory.main.quickSlots.Select(_restoreQuickSlot);
        }

        if (_teleportSound)
        {
            Destroy(_teleportSound.gameObject);
        }

        Destroy(_spaceAnimation);

        if (_playerMusicObject)
            _playerMusicObject.SetActive(true);
    }

    private IEnumerator FetchReferences()
    {
        _warpScreenFXController = MainCamera.camera.GetComponent<WarpScreenFXController>();
        _plagueHeart = PlagueHeartBehaviour.Main;
        var metalScrapTask = PrefabDatabase.GetPrefabAsync("b2d10d9b-878e-4ff8-b71f-cd578e0d2038");
        yield return metalScrapTask;
        if (metalScrapTask.TryGetPrefab(out var metalScrap))
        {
            _scrapMetalMaterial = metalScrap.transform.Find("Model/Metal_wreckage_03_07").GetComponent<Renderer>()
                .sharedMaterial;
        }

        var solarPanelTask = CraftData.GetPrefabForTechTypeAsync(TechType.SolarPanel);
        yield return solarPanelTask;
        _laserPrefab = solarPanelTask.GetResult().GetComponent<PowerRelay>().powerFX.vfxPrefab;

        _teleportSound = new GameObject("TeleportSound").AddComponent<FMOD_CustomLoopingEmitter>();
        _teleportSound.SetAsset(AudioUtils.GetFmodAsset("event:/env/use_teleporter_use_loop"));
        _teleportSound.restartOnPlay = false;

        var rocketTask = CraftData.GetPrefabForTechTypeAsync(TechType.RocketBase);
        yield return rocketTask;
        _planetPrefab = rocketTask.GetResult().transform
            .Find(
                "EndSequence/rocketship_everything2/___MASTER_EVERYTHING_ELSE_JOINT___/___PLANET_JNT___/x_EndSequence_Planet")
            .gameObject;

        _chaosPrefab = Plugin.CreaturesBundle.LoadAsset<GameObject>("ChaosLeviathanEndCinematicPrefab");

        try
        {
            _playerMusicObject = Player.main.transform.Find("SpawnPlayerSounds/PlayerSounds(Clone)/waterAmbience/music")
                .gameObject;
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError("Failed to find player music object: " + e);
        }

        _shuttlePrefab = Plugin.AssetBundle.LoadAsset<GameObject>("ShuttlePrefab");
        _bennetPrefab = Plugin.CharactersBundle.LoadAsset<GameObject>("BennetFleshFormBirthPrefab");
    }

    private IEnumerator EnsureSatelliteExists()
    {
        yield return new WaitForSeconds(0.1f);
        var satellite = PrecursorSatelliteOrbit.Main;
        if (satellite != null) yield break;
        var satelliteTask = CraftData.GetPrefabForTechTypeAsync(PrecursorSatellite.Info.TechType);
        yield return satelliteTask;
        var satellitePrefab = satelliteTask.GetResult();
        if (satellitePrefab == null)
        {
            ErrorMessage.AddMessage("Failed to load satellite!");
            yield break;
        }

        Instantiate(satellitePrefab);
        yield return null; // let the satellite initialize
    }

    private IEnumerator StartCinematicBeginning(bool obey)
    {
        var cinematic = Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>(
            obey ? "DomeFlyThroughCinematic" : "DisobeyFleshCave"));
        cinematic.transform.position = obey ? DomeTeleportPosition : FleshCaveTeleportPosition;
        cinematic.transform.eulerAngles = obey ? Vector3.up * 90 : Vector3.up * 270;
        var cinematicController = cinematic.AddComponent<PlayerCinematicController>();
        cinematicController.animatedTransform = cinematic.transform.Find("PlayerCamera");
        cinematicController.animParamReceivers = Array.Empty<GameObject>();
        cinematicController.animator = cinematic.GetComponent<Animator>();
        cinematicController.interpolationTime = 0;
        cinematicController.interpolationTimeOut = 0;
        cinematicController.playInVr = true;
        cinematicController.StartCinematicMode(Player.main);

        Animator bennetAnimator = null;
        BuildFX bennetFx = null;

        if (obey)
        {
            var shuttle = Instantiate(_shuttlePrefab);
            MaterialUtils.ApplySNShaders(shuttle, 7, 1, 1, new IgnoreParticleSystemsModifier());
            shuttle.AddComponent<SkyApplier>().renderers = shuttle.GetComponentsInChildren<Renderer>();
            foreach (var ps in shuttle.GetComponentsInChildren<ParticleSystem>())
            {
                ps.gameObject.SetActive(false);
            }

            shuttle.transform.position = new(110, 2110.6f, 0);
            shuttle.transform.localEulerAngles = Vector3.up * 90;
            shuttle.transform.localScale = Vector3.one * 3;
            Destroy(shuttle, 15);
        }

        var bennet = Instantiate(_bennetPrefab);
        TrpPrefabUtils.ApplyBennetFleshFormMaterials(bennet);
        bennet.AddComponent<SkyApplier>().renderers = bennet.GetComponentsInChildren<Renderer>();
        bennet.transform.position = obey ? BennetFleshFormSpawnPositionSky : BennetFleshFormSpawnPositionCave;
        bennet.transform.localEulerAngles = obey ? BennetFleshFormSpawnAngleSky : BennetFleshFormSpawnAngleCave;
        bennetAnimator = bennet.GetComponentInChildren<Animator>();
        bennetFx = bennet.AddComponent<BuildFX>();
        bennetFx.renderers = bennet.GetComponentsInChildren<Renderer>();
        bennetFx.centerTransform = bennet.transform;
        float bennetScale = obey ? 3 : 1;
        bennetFx.StartSetUp(new BuildFX.Settings(Color.red, new Vector4(0.3f * bennetScale, 0.2f * bennetScale, 0.2f * bennetScale, -0.12f), 1f * bennetScale, 10 * bennetScale));
        bennetFx.StartFxWhenReady(0f);
        bennet.transform.localScale *= bennetScale;
        Destroy(bennet, 15);

        var skyManager = uSkyManager.main;
        skyManager.UseTimeOfDay = false;
        skyManager.Timeline = 1;

        yield return new WaitForSeconds(3);

        if (bennetAnimator != null)
        {
            bennetAnimator.SetBool("birth", true);
        }

        if (bennetFx != null)
        {
            bennetFx.StartConstructionEffectInterpolation(1, 10);
        }

        yield return new WaitForSeconds(10);
        
        skyManager.UseTimeOfDay = true;

        Destroy(cinematic);
    }

    // fade in duration: 0.2, fade out duration: 0.6
    private IEnumerator Teleport(Vector3 position, float paddingDuration, Action actionDuring = null)
    {
        var player = Player.main;

        player.SetPrecursorOutOfWater(false);
        player.SetCurrentSub(null);

        _teleportSound.Play();

        if (_warpScreenFXController == null)
        {
            // Look again for it
            _warpScreenFXController = MainCamera.camera.GetComponent<WarpScreenFXController>();
            if (_warpScreenFXController == null)
            {
                Plugin.Logger.LogWarning("Could not find WarpScreenFXController! Skipping teleport!");
                actionDuring?.Invoke();
                yield break;
            }
        }

        // Swap material properties for something cooler and less blinding
        var material = _warpScreenFXController.fx.mat;

        var oldColorStrength = material.GetColor(ColorStrength);
        var oldCenterColor = material.GetColor(ColorCenter);
        var oldOuterColor = material.GetColor(ColorOuter);
        material.SetColor(ColorStrength, new Color(0.3f, 0.1f, 0.1f, 3f));
        material.SetColor(ColorCenter, new Color(0.714f, 0.095f, 0, 0.5f));
        material.SetColor(ColorOuter, new Color(0.5f, 0, 0.4f, 0.4f));

        // Increase duration
        var oldWarpScreenDuration = _warpScreenFXController.duration;
        _warpScreenFXController.duration = paddingDuration;

        // Main FX
        _warpScreenFXController.StartWarp();
        yield return new WaitForSeconds(_warpScreenFXController.fadeInDuration);
        yield return new WaitForSeconds(paddingDuration / 2f);

        // Perform on-teleport action
        try
        {
            actionDuring?.Invoke();
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError("Error while activating mid-teleport action: " + e);
        }

        player.SetPosition(position);

        // Complete teleport
        yield return new WaitForSeconds(paddingDuration / 2f);
        yield return new WaitForSeconds(_warpScreenFXController.fadeOutDuration);
        _warpScreenFXController.duration = oldWarpScreenDuration;
        _teleportSound.Stop();

        // Revert material changes
        material.SetColor(ColorStrength, oldColorStrength);
        material.SetColor(ColorCenter, oldCenterColor);
        material.SetColor(ColorOuter, oldOuterColor);
    }

    private IEnumerator PlayReactionToScrapMetal()
    {
        yield return new WaitForSeconds(5);
        Player.main.playerAnimator.SetBool(ReactToDebrisAnimParam, true);
        yield return new WaitForSeconds(1);
        Player.main.playerAnimator.SetBool(ReactToDebrisAnimParam, false);
    }

    private void StartSpaceAnimation()
    {
        _spaceAnimation = Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("SpaceCinematicPrefab"),
            TeleportPositionAboveDome, Quaternion.identity);
        var cinematicTransform = _spaceAnimation.transform.Find("SpaceCinematic");
        var spaceDebrisRenderer = cinematicTransform.Find("SpaceDebris").GetComponent<Renderer>();
        spaceDebrisRenderer.material =
            _scrapMetalMaterial;
        _spaceAnimationController = _spaceAnimation.AddComponent<PlayerCinematicController>();
        _spaceAnimationController.interpolationTime = 0;
        _spaceAnimationController.interpolationTimeOut = 0;
        _spaceAnimationController.animator = cinematicTransform.GetComponent<Animator>();
        _spaceAnimationController.animatedTransform =
            cinematicTransform.Find("SpaceAnimation/Camera/PlayerCamPosition");
        _spaceAnimationController.animParamReceivers = Array.Empty<GameObject>();
        _spaceAnimationController.playInVr = true;
        _spaceAnimationController.animParam = "space_cinematic";
        var skyApplier = _spaceAnimation.AddComponent<SkyApplier>();
        skyApplier.renderers = new[] { spaceDebrisRenderer };

        _spaceAnimationController.StartCinematicMode(Player.main);

        var planet = Instantiate(_planetPrefab, _spaceAnimation.transform.Find("PlanetPosition"));
        planet.transform.localPosition = Vector3.zero;
        planet.transform.localEulerAngles = new Vector3(0, 205, 21);
        planet.transform.localScale = Vector3.one;
        planet.SetActive(true);

        StartCoroutine(PlayReactionToScrapMetal());
    }

    private IEnumerator PlaySpaceAnimation()
    {
        Utils.PlayFMODAsset(SpaceSfx, Player.main.transform.position);
        var satellite = PrecursorSatelliteOrbit.Main;
        satellite.OverrideRotationSpeed(15);
        yield return new WaitForSeconds(4);
        var lasers = SpawnSatelliteChargeUpLasers();
        yield return new WaitForSeconds(2);
        var largeLaser = lasers[^1];
        largeLaser.brightnessTransitionDuration = 10;
        largeLaser.TransitionToNewBrightness(1);
        yield return new WaitForSeconds(4);
        StartCoroutine(RampUpSatelliteSpeed(satellite, 8, 15, 720));
        yield return new WaitForSeconds(8);
        foreach (var laser in lasers)
        {
            if (laser)
                Destroy(laser.gameObject, 2f);
        }
    }

    private IEnumerator RampUpSatelliteSpeed(PrecursorSatelliteOrbit satellite, float duration, float startSpeed,
        float maxSpeed)
    {
        var startTime = Time.time;
        var endTime = startTime + duration;
        while (Time.time < endTime)
        {
            var speed = Mathf.Lerp(startSpeed, maxSpeed, (Time.time - startTime) / duration);
            satellite.OverrideRotationSpeed(speed);
            yield return null;
        }
    }

    private List<SatelliteLaser> SpawnSatelliteChargeUpLasers()
    {
        var list = new List<SatelliteLaser>();
        var satellite = PrecursorSatelliteOrbit.Main;
        var endPosition = satellite.transform.Find("LaserEndPosition");
        var startPositions = satellite.transform.Find("LaserStartPositions");
        foreach (Transform child in startPositions)
        {
            var laserObj = Instantiate(_laserPrefab, satellite.transform);
            laserObj.transform.localPosition = Vector3.zero;
            laserObj.transform.localRotation = Quaternion.identity;
            laserObj.transform.localScale = Vector3.one;
            var laserComponent = laserObj.AddComponent<SatelliteLaser>();
            laserComponent.startPosition = child;
            laserComponent.endPosition = endPosition;
            var material = laserComponent.GetComponent<Renderer>().material;
            material.color = new Color(0.3f, 0.8f, 0.3f, 1f);
            var lineRenderer = laserObj.GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 1;
            lineRenderer.endWidth = 2;

            laserComponent.lineRenderer = lineRenderer;
            laserComponent.root = satellite.transform;

            if (child.gameObject.name == "LargeLaserStartPos")
            {
                laserComponent.SetBrightness(0);
                lineRenderer.startWidth = 7;
                lineRenderer.endWidth = 7;
                laserComponent.endPosition = satellite.transform.Find("LargeLaserEndPosition");
            }

            list.Add(laserComponent);
        }

        return list;
    }

    private void PlayExplosionVFX(Vector3 position)
    {
        var prefab = Plugin.AssetBundle.LoadAsset<GameObject>("PlagueHeartSatelliteExplosion");
        var obj = Instantiate(prefab, position, Quaternion.identity);
        obj.transform.localScale = Vector3.one * 0.3f;
        Destroy(obj, 60);
        MainCameraControl.main.ShakeCamera(3, 6, MainCameraControl.ShakeMode.Sqrt, 1.4f);
    }

    private SatelliteLaser CreateMassiveLaserAtGround(Vector3 endPosition, float heightOffset)
    {
        var laserObj = Instantiate(_laserPrefab);
        laserObj.transform.localPosition = Vector3.zero;
        laserObj.transform.localRotation = Quaternion.identity;
        laserObj.transform.localScale = Vector3.one;
        var laserComponent = laserObj.AddComponent<SatelliteLaser>();
        laserComponent.isGroundLaser = true;
        var material = laserComponent.GetComponent<Renderer>().material;
        material.color = new Color(0.5f, 1.1f, 0.5f, 1f);
        var lineRenderer = laserObj.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 4;
        lineRenderer.endWidth = 4;
        lineRenderer.SetPosition(0, endPosition + Vector3.up * heightOffset);
        lineRenderer.SetPosition(1, endPosition);

        laserComponent.lineRenderer = lineRenderer;

        return laserComponent;
    }

    private void HolsterTool()
    {
        _restoreQuickSlot = Inventory.main.quickSlots.activeSlot;
        Inventory.main.ReturnHeld();
    }
}