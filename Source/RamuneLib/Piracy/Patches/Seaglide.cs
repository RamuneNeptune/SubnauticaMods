

namespace RamuneLib.Piracy.Patches
{
    internal static class SeaglidePatch
    {
        internal static void OnDraw(Player p)
        {
            p.playerController.seaglideForwardMaxSpeed *= 1.1f;
        }
    }
}