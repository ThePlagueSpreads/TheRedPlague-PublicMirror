using System;
using System.Text.RegularExpressions;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Framework.Prototyping;

[PrefabClass]
public static class PrototypeTextPrefabs
{
    // You can safely add new entries here, but be VERY careful when changing/removing these
    // Changing a string will change its prefab's Class ID
    private static readonly string[] PrototypeText =
    [
        "Major story event here",
        "Cave entrance",
        "Derman base below?",
        "Coral Reef Here",
        "Draconis Dock",
        "You're not supposed to be here",
        "Entrance door",
        "Exit door",
        "Office",
        "One-way door to get back",
        "Player proceeds to left",
        "Player proceeds to right",
        "Tight tunnel here",
        "Stop spoiling yourself by reading these"
    ];
    
    [PrefabRegistration]
    private static void Register()
    {
        foreach (var text in PrototypeText)
        {
            var classId = "PROTO_" + GetSafeIdString(text);
            var info = new PrefabInfo(classId, classId + "Prefab", TechType.None)
                .WithFolderPath(TrpPrefabFolders.Prototyping);
            new TextPrefab(info, text).RegisterPrefab();
        }
    }

    private class TextPrefab(PrefabInfo info, string text)
    {
        private PrefabInfo Info { get; } = info;
        private string Text { get; } = text;

        public void RegisterPrefab()
        {
            var prefab = new CustomPrefab(Info);
            prefab.SetGameObject(GetPrefab);
            prefab.Register();
        }

        private GameObject GetPrefab()
        {
            var obj = new GameObject(Info.ClassID);
            obj.SetActive(false);
            PrefabUtils.AddBasicComponents(obj, Info.ClassID, TechType.None, LargeWorldEntity.CellLevel.Far);
            var textObject = new GameObject("Text");
            textObject.transform.parent = obj.transform;
            var textRenderer = textObject.AddComponent<TMPro.TextMeshPro>();
            textRenderer.text = Text;
            textRenderer.color = new Color(0.8f, 0.8f, 0.8f);
            return obj;
        }
    }

    private static string GetSafeIdString(string input)
    {
        // remove whitespace
        string result = Regex.Replace(input, @"\s+", "_");

        // remove invalid characters
        result = Regex.Replace(result, "[^a-zA-Z0-9_]", "");

        // fix duplicate underscores
        result = Regex.Replace(result, @"_+", "_");

        if (string.IsNullOrWhiteSpace(result))
        {
            throw new ArgumentException($"Prototype text '{input}' is invalid (failed to generate safe TechType string)");
        }
        
        return result;
    }
}