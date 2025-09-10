using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Utilities;

public static class PreventSavingUtils
{
    private const float MaxSavePreventionDuration = 300;
    
    public static bool GetSavingIsDisabled()
    {
        if (Time.realtimeSinceStartup > _timeSavingAllowedAgain)
            return false;
        
        return _counter > 0;
    }
    
    private static int _counter;

    private static bool _registeredToOnQuit;

    private static float _timeSavingAllowedAgain;
    
    public static void AddSavingPreventer()
    {
        _counter++;
        
        if (!_registeredToOnQuit)
        {
            SaveUtils.RegisterOnQuitEvent(ResetOnQuit);
            _registeredToOnQuit = true;
        }
        
        _timeSavingAllowedAgain = Time.realtimeSinceStartup + MaxSavePreventionDuration;
    }

    public static void RemoveSavingPreventer()
    {
        _counter--;
    }

    private static void ResetOnQuit()
    {
        _counter = 0;
    }
}