

namespace RamuneLib
{
    internal static class Variables
    {
        internal static string name { get; set; }

        internal static Harmony harmony { get; set; }

        internal static ManualLogSource logger { get; set; }

        internal static BaseUnityPlugin instance { get; set; }

        internal static class Paths
        {
            private static readonly string Location = Assembly.GetExecutingAssembly().Location;

            internal static string GameFolder => BepInExPaths.GameRootPath;

            internal static string BepInExFolder => BepInExPaths.BepInExRootPath;

            internal static string BepInExConfigFolder => BepInExPaths.ConfigPath;

            internal static string BepInExPluginFolder => BepInExPaths.PluginPath;

            internal static string BepInExPatcherFolder => BepInExPaths.PatcherPluginPath;

            internal static string PluginFolder => Path.GetDirectoryName(Location);

            internal static string AssetsFolder => Path.Combine(PluginFolder, "Assets");

            internal static string RecipeFolder => Path.Combine(PluginFolder, "Recipes");

            internal static string LocalizationFolder => Path.Combine(PluginFolder, "Localization");

            internal static string ConfigurationFolder => Path.Combine(PluginFolder, "Configuration");
        }
    }
}