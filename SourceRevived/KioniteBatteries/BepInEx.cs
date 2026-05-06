

namespace Ramune.KioniteBatteries
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInDependency("com.mrpurple6411.CustomBatteries")]
    [BepInDependency("com.ramune.RamunesWorkbench")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class KioniteBatteries : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static KioniteBatteries Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.KioniteBatteries";
        public const string Name = "KioniteBatteries";
        public const string Version = "5.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/KioniteBatteries/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            Prefabs.AdvancedMaterials.Kyanicphur.Register();

            var kioniteBatteryCraftingMaterials = new List<TechType>();

            JsonUtils.GetJsonRecipeDataOrDefault(JsonUtils.GetJsonRecipePath("KioniteBattery")).Ingredients.ForEach(x =>
            {
                for(int i = 0; i < x.amount; i++)
                    kioniteBatteryCraftingMaterials.Add(x.techType);
            });

            var kioniteBattery = new CustomBatteries.API.CbBattery
            {
                EnergyCapacity = config.batteryCapacity,
                ID = "KioniteBattery",
                Name = "kionitebattery.name".LangKeyAbbr(),
                FlavorText = string.Format("kionitebattery.desc".LangKeyAbbr(), "kionitebattery.capacity".LangKeyAbbr()),
                UnlocksWith = TechType.Lithium,
                CustomIcon = ImageUtils.GetSprite("KioniteBattery"),
                AddToFabricator = false,
                CBModelData = new()
                {
                    CustomTexture = ImageUtils.GetTexture("KioniteBatteryTexture"),
                    CustomIllumMap = ImageUtils.GetTexture("KioniteBatteryIllum")
                },
                CraftingMaterials = kioniteBatteryCraftingMaterials
            };

            kioniteBattery.Patch();

            var kionitePowerCellCraftingMaterials = new List<TechType>();

            JsonUtils.GetJsonRecipeDataOrDefault(JsonUtils.GetJsonRecipePath("KionitePowerCell")).Ingredients.ForEach(x =>
            {
                for(int i = 0; i < x.amount; i++)
                    kionitePowerCellCraftingMaterials.Add(x.techType);
            });

            var kionitePowerCell = new CustomBatteries.API.CbPowerCell
            {
                EnergyCapacity = config.powerCellCapacity,
                ID = "KionitePowerCell",
                Name = "kionitepowercell.name".LangKeyAbbr(),
                FlavorText = string.Format("kionitepowercell.desc".LangKeyAbbr(), "kionitepowercell.capacity".LangKeyAbbr()),
                UnlocksWith = TechType.Lithium,
                CustomIcon = ImageUtils.GetSprite("KionitePowerCell"),
                AddToFabricator = false,
                CBModelData = new()
                {
                    CustomTexture = ImageUtils.GetTexture("KionitePowerCellTexture"),
                    CustomIllumMap = ImageUtils.GetTexture("KionitePowerCellIllum")
                },
                CraftingMaterials = kionitePowerCellCraftingMaterials
            };

            kionitePowerCell.Patch();

            RamunesWorkbenchUtils.AddCraftNode(kioniteBattery.TechType, RamunesWorkbenchUtils.Tabs.Batteries);
            RamunesWorkbenchUtils.AddCraftNode(kionitePowerCell.TechType, RamunesWorkbenchUtils.Tabs.PowerCells);
        }
    }
}