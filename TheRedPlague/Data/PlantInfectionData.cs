using UnityEngine;

namespace TheRedPlague.Data;

public static class PlantInfectionData
{
    public static InfectablePLant[] InfectablePLants { get; } = {
        new("Acid Mushroom", 0f, 0.23f,
            Vector3.one * 2, 0.02f,
            "fc7c1098-13af-417a-8038-0053b65498e5",
            "31834aae-35ce-49c1-b5ba-ac4227750679",
            "61a5e0e6-01d5-4ae2-aea6-1186cd769025",
            "99cdec62-302b-4999-ba49-f50c73575a4d"
        ),
        new("Creepvine", 0f, 0.1f,
            Vector3.one * 0.4f, 0.01f,
            "1fd4d86f-3b06-4369-945c-ca65f50b4800",
            "7329db6b-7385-4e77-8afa-71830ead9350",
            "77a95f14-434e-46bd-8fbb-0a7c591849c3",
            "9bfe02bd-60a3-401b-b7a0-627c3bdc4451",
            "a17ef178-6952-4a91-8f66-44e1d8ca0575",
            "de0e28a2-7a17-4254-b520-5f0e28355059",
            "de972f1f-daab-41d6-b274-5173b0dd23d8",
            "ee1baf03-0560-4f4d-ad29-13a337bef0d7"
            )
        {
            OverrideGlowColor = new Color(1.1f, 0.1f, 0.1f)
        }
    };
    
    public class InfectablePLant
    {
        public string PlantName { get; }
        public string[] PlantClassIDs { get; }
        public float NormalInfectionChance { get; }
        public float InfectionChanceWithHiveMind { get; }
        public Vector3 InfectionScale { get; }
        public float InfectionHeight { get; }
        public Color? OverrideGlowColor { get; init; }

        public InfectablePLant(string plantName, float normalInfectionChance, float infectionChanceWithHiveMind, Vector3 infectionScale, float infectionHeight, params string[] plantClassIDs)
        {
            PlantName = plantName;
            PlantClassIDs = plantClassIDs;
            NormalInfectionChance = normalInfectionChance;
            InfectionChanceWithHiveMind = infectionChanceWithHiveMind;
            InfectionScale = infectionScale;
            InfectionHeight = infectionHeight;
        }
    }
}