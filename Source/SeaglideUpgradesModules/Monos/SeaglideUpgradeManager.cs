

namespace Ramune.SeaglideUpgradesModules.Monos
{
    public class SeaglideUpgradeManager : MonoBehaviour
    {
        public Dictionary<InventoryItem, Battery> batteryStorageItems = [];

        public Seaglide seaglide;

        public EnergyMixin energyMixin;

        public StorageContainer moduleStorage;

        public StorageContainer batteryStorage;

        public bool isBoosting = false;

        public bool wasBoosting = false;

        public bool batterySwapAttempted = false;

        public int speedModules = 0;

        public int efficiencyModules = 0;

        public int boostModules = 0;

        public int batterySwapModules = 0;

        public TechType techType;

        public TechType[] moduleAllowedTech =
        [
            Items.BatterySwapUpgrade.Prefab.Info.TechType,
            Items.EfficiencyUpgrade.Prefab.Info.TechType,
            Items.SpeedUpgrade.Prefab.Info.TechType,
            Items.BoostUpgrade.Prefab.Info.TechType
        ];

        public TechType[] batteryAllowedTech =
        [
            TechType.Battery,
            TechType.LithiumIonBattery,
            TechType.PrecursorIonBattery,
        ];

        public string[] batteryAllowedTechModdedStrings = [];


        public float GetSpeedModuleMultiplier() => 1f + (speedModules * 0.1f);


        public float GetEfficiencyModuleEnergyToAdd() => efficiencyModules * 0.013f;


        public float GetCurrentSpeedMultiplier() => GetSpeedModuleMultiplier() + (isBoosting ? SeaglideUpgradesModules.config.BoostMultiplier : 0f);

        // I wrote this method like this so modders can patch them easily if they need to
        public void ApplyCurrentSpeed(float currentSpeedMultiplier) => SeaglideUpgrades.Patches.PlayerToolPatch.ModdedSeaglideTechTypes[techType].Invoke(currentSpeedMultiplier);

        // This one too
        public void ApplyCurrentBatteryStorageSize(int batterySwapModules) => batteryStorage.Resize(batterySwapModules, 1);


        public void Start()
        {
            seaglide = gameObject.GetComponent<Seaglide>();

            if(seaglide == null)
                return;

            moduleStorage.container._label = "SEAGLIDE UPGRADES";
            moduleStorage.container.onAddItem += OnAddModuleItem;
            moduleStorage.container.onRemoveItem += OnRemoveModuleItem;
            moduleStorage.container.SetAllowedTechTypes(moduleAllowedTech);

            batteryAllowedTechModdedStrings.AddRangeToArray(JsonConvert.DeserializeObject<string[]>(File.ReadAllText(Path.Combine(Paths.ConfigurationFolder, "BatterySwapCompatibleTech.json"))) ?? []);

            foreach(var batteryString in batteryAllowedTechModdedStrings)
            {
                if(!EnumHandler.TryGetValue(batteryString, out TechType batteryTechType))
                    continue;
                
                batteryAllowedTech.Add(batteryTechType);
            }

            batteryStorage.container._label = "SEAGLIDE BATTERIES";
            batteryStorage.container.onAddItem += OnAddBatteryItem;
            batteryStorage.container.onRemoveItem += OnRemoveBatteryItem;
            batteryStorage.container.SetAllowedTechTypes(batteryAllowedTech);
        }


        public void OnAddModuleItem(InventoryItem item)
        {
            var moduleTechType = item.item.GetTechType();

            switch(moduleTechType)
            {
                case var x when x == Items.SpeedUpgrade.Prefab.Info.TechType:
                    speedModules++;
                    ApplyCurrentSpeed(GetCurrentSpeedMultiplier());
                    Screen.Warning(string.Format("Increasing speed to: +{0}%", (GetSpeedModuleMultiplier() - 1f) * 100f));
                    break;

                case var x when x == Items.EfficiencyUpgrade.Prefab.Info.TechType:
                    efficiencyModules++;
                    Screen.Warning(string.Format("Increasing power efficiency to: +{0}%", GetEfficiencyModuleEnergyToAdd() * 1000f));
                    break;

                case var x when x == Items.BoostUpgrade.Prefab.Info.TechType:
                    boostModules++;
                    break;

                case var x when x == Items.BatterySwapUpgrade.Prefab.Info.TechType:
                    batterySwapModules++;
                    ApplyCurrentBatteryStorageSize(batterySwapModules);
                    break;
            }
        }


        public void OnRemoveModuleItem(InventoryItem item)
        {
            var moduleTechType = item.item.GetTechType();

            switch(moduleTechType)
            {
                case var x when x == Items.SpeedUpgrade.Prefab.Info.TechType:
                    speedModules--;
                    ApplyCurrentSpeed(GetCurrentSpeedMultiplier());
                    Screen.Warning(string.Format("Reduced speed to: +{0}%", (GetSpeedModuleMultiplier() - 1f) * 100f));
                    break;

                case var x when x == Items.EfficiencyUpgrade.Prefab.Info.TechType:
                    efficiencyModules--;
                    Screen.Warning(string.Format("Reduced power efficiency to: +{0}%", GetEfficiencyModuleEnergyToAdd() * 1000f));
                    break;

                case var x when x == Items.BoostUpgrade.Prefab.Info.TechType:
                    boostModules--;
                    break;

                case var x when x == Items.BatterySwapUpgrade.Prefab.Info.TechType:
                    batterySwapModules--;
                    ApplyCurrentBatteryStorageSize(batterySwapModules);
                    break;
            }
        }


        public void OnAddBatteryItem(InventoryItem item) => batteryStorageItems.Add(item, item.item.GetComponent<Battery>());


        public void OnRemoveBatteryItem(InventoryItem item) => batteryStorageItems.Remove(item);


        public void Update()
        {
            UpdateEnergy();
            UpdateBatterySwap();

            isBoosting = boostModules > 0 && GameInput.GetButtonHeld(SeaglideUpgradesModules.UseBoostUpgrade);

            if(isBoosting && !wasBoosting)
            {
                wasBoosting = true;
                ApplyCurrentSpeed(GetCurrentSpeedMultiplier());
            }
            else if(wasBoosting && !isBoosting)
            { 
                wasBoosting = false;
                ApplyCurrentSpeed(GetCurrentSpeedMultiplier());
            }

            if(!GameInput.GetButtonDown(SeaglideUpgradesModules.OpenModuleStorage) || moduleStorage == null || moduleStorage.open)
                return;

            moduleStorage.Open();
        }


        public void UpdateEnergy()
        {
            if(efficiencyModules < 1 || !seaglide.activeState || seaglide.timeSinceUse < 1f)
                return;

            var energyToAdd = GetEfficiencyModuleEnergyToAdd();

            if(energyToAdd < 0.013f)
                return;

            seaglide.energyMixin.AddEnergy(energyToAdd);
        }


        public void UpdateBatterySwap()
        {
            if(batterySwapModules < 1 || energyMixin == null || energyMixin.IsDepleted())
                return;
        }
    }
}