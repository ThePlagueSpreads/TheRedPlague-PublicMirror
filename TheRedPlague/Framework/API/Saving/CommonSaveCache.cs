using System;
using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using Newtonsoft.Json;

namespace TheRedPlague.Framework.API.Saving;

/// <summary>
/// A class for saving various assorted TRP data in a given save file.
/// This class is designed to be extensible and change across updates. 
/// </summary>
[PrefabClass]
public static class CommonSaveCache
{
    private static SaveData _saveData;
    
    [PrefabRegistration] // this technically isn't a prefab, but the attribute still works just fine
    private static void Register()
    {
        _saveData = SaveDataHandler.RegisterSaveDataCache<SaveData>();

        if (IsPlayerInGame())
        {
            _saveData.Load();
        }
    }

    private static bool IsPlayerInGame()
    {
        return !string.IsNullOrWhiteSpace(SaveLoadManager.GetTemporarySavePath());
    }

    public static SaveData Data => _saveData ?? throw new NullReferenceException("Accessing CommonSaveCache.Data before registration");

    [Serializable]
    [FileName("CommonSaveData")]
    public class SaveData : SaveDataCache
    {
        // Core data
        [JsonProperty] public int Version { get; } = 1;
        
        // -- TRP SAVE DATA --
        
        // Lifepod scares

        [JsonProperty] public int LifepodKnockCount { get; private set; }
        [JsonProperty] public int LifepodScareCount { get; private set; }
        [JsonProperty] public int PlagueAltarScareCount { get; private set; }

        public void IncrementLifepodKnockCount()
        {
            LifepodKnockCount++;
        }

        public void IncrementLifepodScareCount()
        {
            LifepodScareCount++;
        }

        public void IncrementPlagueAltarScareCount()
        {
            PlagueAltarScareCount++;
        }
    }
}