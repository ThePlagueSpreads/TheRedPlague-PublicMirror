using System;
using System.Linq;
using System.Reflection;

namespace TheRedPlague.Registration.AutoPrefabs;

public static class AutoPrefabLoader
{
    public static void RegisterAllPrefabs(Assembly assembly)
    {
        var prefabClassCount = 0;
        var successCount = 0;
        var errorCount = 0;

        var prefabClasses = assembly
            .GetTypes()
            .Select(t => new
            {
                Type = t,
                Attr = t.GetCustomAttribute<PrefabClassAttribute>()
            })
            .Where(x => x.Attr != null)
            .OrderBy(x => x.Attr.Stage)
            .Select(x => x.Type);

        foreach (var prefabClass in prefabClasses)
        {
            prefabClassCount++;
            
            var methods = prefabClass.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetCustomAttribute<PrefabRegistrationAttribute>() != null);

            bool any = false;
            
            foreach (var method in methods)
            {
                any = true;

                try
                {
                    method.Invoke(null, null);
                    successCount++;
                }
                catch (Exception e)
                {
                    Plugin.Logger.LogError($"Exception thrown for PrefabClass '{prefabClass}': {e}");
                    errorCount++;
                }
            }

            if (!any)
            {
                Plugin.Logger.LogWarning($"No valid PrefabRegistration method found in PrefabClass '{prefabClass}'");
            }
        }
        
        Plugin.Logger.LogMessage($"Automatically loaded {successCount} prefabs from {prefabClassCount} prefab classes.");
        if (errorCount > 0)
        {
            Plugin.Logger.LogError($"Failed to load {errorCount} prefabs.");
        }
    }
}