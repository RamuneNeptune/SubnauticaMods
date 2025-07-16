

namespace Ramune.InventorySpaceWarnings.Patches
{
    [HarmonyPatch(typeof(BreakableResource))]
    public static class BreakableResourcePatch
    {
        [HarmonyPatch(nameof(BreakableResource.OnHandHover)), HarmonyPrefix]
        public static bool OnHandHover(BreakableResource __instance)
        {
            if(Player.main.HasInventoryRoom(1, 1))
                return true;

            if(!InventorySpaceWarnings.config.OutcropsDoWarning && !InventorySpaceWarnings.config.OutcropsDoPreventCollection && !InventorySpaceWarnings.config.OutcropsDoIndicator)
                return true;

            if(InventorySpaceWarnings.config.OutcropsDoPreventCollection)
            {
                if(HandTargetPatch.ResourceTrackerCache.TryGetValue(__instance, out var tracker))
                {
                    HandReticle.main.SetText(HandReticle.TextType.Hand, tracker.techType.Name(), false);
                }
                else
                {
                    HandReticle.main.SetText(HandReticle.TextType.Hand, __instance.breakText, true, GameInput.Button.LeftHand);

                    Screen.Warning($"Failed to find ResourceTracker on '{__instance.name}'");
                }
            }
            else
            {
                HandReticle.main.SetText(HandReticle.TextType.Hand, __instance.breakText, true, GameInput.Button.LeftHand);
            }

            if(InventorySpaceWarnings.config.OutcropsDoWarning)
            {
                HandReticle.main.SetText(HandReticle.TextType.HandSubscript, "InventoryFull", true);
            }

            if(InventorySpaceWarnings.config.OutcropsDoIndicator)
            {
                if(!InventorySpaceWarnings.config.OutcropsDoPreventCollection)
                {
                    HandReticle.main.SetText(HandReticle.TextType.Hand, __instance.breakText, true, GameInput.Button.LeftHand);
                }

                HandReticle.main.SetIcon(HandReticle.IconType.HandDeny);
            }

            return false;
        }
    }
}