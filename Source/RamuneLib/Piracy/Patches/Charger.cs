

namespace RamuneLib.Piracy.Patches
{
    internal static class ChargerPatch
    {
        internal static void Initialize(Charger __instance)
        {
            __instance.chargeSpeed *= 0.5f;
        }
    }
}