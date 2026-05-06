

namespace Ramune.LithiumBatteries
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInDependency("com.mrpurple6411.CustomBatteries")]
    [BepInDependency("com.ramune.RamunesWorkbench")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class LithiumBatteries : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static LithiumBatteries Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.LithiumBatteries";
        public const string Name = "LithiumBatteries";
        public const string Version = "5.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/LithiumBatteries/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            var lithiumBatteryCraftingMaterials = new List<TechType>();
            
            JsonUtils.GetJsonRecipeDataOrDefault(JsonUtils.GetJsonRecipePath("LithiumBattery")).Ingredients.ForEach(x =>
            {
                for(int i = 0; i < x.amount; i++)
                    lithiumBatteryCraftingMaterials.Add(x.techType);
            });

            var lithiumBattery = new CustomBatteries.API.CbBattery
            {
                EnergyCapacity = config.batteryCapacity,
                ID = "LithiumBattery",
                Name = "lithiumbattery.name".LangKeyAbbr(),
                FlavorText = string.Format("lithiumbattery.desc".LangKeyAbbr(), "lithiumbattery.capacity".LangKeyAbbr()),
                UnlocksWith = TechType.Lithium,
                CustomIcon = ImageUtils.GetSprite("LithiumBattery"),
                AddToFabricator = false,
                CBModelData = new()
                {
                    CustomTexture = ImageUtils.GetTexture("LithiumBatteryTexture"),
                    CustomIllumMap = ImageUtils.GetTexture("LithiumBatteryIllum")
                },
                CraftingMaterials = lithiumBatteryCraftingMaterials
            };

            lithiumBattery.Patch();

            var lithiumPowerCellCraftingMaterials = new List<TechType>();

            JsonUtils.GetJsonRecipeDataOrDefault(JsonUtils.GetJsonRecipePath("LithiumPowerCell")).Ingredients.ForEach(x =>
            {
                for(int i = 0; i < x.amount; i++)
                    lithiumPowerCellCraftingMaterials.Add(x.techType);
            });

            var lithiumPowerCell = new CustomBatteries.API.CbPowerCell
            {
                EnergyCapacity = config.powerCellCapacity,
                ID = "LithiumPowerCell",
                Name = "lithiumpowercell.name".LangKeyAbbr(),
                FlavorText = string.Format("lithiumpowercell.desc".LangKeyAbbr(), "lithiumpowercell.capacity".LangKeyAbbr()),
                UnlocksWith = TechType.Lithium,
                CustomIcon = ImageUtils.GetSprite("LithiumPowerCell"),
                AddToFabricator = false,
                CBModelData = new()
                {
                    CustomTexture = ImageUtils.GetTexture("LithiumPowerCellTexture"),
                    CustomIllumMap = ImageUtils.GetTexture("LithiumPowerCellIllum")
                },
                CraftingMaterials = lithiumPowerCellCraftingMaterials
            };

            lithiumPowerCell.Patch();

            RamunesWorkbenchUtils.AddCraftNode(lithiumBattery.TechType, RamunesWorkbenchUtils.Tabs.Batteries);
            RamunesWorkbenchUtils.AddCraftNode(lithiumPowerCell.TechType, RamunesWorkbenchUtils.Tabs.PowerCells);

            Prefabs.Electronics.AltIonBattery.Register();
            Prefabs.Electronics.AltIonPowerCell.Register();
        }
    }
}