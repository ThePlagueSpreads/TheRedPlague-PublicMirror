using System;
using JetBrains.Annotations;

namespace TheRedPlague.Registration.AutoPrefabs;

[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class)]
public class PrefabClassAttribute : Attribute
{
    public RegistrationStage Stage { get; }

    public PrefabClassAttribute()
    {
        Stage = RegistrationStage.Default;
    }
    
    public PrefabClassAttribute(RegistrationStage stage)
    {
        Stage = stage;
    }
}