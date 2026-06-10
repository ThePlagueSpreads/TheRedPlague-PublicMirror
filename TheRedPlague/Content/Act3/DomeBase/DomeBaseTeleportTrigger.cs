using System.Collections;
using Nautilus.Utility;
using TheRedPlague.Framework.UI;
using UnityEngine;

namespace TheRedPlague.Content.Act3.DomeBase;

public class DomeBaseTeleportTrigger : MonoBehaviour
{
    public Vector3 targetPosition;

    private static bool _teleporting;
    private static bool _registered;

    private static readonly FMODAsset TeleportSound = AudioUtils.GetFmodAsset("DomeBaseTeleportSound");
    
    private void OnTriggerEnter(Collider other)
    {
        if (_teleporting)
            return;

        if (other.gameObject != Player.main.gameObject)
            return;

        if (Player.main.GetVehicle() != null || Player.main.GetCurrentSub() != null)
            return;
        
        if (!_registered)
        {
            SaveUtils.RegisterOnQuitEvent(OnGameQuit);
            _registered = true;
        }
        
        UWE.CoroutineHost.StartCoroutine(TeleportSequence(targetPosition));
    }

    private static IEnumerator TeleportSequence(Vector3 targetPosition)
    {
        _teleporting = true;
        FMODUWE.PlayOneShot(TeleportSound, Player.main.transform.position);
        TeleportScreenFXController fx = null;
        if (MainCamera.camera != null && MainCamera.camera.TryGetComponent<TeleportScreenFXController>(out var found))
        {
            fx = found;
        }
        if (fx) fx.StartTeleport();
        FadingOverlay.PlayFX(new Color(0.3f, 0.3f, 1f), 0.2f, 3.3f, 1f, 0.5f);
        yield return new WaitForSeconds(1f);
        Player.main.SetPosition(targetPosition);
        yield return new WaitForSeconds(3f);
        if (fx) fx.StopTeleport();
        _teleporting = false;
    }

    private static void OnGameQuit()
    {
        _teleporting = false;
    }
}