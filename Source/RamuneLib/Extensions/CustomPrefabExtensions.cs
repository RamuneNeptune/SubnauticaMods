

namespace RamuneLib.Extensions
{
    internal static class CustomPrefabExtensions
    {
        // Setting prefab recipe ------------------------------------------------------------------------------------------------------------------------------------------
        internal static CustomPrefab WithRecipe(this CustomPrefab customPrefab, RecipeData recipe)
        {
            customPrefab.SetRecipe(recipe);
            return customPrefab;
        }

        internal static CustomPrefab WithRecipe(this CustomPrefab customPrefab, RecipeData recipe, CraftTree.Type craftTreeType)
        {
            customPrefab.SetRecipe(recipe)
                .WithFabricatorType(craftTreeType);

            return customPrefab;
        }


        internal static CustomPrefab WithRecipe(this CustomPrefab customPrefab, RecipeData recipe, CraftTree.Type craftTreeType, float craftingTime)
        {
            customPrefab.SetRecipe(recipe)
                .WithFabricatorType(craftTreeType)
                .WithCraftingTime(craftingTime);

            return customPrefab;
        }


        internal static CustomPrefab WithRecipe(this CustomPrefab customPrefab, RecipeData recipe, CraftTree.Type craftTreeType, params string[] stepsToFabricator)
        {
            customPrefab.SetRecipe(recipe)
                .WithFabricatorType(craftTreeType)
                .WithStepsToFabricatorTab(stepsToFabricator);

            return customPrefab;
        }


        internal static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename, CraftTree.Type craftTreeType, float craftingTime, params string[] stepsToFabricator)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename))
                .WithFabricatorType(craftTreeType)
                .WithStepsToFabricatorTab(stepsToFabricator)
                .WithCraftingTime(craftingTime);

            return customPrefab;
        }


        internal static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename, CraftTree.Type craftTreeType, params string[] stepsToFabricator)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename))
                .WithFabricatorType(craftTreeType)
                .WithStepsToFabricatorTab(stepsToFabricator);

            return customPrefab;
        }


        internal static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename, CraftTree.Type craftTreeType)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename))
                .WithFabricatorType(craftTreeType);

            return customPrefab;
        }


        internal static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename, float craftingTime)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename))
                .WithCraftingTime(craftingTime);

            return customPrefab;
        }


        internal static CustomPrefab WithJsonRecipe(this CustomPrefab customPrefab, string filename)
        {
            customPrefab.SetRecipeFromJson(JsonUtils.GetJsonRecipe(filename));
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab EquipmentType -----------------------------------------------------------------------------------------------------------------------------------

        internal static CustomPrefab WithEquipment(this CustomPrefab customPrefab, EquipmentType equipmentType)
        {
            customPrefab.SetEquipment(equipmentType);
            return customPrefab;
        }

        internal static CustomPrefab WithEquipment(this CustomPrefab customPrefab, EquipmentType equipmentType, out EquipmentGadget equipmentGadget)
        {
            equipmentGadget = customPrefab.SetEquipment(equipmentType);
            return customPrefab;
        }


        internal static CustomPrefab WithEquipmentAndQuickSlotType(this CustomPrefab customPrefab, EquipmentType equipmentType, QuickSlotType quickSlotType)
        {
            customPrefab.SetEquipment(equipmentType).WithQuickSlotType(quickSlotType);
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab as vehicle upgrade/module ------------------------------------------------------------------------------------------------------------------------

        internal static CustomPrefab WithVehicleUpgradeModule(this CustomPrefab customPrefab, EquipmentType equipmentType, QuickSlotType quickSlotType, out UpgradeModuleGadget upgradeModuleGadget)
        {
            upgradeModuleGadget = customPrefab.SetVehicleUpgradeModule(equipmentType, quickSlotType);
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab unlock TechType ---------------------------------------------------------------------------------------------------------------------------------

        internal static CustomPrefab WithUnlock(this CustomPrefab customPrefab, TechType techType)
        {
            customPrefab.SetUnlock(techType);
            return customPrefab;
        }


        internal static CustomPrefab WithAutoUnlock(this CustomPrefab customPrefab)
        {
            KnownTechHandler.UnlockOnStart(customPrefab.Info.TechType);
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab size in inventory -------------------------------------------------------------------------------------------------------------------------------

        internal static CustomPrefab WithSize(this CustomPrefab customPrefab, int x, int y)
        {
            customPrefab.Info.WithSizeInInventory(new Vector2int(x, y));
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Setting prefab PDA category ------------------------------------------------------------------------------------------------------------------------------------

        internal static CustomPrefab WithPDACategory(this CustomPrefab customPrefab, TechGroup techGroup, TechCategory techCategory)
        {
            customPrefab.SetPdaGroupCategory(techGroup, techCategory);
            return customPrefab;
        }


        internal static CustomPrefab WithPDACategoryAfter(this CustomPrefab customPrefab, TechGroup techGroup, TechCategory techCategory, TechType target)
        {
            customPrefab.SetPdaGroupCategoryAfter(techGroup, techCategory, target);
            return customPrefab;
        }


        internal static CustomPrefab WithPDACategoryBefore(this CustomPrefab customPrefab, TechGroup techGroup, TechCategory techCategory, TechType target)
        {
            customPrefab.SetPdaGroupCategoryBefore(techGroup, techCategory, target);
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // Turning prefab into a crafter (e.g. fabricator, workbench, etc..) ----------------------------------------------------------------------------------------------

        internal static CustomPrefab WithFabricator(this CustomPrefab customPrefab, out CraftTree.Type craftTreeType)
        {
            customPrefab.CreateFabricator(out CraftTree.Type _craftTreeType);
            craftTreeType = _craftTreeType;
            return customPrefab;
        }


        internal static CustomPrefab WithFabricator(this CustomPrefab customPrefab, out FabricatorGadget fabricatorGadget)
        {
            fabricatorGadget = customPrefab.CreateFabricator(out _);
            return customPrefab;
        }


        internal static CustomPrefab WithFabricator(this CustomPrefab customPrefab, out CraftTree.Type craftTreeType, out FabricatorGadget fabricatorGadget)
        {
            fabricatorGadget = customPrefab.CreateFabricator(out CraftTree.Type _craftTreeType);
            craftTreeType = _craftTreeType;
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------

        internal static CustomPrefab WithFragment(this CustomPrefab customPrefab, out ScanningGadget scanningGadget, TechType blueprint, float scanTime, int fragmentsToScan = 1, string encyKey = null, bool destroyAfterScan = true, bool isFragment = true)
        {
            scanningGadget = customPrefab.CreateFragment(blueprint, scanTime, fragmentsToScan, encyKey, destroyAfterScan, isFragment);
            return customPrefab;
        }

        internal static CustomPrefab WithSpawns(this CustomPrefab customPrefab, params SpawnLocation[] spawnLocations)
        {
            customPrefab.SetSpawns(spawnLocations);
            return customPrefab;
        }

        internal static CustomPrefab WithSpawns(this CustomPrefab customPrefab, params LootDistributionData.BiomeData[] biomesToSpawnIn)
        {
            customPrefab.SetSpawns(biomesToSpawnIn);
            return customPrefab;
        }

        internal static CustomPrefab WithSpawns(this CustomPrefab customPrefab, WorldEntityInfo entityInfo, params LootDistributionData.BiomeData[] biomesToSpawnIn)
        {
            customPrefab.SetSpawns(entityInfo, biomesToSpawnIn);
            return customPrefab;
        }

        internal static CustomPrefab WithCreatureEgg(this CustomPrefab customPrefab, out EggGadget eggGadget)
        {
            eggGadget = customPrefab.CreateCreatureEgg();
            return customPrefab;
        }

        internal static CustomPrefab WithCreatureEgg(this CustomPrefab customPrefab, int requiredAcuSize, out EggGadget eggGadget)
        {
            eggGadget = customPrefab.CreateCreatureEgg(requiredAcuSize);
            return customPrefab;
        }

        internal static CustomPrefab WithGameObject(this CustomPrefab customPrefab, TechType techTypeToClone)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, techTypeToClone));
            return customPrefab;
        }

        internal static CustomPrefab WithGameObject(this CustomPrefab customPrefab, TechType techTypeToClone, Action<GameObject> modifyPrefab)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, techTypeToClone) { ModifyPrefab = modifyPrefab });
            return customPrefab;
        }

        internal static CustomPrefab WithGameObject(this CustomPrefab customPrefab, string classIdToClone)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, classIdToClone));
            return customPrefab;
        }

        internal static CustomPrefab WithGameObject(this CustomPrefab customPrefab, string classIdToClone, Action<GameObject> modifyPrefab)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, classIdToClone) { ModifyPrefab = modifyPrefab });
            return customPrefab;
        }

        internal static CustomPrefab WithGameObject(this CustomPrefab customPrefab, AssetReferenceGameObject assetReferenceToClone)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, assetReferenceToClone));
            return customPrefab;
        }

        internal static CustomPrefab WithGameObject(this CustomPrefab customPrefab, AssetReferenceGameObject assetReferenceToClone, Action<GameObject> modifyPrefab)
        {
            customPrefab.SetGameObject(new CloneTemplate(customPrefab.Info, assetReferenceToClone) { ModifyPrefab = modifyPrefab });
            return customPrefab;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------




        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------

        internal static string Id(this CustomPrefab customPrefab) => customPrefab.Info.ClassID;


        internal static string DisplayName(this CustomPrefab customPrefab) => Language.main?.Get(customPrefab.Info.TechType) ?? customPrefab.Info.TechType.AsString();


        internal static string Description(this CustomPrefab customPrefab) => Language.main?.Get("Tooltip_" + customPrefab.Info.TechType) ?? "Tooltip_" + customPrefab.Info.TechType.AsString();


        internal static Sprite Sprite(this CustomPrefab customPrefab) => SpriteManager.Get(customPrefab.Info.TechType, SpriteManager.Get(TechType.None));

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}