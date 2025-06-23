

namespace RamuneLib.Piracy.Patches
{
    public static class ChargerPatch
    {
        public static void Initialize(Charger __instance)
        {
            __instance.chargeSpeed *= 0.5f;
        }
    }
}