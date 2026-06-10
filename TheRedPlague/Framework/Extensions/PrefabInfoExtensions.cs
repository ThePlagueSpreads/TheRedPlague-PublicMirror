using Nautilus.Assets;

namespace TheRedPlague.Framework.Extensions;

public static class PrefabInfoExtensions
{
    /// <summary>
    /// Organizes this PrefabInfo into the folder defined by the parameter <paramref name="folderPath"/>.
    /// </summary>
    /// <param name="info">The <see cref="PrefabInfo"/> to modify and organize.</param>
    /// <param name="folderPath">The folder to place the PrefabInfo into. Must be separated by slashes with no ending slash.</param>
    /// <returns>The original PrefabInfo.</returns>
    /// <remarks>See <see cref="TrpPrefabFolders"/>.</remarks>
    public static PrefabInfo WithFolderPath(this PrefabInfo info, string folderPath)
    {
        return info.WithFileName($"{folderPath}/{info.ClassID}");
    }
}