using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;

namespace TheRedPlague.Content.Unused;

[PrefabClass]
public static class PrecursorPhone
{
    [PrefabRegistration]
    public static void Register()
    {
        var precursorPhone = new CustomPrefab(PrefabInfo.WithTechType("PrecursorPhone"));
        var precursorPhoneTemplate =
            new CloneTemplate(precursorPhone.Info, "081ef6c1-aa78-46fd-a20f-a6b63ca5c5f3");
        precursorPhoneTemplate.ModifyPrefab += (go) =>
        {
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponentInChildren<Light>().enabled = false;
            go.GetComponentInChildren<VFXVolumetricLight>().gameObject.SetActive(false);
            go.GetComponent<SkyApplier>().customSkyPrefab = null;
            go.transform.localScale = new Vector3(0.2f, 0.1f, 0.3f);
            go.gameObject.AddComponent<Pickupable>();
        };
        precursorPhone.SetGameObject(precursorPhoneTemplate);
        precursorPhone.SetSpawns(new SpawnLocation(new Vector3(-1324.541f, -206.655f, 266.916f),
            new Vector3(45.373f, 276.516f, 230.206f), new Vector3(0.2f, 0.1f, 0.3f)));
        precursorPhone.Register();
    }
}