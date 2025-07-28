

namespace RamuneLib.Piracy.Patches
{
    public static class BreakableResourcePatch
    {
        public static void BreakIntoResources(BreakableResource __instance)
        {
            Player.main.liveMixin.TakeDamage(1f);

            if(UnityEngine.Random.value <= 0.1f)
                return;

            CoroutineHost.StartCoroutine(Piracy.SpawnCrashfish(__instance.transform.position));
        }
    }
}