

namespace RamuneLib.Piracy.Patches
{
    public static class SeaglidePatch
    {
        public static void OnDraw(Player p)
        {
            p.playerController.seaglideForwardMaxSpeed *= 1.1f;
        }
    }
}