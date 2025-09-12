using System.Collections;
using Nautilus.Assets;
using TheRedPlague.Mono.CreatureBehaviour.Sucker;
using TheRedPlague.Mono.InfectionLogic;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Creatures;

public class PossessedVehicle
{
    public PrefabInfo Info { get; }

    private readonly TechType _vehicleTechType;

    public PossessedVehicle(TechType vehicleTechType)
    {
        Info = PrefabInfo.WithTechType("Possessed" + vehicleTechType);
        _vehicleTechType = vehicleTechType;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(BuildPrefab);
        prefab.Register();
    }

    private IEnumerator BuildPrefab(IOut<GameObject> prefab)
    {
        var task = CraftData.GetPrefabForTechTypeAsync(_vehicleTechType);
        yield return task;
        var obj = UWE.Utils.InstantiateDeactivated(task.GetResult());

        // lock vehicle
        var vehicle = obj.GetComponent<Vehicle>();
        if (vehicle is SeaMoth seaMoth)
        {
            var lights = seaMoth.toggleLights;
            lights.enabled = false;
            lights.lightsOffSound = null;
            lights.lightsOnSound = null;
        }

        var isExosuit = _vehicleTechType == TechType.Exosuit;
        
        var infectedVehicle = obj.AddComponent<InfectedVehicle>().isExosuit = isExosuit;
        obj.AddComponent<SuckerControllerTarget>();

        // remove signal/beacon
        Object.DestroyImmediate(obj.GetComponent<PingInstance>());

        // remove construct vfx
        Object.DestroyImmediate(obj.GetComponent<VFXConstructing>());

        // remove depth crushing
        Object.DestroyImmediate(obj.GetComponent<DepthAlarms>());
        Object.DestroyImmediate(obj.GetComponent<CrushDamage>());

        // fix rigidity
        var rb = obj.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        if (isExosuit)
            rb.constraints = RigidbodyConstraints.FreezeRotation;

        // explode when the player or a creature is close
        if (!isExosuit)
            obj.AddComponent<PossessedVehicleExplode>().vehicle = vehicle;

        // remove eco target type (this used to be a 'shark')
        Object.DestroyImmediate(obj.GetComponent<EcoTarget>());

        obj.EnsureComponent<LargeWorldEntity>().cellLevel = isExosuit
            ? LargeWorldEntity.CellLevel.Near
            : LargeWorldEntity.CellLevel.Far;

        // fix floating text
        if (isExosuit)
        {
            
        }

        prefab.Set(obj);
    }
}