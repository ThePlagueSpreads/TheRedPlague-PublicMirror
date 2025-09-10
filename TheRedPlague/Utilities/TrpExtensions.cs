using System;
using UnityEngine;

namespace TheRedPlague.Utilities;

public static class TrpExtensions
{
    public static void ApplyToAllChildrenRecursive(this Transform transform, Action<Transform> action)
    {
        foreach (Transform child in transform)
        {
            action(child);
            ApplyToAllChildrenRecursive(child, action);
        }
    }
}