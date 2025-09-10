using Nautilus.Utility;
using TheRedPlague.Mono.VFX;

namespace TheRedPlague.Utilities;

public static class OuterSpaceUtils
{
    private static bool _registered;
    private static float _oldPlanetZenith;
    private static float _oldPlanetDistance;
    private static float _oldFarPlane;
    private static float _oldSunSize;
    private static float _oldPlanetOrbitSpeed;

    private const float FarClipPlaneInSpace = 1000;
    
    public static bool InSpace { get; private set; }

    public static void SetPlayerInSpace(bool inSpace)
    {
        if (!_registered)
        {
            SaveUtils.RegisterOnQuitEvent(OnGameQuit);
            _registered = true;
        }

        if (InSpace && inSpace)
        {
            return;
        }
        
        InSpace = inSpace;
        
        var skyManager = uSkyManager.main;

        var domeFarPlane = AdjustFarPlane.Main;
        if (domeFarPlane == null)
        {
            if (inSpace)
            {
                _oldFarPlane = MainCamera.camera.farClipPlane;
                MainCamera.camera.farClipPlane = FarClipPlaneInSpace;
            }
            else
            {
                MainCamera.camera.farClipPlane = _oldFarPlane;
            }
        }
        else
        {
            if (inSpace)
            {
                _oldFarPlane = MainCamera.camera.farClipPlane;
                domeFarPlane.OverrideFarClipPlane(FarClipPlaneInSpace);
            }
            else
            {
                domeFarPlane.StopOverridingFarClipPlane();
            }
        }
        
        if (inSpace)
        {
            _oldPlanetZenith = skyManager.planetZenith;
            _oldPlanetDistance = skyManager.planetDistance;
            _oldSunSize = skyManager.SunSize;
            _oldPlanetOrbitSpeed = skyManager.planetOrbitSpeed;
            skyManager.planetDistance = 6000;
            skyManager.planetZenith = 50;
            skyManager.SunSize = 2;
            skyManager.SkyboxMaterial.EnableKeyword("ROCKETLAUNCH");
            // skyManager.starMaterial.EnableKeyword("ROCKETLAUNCH"); THIS BREAKS PLANET RENDERING
            skyManager.spaceTransition = 1;
            skyManager.UseTimeOfDay = false;
            skyManager.Timeline = 13;
            skyManager.planetOrbitSpeed = 0;
        }
        else
        {
            skyManager.planetZenith = _oldPlanetZenith;
            skyManager.planetDistance = _oldPlanetDistance;
            skyManager.SunSize = _oldSunSize;
            skyManager.SkyboxMaterial.DisableKeyword("ROCKETLAUNCH");
            // skyManager.starMaterial.DisableKeyword("ROCKETLAUNCH");
            skyManager.spaceTransition = 0;
            skyManager.UseTimeOfDay = true;
            skyManager.planetOrbitSpeed = _oldPlanetOrbitSpeed;
        }
    }

    private static void OnGameQuit()
    {
        InSpace = false;
    }
}