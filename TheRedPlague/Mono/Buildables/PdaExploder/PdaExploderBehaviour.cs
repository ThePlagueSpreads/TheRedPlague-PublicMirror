using System.Collections;
using Nautilus.Utility;
using Story;
using TheRedPlague.Mono.VFX;
using TheRedPlague.Utilities;
using UnityEngine;
using UWE;

namespace TheRedPlague.Mono.Buildables.PdaExploder;

public class PdaExploderBehaviour : CinematicModeTrigger, IStoryGoalListener
{
    private const float TimeOutInCaseOfError = 60;
    
    public GameObject crateModel;
    public GameObject cinematicObject;
    public Transform pdaHolder;
    public Collider[] colliders;

    private static readonly FMODAsset CinematicSound = AudioUtils.GetFmodAsset("PdaExplodeSFX");
    private static readonly FMODAsset ExplosionSound = AudioUtils.GetFmodAsset("PdaExplosion");
    private static readonly FMODAsset VoiceLine = AudioUtils.GetFmodAsset("PdaDestructionVoiceLine");

    private bool _canBeActivated;

    private bool _changesActive;

    private GameObject _pdaPrefab;
    private GameObject _seamothExplodeFx;

    private void Start()
    {
        // Set up required events
        onCinematicStart = new CinematicModeEvent();
        onCinematicStart.AddListener(OnCinematicStart);
        onCinematicEnd = new CinematicModeEvent();
        onCinematicEnd.AddListener(OnCinematicEnd);

        // Load and track proper state
        _canBeActivated = !StoryGoalManager.main.IsGoalComplete(StoryUtils.ExplodePda.key);
        StoryGoalManager.main.AddListener(this);

        StartCoroutine(LoadReferences());
    }

    private void OnCinematicStart(CinematicModeEventData data)
    {
        cinematicObject.SetActive(true);
        HidePlayerModelUtils.SetPlayerModelActive(false);
        UWE.CoroutineHost.StartCoroutine(StartCinematicEvents(this));
    }

    private void OnCinematicEnd(CinematicModeEventData data)
    {
        cinematicObject.SetActive(false);
        HidePlayerModelUtils.SetPlayerModelActive(true);
        KnownTech.Remove(PrefabFiles.Buildable.PdaExploder.Info.TechType);
    }

    private IEnumerator LoadReferences()
    {
        var pdaTask = PrefabDatabase.GetPrefabAsync("02dbd99a-a279-4678-9be7-a21202862cb7");
        yield return pdaTask;
        pdaTask.TryGetPrefab(out _pdaPrefab);
        var seamothTask = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
        yield return seamothTask;
        var seamothPrefab = seamothTask.GetResult();
        _seamothExplodeFx = seamothPrefab.GetComponent<SeaMoth>().destructionEffect;
    }

    private static IEnumerator StartCinematicEvents(PdaExploderBehaviour exploder)
    {
        exploder._changesActive = true;
        PreventSavingUtils.AddSavingPreventer();
        StoryUtils.ExplodePda.Trigger();
        Player.main.FreezeStats();
        HideHudUtils.AddHudHider();
        MutePdaUtils.AddPdaMuter();
        Player.main.pda.SetIgnorePDAInput(true);
        Destroy(exploder.gameObject, TimeOutInCaseOfError);
        
        // Remove from world streamer
        var lwe = exploder.GetComponent<LargeWorldEntity>();
        if (lwe)
        {
            LargeWorld.main.streamer.cellManager.UnregisterEntity(lwe);
        }
        Destroy(lwe);
        Destroy(exploder.GetComponent<PrefabIdentifier>());
        exploder.transform.parent = null;
        
        if (exploder)
            FMODUWE.PlayOneShot(CinematicSound, exploder.transform.position);
        
        yield return new WaitForSeconds(2);

        GameObject handPda = null;
        if (exploder)
        {
            handPda = PlacePdaInHand(exploder._pdaPrefab, exploder.cinematicObject.transform);
        }
        
        yield return new WaitForSeconds(3);

        if (exploder && handPda)
        {
            handPda.transform.parent = exploder.pdaHolder;
            var move = handPda.gameObject.AddComponent<MoveTowardsPoint>();
            move.target = exploder.pdaHolder;
            move.moveMetersPerSecond = 1.3f;
            move.rotateAnglesPerSecond = 720;
        }
        
        yield return new WaitForSeconds(2);

        if (exploder)
            FMODUWE.PlayOneShot(VoiceLine, exploder.transform.position);
        
        yield return new WaitForSeconds(18);

        if (exploder)
        {
            FMODUWE.PlayOneShot(ExplosionSound, exploder.transform.position);
            exploder.PlayExplodeFx();
            foreach (var collider in exploder.colliders)
            {
                if (collider)
                    collider.enabled = false;
            }
        }

        yield return new WaitForSeconds(0.1f);
        
        if (exploder)
        {
            exploder.crateModel.SetActive(false);
        }

        yield return new WaitForSeconds(20);

        yield return GiveNewPda(exploder != null ? exploder._pdaPrefab : null);
        
        yield return new WaitForSeconds(1);
        
        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.BennetInitialMeeting.key))
            StoryUtils.BennetExplodePdaEvent.Trigger();
        
        if (exploder)
        {
            Destroy(exploder.gameObject);
        }
    }

    private static GameObject PlacePdaInHand(GameObject pdaPrefab, Transform playerRigParent)
    {
        if (pdaPrefab == null)
        {
            Plugin.Logger.LogWarning("Failed to load PDA prefab in time");
            return null;
        }
        var leftHand = playerRigParent.Find(
            "char_grp/rig/c_pos/c_traj/forearm.l/hand.l");
        if (leftHand == null)
        {
            Plugin.Logger.LogWarning("Failed to find left hand!");
            return null;
        }

        var pda = SpawnPda(pdaPrefab);
        pda.transform.parent = leftHand;
        pda.transform.localPosition = new Vector3(0, 0.0013f, 0);
        pda.transform.localEulerAngles = new Vector3(314, 180, 0);

        return pda;
    }

    private static IEnumerator GiveNewPda(GameObject newPdaPrefab)
    {
        // SET UP CINEMATIC

        var cameraTransform = MainCamera.camera.transform;
        var direction = cameraTransform.forward.WithY(0);
        // Account for un-normalizable vectors
        if (direction.sqrMagnitude <= Mathf.Epsilon) direction = Random.onUnitSphere;
        direction = direction.normalized;
        var cinematic = Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("TadpolePdaRetrievalPrefab"));
        var headOffset = new Vector3(direction.x * -0.012f, direction.y * 1.279f, direction.z * 0.15f);
        cinematic.transform.position = cameraTransform.position - headOffset;
        cinematic.transform.forward = direction;
        var playerCinematicController = cinematic.AddComponent<PlayerCinematicController>();
        playerCinematicController.interpolationTime = 0.2f;
        playerCinematicController.interpolationTimeOut = 0f;
        playerCinematicController.animator = cinematic.transform.Find("TadpolePdaAnimation").GetComponent<Animator>();
        playerCinematicController.animatedTransform = cinematic.transform.Find(
            "TadpolePdaAnimation/char_grp/rig/c_pos/c_traj/head_scale_fix.x/c_head.x/head.x/CameraAnimatedTransform");
        playerCinematicController.animParamReceivers = System.Array.Empty<GameObject>();
        playerCinematicController.animParam = "animation";
        HidePlayerModelUtils.SetPlayerModelActive(false);
        playerCinematicController.StartCinematicMode(Player.main);
        var tadpoleModel = cinematic.transform.Find("TadpolePdaAnimation/Tadpole").gameObject;
        tadpoleModel.SetActive(false);
        MaterialUtils.ApplySNShaders(cinematic);
        cinematic.AddComponent<SkyApplier>().renderers = cinematic.GetComponentsInChildren<Renderer>();
        GameObject newPda = null;
        if (newPdaPrefab)
        {
            newPda = SpawnPda(newPdaPrefab);
        }

        if (newPda)
        {
            newPda.transform.parent = cinematic.transform.Find("TadpolePdaAnimation/Armature/Body/PDASpawnPoint");
            newPda.transform.localPosition = Vector3.zero;
            newPda.transform.localEulerAngles = Vector3.zero;
        }
        
        // START CINEMATIC
        yield return new WaitForSeconds(1f);
        tadpoleModel.SetActive(true);
        yield return new WaitForSeconds(4.2f);
        if (newPda)
        {
            var holdPdaPoint = cinematic.transform.Find("TadpolePdaAnimation/char_grp/rig/c_pos/c_traj/forearm.r/hand.r/HoldPDAPoint");
            var lerp = newPda.AddComponent<LerpToPoint>();
            lerp.duration = 0.3f;
            lerp.target = holdPdaPoint;
            newPda.transform.parent = null;
        }
        yield return new WaitForSeconds(3f);
        
        // END CINEMATIC
        playerCinematicController.OnPlayerCinematicModeEnd();
        HidePlayerModelUtils.SetPlayerModelActive(true);
        Destroy(cinematic);
        Destroy(newPda);
    }

    private static GameObject SpawnPda(GameObject pdaPrefab)
    {
        var pda = UWE.Utils.InstantiateDeactivated(pdaPrefab);
        pda.transform.localScale *= 0.6f;
        DestroyImmediate(pda.GetComponent<LargeWorldEntity>());
        DestroyImmediate(pda.GetComponent<PrefabIdentifier>());
        DestroyImmediate(pda.GetComponent<StoryHandTarget>());
        foreach (var collider in pda.GetComponentsInChildren<Collider>())
        {
            DestroyImmediate(collider);
        }
        DestroyImmediate(pda.GetComponent<Rigidbody>());
        DestroyImmediate(pda.GetComponent<PrefabPlaceholdersGroup>());
        pda.SetActive(true);
        return pda;
    }

    private void PlayExplodeFx()
    {
        if (_seamothExplodeFx == null) return;

        var fx = Instantiate(_seamothExplodeFx);
        fx.transform.position = transform.position;
        fx.transform.eulerAngles = Vector3.right * -90;
        fx.SetActive(true);
        /*
        foreach (var ps in fx.GetComponentsInChildren<ParticleSystem>())
        {
            var main = ps.main;
            main.startSizeMultiplier *= 2f;
            main.scalingMode = ParticleSystemScalingMode.Hierarchy;
        }

        fx.transform.localScale *= 1;*/
        fx.transform.GetChild(1).gameObject.SetActive(false);
        Destroy(fx.GetComponent<FMOD_StudioEventEmitter>());
        Destroy(fx, 20);
    }

    public void NotifyGoalComplete(string key)
    {
        if (key == StoryUtils.ExplodePda.key)
        {
            _canBeActivated = false;
        }
    }

    private void OnDestroy()
    {
        if (_changesActive)
        {
            PreventSavingUtils.RemoveSavingPreventer();
            Player.main.UnfreezeStats();
            HideHudUtils.RemoveHudHider();
            MutePdaUtils.RemovePdaMuter();
            Player.main.pda.SetIgnorePDAInput(false);
            _changesActive = false;
        }

        StoryGoalManager.main.RemoveListener(this);
    }

    public override void OnHandHover(GUIHand hand)
    {
        if (!_canBeActivated) return;
        base.OnHandHover(hand);
    }

    public override void OnHandClick(GUIHand hand)
    {
        if (!_canBeActivated) return;
        base.OnHandClick(hand);
    }
}