

namespace RamuneLib.Piracy.Patches
{
    internal static class ErrorMessagePatch
    {
        internal static void GetEntry(ref TextMeshProUGUI __result)
        {
            if(__result == null)
                return;

            __result.enableWordWrapping = false;
            __result.overflowMode = TextOverflowModes.Overflow;
            __result.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 4000f);
        }
    }
}