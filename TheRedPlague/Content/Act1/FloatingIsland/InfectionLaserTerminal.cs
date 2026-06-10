using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using TheRedPlague.Content.Infection;
using UnityEngine;

namespace TheRedPlague.Content.Act1.FloatingIsland;

[PrefabClass]
public static class InfectionLaserTerminal
{
    [PrefabRegistration]
    private static void Register()
    {
        var infectionLaserTerminalPrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectionLaserTerminal")
            .WithFolderPath(TrpPrefabFolders.Precursor));
        var infectionLaserTerminalTemplate =
            new CloneTemplate(infectionLaserTerminalPrefab.Info, "b1f54987-4652-4f62-a983-4bf3029f8c5b");
        infectionLaserTerminalTemplate.ModifyPrefab += go =>
        {
            go.AddComponent<InfectAnything>();
            Object.DestroyImmediate(go.GetComponent<DisableEmissiveOnStoryGoal>());
            var trigger = go.transform.Find("triggerArea");
            var terminalObj = go.transform.Find("terminal");
            var disable = trigger.GetComponent<PrecursorDisableGunTerminalArea>();
            Object.DestroyImmediate(disable);
            /*
            var originalTerminal = terminalObj.GetComponent<PrecursorDisableGunTerminal>();
            var terminal = terminalObj.gameObject.AddComponent<DisableDomeTerminal>();
            terminal.accessGrantedSound = originalTerminal.accessGrantedSound;
            terminal.accessDeniedSound = originalTerminal.accessDeniedSound;
            terminal.cinematic = originalTerminal.cinematic;
            terminal.useSound = originalTerminal.useSound;
            terminal.openLoopSound = originalTerminal.openLoopSound;
            terminal.onPlayerCuredGoal = originalTerminal.onPlayerCuredGoal;
            terminal.glowRing = originalTerminal.glowRing;
            terminal.glowMaterial = originalTerminal.glowMaterial;
            */
            // Object.DestroyImmediate(originalTerminal);
            // trigger.gameObject.AddComponent<DisableDomeArea>(); // .terminal = terminal;
            // implement custom modifications here
        };
        infectionLaserTerminalPrefab.SetGameObject(infectionLaserTerminalTemplate);
        infectionLaserTerminalPrefab.SetSpawns(new SpawnLocation(new Vector3(-60.053f, 301.044f, -23.278f),
            Vector3.up * 163));
        infectionLaserTerminalPrefab.Register();
    }
}