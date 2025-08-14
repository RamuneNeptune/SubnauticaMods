

namespace Ramune.VehicleStorageSolutions
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class VehicleStorageSolutions : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static VehicleStorageSolutions Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.VehicleStorageSolutions";
        public const string Name = "VehicleStorageSolutions";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!Initializer.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/VehicleStorageSolutions/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            for(int ii = 2; ii < config.storageModulesToGenerate + 2; ii++)
            {
                int i = ii;

                var prefab = PrefabUtils.CreatePrefab($"vss.storage.{i}.id".LangKey(), $"vss.storage.{i}.name".LangKey(), $"vss.storage.{i}.desc".LangKey(), ImageUtils.GetSprite($"VSSStorageModuleMk{i}"))
                    .WithPDACategoryAfter(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades, TechType.VehicleStorageModule)
                    .WithVehicleUpgradeModule(EquipmentType.VehicleModule, QuickSlotType.Instant, out var upgradeModuleGadget)
                    .WithJsonRecipe($"VSSStorageModuleMk{i}", CraftTree.Type.Workbench)
                    .WithAutoUnlock();

                if(i <= 5)
                    StorageDictionary.Add(prefab.Info.TechType, (() => (int)typeof(Config).GetField($"height_seamothMk{i}Module").GetValue(config), () => (int)typeof(Config).GetField($"height_prawnSuitMk{i}Module").GetValue(config)));
                else
                    StorageDictionary.Add(prefab.Info.TechType, (GetIntFromTxt($"SeamothMk{i}HeightToAdd"), GetIntFromTxt($"ExosuitMk{i}HeightToAdd")));

                var clone = new CloneTemplate(prefab.Info, TechType.VehicleStorageModule);

                prefab.SetGameObject(clone);
                prefab.Register();
            }
        }


        public static Func<int> GetIntFromTxt(string key)
        {
            return () =>
            {
                foreach(var line in File.ReadLines(Path.Combine(Variables.Paths.PluginFolder, "Configuration", "ExtraModuleSizes.txt")))
                {
                    if(!line.StartsWith(key + "="))
                        continue;

                    return int.Parse(line.Substring(key.Length + 1));
                }

                return 0;
            };
        }


        public static Dictionary<TechType, (Func<int> seamothGetter, Func<int> exosuitGetter)> StorageDictionary = new();


        public class VSSStorageModule : MonoBehaviour
        {
            public List<Func<int>> Getters = new();

            public Dictionary<int, Func<int>> SeamothGetters = new();
        }
    }
}