

namespace Ramune.FasterScanning.Patches
{
    [HarmonyPatch(typeof(PDAScanner))]
    public static class PDAScannerPatch
    {
        [HarmonyPatch(nameof(PDAScanner.Scan)), HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Scan(IEnumerable<CodeInstruction> instructions)
        {
            Screen.Info("Faster Scanning is enabled. Scanning will be faster now.");

            return new CodeMatcher(instructions)
                .MatchForward(false, new CodeMatch(OpCodes.Call, AccessTools.PropertyGetter(typeof(Time), nameof(Time.deltaTime))), new CodeMatch(OpCodes.Add))
                .Advance(1)
                .Insert(new CodeInstruction(OpCodes.Ldc_R4, FasterScanning.config.Multiplier), new CodeInstruction(OpCodes.Mul))
                .InstructionEnumeration();
        }
    }
}

