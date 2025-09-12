using UnityEngine;

namespace TheRedPlague.Mono.Util;

public class GlobalWorldColliders : MonoBehaviour, IScheduledUpdateBehaviour
{
    private static readonly ColliderData[] Colliders =
    {
        new(new Vector3(-61, 307, -53), new Vector3(0, 13, 0), new Vector3(14, 8, 14), 50 * 50)
    };

    private static GameObject[] _colliderObjects;

    public int scheduledUpdateIndex { get; set; }

    private float _timeUpdateAgain;

    private const float UpdateInterval = 0.5f;

    public string GetProfileTag()
    {
        return "TRP:GlobalWorldColliders";
    }

    private void Start()
    {
        _colliderObjects = new GameObject[Colliders.Length];
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDestroy()
    {
        foreach (var collider in Colliders)
        {
            collider.Activated = false;
        }

        foreach (var obj in _colliderObjects)
        {
            Destroy(obj);
        }

        UpdateSchedulerUtils.Deregister(this);
    }

    public void ScheduledUpdate()
    {
        if (Time.time < _timeUpdateAgain)
            return;
        _timeUpdateAgain = Time.time + UpdateInterval;

        for (int i = 0; i < Colliders.Length; i++)
        {
            var colliderData = Colliders[i];
            bool isLoaded =
                Vector3.SqrMagnitude(Player.main.transform.position - colliderData.Position) <
                colliderData.LoadDistanceSqr;
            if (isLoaded && !colliderData.Activated)
            {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.name = "GlobalWorldCollider-" + i;
                obj.transform.position = colliderData.Position;
                obj.transform.eulerAngles = colliderData.EulerAngles;
                obj.transform.localScale = colliderData.Scale;
                DestroyImmediate(obj.GetComponent<MeshRenderer>());
                DestroyImmediate(obj.GetComponent<MeshFilter>());
                _colliderObjects[i] = obj;
                colliderData.Activated = true;
            }
            else if (!isLoaded && colliderData.Activated)
            {
                Destroy(_colliderObjects[i]);
                colliderData.Activated = false;
            }
        }
    }

    private class ColliderData
    {
        public Vector3 Position { get; }
        public Vector3 EulerAngles { get; }
        public Vector3 Scale { get; }
        public float LoadDistanceSqr { get; }

        public bool Activated { get; set; }

        public ColliderData(Vector3 position, Vector3 eulerAngles, Vector3 scale, float loadDistanceSqr)
        {
            Position = position;
            EulerAngles = eulerAngles;
            Scale = scale;
            LoadDistanceSqr = loadDistanceSqr;
        }
    }
}