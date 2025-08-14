

namespace Ramune.NoPassiveScannerRoomPowerDrain.Patches
{
    [HarmonyPatch(typeof(MapRoomFunctionality))]
    public static class MapRoomFunctionalityPatch
    {
        [HarmonyPatch(nameof(MapRoomFunctionality.UpdateScanning)), HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> UpdateScanning(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                .MatchForward(false, new CodeMatch(OpCodes.Ldc_R4, 0.15f))
                .SetOperand(0f)
                .InstructionEnumeration();
        }
    }
}