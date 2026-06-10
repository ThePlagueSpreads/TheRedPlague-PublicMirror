using Nautilus.Crafting;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using UnityEngine;
using Nautilus.Assets.PrefabTemplates;
using System.Collections;
using Nautilus.Handlers;
using TheRedPlague.Utilities;


namespace TheRedPlague.Content.Buildables.VendingMachineStarshipSouvenir
{
    [PrefabClass]
    public static class VendingMachineStarshipSouvenir
    {
        public static PrefabInfo Инфо { get; } = PrefabInfo.WithTechType("VendingMachineStarshipSouvenir")
            .WithIcon(SpriteManager.Get(TechType.VendingMachine))
            .WithFolderPath(TrpPrefabFolders.EasterEggs);

        private static readonly TechType[] Сувениры = [TechType.StarshipSouvenir];

        [PrefabRegistration]
        public static void Register()
        {
            CustomPrefab префаб = new CustomPrefab(Инфо);
        
            CloneTemplate клон = new CloneTemplate(Инфо, TechType.VendingMachine);
            клон.ModifyPrefabAsync += ModifyVendingMachineAsync;

            префаб.SetGameObject(клон);

            префаб.SetPdaGroupCategory(TechGroup.Miscellaneous, TechCategory.Misc);

            префаб.SetRecipe(new RecipeData(
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Titanium, 2)
                ));

            KnownTechHandler.SetAnalysisTechEntry(Инфо.TechType, System.Array.Empty<TechType>(),
                KnownTechHandler.DefaultUnlockData.BasicUnlockSound);
            префаб.Register();
        }


        private static IEnumerator ModifyVendingMachineAsync(GameObject obj)
        {
            GameObject крутаяМодель = obj.transform.Find("Vending_machine").gameObject;

            VendingMachine компонентКакТамЕго = obj.transform.GetComponent<VendingMachine>();
            компонентКакТамЕго.snacks = Сувениры;
            Object.DestroyImmediate(крутаяМодель.transform.Find("vending_machine_snacks").gameObject);

            var spawns = new[]
            {
                (new Vector3(0.05f, 1.1f, 0.3f), new Vector3(0, 90, 0), new Vector3(1, 1, 1), "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir.prefab"),
                (new Vector3(0.05f, 1.4f, 0.3f), new Vector3(0, 90, 0), new Vector3(1, 1, 1), "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir.prefab"),
                (new Vector3(0.05f, 1.7f, 0.3f), new Vector3(0, 90, 0), new Vector3(1, 1, 1), "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir.prefab")
            };
            
            foreach (var spawn in spawns)
            {
                var result = new TaskResult<GameObject>();
                var request =
                    TrpPrefabUtils.AddChildPrefabByFileName(obj, spawn.Item4, result);
                yield return request;
                var toyModel = result.Get();
                if (toyModel == null)
                {
                    Plugin.Logger.LogError("Failed to load prefab with path " + spawn.Item4 + "!");
                    continue;
                }
                
                toyModel.transform.localPosition = spawn.Item1;
                toyModel.transform.localEulerAngles = spawn.Item2;
                toyModel.transform.localScale = spawn.Item3;
                toyModel.transform.SetParent(крутаяМодель.transform, true);

                foreach (var collider in toyModel.GetComponentsInChildren<Collider>(true))
                {
                    Object.DestroyImmediate(collider);
                }
            }

            obj.GetComponentInChildren<SkyApplier>().renderers = obj.GetComponentsInChildren<Renderer>(true);
        }
    }
}
