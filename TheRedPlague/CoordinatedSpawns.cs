using Nautilus.Handlers;
using TheRedPlague.PrefabFiles.Equipment;
using UnityEngine;

namespace TheRedPlague;

public static class CoordinatedSpawns
{
    private const string AnimatedLightClassId = "ForceFieldIslandLight";
    private const string AnimatedLight2ClassId = "ForceFieldIslandLight2";
    private const string AlienRobotClassID = "4fae8fa4-0280-43bd-bcf1-f3cba97eed77";
    private const string PedestalClassID = "78009225-a9fa-4d21-9580-8719a3368373";
    private const string SkyrayClassID = "6a1b444f-138f-46fa-88bb-d673a2ceb689";
    private const string WarperClassID = "510a71f0-ab6d-4c6a-aa54-a19b3f1c436c";
    
    public static void RegisterCoordinatedSpawns()
    {
        // --- Infected divers ---
        
        // lifepod 2
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.MutantDiver3.ClassID, new Vector3(-481.45f, -496.54f, 1323.83f)));

        // lifepod 3
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.SkeletonCorpse.ClassID, new Vector3(-28.18f, -19.40f, 406.94f)));
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.MutantDiver2.ClassID, new Vector3(-27.92f, -19.85f, 411.73f)));
        
        // lifepod 4
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.InfectedCorpseInfo.ClassID, new Vector3(712.27f, 2.09f, 160.94f)));

        // lifepod 6
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.SkeletonCorpse.ClassID, new Vector3(359.76f, -115.58f, 306.60f)));
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.InfectedCorpseInfo.ClassID, new Vector3(366.26f, -114.64f, 305.62f)));

        // lifepod 7
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.InfectedCorpseInfo.ClassID, new Vector3(-56.19f, -180.21f, -1039.19f)));
        
        // lifepod 12
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.InfectedCorpseInfo.ClassID, new Vector3(1119.06f, -269.02f, 564.86f)));

        // lifepod 13
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.SkeletonCorpse.ClassID, new Vector3(-926.35f, -178.92f, 506.76f)));
        
        // lifepod 17
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.InfectedCorpseInfo.ClassID, new Vector3(-515.96f, -95.58f, -56.83f)));
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.SkeletonCorpse.ClassID, new Vector3(-512.98f, -95.68f, -57.87f)));

        // lifepod 19
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.SkeletonCorpse.ClassID, new Vector3(-810.37f, -299.89f, -877.06f)));
        
        // Administrator drop pod
        
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.SkeletonCorpse.ClassID, new Vector3(-175.49f, -664.36f, 3286.42f)));
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.MutantDiver4.ClassID, new Vector3(-175.12f, -659.21f, 3286.98f)));
        CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(PlagueKnife.Info.ClassID, new Vector3(-175.803f, -666.628f, 3286.333f), new Vector3(82.25f, 211.03f, 182.54f)));
        
        // Drifters
        
        var randomGenerator = new System.Random(51034581);
        for (int i = 0; i < 80; i++)
        {
            var angle = (float) randomGenerator.NextDouble() * Mathf.PI * 2f;
            var distance = Mathf.Pow((float) randomGenerator.NextDouble(), 1/2f) * 1500f;
            var height = 20 + (float) randomGenerator.NextDouble() * 30;
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(ModPrefabs.DrifterHivemindSpawn.ClassID,
                new Vector3(Mathf.Cos(angle) * distance, height, Mathf.Sin(angle) * distance)));
        }
    }

    private struct Spawn
    {
        public string classId;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public Spawn(string classId, Vector3 position, Vector3 rotation = default, Vector3 scale = default)
        {
            this.classId = classId;
            this.position = position;
            this.rotation = rotation;
            if (scale != default) this.scale = scale;
            else scale = Vector3.one;
        }
    }
}