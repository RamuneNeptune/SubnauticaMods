

namespace Ramune.InventorySpaceWarnings.Patches
{
    [HarmonyPatch(typeof(HandTarget))]
    public static class HandTargetPatch
    {
        public static Dictionary<BreakableResource, ResourceTracker> ResourceTrackerCache = new();


        [HarmonyPatch(nameof(HandTarget.Awake)), HarmonyPostfix]
        public static void Awake(HandTarget __instance)
        {
            if(__instance is not BreakableResource breakable)
                return;

            if(!__instance.TryGetComponent<ResourceTracker>(out var tracker))
                return;
            
            if(ResourceTrackerCache.ContainsKey(breakable))
                return;
            
            ResourceTrackerCache.Add(breakable, tracker);
        }
    }
}