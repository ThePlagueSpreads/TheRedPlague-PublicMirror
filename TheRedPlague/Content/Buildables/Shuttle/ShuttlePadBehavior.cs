using System.Collections;
using Story;
using TheRedPlague.Content.Items.Consumable;
using TheRedPlague.Content.Items.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheRedPlague.Content.Buildables.Shuttle;

public class ShuttlePadBehavior : MonoBehaviour, IScheduledUpdateBehaviour
{
    public ShuttlePadStorageContainer container;
    public GameObject shuttlePrefab;
    public Transform landingPoint;
    public float spawnHeight = 2200;
    private Vector3 _domeLandingPosition = new Vector3(111, 2112, 1);
    private Vector3 _domeDodgePosition = new Vector3(227, 2138, 14);

    private TechType[] _plagueResourceTechTypes;

    public VoiceNotification shuttleFromSpaceVoiceNotification;
    public VoiceNotification shuttleToSpaceVoiceNotification;
    public VoiceNotification shuttleDestroyedVoiceNotification;
    public VoiceNotification warnPlayerVoiceNotification;

    public VoiceNotification contractTerminatedVoiceNotification;

    // For act 2 and onward (less harsh)
    public VoiceNotification itemNotWantedVoiceNotification;

    private ShuttleController _currentShuttle;

    private bool _warnedPlayer;

    public bool IsShuttleActive { get; private set; }
    
    public int scheduledUpdateIndex { get; set; }

    private void Awake()
    {
        _plagueResourceTechTypes = new[]
        {
            AmalgamatedBone.Info.TechType,
            WarperHeart.Info.TechType,
            PlagueCatalyst.Info.TechType,
            DormantNeuralMatter.Info.TechType,
            MysteriousRemains.Info.TechType,
            PlagueIngot.Info.TechType
        };
    }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public void PlayShuttleAnimation(ShuttlePath path)
    {
        if (_currentShuttle == null)
        {
            _currentShuttle = Instantiate(shuttlePrefab).GetComponent<ShuttleController>();
            _currentShuttle.gameObject.SetActive(true);
        }

        _currentShuttle.SetPath(path, this);
        IsShuttleActive = true;
    }

    public void DeliverCargoToAlterra()
    {
        StartCoroutine(PlayFullShuttleAnimationCoroutine());
    }

    private IEnumerator PlayFullShuttleAnimationCoroutine()
    {
        yield return CallShuttleFromSpace();
        yield return new WaitForSeconds(8);
        CommentOnCargo();
        yield return new WaitUntil(() => _currentShuttle == null || _currentShuttle.HasLanded);
        yield return new WaitForSeconds(5);
        if (_currentShuttle == null)
        {
            yield break;
        }

        PackageItemsIntoCargo();

        yield return SendShuttleBack();
    }

    private IEnumerator CallShuttleFromSpace()
    {
        PlayShuttleAnimation(DomeIsPresent() ? GetPathFromDomeToPad() : GetPathFromSpaceToPad());
        yield return new WaitForSeconds(2);
        shuttleFromSpaceVoiceNotification.Play();
    }

    private IEnumerator SendShuttleBack()
    {
        PlayShuttleAnimation(DomeIsPresent() ? GetPathFromPadToDome() : GetPathFromPadToSpace());

        if (StoryGoalManager.main.IsGoalComplete(GeneralStory.SendShuttleFirstTime.key))
        {
            shuttleToSpaceVoiceNotification.Play();
        }
        else
        {
            GeneralStory.SendShuttleFirstTime.Trigger();
        }

        IsShuttleActive = false;
        yield break;
    }

    private bool DomeIsPresent()
    {
        return StoryGoalManager.main.IsGoalComplete(Act1Story.DomeConstructionEvent.key);
    }

    public void PackageItemsIntoCargo()
    {
        var itemsContainer = container.container;
        var itemTypes = itemsContainer.GetItemTypes();

        if (itemTypes.Contains(RedPlagueSample.Info.TechType))
        {
            Act1Story.SendPlagueSampleViaShuttleEvent.Trigger();
        }

        if (itemTypes.Contains(Banana.Info.TechType))
        {
            GeneralStory.BananaphobiaGoal.Trigger();
        }

        foreach (var plagueResource in _plagueResourceTechTypes)
        {
            if (itemTypes.Contains(plagueResource))
            {
                StoryGoalManager.main.OnGoalComplete(
                    StoryUtils.GetStoryGoalKeyForShuttleDelivery(plagueResource.ToString()));
            }
        }

        itemsContainer.Clear();
    }

    private void CommentOnCargo()
    {
        if (HasNoDesiredItems(container.container))
        {
            PlayInvalidItemEvent();
        }
    }

    private void PlayInvalidItemEvent()
    {
        var story = StoryGoalManager.main;
        if (!story.IsGoalComplete(GeneralStory.ShuttleInvalidItem1.key))
        {
            GeneralStory.ShuttleInvalidItem1.Trigger();
        }
        else if (!story.IsGoalComplete(GeneralStory.ShuttleInvalidItem2.key))
        {
            GeneralStory.ShuttleInvalidItem2.Trigger();
        }
        else if (!story.IsGoalComplete(GeneralStory.ShuttleInvalidItem3.key))
        {
            GeneralStory.ShuttleInvalidItem3.Trigger();
        }
        // For the beginning of the game only, troll the player a bit
        else if (!StoryGoalManager.main.IsGoalComplete(Act1Story.SendPlagueSampleViaShuttleEvent.key)
                 && !StoryUtils.IsAct1Complete())
        {
            if (!_warnedPlayer)
            {
                warnPlayerVoiceNotification.Play();
                _warnedPlayer = true;
            }
            else
            {
                contractTerminatedVoiceNotification.Play();
                Invoke(nameof(CutToCredits), 6);
            }
        }
        // For act 2 and onward, just say something along the lines of "I don't think anyone wanted that"
        else
        {
            itemNotWantedVoiceNotification.Play();
        }
    }

    private void CutToCredits()
    {
        AddressablesUtility.LoadSceneAsync("EndCreditsSceneCleaner", LoadSceneMode.Single);
    }

    private bool HasNoDesiredItems(ItemsContainer itemsContainer)
    {
        var itemsList = itemsContainer.GetItemTypes();
        if (itemsList == null || itemsList.Count == 0) return true;

        var result = true;

        foreach (var type in itemsList)
        {
            if (type == RedPlagueSample.Info.TechType)
            {
                result = false;
            }
            
            if (type == Banana.Info.TechType)
            {
                result = false;
            }

            foreach (var plagueResource in _plagueResourceTechTypes)
            {
                if (type == plagueResource)
                {
                    result = false;
                }
            }
        }

        return result;
    }

    private ShuttlePath GetPathFromSpaceToPad()
    {
        return new ShuttlePath(new ShuttlePath.Point[]
        {
            new(new Vector3(0, spawnHeight, 0), ShuttlePath.TransitionType.Space, Vector3.down),
            new(landingPoint.position + new Vector3(0, 100, 0), ShuttlePath.TransitionType.Default),
            new(landingPoint.position, ShuttlePath.TransitionType.Ground, landingPoint.forward)
        });
    }

    private ShuttlePath GetPathFromPadToSpace()
    {
        return new ShuttlePath(new ShuttlePath.Point[]
        {
            new(landingPoint.position, ShuttlePath.TransitionType.Ground, _currentShuttle.transform.forward),
            new(landingPoint.position + new Vector3(0, 60, 0), ShuttlePath.TransitionType.Default),
            new(new Vector3(0, spawnHeight, 0), ShuttlePath.TransitionType.Space, Vector3.up)
        });
    }

    private ShuttlePath GetPathFromDomeToPad()
    {
        return new ShuttlePath(new ShuttlePath.Point[]
        {
            new(_domeLandingPosition, ShuttlePath.TransitionType.Ground, Vector3.right),
            new(_domeDodgePosition, ShuttlePath.TransitionType.Default, Vector3.down),
            new(landingPoint.position + new Vector3(0, 100, 0), ShuttlePath.TransitionType.Default),
            new(landingPoint.position, ShuttlePath.TransitionType.Ground, landingPoint.forward)
        });
    }

    private ShuttlePath GetPathFromPadToDome()
    {
        return new ShuttlePath(new ShuttlePath.Point[]
        {
            new(landingPoint.position, ShuttlePath.TransitionType.Ground, _currentShuttle.transform.forward),
            new(landingPoint.position + new Vector3(0, 60, 0), ShuttlePath.TransitionType.Default),
            new(_domeDodgePosition, ShuttlePath.TransitionType.Default, Vector3.left),
            new(_domeLandingPosition, ShuttlePath.TransitionType.Ground, Vector3.left)
            {
                DestroyWhenReached = true
            }
        });
    }

    public void DestroyShuttle()
    {
        IsShuttleActive = false;
        Destroy(_currentShuttle);
    }

    public string GetProfileTag()
    {
        return "TRP:ShuttlePadBehaviour";
    }

    public void ScheduledUpdate()
    {
        if (IsShuttleActive && _currentShuttle == null)
        {
            IsShuttleActive = false;
            shuttleDestroyedVoiceNotification.Play();
        }
    }
}