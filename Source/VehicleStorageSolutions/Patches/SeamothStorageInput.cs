

namespace Ramune.VehicleStorageSolutions.Patches
{
    [HarmonyPatch(typeof(SeamothStorageInput))]
    public static class SeamothStorageInputPatches
    {
        public static int CurrentSlotID = 0;


        [HarmonyPatch(nameof(SeamothStorageInput.OpenPDA)), HarmonyPrefix]
        public static void OpenPDA(SeamothStorageInput __instance) => CurrentSlotID = __instance.slotID;


        [HarmonyPatch(nameof(SeamothStorageInput.OpenFromExternal)), HarmonyPrefix]
        public static void OpenFromExternal(SeamothStorageInput __instance) => CurrentSlotID = __instance.slotID;
    }
}