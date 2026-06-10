using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;

namespace TheRedPlague.Framework.CommonPrefabs.KeypadDoors;

public class CustomKeypadDoor
{
    public PrefabInfo Info { get; }
    private string AccessCode { get; }
    
    public Vector3 KeypadOffset { private get; init; }
    public bool ZeroFix { private get; init; }
    public bool UseAuroraSkies { private get; init; }

    public CustomKeypadDoor(string techType, string accessCode)
    {
        Info = PrefabInfo.WithTechType(techType);
        AccessCode = accessCode;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(new CloneTemplate(Info, "3265d800-9ae0-478c-973c-ddf5351977c0")
        {
            ModifyPrefab = obj =>
            {
                obj.GetComponent<KeypadDoorConsole>().accessCode = AccessCode;

                if (ZeroFix)
                {
                    obj.AddComponent<KeypadDoorZeroFix>();
                }

                if (!UseAuroraSkies)
                {
                    var placeholder = obj.GetComponentInChildren<PrefabPlaceholder>();
                    placeholder.prefabClassId = AutoSkyDoor.Info.ClassID;
                }
                
                if (KeypadOffset == default) return;
                var transforms = new[]
                    { obj.transform.Find("Mesh"), obj.transform.Find("Collider"), obj.transform.Find("KeypadUI") };
                foreach (var tr in transforms)
                {
                    tr.localPosition += KeypadOffset;
                }
            }
        });
        prefab.Register();
    }
}