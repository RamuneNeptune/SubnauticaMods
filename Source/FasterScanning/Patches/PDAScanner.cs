

namespace Ramune.FasterScanning.Patches
{
    [HarmonyPatch(typeof(PDAScanner))]
    public static class PDAScannerPatch
    {
        public static float Multiplier () => FasterScanning.config.Multiplier;


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