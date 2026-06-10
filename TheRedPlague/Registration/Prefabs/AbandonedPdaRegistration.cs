using TheRedPlague.Framework.CommonPrefabs;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public static class AbandonedPdaRegistration
{
    [PrefabRegistration]
    private static void Register()
    {
        // RESEARCH TEAM 7
        new AbandonedPda("ResearchTeamADeathLogPDA", "ResearchTeamADeathLog").Register();
        new AbandonedPda("ResearchTeamATabletLogPDA", "ResearchTeamATabletLog").Register();

        // MAZE BASE
        for (int i = 1; i <= 4; i++)
        {
            new AbandonedPda($"WestLog{i}PDA", $"WestLog{i}").Register();
        }

        // AURORA
        new AbandonedPda("DriveCoreWarningLogPDA", "DriveCoreWarningLog").Register();
        new AbandonedPda("CassyPersonnelReportPDA", "CassyPersonnelReport").Register();

        // CYCLOPS WRECK
        new AbandonedPda("PlagueArmorCommentaryPDA", "PlagueArmorCommentary").Register();

        // ACT 2 SURVIVOR BASES
        new AbandonedPda("RedPlagueSurvivorBase1PDA", "RedPlagueSurvivorBase1Log").Register();
        new AbandonedPda("RedPlagueSurvivorBase1PDA2", "RedPlagueSurvivorBase1Log2").Register();
        new AbandonedPda("RedPlagueSurvivorBase2PDA", "RedPlagueSurvivorBase2Log").Register();
        new AbandonedPda("RedPlagueSurvivorBase2PDA2", "RedPlagueSurvivorBase2Log2").Register();
        new AbandonedPda("RedPlagueSurvivorBase3PDA", "RedPlagueSurvivorBase3Log").Register();
        new AbandonedPda("RedPlagueSurvivorBase3PDA2", "RedPlagueSurvivorBase3Log2").Register();

        // METEOR SITE
        new AbandonedPda("PlagueHeartRetrievalPDA1", "PlagueHeartRetrieval1").Register();
        new AbandonedPda("PlagueHeartRetrievalPDA2", "PlagueHeartRetrieval2").Register();
        new AbandonedPda("PlagueHeartRetrievalPDA3", "PlagueHeartRetrieval3").Register();
        new AbandonedPda("PlagueHeartRetrievalPDA4", "PlagueHeartRetrieval4").Register();

        // DERMAN
        new AbandonedPda("DermanConnectionLogPDA", "DermanConnectionLog").Register();
    }
}