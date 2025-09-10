using Nautilus.Utility;

namespace TheRedPlague.Utilities;

public static class HideHudUtils
{
    internal static bool HidingHud => _hiders > 0;

    private static int _hiders;
    
    private static bool _registered;

    public static void AddHudHider()
    {
        _hiders++;
        if (!_registered)
        {
            SaveUtils.RegisterOnQuitEvent(OnGameQuit);
            _registered = true;
        }
    }

    public static void RemoveHudHider()
    {
        _hiders--;
    }

    private static void OnGameQuit()
    {
        _hiders = 0;
    }
}