

namespace Ramune.SeaglideUpgradesModules.Monos
{
    public class SeaglideUpgradeManager : MonoBehaviour
    {
        public Dictionary<InventoryItem, Battery> batteryStorageItems { get; set; } = new Dictionary<InventoryItem, Battery>();

        public Seaglide seaglide { get; set; }

        public EnergyMixin energyMixin { get; set; }

        public StorageContainer moduleStorage { get; set; }

        public StorageContainer batteryStorage { get; set; }

        public bool isBoosting { get; set; }

        public bool wasBoosting { get; set; }

        public bool batterySwapAttempted { get; set; }

        public int batterySwapModules { get; set; }

        public int boostModules { get; set; }

        public int efficiencyModules { get; set; }

        public int lightModules { get; set; }

        public int noiseModules { get; set; }

        public int powerglideModules { get; set; }

        public int speedModules { get; set; }

        public TechType techType { get; set; }

        public TechType[] moduleAllowedTech { get; set; } =
        [
            Items.BatterySwapUpgrade.Prefab.Info.TechType,
            Items.BoostUpgrade.Prefab.Info.TechType,
            Items.EfficiencyUpgrade.Prefab.Info.TechType,
            Items.LightUpgrade.Prefab.Info.TechType,
            Items.NoiseDampeningUpgrade.Prefab.Info.TechType,
            Items.PowerglideUpgrade.Prefab.Info.TechType,
            Items.SpeedUpgrade.Prefab.Info.TechType,
        ];

        public TechType[] batteryAllowedTech { get; set; } =
        [
            TechType.Battery,
            TechType.LithiumIonBattery,
            TechType.PrecursorIonBattery,
        ];

        public string[] batteryAllowedTechModdedStrings { get; set; } = [];


        public int GetTotalModules() => batterySwapModules + boostModules + efficiencyModules + lightModules + noiseModules + powerglideModules + speedModules;


        public float GetSpeedModuleMultiplier() => 1f + (speedModules * 0.1f);


        public float GetEfficiencyModuleEnergyToAdd() => efficiencyModules * 0.013f;

        
        public float GetLightRangeMultiplier() => 1f + (lightModules * (SeaglideUpgradesModules.config.LightRangeMultiplier - 1f));


        public float GetLightIntensityMultiplier() => 1f + (lightModules * (SeaglideUpgradesModules.config.LightIntensityMultiplier - 1f));


        public float GetLightConesizeMultiplier() => 1f + (lightModules * (SeaglideUpgradesModules.config.LightConesizeMultiplier - 1f));


        public Vector3 GetLightMultipliers() => new(GetLightRangeMultiplier(), GetLightIntensityMultiplier(), GetLightConesizeMultiplier());


        public float GetCurrentSpeedMultiplier()
        {
            var hasPowerglide = powerglideModules > 0;

            var multiplier = GetSpeedModuleMultiplier();

            if(isBoosting)
                multiplier *= SeaglideUpgradesModules.config.BoostMultiplier;

            return multiplier * (hasPowerglide ? SeaglideUpgradesModules.config.PowerglideMultiplier : 1f);
        }


        public void ApplyCurrentSpeed(float currentSpeedMultiplier) => SeaglideUpgrades.Patches.PlayerToolPatch.ModdedSeaglideTechTypes[techType].Invoke(currentSpeedMultiplier);


        public void ApplyCurrentBatteryStorageSize(int batterySwapModules) => batteryStorage.Resize(batterySwapModules == 0 ? 1 : batterySwapModules, 1);


        public void ApplyCurrentLights()
        {
            switch(techType)
            {
                case var x when x == SeaglideUpgrades.Items.SeaglideMK1.Prefab.Info.TechType:
                    SeaglideUpgrades.Config.OnChangeMK1();
                    break;

                case var x when x == SeaglideUpgrades.Items.SeaglideMK2.Prefab.Info.TechType:
                    SeaglideUpgrades.Config.OnChangeMK2();
                    break;

                case var x when x == SeaglideUpgrades.Items.SeaglideMK3.Prefab.Info.TechType:
                    SeaglideUpgrades.Config.OnChangeMK3();
                    break;
            }
        }


        public void RebuildModuleCache()
        {
            batterySwapModules = 0;
            boostModules = 0;
            efficiencyModules = 0;
            lightModules = 0;
            noiseModules = 0;
            powerglideModules = 0;
            speedModules = 0;

            if(moduleStorage?.container == null)
                return;

            foreach(var techType in moduleAllowedTech)
            {
                var items = moduleStorage.container.GetItems(techType);

                if(items == null || items.Count < 1)
                    continue;

                foreach(var item in items)
                {
                    if(item == null || item.item == null)
                        continue;

                    var moduleTechType = item.item.GetTechType();

                    switch(moduleTechType)
                    {
                        case var x when x == Items.BatterySwapUpgrade.Prefab.Info.TechType:
                            batterySwapModules++;
                            break;

                        case var x when x == Items.BoostUpgrade.Prefab.Info.TechType:
                            boostModules++;
                            break;

                        case var x when x == Items.EfficiencyUpgrade.Prefab.Info.TechType:
                            efficiencyModules++;
                            break;

                        case var x when x == Items.LightUpgrade.Prefab.Info.TechType:
                            lightModules++;
                            break;

                        case var x when x == Items.NoiseDampeningUpgrade.Prefab.Info.TechType:
                            noiseModules++;
                            break;

                        case var x when x == Items.PowerglideUpgrade.Prefab.Info.TechType:
                            powerglideModules++;
                            break;

                        case var x when x == Items.SpeedUpgrade.Prefab.Info.TechType:
                            speedModules++;
                            break;
                    }
                }
            }

            ApplyCurrentLights();
            ApplyCurrentBatteryStorageSize(batterySwapModules);
            ApplyCurrentSpeed(GetCurrentSpeedMultiplier());
        }


        public void RebuildBatteryStorageCache()
        {
            batteryStorageItems.Clear();

            if(batteryStorage == null || batteryStorage.container == null)
                return;

            foreach(var techType in batteryAllowedTech)
            {
                var items = batteryStorage.container.GetItems(techType);

                if(items == null || items.Count < 1)
                    continue;

                foreach(var item in items)
                {
                    if(item == null || item.item == null || !item.item.TryGetComponent<Battery>(out var battery))
                        continue;

                    batteryStorageItems[item] = battery;
                }
            }
        }


        public void Start()
        {
            seaglide = gameObject.GetComponent<Seaglide>();

            if(seaglide == null)
                return;

            energyMixin = seaglide.energyMixin;

            if(energyMixin == null)
                return;

            moduleStorage.container._label = "SEAGLIDE UPGRADES";
            moduleStorage.container.onAddItem += OnAddModuleItem;
            moduleStorage.container.onRemoveItem += OnRemoveModuleItem;
            moduleStorage.container.isAllowedToAdd = new IsAllowedToAdd(CanAddModuleItem);
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

            RebuildModuleCache();
            RebuildBatteryStorageCache();
        }


        public void OnAddModuleItem(InventoryItem item)
        {
            var moduleTechType = item.item.GetTechType();

            switch(moduleTechType)
            {
                case var x when x == Items.BatterySwapUpgrade.Prefab.Info.TechType:
                    batterySwapModules++;
                    ApplyCurrentBatteryStorageSize(batterySwapModules);
                    break;

                case var x when x == Items.BoostUpgrade.Prefab.Info.TechType:
                    boostModules++;
                    break;

                case var x when x == Items.EfficiencyUpgrade.Prefab.Info.TechType:
                    efficiencyModules++;
                    Screen.Warning(string.Format("Increasing power efficiency to: +{0}%", GetEfficiencyModuleEnergyToAdd() * 1000f));
                    break;

                case var x when x == Items.LightUpgrade.Prefab.Info.TechType:
                    lightModules++;
                    ApplyCurrentLights();
                    break;

                case var x when x == Items.NoiseDampeningUpgrade.Prefab.Info.TechType:
                    noiseModules++;
                    break;

                case var x when x == Items.PowerglideUpgrade.Prefab.Info.TechType:
                    powerglideModules++;
                    break;

                case var x when x == Items.SpeedUpgrade.Prefab.Info.TechType:
                    speedModules++;
                    ApplyCurrentSpeed(GetCurrentSpeedMultiplier());
                    Screen.Warning(string.Format("Increasing speed to: +{0}%", (GetSpeedModuleMultiplier() - 1f) * 100f));
                    break;
            }
        }


        public void OnRemoveModuleItem(InventoryItem item)
        {
            var moduleTechType = item.item.GetTechType();

            switch(moduleTechType)
            {
                case var x when x == Items.BatterySwapUpgrade.Prefab.Info.TechType:
                    batterySwapModules--;
                    ApplyCurrentBatteryStorageSize(batterySwapModules);
                    break;

                case var x when x == Items.BoostUpgrade.Prefab.Info.TechType:
                    boostModules--;
                    break;

                case var x when x == Items.EfficiencyUpgrade.Prefab.Info.TechType:
                    efficiencyModules--;
                    Screen.Warning(string.Format("Reduced power efficiency to: +{0}%", GetEfficiencyModuleEnergyToAdd() * 1000f));
                    break;

                case var x when x == Items.LightUpgrade.Prefab.Info.TechType:
                    lightModules--;
                    ApplyCurrentLights();
                    break;

                case var x when x == Items.NoiseDampeningUpgrade.Prefab.Info.TechType:
                    noiseModules--;
                    break;

                case var x when x == Items.PowerglideUpgrade.Prefab.Info.TechType:
                    powerglideModules--;
                    break;

                case var x when x == Items.SpeedUpgrade.Prefab.Info.TechType:
                    speedModules--;
                    ApplyCurrentSpeed(GetCurrentSpeedMultiplier());
                    Screen.Warning(string.Format("Reduced speed to: +{0}%", (GetSpeedModuleMultiplier() - 1f) * 100f));
                    break;
            }
        }


        public bool CanAddModuleItem(Pickupable pickupable, bool verbose)
        {
            if(pickupable == null)
                return false;

            var techType = pickupable.GetTechType();

            return techType switch
            {
                var x when x == Items.BatterySwapUpgrade.Prefab.Info.TechType =>
                    batterySwapModules < SeaglideUpgradesModules.config.GetMaxBatterySwapUpgrades(),

                var x when x == Items.BoostUpgrade.Prefab.Info.TechType =>
                    boostModules < SeaglideUpgradesModules.config.GetMaxBoostUpgrades(),

                var x when x == Items.EfficiencyUpgrade.Prefab.Info.TechType =>
                    efficiencyModules < SeaglideUpgradesModules.config.GetMaxEfficiencyUpgrades(),

                var x when x == Items.LightUpgrade.Prefab.Info.TechType =>
                    lightModules < SeaglideUpgradesModules.config.GetMaxLightUpgrades(),

                var x when x == Items.NoiseDampeningUpgrade.Prefab.Info.TechType =>
                    noiseModules < SeaglideUpgradesModules.config.GetMaxNoiseDampeningUpgrades(),

                var x when x == Items.PowerglideUpgrade.Prefab.Info.TechType =>
                    powerglideModules < SeaglideUpgradesModules.config.GetMaxPowerglideUpgrades(),

                var x when x == Items.SpeedUpgrade.Prefab.Info.TechType =>
                    speedModules < SeaglideUpgradesModules.config.GetMaxSpeedUpgrades(),

                _ => false
            };
        }


        public void OnAddBatteryItem(InventoryItem item)
        {
            if(item == null || item.item == null || !item.item.TryGetComponent<Battery>(out var battery))
            {
                Logfile.Warning("Invalid battery item added to storage, removing...");
                Screen.Warning("Invalid battery item added to storage, removing...");

                if(item == null || item.item == null || batteryStorage == null || batteryStorage.container == null)
                    return;
                
                batteryStorage.container.RemoveItem(item.item, true);

                if(Inventory.main != null && Inventory.main.container != null && Inventory.main.container.HasRoomFor(item.item))
                {
                    Inventory.main.container.UnsafeAdd(item);
                }
                else item.item.Drop();

                RebuildBatteryStorageCache();
                batterySwapAttempted = false;
                return;
            }

            batteryStorageItems[item] = battery;
            batterySwapAttempted = false;
        }


        public void OnRemoveBatteryItem(InventoryItem item)
        {
            if(item == null || item.item == null)
            {
                Logfile.Warning("Attempted to remove invalid battery item from storage, ignoring...");
                Screen.Warning("Attempted to remove invalid battery item from storage, ignoring...");
                RebuildBatteryStorageCache();
                return;
            }

            batteryStorageItems.Remove(item);
            batterySwapAttempted = false;
        }


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

            bool modulePrimaryHeld = GameInputHandler.GetButtonHeld(SeaglideUpgradesModules.OpenModuleStorage, GameInput.BindingSet.Primary);
            bool moduleSecondaryHeld = GameInputHandler.GetButtonHeld(SeaglideUpgradesModules.OpenModuleStorage, GameInput.BindingSet.Secondary);
            bool modulePrimaryDown = GameInputHandler.GetButtonDown(SeaglideUpgradesModules.OpenModuleStorage, GameInput.BindingSet.Primary);
            bool moduleSecondaryDown = GameInputHandler.GetButtonDown(SeaglideUpgradesModules.OpenModuleStorage, GameInput.BindingSet.Secondary);

            if(modulePrimaryHeld && moduleSecondaryHeld && (modulePrimaryDown || moduleSecondaryDown) && moduleStorage != null && !moduleStorage.open)
            {
                moduleStorage.Open();
            }

            bool batteryPrimaryHeld = GameInputHandler.GetButtonHeld(SeaglideUpgradesModules.OpenBatteryUpgradeStorage, GameInput.BindingSet.Primary);
            bool batterySecondaryHeld = GameInputHandler.GetButtonHeld(SeaglideUpgradesModules.OpenBatteryUpgradeStorage, GameInput.BindingSet.Secondary);
            bool batteryPrimaryDown = GameInputHandler.GetButtonDown(SeaglideUpgradesModules.OpenBatteryUpgradeStorage, GameInput.BindingSet.Primary);
            bool batterySecondaryDown = GameInputHandler.GetButtonDown(SeaglideUpgradesModules.OpenBatteryUpgradeStorage, GameInput.BindingSet.Secondary);

            if(batteryPrimaryHeld && batterySecondaryHeld && (batteryPrimaryDown || batterySecondaryDown) && batterySwapModules > 0 && batteryStorage != null && !batteryStorage.open)
            {
                batteryStorage.Open();
            }
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
            if(batterySwapModules < 1 || energyMixin == null)
                return;

            if(!energyMixin.IsDepleted())
            {
                batterySwapAttempted = false;
                return;
            }

            if(batterySwapAttempted)
                return;

            var bestBatteryItem = GetBestBatteryItem();

            if(bestBatteryItem == null || bestBatteryItem.item == null)
            {
                batterySwapAttempted = true;
                return;
            }

            var prevLightsActive = seaglide?.toggleLights?.lightsActive ?? false;
            var prevAllowBatteryReplacement = energyMixin.allowBatteryReplacement;

            batterySwapAttempted = true;
            energyMixin.allowBatteryReplacement = true;

            try
            {
                energyMixin.Select(bestBatteryItem);
            }
            catch(Exception ex)
            {
                Logfile.Error($"Failed to swap battery:\n{ex}");
                Screen.Warning("Failed to swap battery, see log for details.");
            }
            finally
            {
                energyMixin.allowBatteryReplacement = prevAllowBatteryReplacement;
            }

            if(seaglide?.toggleLights != null)
            {
                seaglide.toggleLights.SetLightsActive(prevLightsActive);
            }
        }


        public int CompareBatteries(Battery a, Battery b)
        {
            if(a == null && b == null)
                return 0;

            if(a == null)
                return -1;

            if(b == null)
                return 1;

            int charge = SeaglideUpgradesModules.config.BatterySwapChargePriority == 0 ? a.charge.CompareTo(b.charge) : b.charge.CompareTo(a.charge);

            int capacity = SeaglideUpgradesModules.config.BatterySwapCapacityPriority == 0 ? a.capacity.CompareTo(b.capacity) : b.capacity.CompareTo(a.capacity);

            return SeaglideUpgradesModules.config.BatterySwapPriority == 0 ? charge != 0 ? charge : capacity : capacity != 0 ? capacity : charge;
        }


        public InventoryItem GetBestBatteryItem()
        {
            InventoryItem bestItem = null;
            Battery bestBattery = null;
            List<InventoryItem> itemsToRemove = null;

            foreach(var kvp in batteryStorageItems)
            {
                var item = kvp.Key;
                var battery = kvp.Value;

                if(item == null || item.item == null || battery == null)
                {
                    itemsToRemove ??= [];
                    itemsToRemove.Add(item);
                    continue;
                }

                if(battery.charge <= 0f)
                    continue;

                if(bestItem == null || CompareBatteries(battery, bestBattery) > 0)
                {
                    bestItem = item;
                    bestBattery = battery;
                }
            }

            if(itemsToRemove != null)
            {
                foreach(var item in itemsToRemove)
                    batteryStorageItems.Remove(item);
            }

            return bestItem;
        }
    }
}