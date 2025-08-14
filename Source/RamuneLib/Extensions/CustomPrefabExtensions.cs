

namespace RamuneLib.Extensions
{
    public static class CustomPrefabExtensions
    {
        // Setting prefab recipe ------------------------------------------------------------------------------------------------------------------------------------------
        public static CustomPrefab WithRecipe(this CustomPrefab customPrefab, RecipeData recipe)
        {
            customPrefab.SetRecipe(recipe);
            return customPrefab;
        }

        public static CustomPrefab WithRecipe(this CustomPrefab customPrefab, RecipeData recipe, CraftTree.Type craftTreeType)
        {
            customPrefab.SetRecipe(recipe)
                .WithFabricatorType(craftTreeType);

            return customPrefab;
        }


        public static CustomPrefab WithRecipe(this CustomPrefab customPrefab, RecipeData recipe, CraftTree.Type craftTreeType, float craftingTime)
        {
            customPrefab.SetRecipe(recipe)
                .WithFabricatorType(craftTreeType)
                .WithCraftingTime(craftingTime);

            return customPrefab;
        }


        public static CustomPrefab WithRecipe(this CustomPrefab customPrefab, RecipeData recipe, CraftTree.Type craftTreeType, params string[] stepsToFabricator)
        {
            customPrefab.SetRecipe(recipe)
                .WithFabricatorType(craftTreeType)
                .WithStepsToFabricatorTab(stepsToFabricator);

            return customPrefab;
        }


        public static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename, CraftTree.Type craftTreeType, float craftingTime, params string[] stepsToFabricator)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename))
                .WithFabricatorType(craftTreeType)
                .WithStepsToFabricatorTab(stepsToFabricator)
                .WithCraftingTime(craftingTime);

            return customPrefab;
        }


        public static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename, CraftTree.Type craftTreeType, params string[] stepsToFabricator)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename))
                .WithFabricatorType(craftTreeType)
                .WithStepsToFabricatorTab(stepsToFabricator);

            return customPrefab;
        }


        public static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename, CraftTree.Type craftTreeType)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename))
                .WithFabricatorType(craftTreeType);

            return customPrefab;
        }


        public static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename, float craftingTime)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename))
                .WithCraftingTime(craftingTime);

            return customPrefab;
        }


        public static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename));
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab EquipmentType -----------------------------------------------------------------------------------------------------------------------------------

        public static CustomPrefab WithEquipment(this CustomPrefab customPrefab, EquipmentType equipmentType)
        {
            customPrefab.SetEquipment(equipmentType);
            return customPrefab;
        }

        public static CustomPrefab WithEquipment(this CustomPrefab customPrefab, EquipmentType equipmentType, out EquipmentGadget equipmentGadget)
        {
            equipmentGadget = customPrefab.SetEquipment(equipmentType);
            return customPrefab;
        }


        public static CustomPrefab WithEquipmentAndQuickSlotType(this CustomPrefab customPrefab, EquipmentType equipmentType, QuickSlotType quickSlotType)
        {
            customPrefab.SetEquipment(equipmentType).WithQuickSlotType(quickSlotType);
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab as vehicle upgrade/module ------------------------------------------------------------------------------------------------------------------------

        public static CustomPrefab WithVehicleUpgradeModule(this CustomPrefab customPrefab, EquipmentType equipmentType, QuickSlotType quickSlotType, out UpgradeModuleGadget upgradeModuleGadget)
        {
            upgradeModuleGadget = customPrefab.SetVehicleUpgradeModule(equipmentType, quickSlotType);
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab unlock TechType ---------------------------------------------------------------------------------------------------------------------------------

        public static CustomPrefab WithUnlock(this CustomPrefab customPrefab, TechType techType)
        {
            customPrefab.SetUnlock(techType);
            return customPrefab;
        }


        public static CustomPrefab WithAutoUnlock(this CustomPrefab customPrefab)
        {
            KnownTechHandler.UnlockOnStart(customPrefab.Info.TechType);
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab size in inventory -------------------------------------------------------------------------------------------------------------------------------

        public static CustomPrefab WithSize(this CustomPrefab customPrefab, int x, int y)
        {
            customPrefab.Info.WithSizeInInventory(new Vector2int(x, y));
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab PDA category ------------------------------------------------------------------------------------------------------------------------------------

        public static CustomPrefab WithPDACategory(this CustomPrefab customPrefab, TechGroup techGroup, TechCategory techCategory)
        {
            customPrefab.SetPdaGroupCategory(techGroup, techCategory);
            return customPrefab;
        }


        public static CustomPrefab WithPDACategoryAfter(this CustomPrefab customPrefab, TechGroup techGroup, TechCategory techCategory, TechType target)
        {
            customPrefab.SetPdaGroupCategoryAfter(techGroup, techCategory, target);
            return customPrefab;
        }


        public static CustomPrefab WithPDACategoryBefore(this CustomPrefab customPrefab, TechGroup techGroup, TechCategory techCategory, TechType target)
        {
            customPrefab.SetPdaGroupCategoryBefore(techGroup, techCategory, target);
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Turning prefab into a crafter (e.g. fabricator, workbench, etc..) ----------------------------------------------------------------------------------------------

        public static CustomPrefab WithFabricator(this CustomPrefab customPrefab, out CraftTree.Type craftTreeType)
        {
            customPrefab.CreateFabricator(out CraftTree.Type _craftTreeType);
            craftTreeType = _craftTreeType;
            return customPrefab;
        }

        public static CustomPrefab WithFabricator(this CustomPrefab customPrefab, out CraftTree.Type craftTreeType, out FabricatorGadget fabricatorGadget)
        {
            fabricatorGadget = customPrefab.CreateFabricator(out CraftTree.Type _craftTreeType);
            craftTreeType = _craftTreeType;
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static CustomPrefab WithFragment(this CustomPrefab customPrefab, out ScanningGadget scanningGadget, TechType blueprint, float scanTime, int fragmentsToScan = 1, string encyKey = null, bool destroyAfterScan = true, bool isFragment = true)
        {
            scanningGadget = customPrefab.CreateFragment(blueprint, scanTime, fragmentsToScan, encyKey, destroyAfterScan, isFragment);
            return customPrefab;
        }

        public static CustomPrefab WithSpawns(this CustomPrefab customPrefab, params SpawnLocation[] spawnLocations)
        {
            customPrefab.SetSpawns(spawnLocations);
            return customPrefab;
        }

        public static CustomPrefab WithSpawns(this CustomPrefab customPrefab, params LootDistributionData.BiomeData[] biomesToSpawnIn)
        {
            customPrefab.SetSpawns(biomesToSpawnIn);
            return customPrefab;
        }

        public static CustomPrefab WithSpawns(this CustomPrefab customPrefab, WorldEntityInfo entityInfo, params LootDistributionData.BiomeData[] biomesToSpawnIn)
        {
            customPrefab.SetSpawns(entityInfo, biomesToSpawnIn);
            return customPrefab;
        }

        public static CustomPrefab WithCreatureEgg(this CustomPrefab customPrefab, out EggGadget eggGadget)
        {
            eggGadget = customPrefab.CreateCreatureEgg();
            return customPrefab;
        }

        public static CustomPrefab WithCreatureEgg(this CustomPrefab customPrefab, int requiredAcuSize, out EggGadget eggGadget)
        {
            eggGadget = customPrefab.CreateCreatureEgg(requiredAcuSize);
            return customPrefab;
        }

        public static CustomPrefab WithGameObject(this CustomPrefab customPrefab, TechType techTypeToClone)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, techTypeToClone));
            return customPrefab;
        }

        public static CustomPrefab WithGameObject(this CustomPrefab customPrefab, TechType techTypeToClone, Action<GameObject> modifyPrefab)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, techTypeToClone) { ModifyPrefab = modifyPrefab });
            return customPrefab;
        }

        public static CustomPrefab WithGameObject(this CustomPrefab customPrefab, string classIdToClone)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, classIdToClone));
            return customPrefab;
        }

        public static CustomPrefab WithGameObject(this CustomPrefab customPrefab, string classIdToClone, Action<GameObject> modifyPrefab)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, classIdToClone) { ModifyPrefab = modifyPrefab });
            return customPrefab;
        }

        public static CustomPrefab WithGameObject(this CustomPrefab customPrefab, AssetReferenceGameObject assetReferenceToClone)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, assetReferenceToClone));
            return customPrefab;
        }

        public static CustomPrefab WithGameObject(this CustomPrefab customPrefab, AssetReferenceGameObject assetReferenceToClone, Action<GameObject> modifyPrefab)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, assetReferenceToClone) { ModifyPrefab = modifyPrefab });
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string Id(this CustomPrefab customPrefab) => customPrefab.Info.ClassID;


        public static string DisplayName(this CustomPrefab customPrefab) => Language.main.Get(customPrefab.Info.TechType);


        public static string Description(this CustomPrefab customPrefab) => Language.main.Get("Tooltip_" + customPrefab.Info.TechType);


        public static Sprite Sprite(this CustomPrefab customPrefab) => SpriteManager.Get(customPrefab.Info.TechType);

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}