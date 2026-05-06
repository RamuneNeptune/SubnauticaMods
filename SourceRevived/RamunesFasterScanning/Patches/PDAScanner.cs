

namespace Ramune.RamunesFasterScanning.Patches
{
    [HarmonyPatch(typeof(PDAScanner))]
    public static class PDAScannerPatch
    {
        public static float Multiplier() => RamunesFasterScanning.config.MultiplierChoice == 0 ? RamunesFasterScanning.config.Multiplier : RamunesFasterScanning.config.MultiplierMega;


        [HarmonyPatch(nameof(PDAScanner.Scan)), HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Scan(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                .MatchForward(false, new CodeMatch(OpCodes.Call, AccessTools.PropertyGetter(typeof(Time), nameof(Time.deltaTime))))
                .Advance(1)
                .InsertAndAdvance(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PDAScannerPatch), nameof(Multiplier))), new CodeInstruction(OpCodes.Mul))
                .InstructionEnumeration();
        }
    }
}