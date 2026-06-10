using System;
using System.Collections;
using System.IO;
using Nautilus.Handlers;
using UnityEngine;

namespace TheRedPlague.Registration.Assets;

public static class AssetBundles
{
    public static AssetBundle Common { get; private set; }
    
    public static AssetBundle Core { get; private set; }

    public static AssetBundle Creatures { get; private set; }

    public static AssetBundle Scenes { get; private set; }
    
    public static AssetBundle Characters { get; private set; }

    public static AssetBundle Audio { get; private set; }
    
    public static AssetBundle Act3 { get; private set; }
    
    private static readonly RequiredBundle[] RequiredBundles = {
        // Common assets MUST be first
        new("trp_common", "shared assets", b => Common = b),
        new("theredplague", "core assets", b => Core = b),
        new("redplaguecreatures", "creature assets", b => Creatures = b),
        new("redplaguescenes", "scenes", b => Scenes = b),
        new("redplaguecharacters", "characters", b => Characters = b),
        new("theredplague_act3", "act 3", b => Act3 = b),
        new("theredplagueaudio", "audio bundle", b => Audio = b)
    };

    internal static IEnumerator LoadRedPlagueAsync(WaitScreenHandler.WaitScreenTask task, Func<WaitScreenHandler.WaitScreenTask, string, IEnumerator> log)
    {
        foreach (var bundle in RequiredBundles)
        {
            yield return log(task, bundle.TaskName);
            var path = GetAssetBundlePath(bundle.Path);
            var assetBundleTask = AssetBundle.LoadFromFileAsync(path);
            while (!assetBundleTask.isDone)
            {
                task.Status = $"{bundle.TaskName} ({assetBundleTask.progress:P1})";
                yield return null;
            }

            if (assetBundleTask.assetBundle == null)
            {
                Plugin.Logger.LogError($"Failed to load AssetBundle at path {path}");
            }
            
            bundle.OnBundleLoaded.Invoke(assetBundleTask.assetBundle);
        }
    }
    
    private static string GetAssetBundlePath(string assetBundleFileName)
    {
        return Path.Combine(Path.GetDirectoryName(Plugin.Assembly.Location), "Assets", assetBundleFileName);
    }
    
    private class RequiredBundle
    {
        public string Path { get; }
        public string TaskName { get; }
        public Action<AssetBundle> OnBundleLoaded { get; }

        public RequiredBundle(string path, string taskName, Action<AssetBundle> onBundleLoaded)
        {
            Path = path;
            TaskName = taskName;
            OnBundleLoaded = onBundleLoaded;
        }
    }
}