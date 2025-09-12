using Newtonsoft.Json;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Mono.CinematicEvents;

public class ChaosTrailManagerManager : MonoBehaviour
{
    public TrailManager[] trails;
    public TextAsset dataAsset;
    public Transform modelObject;
    public Transform armatureRoot;

    private const float DataSourceLeviathanScale = 1.2f;
    private MultipleTrailManagersData _data;

    private bool _essentialTrailDataLoaded;

    private float _previousScale = -1f;
    
    private void Start()
    {
        _data = JsonConvert.DeserializeObject<MultipleTrailManagersData>(dataAsset.text);
    }

    private float GetRequiredScale()
    {
        return modelObject.localScale.x / 100 * armatureRoot.localScale.x / DataSourceLeviathanScale;
    }

    public void SetTrailsActive(bool active)
    {
        foreach (var trail in trails)
        {
            trail.enabled = active;
        }
    }

    public void UpdateTrails()
    {
        var scale = GetRequiredScale();
        foreach (var trail in trails)
        {
            var id = trail.rootSegment.name;
            if (!_data.trailManagers.TryGetValue(id, out var data))
            {
                Plugin.Logger.LogWarning("Failed to find trail manager data by ID: " + id);
                continue;
            }
            if (!_essentialTrailDataLoaded)
            {
                TrailManagerUtils.LoadData(trail, data);
            }
            TrailManagerUtils.UpdateTrailManagerWithScale(trail, data, scale);
            
            if (_previousScale > 0 && !Mathf.Approximately(_previousScale, scale))
            {
                var scaleChange = scale / _previousScale;
                for (int i = 0; i < trail.prevPositions.Length; i++)
                {
                    trail.prevPositions[i] *= scaleChange;
                }
            }
            
            _previousScale = scale;
        }

        _essentialTrailDataLoaded = true;
    }
}