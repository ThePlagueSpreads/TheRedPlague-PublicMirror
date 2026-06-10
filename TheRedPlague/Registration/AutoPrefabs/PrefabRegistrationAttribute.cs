using System;
using JetBrains.Annotations;

namespace TheRedPlague.Registration.AutoPrefabs;

/// <summary>
/// Called during the first loading screen.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method)]
public class PrefabRegistrationAttribute : Attribute
{
}