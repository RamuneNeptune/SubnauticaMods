

namespace RamuneLib.Piracy.Patches
{
    public static class ChargerPatch
    {
        public static void Initialize(Charger __instance)
        {
            ///Chargers recharge items 50% slower.
            __instance.chargeSpeed *= 0.5f;
        }
    }
}