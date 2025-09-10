using Nautilus.Utility;

namespace TheRedPlague.Utilities;

public static class MutePdaUtils
{
    public static bool GetPdaQueueDisabled()
    {
        return _counter > 0;
    }
    
    private static int _counter;

    private static bool _registeredToOnQuit;
    
    public static void AddPdaMuter()
    {
        _counter++;
        
        if (!_registeredToOnQuit)
        {
            SaveUtils.RegisterOnQuitEvent(ResetOnQuit);
            _registeredToOnQuit = true;
        }
    }

    public static void RemovePdaMuter()
    {
        _counter--;
    }

    private static void ResetOnQuit()
    {
        _counter = 0;
    }
}