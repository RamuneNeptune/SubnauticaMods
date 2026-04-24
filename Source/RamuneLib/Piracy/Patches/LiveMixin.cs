

namespace RamuneLib.Piracy.Patches
{
    internal static class LiveMixinPatch
    {
        internal static void TakeDamage(LiveMixin __instance, float originalDamage, Vector3 position = default, DamageType type = DamageType.Normal, GameObject dealer = null)
        {
            if(!__instance.player)
            {
                return;
            }

            Player.main.transform.localScale *= 0.99f;

            Subtitle("pirate_shrink", "I feel.. smaller?", duration:3);
        }
    }
}