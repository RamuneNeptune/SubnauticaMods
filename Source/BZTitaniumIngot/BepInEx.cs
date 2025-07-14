

namespace Ramune.BZTitaniumIngot
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class BZTitaniumIngot : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static BZTitaniumIngot Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.BZTitaniumIngot";
        public const string Name = "BZTitaniumIngot";
        public const string Version = "4.0.0";

        public void Awake()
        {
            ModMessageSystem.SendGlobal("FindMyUpdates", "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/BZTitaniumIngot/Version.json");

            if(!config.EnableThisMod)
            {
                Logfile.Warning("This mod has been disabled in the config and will not be loaded");
                return;
            }

            Initializer.Initialize(harmony, Logger, Name, Version);

            CraftDataHandler.SetRecipeData(TechType.TitaniumIngot, PrefabUtils.CreateRecipe(1, new Ingredient(TechType.Titanium, 5)));
        }
    }
}