﻿

namespace Ramune.BuildableBrainCoral.Items
{
    public static class BuildableBrainCoral
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("BuildableBrainCoral", "Brain coral", "Colony of microscopic organisms which filter carbon dioxide from the environment, using the carbon to build the colony, and expelling oxygen from specialized exhaust funnels.", ImageUtils.GetSprite(TechType.BrainCoral))
            .WithPDACategory(TechGroup.ExteriorModules, TechCategory.ExteriorOther)
            .WithJsonRecipe("BuildableBrainCoral")
            .WithAutoUnlock();


        public static void Patch()
        {
            var clone = new CloneTemplate(Prefab.Info, TechType.PurpleBrainCoral)
            {
                ModifyPrefab = go =>
                {
                    go.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Far;

                    var model = go.transform.Find("Coral_reef_purple_brain_coral_01").gameObject;

                    model.transform.rotation = Quaternion.Euler(-90, 0, 0);

                    Utility.ConstructableFlags constructableFlags = Utility.ConstructableFlags.Outside | Utility.ConstructableFlags.Ground | Utility.ConstructableFlags.Rotatable;

                    Utility.PrefabUtils.AddConstructable(go, Prefab.Info.TechType, constructableFlags, model);

                    LargeWorldEntity.Register(go);
                }
            };

            CraftDataHandler.SetBackgroundType(Prefab.Info.TechType, BackgroundType.PlantWater);

            Prefab.SetGameObject(clone);

            Prefab.Register();
        }
    }
}