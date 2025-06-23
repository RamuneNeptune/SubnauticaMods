

namespace RamuneLib.Piracy.Patches
{
    public static class SeaglidePatch
    {
        public static void OnDraw(Seaglide __instance, Player p)
        {
            ///Seaglide acceleration is increased by 0.005% everytime it is equipped.
            p.playerController.seaglideForwardMaxSpeed *= 1.005f;
        }
    }
}