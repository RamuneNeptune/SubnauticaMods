

namespace RamuneLib.Piracy.Patches
{
    internal static class BreakableResourcePatch
    {
        internal static void BreakIntoResources(BreakableResource __instance)
        {
            Player.main.liveMixin.TakeDamage(1f);

            if(UnityEngine.Random.value <= 0.1f)
                return;

            CoroutineHost.StartCoroutine(Piracy.SpawnCrashfish(__instance.transform.position));
        }
    }
}