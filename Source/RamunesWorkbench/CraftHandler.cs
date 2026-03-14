﻿

namespace Ramune.RamunesWorkbench
{
    public static class CraftHandler
    {
        public static CraftTree.Type CraftTreeType => Buildables.RamunesWorkbench.craftTreeType;


        public static IEnumerator Initialize()
        {
            while(CraftTreeType is CraftTree.Type.None)
                yield return null;
            
            AddTab("Tools", ImageUtils.GetSprite("TabTools"));
            AddTab("Equipment", ImageUtils.GetSprite("TabEquipment"));
            AddTab("Consumables", ImageUtils.GetSprite("TabConsumables"));

            AddTab("Power", ImageUtils.GetSprite("TabPower"));
            AddTab("Batteries", ImageUtils.GetSprite(TechType.Battery), "Power");
            AddTab("PowerCells", "Power cells", ImageUtils.GetSprite(TechType.PowerCell), "Power");

            AddTab("Modules", ImageUtils.GetSprite("TabModules"));
            AddTab("Seamoth", ImageUtils.GetSprite(TechType.Seamoth), "Modules");
            AddTab("Prawn suit", ImageUtils.GetSprite(TechType.Exosuit), "Modules");
            AddTab("Cyclops", ImageUtils.GetSprite(TechType.Cyclops), "Modules");


            Logfile.Info("<---- RAMUNE'S WORKBENCH PRCOESSING START ---->");
            Logfile.Info("");


            if(IsLoaded("MegaO2Tank"))
            {
                AddTab("Tanks", ImageUtils.GetSprite(TechType.PlasteelTank), "Equipment");
                AddCraft("MegaO2Tank", "Equipment", "Tanks");
                Logfile.Info(">> 'Mega O2 Tank' items processed");
            }


            if(IsLoaded("SeaglideUpgrades"))
            {
                AddTab("Seaglides", ImageUtils.GetSprite(TechType.Seaglide), "Equipment");
                AddCraft("SeaglideMK1", "Equipment", "Seaglides");
                AddCraft("SeaglideMK2", "Equipment", "Seaglides");
                AddCraft("SeaglideMK3", "Equipment", "Seaglides");
                Logfile.Info(">> 'Seaglide Upgrades' items processed");
            }


            if(IsLoaded("LithiumBatteries"))
            {
                AddCraft("IonBatteryAlt", "Power", "Batteries");
                AddCraft("IonPowerCellAlt", "Power", "PowerCells");
                AddCraft("LithiumBattery", "Power", "Batteries");
                AddCraft("LithiumPowerCell", "Power", "PowerCells");
                Logfile.Info(">> 'Lithium Batteries' items processed");
            }


            if(IsLoaded("KioniteBatteries"))
            {
                AddCraft("KioniteBattery", "Power", "Batteries");
                AddCraft("KionitePowerCell", "Power", "PowerCells");
                Logfile.Info(">> 'Kionite Batteries' items processed");
            }


            if(IsLoaded("OxygenCanisters"))
            {
                AddCraft("OxygenCanister", "Consumables");
                AddCraft("LargeOxygenCanister", "Consumables");
                Logfile.Info(">> 'Oxygen Canisters' items processed");
            }


            if(IsLoaded("SeamothTurbo"))
            {
                AddCraft("SeamothTurbo", "Modules", "Seamoth");
                Logfile.Info(">> 'Seamoth Turbo Module' items processed");
            }


            if(IsLoaded("SeamothLeviathanRadar"))
            {
                AddCraft("SeamothLeviathanRadar", "Modules", "Seamoth");
                Logfile.Info(">> 'Seamoth Leviathan Radar' items processed");
            }


            if(IsLoaded("StasisModule"))
            {
                AddCraft("StasisModule", "Modules", "Seamoth");
                Logfile.Info(">> 'Stasis Module' items processed");
            }

            Logfile.Info("");
            Logfile.Info("<---- RAMUNE'S WORKBENCH PRCOESSING END ---->");
        }


        public static IEnumerator WaitForChainloader()
        {
            FieldInfo loaded = typeof(BepInEx.Bootstrap.Chainloader).GetField("_loaded", BindingFlags.NonPublic | BindingFlags.Static);

            yield return new WaitUntil(() => (bool)loaded.GetValue(null));

            CoroutineHost.StartCoroutine(Initialize());
        }


        public static bool IsLoaded(string guid)
        {
            if(BepInEx.Bootstrap.Chainloader.PluginInfos.TryGetValue("com.ramune." + guid, out _))
                return true;

            return false;
        }


        public static void AddCraft(string techType)
        {
            if(!EnumHandler.TryGetValue(techType, out TechType _techType)) return;
            CraftTreeHandler.AddCraftingNode(CraftTreeType, _techType);
        }


        public static void AddCraft(string techType, params string[] stepsToTab)
        {
            if(!EnumHandler.TryGetValue(techType, out TechType _techType)) return;
            CraftTreeHandler.AddCraftingNode(CraftTreeType, _techType, stepsToTab);
        }


        public static void AddTab(string name, Sprite sprite) => CraftTreeHandler.AddTabNode(CraftTreeType, name, name, sprite);


        public static void AddTab(string name, Sprite sprite, params string[] stepsToTab) => CraftTreeHandler.AddTabNode(CraftTreeType, name, name, sprite, stepsToTab);


        public static void AddTab(string id, string name, Sprite sprite, params string[] stepsToTab) => CraftTreeHandler.AddTabNode(CraftTreeType, id, name, sprite, stepsToTab);
    }
}