

namespace RamuneLib.Piracy.Patches
{
    public static class SeaglidePatch
    {
        public static void OnDraw(Seaglide __instance, Player p)
        {
            p.playerController.seaglideForwardMaxSpeed *= 1.1f;
        }
    }
}