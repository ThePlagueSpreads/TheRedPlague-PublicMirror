using System;
using System.Collections.Generic;
using System.Linq;
using TheRedPlague.Utilities.SerializableStructs;
using UnityEngine;

namespace TheRedPlague.Utilities;

public static class TrailManagerUtils
{
    public static TrailManagerData GetData(TrailManager trailManager)
    {
        return new TrailManagerData
        {
            trailStartPositions = trailManager.trailStartPositions.ToSerializable(),
            trailStartRotations = trailManager.trailStartRotations.ToSerializable(),
            trailSpaceForward = trailManager.trailSpaceForward.ToSerializable(),
            trailSpaceUp = trailManager.trailSpaceUp.ToSerializable(),
            trailSpaceRotOffset = trailManager.trailSpaceRotOffset.ToSerializable(),
            rotationMultipliers = trailManager.rotationMultipliers.ToSerializable(),
            distances = trailManager.distances
        };
    }

    public static void LoadData(TrailManager trailManager, TrailManagerData data)
    {
        trailManager.trailStartPositions = data.trailStartPositions.ToUnity();
        trailManager.trailStartRotations = data.trailStartRotations.ToUnity();
        trailManager.trailSpaceForward = data.trailSpaceForward.ToUnity();
        trailManager.trailSpaceUp = data.trailSpaceUp.ToUnity();
        trailManager.trailSpaceRotOffset = data.trailSpaceRotOffset.ToUnity();
        trailManager.rotationMultipliers = data.rotationMultipliers.ToUnity();
        trailManager.distances = data.distances;
    }

    public static void UpdateTrailManagerWithScale(TrailManager trailManager, TrailManagerData trailManagerData,
        float scaleFactor)
    {
        trailManager.trailStartPositions = trailManagerData.GetTrailStartPositionsScaled(scaleFactor);
        trailManager.distances = trailManagerData.GetDistancesScaled(scaleFactor);
    }
}

[Serializable]
public class MultipleTrailManagersData
{
    public Dictionary<string, TrailManagerData> trailManagers;
}

[Serializable]
public class TrailManagerData
{
    public SerializableVector3[] trailStartPositions;
    public SerializableQuaternion[] trailStartRotations;
    public SerializableVector3[] trailSpaceForward;
    public SerializableVector3[] trailSpaceUp;
    public SerializableQuaternion[] trailSpaceRotOffset;
    public SerializableVector3[] rotationMultipliers;
    public float[] distances;

    public Vector3[] GetTrailStartPositionsScaled(float scale)
        => trailStartPositions.Select(v => v.ToVector3() * scale).ToArray();

    public float[] GetDistancesScaled(float scale)
        => distances.Select(d => d * scale).ToArray();
}